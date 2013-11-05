using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace goku.resources
{
    public class dal
    {
        #region .:sendFile:.
        internal static int validationByCNPJ(string strCNPJ)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();
            int id = 0;

            cmm.Connection = cnn;
            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"SELECT farmacias.Id FROM farmacias WHERE (farmacias.Cnpj = @Cnpj)");

            string scnpj = strCNPJ.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            cmm.CommandText = strSQL.ToString();
            cmm.Parameters.Clear();
            cmm.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = scnpj;

            try
            {
                cnn.Open();

                using (IDataReader idrd = cmm.ExecuteReader())
                {
                    if (idrd.Read())
                    {
                        if (!idrd.IsDBNull(idrd.GetOrdinal("Id"))) id = idrd.GetInt32(idrd.GetOrdinal("Id"));
                    }
                }
            }
            finally
            {
                cnn.Close();
            }

            return id;
        }

        internal static string insertXml(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm;
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
                    insertXmlData(dt);

            }
            else { msg = "Erro ao converter o xml."; }

            return msg;
        }

        private static object SerializeData(object[] data)
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
                    sb.Append(d.ToString().Replace(",", "."));
                }
                sb.Append("|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        private static void insertXmlData(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            DataTable auxDt = new DataTable();

            auxDt = dt.DefaultView.ToTable(true, "cnpj", "ano", "mes");

            if (auxDt.Rows.Count > 0)
            {
                for (int i = 0; i < auxDt.Rows.Count; i++)
                {
                    cmm.CommandText = "INSERT INTO arquivosenviados (cnpj, tipo, mes, ano)"
                        + " VALUES (@cnpj, 'XML', @mes, @ano)";
                    cmm.Parameters.Clear();
                    cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = auxDt.Rows[i][0].ToString();
                    cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = auxDt.Rows[i][1];
                    cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = auxDt.Rows[i][2];

                    try
                    {
                        cnn.Open();
                        cmm.ExecuteNonQuery();
                    }
                    catch
                    {
                        cnn.Close();
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            }
        }

        internal static string inserTxt(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

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
                    AddTxtData(dt);

            }
            else { msg = "Erro ao converter o txt."; }

            return msg;
        }

        private static void AddTxtData(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            DataTable auxDt = new DataTable();
            auxDt = dt.DefaultView.ToTable(true, "cnpj", "ano", "mes");
            
            if (auxDt.Rows.Count > 0)
            {
                try
                {
                    cnn.Open();

                    for (int i = 0; i < auxDt.Rows.Count; i++)
                    {
                        cmm.CommandText = "INSERT INTO arquivosenviados (cnpj, tipo, mes, ano)"
                            + " VALUES (@cnpj, 'TXT', @mes, @ano)";
                        cmm.Parameters.Clear();
                        cmm.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = auxDt.Rows[i][0].ToString();
                        cmm.Parameters.Add("@ano", NpgsqlDbType.Integer).Value = auxDt.Rows[i][1];
                        cmm.Parameters.Add("@mes", NpgsqlDbType.Integer).Value = auxDt.Rows[i][2];

                        cmm.ExecuteNonQuery();
                    }
                }
                finally
                {
                    cnn.Close();
                }
            }
        }
        #endregion

        #region .:consolidation:.
        internal static void deleteDuplicate()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            List<Files> lstFiles = new List<Files>();

            try
            {
                StringBuilder strSQL = new StringBuilder();
                Files clsFile = new Files();

                strSQL.Append("SELECT DISTINCT CNPJ, Mes, Ano FROM base_cliente_espera ORDER BY Mes");

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        clsFile = new Files();

                        if (!drd.IsDBNull(drd.GetOrdinal("CNPJ"))) clsFile.Cnpj = drd.GetString(drd.GetOrdinal("CNPJ")); else clsFile.Cnpj = String.Empty;
                        if (!drd.IsDBNull(drd.GetOrdinal("Mes"))) clsFile.Mes = drd.GetInt32(drd.GetOrdinal("Mes")); else clsFile.Mes = 0;
                        if (!drd.IsDBNull(drd.GetOrdinal("Ano"))) clsFile.Ano = drd.GetInt32(drd.GetOrdinal("Ano")); else clsFile.Ano = 0;

                        lstFiles.Add(clsFile);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            lstFiles.ForEach(delegate(Files _file)
            {
                deleteDuplicateBClientes(_file);

                deleteDuplicateFromConsolidation(_file);
            });
        }

        private static void deleteDuplicateBClientes(Files _file)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            StringBuilder strSQL = new StringBuilder();
            try
            {
                msc.Open();

                strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM base_clientes");
                strSQL.Append(" WHERE CNPJ = '" + _file.Cnpj + "'");
                strSQL.Append(" AND Mes = " + _file.Mes + " AND Ano = " + _file.Ano);

                DbCommand cmd = msc.CreateCommand();

                cmd = msc.CreateCommand();
                cmd.CommandText = strSQL.ToString();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }

        private static void deleteDuplicateFromConsolidation(Files _file)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            StringBuilder strSQL = new StringBuilder();
            try
            {
                msc.Open();

                strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM consolidado");
                strSQL.Append(" WHERE CNPJ = '" + _file.Cnpj + "'");
                strSQL.Append(" AND Mes = " + _file.Mes + " AND Ano = " + _file.Ano);

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = String.Empty;
                cmd.CommandText = strSQL.ToString();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }

        internal static List<CNPJ> dataTransfer()
        {
            List<CNPJ> lstCnpj = GetlistCnpj();

            if (lstCnpj.Count > 0)
            {
                NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);

                lstCnpj.ForEach(delegate(CNPJ cnpj)
                {

                    try
                    {
                        StringBuilder strSQL = new StringBuilder();
                        strSQL.Append(@"INSERT INTO base_clientes (Cnpj,Mes,Ano,Barras,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto,farmaciaid)
                        SELECT distinct b.Cnpj,b.Mes,b.Ano,b.Barras,b.Quantidade,b.Valor_Bruto,b.Valor_Liquido,b.Valor_Desconto,f.id
                        FROM base_cliente_espera b
                        INNER JOIN farmacias f ON b.Cnpj = f.Cnpj
                        WHERE b.Cnpj = @Cnpj");

                        NpgsqlCommand cmdUsers = msc.CreateCommand();

                        cmdUsers.CommandText = strSQL.ToString();
                        cmdUsers.CommandTimeout = 9999;
                        cmdUsers.Parameters.Clear();
                        cmdUsers.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = cnpj.Cnpj;

                        msc.Open();
                        cmdUsers.ExecuteNonQuery();
                        cnpj.Return = true;
                    }
                    catch
                    {
                        cnpj.Return = false;
                    }
                    finally
                    {
                        msc.Close();
                    }
                });
            }

            return lstCnpj;
        }

        private static List<CNPJ> GetlistCnpj()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            List<CNPJ> lstCnpj = new List<CNPJ>();
            CNPJ objCnpj;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT distinct base_cliente_espera.Cnpj FROM base_cliente_espera");

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        if (!drd.IsDBNull(drd.GetOrdinal("Cnpj"))) lstCnpj.Add(objCnpj = new CNPJ() { Cnpj = drd.GetString(drd.GetOrdinal("Cnpj")) });
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return lstCnpj;
        }

        internal static bool dataConsolidation(CNPJ _cnpj)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            bool blnOk = false;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"INSERT INTO consolidado (CNPJ,Mes,Ano,Grupo,Sub_Consultoria,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto,Importado,farmaciaid) 
                            SELECT Cnpj,Mes,Ano,apelido,nome,Sum(Quantidade),Sum(Valor_Bruto),Sum(Valor_Liquido),Sum(Valor_Desconto),Importado,id
                            FROM (
                            SELECT distinct b.Cnpj,b.Mes,b.Ano,CASE WHEN (p.apelido is null or p.apelido = '') THEN p.nome ELSE p.apelido END as apelido,
                            ps.nome
                            ,b.Quantidade,b.Valor_Bruto,b.Valor_Liquido,b.Valor_Desconto,produtos_base.Importado,f.id,
                            b.barras
                            FROM base_cliente_espera b
                            INNER JOIN produtos_base ON b.Barras = produtos_base.CodBarra
                            INNER JOIN farmacias f ON b.Cnpj = f.Cnpj
                            INNER JOIN produtos_grupos p ON produtos_base.""grupoID"" = p.id
                            INNER JOIN produtos_subgrupos ps ON produtos_base.""subID"" = ps.id
                            WHERE b.Cnpj = @Cnpj
                            ) t
                            GROUP BY Cnpj,Mes,Ano,apelido,nome,nome,Importado,id");

                NpgsqlCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;
                cmdUsers.CommandTimeout = 9999;
                msc.Open();
                cmdUsers.ExecuteNonQuery();
                blnOk = true;
            }
            finally
            {
                msc.Close();
            }

            return blnOk;
        }

        internal static bool updateClientProdut(CNPJ _cnpj)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            Boolean blnOK = false;
            List<ListItem> lstean = GetListProd(_cnpj);
            StringBuilder strSQL;
            StringBuilder strBarras = new StringBuilder();

            if (lstean.Count > 0)
            {
                strSQL = new StringBuilder();

                strSQL.Append(@"update produtos_clientes set nome = be.descricao, grupo = be.grupo, ""ultimaModf"" = current_date
                    from ( select b.descricao, b.grupo, f.id, b.barras 
                    from base_cliente_espera b 
                    inner join farmacias f on f.cnpj = b.cnpj 
                    WHERE b.Cnpj = @Cnpj
                    group by b.cnpj, b.descricao, b.grupo, f.id, b.barras 
                    order by b.descricao) be where produtos_clientes.cliente_id = be.id and (case when be.barras is null or be.barras = '' then produtos_clientes.nome = be.descricao else produtos_clientes.codbarra = be.barras end);");

                try
                {
                    NpgsqlCommand cmdUsers = msc.CreateCommand();

                    cmdUsers.CommandText = strSQL.ToString();
                    cmdUsers.CommandTimeout = 9999;
                    cmdUsers.Parameters.Clear();
                    cmdUsers.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;

                    msc.Open();
                    cmdUsers.ExecuteNonQuery();
                    blnOK = true;
                }
                catch
                {
                    blnOK = false;
                }
                finally
                {
                    msc.Close();
                }

                int i = 0;
                lstean.ForEach(delegate(ListItem item)
                {
                    if (i == 0)
                    {
                        strBarras.Append("('" + item.Value + "'");
                        i++;
                    }
                    else
                    {
                        strBarras.Append(",'" + item.Value + "'");
                    }

                });

                strBarras.Append(")");

                strSQL = new StringBuilder();
                strSQL.Append(String.Format(@"INSERT INTO produtos_clientes (nome, grupo, cliente_id, ""ultimaModf"", codbarra) 
                    select b.descricao, b.grupo, f.id, current_date, b.barras 
	                from base_cliente_espera b inner join farmacias f on f.cnpj = b.cnpj 
	                left join produtos_clientes p on b.id = p.cliente_id
	                WHERE b.Cnpj = @Cnpj and b.barras not in {0}
                    and case when b.barras is null or b.barras = '' then p.nome <> b.descricao else b.descricao <> '' end
	                group by b.cnpj, b.descricao, b.grupo, f.id, b.barras order by b.descricao", strBarras));

                try
                {
                    NpgsqlCommand cmdUsers = msc.CreateCommand();

                    cmdUsers.CommandText = strSQL.ToString();
                    cmdUsers.CommandTimeout = 9999;
                    cmdUsers.Parameters.Clear();
                    cmdUsers.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;
                    cmdUsers.Parameters.Add("@barras", NpgsqlDbType.Varchar).Value = strBarras;

                    msc.Open();
                    cmdUsers.ExecuteNonQuery();
                    blnOK = true;
                }
                catch
                {
                    blnOK = false;
                }
                finally
                {
                    msc.Close();
                }
            }
            else
            {
                try
                {
                    strSQL = new StringBuilder();
                    strSQL.Append(@"INSERT INTO produtos_clientes (nome, grupo, cliente_id, ""ultimaModf"", codbarra) 
                    select b.descricao, b.grupo, f.id, current_date, b.barras 
                    from base_cliente_espera b 
                    inner join farmacias f on f.cnpj = b.cnpj 
                    WHERE b.Cnpj = @Cnpj
                    group by b.cnpj, b.descricao, b.grupo, f.id, b.barras order by b.descricao");

                    NpgsqlCommand cmdUsers = msc.CreateCommand();

                    cmdUsers.CommandText = strSQL.ToString();
                    cmdUsers.CommandTimeout = 9999;
                    cmdUsers.Parameters.Clear();
                    cmdUsers.Parameters.Add("@Cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;

                    msc.Open();
                    cmdUsers.ExecuteNonQuery();
                    blnOK = true;
                }
                catch
                {
                    blnOK = false;
                }
                finally
                {
                    msc.Close();
                }
            }

            return blnOK;
        }

        private static List<ListItem> GetListProd(CNPJ _cnpj)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            List<ListItem> lst = new List<ListItem>();

            try
            {
                StringBuilder strSQL = new StringBuilder();

                strSQL.Append("select distinct codbarra, nome from produtos_clientes p inner join farmacias f on p.cliente_id = f.id where f.cnpj = @cnpj");

                NpgsqlCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        if (!drd.IsDBNull(drd.GetOrdinal("nome"))) lst.Add(new ListItem(drd.GetString(drd.GetOrdinal("nome")), drd.GetString(drd.GetOrdinal("codbarra"))));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return lst;
        }

        internal static void DeletData(List<CNPJ> lstCnpj)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);

            lstCnpj.ForEach(delegate(CNPJ _cnpj)
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM base_cliente_espera where Cnpj=@cnpj");
                
                NpgsqlCommand cmd = msc.CreateCommand();
                
                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.Add("@cnpj", NpgsqlDbType.Varchar).Value = _cnpj.Cnpj;

                try
                {
                    msc.Open();

                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    msc.Close();
                }
            });
        }
        #endregion

    }
}
