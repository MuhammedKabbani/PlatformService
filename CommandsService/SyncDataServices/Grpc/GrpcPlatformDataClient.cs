using System.Threading.Tasks;
using AutoMapper;
using CommandsService;
using CommandsService.Models;
using Grpc.Net.Client;

namespace CommandsService.SyncDataServices.Grpc;

public class GrpcPlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    private readonly ILogger<GrpcPlatformDataClient> _logger;

    public GrpcPlatformDataClient(IConfiguration configuration, IMapper mapper, ILogger<GrpcPlatformDataClient> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<Platform>?> ReturnAllPlatforms()
    {
        _logger.LogInformation("Calling Grpc service {host}",_configuration["GrpcPlatform"]);
        var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
        var client = new GrpcPlatform.GrpcPlatformClient(channel);
        var request = new GetAllRequest();

        try
        {
            var reply = await client.GetAllPlatformsAsync(request);
            return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Couldn't call Grpc server {ex}", ex.Message);
            return null;
        }
    }
}