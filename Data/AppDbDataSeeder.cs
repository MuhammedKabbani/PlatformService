using PlatformService.Models;

namespace PlatformService.Data;
public class AppDbDataSeeder
{
    public static void SeedData(AppDbContext? dbContext)
    {
        if (dbContext == null)
            return;

        if (!dbContext.Platforms.Any())
        {
            dbContext.Platforms.AddRange(
                new Platform() { Id = 1, Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Id = 2, Name = "Sql Server", Publisher = "Microsoft", Cost = "Free" },
                new Platform() { Id = 3, Name = "Kubernetes", Publisher = "CNCF", Cost = "Free" }
            );
            dbContext.SaveChanges();
        }
    }
}