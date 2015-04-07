using System;
using SIAO.SRV.TO;
using Npgsql;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

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

			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("id"))) { clsUsers.UserId = (Int32)drdUsers.GetValue(drdUsers.GetOrdinal("id")); } else { clsUsers.UserId = 0; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("username"))) { clsUsers.UserName = drdUsers.GetString(drdUsers.GetOrdinal("username")); } else { clsUsers.UserName = ""; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("lastactivitydate"))) { clsUsers.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("lastactivitydate")); } else { clsUsers.LastActivityDate = DateTime.Now; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("email"))) { clsUsers.Email = drdUsers.GetString(drdUsers.GetOrdinal("email")); } else { clsUsers.Email = ""; }
			if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("active"))) { clsUsers.Inactive = (drdUsers.GetBoolean(drdUsers.GetOrdinal("active")) == true ? false : true); } else { clsUsers.Inactive = true; }
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

		private static Usuarios_TiposTO LoadTipos(IDataReader drdTipos)
		{
			Usuarios_TiposTO clsTipo = new Usuarios_TiposTO();

			if (!drdTipos.IsDBNull(drdTipos.GetOrdinal("id"))) { clsTipo.id = drdTipos.GetInt32(drdTipos.GetOrdinal("id")); } else { clsTipo.id = 0; }
			if (!drdTipos.IsDBNull(drdTipos.GetOrdinal("type"))) { clsTipo.Tipo = drdTipos.GetString(drdTipos.GetOrdinal("type")); } else { clsTipo.Tipo = ""; }

			return clsTipo;
		}
		#endregion

		#region .: Search :.

		public static UsersTO GetById(int intUserId)
		{
			UsersTO clsUsers = new UsersTO();

			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append(@"select u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,u.name,m.usertype_id,ut.type
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_user_types ut on m.usertype_id = ut.id
					where u.id = @UserId");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();

				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", intUserId));
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

		internal static List<Usuarios_TiposTO> GetTiposAll()
		{
			List<Usuarios_TiposTO> lstTipos = new List<Usuarios_TiposTO>();

			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append("select id,type from t_user_types");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();

				msc.Open();

				using (IDataReader drdTipos = cmdUsers.ExecuteReader())
				{
					while (drdTipos.Read())
					{
						lstTipos.Add(LoadTipos(drdTipos));
					}
				}
			}
			finally
			{
				msc.Close();
			}

			return lstTipos;
		}

		internal static List<UsersTO> GetIndicesByFiltro(string strNome)
		{
			List<UsersTO> clsUsers = new List<UsersTO>();

			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append(@"select u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,u.name,m.usertype_id,ut.type
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_user_types ut on m.usertype_id = ut.id
					where lower(u.name) like lower(@Name)
					order by u.name");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();
				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", string.Format("%{0}%", strNome)));

				msc.Open();

				using (IDataReader drdUsers = cmdUsers.ExecuteReader())
				{
					while (drdUsers.Read())
					{
						clsUsers.Add(Load(drdUsers));
					}
				}
			}
			finally
			{
				msc.Close();
			}

			return clsUsers;
		}

		internal static List<UsersTO> GetIndicesByFiltro(string strNome, UsersTO objUser)
		{
			List<UsersTO> clsUsers = new List<UsersTO>();

			NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append(@"select u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,u.name,m.usertype_id,ut.type
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_user_types ut on m.usertype_id = ut.id
						left join t_store_group s on u.id = s.user_id
						left join t_stores st on s.store_id = st.id
					where lower(u.name) like lower(@Name) and m.usertype_id <> 1");

				if (objUser.Nivel.Equals(1))
					strSQL.Append(@" and (s.network_id = (select network_id from t_store_group where user_id = @usuarioid) or st.network_id = (select network_id from t_store_group 
						where user_id = @usuarioid) or u.owner = @usuarioid)");
				else if (objUser.Nivel.Equals(2))
					strSQL.Append(@" and (s.store_id = (select store_id from t_store_group where user_id = @usuarioid) or st.id = (select store_id from t_store_group 
						where user_id = @usuarioid) or u.owner = @usuarioid)");

				strSQL.Append(" order by u.name");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();
				cmdUsers.Parameters.Clear();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", string.Format("%{0}%", strNome)));
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@usuarioid", objUser.UserId));

				msc.Open();

				using (IDataReader drdUsers = cmdUsers.ExecuteReader())
				{
					while (drdUsers.Read())
					{
						clsUsers.Add(Load(drdUsers));
					}
				}
			}
			finally
			{
				msc.Close();
			}

			return clsUsers;
		}

		internal static List<UsersTO> GetLst()
		{
			List<UsersTO> clsUsers = new List<UsersTO>();

			DbConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append(@"select u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,u.name,m.usertype_id,ut.type
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_user_types ut on m.usertype_id = ut.id
					order by u.name");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();

				msc.Open();

				using (IDataReader drdUsers = cmdUsers.ExecuteReader())
				{
					while (drdUsers.Read())
					{
						clsUsers.Add(Load(drdUsers));
					}
				}
			}
			finally
			{
				msc.Close();
			}

			return clsUsers;
		}

		internal static List<UsersTO> GetAllMinion(UsersTO owner)
		{
			List<UsersTO> clsUsers = new List<UsersTO>();

			DbConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

			try
			{
				StringBuilder strSQL = new StringBuilder();
				strSQL.Append(@"select u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,case when m.usertype_id = 1 then 'adm' else
						case when m.usertype_id = 2 then 'nvp' else case when m.usertype_id = 3 then 'nvg' else '' end end end as Access,u.name,m.usertype_id,ut.type
					from t_users u
						left join t_memberships m on u.id = m.user_id
						left join t_user_types ut on m.usertype_id = ut.id
						left join t_store_group s on u.id = s.user_id
						left join t_stores st on s.store_id = st.id
					where m.usertype_id <> 1");

				if (owner.Nivel.Equals(1))
					strSQL.Append(@" and (s.network_id = (select network_id from t_store_group where user_id = @usuarioid) or st.network_id = (select network_id from t_store_group where user_id = @usuarioid)
					or u.owner = @usuarioid)");
				else if (owner.Nivel.Equals(2))
					strSQL.Append(@" and (s.store_id = (select store_id from t_store_group where user_id = @usuarioid) or st.id = (select store_id from t_store_group where user_id = @usuarioid)
					or u.owner = @usuarioid)");

				strSQL.Append(@" group by u.id,u.username,m.lastactivitydate,m.password,m.email,m.active,m.createdate,m.expirationdate,Access,u.name,m.usertype_id,ut.type
					order by u.name");

				DbCommand cmdUsers = msc.CreateCommand();
				cmdUsers.CommandText = strSQL.ToString();
				cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@usuarioid", owner.UserId));

				msc.Open();

				using (IDataReader drdUsers = cmdUsers.ExecuteReader())
				{
					while (drdUsers.Read())
					{
						clsUsers.Add(Load(drdUsers));
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

