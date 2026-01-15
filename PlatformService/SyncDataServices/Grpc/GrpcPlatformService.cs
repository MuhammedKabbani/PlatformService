using AutoMapper;
using Grpc.Core;
using PlatformService.Data.Repositories.Abstract;

namespace PlatformService.SyncDataServices.Grpc;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepository _platformRepo;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IPlatformRepository platformRepository, IMapper mapper)
    {
        _platformRepo = platformRepository;
        _mapper = mapper;
    }

    public override async Task<platformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new platformResponse();
        var platforms = await _platformRepo.GetAllAsync(trackChanges: false);
        foreach(var plat in platforms)
        {
            response.Platform.Add(_mapper.Map<GrpcPlatformModel>(plat));
        }
        return response;
    }
}