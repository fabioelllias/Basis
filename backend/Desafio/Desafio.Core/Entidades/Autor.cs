using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class Autor
    {
        public int CodAu { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
    }
}
