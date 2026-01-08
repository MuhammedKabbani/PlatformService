namespace PlatformService.SyncDataServices.Http;

using PlatformService.Dtos;
using PlatformService.SyncDataServices;
using Microsoft.Extensions.Configuration;
public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        var httpContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(platform),
            System.Text.Encoding.UTF8,
            "application/json");

        var response =  await _httpClient.PostAsync($"{_configuration.GetSection("CommandService")["BaseUrl"]}/platforms/", httpContent);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to CommandsService was OK!");
        }
        else
        {
            Console.WriteLine("--> Sync POST to CommandsService was NOT OK!");
        }
    }
}