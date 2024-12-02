using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Entidades
{
    public class EntityBase
    {
        public int Id { get; set; }

        public bool IsNew() { return Id == 0; }
    }
}
