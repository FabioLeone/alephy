using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goku.resources
{
    [Serializable]
    public class Files
    {
        public String Cnpj { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
    }

    [Serializable]
    public class CNPJ
    {
        public CNPJ()
        {
            this.Cnpj = String.Empty;
            this.Return = false;
        }

        public String Cnpj { get; set; }
        public Boolean Return { get; set; }
    }
}
