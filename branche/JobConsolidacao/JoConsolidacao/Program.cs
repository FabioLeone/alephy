using System;
using System.Data.Common;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;
using System.Text;
using System.Data;
using System.Collections.Generic;

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
            List<Cnpj> lstCnpj = TransfereDados();
            if (lstCnpj.Count > 0)
            {
                lstCnpj.ForEach(delegate(Cnpj _cnpj) {
                    if (_cnpj.Return)
                    {
                        NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

                        try
                        {
                            StringBuilder strSQL = new StringBuilder();
                            strSQL.Append(@"INSERT INTO consolidado (consolidado.CNPJ,consolidado.Mes,consolidado.Ano,consolidado.Grupo,consolidado.Sub_Consultoria,consolidado.Quantidade,consolidado.Valor_Bruto,consolidado.Valor_Liquido,consolidado.Valor_Desconto,consolidado.Importado,consolidado.farmaciaid) 
                                SELECT base_cliente_espera.Cnpj,base_cliente_espera.Mes,base_cliente_espera.Ano,produtos_base.Grupo,produtos_base.Sub_Consultoria,Sum(base_cliente_espera.Quantidade),Sum(base_cliente_espera.Valor_Bruto),Sum(base_cliente_espera.Valor_Liquido),Sum(base_cliente_espera.Valor_Desconto),
                                produtos_base.Importado
                                FROM base_cliente_espera
                                INNER JOIN produtos_base ON base_cliente_espera.Barras = produtos_base.CodBarra
                                GROUP BY base_cliente_espera.Cnpj,base_cliente_espera.Mes,base_cliente_espera.Ano,produtos_base.Grupo,produtos_base.Sub_Consultoria,produtos_base.Importado");

                            DbCommand cmdUsers = msc.CreateCommand();

                            cmdUsers.CommandText = strSQL.ToString();
                            cmdUsers.CommandTimeout = 9999;
                            msc.Open();
                            cmdUsers.ExecuteNonQuery();
                            blnOk = true;
                        }
                        finally
                        {
                            msc.Close();
                        }
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
            int intQtde = 0;

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
                        if (!drd.IsDBNull(drd.GetOrdinal("Qtde"))) intQtde = drd.GetInt32(drd.GetOrdinal("Qtde"));  else intQtde = 0; 
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return intQtde > 0 ? true : false;
        }

        private static List<Cnpj> TransfereDados()
        {
            List<Cnpj> lstCnpj = GetlistCnpj();

            if (lstCnpj.Count > 0)
            {
                NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

                lstCnpj.ForEach(delegate(Cnpj cnpj)
                {
                    try
                    {
                        StringBuilder strSQL = new StringBuilder();
                        strSQL.Append("INSERT INTO base_clientes (Razao_Social,Cnpj,Mes,Ano,Barras,Descricao,Fabricante,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto,farmaciaid)");
                        strSQL.Append(@" SELECT b.Razao_Social,b.Cnpj,b.Mes,b.Ano,b.Barras,b.Descricao,b.Fabricante,b.Quantidade,b.Valor_Bruto,b.Valor_Liquido,b.Valor_Desconto,f.id
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

        private static List<Cnpj> GetlistCnpj()
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
            List<Cnpj> lstCnpj = new List<Cnpj>();
            Cnpj objCnpj;

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
                        if (!drd.IsDBNull(drd.GetOrdinal("Cnpj"))) lstCnpj.Add(objCnpj = new Cnpj(){ Cnpj = drd.GetString(drd.GetOrdinal("Cnpj"))});
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

            try
            {
                StringBuilder strSQL = new StringBuilder();
                List<Files> lstFiles = new List<Files>();
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

                lstFiles.ForEach(delegate(Files _file)
                {
                    strSQL = new StringBuilder();
                    strSQL.Append("DELETE FROM base_clientes");
                    strSQL.Append(" WHERE CNPJ = '" + _file.Cnpj + "'");
                    strSQL.Append(" AND Mes = " + _file.Mes + " AND Ano = " + _file.Ano);

                    cmd = msc.CreateCommand();
                    cmd.CommandText = strSQL.ToString();

                    cmd.ExecuteNonQuery();

                    strSQL = new StringBuilder();
                    strSQL.Append("DELETE FROM consolidado");
                    strSQL.Append(" WHERE CNPJ = '" + _file.Cnpj + "'");
                    strSQL.Append(" AND Mes = " + _file.Mes + " AND Ano = " + _file.Ano);

                    cmd.CommandText = String.Empty;
                    cmd.CommandText = strSQL.ToString();

                    cmd.ExecuteNonQuery();
                });
            }
            finally
            {
                msc.Close();
            }
            
        }

        #endregion
    }
}
