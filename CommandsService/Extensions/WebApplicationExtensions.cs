using CommandsService.Data;
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
                context.Database.Migrate();
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
}