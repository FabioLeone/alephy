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

    [Serializable]
    public class CNPJ
    {
        public CNPJ() {
            this.Cnpj = String.Empty;
            this.Return = false;
        }

        public String Cnpj { get; set; }
        public Boolean Return { get; set; }
    }

    [Serializable]
    public class BaseClientes
    {
        public int id { get; set; }
        public string razao { get; set; }
        public string cnpj { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public string barras { get; set; }
        public string descricao { get; set; }
        public string fabricante { get; set; }
        public int quantidade { get; set; }
        public decimal valor_bruto { get; set; }
        public decimal valor_liquido { get; set; }
        public decimal valot_desconto { get; set; }
        public int farmaciaid { get; set; }
    }
}
