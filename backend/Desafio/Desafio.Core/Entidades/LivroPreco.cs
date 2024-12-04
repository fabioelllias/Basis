namespace Desafio.Core.Entidades
{
    public class LivroPreco : EntityBase
    {
        public decimal Preco {  get; set; } = decimal.Zero;
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;
        public int FormaCompraId { get; set; }
        public FormaCompra FormaCompra { get; set; } = null!;
    }
}
