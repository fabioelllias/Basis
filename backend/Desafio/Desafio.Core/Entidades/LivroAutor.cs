using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class LivroAutor
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!;
    }
}
