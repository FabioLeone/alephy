using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV.TO
{
    [Serializable]
    public class GraficTO
    {
        public string Razao_Social { get; set; }
        public int Mes { get; set; }
        public string Grupo { get; set; }
        public string Sub_Consultoria { get; set; }
        public decimal Liquido { get; set; }
        public decimal Desconto { get; set; }
    }

    [Serializable]
    public class TotaisGraficMesTO
    {
        public int Mes { get; set; }
        public decimal Liquido { get; set; }
        public decimal Desconto { get; set; }
    }

    [Serializable]
    public class IndicesGraficTO
    {
        public int id { get; set; }
        public string grupo { get; set; }
        public string categoria { get; set; }
        public decimal venda { get; set; }
        public decimal desconto { get; set; }
    }
}
