using System;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using MySql.Data.MySqlClient;
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

        public static List<GraficTO> GetGraficMes(int intMes, UsersTO clsUser, string strConnection, string strLoja)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            MySqlConnection msc = new MySqlConnection(strConnection);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(" SELECT * FROM ( ");
                strSQL.Append(" SELECT base_clientes.Razao_Social,base_clientes.Cnpj, base_clientes.Mes, produtos_base.Grupo, produtos_base.Sub_Consultoria, ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, 1-(SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes ");
                strSQL.Append(" LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" AND (produtos_base.Grupo = 'Genéricos' AND produtos_base.Sub_Consultoria = 'PDE 2-2 (FPB)') OR ((produtos_base.Sub_Consultoria = 'PDE 2 (trata)') AND base_clientes.Mes = @Mes)");
                strSQL.Append(" OR ((produtos_base.Grupo = 'Propagados' AND produtos_base.Sub_Consultoria LIKE 'PDE 1 %') AND base_clientes.Mes = @Mes)");
                strSQL.Append(" GROUP BY base_clientes.Razao_Social, base_clientes.Mes, produtos_base.Grupo, produtos_base.Sub_Consultoria ");
                strSQL.Append(" UNION ");
                strSQL.Append(" SELECT '' AS Razao_Social,base_clientes.Cnpj, base_clientes.Mes, 'Total' AS Grupo, 'RELAC (PBM)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, 1-(SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto ");
                strSQL.Append(" FROM base_clientes ");
                strSQL.Append(" LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra ");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes ");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'RELAC (PBM)' ");
                strSQL.Append(" GROUP BY base_clientes.Mes,base_clientes.Cnpj ");
                strSQL.Append(" UNION ");
                strSQL.Append(" SELECT '' AS Razao_Social,base_clientes.Cnpj, base_clientes.Mes, 'Total' AS Grupo, 'PDE 2 (trata)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, 1-(SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto ");
                strSQL.Append(" FROM base_clientes ");
                strSQL.Append(" LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra ");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes ");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'PDE 2 %' ");
                strSQL.Append(" GROUP BY base_clientes.Mes,base_clientes.Cnpj ");
                strSQL.Append(" UNION ");
                strSQL.Append(" SELECT '' AS Razao_Social,base_clientes.Cnpj, base_clientes.Mes, 'Total' AS Grupo, 'PORT (PSICO)'AS Sub_Consultoria,  ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, 1-(SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto ");
                strSQL.Append(" FROM base_clientes ");
                strSQL.Append(" LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra ");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes ");
                strSQL.Append(" AND produtos_base.Sub_Consultoria LIKE 'PORT (PSICO)' ");
                strSQL.Append(" GROUP BY base_clientes.Mes,base_clientes.Cnpj  ");
                strSQL.Append(" UNION ");
                strSQL.Append(" SELECT '' AS Razao_Social,base_clientes.Cnpj, base_clientes.Mes, 'zzzzzz' AS Grupo, 'zzzzzz'AS Sub_Consultoria, ");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, 1-(SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto ");
                strSQL.Append(" FROM base_clientes ");
                strSQL.Append(" LEFT JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra ");
                strSQL.Append(" WHERE base_clientes.Mes = @Mes GROUP BY base_clientes.Cnpj) AS xTemp WHERE Mes > 0");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (strLoja != "")
                {
                    strSQL.Append(" AND Cnpj IN ('" + strLoja + "')");
                }
                else
                {
                    if (clsUser.Access == "nvg")
                    {
                        cmdGrafic.CommandText += "SELECT farmacias.Cnpj FROM memberships"
                            + " INNER JOIN redesfarmaceuticas ON memberships.UserId = redesfarmaceuticas.UserId"
                            + " INNER JOIN farmacias ON redesfarmaceuticas.Id = farmacias.idRede"
                            + " WHERE memberships.UserId = @UserId";
                        cmdGrafic.Parameters.Clear();
                        cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UserId", clsUser.UserId));

                        msc.Open();
                        using (IDataReader drdCnpj = cmdGrafic.ExecuteReader())
                        {
                            while (drdCnpj.Read())
                            {
                                lstCnpj.Add(!drdCnpj.IsDBNull(drdCnpj.GetOrdinal("Cnpj")) ? drdCnpj.GetString(drdCnpj.GetOrdinal("Cnpj")) : string.Empty);
                            }
                        }
                        msc.Close();

                        if (lstCnpj.Count > 0)
                        {
                            clsUser.Cnpj = new List<string>();
                            lstCnpj.ForEach(delegate(string _cnpj)
                            {
                                clsUser.Cnpj.Add(_cnpj);
                            });
                        }

                        if (clsUser.Cnpj != null)
                        {
                            if (clsUser.Cnpj.Count > 0)
                            {
                                strSQL.Append(" AND Cnpj IN ('");

                                int i = 0;
                                clsUser.Cnpj.ForEach(delegate(string _cnpj)
                                {
                                    if (i == 0) { strSQL.Append(_cnpj); i++; } else { strSQL.Append("', '" + _cnpj); i++; }
                                });
                                strSQL.Append("')");
                            }
                        }
                    }
                    else if (clsUser.Access == "nvp")
                    {
                        cmdGrafic.CommandText += "SELECT farmacias.Cnpj FROM usuarios_farmacias"
                            + " INNER JOIN farmacias ON usuarios_farmacias.FarmaciaId = farmacias.Id"
                            + " INNER JOIN memberships ON usuarios_farmacias.UserId = memberships.UserId"
                            + " WHERE memberships.UserId = @UserId OR farmacias.ProprietarioId = @UserId";
                        cmdGrafic.Parameters.Clear();
                        cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UserId", clsUser.UserId));

                        msc.Open();
                        using (IDataReader drdCnpj = cmdGrafic.ExecuteReader())
                        {
                            while (drdCnpj.Read())
                            {
                                lstCnpj.Add(!drdCnpj.IsDBNull(drdCnpj.GetOrdinal("Cnpj")) ? drdCnpj.GetString(drdCnpj.GetOrdinal("Cnpj")) : string.Empty);
                            }
                        }
                        msc.Close();

                        if (lstCnpj.Count > 0)
                        {
                            clsUser.Cnpj = new List<string>();
                            lstCnpj.ForEach(delegate(string _cnpj)
                            {
                                clsUser.Cnpj.Add(_cnpj);
                            });
                        }

                        if (clsUser.Cnpj != null)
                        {
                            if (clsUser.Cnpj.Count > 0)
                            {
                                strSQL.Append(" AND Cnpj IN ('");
                                int i = 0;
                                clsUser.Cnpj.ForEach(delegate(string _cnpj)
                                {
                                    if (i == 0) { strSQL.Append(_cnpj); i++; } else { strSQL.Append("', '" + _cnpj); i++; }
                                });
                                strSQL.Append("')");
                            }
                        }
                    }
                }


                strSQL.Append(" ORDER BY Grupo, Sub_Consultoria ");
                
                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@Mes", intMes));

                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrfic(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        public static TotaisGraficMesTO GetTotalMes(int intMes, string strConnection)
        {
            TotaisGraficMesTO clsTotalMes = new TotaisGraficMesTO();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT base_clientes.Mes,");
                strSQL.Append(" SUM(base_clientes.Valor_Liquido) AS Liquido, (SUM(base_clientes.Valor_Liquido) / SUM(base_clientes.Valor_Bruto)) AS Desconto");
                strSQL.Append(" FROM base_clientes");
                strSQL.Append(" INNER JOIN produtos_base ON base_clientes.Barras = produtos_base.CodBarra");
                strSQL.Append(" WHERE produtos_base.Grupo IN ('Propagados', 'Alternativos' , 'Genéricos') AND base_clientes.Mes = @Mes");
                strSQL.Append(" GROUP BY base_clientes.Mes;");

                DbCommand cmdTotalMes = msc.CreateCommand();
                cmdTotalMes.CommandText = strSQL.ToString();
                cmdTotalMes.Parameters.Clear();
                cmdTotalMes.Parameters.Add(DbHelper.GetParameter(cmdTotalMes, DbType.Int32, "@Mes", intMes));

                msc.Open();

                using (IDataReader drdTotalMes = cmdTotalMes.ExecuteReader())
                {
                    if (drdTotalMes.Read()) { clsTotalMes = LoadTotal(drdTotalMes); }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsTotalMes;
        }

        public static List<IndicesGraficTO> GetIndicesALL(string strConnection)
        {
            List<IndicesGraficTO> clsIndicesGrafic = new List<IndicesGraficTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios;");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic.Add(LoadIndicesGrafic(drdIndicesGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsIndicesGrafic;
        }

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsetIndices(IndicesGraficTO clsIndicesGrafic, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("INSERT INTO indice_relatorios (indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto)");
                strSQL.Append(" VALUES (@grupo, @categoria, @venda, @desconto);");
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios WHERE indice_relatorios.id=@@IDENTITY;");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();

                cmdIndicesGrafic.Parameters.Clear();
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "@grupo", clsIndicesGrafic.grupo));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "@categoria", clsIndicesGrafic.categoria));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Decimal, "@venda", clsIndicesGrafic.venda));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Decimal, "@desconto", clsIndicesGrafic.desconto));

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic = LoadIndicesGrafic(drdIndicesGrafic);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsIndicesGrafic;
        }

        public static Boolean UpdateIndices(IndicesGraficTO clsIndicesGrafic, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("UPDATE indice_relatorios SET indice_relatorios.grupo=@grupo, indice_relatorios.categoria=@categoria, indice_relatorios.venda=@venda, indice_relatorios.desconto=@desconto");
                strSQL.Append(" WHERE indice_relatorios.id=@id;");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();
                cmdIndicesGrafic.Parameters.Clear();
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Int32, "@id", clsIndicesGrafic.id));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "@grupo", clsIndicesGrafic.grupo));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "@categoria", clsIndicesGrafic.categoria));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Decimal, "@venda", clsIndicesGrafic.venda));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Decimal, "@desconto", clsIndicesGrafic.desconto));

                msc.Open();

                cmdIndicesGrafic.ExecuteNonQuery();

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

        public static Boolean DeleteIndice(IndicesGraficTO clsIndicesGrafic, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM indice_relatorios");
                strSQL.Append(" WHERE indice_relatorios.id=@id;");


                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();

                cmdIndicesGrafic.Parameters.Clear();
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Int32, "@id", clsIndicesGrafic.id));

                msc.Open();

                cmdIndicesGrafic.ExecuteNonQuery();

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


        internal static IndicesGraficTO GetIndicesById(int intId, string strConnection)
        {
            IndicesGraficTO clsIndicesGrafic = new IndicesGraficTO();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios");
                strSQL.Append(" WHERE indice_relatorios.id = @id");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();
                cmdIndicesGrafic.Parameters.Clear();
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.Int32, "id", intId));

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    if (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic = LoadIndicesGrafic(drdIndicesGrafic);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsIndicesGrafic;
        }

        internal static List<IndicesGraficTO> GetIndicesByFiltro(string strGrupo, string strSub_Categoria, string strConnection)
        {
            List<IndicesGraficTO> clsIndicesGrafic = new List<IndicesGraficTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios");
                strSQL.Append(" WHERE indice_relatorios.id > 0");

                if (!string.IsNullOrEmpty(strGrupo)) { strSQL.Append(" AND indice_relatorios.grupo = @grupo"); }
                if (!string.IsNullOrEmpty(strSub_Categoria)) { strSQL.Append(" AND indice_relatorios.categoria = @categoria"); }
                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();
                cmdIndicesGrafic.Parameters.Clear();
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "grupo", strGrupo));
                cmdIndicesGrafic.Parameters.Add(DbHelper.GetParameter(cmdIndicesGrafic, DbType.String, "categoria", strSub_Categoria));

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic.Add(LoadIndicesGrafic(drdIndicesGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsIndicesGrafic;
        }

        internal static List<string> GetCategorias(string strConnection)
        {
            List<string> clsCategorias = new List<string>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT DISTINCT indice_relatorios.categoria");
                strSQL.Append(" FROM indice_relatorios");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        if (!drdIndicesGrafic.IsDBNull(drdIndicesGrafic.GetOrdinal("categoria"))) { clsCategorias.Add(drdIndicesGrafic.GetString(drdIndicesGrafic.GetOrdinal("categoria"))); } else { clsCategorias.Add(string.Empty); }
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsCategorias;
        }

        internal static List<string> GetGrupos(string strConnection)
        {
            List<string> clsGrupos = new List<string>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT DISTINCT indice_relatorios.grupo");
                strSQL.Append(" FROM indice_relatorios");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        if (!drdIndicesGrafic.IsDBNull(drdIndicesGrafic.GetOrdinal("grupo"))) { clsGrupos.Add(drdIndicesGrafic.GetString(drdIndicesGrafic.GetOrdinal("grupo"))); } else { clsGrupos.Add(string.Empty); }
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrupos;
        }
    }
}
