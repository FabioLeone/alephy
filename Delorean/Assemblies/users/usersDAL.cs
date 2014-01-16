using Assemblies.utilities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies.users
{
    public class usersDAL
    {
        #region .:Search:.
        internal static usersTO GetByNameAndPassword(string sName, string sPassword)
        {
            usersTO objUser = new usersTO();

            NpgsqlConnection nc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);

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

                NpgsqlCommand cmdUsers = nc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Add("@Name", NpgsqlDbType.Varchar).Value = cdModel.cript(sName.ToUpper());
                cmdUsers.Parameters.Add("@Password", NpgsqlDbType.Varchar).Value = cdModel.cript(sPassword);

                nc.Open();

                using (IDataReader drdUsers = cmdUsers.ExecuteReader())
                {
                    if (drdUsers.Read())
                    {
                        objUser = Load(drdUsers);
                    }
                }
            }
            finally
            {
                nc.Close();
            }

            return objUser;
        }

        #endregion

        #region .:Load:.
        private static usersTO Load(IDataReader drdUsers)
        {
            usersTO clsUsers = new usersTO();

            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserId"))) { clsUsers.UserId = drdUsers.GetInt32(drdUsers.GetOrdinal("UserId")); } else { clsUsers.UserId = 0; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("UserName"))) { clsUsers.UserName = drdUsers.GetString(drdUsers.GetOrdinal("UserName")); } else { clsUsers.UserName = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("LastActivityDate"))) { clsUsers.LastActivityDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("LastActivityDate")); } else { clsUsers.LastActivityDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Password"))) { clsUsers.Password = cdModel.desc(drdUsers.GetString(drdUsers.GetOrdinal("Password"))); } else { clsUsers.Password = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Email"))) { clsUsers.Email = drdUsers.GetString(drdUsers.GetOrdinal("Email")); } else { clsUsers.Email = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Inactive"))) { clsUsers.Inactive = (drdUsers.GetInt32(drdUsers.GetOrdinal("Inactive")) == 1 ? true : false); } else { clsUsers.Inactive = true; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("CreateDate"))) { clsUsers.CreateDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("CreateDate")); } else { clsUsers.CreateDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("ExpirationDate"))) { clsUsers.ExpirationDate = drdUsers.GetDateTime(drdUsers.GetOrdinal("ExpirationDate")); } else { clsUsers.ExpirationDate = DateTime.Now; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Access"))) { clsUsers.Access = cdModel.desc(drdUsers.GetString(drdUsers.GetOrdinal("Access"))); } else { clsUsers.Access = ""; }
            if (!drdUsers.IsDBNull(drdUsers.GetOrdinal("Name"))) { clsUsers.Name = cdModel.desc(drdUsers.GetString(drdUsers.GetOrdinal("Name"))); } else { clsUsers.Name = ""; }
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
        #endregion
    }
}
