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

        public override bool Equals(object? obj)
        {
            if (obj is LivroAssunto other)
            {
                return LivroId == other.LivroId && AssuntoId == other.AssuntoId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LivroId, AssuntoId);
        }
    }
}
