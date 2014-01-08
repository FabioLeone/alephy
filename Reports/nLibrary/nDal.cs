using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace nLibrary
{
    class nDal
    {
        internal static object getAll()
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT Id, Descricao FROM redesfarmaceuticas ORDER BY Descricao";

            try
            {
                cnn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

                da.Fill(ds, "Redes");
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally {
                cnn.Close();
            }

            return ds;
        }

        internal static object getRelatory(DateTime dtFrom, DateTime dtTo, int nId, int sId)
        {
            List<relatory> lr = new List<relatory>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT 
            consolidado.CNPJ,
            farmacias.nomefantasia AS ""Nome Fantasia"",
            farmacias.RazaoSocial AS ""Razao Social"",
            consolidado.ano,
            consolidado.Mes,
            ""upper""(
            CASE 
		        WHEN consolidado.sub_consultoria IS NULL
			        OR consolidado.sub_consultoria = '' 
		        THEN 'NÃO INDENTIFICADO'
		        ELSE consolidado.sub_consultoria
            END
            ) as ""Sub Consultoria"",
            consolidado.Grupo,
            ""upper""(
            CASE 
		        WHEN consolidado.importado IS NULL
			        OR consolidado.importado = '0' 
		        THEN 'NACIONAL'
		        ELSE 'IMPORTADO'
            END
            ) as ""importado"",
            consolidado.Quantidade AS ""Soma De Quantidade"",
            consolidado.Valor_Bruto AS ""Soma De Valor bruto"",
            consolidado.Valor_Liquido AS ""Soma De Valor liquido"",
            consolidado.Valor_Desconto AS ""Soma De Valor desconto""
            FROM
            consolidado
            INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
            WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
            AND (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
            AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))");

            String ini, fim;

            ini = dtFrom.ToString("MM yyyy");
            fim = dtTo.ToString("MM yyyy");

            if (sId > 0)
            {
                SQL.Append(" AND farmacias.id = @id");
            }
            else if (nId > 0)
            {
                SQL.Append(" AND farmacias.idRede = @idRede");
            }

            SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            
            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@id", NpgsqlDbType.Integer).Value = sId;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = nId;
            cmm.CommandTimeout = 9999;
            
            try
            {
                cnn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

                da.Fill(ds, "Mod2");
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                cnn.Close();
            }

            try
            {
                if (ds.Tables.Count > 0 && ds.Tables["Mod2"].Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["Mod2"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["Mod2"].Rows.Count; i++)
                        {
                            relatory or = new relatory();

                            or.Razao = ds.Tables["Mod2"].Rows[i]["Razao Social"].ToString();
                            or.Cnpj = suLibrary.Util.MaskCnpj(ds.Tables["Mod2"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["Mod2"].Rows[i]["Sub Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["Mod2"].Rows[i]["Mes"];
                            or.Grupo = ds.Tables["Mod2"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor desconto"].ToString());
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }
                            or.NomeFantasia = ds.Tables["Mod2"].Rows[i]["Nome Fantasia"].ToString();
                            or.Ano = Convert.ToInt32(ds.Tables["Mod2"].Rows[i]["Ano"].ToString());
                            or.Importado = ds.Tables["Mod2"].Rows[i]["importado"].ToString();
                            or.Periodo = String.Format("{0} à {1}", dtFrom.ToString("MM/yyyy"), dtTo.ToString("MM/yyyy"));

                            lr.Add(or);
                        }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return lr;
        }

        internal static List<grafic> getGrafic(string strAIni, string strAFim, int nId, int sId)
        {
            List<grafic> clsGrafic = new List<grafic>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            List<string> lstCnpj = new List<string>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT farmacias.razaosocial,farmacias.nomefantasia, xTemp.* FROM (
                select farmaciaId, cnpj, mes, ano, grupo, sub_consultoria ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                ((UPPER(grupo) LIKE 'GENÉRICOS' and UPPER(sub_consultoria) like 'PDE 2%') 
	                OR 
	                (UPPER(grupo) LIKE 'ALTERNATIVOS' and UPPER(sub_consultoria) = 'PDE 2 (TRATA)')
	                OR 
	                (UPPER(grupo) LIKE 'PROPAGADOS' and UPPER(sub_consultoria) in ('PDE 1 (ANTI - RH)','PDE 2 (TRATA)')))
	                GROUP BY farmaciaId, cnpj, mes, ano, grupo, sub_consultoria 
                union
	                select farmaciaId, cnpj, mes, ano, 'Total', sub_consultoria,sum(valor_liquido) as ""Liquido"",
                    CASE WHEN SUM(consolidado.Valor_Bruto) > 0 THEN SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto) ELSE 0 END as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
	                upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
	                AND
	                upper(sub_consultoria) in ('PDE 2 (TRATA)','PORT (PSICO)','RELAC (PBM)')
	                GROUP BY farmaciaId, cnpj, mes, ano, sub_consultoria
                union
	                select farmaciaId, cnpj, mes, ano, 'zzzzzz', NULL ,sum(valor_liquido) as ""Liquido"",SUM(consolidado.Valor_Desconto) / SUM(consolidado.Valor_Bruto)as ""Desconto"", 
                    sum(quantidade) as ""Quantidade""
	                from consolidado
	                WHERE 
                    upper(Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
			        GROUP BY farmaciaId, cnpj, mes, ano
                ) AS xTemp 
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ
                WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy')) AND
                (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))
                AND farmacias.idRede = @idRede");

                if (sId > 0)
                    strSQL.Append(" AND xTemp.farmaciaId = @id");

                strSQL.Append(" ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                NpgsqlCommand cmdGrafic = msc.CreateCommand();

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = nId;
                cmdGrafic.Parameters.Add("@ini", NpgsqlDbType.Varchar).Value = strAIni.Replace("/", " ");
                cmdGrafic.Parameters.Add("@fim", NpgsqlDbType.Varchar).Value = strAFim.Replace("/", " ");
                cmdGrafic.Parameters.Add("@id", NpgsqlDbType.Integer).Value = sId;

                cmdGrafic.CommandTimeout = 9999;

                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(GraficLoad(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }

        private static grafic GraficLoad(IDataReader drdGrafic)
        {
            grafic clsGrafic = new grafic();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("razaosocial"))) { clsGrafic.Razao_Social = drdGrafic.GetString(drdGrafic.GetOrdinal("razaosocial")); } else { clsGrafic.Razao_Social = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGrafic.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGrafic.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Ano"))) { clsGrafic.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Ano")); } else { clsGrafic.Ano = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Grupo"))) { clsGrafic.Grupo = drdGrafic.GetString(drdGrafic.GetOrdinal("Grupo")); } else { clsGrafic.Grupo = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Sub_Consultoria"))) { clsGrafic.Sub_Consultoria = drdGrafic.GetString(drdGrafic.GetOrdinal("Sub_Consultoria")); } else { clsGrafic.Sub_Consultoria = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Liquido"))) { clsGrafic.Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Liquido")); } else { clsGrafic.Liquido = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Desconto"))) { clsGrafic.Desconto = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Desconto")); } else { clsGrafic.Desconto = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("nomefantasia"))) clsGrafic.Nome_Fantasia = drdGrafic.GetString(drdGrafic.GetOrdinal("nomefantasia")); else clsGrafic.Nome_Fantasia = string.Empty;
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Quantidade"))) clsGrafic.quantidade = drdGrafic.GetInt64(drdGrafic.GetOrdinal("Quantidade")); else clsGrafic.quantidade = 0;

            return clsGrafic;
        }

        internal static List<IndicesGraficTO> getIndicesALL()
        {
            List<IndicesGraficTO> clsIndicesGrafic = new List<IndicesGraficTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT indice_relatorios.id, indice_relatorios.grupo, indice_relatorios.categoria, indice_relatorios.venda, indice_relatorios.desconto");
                strSQL.Append(" FROM indice_relatorios;");

                NpgsqlCommand cmdIndicesGrafic = msc.CreateCommand();
                cmdIndicesGrafic.CommandText = strSQL.ToString();
                cmdIndicesGrafic.CommandTimeout = 9999;
                msc.Open();

                using (IDataReader drdIndicesGrafic = cmdIndicesGrafic.ExecuteReader())
                {
                    while (drdIndicesGrafic.Read())
                    {
                        clsIndicesGrafic.Add(LoadIG(drdIndicesGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsIndicesGrafic;
        }

        private static IndicesGraficTO LoadIG(IDataReader drdIndices)
        {
            IndicesGraficTO clsIndicesGrafic = new IndicesGraficTO();

            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("id"))) { clsIndicesGrafic.id = drdIndices.GetInt32(drdIndices.GetOrdinal("id")); } else { clsIndicesGrafic.id = 0; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("grupo"))) { clsIndicesGrafic.grupo = drdIndices.GetString(drdIndices.GetOrdinal("grupo")); } else { clsIndicesGrafic.grupo = string.Empty; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("categoria"))) { clsIndicesGrafic.categoria = drdIndices.GetString(drdIndices.GetOrdinal("categoria")); } else { clsIndicesGrafic.categoria = string.Empty; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("venda"))) { clsIndicesGrafic.venda = drdIndices.GetDecimal(drdIndices.GetOrdinal("venda")); } else { clsIndicesGrafic.venda = 0; }
            if (!drdIndices.IsDBNull(drdIndices.GetOrdinal("desconto"))) { clsIndicesGrafic.desconto = drdIndices.GetDecimal(drdIndices.GetOrdinal("desconto")); } else { clsIndicesGrafic.desconto = 0; }

            return clsIndicesGrafic;
        }

        internal static List<grafic> getGrafic(string strIni, int sId, string strFim)
        {
            List<grafic> clsGrafic = new List<grafic>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
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
                INNER JOIN farmacias ON farmacias.Cnpj = xTemp.CNPJ
                WHERE (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') >= to_date(@ini, 'MM yyyy'))
                AND (to_date(to_char(xTemp.mes,'99') || to_char(xTemp.ano,'9999'), 'MM yyyy') <= to_date(@fim, 'MM yyyy'))");

                NpgsqlCommand cmdGrafic = msc.CreateCommand();

                if (sId > 0)
                    strSQL.Append(" AND farmacias.id = '" + sId + "'");
                
                strSQL.Append(" ORDER BY Ano,Mes,Grupo,Sub_Consultoria");

                cmdGrafic.CommandText = strSQL.ToString();
                cmdGrafic.Parameters.Clear();
                cmdGrafic.Parameters.Add("@ini", NpgsqlDbType.Varchar).Value = strIni.Replace("/", " ");
                cmdGrafic.Parameters.Add("@fim", NpgsqlDbType.Varchar).Value = strFim.Replace("/", " ");
                cmdGrafic.CommandTimeout = 9999;

                msc.Open();

                using (IDataReader drdGrafic = cmdGrafic.ExecuteReader())
                {
                    while (drdGrafic.Read())
                    {
                        clsGrafic.Add(GraficLoad(drdGrafic));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGrafic;
        }
    }
}
