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
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
    }
}
