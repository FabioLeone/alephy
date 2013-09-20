using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using SIAO.SRV.TO;

namespace SIAO.SRV
{
    internal class RelatoriosDAL
    {
        #region .:Search:.
        internal static List<clsRelat1> GetMod2(UsersTO clsUser, string strIni, string strFim, int intRedeId, string strCnpj)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
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
            INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

            if(clsUser.TipoId.Equals(1)) SQL.Append(" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid OR farmacias.idRede = usuarios_vinculos.redeid");
            else SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid OR farmacias.idRede = usuarios_vinculos.redeid");

            SQL.Append(@" WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
            AND (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
            AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))");

            String ini, fim;

            ini = strIni.Replace('/', ' ');
            fim = strFim.Replace('/', ' ');

            if (clsUser.TipoId.Equals(1))
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }
                else if (intRedeId > 0)
                {
                    SQL.Append(" AND farmacias.idRede = @idRede");
                }

                SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }
            else
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }

                SQL.Append(@" AND usuarios_vinculos.UsuarioId = @UsuarioId
                    ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = strCnpj;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@UsuarioId", NpgsqlDbType.Integer).Value = clsUser.UserId;
            cmm.CommandTimeout = 9999;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Mod2");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0 && ds.Tables["Mod2"].Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["Mod2"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["Mod2"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["Mod2"].Rows[i]["Razao Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["Mod2"].Rows[i]["Cnpj"].ToString());
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
                            or.Periodo = String.Format("{0} à {1}", strIni, strFim);

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        internal static List<clsRelat1> GetMod2(UsersTO clsUser, string strCnpj, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            string strMF = DateTime.Now.AddMonths(-1).Month.ToString();
            string strMI = DateTime.Now.AddMonths(-6).Month.ToString();

            string strAF = DateTime.Now.Year.ToString();
            string strAI = DateTime.Now.AddMonths(-6).Year.ToString();

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
            INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

            if(clsUser.TipoId.Equals(1)) SQL.Append(" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid OR farmacias.idRede = usuarios_vinculos.redeid");
            else SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid OR farmacias.idRede = usuarios_vinculos.redeid");

            SQL.Append(String.Format(@" WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
            AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM-yyyy') >= to_date('{0} {1}','MM-yyyy')) AND
            (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM-yyyy') <= to_date('{2} {3}','MM-yyyy'))", strMI, strAI, strMF, strAF));

            if (clsUser.TipoId.Equals(1))
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }
                else if (intRedeId > 0)
                {
                    SQL.Append(" AND farmacias.idRede = @idRede");
                }

                SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }
            else
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }
                else if (intRedeId > 0)
                {
                    SQL.Append(" AND farmacias.idRede = @idRede");
                }

                SQL.Append(@" AND usuarios_vinculos.UsuarioId = @UsuarioId
                ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = strCnpj;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@UsuarioId", NpgsqlDbType.Integer).Value = clsUser.UserId;
            cmm.CommandTimeout = 9999;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Mod2");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["Mod2"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["Mod2"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["Mod2"].Rows[i]["Razao Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["Mod2"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["Mod2"].Rows[i]["Sub Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["Mod2"].Rows[i]["Mes"];
                            or.Ano = (int)ds.Tables["Mod2"].Rows[i]["Ano"];
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
                            or.Importado = ds.Tables["Mod2"].Rows[i]["importado"].ToString();
                            or.Periodo = String.Format("{0} à {1}", strMI + "/" + strAI, strMF + "/" + strAF);

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        internal static List<clsRelat1> GetMod2(UsersTO clsUser, string strIni, string strFim, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT 
                redesfarmaceuticas.CNPJ,
                redesfarmaceuticas.descricao AS ""Nome Fantasia"",
                redesfarmaceuticas.descricao AS ""Razao Social"",
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
                SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
                SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"",
                SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
                SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                LEFT JOIN redesfarmaceuticas ON farmacias.idRede = redesfarmaceuticas.id
                WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
                AND farmacias.idRede = @idRede
                AND (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
                AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))
                GROUP BY redesfarmaceuticas.CNPJ, redesfarmaceuticas.descricao, redesfarmaceuticas.descricao, consolidado.ano, consolidado.Mes,
                consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado                
                ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");

            String ini, fim;

            ini = strIni.Replace('/', ' ');
            fim = strFim.Replace('/', ' ');

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.CommandTimeout = 9999;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Mod2");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["Mod2"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["Mod2"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["Mod2"].Rows[i]["Razao Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["Mod2"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["Mod2"].Rows[i]["Sub Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["Mod2"].Rows[i]["Mes"];
                            or.Grupo = ds.Tables["Mod2"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["Mod2"].Rows[i]["Soma De Valor desconto"].ToString());
                            or.NomeFantasia = ds.Tables["Mod2"].Rows[i]["Nome Fantasia"].ToString();
                            or.Ano = Convert.ToInt32(ds.Tables["Mod2"].Rows[i]["Ano"].ToString());
                            or.Importado = ds.Tables["Mod2"].Rows[i]["importado"].ToString();
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }
                            or.Periodo = String.Format("{0} à {1}", strIni, strFim);

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        internal static List<clsRelat1> GetMod2(UsersTO clsUser, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            string SQL = @"SELECT 
                redesfarmaceuticas.CNPJ,
                redesfarmaceuticas.descricao AS ""Nome Fantasia"",
                redesfarmaceuticas.descricao AS ""Razao Social"",
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
                SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
                SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"",
                SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
                SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto""
                FROM
                consolidado
                INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                LEFT JOIN redesfarmaceuticas ON farmacias.idRede = redesfarmaceuticas.id
                WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
                AND farmacias.idRede = @idRede
                AND (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
                AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))
                GROUP BY redesfarmaceuticas.CNPJ, redesfarmaceuticas.descricao, redesfarmaceuticas.descricao, consolidado.ano, consolidado.Mes,
                consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado                
                ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo";

            string strMF = DateTime.Now.Month.ToString();
            string strMI = DateTime.Now.AddMonths(-7).Month.ToString();

            string strAF = DateTime.Now.Year.ToString();
            string strAI = DateTime.Now.AddMonths(-7).Year.ToString();

            cmm.CommandText = SQL;
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = strMI + " " + strAI;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = strMF + " " + strAF;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;

            cmm.CommandTimeout = 9999;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Mod2");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["Mod2"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["Mod2"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["Mod2"].Rows[i]["Razao Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["Mod2"].Rows[i]["Cnpj"].ToString());
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
                            or.Periodo = String.Format("{0} à {1}", strMI + "/" + strAI, strMF + "/" + strAF);

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }
        #endregion

    }
}
