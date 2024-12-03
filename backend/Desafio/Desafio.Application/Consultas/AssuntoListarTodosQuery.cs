using MediatR;
using Desafio.Infrastructure;

namespace Desafio.Application
{
    public class AssuntoListarTodosQuery : IRequest<CommandResult>
    {
        public AssuntoListarTodosQuery()
        {
        }
    }
    public class AssuntoResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }        
    }
}