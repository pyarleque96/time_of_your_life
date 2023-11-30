using AutoMapper;
using time_of_your_life.Application.Contratcs.Clock;
using time_of_your_life.Infrastructure.Data.Entities;

namespace time_of_your_life.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ClockPropsDto, ClockProps>().ReverseMap();
        CreateMap<ClockPropsDto, ClockPropsDto>().ReverseMap();
    }
}
