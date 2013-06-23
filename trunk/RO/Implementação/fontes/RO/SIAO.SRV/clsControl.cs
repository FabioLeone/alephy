﻿using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data.SqlClient;
using System.Configuration;
using Npgsql;
using NpgsqlTypes;
using System.Linq;

namespace SIAO.SRV
{
    public class clsControl
    {
        #region .:Variables:.
        clsDB clsDB = new clsDB();
        NpgsqlCommand cmm = new NpgsqlCommand();
        clsFuncs o = new clsFuncs();
        #endregion

        #region .:Search:.

        public System.Data.DataSet GetUser(string scn, string p)
        {
            string s = o.encr(p);
            DataSet ds = new DataSet();

            cmm.CommandText = "SELECT users.UserName, memberships.UserId"
                + " FROM  users INNER JOIN"
                + " memberships ON users.UserId = memberships.UserId"
                + " WHERE (memberships.Inactive = 0) AND (memberships.Access = '" + s + "')"
                + " AND (memberships.ExpirationDate > SYSDATE())";

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Gerentes");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public static DataSet GetRedes(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            clsDB clsDB = new clsDB();

            cmm.Connection = cnn;

            cmm.CommandText = "SELECT Id, Descricao FROM redesfarmaceuticas ORDER BY Descricao";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Redes");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public static Rede GetRedeByUserId(int intUserId)
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            Rede r = new Rede();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,usuarios_vinculos.usuarioId FROM  redesfarmaceuticas 
            INNER JOIN usuarios_vinculos ON redesfarmaceuticas.Id = usuarios_vinculos.LinkId
            WHERE (usuarios_vinculos.usuarioId = @usuarioId)";

            cmm.Parameters.Add("@usuarioId", NpgsqlDbType.Integer).Value = intUserId;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
                {
                    r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                    r.RedeName = ds.Tables[0].Rows[0]["Descricao"].ToString();
                    r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["usuarioId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["usuarioId"]);
                }
            }

            return r;
        }

        public Int32 GetByName(string strName)
        {
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            clsDB clsDB = new clsDB();
            Int32 intRedeId = 0;

            cmm.Connection = cnn;

            cmm.CommandText = "SELECT Id FROM redesfarmaceuticas WHERE Descricao=@Descricao";
            cmm.Parameters.Add("@Descricao", NpgsqlDbType.Varchar).Value = strName;

            if (clsDB.openConnection(cmm))
            {
                Int32.TryParse(clsDB.Query(intRedeId,ref cmm).ToString(),out intRedeId);
            }
            clsDB.closeConnection(cmm);

            return intRedeId;
        }

        public static Loja GetLojaByCNPJ(string strCNPJ)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            Loja clsLoja = new Loja();

            NpgsqlCommand cmm = new NpgsqlCommand();
            clsDB clsDB = new clsDB();

            cmm.Connection = cnn;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"SELECT farmacias.Id, farmacias.Proprietario, farmacias.Gerente, farmacias.Email,
            farmacias.Email2, farmacias.NomeFantasia, farmacias.RazaoSocial, farmacias.Cnpj,
            farmacias.Endereco, farmacias.Numero, farmacias.Bairro, farmacias.Complemento,
            farmacias.Cidade, farmacias.UF, farmacias.Tel1, farmacias.Tel2, farmacias.Celular,
            farmacias.Site, farmacias.Skype, farmacias.Msn, farmacias.Ativo, farmacias.idRede,farmacias.CEP
            FROM farmacias WHERE (farmacias.Cnpj = @Cnpj)");

            string scnpj = strCNPJ.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = strSQL.ToString();
            cmm.Parameters.Clear();
            cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = scnpj;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Loja");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["Loja"].Rows[0]["Id"] != DBNull.Value)
                {
                    clsLoja.Id = Convert.ToInt16(ds.Tables["Loja"].Rows[0]["Id"].ToString());
                    clsLoja.Proprietario = ds.Tables["Loja"].Rows[0]["Proprietario"].ToString() == "" ? "" : ds.Tables["Loja"].Rows[0]["Proprietario"].ToString();
                    clsLoja.Gerente = ds.Tables["Loja"].Rows[0]["Gerente"].ToString() == "" ? "" : ds.Tables["Loja"].Rows[0]["Gerente"].ToString();
                    clsLoja.Email = ds.Tables["Loja"].Rows[0]["Email"].ToString();
                    clsLoja.Email2 = ds.Tables["Loja"].Rows[0]["Email2"].ToString();
                    clsLoja.NomeFantasia = ds.Tables["Loja"].Rows[0]["NomeFantasia"].ToString();
                    clsLoja.Razao = ds.Tables["Loja"].Rows[0]["RazaoSocial"].ToString();
                    clsLoja.Cnpj = ds.Tables["Loja"].Rows[0]["Cnpj"].ToString();
                    clsLoja.Endereco = ds.Tables["Loja"].Rows[0]["Endereco"].ToString();
                    clsLoja.EndNumero = Convert.ToInt32(ds.Tables["Loja"].Rows[0]["Numero"].ToString());
                    clsLoja.Bairro = ds.Tables["Loja"].Rows[0]["Bairro"].ToString();
                    clsLoja.Complemento = ds.Tables["Loja"].Rows[0]["Complemento"].ToString();
                    clsLoja.Cidade = ds.Tables["Loja"].Rows[0]["Cidade"].ToString();
                    clsLoja.Uf = ds.Tables["Loja"].Rows[0]["UF"].ToString();
                    clsLoja.Fone = ds.Tables["Loja"].Rows[0]["Tel1"].ToString();
                    clsLoja.Fone2 = ds.Tables["Loja"].Rows[0]["Tel2"].ToString();
                    clsLoja.Celular = ds.Tables["Loja"].Rows[0]["Celular"].ToString();
                    clsLoja.Site = ds.Tables["Loja"].Rows[0]["Site"].ToString();
                    clsLoja.Skype = ds.Tables["Loja"].Rows[0]["Skype"].ToString();
                    clsLoja.Msn = ds.Tables["Loja"].Rows[0]["Msn"].ToString();
                    clsLoja.Ativo = (ds.Tables["Loja"].Rows[0]["Ativo"].ToString() == "1" ? true : false);
                    clsLoja.idRede = Convert.ToInt32(ds.Tables["Loja"].Rows[0]["idRede"].ToString());
                    clsLoja.CEP = ds.Tables["Loja"].Rows[0]["CEP"].ToString();
                }
            }

