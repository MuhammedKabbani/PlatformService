using CommandsService.Data;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;
namespace CommandsService.Extensions;
public static class WebApplicationExtensions
{
    internal static void MigrateDatabase(this WebApplication app, bool isProduction)
    {
        if (!isProduction)
            return;
        
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        if (context == null)
        {
            System.Console.WriteLine("Unable to resolve AppDbContext instance");
            return;
        }
        var retry = 0;
        while (retry < 5)
        {
            try
            {
                System.Console.WriteLine($"Migrating database: {retry + 1}");
                context.Database.Migrate();
                System.Console.WriteLine($"Migrating successfully");
                break;
            }
            catch (Exception ex)
            {
                retry++;
                Console.WriteLine($"-> DB not ready, retry {retry}: {ex.Message}");
                Thread.Sleep(5000);
            }
        }
    }
    internal static async Task SeedDataAsync(this IApplicationBuilder app)
    {
        await using (var serviceScope = app.ApplicationServices.CreateAsyncScope())
        {
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
            
            if (grpcClient == null)
                return;

            var platforms = await grpcClient.ReturnAllPlatforms();

            await PrepDb.AddData(platforms, serviceScope.ServiceProvider.GetService<IPlatformRepository>());
        }   
    }
}