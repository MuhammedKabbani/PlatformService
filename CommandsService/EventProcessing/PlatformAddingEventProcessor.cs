
using System.Text.Json;
using AutoMapper;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.Data.Repositories.Concrate;
using CommandsService.Dtos;
using CommandsService.Models;
namespace CommandsService.EventProcessing;


public class PlatformAddingEventProcessor : IEventProcessor
{
    private readonly IMapper _mapper;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<PlatformAddingEventProcessor> _logger;

    public PlatformAddingEventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<PlatformAddingEventProcessor> logger)
    {
        _mapper = mapper;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    public async Task ProccessEventAsync(string message)
    {
        var eventType = DetermineEvent(message);
        if (eventType != EventType.PlatformPublished)
            return;

        await AddPlatfrom(message);
    }

    private async Task AddPlatfrom(string platformPublishedMessage)
    {
        await using (var scope =  _scopeFactory.CreateAsyncScope())
        {
            var platformRepo = scope.ServiceProvider.GetRequiredService<IPlatformRepository>();
            var paltformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

            try
            {
                var plat = _mapper.Map<Platform>(paltformPublishedDto);
                System.Console.WriteLine("Id = " + plat.Id);
                if (!await platformRepo.OriginalPlatformExsists(plat.OriginalId))
                {
                    await platformRepo.CreatePlatform(plat);
                }
                else
                {
                    _logger.LogWarning("Platform is already exsist.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Couldn't add platform to Db. {message}", ex.Message);
            }
        }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        _logger.LogInformation("Determining Event...");
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        if (eventType == null || eventType.Event != "Platform_Published")
        {
            _logger.LogInformation("couldn't determine event type");
            return EventType.Undetermined;
        }

        _logger.LogInformation("Platform poublished event detected");
        return EventType.PlatformPublished;
    }

}
public enum EventType
{
    PlatformPublished,
     Undetermined
}