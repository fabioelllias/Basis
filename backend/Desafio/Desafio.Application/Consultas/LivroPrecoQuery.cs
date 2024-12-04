using Desafio.Infrastructure;
using MediatR;

namespace Desafio.Application
{
    public class LivroPrecoQuery : IRequest<CommandResult>
    {
        public static LivroPrecoQuery Create(int id)
        {
            return new LivroPrecoQuery(id);
        }
        public int Id { get; set; }

        public LivroPrecoQuery(int id)
        {
            Id = id;
        }
    }

    public class LivroPrecoResult
    {
        public string Livro { get; set; }
        public ICollection<LivroPrecoItemResult> Precos { get; set; } = new List<LivroPrecoItemResult>();
    }

    public class LivroPrecoItemResult
    {
        public string FormaCompra { get; set; }
        public string Preco { get; set; }
    }

}
