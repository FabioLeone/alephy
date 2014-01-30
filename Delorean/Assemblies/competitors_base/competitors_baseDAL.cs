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
    class competitors_baseDAL
    {
        #region .:Searches:.
        internal static List<competitors_baseTO> getAll()
        {
            NpgsqlConnection nc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            List<competitors_baseTO> lst = new List<competitors_baseTO>();
            StringBuilder sb = new StringBuilder(@"select bc.id, b.barras, p.nomeprod, bc.rede, bc.valor_preco, bc.valor_desconto
            from base_especial b
	            left join produtos_base p on b.barras = p.codbarra
	            left join base_concorrente bc on b.barras = bc.barras
            where bc.rede is not null;");

            NpgsqlCommand ncm = nc.CreateCommand();
            ncm.CommandText = sb.ToString();

            try
            {
                nc.Open();

                using (IDataReader drd = ncm.ExecuteReader())
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
        private static competitors_baseTO Load(IDataReader drd)
        {
            competitors_baseTO o = new competitors_baseTO();

            if (!drd.IsDBNull(drd.GetOrdinal("id"))) o.id = drd.GetInt32(drd.GetOrdinal("id")); else o.id = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("barras"))) o.barras = drd.GetString(drd.GetOrdinal("barras")); else o.barras = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("nomeprod"))) o.nomeprod = drd.GetString(drd.GetOrdinal("nomeprod")); else o.nomeprod = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("rede"))) o.rede = drd.GetString(drd.GetOrdinal("rede")); else o.rede = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("valor_preco"))) o.valor_preco = drd.GetDecimal(drd.GetOrdinal("valor_preco")); else o.valor_preco = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("valor_desconto"))) o.valor_desconto = drd.GetDecimal(drd.GetOrdinal("valor_desconto")); else o.valor_desconto = 0;

            return o;
        }
        #endregion
    }
}
