using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class VinculoDAL
    {
        #region .: Load :.

        private static VinculoTO Load(IDataReader drdVinculo)
        {
            VinculoTO clsVinculo = new VinculoTO();

            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("id"))) { clsVinculo.id = drdVinculo.GetInt32(drdVinculo.GetOrdinal("id")); } else { clsVinculo.id = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("UserId"))) { clsVinculo.UsuarioId = drdVinculo.GetInt32(drdVinculo.GetOrdinal("UserId")); } else { clsVinculo.UsuarioId = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("LinkId"))) { clsVinculo.LinkId = drdVinculo.GetInt32(drdVinculo.GetOrdinal("LinkId")); } else { clsVinculo.LinkId = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("UserName"))) { clsVinculo.UserName = drdVinculo.GetString(drdVinculo.GetOrdinal("UserName")); } else { clsVinculo.UserName = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("CNPJ"))) { clsVinculo.CNPJ = drdVinculo.GetString(drdVinculo.GetOrdinal("CNPJ")); } else { clsVinculo.CNPJ = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("Empresa"))) { clsVinculo.Empresa = drdVinculo.GetString(drdVinculo.GetOrdinal("Empresa")); } else { clsVinculo.Empresa = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("TipoId"))) { clsVinculo.TipoId = drdVinculo.GetInt32(drdVinculo.GetOrdinal("TipoId")); } else { clsVinculo.TipoId = 0; }

            return clsVinculo;
        }
        #endregion

        #region .:Search:.
        internal static List<VinculoTO> GetByTipoIdAndSearch(int intTipoId, string strSearch)
        {
            List<VinculoTO> clsVinculos = new List<VinculoTO>();

            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            int intUserId = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT usuarios_vinculos.id,users.UserId,usuarios_vinculos.LinkId,users.UserName, CASE WHEN users.TipoId = 2 THEN farmacias.CNPJ ELSE
                (CASE WHEN users.TipoId = 3 THEN redesfarmaceuticas.CNPJ ELSE (CASE WHEN users.TipoId = 4 THEN 'Industria' ELSE '' END) END) END AS CNPJ, 
                CASE WHEN users.TipoId = 2 THEN farmacias.NomeFantasia ELSE
                (CASE WHEN users.TipoId = 3 THEN redesfarmaceuticas.Descricao ELSE (CASE WHEN users.TipoId = 4 THEN 'Industria' ELSE '' END) END) END AS Empresa,
                users.TipoId
                FROM users LEFT JOIN
                usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId LEFT JOIN
                redesfarmaceuticas ON usuarios_vinculos.LinkId = redesfarmaceuticas.Id LEFT JOIN
                farmacias ON usuarios_vinculos.LinkId = farmacias.Id
                WHERE users.TipoId = @TipoId");
            
                if(int.TryParse(strSearch,out intUserId)){
                    strSQL.Append(" AND users.UserId = @UserId");
                }else if(!String.IsNullOrEmpty(strSearch)){
                    strSQL.Append(" AND users.UserName like @UserName");
                }
                 
                strSQL.Append(" ORDER BY users.UserName");

                DbCommand cmdVinculos = msc.CreateCommand();
                cmdVinculos.CommandText = strSQL.ToString();
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos,DbType.Int32,"@TipoId", intTipoId));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@UserId", intUserId));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.String, "@UserName", String.Format("%{0}%",strSearch)));

                msc.Open();

                using (IDataReader drdVinculos = cmdVinculos.ExecuteReader())
                {
                    while (drdVinculos.Read())
                    {
                        clsVinculos.Add(Load(drdVinculos));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsVinculos;
        }
        #endregion

        #region .:Persistence:.
        internal static void Insert(VinculoTO clsVinculo)
        {
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("INSERT INTO usuarios_vinculos (UsuarioId, LinkId) VALUES (@UsuarioId, @LinkId);");
                strSQL.Append("SELECT usuarios_vinculos.id FROM usuarios_vinculos WHERE usuarios_vinculos.id = @@IDENTITY;");

                DbCommand cmdVinculo = msc.CreateCommand();
                cmdVinculo.CommandText = strSQL.ToString();

                cmdVinculo.Parameters.Clear();
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@UsuarioId", clsVinculo.UsuarioId));
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@LinkId", clsVinculo.LinkId));

                msc.Open();

                using (IDataReader drdVinculo = cmdVinculo.ExecuteReader())
                {
                    if (drdVinculo.Read())
                    {
                        if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("id"))) { clsVinculo.id = drdVinculo.GetInt32(drdVinculo.GetOrdinal("id")); } else { clsVinculo.id = 0; }
                    }
                }
            }
            finally
            {
                msc.Close();
            }
        }

        internal static void Update(VinculoTO clsVinculo)
        {
            MySqlConnection msc = new MySqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SET NOCOUNT=ON;");
                strSQL.Append("UPDATE usuarios_vinculos SET UsuarioId = @UsuarioId, LinkId = @LinkId WHERE usuarios_vinculos.id=@id");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@id", clsVinculo.id));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", clsVinculo.UsuarioId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@LinkId", clsVinculo.LinkId));

                msc.Open();

                cmdUsers.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }
        #endregion
    }
}
