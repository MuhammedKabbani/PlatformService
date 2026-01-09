using CommandsService.Data.Repositories.Abstract;
using CommandsService.Models;

namespace CommandsService.Data.Repositories.Concrate;

public class CommandRepository : RepositoryBase<Command>, ICommandRepository
{
    private readonly IPlatformRepository _platformRepo;
    public CommandRepository(AppDbContext context, IPlatformRepository platformRepo) : base(context)
    {
        _platformRepo = platformRepo;
    }

    public async Task CreateCommandAsync(int platformId, Command command)
    {       
        ArgumentNullException.ThrowIfNull(command);

        if (!await _platformRepo.PlatformExsists(platformId))
            return;
        
        command.PlatformId = platformId;

        await AddAsync(command);
        await SaveAsync();
    }

    public async Task<IEnumerable<Command>> GetAllCommandsByPlatformAsync(int platformId, bool trackChanges)
    {
        return await GetAllAsync(trackChanges, c => c.PlatformId == platformId);
    }

    public async Task<Command?> GetCommandAsync(int platformId, int commandId, bool trackChanges)
    {
        return await FirstOrDefaultAsync(c => c.Id == commandId && c.PlatformId == platformId, trackChanges);
    }
}