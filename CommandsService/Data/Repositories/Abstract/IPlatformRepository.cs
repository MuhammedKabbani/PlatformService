using CommandsService.Models;

namespace CommandsService.Data.Repositories.Abstract;

public interface IPlatformRepository : IRepositoryBase<Platform>
{
    Task<IEnumerable<Platform>> GetAllPlatformsAsync(bool trackChanges);
    Task CreatePlatform(Platform plat);
    Task<bool> PlatformExsists(int platformId);
}