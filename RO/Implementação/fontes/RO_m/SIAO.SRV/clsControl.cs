using System;
using System.Xml;
using System.Data;
using Npgsql;
using NpgsqlTypes;
using System.Configuration;

namespace SIAO.SRV
{
	public class clsControl
	{
		public clsControl ()
		{
		}

		#region .:Search:.

		public static DataSet GetRedes()
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			clsDB clsDB = new clsDB();

			cmm.Connection = cnn;

			cmm.CommandText = "select id,name from t_networks order by name";

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Redes");
			}
			clsDB.closeConnection(cmm);

			return ds;
		}

		public static Rede GetRedeByUserId(int intUserId)
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Rede r = new Rede();

			cmm.Connection = cnn;
			cmm.CommandText = @"select n.id,n.name,s.user_id
				from t_networks n 
					inner join t_store_group s on n.id = s.network_id
				where s.user_id = @usuarioId";

			cmm.Parameters.Add("@usuarioId", NpgsqlDbType.Integer).Value = intUserId;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
				{
					r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["Id"].ToString());
					r.RedeName = ds.Tables[0].Rows[0]["name"].ToString();
					r.UserId = Convert.ToInt16(ds.Tables[0].Rows[0]["user_id"].ToString() == "" ? 0 : ds.Tables[0].Rows[0]["user_id"]);
				}
			}

			return r;
		}

		internal static Rede GetRedeByLojaId(int loja_id)
		{
			DataSet ds = new DataSet();
			NpgsqlCommand cmm = new NpgsqlCommand();
			NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
			Rede r = new Rede();

			cmm.Connection = cnn;
			cmm.CommandText = @"select n.id,n.name
				from t_networks n
					inner join t_stores s on n.id = s.network_id
				where s.id = @loja_id
				group by n.id
				order by n.name";

			cmm.Parameters.Add("@loja_id", NpgsqlDbType.Integer).Value = loja_id;

			if (clsDB.openConnection(cmm))
			{
				ds = clsDB.QueryDS(ref cmm, ref ds, "Rede");
			}
			clsDB.closeConnection(cmm);

			if (ds.Tables.Count > 0)
			{
				if (ds.Tables[0].Rows[0]["Id"] != DBNull.Value)
				{
					r.RedeId = Convert.ToInt16(ds.Tables[0].Rows[0]["id"].ToString());
					r.RedeName = ds.Tables[0].Rows[0]["name"].ToString();
				}
			}

			return r;
		}

		#endregion
	}
}

