using System;
using System.Data.Common;
using MySql.Data.MySqlClient;
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
            if (TransfereDados())
            {
                MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

                try
                {
                    StringBuilder strSQL = new StringBuilder();
                    strSQL.Append(@"INSERT INTO consolidado (consolidado.CNPJ,consolidado.Mes,consolidado.Ano,consolidado.Grupo,consolidado.Sub_Consultoria,consolidado.Quantidade,consolidado.Valor_Bruto,consolidado.Valor_Liquido,consolidado.Valor_Desconto) 
                                SELECT base_cliente_espera.Cnpj,base_cliente_espera.Mes,base_cliente_espera.Ano,produtos_base.Grupo,produtos_base.Sub_Consultoria,Sum(base_cliente_espera.Quantidade),Sum(base_cliente_espera.Valor_Bruto),Sum(base_cliente_espera.Valor_Liquido),Sum(base_cliente_espera.Valor_Desconto)
                                FROM base_cliente_espera
                                INNER JOIN produtos_base ON base_cliente_espera.Barras = produtos_base.CodBarra
                                GROUP BY base_cliente_espera.Cnpj,base_cliente_espera.Mes,base_cliente_espera.Ano,produtos_base.Grupo,produtos_base.Sub_Consultoria");

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

            if (blnOk)
            {
                DeletaDados();
            }
        }

        #region .: Metodos :.
        protected static Boolean VerificaConteudo()
        {
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);
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
        private static Boolean TransfereDados()
        {
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("INSERT INTO base_clientes (Razao_Social,Cnpj,Mes,Ano,Barras,Descricao,Fabricante,Quantidade,Valor_Bruto,Valor_Liquido,Valor_Desconto)");
                strSQL.Append(@" SELECT base_cliente_espera.Razao_Social,base_cliente_espera.Cnpj,base_cliente_espera.Mes,base_cliente_espera.Ano,
                                base_cliente_espera.Barras,base_cliente_espera.Descricao,base_cliente_espera.Fabricante,base_cliente_espera.Quantidade,base_cliente_espera.Valor_Bruto,base_cliente_espera.Valor_Liquido,base_cliente_espera.Valor_Desconto FROM base_cliente_espera");

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.CommandTimeout = 9999;
                msc.Open();
                cmdUsers.ExecuteNonQuery();
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
        private static void DeletaDados()
        {
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

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
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_String"].ConnectionString);

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
