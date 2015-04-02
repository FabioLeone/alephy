using System;
using SIAO.SRV.TO;
using Npgsql;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace SIAO.SRV.DAL
{
	public class UsersDAL
	{
		public UsersDAL ()
		{
		}

		#region .: Load :.
		private static UsersTO Load(IDataReader drdUsers)
		{
			UsersTO clsUsers = new UsersTO();

			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("id"))) { clsUsers.UserId = drdUsers.GetInt32(drdUsers.GetOrdinal("id")); } else { clsUsers.UserId = 0; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("username"))) { clsUsers.UserName = drdUsers.GetString(drdUsers.GetOrdinal("username")); } else { clsUsers.UserName = ""; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("lastactivitydate"))) { clsUsers.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("lastactivitydate")); } else { clsUsers.LastActivityDate = DateTime.Now; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("email"))) { clsUsers.Email = drdUsers.GetString(drdUsers.GetOrdinal("email")); } else { clsUsers.Email = ""; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("active"))) { clsUsers.Inactive = (drdUsers.GetInt32(drdUsers.GetOrdinal("active")) == 1 ? false : true); } else { clsUsers.Inactive = true; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("createdate"))) { clsUsers.CreateDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("createdate")); } else { clsUsers.CreateDate = DateTime.Now; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("expirationdate"))) { clsUsers.ExpirationDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("expirationdate")); } else { clsUsers.ExpirationDate = DateTime.Now; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Access"))) { clsUsers.Access = drdUsers.GetString(drdUsers.GetOrdinal("Access")); } else { clsUsers.Access = ""; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("name"))) { clsUsers.Name = drdUsers.GetString(drdUsers.GetOrdinal("name")); } else { clsUsers.Name = ""; }

			try
			{
				if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("store_id"))) { clsUsers.FarmaciaId = drdUsers.GetInt32(drdUsers.GetOrdinal("store_id")); } else { clsUsers.FarmaciaId = 0; }
			}
			catch
			{ 
				clsUsers.FarmaciaId = 0;
			}

			try
			{
				if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("usertype_id"))) { clsUsers.TipoId = drdUsers.GetInt32(drdUsers.GetOrdinal("usertype_id")); } else { clsUsers.TipoId = 0; }
			}
			catch 
			{
				clsUsers.TipoId = 0; 
			}

			try
			{
				if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Tipo"))) { clsUsers.Tipo = drdUsers.GetString(drdUsers.GetOrdinal("Tipo")); } else { clsUsers.Tipo = ""; }
			}
			catch
			{
				clsUsers.Tipo = ""; 
			}

			try
			{
				if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("userlevel"))) { clsUsers.Nivel = drdUsers.GetInt32(drdUsers.GetOrdinal("userlevel")); } else { clsUsers.Nivel = 0; }
			}
			catch
			{
				clsUsers.Nivel = 0;
			}

			return clsUsers;
		}
		#endregion

		#region .: Custom Search :.
		internal static UsersTO GetByNameAndPassword(string strName, string strPassword)
		{
			UsersTO clsUsers = new UsersTO();

			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				String strSQL = @"select u.id,u.name,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,m.username,s.store_id,m.usertype_id,m.userlevel
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_store_group s on u.id = s.user_id
					where lower(m.username) = lower(@Name) and m.password = @Password";

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();

				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", strName));
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Password", CDM.Cript(strPassword)));

				msc.Open();

				using (IDataReader drdUsers = cmdUsers.ExecuteReader())
				{
					if (drdUsers.Read())
					{
						clsUsers = Load(drdUsers);
					}
				}
			}
			finally
			{
				msc.Close();
			}

			return clsUsers;
		}
		#endregion

		#region .: Persistence :.

		internal static bool UpdateActivity(UsersTO clsUser)
		{
			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				String strSQL = "update t_memberships set lastactivitydate = @LastActivityDate where user_id = @UserId";

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();

				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUser.UserId));
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@LastActivityDate", clsUser.LastActivityDate));

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

		public static Boolean Delete(UsersTO clsUsers, string strConnection)
		{
			NpgsqlConnection msc = new NpgsqlConnection(strConnection);

			try
			{
				StringBuilder strSQL = new StringBuilder();

				DbCommand cmdUsers = msc.CreateCommand();

				strSQL.Clear();
				strSQL.Append("delete from t_memberships where user_id = @UserId");

				cmdUsers.CommandText = strSQL.ToString();
				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));

				msc.Open();

				cmdUsers.ExecuteNonQuery();

				strSQL.Clear();
				strSQL.Append("delete from t_users where id = @UserId");

				cmdUsers.CommandText = strSQL.ToString();
				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
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
		#endregion
	}
}

