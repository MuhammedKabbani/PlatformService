using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PlatformService.ASyncDataServices.RabbitMQ;

public class RabbtiMQMessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private IConnection? _connection;
    private IChannel? _channel;
    private ILogger<RabbtiMQMessageBusClient> _logger;
    public RabbtiMQMessageBusClient(IConfiguration configuration, ILogger<RabbtiMQMessageBusClient> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    private async Task RabbitMq_ConnectionShutDown(object sender, ShutdownEventArgs e)
    {
        await Task.Run(() => System.Console.WriteLine("RabbitMQ Connection Shutdown"));
    }

    public async Task PublishToPlatformAsync(PlatformPublishedDto platformPublishDto)
    {
        if (_connection == null)
            await Initlizle();
        
        var message = JsonSerializer.Serialize(platformPublishDto);
        if (_connection?.IsOpen ?? false)
        {
            _logger.LogInformation("RabbitMq sending message...");
            
            await SendMessageAsync(message);
            return;   
        }
    }

    private async Task Initlizle()
    {        
        var host = _configuration.GetSection("RabbitMQProps")["Host"] ?? string.Empty;
        var port = _configuration.GetSection("RabbitMQProps")["Port"] ?? string.Empty;
        
        var factory = new ConnectionFactory(){
            HostName = host,
            Port = Convert.ToInt32(port),
            UserName = "guest",
            Password = "guest"
        };
        try
        {
            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout, arguments: null);
            _connection.ConnectionShutdownAsync += RabbitMq_ConnectionShutDown;
        }
        catch (System.Exception ex)
        {
            _logger.LogError($"Could not connecto to the message bus {ex.Message}");
        }
    }

    private async Task SendMessageAsync(string message)
    {
        if (_channel == null)
            return;

        var body = Encoding.UTF8.GetBytes(message);
        await _channel.BasicPublishAsync(exchange: "trigger", routingKey: "", body);
        _logger.LogInformation("RabbitMQ message: {message} sent",message);
    }
    public async Task Dispose()
    {
        if (_channel == null || _connection == null)
            return;
        _logger.LogInformation("Disposing message bus...");
        if (_channel.IsOpen)
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }
    }
}