using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV.TO
{
    public class RelatoriosVisualizadosTO
    {
        public int RelatorioVisualizadoId { get; set; }
        public int UserId { get; set; }
        public String Relatorio { get; set; }
        public DateTime Data { get; set; }
    }
}
