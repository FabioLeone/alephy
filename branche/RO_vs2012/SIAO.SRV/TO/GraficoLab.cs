using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV.TO
{
    [Serializable]
    public class GraficoLabTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string  NomeLab { get; set; }
        public decimal Valor_Liquido { get; set; }
    }
}
