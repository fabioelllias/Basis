using AutoMapper;
using Desafio.Core.Entidades;

namespace Desafio.Application
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<Autor>, List<AutorResult>>().ReverseMap();
        }
    }
}
