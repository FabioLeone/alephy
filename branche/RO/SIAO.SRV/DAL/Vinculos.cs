using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Npgsql;
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
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("redeid"))) { clsVinculo.redeid = drdVinculo.GetInt32(drdVinculo.GetOrdinal("redeid")); } else { clsVinculo.redeid = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("farmaciaid"))) { clsVinculo.farmaciaid = drdVinculo.GetInt32(drdVinculo.GetOrdinal("farmaciaid")); } else { clsVinculo.farmaciaid = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("industriaid"))) { clsVinculo.industriaid = drdVinculo.GetInt32(drdVinculo.GetOrdinal("industriaid")); } else { clsVinculo.industriaid = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("UserName"))) { clsVinculo.UserName = drdVinculo.GetString(drdVinculo.GetOrdinal("UserName")); } else { clsVinculo.UserName = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("CNPJ"))) { clsVinculo.CNPJ = drdVinculo.GetString(drdVinculo.GetOrdinal("CNPJ")); } else { clsVinculo.CNPJ = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("Empresa"))) { clsVinculo.Empresa = drdVinculo.GetString(drdVinculo.GetOrdinal("Empresa")); } else { clsVinculo.Empresa = ""; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("TipoId"))) { clsVinculo.TipoId = drdVinculo.GetInt32(drdVinculo.GetOrdinal("TipoId")); } else { clsVinculo.TipoId = 0; }
            if (!drdVinculo.IsDBNull(drdVinculo.GetOrdinal("nivel"))) { clsVinculo.nivel = drdVinculo.GetInt32(drdVinculo.GetOrdinal("nivel")); } else { clsVinculo.nivel = 0; }

            return clsVinculo;
        }
        #endregion

        #region .:Search:.
        internal static List<VinculoTO> GetByTipoIdAndSearch(int intTipoId, string strSearch)
        {
            List<VinculoTO> clsVinculos = new List<VinculoTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            int intUserId = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT uv.id,u.UserId,uv.redeid,uv.farmaciaid,uv.industriaid,u.UserName, 
				CASE WHEN u.TipoId = 1 THEN ( CASE WHEN u.nivel = 1 THEN r.cnpj ELSE (
				CASE WHEN u.nivel = 2 THEN f.cnpj ELSE '' END ) END ) ELSE (
			    CASE WHEN u.TipoId = 2 THEN f.CNPJ ELSE ( CASE WHEN u.TipoId = 3 THEN r.CNPJ ELSE (
				CASE WHEN u.TipoId = 4 THEN '' ELSE '' END ) END ) END
				) END AS CNPJ, CASE WHEN u.TipoId = 1 THEN ( CASE WHEN u.nivel = 1 THEN r.descricao ELSE (
				CASE WHEN u.nivel = 2 THEN f.NomeFantasia ELSE (
				CASE WHEN u.nivel = 3 THEN 'Industria' ELSE '' END ) END ) END ) ELSE (
				CASE WHEN u.TipoId = 2 THEN f.NomeFantasia ELSE ( CASE WHEN u.TipoId = 3 THEN r.Descricao ELSE (
                CASE WHEN u.TipoId = 4 THEN 'Industria' ELSE '' END ) END ) END
				) END AS Empresa, u.TipoId, u.owner,u.nivel
                FROM users u LEFT JOIN
                usuarios_vinculos uv ON u.UserId = uv.UsuarioId LEFT JOIN
                redesfarmaceuticas r ON uv.redeid = r.Id LEFT JOIN
                farmacias f ON uv.farmaciaid = f.Id
                WHERE u.TipoId = @TipoId");
            
                if(int.TryParse(strSearch,out intUserId)){
                    strSQL.Append(" AND u.UserId = @UserId");
                }else if(!String.IsNullOrEmpty(strSearch)){
                    strSQL.Append(" AND upper(u.UserName) like upper(@UserName)");
                }
                 
                strSQL.Append(" ORDER BY u.UserName");

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

        internal static List<VinculoTO> GetByTipoIdAndSearch(int intTipoId, string strSearch, TO.UsersTO oUser)
        {
            List<VinculoTO> clsVinculos = new List<VinculoTO>();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            int intUserId = 0;

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT uv.id,u.UserId,uv.redeid,uv.farmaciaid,uv.industriaid,u.UserName, 
                CASE WHEN u.TipoId = 2 THEN f.CNPJ ELSE (
	                CASE WHEN u.TipoId = 3 THEN r.CNPJ ELSE (
		                CASE WHEN u.TipoId = 4 THEN 'Industria' ELSE '' END
	                ) END
                ) END AS CNPJ, CASE WHEN u.TipoId = 2 THEN f.NomeFantasia ELSE (
	                CASE WHEN u.TipoId = 3 THEN r.Descricao ELSE (
		                CASE WHEN u.TipoId = 4 THEN 'Industria' ELSE '' END
	                ) END
                ) END AS Empresa, u.TipoId, u.owner,u.nivel
                FROM users u LEFT JOIN
                usuarios_vinculos uv ON u.UserId = uv.UsuarioId LEFT JOIN
                redesfarmaceuticas r ON uv.redeid = r.Id LEFT JOIN
                farmacias f ON uv.farmaciaid = f.Id
                WHERE u.TipoId = @TipoId");

                if (int.TryParse(strSearch, out intUserId))
                {
                    strSQL.Append(" AND u.UserId = @UserId");
                }
                else if (!String.IsNullOrEmpty(strSearch))
                {
                    strSQL.Append(" AND upper(u.UserName) like upper(@UserName)");
                }

                if (oUser.Nivel.Equals(1))
                    strSQL.Append(@" AND u.TipoId <> 1
					AND (uv.redeid = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
					OR f.idrede = (SELECT redeid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");
                else if (oUser.Nivel.Equals(2))
                    strSQL.Append(@" AND u.TipoId <> 1
					AND (uv.farmaciaid = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid) 
					OR f.id = (SELECT farmaciaid FROM usuarios_vinculos WHERE usuarioid = @usuarioid)
                    OR u.owner = @usuarioid)");

                strSQL.Append(" ORDER BY u.UserName");

                DbCommand cmdVinculos = msc.CreateCommand();
                cmdVinculos.CommandText = strSQL.ToString();
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@TipoId", intTipoId));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@UserId", intUserId));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.String, "@UserName", String.Format("%{0}%", strSearch)));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@usuarioid", oUser.UserId));

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

        internal static VinculoTO GetByCNPJ(string strCNPJ, int intTipoId)
        {
            VinculoTO clsVinculo = new VinculoTO();

            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);
            
            string scnpj = strCNPJ.Replace(".", "");
            scnpj = scnpj.Replace("/", "");
            scnpj = scnpj.Replace("-", "");

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(@"SELECT usuarios_vinculos.id,users.UserId,usuarios_vinculos.redeid,usuarios_vinculos.farmaciaid,usuarios_vinculos.industriaid,
                users.UserName, CASE WHEN users.TipoId = 2 THEN farmacias.CNPJ ELSE
                (CASE WHEN users.TipoId = 3 THEN redesfarmaceuticas.CNPJ ELSE (CASE WHEN users.TipoId = 4 THEN 'Industria' ELSE '' END) END) END AS CNPJ, 
                CASE WHEN users.TipoId = 2 THEN farmacias.NomeFantasia ELSE
                (CASE WHEN users.TipoId = 3 THEN redesfarmaceuticas.Descricao ELSE (CASE WHEN users.TipoId = 4 THEN 'Industria' ELSE '' END) END) END AS Empresa,
                users.TipoId, users.nivel
                FROM users LEFT JOIN
                usuarios_vinculos ON users.UserId = usuarios_vinculos.UsuarioId LEFT JOIN
                redesfarmaceuticas ON usuarios_vinculos.redeid = redesfarmaceuticas.Id LEFT JOIN
                farmacias ON usuarios_vinculos.farmaciaid = farmacias.Id
                WHERE (redesfarmaceuticas.CNPJ = @CNPJ OR farmacias.CNPJ = @CNPJ) AND users.tipoid = @tipoid ORDER BY users.UserName");

                DbCommand cmdVinculos = msc.CreateCommand();
                cmdVinculos.CommandText = strSQL.ToString();
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.String, "@CNPJ", scnpj));
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@tipoid", intTipoId));

                msc.Open();

                using (IDataReader drdVinculos = cmdVinculos.ExecuteReader())
                {
                    if (drdVinculos.Read())
                    {
                        clsVinculo = Load(drdVinculos);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsVinculo;
        }
        #endregion

        #region .:Persistence:.
        internal static void Insert(VinculoTO clsVinculo)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("INSERT INTO usuarios_vinculos (UsuarioId, redeid, farmaciaid, industriaid) VALUES (@UsuarioId, @redeid, @farmaciaid, @industriaid) RETURNING id;");

                DbCommand cmdVinculo = msc.CreateCommand();
                cmdVinculo.CommandText = strSQL.ToString();

                cmdVinculo.Parameters.Clear();
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@UsuarioId", clsVinculo.UsuarioId));
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@redeid", clsVinculo.redeid));
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@farmaciaid", clsVinculo.farmaciaid));
                cmdVinculo.Parameters.Add(DbHelper.GetParameter(cmdVinculo, DbType.Int32, "@industriaid", clsVinculo.industriaid));

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
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("UPDATE usuarios_vinculos SET UsuarioId = @UsuarioId, redeid = @redeid, farmaciaid = @farmaciaid, industriaid = @industriaid WHERE usuarios_vinculos.id=@id");

                DbCommand cmdUsers = msc.CreateCommand();
                cmdUsers.CommandText = strSQL.ToString();

                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@id", clsVinculo.id));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", clsVinculo.UsuarioId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@redeid", clsVinculo.redeid));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@farmaciaid", clsVinculo.farmaciaid));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@industriaid", clsVinculo.industriaid));

                msc.Open();

                cmdUsers.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }
        
        internal static void Delete(VinculoTO clsVinculo)
        {
            NpgsqlConnection msc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("DELETE FROM usuarios_vinculos WHERE id = @id");

                DbCommand cmdVinculos = msc.CreateCommand();
                cmdVinculos.CommandText = strSQL.ToString();

                cmdVinculos.Parameters.Clear();
                cmdVinculos.Parameters.Add(DbHelper.GetParameter(cmdVinculos, DbType.Int32, "@id", clsVinculo.id));

                msc.Open();

                cmdVinculos.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }
        #endregion

    }
}
