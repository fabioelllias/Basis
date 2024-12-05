using AutoMapper;
using Desafio.Core.Entidades;

namespace Desafio.Application
{
    public class LivroAtualizarComandToLivroConverter : ITypeConverter<LivroAtualizarComand, Livro>
    {
        public Livro Convert(LivroAtualizarComand source, Livro destination, ResolutionContext context)
        {
            destination = destination ?? new Livro();
            destination.Id = source.Id;
            destination.Titulo = source.Titulo;
            destination.Editora = source.Editora;
            destination.Edicao = source.Edicao;
            destination.AnoPublicacao = source.AnoPublicacao;

            destination.LivroAutores = source.Autores
                .Select(autorId => new LivroAutor { AutorId = autorId, LivroId = source.Id })
                .ToList();

            destination.LivroAssuntos = source.Assuntos
                .Select(assuntoId => new LivroAssunto { AssuntoId = assuntoId, LivroId = source.Id })
                .ToList();

            return destination;
        }
    }
}