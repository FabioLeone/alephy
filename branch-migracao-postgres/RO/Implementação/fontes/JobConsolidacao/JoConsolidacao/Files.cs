using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobConsolidacao
{
    [Serializable]
    public class Files
    {
        public String Cnpj { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }
}
