using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.Repositories.Abstract;
using PlatformService.Data.Repositories.Concrate;
namespace PlatformService.Extensions;


internal static class ServiceCollectionExtensions
{
    internal static void RegisterContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    }
    internal static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlatformRepository, PlatformRepository>();
    }
    internal static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}