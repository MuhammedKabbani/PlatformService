using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data;
public class AppDbDataSeeder
{
    public static void SeedData(AppDbContext? dbContext, bool isProduction)
    {
        if (dbContext == null)
            return;

        if (isProduction)
        {
            System.Console.WriteLine("-> Try to migrate Database.");
            var retry = 0;
            while (retry < 5)
            {
                try
                {
                    dbContext.Database.Migrate();
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

        if (!dbContext.Platforms.Any())
        {
            System.Console.WriteLine("-> Seeding Data.");
            dbContext.Platforms.AddRange(
                new Platform() {Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() {Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                new Platform() {Name = "Kubernetes", Publisher = "CNCF", Cost = "Free" }
            );
            dbContext.SaveChanges();
        }
    }
}