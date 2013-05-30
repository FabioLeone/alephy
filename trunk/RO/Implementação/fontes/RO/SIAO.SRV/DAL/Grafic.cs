using System;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using Npgsql;
using System.Data.Common;
using System.Configuration;
using NpgsqlTypes;

namespace SIAO.SRV.DAL
{
    public class GraficDAL
    {
        #region .: Load :.

        private static GraficTO LoadGrfic(IDataReader drdGrafic)
        {
            GraficTO clsGrafic = new GraficTO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("razaosocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("razaosocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("Grupo")); } else { clsGrafic.Grupo = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Sub_Consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("Sub_Consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Desconto"))) { clsGrafic.Desconto = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Desconto")); } else { clsGrafic.Desconto = 0; }

            return clsGrafic;
        }

        private static Grafic2TO LoadGrficII(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("CNPJ"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("CNPJ")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("NomeFantasia"))) clsGrafic.NomeFantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("NomeFantasia")); else clsGrafic.NomeFantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("RazaoSocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("RazaoSocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Ano"))) clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Ano")); else clsGrafic.Ano = 0;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("sub_consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("sub_consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("quantidade"))) { clsGrafic.quantidade = drdGrafic.GetInt32(drdGrafic.GetOrdinal("quantidade")); } else { clsGrafic.quantidade = 0; }

            return clsGrafic;
        }

        private static Grafic2TO LoadGrficIII(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("cnpj"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("cnpj")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.NomeFantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.NomeFantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("razaosocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("razaosocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("ano"))) clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("ano")); else clsGrafic.Ano = 0;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("grupo")); } else { clsGrafic.Grupo = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }

            return clsGrafic;
        }

        private static Grafic2TO LoadGrficIV(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("cnpj"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("cnpj")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.NomeFantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.NomeFantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("razaosocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("razaosocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("ano"))) clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("ano")); else clsGrafic.Ano = 0;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("SubGrupo"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("SubGrupo")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }

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

        public static List<GraficTO> GetGraficMes(string strIni, UsersTO clsUser, string strLoja, string strFim)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT farmacias.razaosocial, xTemp.* FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                Grupo in ('Propagados','Alternativos','Genéricos')
	                AND
	                sub_consultoria in ('PDE 2 (trata)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                Grupo in ('Propagados','Alternativos','Genéricos')
                    GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ
                INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId
                WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy'))
                AND (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND farmacias.Cnpj = '" + strLoja + "'");
                else if(!clsUser.TipoId.Equals(1))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                strSQL.Append(" ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/"," ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/"," ")));
                cmdGrafic.CommandTimeout = 9999;

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

        internal static List<GraficTO> GetGraficMes(string strIni, UsersTO clsUser, string strFim, int idRede)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT farmacias.razaosocial, xTemp.* FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                Grupo in ('Propagados','Alternativos','Genéricos')
	                AND
	                sub_consultoria in ('PDE 2 (trata)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto""
	                from consolidado
	                WHERE 
	                Grupo in ('Propagados','Alternativos','Genéricos')
			        GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ
                INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId
                WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy'))
                AND (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))
                AND farmacias.idRede = @idRede
                ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                DbCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/", " ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/", " ")));

                cmdGrafic.CommandTimeout = 9999;

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

        public static List<IndicesGraficTO> GetIndicesALL()
        {
            List<IndicesGraficTO> clsIndicesGrafic = new List<IndicesGraficTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios;");

                DbCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();
                cmdIndicesGrafic.CommandTimeout = 9999;
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

        public static List<Grafic2TO> GetGraficAno(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                sub_consultoria,
                SUM(quantidade) AS ""quantidade""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                consolidado.sub_consultoria,
                consolidado.quantidade
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                UPPER(consolidado.sub_consultoria) like '%PDE%'
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!clsUser.TipoId.Equals(1))
                        strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                strSQL.Append(@") a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, sub_consultoria
                ORDER BY Ano, Mes DESC");

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/"," ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> GetGraficAno(string strIni, string strFim, UsersTO clsUsers, int intRedeId)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                sub_consultoria,
                SUM(quantidade) AS ""quantidade""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                consolidado.sub_consultoria,
                consolidado.quantidade
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE farmacias.idRede = @idRede 
                AND UPPER(consolidado.sub_consultoria) like '%PDE%'
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

                DbCommand cmdGrafic = msc.CreateCommand();

                strSQL.Append(@") a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, sub_consultoria
                ORDER BY Ano, Mes DESC");

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic31ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT * FROM
                (SELECT
                ""public"".consolidado.cnpj,
                ""public"".farmacias.nomefantasia,
                ""public"".farmacias.razaosocial,
                ""public"".consolidado.ano,
                ""public"".consolidado.mes,
                ""public"".consolidado.grupo,
                sum(""public"".consolidado.valor_liquido) AS ""Total Liquido""
                FROM
                ""public"".consolidado
                INNER JOIN ""public"".farmacias ON ""public"".farmacias.cnpj = ""public"".consolidado.cnpj
                WHERE
                ""public"".consolidado.grupo like any ('{Propagados,Alternativos,Genéricos}')
                GROUP BY ""public"".consolidado.cnpj,
                ""public"".farmacias.nomefantasia,
                ""public"".farmacias.razaosocial,
                ""public"".consolidado.ano,
                ""public"".consolidado.mes,
                ""public"".consolidado.grupo
                UNION ALL
                SELECT
                NULL,
                NULL,
                NULL,
                consolidado.Ano,
                consolidado.Mes,
                'Total',
                Sum(consolidado.Valor_Liquido) AS ""Liquido""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                ""public"".consolidado.grupo like any ('{Propagados,Alternativos,Genéricos}')
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede
                GROUP BY Ano, Mes
                ) AS a ORDER BY Ano, Mes, Cnpj DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic31ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT * FROM
                (SELECT
                ""public"".consolidado.cnpj,
                ""public"".farmacias.nomefantasia,
                ""public"".farmacias.razaosocial,
                ""public"".consolidado.ano,
                ""public"".consolidado.mes,
                ""public"".consolidado.grupo,
                sum(""public"".consolidado.valor_liquido) AS ""Total Liquido""
                FROM
                ""public"".consolidado
                INNER JOIN ""public"".farmacias ON ""public"".farmacias.cnpj = ""public"".consolidado.cnpj
                WHERE
                ""public"".consolidado.grupo like any ('{Propagados,Alternativos,Genéricos}')
                GROUP BY ""public"".consolidado.cnpj,
                ""public"".farmacias.nomefantasia,
                ""public"".farmacias.razaosocial,
                ""public"".consolidado.ano,
                ""public"".consolidado.mes,
                ""public"".consolidado.grupo
                UNION ALL
                SELECT
                NULL,
                NULL,
                NULL,
                consolidado.Ano,
                consolidado.Mes,
                'Total',
                Sum(consolidado.Valor_Liquido) AS ""Liquido""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                ""public"".consolidado.grupo like any ('{Propagados,Alternativos,Genéricos}')
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND usuarios_vinculos.UsuarioId = @UsuarioId
                GROUP BY Ano, Mes
                ) AS a ORDER BY Ano, Mes, Cnpj DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic32ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                Grupo,
                SUM(Valor_Liquido) AS ""Liquido""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                ""upper""(
                CASE 
		                WHEN consolidado.Grupo IS NULL THEN 'Não Identificado'
		                ELSE consolidado.Grupo
                END
                ) as Grupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede
                ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, Grupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic32ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                Grupo,
                SUM(Valor_Liquido) AS ""Liquido""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                ""upper""(
                CASE 
		                WHEN consolidado.Grupo IS NULL THEN 'Não Identificado'
		                ELSE consolidado.Grupo
                END
                ) as Grupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND usuarios_vinculos.UsuarioId = @UsuarioId
                ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, Grupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIII(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic33ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                SubGrupo,
                SUM(Valor_Liquido) AS ""Liquido""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                ""upper""(
                CASE 
		                WHEN consolidado.sub_consultoria IS NULL THEN 'Não Identificado'
		                ELSE consolidado.sub_consultoria
                END
                ) as SubGrupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede
                ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, SubGrupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIV(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        internal static List<Grafic2TO> Grafic33ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT 
                CNPJ,
                NomeFantasia,
                RazaoSocial,
                Ano,
                Mes,
                SubGrupo,
                SUM(Valor_Liquido) AS ""Liquido""
                FROM(
                SELECT
                consolidado.CNPJ,
                farmacias.NomeFantasia,
                farmacias.RazaoSocial,
                consolidado.Ano,
                consolidado.Mes,
                ""upper""(
                CASE 
		                WHEN consolidado.sub_consultoria IS NULL THEN 'Não Identificado'
		                ELSE consolidado.sub_consultoria
                END
                ) as SubGrupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                INNER JOIN usuarios_vinculos ON usuarios_vinculos.LinkId = farmacias.id OR usuarios_vinculos.LinkId = farmacias.idRede
                WHERE 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND usuarios_vinculos.UsuarioId = @UsuarioId
                ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, SubGrupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strIni) && !String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-7).Month.ToString() + " " + DateTime.Now.AddMonths(-7).Year.ToString();
                }
                else
                {
                    strIni = strIni.Replace("/", " ");
                    strFim = strFim.Replace("/", " ");
                }

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(LoadGrficIV(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }
      
        internal static IndicesGraficTO GetIndicesById(int intId, string strConnection)
        {
            IndicesGraficTO clsIndicesGrafic = new IndicesGraficTO();

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsetIndices(IndicesGraficTO clsIndicesGrafic, string strConnection)
        {
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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

    }
}
