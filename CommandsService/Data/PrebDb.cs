using System.Threading.Tasks;
using CommandsService.Data.Repositories.Abstract;
using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;

namespace CommandsService.Data;

public static class PrepDb
{
    public static async Task AddData(IEnumerable<Platform>? platforms, IPlatformRepository? platformRepo){
        if (platforms == null || !platforms.Any() || platformRepo == null)
            return;
        System.Console.WriteLine("--> Seeding new platforms...");

        foreach(var plat in platforms)
        {
            if (!await platformRepo.OriginalPlatformExsists(plat.OriginalId))
            {
                await platformRepo.CreatePlatform(plat);
            }
        }
    }
}