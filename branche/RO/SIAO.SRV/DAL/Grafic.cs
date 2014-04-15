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
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("Grupo")); } else { clsGrafic.Grupo = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Sub_Consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("Sub_Consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Desconto"))) { clsGrafic.Desconto = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Desconto")); } else { clsGrafic.Desconto = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.Nome_Fantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.Nome_Fantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Quantidade"))) clsGrafic.quantidade = drdGrafic.GetInt64(drdGrafic.GetOrdinal("Quantidade")); else clsGrafic.quantidade = 0;
            try
                {
                    if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
                }
            catch 
            {
                clsGrafic.Mes = 0;
            }

            try
            {
                
                    if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Ano"))) { clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Ano")); } else { clsGrafic.Ano = 0; }
            }
            catch 
            {
                clsGrafic.Ano = 0;
            }
            return clsGrafic;
        }

        private static Grafic2TO LoadGrficII(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("CNPJ"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("CNPJ")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("NomeFantasia"))) clsGrafic.Nome_Fantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("NomeFantasia")); else clsGrafic.Nome_Fantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("RazaoSocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("RazaoSocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Ano"))) clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Ano")); else clsGrafic.Ano = 0;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("sub_consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("sub_consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("quantidade"))) { clsGrafic.quantidade = drdGrafic.GetInt64(drdGrafic.GetOrdinal("quantidade")); } else { clsGrafic.quantidade = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("grupo"))) clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("grupo")); else clsGrafic.Grupo = string.Empty;

            return clsGrafic;
        }

        private static Grafic2TO LoadGrficIII(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("cnpj"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("cnpj")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.Nome_Fantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.Nome_Fantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("razaosocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("razaosocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("ano"))) clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("ano")); else clsGrafic.Ano = 0;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("grupo")); } else { clsGrafic.Grupo = string.Empty; }
            try
            {
                if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }
            }
            catch
            {
                clsGrafic.Liquido = 0;
            }


            return clsGrafic;
        }

        private static Grafic2TO LoadGrficIV(IDataReader drdGrafic)
        {
            Grafic2TO clsGrafic = new Grafic2TO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("cnpj"))) clsGrafic.CNPJ = drdGrafic.GetString(drdGrafic.GetOrdinal("cnpj")); else clsGrafic.CNPJ = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.Nome_Fantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.Nome_Fantasia = string.Empty;
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
                strSQL.Append(@"SELECT
                farmacias.razaosocial,
                farmacias.nomefantasia,
                xTemp.cnpj, xTemp.grupo, xTemp.sub_consultoria, SUM(xTemp.""Liquido"") AS ""Liquido"", 
                AVG(xTemp.""Desconto"") AS ""Desconto"", SUM(xTemp.""Quantidade"")::BIGINT AS ""Quantidade""
                FROM
                (
	                SELECT
		                cnpj,
		                mes,
		                ano,
		                grupo,
		                sub_consultoria,
		                SUM (valor_liquido) AS ""Liquido"",
		                SUM (consolidado.Valor_Desconto) / SUM (consolidado.Valor_Bruto) AS ""Desconto"",
		                SUM (quantidade) AS ""Quantidade""
	                FROM
		                consolidado
	                WHERE
		                (
			                (
				                UPPER (grupo) LIKE 'GENÉRICOS'
				                AND UPPER (sub_consultoria) LIKE 'PDE 2%'
			                )
			                OR (
				                UPPER (grupo) LIKE 'ALTERNATIVOS'
				                AND UPPER (sub_consultoria) = 'PDE 2 (TRATA)'
			                )
			                OR (
				                UPPER (grupo) LIKE 'PROPAGADOS'
				                AND UPPER (sub_consultoria) IN (
					                'PDE 1 (ANTI - RH)',
					                'PDE 2 (TRATA)'
				                )
			                )
		                )
	                GROUP BY
		                cnpj,
		                mes,
		                ano,
		                grupo,
		                sub_consultoria
	                UNION
		                SELECT
			                cnpj,
			                mes,
			                ano,
			                'Total',
			                sub_consultoria,
			                SUM (valor_liquido) AS ""Liquido"",
			                CASE
		                WHEN SUM (consolidado.Valor_Bruto) > 0 THEN
			                SUM (consolidado.Valor_Desconto) / SUM (consolidado.Valor_Bruto)
		                ELSE
			                0
		                END AS ""Desconto"",
		                SUM (quantidade) AS ""Quantidade""
	                FROM
		                consolidado
	                WHERE
		                UPPER (Grupo) IN (
			                'PROPAGADOS',
			                'ALTERNATIVOS',
			                'GENÉRICOS'
		                )
	                AND UPPER (sub_consultoria) IN (
		                'PDE 2 (TRATA)',
		                'PORT (PSICO)',
		                'RELAC (PBM)'
	                )
	                GROUP BY
		                cnpj,
		                mes,
		                ano,
		                sub_consultoria
	                UNION
		                SELECT
			                cnpj,
			                mes,
			                ano,
			                'zzzzzz',
			                NULL,
			                SUM (valor_liquido) AS ""Liquido"",
			                CASE
		                WHEN SUM (consolidado.Valor_Bruto) > 0 THEN
			                SUM (consolidado.Valor_Desconto) / SUM (consolidado.Valor_Bruto)
		                ELSE
			                0
		                END AS ""Desconto"",
		                SUM (quantidade) AS ""Quantidade""
	                FROM
		                consolidado
	                WHERE
		                UPPER (Grupo) IN (
			                'PROPAGADOS',
			                'ALTERNATIVOS',
			                'GENÉRICOS'
		                )
	                GROUP BY
		                cnpj,
		                mes,
		                ano
                ) AS xTemp
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                        break;
                                    case 2:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                            break;
                        case 3:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                            break;
                    }
                }

                strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy'))
                AND (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND farmacias.Cnpj = '" + strLoja + "'");
                else if (!clsUser.TipoId.Equals(1))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                strSQL.Append(@" GROUP BY farmacias.razaosocial,
	            farmacias.nomefantasia,
	            xTemp.cnpj, xTemp.grupo, xTemp.sub_consultoria
                ORDER BY
	                Grupo,
	                Sub_Consultoria");

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
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

        internal static List<GraficTO> GetGraficMes(string strIni, UsersTO clsUser, string strFim, int idRede, string strCnpj)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"SELECT");
            
            if(String.IsNullOrEmpty(strCnpj)) strSQL.Append(" r.descricao as razaosocial,r.descricao as nomefantasia, r.cnpj,");
            else strSQL.Append(" farmacias.razaosocial,farmacias.nomefantasia, xTemp.cnpj,");

            strSQL.Append(@" xTemp.grupo, xTemp.sub_consultoria, SUM(xTemp.""Liquido"") as ""Liquido"", AVG(xTemp.""Desconto"") as ""Desconto"", SUM(xTemp.""Quantidade"")::BIGINT as ""Quantidade"" FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
                    upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
			        GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

            if(String.IsNullOrEmpty(strCnpj)) strSQL.Append(" LEFT JOIN redesfarmaceuticas r ON farmacias.idrede = r.id");

            strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy')) AND
                (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");


            if (idRede > 0)
                strSQL.Append(" AND farmacias.idRede = @idRede");

            if (!String.IsNullOrEmpty(strCnpj))
                strSQL.Append(" AND xTemp.CNPJ = @CNPJ");
            else if (clsUser.FarmaciaId > 0)
                strSQL.Append(" AND farmacias.id = @id");

            if (String.IsNullOrEmpty(strCnpj)) strSQL.Append(" GROUP BY r.descricao, r.cnpj, xTemp.grupo, xTemp.sub_consultoria");
            else strSQL.Append(" GROUP BY farmacias.razaosocial,farmacias.nomefantasia, xTemp.cnpj, xTemp.grupo, xTemp.sub_consultoria");

            strSQL.Append(" ORDER BY Grupo, Sub_Consultoria");

            DbCommand cmdGrafic = msc.CreateCommand();

            cmdGrafic.CommandText = strSQL.ToString();
            cmdGrafic.Parameters.Clear();
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", idRede));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@id", clsUser.FarmaciaId));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/", " ")));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/", " ")));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strCnpj));

            try
            {
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

        internal static List<GraficTO> GetGraficMes(string strIni, UsersTO clsUser, string strFim, int idRede, string strCnpj, bool blnSum)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"SELECT");
            
            if(blnSum) strSQL.Append(" r.descricao as razaosocial, r.descricao as nomefantasia, r.cnpj,");
            else strSQL.Append(" farmacias.razaosocial,farmacias.nomefantasia, xTemp.cnpj,");
            
            strSQL.Append(@" xTemp.mes,xTemp.ano,xTemp.grupo,xTemp.sub_consultoria,xTemp.""Liquido"",
	            xTemp.""Desconto"",xTemp.""Quantidade"" FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
                    upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
			        GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

            if (String.IsNullOrEmpty(strCnpj) && ((clsUser.TipoId == 1 && clsUser.Nivel == 2) || clsUser.TipoId == 2)) strSQL.Append(" LEFT JOIN usuarios_vinculos u ON farmacias.id = u.farmaciaid");
            if (blnSum) strSQL.Append(" LEFT JOIN redesfarmaceuticas r ON farmacias.idrede = r.id");

            strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy')) AND
                (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

            if (idRede > 0)
                strSQL.Append(" AND farmacias.idRede = @idRede");

            if (!String.IsNullOrEmpty(strCnpj))
                strSQL.Append(" AND xTemp.CNPJ = @CNPJ");
            else if (clsUser.FarmaciaId > 0 && !blnSum)
                strSQL.Append(" AND farmacias.id = @id");
            else if (blnSum && ((clsUser.TipoId == 1 && clsUser.Nivel == 2) || clsUser.TipoId == 2)) strSQL.Append(" AND u.usuarioid = @userId");

            strSQL.Append(" ORDER BY cnpj,Ano,Mes,Grupo,Sub_Consultoria");

            DbCommand cmdGrafic = msc.CreateCommand();

            cmdGrafic.CommandText = strSQL.ToString();
            cmdGrafic.Parameters.Clear();
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", idRede));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@id", clsUser.FarmaciaId));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@userId", clsUser.UserId));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/", " ")));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/", " ")));
            cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strCnpj));

            cmdGrafic.CommandTimeout = 9999;

            try
            {
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

        internal static List<GraficTO> GetGrafic2Mes(string strIni, UsersTO clsUser, string strFim, int idRede, string strCnpj)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT");

                if (String.IsNullOrEmpty(strCnpj)) strSQL.Append(@" r.descricao as razaosocial, r.descricao as nomefantasia, r.cnpj,
                xTemp.mes,xTemp.ano,xTemp.grupo,xTemp.sub_consultoria,SUM(xTemp.""Liquido"") as ""Liquido"", AVG(xTemp.""Desconto"") as ""Desconto"", SUM(xTemp.""Quantidade"")::BIGINT as ""Quantidade""");
                else strSQL.Append(@" farmacias.razaosocial,farmacias.nomefantasia, xTemp.*");
                
                strSQL.Append(@" FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
                    upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
			        GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

                if (String.IsNullOrEmpty(strCnpj)) strSQL.Append(" LEFT JOIN redesfarmaceuticas r on farmacias.idrede = r.id");

                strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy')) AND
                (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

                if (idRede > 0)
                    strSQL.Append(" AND farmacias.idRede = @idRede");

                if (!String.IsNullOrEmpty(strCnpj))
                    strSQL.Append(" AND xTemp.CNPJ = @CNPJ");
                else if (clsUser.FarmaciaId > 0)
                    strSQL.Append(" AND farmacias.id = @id");

                if (String.IsNullOrEmpty(strCnpj)) strSQL.Append(@" GROUP BY r.descricao, r.descricao, r.cnpj,
                xTemp.mes,xTemp.ano,xTemp.grupo,xTemp.sub_consultoria");

                strSQL.Append(" ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                DbCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", idRede));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@id", clsUser.FarmaciaId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/", " ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/", " ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strCnpj));

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

        public static List<GraficTO> GetGrafic2Mes(string strIni, UsersTO clsUser, string strLoja, string strFim)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT farmacias.razaosocial,farmacias.nomefantasia, xTemp.* FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
                    GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                        break;
                                    case 2:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                            break;
                        case 3:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                            break;
                    }
                }

                strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy'))
                AND (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND farmacias.Cnpj = '" + strLoja + "'");
                else if (!clsUser.TipoId.Equals(1))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                strSQL.Append(" ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
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

        internal static List<Grafic2TO> Grafic31ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId, string strCnpj)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT * FROM
                (SELECT
                consolidado.cnpj,
                farmacias.nomefantasia,
                farmacias.razaosocial,
                consolidado.ano,
                consolidado.mes,
                consolidado.grupo,
                sum(consolidado.valor_liquido) AS ""Liquido""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.cnpj = consolidado.cnpj
                WHERE UPPER(consolidado.grupo) like any ('{PROPAGADOS,ALTERNATIVOS,GENÉRICOS}')
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede");

                if (!String.IsNullOrEmpty(strCnpj))
                    strSQL.Append(" AND consolidado.cnpj = @cnpj");

                strSQL.Append(@" GROUP BY consolidado.cnpj,
                farmacias.nomefantasia,
                farmacias.razaosocial,
                consolidado.ano,
                consolidado.mes,
                consolidado.grupo
                ) AS a ORDER BY Ano, Mes, Cnpj DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@cnpj", strCnpj));
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
                consolidado.cnpj,
                farmacias.nomefantasia,
                farmacias.razaosocial,
                consolidado.ano,
                consolidado.mes,
                consolidado.grupo,
                sum(consolidado.valor_liquido) AS ""Liquido""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.cnpj = consolidado.cnpj");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                        break;
                                    case 2:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                            break;
                        case 3:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                            break;
                    }
                }

                strSQL.Append(@" WHERE
                UPPER(consolidado.grupo) like any ('{PROPAGADOS,ALTERNATIVOS,GENÉRICOS}')
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

                if (!clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND consolidado.cnpj = @cnpj");

                strSQL.Append(@" GROUP BY consolidado.cnpj,
                farmacias.nomefantasia,
                farmacias.razaosocial,
                consolidado.ano,
                consolidado.mes,
                consolidado.grupo
                ) AS a ORDER BY Ano, Mes, Cnpj DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataIni", strIni));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@DataFim", strFim));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@cnpj", strLoja));
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

        internal static List<Grafic2TO> Grafic32ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId, string strCnpj)
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
		                WHEN consolidado.Grupo IS NULL THEN 'NE'
		                ELSE consolidado.Grupo
                END
                ) as Grupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                WHERE (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede");

                if (!String.IsNullOrEmpty(strCnpj))
                    strSQL.Append(" AND consolidado.CNPJ = @CNPJ");

                strSQL.Append(@" ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, Grupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (String.IsNullOrEmpty(strIni) && String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
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
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strCnpj));
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
		                WHEN consolidado.Grupo IS NULL THEN 'NE'
		                ELSE consolidado.Grupo
                END
                ) as Grupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                        break;
                                    case 2:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                            break;
                        case 3:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                            break;
                    }
                }

                strSQL.Append(@" WHERE 
                (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

                if (!clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND consolidado.CNPJ = @CNPJ");

                strSQL.Append(@") a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, Grupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (String.IsNullOrEmpty(strIni) && String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString();
                    strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
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
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strLoja));
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

        internal static List<Grafic2TO> Grafic33ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId, string strCnpj)
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
		                WHEN consolidado.sub_consultoria IS NULL THEN 'NE'
		                ELSE consolidado.sub_consultoria
                END
                ) as SubGrupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                WHERE (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                AND farmacias.idRede = @idRede");

                if (!String.IsNullOrEmpty(strCnpj))
                    strSQL.Append(" AND consolidado.CNPJ = @CNPJ");

                strSQL.Append(@" ) a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, SubGrupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (String.IsNullOrEmpty(strIni) && String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
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
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strCnpj));
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
		                WHEN consolidado.sub_consultoria IS NULL THEN 'NE'
		                ELSE consolidado.sub_consultoria
                END
                ) as SubGrupo,
                consolidado.Valor_Liquido
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                        break;
                                    case 2:
                                        strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                            break;
                        case 3:
                            strSQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                            break;
                    }
                }

                strSQL.Append(@" WHERE (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) 
                AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

                if (!clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0))
                    strSQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND consolidado.CNPJ = @CNPJ");

                strSQL.Append(@") a GROUP BY CNPJ, nomefantasia, razaosocial, Ano, Mes, SubGrupo
                ORDER BY Ano, Mes DESC");

                DbCommand cmdGrafic = msc.CreateCommand();

                if (String.IsNullOrEmpty(strIni) && String.IsNullOrEmpty(strFim))
                {
                    strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
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
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@CNPJ", strLoja));
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

        internal static List<GraficTO> GetLastMonth(UsersTO clsUser, string strIni, string strFim, string strLoja, int intRedeId)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT farmacias.razaosocial,farmacias.nomefantasia, xTemp.* FROM (
                select cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY cnpj, mes, ano, sub_consultoria
                union
	                select cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
                    upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
			        GROUP BY cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ");

                if ((clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0)) || !clsUser.TipoId.Equals(1))
                {
                    switch (clsUser.TipoId)
                    {
                        case 1:
                            {
                                switch (clsUser.Nivel)
                                {
                                    case 1:
                                        strSQL.Append(@" LEFT JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid 
                                        WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') = to_date((
                                        SELECT c.Mes || ' ' || c.Ano FROM consolidado c LEFT JOIN farmacias f ON c.cnpj = f.cnpj");
                                        break;
                                    case 2:
                                        strSQL.Append(@" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid 
                                        WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') = to_date((
                                        SELECT c.Mes || ' ' || c.Ano FROM consolidado c LEFT JOIN farmacias f ON c.cnpj = f.cnpj");
                                        break;
                                }
                            }
                            break;
                        case 2:
                            strSQL.Append(@" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid 
                            WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') = to_date(( SELECT c.Mes || ' ' || c.Ano 
                            FROM consolidado c 
                            LEFT JOIN farmacias f ON c.cnpj = f.cnpj 
                            INNER JOIN usuarios_vinculos uv ON f.Id = uv.farmaciaid OR f.idRede = uv.redeid");
                            break;
                        case 3:
                            strSQL.Append(@" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid 
                            WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') = to_date(( SELECT c.Mes || ' ' || c.Ano 
                            FROM consolidado c 
                            LEFT JOIN farmacias f ON c.cnpj = f.cnpj 
                            INNER JOIN usuarios_vinculos uv ON f.Id = uv.farmaciaid OR f.idRede = uv.redeid");
                            break;
                    }
                }
                else
                    strSQL.Append(@" WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') = to_date(( SELECT c.Mes || ' ' || c.Ano 
                                        FROM consolidado c 
                                        LEFT JOIN farmacias f ON c.cnpj = f.cnpj 
                                        INNER JOIN usuarios_vinculos uv ON f.Id = uv.farmaciaid OR f.idRede = uv.redeid");

                if (intRedeId > 0)
                    strSQL.Append(@" AND f.idRede = @idRede ORDER BY ano DESC, mes DESC LIMIT 1
                ), 'MM yyyy')) AND farmacias.idRede = @idRede");


                if (!String.IsNullOrEmpty(strLoja) && intRedeId > 0)
                    strSQL.Append(" AND farmacias.Cnpj = '" + strLoja + "'");
                else if (!String.IsNullOrEmpty(strLoja))
                    strSQL.Append(" AND f.Cnpj = '" + strLoja + "' ORDER BY ano DESC, mes DESC LIMIT 1), 'MM yyyy')) AND farmacias.Cnpj = '" + strLoja + "'");
                else if (!clsUser.TipoId.Equals(1) && !clsUser.Nivel.Equals(0))
                    strSQL.Append(" AND uv.usuarioid = @UsuarioId ORDER BY ano DESC, mes DESC LIMIT 1), 'MM yyyy')) AND usuarios_vinculos.UsuarioId = @UsuarioId");

                strSQL.Append(" ORDER BY Ano,Mes,Sub_Consultoria,Grupo");

                DbCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@idRede", intRedeId));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@ini", strIni.Replace("/", " ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.String, "@fim", strFim.Replace("/", " ")));
                cmdGrafic.Parameters.Add(DbHelper.GetParameter(cmdGrafic, DbType.Int32, "@UsuarioId", clsUser.UserId));

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
