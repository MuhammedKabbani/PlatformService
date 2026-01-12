using AutoMapper;
using PlatformService.Models;
using PlatformService.Dtos;

namespace PlatformService.Utilities.Mapping.AutoMapper.Profiles;


internal class PlatformMappingProfile : Profile
{
    public PlatformMappingProfile()
    {
            CreateMap<Platform, PlatformCreateDto>().ReverseMap();
            CreateMap<Platform, PlatformReadDto>().ReverseMap();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
    }
}
