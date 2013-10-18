using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using System.Web.UI.WebControls;

namespace JobConsolidacao
{
    class Program
    {
        static void Main(string[] args)
        {
            if (VerificaConteudo())
                RunJob();
        }

        private static void RunJob()
        {
            Boolean blnOk = false;

            DeletaDuplicados();
            List<CNPJ> lstCnpj = TransfereDados();
            if (lstCnpj.Count > 0)
            {
                lstCnpj.ForEach(delegate(CNPJ _cnpj)
                {
                    if (_cnpj.Return)
                    {
                        NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

                        try
                        {
                            StringBuilder strSQL = new StringBuilder();
                            strSQL.Append(@"INSERT INTO consolidado (CNPJ,Mes,Ano,Grupo,Sub_Consultoria,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto,Importado,farmaciaid) 
                                SELECT b.Cnpj,b.Mes,b.Ano,CASE WHEN (p.apelido is null or p.apelido = '') THEN p.nome ELSE p.apelido END,
                                ps.nome,
                                Sum(b.Quantidade),Sum(b.Valor_Bruto),Sum(b.Valor_Liquido),
                                Sum(b.Valor_Desconto),produtos_base.Importado,f.id
                                FROM base_cliente_espera b
                                INNER JOIN produtos_base ON b.Barras = produtos_base.CodBarra
                                INNER JOIN farmacias f ON b.Cnpj = f.Cnpj
                                INNER JOIN produtos_grupos p ON produtos_base.""grupoID"" = p.id
                                INNER JOIN produtos_subgrupos ps ON produtos_base.""subID"" = ps.id
                                WHERE b.Cnpj = @Cnpj
                                GROUP BY b.Cnpj,b.Mes,b.Ano,p.apelido,p.nome,ps.nome,
                                produtos_base.Importado,f.id");

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

                        if (blnOk)
                            blnOk = AtualizaProdutosCliente(_cnpj);
                    }
                });
            }

            if (blnOk)
            {
                DeletaDados();
            }
        }

        #region .: Metodos :.

        protected static Boolean VerificaConteudo()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
            Int64 intQtde = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT COUNT(ID) AS Qtde FROM base_cliente_espera");

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    if (drd.Read())
                    {
                        if (!drd.IsDBNull(drd.GetOrdinal("Qtde"))) intQtde = drd.GetInt64(drd.GetOrdinal("Qtde")); else intQtde = 0;
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return intQtde > 0 ? true : false;
        }

        private static List<CNPJ> TransfereDados()
        {
            List<CNPJ> lstCnpj = GetlistCnpj();

            if (lstCnpj.Count > 0)
            {
                NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

                lstCnpj.ForEach(delegate(CNPJ cnpj)
                {

                    try
                    {
                        StringBuilder strSQL = new StringBuilder();
                        strSQL.Append(@"INSERT INTO base_clientes (Cnpj,Mes,Ano,Barras,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto,farmaciaid)
                        SELECT b.Cnpj,b.Mes,b.Ano,b.Barras,b.Quantidade,b.Valor_Bruto,b.Valor_Liquido,b.Valor_Desconto,f.id
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
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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

        private static void DeletaDados()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM base_cliente_espera");

                DbCommand cmd = msc.CreateCommand();
                cmd.CommandText = strSQL.ToString();

                msc.Open();

                cmd.ExecuteNonQuery();

            }
            finally
            {
                msc.Close();
            }
        }

        private static void DeletaDuplicados()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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
                DeletaDuplicadosBClientes(_file);

                DeletaDuplicadosConsolidado(_file);
            });
        }

        private static void DeletaDuplicadosBClientes(Files _file)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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

        private static void DeletaDuplicadosConsolidado(Files _file)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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

        private static bool AtualizaProdutosCliente(CNPJ _cnpj)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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
	                WHERE b.Cnpj = @Cnpj and b.barras not in {0} and p.nome <> b.descricao
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
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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

        #endregion
    }
}
