using System;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Data.Common;
using System.Data;
using Npgsql;
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

            try
            {
                if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("nivel"))) { clsUsers.Nivel = drdUsers.GetInt32(drdUsers.GetOrdinal("nivel")); } else { clsUsers.Nivel = 0; }
            }
            catch
            {
                clsUsers.Nivel = 0;
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

        public static List<UsersTO> GetAll()
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            DbConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, uv.FarmaciaId,users.TipoId,usuarios_tipos.Tipo
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN 
                usuarios_vinculos uv ON users.UserId = uv.UsuarioId LEFT JOIN
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

        public static UsersTO GetById(int intUserId)
        {
            UsersTO clsUsers = new UsersTO();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT u.UserId, u.UserName, u.LastActivityDate, m.Password,m.Email, m.Inactive, 
                m.CreateDate, m.ExpirationDate,m.Access, m.Name, uv.FarmaciaId,u.TipoId,ut.Tipo,u.nivel
                FROM users u LEFT JOIN memberships m ON u.UserId = m.UserId 
                LEFT JOIN usuarios_vinculos uv ON u.UserId = uv.UsuarioId LEFT JOIN
                usuarios_tipos ut ON u.TipoId = ut.id WHERE u.UserId=@UserId");

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT u.UserId, u.UserName, u.LastActivityDate, m.Password,
                m.Email, m.Inactive, m.CreateDate, m.ExpirationDate,
                m.Access, m.Name, uv.FarmaciaId,u.TipoId,u.nivel
                FROM users u 
                LEFT JOIN memberships m ON u.UserId = m.UserId
                LEFT JOIN usuarios_vinculos uv ON u.UserId = uv.usuarioid");
                strSQL.Append(" WHERE m.Name=@Name AND m.Password=@Password");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                Console.WriteLine(CDM.Cript(strName.ToUpper()));
                Console.WriteLine(CDM.Cript(strName.ToUpper()));
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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, usuarios_vinculos.FarmaciaId
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId 
                LEFT JOIN usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId
                WHERE users.UserName LIKE @UserName
                ORDER BY users.UserName");

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, usuarios_vinculos.FarmaciaId
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId 
                LEFT JOIN usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId");
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

        internal static List<UsersTO> GetIndicesByFiltro(string strNome, UsersTO objUser)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT u.UserId, u.UserName, u.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, uv.FarmaciaId,u.TipoId,usuarios_tipos.Tipo
                FROM users u LEFT JOIN memberships ON u.UserId = memberships.UserId LEFT JOIN 
                usuarios_tipos ON u.TipoId = usuarios_tipos.id
                LEFT JOIN usuarios_vinculos uv ON u.userid = uv.usuarioid
                LEFT JOIN farmacias f ON f.id = uv.farmaciaid
                WHERE  u.TipoId <> 1 AND UPPER(u.UserName) LIKE @Name");

                if (objUser.Nivel.Equals(1))
                    strSQL.Append(@" AND (uv.redeid = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
                    OR f.idrede = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");
                else if (objUser.Nivel.Equals(2))
                    strSQL.Append(@" AND (uv.farmaciaid = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
					OR f.id = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");

                strSQL.Append(" ORDER BY u.UserName");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", string.Format("%{0}%", strNome.ToUpper())));
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

        internal static List<UsersTO> GetIndicesByFiltro(string strNome)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, usuarios_vinculos.FarmaciaId,users.TipoId,usuarios_tipos.Tipo
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN 
                usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId LEFT JOIN
                usuarios_tipos ON users.TipoId = usuarios_tipos.id
                WHERE UPPER(users.UserName) LIKE @Name
                ORDER BY users.UserName");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.String, "@Name", string.Format("%{0}%", strNome.ToUpper())));

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

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId,users.UserName,users.LastActivityDate,memberships.`Password`,memberships.Email,memberships.Inactive,memberships.CreateDate,memberships.ExpirationDate,memberships.Access,memberships.`Name` FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_vinculos u ON u.usuarioid = users.UserId ");
                if (intRedeId > 0)
                    strSQL.Append(" WHERE u.redeid = @idRede");
                else
                    strSQL.Append(String.Format(" WHERE (u.Redeid = 0 Or u.Redeid IS NULL) AND memberships.Access <> '{0}'", CDM.Cript("adm")));
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

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

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

        internal static List<UsersTO> GetAllMinion(UsersTO owner)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            DbConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT u.UserId, u.UserName, u.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, uv.FarmaciaId,u.TipoId,usuarios_tipos.Tipo
                FROM users u LEFT JOIN memberships ON u.UserId = memberships.UserId LEFT JOIN 
                usuarios_tipos ON u.TipoId = usuarios_tipos.id
                LEFT JOIN usuarios_vinculos uv ON u.userid = uv.usuarioid
                LEFT JOIN farmacias f ON f.id = uv.farmaciaid
                WHERE  u.TipoId <> 1");

                if (owner.Nivel.Equals(1))
                    strSQL.Append(@" AND (uv.redeid = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
                    OR f.idrede = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");
                else if (owner.Nivel.Equals(2))
                    strSQL.Append(@" AND (uv.farmaciaid = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
					OR f.id = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");

                strSQL.Append(" ORDER BY u.UserName");

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

        public static UsersTO Insert(UsersTO clsUsers, string strConnection)
        {
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("INSERT INTO users (UserName, LastActivityDate) VALUES (@UserName, @LastActivityDate);");
                strSQL.Append(@"SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.Password,
                memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,
                memberships.Access, memberships.Name, usuarios_vinculos.FarmaciaId
                FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId 
                LEFT JOIN usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId
                WHERE users.UserId=@@IDENTITY");

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
            }
            finally
            {
                msc.Close();
            }

            return clsUsers;
        }

        public static Boolean Update(UsersTO clsUsers, string strConnection)
        {
            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

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
                strSQL.Append("DELETE FROM memberships WHERE UserId = @UserId");

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsUsers.UserId));

                msc.Open();
                
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
