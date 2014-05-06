using Npgsql;
using NpgsqlTypes;
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
                var sql = "copy base_compra (farmaciaid, barras, valor_custo, fabricante, data) from stdin with delimiter '|'";

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
                var sql = "copy base_compra (farmaciaid, barras, valor_custo, fabricante, data) from stdin with delimiter '|'";

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

        internal static base_viewer insert(base_viewer o, int id)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"INSERT INTO base_compra(farmaciaid, barras, valor_custo, data)
                VALUES (@farmaciaid, @barras, @valor_custo, @data) RETURNING id;");

                NpgsqlCommand cmd = msc.CreateCommand();
                cmd.CommandText = strSQL.ToString();

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@farmaciaid", NpgsqlDbType.Integer).Value = id;
                cmd.Parameters.Add("@barras", NpgsqlDbType.Varchar).Value = o.barras;
                cmd.Parameters.Add("@valor_custo", NpgsqlDbType.Money).Value = o.valor_custo;
                cmd.Parameters.Add("@data", NpgsqlDbType.Date).Value = DateTime.Now.Date;

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    if (drd.Read())
                    {
                        if (!drd.IsDBNull(drd.GetOrdinal("id"))) { o.bcid = drd.GetInt32(drd.GetOrdinal("id")); } else { o.bcid = 0; }
                    }
                }

                strSQL.Clear();
                strSQL.Append(@"select bc.id, b.barras, b.produto, bc.valor_custo, (bc.valor_custo / (1 - f.margem_esperada))::numeric(12,2) as ""1 Unidade"",
	                ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto))::numeric(12,2) as ""Acima de X"", 
	                ((((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)) - bc.valor_custo) / ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)))::numeric(12,2) as ""Margem Real""
                from base_especial b
	                left join base_compra bc on b.barras = bc.barras
	                left join fator_venda f on b.barras = f.barras
                where bc.id = @id;");

                cmd.CommandText = strSQL.ToString();

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = o.bcid;

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    if (drd.Read())
                    {
                        o = Load(drd);
                    }
                }

            }
            finally
            {
                msc.Close();
            }

            return o;
        }

        internal static base_viewer upCost(int id, decimal dCost)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            base_viewer o = new base_viewer();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"UPDATE base_compra SET valor_custo=@valor_custo WHERE id=@id;");
            strSQL.Append(@"select bc.id, b.barras, b.produto, bc.valor_custo, (bc.valor_custo / (1 - f.margem_esperada))::numeric(12,2) as ""1 Unidade"",
	            ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto))::numeric(12,2) as ""Acima de X"", 
	            ((((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)) - bc.valor_custo) / ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)))::numeric(12,2) as ""Margem Real""
            from base_especial b
	            left join base_compra bc on b.barras = bc.barras
	            left join fator_venda f on b.barras = f.barras
            where bc.id = @id;");

            NpgsqlCommand cmd = msc.CreateCommand();
            cmd.CommandText = strSQL.ToString();

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            cmd.Parameters.Add("@valor_custo", NpgsqlTypes.NpgsqlDbType.Money).Value = dCost;

            try
            {
                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    if (drd.Read())
                    {
                        o = Load(drd);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return o;
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
                strSQL.Append(@"DELETE FROM base_compra WHERE farmaciaid = (select id from farmacias where cnpj = @cnpj);");

                cmd.CommandText = strSQL.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@cnpj", NpgsqlTypes.NpgsqlDbType.Varchar).Value = auxDt.Rows[0][0];
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

        #region .:Loads:.
        private static base_viewer Load(IDataReader drd)
        {
            base_viewer o = new base_viewer();

            if (!drd.IsDBNull(drd.GetOrdinal("id"))) o.bcid = drd.GetInt32(drd.GetOrdinal("id")); else o.bcid = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("barras"))) o.barras = drd.GetString(drd.GetOrdinal("barras")); else o.barras = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("produto"))) o.nomeprod = drd.GetString(drd.GetOrdinal("produto")); else o.nomeprod = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("valor_custo"))) o.valor_custo = drd.GetDecimal(drd.GetOrdinal("valor_custo")); else o.valor_custo = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("1 Unidade"))) o.one_unit = drd.GetDecimal(drd.GetOrdinal("1 Unidade")); else o.one_unit = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("Acima de X"))) o.upx = drd.GetDecimal(drd.GetOrdinal("Acima de X")); else o.upx = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("Margem Real"))) o.actual_margin = drd.GetDecimal(drd.GetOrdinal("Margem Real")); else o.actual_margin = 0;

            return o;
        }
        #endregion
    }
}
