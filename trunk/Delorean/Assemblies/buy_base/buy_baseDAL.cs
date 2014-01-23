using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    class buy_baseDAL
    {
        #region .:Persistence:.
        internal static string addXml(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            NpgsqlCommand cmm;
            string msg = "";

            if (dt.Rows.Count > 0)
            {
                var sql = "copy base_compra (farmaciaid, barras, valor_custo, data) from stdin with delimiter '|'";

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
            }
            else { msg = "Erro ao converter o xml."; }

            return msg;
        }

        internal static string addTxt(DataTable dt)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;

            string msg = "";

            if (dt.Rows.Count > 0)
            {
                var sql = "copy base_compra (farmaciaid, barras, valor_custo, data) from stdin with delimiter '|'";

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
            }
            else { msg = "Erro ao converter o txt."; }

            return msg;
        }

        internal static void removeDuplicate(DataTable dt)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            StringBuilder strSQL = new StringBuilder();
            DataTable auxDt = new DataTable();

            auxDt = dt.DefaultView.ToTable(true, "cnpj");

            NpgsqlCommand cmd = msc.CreateCommand();
                
            if (auxDt.Rows.Count > 0)
            {
                strSQL = new StringBuilder();
                strSQL.Append(string.Format(@"DELETE FROM base_compra WHERE farmaciaid = @farmaciaid 
                and to_char(data, 'MM yyyy') = '{0}'", DateTime.Now.ToString("MM yyyy")));

                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@farmaciaid", NpgsqlTypes.NpgsqlDbType.Integer).Value = auxDt.Rows[0][0];
            }

            try
            {
                msc.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }
        #endregion

        #region .:Methods:.
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
                    sb.Append(d.ToString().Replace(".", "").Replace(",", "."));
                }
                sb.Append("|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }

        private static object SerializeXmlData(object[] data)
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
                    sb.Append(d.ToString());
                }
                sb.Append("|");
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
        }
        #endregion

    }
}
