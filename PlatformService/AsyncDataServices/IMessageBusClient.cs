using PlatformService.Dtos;

namespace PlatformService.ASyncDataServices;

public interface IMessageBusClient
{
    Task PublishToPlatformAsync(PlatformPublishedDto platformPublishDto);
    Task Dispose();
}