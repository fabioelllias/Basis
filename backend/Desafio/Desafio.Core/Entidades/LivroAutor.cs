using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class LivroAutor : EntityBase
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is LivroAutor other)
            {
                return LivroId == other.LivroId && AutorId == other.AutorId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LivroId, AutorId);
        }
    }
}
