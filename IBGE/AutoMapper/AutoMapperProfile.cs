using AutoMapper;
using IBGE.DTO;
using IBGE.Entities;

namespace Challenge.Balta.IBGE.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LocationViewModel, Location>().ReverseMap();
        }
    }
}