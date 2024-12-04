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


            CreateMap<Assunto, AssuntoResult>()
              .ForMember(dest => dest.Id, orin => orin.MapFrom(src => src.Id))
              .ReverseMap();

            CreateMap<Assunto, AssuntoCriarComand>()
                .ReverseMap();

            CreateMap<Assunto, AssuntoCriarResult>()
                .ReverseMap();

            CreateMap<Assunto, AssuntoAtualizarComand>()
                .ReverseMap();

            CreateMap<Assunto, AssuntoAtualizarResult>()
                .ReverseMap();


            CreateMap<Livro, LivroResult>()
                .ForMember(dest => dest.Id, orin => orin.MapFrom(src => src.Id))
                .ForMember(dest => dest.Autores, orin => orin.MapFrom(src => string.Join(',', src.LivroAutores.Select(item => item.Autor.Nome).ToArray())))
                .ForMember(dest => dest.Assuntos, orin => orin.MapFrom(src => string.Join(',', src.LivroAssuntos.Select(item => item.Assunto.Descricao).ToArray()))
                );

            CreateMap<LivroCriarComand, Livro>()
                .ConvertUsing<LivroCriarComandToLivroConverter>();

            CreateMap<Livro, LivroCriarResult>()
                 .ForMember(dest => dest.Autores, orin => orin.MapFrom(src => string.Join(',', src.LivroAutores.Select(item => item.Autor.Nome).ToArray())))
                 .ForMember(dest => dest.Assuntos, orin => orin.MapFrom(src => string.Join(',', src.LivroAssuntos.Select(item => item.Assunto.Descricao).ToArray()))
               );

            CreateMap<LivroAtualizarComand, Livro>()
                .ConvertUsing<LivroAtualizarComandToLivroConverter>();

            CreateMap<Livro, LivroAtualizarResult>()
                 .ForMember(dest => dest.Autores, orin => orin.MapFrom(src => string.Join(',', src.LivroAutores.Select(item => item.Autor.Nome).ToArray())))
                 .ForMember(dest => dest.Assuntos, orin => orin.MapFrom(src => string.Join(',', src.LivroAssuntos.Select(item => item.Assunto.Descricao).ToArray()))
               );
        }
    }
}