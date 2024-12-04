using Desafio.Infrastructure;
using MediatR;

namespace Desafio.Application
{
    public class LivroFormaCompraQuery : IRequest<CommandResult>
    {
        public LivroFormaCompraQuery()
        {
            
        }
    }

    public class LivroFormaCompraResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
