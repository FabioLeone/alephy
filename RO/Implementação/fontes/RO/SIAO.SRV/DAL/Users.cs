using System;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Configuration;

namespace SIAO.SRV.DAL
{
    public class UsersDAL
    {
        #region .: Load :.

        private static UsersTO Load(IDataReader drdUsers)
        {
            UsersTO clsUsers = new UsersTO();

            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserId"))) { clsUsers.UserId = drdUsers.GetInt32(drdUsers.GetOrdinal("UserId")); } else { clsUsers.UserId = 0; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserName"))) { clsUsers.UserName = drdUsers.GetString(drdUsers.GetOrdinal("UserName")); } else { clsUsers.UserName = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("LastActivityDate"))) { clsUsers.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("LastActivityDate")); } else { clsUsers.LastActivityDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Password"))) { clsUsers.Password = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Password"))); } else { clsUsers.Password = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Email"))) { clsUsers.Email = drdUsers.GetString(drdUsers.GetOrdinal("Email")); } else { clsUsers.Email = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Inactive"))) { clsUsers.Inactive = (drdUsers.GetInt32(drdUsers.GetOrdinal("Inactive")) == 1 ? true : false); } else { clsUsers.Inactive = true; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("CreateDate"))) { clsUsers.CreateDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("CreateDate")); } else { clsUsers.CreateDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("ExpirationDate"))) { clsUsers.ExpirationDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("ExpirationDate")); } else { clsUsers.ExpirationDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Access"))) { clsUsers.Access = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Access"))); } else { clsUsers.Access = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Name"))) { clsUsers.Name = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Name"))); } else { clsUsers.Name = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("FarmaciaId"))) { clsUsers.FarmaciaId = drdUsers.GetInt32(drdUsers.GetOrdinal("FarmaciaId")); } else { clsUsers.FarmaciaId = 0; }

            try
            {
                if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("TipoId"))) { clsUsers.TipoId = drdUsers.GetInt32(drdUsers.GetOrdinal("TipoId")); } else { clsUsers.TipoId = 0; }
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

            return clsUsers;
        }

        private static UsersTO LoadII(IDataReader drdUsers)
        {
            UsersTO clsUsers = new UsersTO();

            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserId"))) { clsUsers.UserId = drdUsers.GetInt32(drdUsers.GetOrdinal("UserId")); } else { clsUsers.UserId = 0; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserName"))) { clsUsers.UserName = drdUsers.GetString(drdUsers.GetOrdinal("UserName")); } else { clsUsers.UserName = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("LastActivityDate"))) { clsUsers.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("LastActivityDate")); } else { clsUsers.LastActivityDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Password"))) { clsUsers.Password = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Password"))); } else { clsUsers.Password = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Email"))) { clsUsers.Email = drdUsers.GetString(drdUsers.GetOrdinal("Email")); } else { clsUsers.Email = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Inactive"))) { clsUsers.Inactive = (drdUsers.GetInt32(drdUsers.GetOrdinal("Inactive")) == 1 ? true : false); } else { clsUsers.Inactive = true; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("CreateDate"))) { clsUsers.CreateDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("CreateDate")); } else { clsUsers.CreateDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("ExpirationDate"))) { clsUsers.ExpirationDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("ExpirationDate")); } else { clsUsers.ExpirationDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Access"))) { clsUsers.Access = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Access"))); } else { clsUsers.Access = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Name"))) { clsUsers.Name = CDM.Desc(drdUsers.GetString(drdUsers.GetOrdinal("Name"))); } else { clsUsers.Name = ""; }

            return clsUsers;
        }
        private static Usuarios_TiposTO LoadTipos(IDataReader drdTipos)
        {
            Usuarios_TiposTO clsTipo = new Usuarios_TiposTO();

            if (!drdTipos.IsDBNull(drdTipos.GetOrdinal("id"))) { clsTipo.id = drdTipos.GetInt32(drdTipos.GetOrdinal("id")); } else { clsTipo.id = 0; }
            if (!drdTipos.IsDBNull(drdTipos.GetOrdinal("Tipo"))) { clsTipo.Tipo = drdTipos.GetString(drdTipos.GetOrdinal("Tipo")); } else { clsTipo.Tipo = ""; }

            return clsTipo;
        }

        #endregion

        #region .: Search :.

        public static List<UsersTO> GetAll(string strConnection)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId,users.TipoId,usuarios_tipos.Tipo
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN 
                usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId LEFT JOIN
                usuarios_tipos ON users.TipoId = usuarios_tipos.id
                ORDER BY users.UserName");

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

        public static UsersTO GetById(int intUserId, string strConnection)
        {
            UsersTO clsUsers = new UsersTO();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId,users.TipoId,usuarios_tipos.Tipo
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId LEFT JOIN
                usuarios_tipos ON users.TipoId = usuarios_tipos.id
                WHERE users.UserId=@UserId");

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

        internal static UsersTO GetByNameAndPassword(string strName, string strPassword, string strConnectionString)
        {
            UsersTO clsUsers = new UsersTO();

            MySqlConnection msc = new MySqlConnection(strConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId,users.TipoId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE memberships.`Name`=@Name AND memberships.`Password`=@Password");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", CDM.Cript(strName.ToUpper())));
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

        public static object GetByName(string strNome, string strConnection)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE users.UserName LIKE @UserName");
                strSQL.Append(" ORDER BY users.UserName");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@UserName", string.Format("%{0}%", strNome)));

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

        internal static List<UsersTO> GetByAccessType(string strAccess, string strConnection)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE memberships.Access=@Access");
                strSQL.Append(" ORDER BY users.UserName");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();

                switch (strAccess)
                {
                    case "gerente":
                        cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Access", CDM.Cript("nvg")));
                        break;
                    case "proprietario":
                        cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Access", CDM.Cript("nvp")));
                        break;
                    case "administrador":
                        cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Access", CDM.Cript("adm")));
                        break;
                }
                
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

        internal static List<UsersTO> GetIndicesByFiltro(string strNome, string strConnection)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, usuarios_farmacias.FarmaciaId,users.TipoId,usuarios_tipos.Tipo
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN 
                usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId LEFT JOIN
                usuarios_tipos ON users.TipoId = usuarios_tipos.id
                WHERE users.UserName LIKE @Name
                ORDER BY users.UserName");

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

        internal static List<UsersTO> GetByRedeId(int intRedeId, string strConnection)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId,users.UserName,users.LastActivityDate,memberships.`Password`,memberships.Email,memberships.Inactive,memberships.CreateDate,memberships.ExpirationDate,memberships.Access,memberships.`Name` FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN farmacias ON farmacias.ProprietarioID = users.UserId ");
                if (intRedeId > 0)
                    strSQL.Append(" WHERE farmacias.idRede = @idRede");
                else
                    strSQL.Append(String.Format(" WHERE (farmacias.idRede = 0 Or farmacias.idRede IS NULL) AND memberships.Access <> '{0}'", CDM.Cript("adm")));
                strSQL.Append(" GROUP BY users.UserId,users.UserName,users.LastActivityDate,memberships.`Password`,memberships.Email,memberships.Inactive,memberships.CreateDate,memberships.ExpirationDate,memberships.Access,memberships.`Name`");
                
                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@idRede", intRedeId));

                msc.Open();

                using (IDataReader drdUsers = cmdUsers.ExecuteReader())
                {
                    while (drdUsers.Read())
                    {
                        clsUsers.Add(LoadII(drdUsers));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsUsers.OrderBy(u=>u.Name).ToList();
        }

        internal static List<Usuarios_TiposTO> GetTiposAll()
        {
            List<Usuarios_TiposTO> lstTipos = new List<Usuarios_TiposTO>();

            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT usuarios_tipos.id,usuarios_tipos.Tipo FROM usuarios_tipos");
                
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

        #endregion

        #region .: Persistence :.

        public static UsersTO Insert(UsersTO clsUsers, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("INSERT INTO users (UserName, LastActivityDate) VALUES (@UserName, @LastActivityDate);");
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE users.UserId=@@IDENTITY");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@UserName", clsUsers.UserName));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@LastActivityDate", clsUsers.LastActivityDate));

                msc.Open();

                using (IDataReader drdUsers = cmdUsers.ExecuteReader())
                {
                    if (drdUsers.Read())
                    {
                        clsUsers.UserId = Load(drdUsers).UserId;
                    }
                }

                strSQL.Clear();
                strSQL.Append("INSERT INTO memberships (UserId, Password, Email, Inactive, CreateDate, ");
                strSQL.Append(" ExpirationDate, Access, Name) VALUES ( @UserId, @Password, @Email, @Inactive,");
                strSQL.Append(" @CreateDate, @ExpirationDate, @Access, @Name)");

                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Password", CDM.Cript(clsUsers.Password)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Email", clsUsers.Email));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@Inactive", (clsUsers.Inactive == false ? 0 : 1)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@CreateDate", clsUsers.CreateDate));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@ExpirationDate", clsUsers.ExpirationDate));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Access", CDM.Cript(clsUsers.Access)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", CDM.Cript(clsUsers.Name)));
                cmdUsers.ExecuteNonQuery();

                strSQL.Clear();
                strSQL.Append("INSERT INTO usuarios_farmacias (UserId, FarmaciaId) VALUES ( @UserId, @FarmaciaId)");

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@FarmaciaId", clsUsers.FarmaciaId));
                cmdUsers.ExecuteNonQuery();

            }
            finally
            {
                msc.Close();
            }

            return clsUsers;
        }

        public static Boolean Update(UsersTO clsUsers, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("UPDATE users SET UserName = @UserName, LastActivityDate = @LastActivityDate");
                strSQL.Append(" WHERE users.UserId=@UserId");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@UserName", clsUsers.UserName));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@LastActivityDate", clsUsers.LastActivityDate));

                msc.Open();

                cmdUsers.ExecuteNonQuery();

                strSQL.Clear();
                strSQL.Append("UPDATE memberships SET Password = @Password, Email = @Email, ");
                strSQL.Append(" Inactive = @Inactive, CreateDate = @CreateDate, ExpirationDate = @ExpirationDate,");
                strSQL.Append(" Access = @Access, Name = @Name WHERE UserId = @UserId");

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Password", CDM.Cript(clsUsers.Password)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Email", clsUsers.Email));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@Inactive", (clsUsers.Inactive == false ? 0 : 1)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@CreateDate", clsUsers.CreateDate));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.DateTime, "@ExpirationDate", clsUsers.ExpirationDate));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Access", CDM.Cript(clsUsers.Access)));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", CDM.Cript(clsUsers.Name)));
                cmdUsers.ExecuteNonQuery();

                strSQL.Clear();
                strSQL.Append("UPDATE usuarios_farmacias SET FarmaciaId = @FarmaciaId WHERE UserId = @UserId");

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@FarmaciaId", clsUsers.FarmaciaId));
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
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM usuarios_farmacias WHERE UserId = @UserId");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));

                msc.Open();

                cmdUsers.ExecuteNonQuery();

                strSQL.Clear();
                strSQL.Append("DELETE FROM memberships WHERE UserId = @UserId");

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));
                cmdUsers.ExecuteNonQuery();

                strSQL.Clear();
                strSQL.Append("DELETE FROM users WHERE users.UserId=@UserId");

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
