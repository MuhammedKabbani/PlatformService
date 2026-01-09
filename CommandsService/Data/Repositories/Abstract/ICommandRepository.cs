using CommandsService.Models;

namespace CommandsService.Data.Repositories.Abstract;

public interface ICommandRepository : IRepositoryBase<Command>
{
    Task<IEnumerable<Command>> GetAllCommandsByPlatformAsync(int platformId, bool trackChanges);
    Task<Command?> GetCommandAsync(int platformId, int commandId, bool trackChanges);
    Task CreateCommandAsync(int platformId, Command command);
}