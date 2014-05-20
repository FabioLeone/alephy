using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using Npgsql;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class RelatoriosVisualizadosDAL
    {
        #region .: Load :.
        private static RelatoriosVisualizadosTO Load(IDataReader drdRelatodios) {
            RelatoriosVisualizadosTO clsRelatorio = new RelatoriosVisualizadosTO();
            if (!drdRelatodios.IsDBNull(drdRelatodios.GetOrdinal("relatorio_visualizadoId"))) { clsRelatorio.RelatorioVisualizadoId = drdRelatodios.GetInt32(drdRelatodios.GetOrdinal("relatorio_visualizadoId")); } else { clsRelatorio.RelatorioVisualizadoId = 0; }
            if (!drdRelatodios.IsDBNull(drdRelatodios.GetOrdinal("UserId"))) { clsRelatorio.UserId = drdRelatodios.GetInt32(drdRelatodios.GetOrdinal("UserId")); } else { clsRelatorio.UserId = 0; }
            if (!drdRelatodios.IsDBNull(drdRelatodios.GetOrdinal("Relatorio"))) { clsRelatorio.Relatorio = drdRelatodios.GetString(drdRelatodios.GetOrdinal("Relatorio")); } else { clsRelatorio.Relatorio = ""; }
            if (!drdRelatodios.IsDBNull(drdRelatodios.GetOrdinal("Data"))) { clsRelatorio.Data = drdRelatodios.GetDateTime(drdRelatodios.GetOrdinal("Data")); } else { clsRelatorio.Data = DateTime.MinValue; }
            return clsRelatorio;
        }
        #endregion

        #region .: Persistence :.
        public static bool Insert(RelatoriosVisualizadosTO clsRelatorio, string strConnection)
        {
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                String strSQL = "INSERT INTO relatorios_visualizados (UserId,Relatorio,`Data`) VALUES (@UserId,@Relatorio,@Data)";

                DbCommand cmdRelatorio = msc.CreateCommand();

                cmdRelatorio.CommandText = strSQL;
                cmdRelatorio.Parameters.Clear();
                cmdRelatorio.Parameters.Add(DbHelper.GetParameter(cmdRelatorio, DbType.Int32, "@UserId", clsRelatorio.UserId));
                cmdRelatorio.Parameters.Add(DbHelper.GetParameter(cmdRelatorio, DbType.String, "@Relatorio", clsRelatorio.Relatorio));
                cmdRelatorio.Parameters.Add(DbHelper.GetParameter(cmdRelatorio, DbType.DateTime, "@Data", DateTime.Now));

                msc.Open();
                cmdRelatorio.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                msc.Close();
            }
        }
        #endregion
    }
}
