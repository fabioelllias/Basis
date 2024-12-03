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
            
            CreateMap<Autor, AutorCriarComand>()
                .ReverseMap();

            CreateMap<Autor, AutorCriarResult>()
                .ReverseMap();

            CreateMap<Autor, AutorAtualizarComand>()
                .ReverseMap();

            CreateMap<Autor, AutorAtualizarResult>()
                .ReverseMap();                
        }
    }
}