
using PlatformService.Data.Repositories.Abstract;

namespace PlatformService.Data.Repositories.Concrate;

public class PlatformRepository : RepositoryBase<PlatformService.Models.Platform>, IPlatformRepository
{
    public PlatformRepository(AppDbContext context) : base(context)
    {
    }
}

