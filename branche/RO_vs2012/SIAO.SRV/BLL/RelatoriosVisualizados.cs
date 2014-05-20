using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class RelatoriosVisualizadosBLL
    {
        #region .: Persistence :.
        public static Boolean Insert(RelatoriosVisualizadosTO clsRelatorio, String strConn) {
            return RelatoriosVisualizadosDAL.Insert(clsRelatorio, strConn);
        }
        #endregion
    }
}
