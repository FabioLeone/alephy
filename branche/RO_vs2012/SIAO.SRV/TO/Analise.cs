using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV.TO
{
    public class AnaliseTO
    {
        public int id { get; set; }
        public decimal reference { get; set; }
        public string text { get; set; }
        public int itemid { get; set; }
    }

    public class AnaliseItemTO
    {
        public int id { get; set; }
        public string item { get; set; }
    }
}
