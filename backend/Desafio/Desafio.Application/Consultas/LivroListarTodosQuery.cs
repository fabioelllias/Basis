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
        public List<AutoresResult> Autores { get; set; }
        public List<AssuntosResult> Assuntos { get; set; }
        public List<FormasCompraResult> FormasCompra { get; set; } = new List<FormasCompraResult>();
    }

    public class AutoresResult
    {
        public int Id { get; set; }
        public string Autor { get; set; }
    }

    public class AssuntosResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class FormasCompraResult
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public string Preco { get; set; }
    }
}