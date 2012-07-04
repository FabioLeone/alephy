using System;
using System.Data;
using System.Data.SqlClient;

namespace SIAO.SRV
{
    public class clsDB
    {
        // Abre o acesso ao banco
        public bool openConnection(SqlCommand cmm)
        {
            bool lbOk = false;
            try
            {
                cmm.Connection.Open();
                lbOk = true;
            }
            catch
            {
                lbOk = false;
            }

            return lbOk;
        }

        // Fecha o acesso ao banco
        public void closeConnection(SqlCommand cmm)
        {
            try
            {
                if (cmm.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmm.Connection.Close();
                }
            }
            catch
            {
            }
        }

        // Traz o retorno do banco
        public object Query(object retorno, ref SqlCommand cmm)
        {
            DataSet ds = new DataSet();

            try
            {
                if (cmm.Connection.State == System.Data.ConnectionState.Closed)
                {
                    openConnection(cmm);
                }
            }
            catch
            {
                openConnection(cmm);
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmm);

                da.Fill(ds);
            }
            catch
            {

            }

            if (ds.Tables[0].Rows.Count > 0) { return defineTipoRetorno(retorno, ds); } else { return defineTipoRetorno(retorno, addLine(ref ds, 0)); }

        }

        // Difine o tipo de retorno do banco
        private object defineTipoRetorno(object retorno, DataSet ds)
        {
            switch (retorno.GetType().Name)
            {
                case "DataSet":
                    return (DataSet)ds;
                case "DataRow":
                    return (DataRow)ds.Tables[0].Rows[0];
                case "DataColumn":
                    return (DataColumn)ds.Tables[0].Columns[0];
                default:
                    return (object)ds.Tables[0].Rows[0][0];
            }
        }

        // Executa SQL
        public void Execute(ref SqlCommand cmm)
        {
            try
            {
                if (cmm.Connection.State == System.Data.ConnectionState.Closed)
                {
                    openConnection(cmm);
                }
            }
            catch
            {
                openConnection(cmm);
            }

            cmm.ExecuteNonQuery();
        }

        // Traz o dataset do banco
        public DataSet QueryDS(ref SqlCommand cmm, ref DataSet ds, string nomeTabela)
        {

            try
            {
                if (cmm.Connection.State == System.Data.ConnectionState.Closed)
                {
                    openConnection(cmm);
                }
            }
            catch
            {
                openConnection(cmm);
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmm);

                da.Fill(ds, nomeTabela);
            }
            catch
            {
                DataTable dt = new DataTable();
                dt.TableName = nomeTabela;
                ds.Tables.Add(dt);
            }

            if (ds.Tables[nomeTabela].Rows.Count > 0) { return ds; } else { return addLine(ref ds, nomeTabela); }
        }

        // Acrescenta uma linha a tabela
        private DataSet addLine(ref DataSet ds, object Tabela)
        {
            if (Tabela.GetType().Name == "String")
            {
                DataRow dr = ds.Tables[Tabela.ToString()].NewRow();

                if (ds.Tables[Tabela.ToString()].Columns.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[Tabela.ToString()].Columns.Count; i++) { dr[i] = DBNull.Value; }

                    ds.Tables[Tabela.ToString()].Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = ds.Tables[(int)Tabela].NewRow();

                if (ds.Tables[(int)Tabela].Columns.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[(int)Tabela].Columns.Count; i++) { dr[i] = DBNull.Value; }

                    ds.Tables[(int)Tabela].Rows.Add(dr);
                }
            }


            return ds;
        }
    }
}
