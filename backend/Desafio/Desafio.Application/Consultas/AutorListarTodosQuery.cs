using MediatR;
using Desafio.Infrastructure;

namespace Desafio.Application
{
    public class AutorListarTodosQuery : IRequest<CommandResult>
    {
        public AutorListarTodosQuery()
        {
        }
    }
    public class AutorResult
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
    }
}