using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class LivroAssunto
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;
        public int AssuntoId { get; set; }
        public Assunto Assunto { get; set; } = null!;
    }
}