            return clsLoja;
        }
        
        public DataSet GetUf(string scn)
        {
            DataSet ds = new DataSet();

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            cmm.CommandText = "SELECT id,UF FROM uf";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "UF");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public static Rede GetRedeByCNPJ(string strCNPJ)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            Rede r = new Rede();

            NpgsqlCommand cmm = new NpgsqlCommand();
            clsDB clsDB = new clsDB();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,redesfarmaceuticas.UserId FROM  redesfarmaceuticas 
            WHERE (redesfarmaceuticas.CNPJ = @CNPJ)";


            string scnpj = strCNPJ.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.Parameters.Add("@CNPJ", NpgsqlDbType.Varchar).Value = scnpj;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
                {
                    r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                    r.RedeName = ds.Tables[0].Rows[0]["Descricao"].ToString();
                    r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["UserId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["UserId"]);
                }
            }

            return r;
        }

        public DataSet GetFarmacias(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT Id, NomeFantasia FROM farmacias";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public object GetFarmaciasByRedeId(string scn, string redeId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT Id, NomeFantasia FROM farmacias";
            cmm.CommandText += " WHERE idRede = " + redeId;
            cmm.CommandText += " ORDER BY NomeFantasia";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public Rede GetRedesEdit(string scn, string strRedeId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            Rede r = new Rede();

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,redesfarmaceuticas.UserId,redesfarmaceuticas.CNPJ FROM  redesfarmaceuticas WHERE (redesfarmaceuticas.Id = " + strRedeId + ")";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "RedeEd");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
                r.RedeName = ds.Tables[0].Rows[0]["Descricao"].ToString();
                r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["UserId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["UserId"]);
                r.CNPJ = ds.Tables[0].Rows[0]["CNPJ"].ToString();
            }

            return r;
        }

        public Loja GetLojaEdit(string scn, string p)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            Loja clsLoja = new Loja();

            cmm.Connection = cnn;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"SELECT farmacias.Id, farmacias.Proprietario, farmacias.Gerente, farmacias.Email,
            farmacias.Email2, farmacias.NomeFantasia, farmacias.RazaoSocial, farmacias.Cnpj,
            farmacias.Endereco, farmacias.Numero, farmacias.Bairro, farmacias.Complemento,
            farmacias.Cidade, farmacias.UF, farmacias.Tel1, farmacias.Tel2, farmacias.Celular,
            farmacias.Site, farmacias.Skype, farmacias.Msn, farmacias.Ativo, farmacias.idRede,farmacias.CEP
            FROM farmacias WHERE (farmacias.Id = @Id)");

            cmm.CommandText = strSQL.ToString();
            cmm.Parameters.Clear();
            cmm.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = p;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "LojaEd");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                clsLoja.Id = Convert.ToInt16(ds.Tables["LojaEd"].Rows[0]["Id"].ToString());
                clsLoja.Proprietario = ds.Tables["LojaEd"].Rows[0]["Proprietario"].ToString() == "" ? "" : ds.Tables["LojaEd"].Rows[0]["Proprietario"].ToString();
                clsLoja.Gerente = ds.Tables["LojaEd"].Rows[0]["Gerente"].ToString() == "" ? "" : ds.Tables["LojaEd"].Rows[0]["Gerente"].ToString();
                clsLoja.Email = ds.Tables["LojaEd"].Rows[0]["Email"].ToString();
                clsLoja.Email2 = ds.Tables["LojaEd"].Rows[0]["Email2"].ToString();
                clsLoja.NomeFantasia = ds.Tables["LojaEd"].Rows[0]["NomeFantasia"].ToString();
                clsLoja.Razao = ds.Tables["LojaEd"].Rows[0]["RazaoSocial"].ToString();
                clsLoja.Cnpj = ds.Tables["LojaEd"].Rows[0]["Cnpj"].ToString();
                clsLoja.Endereco = ds.Tables["LojaEd"].Rows[0]["Endereco"].ToString();
                clsLoja.EndNumero = Convert.ToInt32(ds.Tables["LojaEd"].Rows[0]["Numero"].ToString());
                clsLoja.Bairro = ds.Tables["LojaEd"].Rows[0]["Bairro"].ToString();
                clsLoja.Complemento = ds.Tables["LojaEd"].Rows[0]["Complemento"].ToString();
                clsLoja.Cidade = ds.Tables["LojaEd"].Rows[0]["Cidade"].ToString();
                clsLoja.Uf = ds.Tables["LojaEd"].Rows[0]["UF"].ToString();
                clsLoja.Fone = ds.Tables["LojaEd"].Rows[0]["Tel1"].ToString();
                clsLoja.Fone2 = ds.Tables["LojaEd"].Rows[0]["Tel2"].ToString();
                clsLoja.Celular = ds.Tables["LojaEd"].Rows[0]["Celular"].ToString();
                clsLoja.Site = ds.Tables["LojaEd"].Rows[0]["Site"].ToString();
                clsLoja.Skype = ds.Tables["LojaEd"].Rows[0]["Skype"].ToString();
                clsLoja.Msn = ds.Tables["LojaEd"].Rows[0]["Msn"].ToString();
                clsLoja.Ativo = (ds.Tables["LojaEd"].Rows[0]["Ativo"].ToString() == "1" ? true : false);
                clsLoja.idRede = Convert.ToInt32(ds.Tables["LojaEd"].Rows[0]["idRede"].ToString());
                clsLoja.CEP = ds.Tables["LojaEd"].Rows[0]["CEP"].ToString();
            }

            return clsLoja;
        }

        public DataSet GetLojaByUserId(int intUserId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT Cnpj, NomeFantasia, GerenteId, ProprietarioID 
            FROM farmacias 
            INNER JOIN usuarios_vinculos ON farmacias.id = usuarios_vinculos.LinkId
            WHERE usuarios_vinculos.usuarioid = @usuarioid
            ORDER BY NomeFantasia";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@usuarioid", NpgsqlDbType.Integer).Value = intUserId;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public DataSet GetLojaByRedeId(int intRedeId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT Cnpj, NomeFantasia, GerenteId, ProprietarioID,idRede FROM farmacias
            WHERE farmacias.idRede = @idRede ORDER BY NomeFantasia";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public static DataSet GetGrupos(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection msc = new NpgsqlConnection(scn);
            clsDB clsDB = new clsDB();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = msc;
            cmm.CommandText = "SELECT DISTINCT Grupo FROM produtos_base WHERE Grupo NOT IN ('Fitoterápicos', 'DERMOCOSMETICOS') ORDER BY Grupo";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Grupos");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public static DataSet GetSubCategorias(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection msc = new NpgsqlConnection(scn);
            clsDB clsDB = new clsDB();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = msc;
            cmm.CommandText = "SELECT DISTINCT Sub_Consultoria FROM produtos_base WHERE Sub_Consultoria <> '' ORDER BY Sub_Consultoria";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Sub_Consultoria");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        // Utilizado pelo modelo1
        public List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, string strCnpj, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT 
                    farmacias.RazaoSocial AS Razao_Social,
                    farmacias.nomefantasia,
                    consolidado.CNPJ,
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
                    consolidado.Quantidade AS ""Soma De Quantidade"",
                    consolidado.Valor_Bruto AS ""Soma De Valor bruto"",
                    consolidado.Valor_Liquido AS ""Soma De Valor liquido"",
                    consolidado.Valor_Desconto AS ""Soma De Valor desconto""
                    FROM
                    consolidado
                    INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

            if (clsUser.TipoId.Equals(1)) SQL.Append(" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");
            else SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");

            SQL.Append(@" WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
                    AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                    (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

            String ini, fim;

            ini = strInicio.Replace('/', ' ');
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

                SQL.Append(" ORDER BY usuarios_vinculos.UsuarioId,consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
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
                ds = clsDB.QueryDS(ref cmm, ref ds, "CrossR1");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0 && ds.Tables["CrossR1"].Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["CrossR1"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["CrossR1"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Periodo = String.Format("{0} à {1}", strInicio, strFim);
                            or.Razao = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["CrossR1"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["CrossR1"].Rows[i]["Sub Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["CrossR1"].Rows[i]["Mes"];
                            or.Grupo = ds.Tables["CrossR1"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor desconto"].ToString());
                            or.Ano = Convert.ToInt32(ds.Tables["CrossR1"].Rows[i]["Ano"].ToString());
                            or.NomeFantasia = ds.Tables["CrossR1"].Rows[i]["nomefantasia"].ToString();
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        // Utilizado pelo modelo1
        public List<clsRelat1> GetCross(UsersTO clsUser, string strCnpj, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            string strMF = DateTime.Now.Month.ToString();
            string strMI = DateTime.Now.AddMonths(-7).Month.ToString();

            string strAF = DateTime.Now.Year.ToString();
            string strAI = DateTime.Now.AddMonths(-7).Year.ToString();

            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT farmacias.RazaoSocial AS Razao_Social,farmacias.nomefantasia, consolidado.CNPJ,
                        consolidado.Mes, consolidado.Ano, consolidado.Sub_Consultoria, consolidado.Grupo,
                        consolidado.Quantidade AS ""Soma De Quantidade"",
                        consolidado.Valor_Bruto AS ""Soma De Valor bruto"",
                        consolidado.Valor_Liquido AS ""Soma De Valor liquido"",
                        consolidado.Valor_Desconto AS ""Soma De Valor desconto"" FROM consolidado
                        INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");

            if(clsUser.TipoId.Equals(1)) SQL.Append(" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");
            else SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");

            SQL.Append(String.Format(@" WHERE upper(consolidado.Grupo) IN ('GENÉRICOS' , 'ALTERNATIVOS' , 'PROPAGADOS') 
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

                SQL.Append(" ORDER BY usuarios_vinculos.UsuarioId,consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }
            else
            {
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
                ds = clsDB.QueryDS(ref cmm, ref ds, "CrossR1");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["CrossR1"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["CrossR1"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["CrossR1"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["CrossR1"].Rows[i]["Sub_Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["CrossR1"].Rows[i]["Mes"];
                            or.Ano = (int)ds.Tables["CrossR1"].Rows[i]["Ano"];
                            or.Grupo = ds.Tables["CrossR1"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor desconto"].ToString());
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }
                            or.NomeFantasia = ds.Tables["CrossR1"].Rows[i]["nomefantasia"].ToString();
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

        // Utilizado pelo modelo1
        public List<clsRelat1> GetCross(UsersTO clsUser, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            string SQL = @"SELECT redesfarmaceuticas.descricao AS Razao_Social,redesfarmaceuticas.CNPJ,consolidado.Mes,consolidado.Ano,
                    consolidado.Sub_Consultoria,consolidado.Grupo,SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
                    SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"", SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
                    SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto"" FROM consolidado
                    INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ";

            if(clsUser.TipoId.Equals(1)) SQL += " INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId";
            else SQL += " LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId";

            SQL += @" LEFT JOIN redesfarmaceuticas ON farmacias.idRede = redesfarmaceuticas.id
                    WHERE upper(consolidado.Grupo) IN ('GENÉRICOS' , 'ALTERNATIVOS' , 'PROPAGADOS') 
                    AND farmacias.idRede = @idRede
                    AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                    (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                    GROUP BY redesfarmaceuticas.descricao,redesfarmaceuticas.CNPJ,consolidado.Mes,consolidado.Ano,
                    consolidado.Sub_Consultoria,consolidado.Grupo
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
                ds = clsDB.QueryDS(ref cmm, ref ds, "CrossR1");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["CrossR1"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["CrossR1"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Razao = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.NomeFantasia = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.Periodo = String.Format("{0} à {1}", strMI + "/" + strAI, strMF + "/" + strAF);
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["CrossR1"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["CrossR1"].Rows[i]["Sub_Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["CrossR1"].Rows[i]["Mes"];
                            or.Grupo = ds.Tables["CrossR1"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor desconto"].ToString());
                            or.Ano = Convert.ToInt32(ds.Tables["CrossR1"].Rows[i]["Ano"].ToString());
                            
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        // Utilizado pelo modelo1
        public List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, int intRedeId)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT redesfarmaceuticas.descricao AS Razao_Social,redesfarmaceuticas.CNPJ,consolidado.Mes,consolidado.Ano,
                    consolidado.Sub_Consultoria,consolidado.Grupo,SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
                    SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"", SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
                    SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto"" FROM consolidado
                    INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ");
            if (clsUser.TipoId.Equals(1)) SQL.Append(" LEFT JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");
            else SQL.Append(" INNER JOIN usuarios_vinculos ON farmacias.Id = usuarios_vinculos.LinkId OR farmacias.idRede = usuarios_vinculos.LinkId");

            SQL.Append(@" LEFT JOIN redesfarmaceuticas ON farmacias.idRede = redesfarmaceuticas.id
                    WHERE upper(consolidado.Grupo) IN ('GENÉRICOS' , 'ALTERNATIVOS' , 'PROPAGADOS') 
                    AND farmacias.idRede = @idRede
                    AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                    (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))
                    GROUP BY redesfarmaceuticas.descricao,redesfarmaceuticas.CNPJ,consolidado.Mes,consolidado.Ano,
                    consolidado.Sub_Consultoria,consolidado.Grupo
                    ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");

            String ini, fim;

            ini = strInicio.Replace('/', ' ');
            fim = strFim.Replace('/', ' ');

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@idRede ", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.CommandTimeout = 9999;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "CrossR1");
            }
            clsDB.closeConnection(cmm);

            try
            {
                if (ds.Tables.Count > 0)
                {
                    if (!String.IsNullOrEmpty(ds.Tables["CrossR1"].Rows[0][0].ToString()))
                        for (int i = 0; i < ds.Tables["CrossR1"].Rows.Count; i++)
                        {
                            clsRelat1 or = new clsRelat1();

                            or.Periodo = String.Format("{0} à {1}", strInicio, strFim);
                            or.Razao = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.NomeFantasia = ds.Tables["CrossR1"].Rows[i]["Razao_Social"].ToString();
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["CrossR1"].Rows[i]["Cnpj"].ToString());
                            or.SubConsultoria = ds.Tables["CrossR1"].Rows[i]["Sub_Consultoria"].ToString();
                            or.Mes = (int)ds.Tables["CrossR1"].Rows[i]["Mes"];
                            or.Grupo = ds.Tables["CrossR1"].Rows[i]["Grupo"].ToString();
                            or.SomaDeQuantidade = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Quantidade"].ToString());
                            or.SomaDeValorBruto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor bruto"].ToString());
                            or.SomaDeValorLiquido = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor liquido"].ToString());
                            or.SomaDeValorDesconto = Convert.ToDecimal(ds.Tables["CrossR1"].Rows[i]["Soma De Valor desconto"].ToString());
                            or.Ano = Convert.ToInt32(ds.Tables["CrossR1"].Rows[i]["Ano"].ToString());
                            if (or.SomaDeValorDesconto > 0)
                            {
                                if (or.SomaDeValorBruto > 0) { or.PercentualDesconto = Convert.ToDecimal(((or.SomaDeValorDesconto / or.SomaDeValorBruto) * 100).ToString("N2")); }
                            }
                            else { or.PercentualDesconto = 0; }

                            lr.Add(or);
                        }
                }
            }
            finally
            {

            }

            return lr;
        }

        public DataSet GetUsers(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT users.UserId, users.UserName"
                + " FROM  users INNER JOIN"
                + " memberships ON users.UserId = memberships.UserId"
                + " WHERE memberships.Inactive = 0";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Users");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public UsersTO GetUserEdit(string scn, string p)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            UsersTO clsUser = new UsersTO();

            cmm.Connection = cnn;
            cmm.CommandText = "SELECT users.UserId, users.UserName, memberships.Email,"
                + " memberships.Inactive, memberships.ExpirationDate, memberships.Access, "
                + " memberships.Name, usuarios_farmacias.FarmaciaId, memberships.`Password`"
                + " FROM  users INNER JOIN"
                + " memberships ON users.UserId = memberships.UserId LEFT OUTER JOIN"
                + " usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId"
                + " WHERE (memberships.Inactive = 0) AND (users.UserId = " + p + ")";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "UserEd");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                clsUser.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["UserId"].ToString());
                clsUser.Inactive = (bool)(ds.Tables[0].Rows[0]["Inactive"].ToString() == "0" ? false : true);
                clsUser.FarmaciaId = Convert.ToInt16(ds.Tables[0].Rows[0]["FarmaciaId"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["FarmaciaId"]);
                clsUser.Name = o.denc(ds.Tables[0].Rows[0]["Name"].ToString());
                clsUser.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                clsUser.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                clsUser.ExpirationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"].ToString());
                clsUser.Access = o.denc(ds.Tables[0].Rows[0]["Access"].ToString());
                clsUser.Password = o.denc(ds.Tables[0].Rows[0]["Password"].ToString());
            }

            return clsUser;
        }

        #endregion

        #region .:Persistence:.
        public string AddUser(UsersTO clsUser, string scn)
        {
            string msg = "";

            try
            {
                string pass = o.encr(clsUser.Password);
                string nme = o.encr(clsUser.Name.ToUpper());

                NpgsqlConnection cnn = new NpgsqlConnection(scn);
                cmm.Connection = cnn;

                if (clsDB.openConnection(cmm))
                {
                    cmm.CommandText = "SELECT UserId FROM  users WHERE UserName = @UserName";
                    cmm.Parameters.Clear();
                    cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;

                    int id = 0;
                    if (clsDB.Query(id, ref cmm) == DBNull.Value)
                    {
                        cmm.CommandText = @"INSERT INTO users (UserName, LastActivityDate, TipoId) VALUES (@UserName, @LastActivityDate, @TipoId)";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;
                        cmm.Parameters.Add("@LastActivityDate", NpgsqlDbType.Date).Value = DateTime.Now.Date;
                        cmm.Parameters.Add("@TipoId",NpgsqlDbType.Integer).Value = clsUser.TipoId;

                        clsDB.Execute(ref cmm);

                        cmm.CommandText = "SELECT UserId FROM  users WHERE UserName = @UserName";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;

                        id = (int)clsDB.Query(id, ref cmm);

                        string cDate = clsUser.CreateDate.Year + "-" + clsUser.CreateDate.Month + "-" + clsUser.CreateDate.Day + " 00:00:00";
                        string eDate = clsUser.ExpirationDate.Year + "-" + clsUser.ExpirationDate.Month + "-" + clsUser.ExpirationDate.Day + " 00:00:00";

                        cmm.CommandText = "INSERT INTO memberships"
                            + " (UserId, Password, Email, Inactive, CreateDate, ExpirationDate, Name)"
                            + " VALUES (" + id + ", '" + pass + "', '" + clsUser.Email + "', "
                            + (clsUser.Inactive == false ? 0 : 1) + ", '" + cDate + "', '" + eDate + "', '"
                            + nme + "')";

                        clsDB.Execute(ref cmm);
                    }
                    else { msg = "Usuário já cadastrado."; }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }
     
        public string AddRede(string scn, Rede rede)
        {
            string msg = "";
            cmm.CommandText = "INSERT INTO redesfarmaceuticas (Descricao,CNPJ)"
                + " VALUES ('" + rede.RedeName + "','"+ rede.CNPJ +"')";

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            try
            {
                if (clsDB.openConnection(cmm))
                {
                    clsDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            clsDB.closeConnection(cmm);

            return msg;
        }

        public string UpdateRede(string scn, Rede rede)
        {
            string msg = "";
            cmm.CommandText = "UPDATE redesfarmaceuticas SET Descricao = '" + rede.RedeName
                + "', CNPJ = '"+ rede.CNPJ +"' WHERE Id = " + rede.RedeId;

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            try
            {
                if (clsDB.openConnection(cmm))
                {
                    clsDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            clsDB.closeConnection(cmm);

            return msg;
        }

        public string AddLoja(string scn, Loja ol)
        {
            string msg = "";

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            string scnpj = ol.Cnpj.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = @"INSERT INTO farmacias (Proprietario, Gerente, Email, Email2, NomeFantasia, RazaoSocial, Cnpj, Endereco, Numero,
            Bairro, Complemento, Cidade, UF, Tel1, Tel2, Celular, Site, Skype, Msn, Ativo, idRede, CEP)"
                + " VALUES ('" + ol.Proprietario + "', '" + ol.Gerente + "', '" + ol.Email + "', '" + ol.Email2 + "', '"
                + ol.NomeFantasia + "', '" + ol.Razao + "', '" + scnpj + "', '" + ol.Endereco + "', '" + ol.EndNumero
                + "', '" + ol.Bairro + "', '" + ol.Complemento + "', '" + ol.Cidade + "', '" + ol.Uf + "', '" + ol.Fone
                + "', '" + ol.Fone2 + "', '" + ol.Celular + "', '" + ol.Site + "', '" + ol.Skype + "', "
                + " '" + ol.Msn + "', " + (ol.Ativo == true ? 1 : 0) + ", " + ol.idRede + ",'" + ol.CEP + "')";

            try
            {
                if (clsDB.openConnection(cmm))
                {
                    clsDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            clsDB.closeConnection(cmm);

            return msg;
        }

        public string UpdateLoja(string scn, Loja clsLoja)
        {
            string msg = "";

            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            string scnpj = clsLoja.Cnpj.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = "UPDATE farmacias SET Proprietario = '" + clsLoja.Proprietario + "', Gerente = '" + clsLoja.Gerente
                + "', Email = '" + clsLoja.Email + "', Email2 = '" + clsLoja.Email2 + "', NomeFantasia = '" + clsLoja.NomeFantasia
                + "', RazaoSocial = '" + clsLoja.Razao + "', Cnpj = '" + scnpj + "', Endereco = '" + clsLoja.Endereco + "', Numero = '"
                + clsLoja.EndNumero + "', Bairro = '" + clsLoja.Bairro + "', Complemento = '" + clsLoja.Complemento + "', Cidade = '"
                + clsLoja.Cidade + "', UF = '" + clsLoja.Uf + "', Tel1 = '" + clsLoja.Fone + "', Tel2 = '" + clsLoja.Fone2 + "', Celular = '"
                + clsLoja.Celular + "', Site = '" + clsLoja.Site + "', Skype = '" + clsLoja.Skype + "', " + " Msn = " + " '" + clsLoja.Msn
                + "', Ativo = " + (clsLoja.Ativo == true ? 1 : 0) + ", idRede = " + clsLoja.idRede + ",CEP = '" + clsLoja.CEP + "' WHERE Id = "
                + clsLoja.Id;

            try
            {
                if (clsDB.openConnection(cmm))
                {
                    clsDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            clsDB.closeConnection(cmm);

            return msg;
        }

        public string AddXml(string scn, XmlDocument xd, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            DataSet ds = new DataSet();

            ds.ReadXml(new XmlNodeReader(xd));
            string msg = "";

            if (ds.Tables.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        cmm.CommandText = "SELECT arquivosenviados.id FROM arquivosenviados"
                            + " WHERE arquivosenviados.cnpj = @cnpj AND arquivosenviados.mes = @mes"
                            + " AND arquivosenviados.ano = @ano";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[0]["cnpj"].ToString();
                        cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[0]["mes"];
                        cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[0]["ano"];

                        int id = 0;
                        if (clsDB.Query(id, ref cmm) == DBNull.Value)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cmm.CommandText = "INSERT INTO base_cliente_espera (Cnpj, Mes, Ano, Barras, Descricao, Fabricante, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto)"
                                    + " VALUES ( @Cnpj, @Mes, @Ano, @Barras, @Descricao, @Fabricante, @Quantidade, @Valor_Bruto, @Valor_Liquido, @Valor_Desconto)";
                                cmm.Parameters.Clear();
                                cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["cnpj"].ToString();
                                cmm.Parameters.Add("@Mes", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["mes"];
                                cmm.Parameters.Add("@Ano", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["ano"];
                                cmm.Parameters.Add("@Barras", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["ean"].ToString();
                                cmm.Parameters.Add("@Descricao", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["nprod"].ToString();
                                cmm.Parameters.Add("@Fabricante", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["fab"].ToString();
                                cmm.Parameters.Add("@Quantidade", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["quant"];
                                cmm.Parameters.Add("@Valor_Bruto", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["vbruto"];
                                cmm.Parameters.Add("@Valor_Liquido", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["vliquido"];
                                cmm.Parameters.Add("@Valor_Desconto", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["desconto"];

                                clsDB.Execute(ref cmm);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                cmm.CommandText = "UPDATE base_cliente_espera SET Barras = @Barras, Descricao = @Descricao,"
                                    + " Fabricante = @Fabricante, Quantidade = @Quantidade, Valor_Bruto = "
                                    + "@Valor_Bruto, Valor_Liquido = @Valor_Liquido, Valor_Desconto = @Valor_Desconto WHERE Cnpj = @Cnpj AND Mes = @Mes AND Ano = @Ano";
                                cmm.Parameters.Clear();
                                cmm.Parameters.Add("@Barras", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["ean"].ToString();
                                cmm.Parameters.Add("@Descricao", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["nprod"].ToString();
                                cmm.Parameters.Add("@Fabricante", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["fab"].ToString();
                                cmm.Parameters.Add("@Quantidade", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["quant"];
                                cmm.Parameters.Add("@Valor_Bruto", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["vbruto"];
                                cmm.Parameters.Add("@Valor_Liquido", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["vliquido"];
                                cmm.Parameters.Add("@Valor_Desconto", NpgsqlDbType.Real).Value = ds.Tables[0].Rows[i]["desconto"];
                                cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[i]["cnpj"].ToString();
                                cmm.Parameters.Add("@Mes", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["mes"];
                                cmm.Parameters.Add("@Ano", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[i]["ano"];

                                clsDB.Execute(ref cmm);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                clsDB.closeConnection(cmm);

                AddXmlData(scn, ds, clsUser);

            }
            else { msg = "Erro ao converter o xml."; }

            return msg;
        }

        private void AddXmlData(string scn, DataSet ds, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            if (ds.Tables.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                            + " VALUES ( @UserId, @cnpj, 'XML', @mes, @ano)";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = clsUser.UserId;
                        cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = ds.Tables[0].Rows[0]["cnpj"].ToString();
                        cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[0]["mes"];
                        cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = ds.Tables[0].Rows[0]["ano"];

                        clsDB.Execute(ref cmm);
                    }
                }
                catch (Exception)
                {
                }

                clsDB.closeConnection(cmm);
            }
        }

        public string AddDbf(string scn, DataTable dt)
        {
            string msg = "";
            NpgsqlConnection msc = new NpgsqlConnection(scn);
            DataSet ds = new DataSet();

            cmm.Connection = msc;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        cmm.CommandText = "SELECT produtos_base.CclsDBarra,produtos_base.CodProd,produtos_base.NomeProd,produtos_base.Apresenta,produtos_base.CodLab,produtos_base.NomeLab,produtos_base.CodPat,produtos_base.NomePat,produtos_base.Grupo,produtos_base.Sub_Consultoria,produtos_base.Sub_Divisao,produtos_base.NCM,produtos_base.`NomePat-NCM`,produtos_base.Descricao_NCM,produtos_base.Lista,produtos_base.ALiq,produtos_base.St FROM produtos_base";
                        cmm.CommandText += " WHERE CclsDBarra IN (";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i > 0) { cmm.CommandText += ","; }
                            cmm.CommandText += dt.Rows[i][16];
                        }
                        cmm.CommandText += ")";

                        ds = clsDB.QueryDS(ref cmm, ref ds, "produtos_base");

                        if (ds.Tables.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (ds.Tables[0].Select("CclsDBarra = '" + dt.Rows[i][16] + "'").Length == 0)
                                {
                                    cmm.CommandText = "INSERT INTO produtos_base (CclsDBarra, CodProd, NomeProd, Apresenta, CodLab,"
                                        + " Grupo, `NomePat-NCM`, ALiq, St)"
                                        + " VALUES ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1]
                                        + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] + "',"
                                        + " '" + dt.Rows[i][2] + "', '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] + ", '" + dt.Rows[i][59] + "')";

                                    clsDB.Execute(ref cmm);
                                }
                                else
                                {
                                    cmm.CommandText = "UPDATE produtos_base SET CodProd = " + dt.Rows[i][0]
                                        + ", NomeProd = '" + dt.Rows[i][1] + "', Apresenta = '"
                                        + dt.Rows[i][35] + "', CodLab = '" + dt.Rows[i][11] + "', Grupo = '"
                                        + dt.Rows[i][2] + "', `NomePat-NCM` = '" + dt.Rows[i][41]
                                        + "', ALiq = " + dt.Rows[i][54] + ", St = '" + dt.Rows[i][59] + "'"
                                        + " WHERE CclsDBarra = " + dt.Rows[i][16];

                                    clsDB.Execute(ref cmm);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                cmm.CommandText = "INSERT INTO produtos_base (CclsDBarra, CodProd, NomeProd, Apresenta, CodLab,"
                                + " Grupo, `NomePat-NCM`, ALiq, St)"
                                + " VALUES ('" + dt.Rows[i][16] + "', " + dt.Rows[i][0] + ", '" + dt.Rows[i][1]
                                + "', '" + dt.Rows[i][35] + "', '" + dt.Rows[i][11] + "',"
                                + " '" + dt.Rows[i][2] + "', '" + dt.Rows[i][41] + "', " + dt.Rows[i][54] + ", '" + dt.Rows[i][59] + "')";

                                clsDB.Execute(ref cmm);
                            }
                        }
                    }
                    clsDB.closeConnection(cmm);
                }
                catch (Exception)
                {

                }
            }
            else { msg = "Erro na gravação dos dados"; }
            return msg;
        }

        public string AddTxt(string scn, DataTable dt, UsersTO clsUser, List<int> lstMeses, List<string> lstCnpj)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            string msg = "";
            int itest = 0;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        cmm.CommandText = "DELETE FROM base_cliente_espera";
                        string strCnpj = string.Empty;
                        int k = 0;
                        lstCnpj.ForEach(delegate(string _cnpj)
                        {
                            if (k == 0) { strCnpj = "'" + _cnpj + "'"; k++; } else { strCnpj += ",'" + _cnpj + "'"; k++; }
                        });
                        cmm.CommandText += " WHERE Cnpj IN (" + strCnpj + ")";
                        string strMeses = "";
                        int j = 0;
                        lstMeses.ForEach(delegate(int _mes)
                        {
                            if (j == 0) { strMeses = _mes.ToString(); j++; } else { strMeses += ", " + _mes.ToString(); j++; }
                        });
                        cmm.CommandText += " AND Mes IN (" + strMeses + ") AND Ano = " + dt.Rows[0]["ano"];

                        clsDB.Execute(ref cmm);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string svb = "", svl = "", svd = "";

                            svb = dt.Rows[i][10].ToString().Replace(".", "");
                            svb = svb.Replace(",", ".");

                            svl = dt.Rows[i][11].ToString().Replace(".", "");
                            svl = svl.Replace(",", ".");

                            svd = dt.Rows[i][12].ToString().Replace(".", "");
                            svd = svd.Replace(",", ".");

                            cmm.CommandText = string.Empty;
                            cmm.CommandText = "INSERT INTO base_cliente_espera (Razao_Social, Cnpj, Mes, Ano, Barras, Descricao,"
                                + " Fabricante, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto)"
                                + " VALUES ('" + dt.Rows[i][0].ToString().Replace("'", "''") + "', '"
                                + dt.Rows[i][1].ToString() + "', " + dt.Rows[i][2] + ", " + dt.Rows[i][3]
                                + ", '" + dt.Rows[i][4].ToString() + "', '"
                                + dt.Rows[i][5].ToString().Replace("'", "''") + "', '"
                                + dt.Rows[i][6].ToString().Replace("'", "''") + "', " + dt.Rows[i][9] + ", "
                                + svb + ", " + svl + ", " + svd + ")";

                            itest = i;

                            clsDB.Execute(ref cmm);
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }

                clsDB.closeConnection(cmm);

                this.AddTxtData(scn, dt, clsUser);

            }
            else { msg = "Erro ao converter o txt."; }

            return msg;
        }

        private void AddTxtData(string scn, DataTable dt, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(scn);
            cmm.Connection = cnn;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                                + " VALUES (@UserId, @cnpj, 'TXT', @mes, @ano)";
                            cmm.Parameters.Clear();
                            cmm.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = clsUser.UserId;
                            cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = dt.Rows[i]["cnpj"].ToString();
                            cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = dt.Rows[i][2];
                            cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = dt.Rows[i]["ano"];

                            clsDB.Execute(ref cmm);
                        }
                    }
                }
                catch (Exception)
                {
                }

                clsDB.closeConnection(cmm);
            }
        }

        public string UpdateUser(UsersTO clsUser, string scn)
        {
            string msg = "";

            try
            {
                string pass = o.encr(clsUser.Password);
                string nme = o.encr(clsUser.Name.ToUpper());

                NpgsqlConnection cnn = new NpgsqlConnection(scn);
                cmm.Connection = cnn;

                if (clsDB.openConnection(cmm))
                {
                    string lDate = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00";
                    StringBuilder stbSQL = new StringBuilder();
                    stbSQL.Append("UPDATE users SET UserName = '" + clsUser.UserName + "', LastActivityDate = '" + lDate + "'");
                    if (clsUser.TipoId > 0) stbSQL.Append(",TipoId = @TipoId");

                    stbSQL.Append(" WHERE UserId = " + clsUser.UserId);
                    cmm.CommandText = stbSQL.ToString();
                    cmm.Parameters.Add("@TipoId", NpgsqlDbType.Integer).Value = clsUser.TipoId;

                    clsDB.Execute(ref cmm);

                    string eDate = clsUser.ExpirationDate.Year + "-" + clsUser.ExpirationDate.Month + "-" + clsUser.ExpirationDate.Day + " 00:00:00";

                    cmm.CommandText = "UPDATE memberships SET Password = '" + pass + "', Email = '"
                        + clsUser.Email + "', Inactive = " + (clsUser.Inactive == false ? 0 : 1)
                        + ", ExpirationDate = '" + eDate + "', Name = '" + nme
                        + "' WHERE UserId = " + clsUser.UserId;

                    clsDB.Execute(ref cmm);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        #endregion

    }
}
