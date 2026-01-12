
using System.Text;
using CommandsService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandsService.AsyncDataServices.RabbitMQ;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProccessor;
    private IConnection? _connection;
    private IChannel? _channel;
    private QueueDeclareOk? _queueName;
    private readonly ILogger<MessageBusSubscriber> _logger;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
    {
        _configuration = configuration;
        _eventProccessor = eventProcessor;
        _logger = logger;
    }
    private async Task InitializeRabbitMQ()
    {
        var host = _configuration.GetSection("RabbitMQProps")["Host"] ?? string.Empty;
        var port = _configuration.GetSection("RabbitMQProps")["Port"] ?? string.Empty;
        var factory = new ConnectionFactory()
        {
            HostName = host,
            Port = Convert.ToInt32(port),
        };
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout, arguments: null);
        _queueName = await _channel.QueueDeclareAsync();
        await _channel.QueueBindAsync(queue: _queueName, exchange: "trigger", routingKey: "");
        _logger.LogInformation("Listening on the message bus...");

        _connection.ConnectionShutdownAsync += RabbitMQ_ConnectionShutDown;

    }

    private async Task RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("Connection Shut Down.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_connection == null || _channel == null || _queueName == null)
            await InitializeRabbitMQ();
        
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ModuleHandle, ea) =>
        {
            _logger.LogInformation("Event recived.");
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            await _eventProccessor.ProccessEventAsync(notificationMessage);
        };
        await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
    }
    public override void Dispose()
    {
        if (_channel?.IsOpen ?? false)
        {
            _channel.CloseAsync();
            _connection?.CloseAsync();
        }
        base.Dispose();
    }
}