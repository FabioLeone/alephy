using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class competitors_baseTO
    {
        public int id { get; set; }
        public string barras { get; set; }
        public string nomeprod { get; set; }
        public string rede { get; set; }
        public decimal valor_preco { get; set; }
        public decimal valor_desconto { get; set; }
    }
}
