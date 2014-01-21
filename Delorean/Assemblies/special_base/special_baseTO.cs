using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    [Serializable]
    public class special_baseTO
    {
        public int id { get; set; }
        public int grupoid { get; set; }
        public string barras { get; set; }
    }

    [Serializable]
    public class base_viewer : special_baseTO
    {
        public int bcid { get; set; }
        public string nomeprod { get; set; }
        public decimal valor_custo { get; set; }
        public decimal one_unit { get; set; }
        public decimal upx { get; set; }
        public decimal actual_margin { get; set; }
    }
}
