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
        internal static List<clsRelat1> GetMod2(UsersTO clsUser, string strIni, string strFim, int intRedeId, string strCnpj, bool blnSum)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT ");

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(@" r.descricao AS ""Razao Social"",
	            r.descricao AS ""Nome Fantasia"",
	            r.cnpj,");
            else SQL.Append(@" consolidado.CNPJ,
                farmacias.nomefantasia AS ""Nome Fantasia"",
                farmacias.RazaoSocial AS ""Razao Social"",");

            SQL.Append(@" consolidado.ano,
            consolidado.Mes,
            ""upper""(
            CASE 
		        WHEN consolidado.sub_consultoria IS NULL
			        OR consolidado.sub_consultoria = '' 
		        THEN 'NÃO INDENTIFICADO'
		        ELSE consolidado.sub_consultoria
            END
            ) as ""Sub Consultoria"",
            ""upper""(
		    CASE 
		        WHEN upper(consolidado.Grupo) IN ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS','PERFUMARIA')
		        THEN consolidado.Grupo
		        ELSE 'EXTRA MEDICAMENTOS'
		    END
		    ) as ""Grupo"",
            ""upper""(
            CASE 
		        WHEN consolidado.importado IS NULL
			        OR consolidado.importado = '0' 
		        THEN 'NACIONAL'
		        ELSE 'IMPORTADO'
            END
            ) as ""importado"",");

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(@" SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
	            SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"",
	            SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
	            SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto""");
            else SQL.Append(@" consolidado.Quantidade AS ""Soma De Quantidade"",
                consolidado.Valor_Bruto AS ""Soma De Valor bruto"",
                consolidado.Valor_Liquido AS ""Soma De Valor liquido"",
                consolidado.Valor_Desconto AS ""Soma De Valor desconto""");

            SQL.Append(@" FROM
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
                                    SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                    break;
                                case 2:
                                    SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                    break;
                            }
                        }
                        break;
                    case 2:
                        SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                        break;
                    case 3:
                        SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                        break;
                }
            }

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" LEFT JOIN redesfarmaceuticas r ON farmacias.idrede = r.id");

            SQL.Append(@" WHERE (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
            AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))");

            String ini, fim;

            ini = strIni.Replace('/', ' ');
            fim = strFim.Replace('/', ' ');

            if (clsUser.TipoId.Equals(1) && clsUser.Nivel.Equals(0))
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }
                else if (intRedeId > 0)
                {
                    SQL.Append(" AND farmacias.idRede = @idRede");
                }

                if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes, consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado");
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

                SQL.Append(@" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes, consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado");

                SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
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

        internal static List<clsRelat1> GetMod2(UsersTO clsUser, string strCnpj, int intRedeId, bool blnSum)
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
            SQL.Append(@"SELECT ");

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(@"r.descricao AS ""Nome Fantasia"",
	            r.descricao AS ""Razao Social"",
	            r.cnpj,");
            else SQL.Append(@"consolidado.CNPJ,
            farmacias.nomefantasia AS ""Nome Fantasia"",
            farmacias.RazaoSocial AS ""Razao Social"",");

            SQL.Append(@" consolidado.ano,
            consolidado.Mes,
            ""upper""(
                CASE 
                WHEN consolidado.sub_consultoria IS NULL
                OR consolidado.sub_consultoria = '' 
                THEN 'NÃO INDENTIFICADO'
                ELSE consolidado.sub_consultoria
                END
            ) as ""Sub Consultoria"",
            ""upper""(
		    CASE 
		        WHEN upper(consolidado.Grupo) IN ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS','PERFUMARIA')
		        THEN consolidado.Grupo
		        ELSE 'EXTRA MEDICAMENTOS'
		    END
		    ) as ""Grupo"",
            ""upper""(
                CASE 
                WHEN consolidado.importado IS NULL
                OR consolidado.importado = '0' 
                THEN 'NACIONAL'
                ELSE 'IMPORTADO'
                END
            ) as ""importado"",");

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(@" SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
	            SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"",
	            SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
	            SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto""");
            else SQL.Append(@" consolidado.Quantidade AS ""Soma De Quantidade"",
                consolidado.Valor_Bruto AS ""Soma De Valor bruto"",
                consolidado.Valor_Liquido AS ""Soma De Valor liquido"",
                consolidado.Valor_Desconto AS ""Soma De Valor desconto""");

            SQL.Append(@" FROM
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
                                    SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                                    break;
                                case 2:
                                    SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                                    break;
                            }
                        }
                        break;
                    case 2:
                        SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.farmaciaid");
                        break;
                    case 3:
                        SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.idRede = usuarios_vinculos.redeid");
                        break;
                }
            }

            if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" LEFT JOIN redesfarmaceuticas r ON farmacias.idrede = r.id");

            SQL.Append(String.Format(@" WHERE (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM-yyyy') >= to_date('{0} {1}','MM-yyyy')) AND
            (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM-yyyy') <= to_date('{2} {3}','MM-yyyy'))", strMI, strAI, strMF, strAF));

            if (clsUser.TipoId.Equals(1) && clsUser.Nivel.Equals(0))
            {
                if (!String.IsNullOrEmpty(strCnpj))
                {
                    SQL.Append(" AND farmacias.Cnpj = @Cnpj");
                }
                else if (intRedeId > 0)
                {
                    SQL.Append(" AND farmacias.idRede = @idRede");
                }

                if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes, consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado");

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

                SQL.Append(@" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (String.IsNullOrEmpty(strCnpj) && blnSum) SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes, consolidado.sub_consultoria, consolidado.Grupo, consolidado.importado");

                SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
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

        internal static List<PercReport> GetPercent(UsersTO clsUser, string strIni, string strFim, int intRedeId, string strCnpj)
        {
            List<PercReport> lst = new List<PercReport>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT
            consolidado.mes,
            consolidado.ano,
            consolidado.cnpj,
            consolidado.sub_consultoria,
            ((SUM(valor_liquido) / vl) * 100)::numeric(12,2) AS ""participação""
            FROM
            consolidado
            INNER JOIN ""v.totalliquido"" ON consolidado.cnpj = ""v.totalliquido"".cnpj
            AND ""v.totalliquido"".mes = consolidado.mes
            AND ""v.totalliquido"".ano = consolidado.ano
            WHERE
            consolidado.cnpj = @cnpj
            AND (to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') >= to_date( @DataIni, 'MM yyyy')
            AND to_date(to_char(consolidado.mes,'99') || to_char(consolidado.ano,'9999'), 'MM yyyy') <= to_date( @DataFim, 'MM yyyy'))
            GROUP BY
            consolidado.mes,
            consolidado.ano,
            consolidado.cnpj,
            consolidado.sub_consultoria,
            vl
            ORDER BY ano,mes");

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = strCnpj;
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = strIni;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = strFim;

            try{
                cnn.Open();

                using (IDataReader drd = cmm.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        lst.Add(Load(drd));
                    }
                }
            }
            finally
            {
                cnn.Close();
            }

            return lst;
        }
        #endregion

        #region .:Loads:.
        private static PercReport Load(IDataReader drd)
        {
            PercReport obj = new PercReport();

            if (!drd.IsDBNull(drd.GetOrdinal("mes"))) obj.Mes = drd.GetInt32(drd.GetOrdinal("mes")); else obj.Mes = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("ano"))) obj.Ano = drd.GetInt32(drd.GetOrdinal("ano")); else obj.Ano = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("cnpj"))) obj.Cnpj = drd.GetString(drd.GetOrdinal("cnpj")); else obj.Cnpj = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("sub_consultoria"))) obj.SubConsultoria = drd.GetString(drd.GetOrdinal("sub_consultoria")); else obj.SubConsultoria = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("participação"))) obj.Participacao = drd.GetDecimal(drd.GetOrdinal("participação")); else obj.Participacao = 0;

            return obj;
        }
        #endregion
    }
}
