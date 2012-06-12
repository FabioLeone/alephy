using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIAO.SRV.TO;
using System.Data.Common;
using System.Data;

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

            return clsUsers;
        }

        #endregion

        #region .: Search :.

        public static List<UsersTO> GetAll()
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" ORDER BY users.UserName");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                using (IDataReader drdUsers = db.ExecuteReader(cmdUsers)) {
                    while (drdUsers.Read())
                    {
                        clsUsers.Add(Load(drdUsers));
                    }
                }
            }
            finally
            {

            }

            return clsUsers;
        }

        public static UsersTO GetById(int intUserId) {
            UsersTO clsUsers = new UsersTO();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE users.UserId=@UserId");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, intUserId);

                using (IDataReader drdUsers = db.ExecuteReader(cmdUsers))
                {
                    if (drdUsers.Read())
                    {
                        clsUsers = Load(drdUsers);
                    }
                }
            }
            finally
            {

            }

            return clsUsers;
        }

        #endregion

        #region .: Custom Search :.

        public static UsersTO GetByNameAndPassword(string strName, string strPassword)
        {
            UsersTO clsUsers = new UsersTO();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE memberships.`Name`=@Name AND memberships.`Password`=@Password");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@Name", DbType.String, CDM.Cript(strName.ToUpper()));
                db.AddInParameter(cmdUsers, "@Password", DbType.String, CDM.Cript(strPassword));

                using (IDataReader drdUsers = db.ExecuteReader(cmdUsers))
                {
                    if (drdUsers.Read())
                    {
                        clsUsers = Load(drdUsers);
                    }
                }
            }
            finally
            {

            }

            return clsUsers;
        }

        public static object GetByName(string strNome)
        {
            List<UsersTO> clsUsers = new List<UsersTO>();

            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT users.UserId, users.UserName, users.LastActivityDate, memberships.`Password`,");
                strSQL.Append(" memberships.Email, memberships.Inactive, memberships.CreateDate, memberships.ExpirationDate,");
                strSQL.Append(" memberships.Access, memberships.`Name`, usuarios_farmacias.FarmaciaId");
                strSQL.Append(" FROM users LEFT JOIN memberships ON users.UserId = memberships.UserId LEFT JOIN usuarios_farmacias ON users.UserId = usuarios_farmacias.UserId");
                strSQL.Append(" WHERE users.UserName LIKE @UserName");
                strSQL.Append(" ORDER BY users.UserName");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserName", DbType.String, string.Format("%{0}%", strNome));

                using (IDataReader drdUsers = db.ExecuteReader(cmdUsers))
                {
                    while (drdUsers.Read())
                    {
                        clsUsers.Add(Load(drdUsers));
                    }
                }
            }
            finally
            {

            }

            return clsUsers;
        }

        #endregion

        #region .: Persistence :.

        public static UsersTO Insert(UsersTO clsUsers) {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

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

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserName", DbType.String, clsUsers.UserName);
                db.AddInParameter(cmdUsers, "@LastActivityDate", DbType.DateTime, clsUsers.LastActivityDate);

                using (IDataReader drdUsers = db.ExecuteReader(cmdUsers))
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

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.AddInParameter(cmdUsers, "@Password", DbType.String, CDM.Cript(clsUsers.Password));
                db.AddInParameter(cmdUsers, "@Email", DbType.String, clsUsers.Email);
                db.AddInParameter(cmdUsers, "@Inactive", DbType.Int32, (clsUsers.Inactive == false ? 0 : 1));
                db.AddInParameter(cmdUsers, "@CreateDate", DbType.DateTime, clsUsers.CreateDate);
                db.AddInParameter(cmdUsers, "@ExpirationDate", DbType.DateTime, clsUsers.ExpirationDate);
                db.AddInParameter(cmdUsers, "@Access", DbType.String, CDM.Cript(clsUsers.Access));
                db.AddInParameter(cmdUsers, "@Name", DbType.String, CDM.Cript(clsUsers.Name));
                db.ExecuteNonQuery(cmdUsers);

                strSQL.Clear();
                strSQL.Append("INSERT INTO usuarios_farmacias (UserId, FarmaciaId) VALUES ( @UserId, @FarmaciaId)");

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.AddInParameter(cmdUsers, "@FarmaciaId", DbType.Int32, clsUsers.FarmaciaId);
                db.ExecuteNonQuery(cmdUsers);

            }
            finally
            {

            }

            return clsUsers;
        }

        public static Boolean Update(UsersTO clsUsers)
        {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("UPDATE users SET UserName = @UserName, LastActivityDate = @LastActivityDate");
                strSQL.Append(" WHERE users.UserId=@UserId");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.AddInParameter(cmdUsers, "@UserName", DbType.String, clsUsers.UserName);
                db.AddInParameter(cmdUsers, "@LastActivityDate", DbType.DateTime, clsUsers.LastActivityDate);
                db.ExecuteNonQuery(cmdUsers);

                strSQL.Clear();
                strSQL.Append("UPDATE memberships SET Password = @Password, Email = @Email, ");
                strSQL.Append(" Inactive = @Inactive, CreateDate = @CreateDate, ExpirationDate = @ExpirationDate,");
                strSQL.Append(" Access = @Access, Name = @Name WHERE UserId = @UserId");

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.AddInParameter(cmdUsers, "@Password", DbType.String, CDM.Cript(clsUsers.Password));
                db.AddInParameter(cmdUsers, "@Email", DbType.String, clsUsers.Email);
                db.AddInParameter(cmdUsers, "@Inactive", DbType.Int32, (clsUsers.Inactive == false ? 0 : 1));
                db.AddInParameter(cmdUsers, "@CreateDate", DbType.DateTime, clsUsers.CreateDate);
                db.AddInParameter(cmdUsers, "@ExpirationDate", DbType.DateTime, clsUsers.ExpirationDate);
                db.AddInParameter(cmdUsers, "@Access", DbType.String, CDM.Cript(clsUsers.Access));
                db.AddInParameter(cmdUsers, "@Name", DbType.String, CDM.Cript(clsUsers.Name));
                db.ExecuteNonQuery(cmdUsers);

                strSQL.Clear();
                strSQL.Append("UPDATE usuarios_farmacias SET FarmaciaId = @FarmaciaId WHERE UserId = @UserId");

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.AddInParameter(cmdUsers, "@FarmaciaId", DbType.Int32, clsUsers.FarmaciaId);
                db.ExecuteNonQuery(cmdUsers);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Boolean Delete(UsersTO clsUsers) {
            Database db = DatabaseFactory.CreateDatabase("SIAOConnectionString");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("DELETE FROM usuarios_farmacias WHERE UserId = @UserId");

                DbCommand cmdUsers = db.GetSqlStringCommand(strSQL.ToString());

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.ExecuteNonQuery(cmdUsers);

                strSQL.Clear();
                strSQL.Append("DELETE FROM memberships WHERE UserId = @UserId");

                cmdUsers = db.GetSqlStringCommand(strSQL.ToString());
                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.ExecuteNonQuery(cmdUsers);

                strSQL.Clear();
                strSQL.Append("DELETE FROM users WHERE users.UserId=@UserId");

                db.AddInParameter(cmdUsers, "@UserId", DbType.Int32, clsUsers.UserId);
                db.ExecuteNonQuery(cmdUsers);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
