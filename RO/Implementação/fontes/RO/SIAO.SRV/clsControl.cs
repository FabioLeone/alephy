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

        public static DataSet GetRedes()
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
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
            INNER JOIN usuarios_vinculos ON redesfarmaceuticas.Id = usuarios_vinculos.redeid
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
                Int32.TryParse(clsDB.Query(intRedeId, ref cmm).ToString(), out intRedeId);
            }
            clsDB.closeConnection(cmm);

            return intRedeId;
        }

        internal static Rede GetRedeById(int intId)
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            Rede r = new Rede();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT redesfarmaceuticas.Id,redesfarmaceuticas.Descricao FROM  redesfarmaceuticas 
            WHERE (redesfarmaceuticas.Id = @Id)";

            cmm.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = intId;

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
                }
            }

            return r;
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
            farmacias.Site, farmacias.Skype, farmacias.Ativo, farmacias.idRede,farmacias.CEP
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
                    clsLoja.EndNumero = ds.Tables["Loja"].Rows[0]["Numero"].ToString();
                    clsLoja.Bairro = ds.Tables["Loja"].Rows[0]["Bairro"].ToString();
                    clsLoja.Complemento = ds.Tables["Loja"].Rows[0]["Complemento"].ToString();
                    clsLoja.Cidade = ds.Tables["Loja"].Rows[0]["Cidade"].ToString();
                    clsLoja.Uf = ds.Tables["Loja"].Rows[0]["UF"].ToString();
                    clsLoja.Fone = ds.Tables["Loja"].Rows[0]["Tel1"].ToString();
                    clsLoja.Fone2 = ds.Tables["Loja"].Rows[0]["Tel2"].ToString();
                    clsLoja.Celular = ds.Tables["Loja"].Rows[0]["Celular"].ToString();
                    clsLoja.Site = ds.Tables["Loja"].Rows[0]["Site"].ToString();
                    clsLoja.Skype = ds.Tables["Loja"].Rows[0]["Skype"].ToString();
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

        public DataSet GetUf(int intId)
        {
            DataSet ds = new DataSet();

            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            cmm.Connection = cnn;

            cmm.CommandText = "SELECT distinct uf.id,uf.UF FROM uf inner join farmacias f on uf.id = f.uf where f.idrede = @Id;";

            cmm.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = intId;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "UF");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        public DataSet GetUfByUser(int intId)
        {
            DataSet ds = new DataSet();

            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            cmm.Connection = cnn;

            cmm.CommandText = "SELECT distinct uf.id,uf.UF FROM uf inner join farmacias f on uf.id = f.uf inner join usuarios_vinculos u on f.id = u.farmaciaid where u.usuarioid = @Id;";

            cmm.Parameters.Add("@Id", NpgsqlDbType.Integer).Value = intId;

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
            farmacias.Site, farmacias.Skype, farmacias.Ativo, farmacias.idRede,farmacias.CEP
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
                clsLoja.EndNumero = ds.Tables["LojaEd"].Rows[0]["Numero"].ToString();
                clsLoja.Bairro = ds.Tables["LojaEd"].Rows[0]["Bairro"].ToString();
                clsLoja.Complemento = ds.Tables["LojaEd"].Rows[0]["Complemento"].ToString();
                clsLoja.Cidade = ds.Tables["LojaEd"].Rows[0]["Cidade"].ToString();
                clsLoja.Uf = ds.Tables["LojaEd"].Rows[0]["UF"].ToString();
                clsLoja.Fone = ds.Tables["LojaEd"].Rows[0]["Tel1"].ToString();
                clsLoja.Fone2 = ds.Tables["LojaEd"].Rows[0]["Tel2"].ToString();
                clsLoja.Celular = ds.Tables["LojaEd"].Rows[0]["Celular"].ToString();
                clsLoja.Site = ds.Tables["LojaEd"].Rows[0]["Site"].ToString();
                clsLoja.Skype = ds.Tables["LojaEd"].Rows[0]["Skype"].ToString();
                clsLoja.Ativo = (ds.Tables["LojaEd"].Rows[0]["Ativo"].ToString() == "1" ? true : false);
                clsLoja.idRede = Convert.ToInt32(ds.Tables["LojaEd"].Rows[0]["idRede"].ToString());
                clsLoja.CEP = ds.Tables["LojaEd"].Rows[0]["CEP"].ToString();
            }

            return clsLoja;
        }

        public static DataSet GetLojaByUserId(int intUserId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT DISTINCT id, Cnpj, NomeFantasia
            FROM (
            SELECT f.id, Cnpj, NomeFantasia
                        FROM farmacias f
                        INNER JOIN usuarios_vinculos ON f.id = usuarios_vinculos.farmaciaid
                        WHERE usuarios_vinculos.usuarioid = @usuarioid
            UNION
            SELECT f.id, Cnpj, NomeFantasia
                        FROM farmacias f
                        INNER JOIN usuarios_vinculos ON f.idrede = usuarios_vinculos.redeid
                        WHERE usuarios_vinculos.usuarioid = @usuarioid
            AND idrede > 0
                  ) as v
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

        public static DataSet GetLojaByRedeId(int intRedeId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT id, Cnpj, NomeFantasia, idRede FROM farmacias
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

        internal static DataSet GetLojaByRedeIdAndCity(int intRedeId, string city)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT id, Cnpj, NomeFantasia, idRede FROM farmacias
            WHERE farmacias.idRede = @idRede AND cidade = @city ORDER BY NomeFantasia";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@city", NpgsqlDbType.Varchar).Value = city;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        internal static object GetLojaByRedeIdAndUf(int intRedeId, int ufId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT id, Cnpj, NomeFantasia, idRede FROM farmacias
            WHERE farmacias.idRede = @idRede AND uf = @uf ORDER BY NomeFantasia";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = ufId;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        internal static Loja GetLojaById(int intLoja)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();
            Loja objLoja = new Loja();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT id,Cnpj, NomeFantasia, idRede FROM farmacias
            WHERE farmacias.id = @id";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@id", NpgsqlDbType.Integer).Value = intLoja;

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Farmacias");
            }
            clsDB.closeConnection(cmm);

            if (ds.Tables["Farmacias"].Rows.Count > 0)
            {
                objLoja.Cnpj = ds.Tables["Farmacias"].Rows[0]["Cnpj"].ToString();
                objLoja.Id = Convert.ToInt32(ds.Tables["Farmacias"].Rows[0]["id"].ToString());
                objLoja.idRede = Convert.ToInt32(ds.Tables["Farmacias"].Rows[0]["idRede"].ToString());
                objLoja.NomeFantasia = ds.Tables["Farmacias"].Rows[0]["NomeFantasia"].ToString();
            }

            return objLoja;
        }

        public static DataSet GetGrupos(string scn)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection msc = new NpgsqlConnection(scn);
            clsDB clsDB = new clsDB();
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = msc;
            cmm.CommandText = @"SELECT id, CASE WHEN (apelido is null or apelido = '') THEN nome ELSE apelido END as Grupo
            FROM produtos_grupos WHERE nome NOT IN ('Fitoterápicos', 'DERMOCOSMETICOS') ORDER BY nome";

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
            cmm.CommandText = "SELECT id, nome FROM produtos_subgrupos ORDER BY nome";

            if (clsDB.openConnection(cmm))
            {
                ds = clsDB.QueryDS(ref cmm, ref ds, "Sub_Consultoria");
            }
            clsDB.closeConnection(cmm);

            return ds;
        }

        // Utilizado pelo modelo1 b
        internal List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, string strCnpj, int intRedeId, Boolean blnSum, string strCity, int intUf)
        {
            List<clsRelat1> lr = new List<clsRelat1>();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            DataSet ds = new DataSet();

            cmm.Connection = cnn;
            StringBuilder SQL = new StringBuilder();
            SQL.Append(@"SELECT");
 
            if(String.IsNullOrEmpty(strCnpj) && blnSum)
                SQL.Append(" r.descricao AS Razao_Social, r.descricao AS nomefantasia, r.cnpj,");
            else
                SQL.Append(" farmacias.RazaoSocial AS Razao_Social, farmacias.nomefantasia, consolidado.CNPJ,");

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
                    consolidado.Grupo,");

            if (String.IsNullOrEmpty(strCnpj) && blnSum)
                SQL.Append(@" SUM(consolidado.Quantidade) AS ""Soma De Quantidade"",
	                SUM(consolidado.Valor_Bruto) AS ""Soma De Valor bruto"",
	                SUM(consolidado.Valor_Liquido) AS ""Soma De Valor liquido"",
	                SUM(consolidado.Valor_Desconto) AS ""Soma De Valor desconto""");
            else
                SQL.Append(@" consolidado.Quantidade AS ""Soma De Quantidade"",
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

            if (String.IsNullOrEmpty(strCnpj) && blnSum)
                SQL.Append(" LEFT JOIN redesfarmaceuticas r ON farmacias.idrede = r.id");

            SQL.Append(@" WHERE upper(consolidado.Grupo) in ('PROPAGADOS','ALTERNATIVOS','GENÉRICOS')
                    AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                    (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

            String ini, fim;

            ini = strInicio.Replace('/', ' ');
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

                if(intUf > 0)
                    SQL.Append(" AND farmacias.uf = @uf");

                if (!String.IsNullOrEmpty(strCity))
                    SQL.Append(" AND farmacias.cidade = @city");

                if (String.IsNullOrEmpty(strCnpj) && blnSum)
                    SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes,consolidado.sub_consultoria, consolidado.Grupo");

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

                SQL.Append(" AND usuarios_vinculos.UsuarioId = @UsuarioId");

                if (intUf > 0)
                    SQL.Append(" AND farmacias.uf = @uf");

                if (!String.IsNullOrEmpty(strCity))
                    SQL.Append(" AND farmacias.cidade = @city");

                if (String.IsNullOrEmpty(strCnpj) && blnSum)
                    SQL.Append(" GROUP BY r.cnpj, r.descricao, consolidado.ano, consolidado.Mes,consolidado.sub_consultoria, consolidado.Grupo");

                SQL.Append(" ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");
            }

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = strCnpj;
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@UsuarioId", NpgsqlDbType.Integer).Value = clsUser.UserId;
            cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = intUf;
            cmm.Parameters.Add("@city", NpgsqlDbType.Varchar).Value = strCity;
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
        
        // Utilizado pelo modelo1 a
        public List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, int intRedeId, string strCnpj, string strCity, int intUf)
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
                    INNER JOIN farmacias ON farmacias.Cnpj = consolidado.CNPJ
                    LEFT JOIN redesfarmaceuticas ON farmacias.idRede = redesfarmaceuticas.id
                    WHERE upper(consolidado.Grupo) IN ('GENÉRICOS' , 'ALTERNATIVOS' , 'PROPAGADOS') 
                    AND farmacias.idRede = @idRede
                    AND (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') >= to_date(@DataIni,'MM yyyy')) AND
                    (to_date(to_char(consolidado.Mes,'99') || to_char(consolidado.Ano,'9999'), 'MM yyyy') <= to_date(@DataFim,'MM yyyy'))");

            if (!String.IsNullOrEmpty(strCnpj))
                SQL.Append(" AND consolidado.CNPJ = @CNPJ");

            if (intUf > 0)
                SQL.Append(" AND farmacias.uf = @uf");

            if (!String.IsNullOrEmpty(strCity))
                SQL.Append(" AND farmacias.cidade = @city");

            SQL.Append(@" GROUP BY redesfarmaceuticas.descricao,redesfarmaceuticas.CNPJ,consolidado.Mes,consolidado.Ano,
                    consolidado.Sub_Consultoria,consolidado.Grupo
                    ORDER BY consolidado.Ano,consolidado.Mes,consolidado.Sub_Consultoria,consolidado.Grupo");

            String ini, fim;

            ini = strInicio.Replace('/', ' ');
            fim = strFim.Replace('/', ' ');

            cmm.CommandText = SQL.ToString();
            cmm.Parameters.Add("@DataIni", NpgsqlDbType.Varchar).Value = ini;
            cmm.Parameters.Add("@DataFim", NpgsqlDbType.Varchar).Value = fim;
            cmm.Parameters.Add("@idRede ", NpgsqlDbType.Integer).Value = intRedeId;
            cmm.Parameters.Add("@CNPJ", NpgsqlDbType.Varchar).Value = strCnpj;
            cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = intUf;
            cmm.Parameters.Add("@city", NpgsqlDbType.Varchar).Value = strCity;
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
                            or.Cnpj = clsFuncs.MaskCnpj(ds.Tables["CrossR1"].Rows[i]["CNPJ"].ToString());
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

        internal static Rede GetRedeByLojaId(int loja_id)
        {
            DataSet ds = new DataSet();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            Rede r = new Rede();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT redesfarmaceuticas.Id,redesfarmaceuticas.Descricao FROM  redesfarmaceuticas 
            INNER JOIN farmacias f ON redesfarmaceuticas.Id = f.idrede
            WHERE (f.id = @loja_id)";

            cmm.Parameters.Add("@loja_id", NpgsqlDbType.Integer).Value = loja_id;

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
                }
            }

            return r;
        }

        public DataTable GetCityByNetworkAndUF(int id, int ufId)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            StringBuilder sb = new StringBuilder(@"SELECT DISTINCT cidade FROM farmacias WHERE farmacias.idRede = @idRede AND uf = @uf ORDER BY cidade");
            cmm.Connection = cnn;

            cmm.CommandText = sb.ToString();
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = id;
            cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = ufId;

            if (clsDB.openConnection(cmm))
            {
                dt = clsDB.QueryDS(ref cmm, ref dt, "city");
            }
            clsDB.closeConnection(cmm);

            return dt;
        }

        public DataTable GetCityByUserIdAndUF(int id, int ufId)
        {
            DataTable dt = new DataTable();
            NpgsqlCommand cmm = new NpgsqlCommand();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            StringBuilder sb = new StringBuilder(@"SELECT DISTINCT cidade FROM farmacias f INNER JOIN usuarios_vinculos u ON f.id = u.farmaciaid WHERE u.usuarioid = @id AND uf = @uf ORDER BY cidade");
            cmm.Connection = cnn;

            cmm.CommandText = sb.ToString();
            cmm.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
            cmm.Parameters.Add("@uf", NpgsqlDbType.Integer).Value = ufId;

            if (clsDB.openConnection(cmm))
            {
                dt = clsDB.QueryDS(ref cmm, ref dt, "city");
            }
            clsDB.closeConnection(cmm);

            return dt;
        }

        #endregion

        #region .:Persistence:.

        internal static string AddUser(UsersTO clsUser)
        {
            clsFuncs o = new clsFuncs();
            NpgsqlCommand cmm = new NpgsqlCommand();
            clsDB clsDB = new clsDB();
            string msg = "";

            try
            {
                string pass = o.encr(clsUser.Password);
                string nme = o.encr(clsUser.Name.ToUpper());

                NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
                cmm.Connection = cnn;

                if (clsDB.openConnection(cmm))
                {
                    cmm.CommandText = "SELECT UserId FROM  users WHERE UserName = @UserName";
                    cmm.Parameters.Clear();
                    cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;

                    int id = 0;
                    if (clsDB.Query(id, ref cmm) == DBNull.Value)
                    {
                        cmm.CommandText = @"INSERT INTO users (UserName, LastActivityDate, TipoId, nivel, owner) VALUES (@UserName, @LastActivityDate, @TipoId, @nivel, @owner)";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@UserName", NpgsqlDbType.Varchar).Value = clsUser.UserName;
                        cmm.Parameters.Add("@LastActivityDate", NpgsqlDbType.Date).Value = DateTime.Now.Date;
                        cmm.Parameters.Add("@TipoId", NpgsqlDbType.Integer).Value = clsUser.TipoId;
                        cmm.Parameters.Add("@nivel", NpgsqlDbType.Integer).Value = clsUser.Nivel;
                        cmm.Parameters.Add("@owner", NpgsqlDbType.Integer).Value = clsUser.owner;

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
                + " VALUES ('" + rede.RedeName + "','" + rede.CNPJ + "')";

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
                + "', CNPJ = '" + rede.CNPJ + "' WHERE Id = " + rede.RedeId;

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
            Bairro, Complemento, Cidade, UF, Tel1, Tel2, Celular, Site, Skype, Ativo, idRede, CEP)"
                + " VALUES ('" + ol.Proprietario + "', '" + ol.Gerente + "', '" + ol.Email + "', '" + ol.Email2 + "', '"
                + ol.NomeFantasia + "', '" + ol.Razao + "', '" + scnpj + "', '" + ol.Endereco + "', '" + ol.EndNumero
                + "', '" + ol.Bairro + "', '" + ol.Complemento + "', '" + ol.Cidade + "', '" + ol.Uf + "', '" + ol.Fone
                + "', '" + ol.Fone2 + "', '" + ol.Celular + "', '" + ol.Site + "', '" + ol.Skype + "', "
                + (ol.Ativo == true ? 1 : 0) + ", " + ol.idRede + ",'" + ol.CEP + "')";

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
                + clsLoja.Celular + "', Site = '" + clsLoja.Site + "', Skype = '" + clsLoja.Skype + "', Ativo = " + (clsLoja.Ativo == true ? 1 : 0) + ", idRede = " + clsLoja.idRede + ",CEP = '" + clsLoja.CEP + "' WHERE Id = "
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

        public string NewAddXml(DataTable dt, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            string msg = "";

            if (dt.Rows.Count > 0)
            {
                var sql = "copy base_cliente_espera (Cnpj, Barras, Descricao";

                if (dt.Columns.Contains("grupo"))
                    sql += ", grupo";

                sql += ", Fabricante, Ano, Mes, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto) from stdin with delimiter '|'";

                cmm = new NpgsqlCommand(sql, cnn);

                cnn.Open();

                var copy = new NpgsqlCopyIn(cmm, cnn);
                try
                {
                    copy.Start();
                    foreach (DataRow row in dt.Rows)
                    {
                        var data = SerializeXmlData(row.ItemArray);
                        var raw = Encoding.UTF8.GetBytes(string.Concat(data, "\n"));
                        copy.CopyStream.Write(raw, 0, raw.Length);
                    }
                }
                catch (Exception ex)
                {
                    copy.Cancel("Undo copy");
                    msg = ex.Message;
                }
                finally
                {
                    if (copy.CopyStream != null)
                    {
                        copy.CopyStream.Close();
                    }
                    copy.End();
                }

                if (String.IsNullOrEmpty(msg))
                    AddXmlData(dt, clsUser);

            }
            else { msg = "Erro ao converter o xml."; }

            return msg;
        }

        private object SerializeData(object[] data)
        {
            var sb = new StringBuilder();
            foreach (var d in data)
            {
                if (d == null)
                {
                    sb.Append("\\N");
                }
                else if (d is DateTime)
                {
                    sb.Append(((DateTime)d).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else if (d is Enum)
                {
                    sb.Append(((Enum)d).ToString("d"));
                }
                else
                {
                    sb.Append(d.ToString().Replace(".", "").Replace(",", "."));
                }
                sb.Append("|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        private object SerializeXmlData(object[] data)
        {
            var sb = new StringBuilder();
            foreach (var d in data)
            {
                if (d == null)
                {
                    sb.Append("\\N");
                }
                else if (d is DateTime)
                {
                    sb.Append(((DateTime)d).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else if (d is Enum)
                {
                    sb.Append(((Enum)d).ToString("d"));
                }
                sb.Append("|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
        private void AddXmlData(DataTable dt, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            cmm.Connection = cnn;
            DataTable auxDt = new DataTable();

            auxDt = dt.DefaultView.ToTable(true, "cnpj", "ano", "mes");

            if (auxDt.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < auxDt.Rows.Count; i++)
                    {
                        cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                            + " VALUES ( @UserId, @cnpj, 'XML', @mes, @ano)";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = clsUser.UserId;
                        cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = auxDt.Rows[i][0].ToString();
                        cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = auxDt.Rows[i][1];
                        cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = auxDt.Rows[i][2];

                        if (clsDB.openConnection(cmm))
                            clsDB.Execute(ref cmm);

                        clsDB.closeConnection(cmm);
                    }
                }
                catch
                {
                    clsDB.closeConnection(cmm);
                }

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

        public string AddTxt(DataTable dt, UsersTO clsUser, List<int> lstMeses, List<string> lstCnpj)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            cmm.Connection = cnn;

            string msg = "";

            if (dt.Rows.Count > 0)
            {
                try
                {
                    cmm.CommandText = @"INSERT INTO base_cliente_espera (Razao_Social, Cnpj, Mes, Ano, Barras, Descricao,
                              Fabricante, Quantidade, Valor_Bruto, Valor_Liquido, Valor_Desconto)
                              VALUES (@Razao_Social, @Cnpj, @Mes, @Ano, @Barras, @Descricao,
                              @Fabricante, @Quantidade, @Valor_Bruto, @Valor_Liquido, @Valor_Desconto)";

                    cmm.Parameters.Add("@Razao_Social", NpgsqlDbType.Varchar, 255, "RazaoSocial");
                    cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar, 255, "Cnpj");
                    cmm.Parameters.Add("@Mes", NpgsqlDbType.Integer, 2, "Mes");
                    cmm.Parameters.Add("@Ano", NpgsqlDbType.Integer, 4, "Ano");
                    cmm.Parameters.Add("@Barras", NpgsqlDbType.Varchar, 255, "Barras");
                    cmm.Parameters.Add("@Descricao", NpgsqlDbType.Varchar, 255, "Descricao");
                    cmm.Parameters.Add("@Fabricante", NpgsqlDbType.Varchar, 255, "Fabricante");
                    cmm.Parameters.Add("@Quantidade", NpgsqlDbType.Integer, 255, "Quantidade");
                    cmm.Parameters.Add("@Valor_Bruto", NpgsqlDbType.Numeric, 15, "Valorbruto");
                    cmm.Parameters.Add("@Valor_Liquido", NpgsqlDbType.Numeric, 15, "Valorliquido");
                    cmm.Parameters.Add("@Valor_Desconto", NpgsqlDbType.Numeric, 15, "Valordesconto");

                    cnn.Open();
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmm);
                    adapter.InsertCommand = cmm;
                    adapter.Update(dt);
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    clsDB.closeConnection(cmm);
                }

                AddTxtData(dt, clsUser);

            }
            else { msg = "Erro ao converter o txt."; }

            return msg;
        }

        public string NewAddTxt(DataTable dt, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            cmm.Connection = cnn;

            string msg = "";

            if (dt.Rows.Count > 0)
            {
                var sql = "copy base_cliente_espera (razao_social, cnpj, mes, ano, barras, descricao, fabricante, grupo, total_custo, quantidade, valor_bruto, valor_liquido, valor_desconto) from stdin with delimiter '|'";

                cmm = new NpgsqlCommand(sql, cnn);

                cnn.Open();

                var copy = new NpgsqlCopyIn(cmm, cnn);
                try
                {
                    copy.Start();
                    foreach (DataRow row in dt.Rows)
                    {
                        var data = SerializeData(row.ItemArray);
                        var raw = Encoding.UTF8.GetBytes(string.Concat(data, "\n"));
                        copy.CopyStream.Write(raw, 0, raw.Length);
                    }
                }
                catch (Exception ex)
                {
                    copy.Cancel("Undo copy");
                    msg = ex.Message;
                }
                finally
                {
                    if (copy.CopyStream != null)
                    {
                        copy.CopyStream.Close();
                    }
                    copy.End();
                }

                if (String.IsNullOrEmpty(msg))
                    AddTxtData(dt, clsUser);

            }
            else { msg = "Erro ao converter o txt."; }

            return msg;
        }

        public static void AddTxtData(DataTable dt, UsersTO clsUser)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();
            cmm.Connection = cnn;
            DataTable auxDt = new DataTable();
            auxDt = dt.DefaultView.ToTable(true, "cnpj", "ano", "mes");
            if (auxDt.Rows.Count > 0)
            {
                try
                {
                    if (clsDB.openConnection(cmm))
                    {
                        for (int i = 0; i < auxDt.Rows.Count; i++)
                        {
                            cmm.CommandText = "INSERT INTO arquivosenviados (UserId, cnpj, tipo, mes, ano)"
                                + " VALUES (@UserId, @cnpj, 'TXT', @mes, @ano)";
                            cmm.Parameters.Clear();
                            cmm.Parameters.Add("@UserId", NpgsqlDbType.Integer).Value = clsUser.UserId;
                            cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = auxDt.Rows[i][0].ToString();
                            cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = auxDt.Rows[i][1];
                            cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = auxDt.Rows[i][2];

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

        internal static string UpdateUser(UsersTO clsUser)
        {
            clsFuncs o = new clsFuncs();
            NpgsqlCommand cmm = new NpgsqlCommand();
            clsDB clsDB = new clsDB();
            string msg = "";

            try
            {
                string pass = o.encr(clsUser.Password);
                string nme = o.encr(clsUser.Name.ToUpper());

                NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
                cmm.Connection = cnn;

                if (clsDB.openConnection(cmm))
                {
                    string lDate = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00";
                    StringBuilder stbSQL = new StringBuilder();
                    stbSQL.Append("UPDATE users SET UserName = '" + clsUser.UserName + "', LastActivityDate = '" + lDate + "'");
                    if (clsUser.TipoId > 0) stbSQL.Append(",TipoId = @TipoId");

                    stbSQL.Append(",nivel = @nivel WHERE UserId = " + clsUser.UserId);
                    cmm.CommandText = stbSQL.ToString();
                    cmm.Parameters.Add("@TipoId", NpgsqlDbType.Integer).Value = clsUser.TipoId;
                    cmm.Parameters.Add("@nivel", NpgsqlDbType.Integer).Value = clsUser.Nivel;

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

        #region .:Methods:.
        public static void GetOption(System.Web.UI.HtmlControls.HtmlGenericControl ulConf, UsersTO ouser)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            <li>
                <a href='wfmUsers.aspx' title=''>
                    <div class='imgCon'>
                        <p>Cadastro de usuários</p>
                    </div>
                </a>
            </li>");

            if (ouser.Nivel.Equals(0))
                sb.Append(@"
                <li>
                    <a href='wfmCadastroRede.aspx' title=''>
                        <div class='imgCon'>
                            <p>Cadastro de redes</p>
                        </div>
                    </a>
                </li>");

            if (!ouser.Nivel.Equals(2))
                sb.Append(@"
                <li>
                    <a href='wfmCadastroLojas.aspx' title=''>
                        <div class='imgCon'>
                            <p>Cadastro de lojas</p>
                        </div>
                    </a>
                </li>");

            sb.Append(@"
            <li>
                <a href='wfmVinculos.aspx' title=''>
                    <div class='imgCon'>
                        <p>Vincular usuário</p>
                    </div>
                </a>
            </li>");

            if (ouser.Nivel.Equals(0))
                sb.Append(@"
                <li>
                    <a href='wfmFiles.aspx' title=''>
                        <div class='imgCon'>
                            <p>Histórico de uploads</p>
                        </div>
                    </a>    
                </li>
                <li>
                    <a href='wfmBanco.aspx' title=''>
                            <div class='imgCon'>
                            <p>Gerênciamento do banco</p>
                        </div>
                    </a>
                </li>");

            ulConf.InnerHtml = sb.ToString();
        }
        #endregion
    }
}
