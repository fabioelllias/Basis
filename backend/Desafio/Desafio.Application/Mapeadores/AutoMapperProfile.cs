using AutoMapper;
using Desafio.Core.Entidades;

namespace Desafio.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Autor, AutorResult>()
                .ForMember(dest => dest.Id, orin => orin.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
