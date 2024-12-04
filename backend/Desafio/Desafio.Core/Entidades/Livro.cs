namespace Desafio.Core.Entidades
{
    public class Livro : EntityBase
    {
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;
        public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
        public ICollection<LivroPreco> LivroPrecos { get; set; } = new List<LivroPreco>();
    }
}
