using MediatR;
using Desafio.Infrastructure;

namespace Desafio.Application
{
    public class LivroListarTodosQuery : IRequest<CommandResult>
    {
        public LivroListarTodosQuery()
        {
        }
    }
    public class LivroResult
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public string Autores { get; set; }
        public string Assuntos { get; set; }
    }
}