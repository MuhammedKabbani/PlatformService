using AutoMapper;
using CommandsService.Models;
using CommandsService.Dtos;
using CommandsService;
namespace CommandsService.Utilities.Mapping.AutoMapper.Profiles;


internal class PlatformMappingProfile : Profile
{
    public PlatformMappingProfile()
    {
            CreateMap<Command, CommandCreateDto>().ReverseMap();
            CreateMap<Command, CommandReadDto>().ReverseMap();
            CreateMap<Platform, PlatformReadDto>().ReverseMap();
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<PlatformPublishedDto, GenericEventDto>().ReverseMap();

            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest => dest.OriginalId, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());

    }
}
