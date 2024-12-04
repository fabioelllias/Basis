using AutoMapper;
using Desafio.Application;
using Desafio.Core.Entidades;

namespace Desafio.Application
{
    public class LivroCriarComandToLivroConverter : ITypeConverter<LivroCriarComand, Livro>
    {
        public Livro Convert(LivroCriarComand source, Livro destination, ResolutionContext context)
        {
            destination = destination ?? new Livro();

            destination.Titulo = source.Titulo;
            destination.Editora = source.Editora;
            destination.Edicao = source.Edicao;
            destination.AnoPublicacao = source.AnoPublicacao;

            destination.LivroAutores = source.Autores
                .Select(autorId => new LivroAutor { AutorId = autorId })
                .ToList();

            destination.LivroAssuntos = source.Assuntos
                .Select(assuntoId => new LivroAssunto { AssuntoId = assuntoId })
                .ToList();

            return destination;
        }
    }
}