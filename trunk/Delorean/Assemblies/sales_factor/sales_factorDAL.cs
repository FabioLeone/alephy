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
            StringBuilder sb = new StringBuilder(@"select id,barras,cond_cxs,margem_esperada,desconto,(desconto / cond_cxs) as ""Demais cxs"" from fator_venda");

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

        #endregion

        #region .:Loads:.
        private static sales_factorTO Load(IDataReader drd)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
