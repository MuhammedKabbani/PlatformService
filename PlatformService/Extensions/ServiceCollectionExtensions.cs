using Microsoft.EntityFrameworkCore;
using PlatformService.ASyncDataServices;
using PlatformService.ASyncDataServices.RabbitMQ;
using PlatformService.Data;
using PlatformService.Data.Repositories.Abstract;
using PlatformService.Data.Repositories.Concrate;
using PlatformService.SyncDataServices;
using PlatformService.SyncDataServices.Http;
namespace PlatformService.Extensions;


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
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("PlatformsCon")));
        
    }
    private static void RegisterInMemoryContext(this IServiceCollection services)
    {
        System.Console.WriteLine("-> Using In Memory Context");
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
    internal static void RegisterHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
    }
    internal static void RegisterAsynClients(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBusClient, RabbtiMQMessageBusClient>();
    }
    internal static void RegisterGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
    }
}