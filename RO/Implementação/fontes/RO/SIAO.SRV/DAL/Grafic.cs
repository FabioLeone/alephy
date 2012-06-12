using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MySql.Data;
using SIAO.SRV.TO;
using System.Data;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class GraficDAL
    {
        #region .: Load :.

        private static GraficTO LoadGrfic(IDataReader drdGrafic)
        {
            GraficTO clsGrafic = new GraficTO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Razao_Social"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("Razao_Social")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("Grupo")); } else { clsGrafic.Grupo = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Sub_Consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("Sub_Consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Desconto"))) { clsGrafic.Desconto = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Desconto")); } else { clsGrafic.Desconto = 0; }

            return clsGrafic;
        }

        private static TotaisGraficMesTO LoadTotal(IDataReader drdTotal)
        {
            TotaisGraficMesTO clsTotalGraficMes = new TotaisGraficMesTO();

            if (!drdTotal.IsDBNull(drdTotal.GetOrdinal("Mes"))) { clsTotalGraficMes.Mes = drdTotal.GetInt32(drdTotal.GetOrdinal("Mes")); } else { clsTotalGraficMes.Mes = 0; }
            if (!drdTotal.IsDBNull(drdTotal.GetOrdinal("Liquido"))) { clsTotalGraficMes.Liquido = drdTotal.GetDecimal(drdTotal.GetOrdinal("Liquido")); } else { clsTotalGraficMes.Liquido = 0; }
            if (!drdTotal.IsDBNull(drdTotal.GetOrdinal("Desconto"))) { clsTotalGraficMes.Desconto = drdTotal.GetDecimal(drdTotal.GetOrdinal("Desconto")); } else { clsTotalGraficMes.Desconto = 0; }

            return clsTotalGraficMes;
        }

        private static IndicesGraficTO LoadIndicesGrafic(IDataReader drdIndices)
        {
            IndicesGraficTO clsIndicesGrafic = new IndicesGraficTO();

            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("id"))) { clsIndicesGrafic.id = drdIndices.GetInt32(drdIndices.GetOrdinal("id")); } else { clsIndicesGrafic.id = 0; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("grupo"))) { clsIndicesGrafic.grupo = drdIndices.GetString(drdIndices.GetOrdinal("grupo")); } else { clsIndicesGrafic.grupo = string.Empty; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("categoria"))) { clsIndicesGrafic.categoria = drdIndices.GetString(drdIndices.GetOrdinal("categoria")); } else { clsIndicesGrafic.categoria = string.Empty; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("venda"))) { clsIndicesGrafic.venda = drdIndices.GetDecimal(drdIndices.GetOrdinal("venda")); } else { clsIndicesGrafic.venda = 0; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("desconto"))) { clsIndicesGrafic.desconto = drdIndices.GetDecimal(drdIndices.GetOrdinal("desconto")); } else { clsIndicesGrafic.desconto = 0; }

            return clsIndicesGrafic;
        }

        #endregion

        #region .: Search :.

        public static List<GraficTO> GetGraficMes(int intMes) {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT * FROM (");
                strSQL.Append(" SELECT base_clientes.Razao_Social, base_clientes.Mes, produtos_base.Grupo, produtos_base.Sub_Consultoria, ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" GROUP BY base_clientes.Razao_Social, base_clientes.Mes, produtos_base.Grupo, produtos_base.Sub_Consultoria");
                strSQL.Append(" UNION");
                strSQL.Append(" SELECT '' AS Razao_Social, base_clientes.Mes, 'Total' AS Grupo, 'RELAC (PBM)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'RELAC (PBM)'");
                strSQL.Append(" GROUP BY base_clientes.Mes");
                strSQL.Append(" UNION");
                strSQL.Append(" SELECT '' AS Razao_Social, base_clientes.Mes, 'Total' AS Grupo, 'PDE 2 (trata)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'PDE 2 %'");
                strSQL.Append(" GROUP BY base_clientes.Mes");
                strSQL.Append(" UNION");
                strSQL.Append(" SELECT '' AS Razao_Social, base_clientes.Mes, 'Total' AS Grupo, 'PORT (PSICO)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'PORT (PSICO)'");
                strSQL.Append(" GROUP BY base_clientes.Mes");
                strSQL.Append(" UNION");
                strSQL.Append(" SELECT '' AS Razao_Social, base_clientes.Mes, 'zzzzzz' AS Grupo, 'zzzzzz'AS Sub_Consultoria,");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" ) AS xTemp");
                strSQL.Append(" ORDER BY Grupo, Sub_Consultoria");

                DbCommand cmdGrafic = db.GetSqlStringCommand(strSQL.ToString());

                db.AddInParameter(cmdGrafic, "@Mes", DbType.Int32, intMes);

                using (IDataReader drdGrafic = db.ExecuteReader(cmdGrafic)) {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrfic(drdGrafic));
                    }
                }
            }
            finally
            {

            }

            return clsGrafic;
        }

        public static TotaisGraficMesTO GetTotalMes(int intMes) {
            TotaisGraficMesTO clsTotalMes = new TotaisGraficMesTO();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT base_clientes.Mes,");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" GROUP BY base_clientes.Mes;");

                DbCommand cmdTotalMes = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdTotalMes, "@Mes", DbType.Int32, intMes);

                using (IDataReader drdTotalMes = db.ExecuteReader(cmdTotalMes)) {
                    if (drdTotalMes.Read()) { clsTotalMes = LoadTotal(drdTotalMes); }
                }
            }
            finally
            {

            }

            return clsTotalMes;
        }

        public static List<IndicesGraficTO> GetIndicesALL() {
            List<IndicesGraficTO> clsIndicesGrafic = new List<IndicesGraficTO>();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios;");

                DbCommand cmdIndicesGrafic = db.GetSqlStringCommand(strSQL.ToString());

                using (IDataReader drdIndicesGrafic = db.ExecuteReader(cmdIndicesGrafic)) {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic.Add(LoadIndicesGrafic(drdIndicesGrafic));
                    }
                }
            }
            finally
            {

            }

            return clsIndicesGrafic;
        }

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsetIndices(IndicesGraficTO clsIndicesGrafic)
        {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("INSERT INTO indice_relatorios (indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto)");
                strSQL.Append(" VALUES (@grupo, @categoria, @venda, @desconto);");
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios WHERE indice_relatorios.id=@@IDENTITY;");

                DbCommand cmdIndicesGrafic = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdIndicesGrafic, "@grupo", DbType.String, clsIndicesGrafic.grupo);
                db.AddInParameter(cmdIndicesGrafic, "@categoria", DbType.String, clsIndicesGrafic.categoria);
                db.AddInParameter(cmdIndicesGrafic, "@venda", DbType.Decimal, clsIndicesGrafic.venda);
                db.AddInParameter(cmdIndicesGrafic, "@desconto", DbType.Decimal, clsIndicesGrafic.desconto);

                using (IDataReader drdIndicesGrafic = db.ExecuteReader(cmdIndicesGrafic))
                {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic = LoadIndicesGrafic(drdIndicesGrafic);
                    }
                }
            }
            finally
            {

            }

            return clsIndicesGrafic;
        }

        public static Boolean UpdateIndices(IndicesGraficTO clsIndicesGrafic) {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("UPDATE indice_relatorios SET indice_relatorios.grupo=@grupo, indice_relatorios.categoria=@categoria, indice_relatorios.venda=@venda, indice_relatorios.desconto=@desconto");
                strSQL.Append(" WHERE indice_relatorios.id=@id;");

                DbCommand cmdIndicesGrafic = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdIndicesGrafic, "@id", DbType.Int32, clsIndicesGrafic.id);
                db.AddInParameter(cmdIndicesGrafic, "@grupo", DbType.String, clsIndicesGrafic.grupo);
                db.AddInParameter(cmdIndicesGrafic, "@categoria", DbType.String, clsIndicesGrafic.categoria);
                db.AddInParameter(cmdIndicesGrafic, "@venda", DbType.Decimal, clsIndicesGrafic.venda);
                db.AddInParameter(cmdIndicesGrafic, "@desconto", DbType.Decimal, clsIndicesGrafic.desconto);

                db.ExecuteNonQuery(cmdIndicesGrafic);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean DeleteIndice(IndicesGraficTO clsIndicesGrafic) {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("DELETE FROM indice_relatorios");
                strSQL.Append(" WHERE indice_relatorios.id=@id;");

                DbCommand cmdIndicesGrafic = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdIndicesGrafic, "@id", DbType.Int32, clsIndicesGrafic.id);

                db.ExecuteNonQuery(cmdIndicesGrafic);

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
