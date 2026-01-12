namespace CommandsService.EventProcessing;

public interface IEventProcessor
{
    Task ProccessEventAsync(string message);
}