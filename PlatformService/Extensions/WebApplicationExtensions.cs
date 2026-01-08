using PlatformService.Data;
namespace PlatformService.Extensions;
public static class WebApplicationExtensions
{
    internal static void SeedData(this WebApplication app, bool isProduction)
    {
        using(var scope = app.Services.CreateScope())
        {
            AppDbDataSeeder.SeedData(scope.ServiceProvider.GetService<AppDbContext>(), isProduction);
        }
    }
}