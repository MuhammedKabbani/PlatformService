using CommandsService.Data.Repositories.Abstract;
using CommandsService.Models;

namespace CommandsService.Data.Repositories.Concrate;

public class PlatformRepository : RepositoryBase<Platform>, IPlatformRepository
{
    public PlatformRepository(AppDbContext context) : base(context)
    {
    }

    public async Task CreatePlatform(Platform plat)
    {
        ArgumentNullException.ThrowIfNull(plat);
        
        var sameCommand = await AnyAsync(p => p.Name.Equals(plat.Name, StringComparison.InvariantCultureIgnoreCase));
        if (sameCommand)
            return;
        
        await AddAsync(plat);
        await SaveAsync();
    }

    public async Task<IEnumerable<Platform>> GetAllPlatformsAsync(bool trackChanges)
    {
        return await GetAllAsync(trackChanges);
    }

    public async Task<bool> PlatformExsists(int platformId)
    {
        return await AnyAsync(p => p.Id == platformId);
    }
}