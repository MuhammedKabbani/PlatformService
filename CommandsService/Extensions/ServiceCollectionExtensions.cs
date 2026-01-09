using Microsoft.EntityFrameworkCore;
using CommandsService.Data;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.Data.Repositories.Concrate;
namespace CommandsService.Extensions;


internal static class ServiceCollectionExtensions
{
        internal static void RegisterDbContext(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        if (environment.IsProduction())
            services.RegisterSqlContext(configuration);
        else
            services.RegisterInMemoryContext();
    }
    private static void RegisterSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        System.Console.WriteLine("-> Using Sql Server Context");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("CommandsCon")));
        
    }
    private static void RegisterInMemoryContext(this IServiceCollection services)
    {
        System.Console.WriteLine("-> Using In Memory Context");
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
    }
    internal static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<ICommandRepository, CommandRepository>();
    }
    internal static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}