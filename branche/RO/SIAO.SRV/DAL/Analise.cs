using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using Npgsql;
using System.Configuration;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    internal class AnaliseDAL
    {
        #region .: Load :.

        private static AnaliseTO Load(IDataReader drdAnalise)
        {
            AnaliseTO clsAnalise = new AnaliseTO();

            if (!drdAnalise.IsDBNull(drdAnalise.GetOrdinal("id"))) clsAnalise.id = drdAnalise.GetInt32(drdAnalise.GetOrdinal("id")); else clsAnalise.id = 0;
            if (!drdAnalise.IsDBNull(drdAnalise.GetOrdinal("reference"))) clsAnalise.reference = drdAnalise.GetDecimal(drdAnalise.GetOrdinal("reference")); else clsAnalise.reference = 0;
            if (!drdAnalise.IsDBNull(drdAnalise.GetOrdinal("text"))) clsAnalise.text = drdAnalise.GetString(drdAnalise.GetOrdinal("text")); else clsAnalise.text = String.Empty;
            if (!drdAnalise.IsDBNull(drdAnalise.GetOrdinal("itemid"))) clsAnalise.itemid = drdAnalise.GetInt32(drdAnalise.GetOrdinal("itemid")); else clsAnalise.itemid = 0;

            return clsAnalise;
        }

        private static AnaliseItemTO LoadItem(IDataReader drdItem)
        {
            AnaliseItemTO clsItem = new AnaliseItemTO();

            if (!drdItem.IsDBNull(drdItem.GetOrdinal("id"))) clsItem.id = drdItem.GetInt32(drdItem.GetOrdinal("id")); else clsItem.id = 0;
            if (!drdItem.IsDBNull(drdItem.GetOrdinal("item"))) clsItem.item = drdItem.GetString(drdItem.GetOrdinal("item")); else clsItem.item = String.Empty;

            return clsItem;
        }
        #endregion

        #region .: Search :.
        internal static List<AnaliseTO> GetAnaliseAll()
        {
            List<AnaliseTO> clsAnalise = new List<AnaliseTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT analise.id, analise.reference, analise.text, analise.itemid FROM analise;");

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        clsAnalise.Add(Load(drd));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsAnalise;
        }

        internal static List<AnaliseItemTO> GetItemAll()
        {
            List<AnaliseItemTO> clsItem = new List<AnaliseItemTO>();
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT \"analiseItens\".id, \"analiseItens\".item FROM \"analiseItens\";");

                DbCommand cmd = msc.CreateCommand();

                cmd.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drd = cmd.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        clsItem.Add(LoadItem(drd));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsItem;
        }
        #endregion
    }
}
