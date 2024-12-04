using AutoMapper;
using Desafio.Core.Entidades;

namespace Desafio.Application
{  
    public class LivroToLivroPrecoConverter : ITypeConverter<Livro, LivroPrecoResult>
    {
        public LivroPrecoResult Convert(Livro source, LivroPrecoResult destination, ResolutionContext context)
        {
            destination = destination ?? new LivroPrecoResult();
            destination.Livro = $"{source.Titulo} - {source.Editora} - {source.AnoPublicacao}";
            destination.Precos = source.LivroPrecos.Select(x => new LivroPrecoItemResult
            {
                FormaCompra = x.FormaCompra.Descricao,
                Preco = x.Preco.ToString("n2")
            })
            .ToList();            

            return destination;
        }
    }
}
