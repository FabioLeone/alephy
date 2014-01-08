using Npgsql;
using suLibrary;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace uLibrary
{
    class uDal
    {
        #region .:search:.
        internal static User getByNameAndPassword(string strName, string strPassword)
        {
            User clsUser = new User();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);

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

                NpgsqlCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add("@Name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cdm.cript(strName.ToUpper());
                cmdUsers.Parameters.Add("@Password", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cdm.cript(strPassword);

                msc.Open();

                using (IDataReader drdUsers = cmdUsers.ExecuteReader())
                {
                    if (drdUsers.Read())
                    {
                        clsUser = Load(drdUsers);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsUser;
        }
        #endregion

        #region .:load:.
        private static User Load(IDataReader drdUsers)
        {
            User clsUser = new User();

            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserId"))) { clsUser.UserId = drdUsers.GetInt32(drdUsers.GetOrdinal("UserId")); } else { clsUser.UserId = 0; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserName"))) { clsUser.UserName = drdUsers.GetString(drdUsers.GetOrdinal("UserName")); } else { clsUser.UserName = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("LastActivityDate"))) { clsUser.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("LastActivityDate")); } else { clsUser.LastActivityDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Password"))) { clsUser.Password = cdm.desc(drdUsers.GetString(drdUsers.GetOrdinal("Password"))); } else { clsUser.Password = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Email"))) { clsUser.Email = drdUsers.GetString(drdUsers.GetOrdinal("Email")); } else { clsUser.Email = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Inactive"))) { clsUser.Inactive = (drdUsers.GetInt32(drdUsers.GetOrdinal("Inactive")) == 1 ? true : false); } else { clsUser.Inactive = true; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("CreateDate"))) { clsUser.CreateDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("CreateDate")); } else { clsUser.CreateDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("ExpirationDate"))) { clsUser.ExpirationDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("ExpirationDate")); } else { clsUser.ExpirationDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Access"))) { clsUser.Access = cdm.desc(drdUsers.GetString(drdUsers.GetOrdinal("Access"))); } else { clsUser.Access = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Name"))) { clsUser.Name = cdm.desc(drdUsers.GetString(drdUsers.GetOrdinal("Name"))); } else { clsUser.Name = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("FarmaciaId"))) { clsUser.FarmaciaId = drdUsers.GetInt32(drdUsers.GetOrdinal("FarmaciaId")); } else { clsUser.FarmaciaId = 0; }

            try
            {
                if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("TipoId"))) { clsUser.TipoId = drdUsers.GetInt32(drdUsers.GetOrdinal("TipoId")); } else { clsUser.TipoId = 0; }
            }
            catch
            {
                clsUser.TipoId = 0;
            }

            try
            {
                if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Tipo"))) { clsUser.Tipo = drdUsers.GetString(drdUsers.GetOrdinal("Tipo")); } else { clsUser.Tipo = ""; }
            }
            catch
            {
                clsUser.Tipo = "";
            }

            try
            {
                if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("nivel"))) { clsUser.Nivel = drdUsers.GetInt32(drdUsers.GetOrdinal("nivel")); } else { clsUser.Nivel = 0; }
            }
            catch
            {
                clsUser.Nivel = 0;
            }

            return clsUser;
        }
        #endregion
    }
}
