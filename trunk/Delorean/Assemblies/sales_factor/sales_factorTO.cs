using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    [Serializable]
    public class sales_factorTO
    {
        public int id { get; set; }
        public string barras { get; set; }
        public string prod_name { get; set; }
        public int cont_bxs { get; set; }
        public decimal expected_margin { get; set; }
        public decimal discount { get; set; }
        public decimal other_bxs { get; set; }
    }
}
