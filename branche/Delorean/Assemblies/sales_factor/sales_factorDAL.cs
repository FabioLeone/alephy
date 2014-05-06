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
    class sales_factorDAL
    {
        #region .:Searches:.
        internal static List<sales_factorTO> getAll()
        {
            NpgsqlConnection nc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            List<sales_factorTO> lst = new List<sales_factorTO>();
            StringBuilder sb = new StringBuilder(@"select f.id,case when f.barras is null then b.barras else f.barras end,b.produto,f.cond_cxs,f.margem_esperada,f.desconto,(f.desconto / f.cond_cxs)::numeric(12,2) as ""Demais cxs"" from fator_venda f
            right join base_especial b on f.barras = b.barras order by b.produto");

            NpgsqlCommand ncmm = nc.CreateCommand();
            ncmm.CommandText = sb.ToString();

            try
            {
                nc.Open();

                using (IDataReader drd = ncmm.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        lst.Add(Load(drd));
                    }
                }
            }
            finally
            {
                nc.Close();
            }

            return lst;
        }

        internal static List<sales_factorTO> getByFilter(string p1, string p2)
        {
            NpgsqlConnection nc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            List<sales_factorTO> lst = new List<sales_factorTO>();
            StringBuilder sb = new StringBuilder(@"select f.id,case when f.barras is null then b.barras else f.barras end,b.produto,f.cond_cxs,f.margem_esperada,f.desconto,(f.desconto / f.cond_cxs)::numeric(12,2) as ""Demais cxs"" from fator_venda f
            right join base_especial b on f.barras = b.barras");

            if (string.IsNullOrEmpty(p1)) p1 = string.Empty;

            if (p1.Equals("f"))
            {
                sb.Append(" where (cond_cxs > 0 or margem_esperada > 0 or desconto > 0)");

                if(!string.IsNullOrEmpty(p2))
                    sb.Append(" and lower(b.produto) like @nomeprod");
            }
            else if (p1.Equals("u"))
            {
                sb.Append(" where ((cond_cxs = 0 or cond_cxs is null) or (margem_esperada = 0 or margem_esperada is null) or (desconto = 0 or desconto is null))");
                
                if (!string.IsNullOrEmpty(p2))
                    sb.Append(" and lower(b.produto) like @nomeprod");
            }
            else if (!string.IsNullOrEmpty(p2))
                sb.Append(" where lower(b.produto) like @nomeprod");

            sb.Append(" order by b.produto");

            NpgsqlCommand ncmm = nc.CreateCommand();
            ncmm.CommandText = sb.ToString();
            ncmm.Parameters.Add("@nomeprod", NpgsqlDbType.Varchar).Value = string.Format("%{0}%", p2).ToLower();
            try
            {
                nc.Open();

                using (IDataReader drd = ncmm.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        lst.Add(Load(drd));
                    }
                }
            }
            finally
            {
                nc.Close();
            }

            return lst;
        }

        #endregion

        #region .:Loads:.
        private static sales_factorTO Load(IDataReader drd)
        {
            sales_factorTO o = new sales_factorTO();

            if (!drd.IsDBNull(drd.GetOrdinal("id"))) o.id = drd.GetInt32(drd.GetOrdinal("id")); else o.id = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("barras"))) o.barras = drd.GetString(drd.GetOrdinal("barras")); else o.barras = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("produto"))) o.prod_name = drd.GetString(drd.GetOrdinal("produto")); else o.prod_name = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("cond_cxs"))) o.cont_bxs = drd.GetInt32(drd.GetOrdinal("cond_cxs")); else o.cont_bxs = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("margem_esperada"))) o.expected_margin = drd.GetDecimal(drd.GetOrdinal("margem_esperada")); else o.expected_margin = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("desconto"))) o.discount = drd.GetDecimal(drd.GetOrdinal("desconto")); else o.discount = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("Demais cxs"))) o.other_bxs = drd.GetDecimal(drd.GetOrdinal("Demais cxs")); else o.other_bxs = 0;

            return o;
        }
        #endregion

        #region .:Persistences:.
        internal static bool insert(sales_factorTO o)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            bool bOk = false;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"INSERT INTO fator_venda(barras, cond_cxs, margem_esperada, desconto)
                VALUES (@barras, @cond_cxs, @margem_esperada, @desconto) RETURNING id;");

                NpgsqlCommand cmd = msc.CreateCommand();
                cmd.CommandText = strSQL.ToString();

                cmd.Parameters.Clear();
                cmd.Parameters.Add("@barras", NpgsqlDbType.Varchar).Value = o.barras;
                cmd.Parameters.Add("@cond_cxs", NpgsqlDbType.Integer).Value = o.cont_bxs;
                cmd.Parameters.Add("@margem_esperada", NpgsqlDbType.Money).Value = o.expected_margin;
                cmd.Parameters.Add("@desconto", NpgsqlDbType.Money).Value = o.discount;

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    if (drd.Read())
                    {
                        if (!drd.IsDBNull(drd.GetOrdinal("id"))) { o.id = drd.GetInt32(drd.GetOrdinal("id")); } else { o.id = 0; }
                    }
                }

                bOk = true;
            }
            catch {
                bOk = false;
            }
            finally
            {
                msc.Close();
            }

            return bOk;
        }

        internal static sales_factorTO upCond(int id, int dCond)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            sales_factorTO o = new sales_factorTO();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"UPDATE fator_venda SET cond_cxs=@cond_cxs WHERE id=@id;");
            strSQL.Append(@"select f.id,case when f.barras is null then b.barras else f.barras end,b.produto,f.cond_cxs,f.margem_esperada,f.desconto,(f.desconto / f.cond_cxs)::numeric(12,2) as ""Demais cxs"" from fator_venda f
            right join base_especial b on f.barras = b.barras WHERE f.id=@id;");

            NpgsqlCommand cmd = msc.CreateCommand();
            cmd.CommandText = strSQL.ToString();

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            cmd.Parameters.Add("@cond_cxs", NpgsqlTypes.NpgsqlDbType.Integer).Value = dCond;

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

        internal static sales_factorTO upMargin(int id, decimal dMarg)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            sales_factorTO o = new sales_factorTO();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"UPDATE fator_venda SET margem_esperada=@margem_esperada WHERE id=@id;");
            strSQL.Append(@"select f.id,case when f.barras is null then b.barras else f.barras end,b.produto,f.cond_cxs,f.margem_esperada,f.desconto,(f.desconto / f.cond_cxs)::numeric(12,2) as ""Demais cxs"" from fator_venda f
            right join base_especial b on f.barras = b.barras WHERE f.id=@id;");

            NpgsqlCommand cmd = msc.CreateCommand();
            cmd.CommandText = strSQL.ToString();

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            cmd.Parameters.Add("@margem_esperada", NpgsqlTypes.NpgsqlDbType.Money).Value = dMarg;

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

        internal static sales_factorTO upDesc(int id, decimal dDesc)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            sales_factorTO o = new sales_factorTO();

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append(@"UPDATE fator_venda SET desconto=@desconto WHERE id=@id;");
            strSQL.Append(@"select f.id,case when f.barras is null then b.barras else f.barras end,b.produto,f.cond_cxs,f.margem_esperada,f.desconto,(f.desconto / f.cond_cxs)::numeric(12,2) as ""Demais cxs"" from fator_venda f
            right join base_especial b on f.barras = b.barras WHERE f.id=@id;");

            NpgsqlCommand cmd = msc.CreateCommand();
            cmd.CommandText = strSQL.ToString();

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", NpgsqlTypes.NpgsqlDbType.Integer).Value = id;
            cmd.Parameters.Add("@desconto", NpgsqlTypes.NpgsqlDbType.Money).Value = dDesc;

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
        #endregion
    }
}
