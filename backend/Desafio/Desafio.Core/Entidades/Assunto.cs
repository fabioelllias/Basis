using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class Assunto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    }
}
