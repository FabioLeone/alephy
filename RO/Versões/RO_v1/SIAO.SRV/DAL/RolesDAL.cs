using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class RolesDAL
    {
        #region .: Load :.
        private static RolesTO Load(IDataReader drdRoles) {
            RolesTO clsRoles = new RolesTO();
            if (!drdRoles.IsDBNull(drdRoles.GetOrdinal("RoleId"))) { clsRoles.RoleId = drdRoles.GetInt32(drdRoles.GetOrdinal("RoleId")); } else { clsRoles.RoleId = 0; }
            if (!drdRoles.IsDBNull(drdRoles.GetOrdinal("UserId"))) { clsRoles.UserId = drdRoles.GetInt32(drdRoles.GetOrdinal("UserId")); } else { clsRoles.UserId = 0; }
            if (!drdRoles.IsDBNull(drdRoles.GetOrdinal("Envio"))) { clsRoles.Envio = drdRoles.GetBoolean(drdRoles.GetOrdinal("Envio")); } else { clsRoles.Envio = false; }
            if (!drdRoles.IsDBNull(drdRoles.GetOrdinal("RelatoriosTodos"))) { clsRoles.RelatoriosTodos = drdRoles.GetBoolean(drdRoles.GetOrdinal("RelatoriosTodos")); } else { clsRoles.RelatoriosTodos = false; }
            return clsRoles;
        }
        private static RelatoriosTO LoadRelatorios(IDataReader drdRelatorios)
        {
            RelatoriosTO clsRelatorio = new RelatoriosTO();
            if (!drdRelatorios.IsDBNull(drdRelatorios.GetOrdinal("RelatorioId"))) { clsRelatorio.RelatorioId = drdRelatorios.GetInt32(drdRelatorios.GetOrdinal("RelatorioId")); } else { clsRelatorio.RelatorioId = 0; }
            if (!drdRelatorios.IsDBNull(drdRelatorios.GetOrdinal("RelatorioTipoId"))) { clsRelatorio.RelatorioTipoId = drdRelatorios.GetInt32(drdRelatorios.GetOrdinal("RelatorioTipoId")); } else { clsRelatorio.RelatorioTipoId = 0; }
            if (!drdRelatorios.IsDBNull(drdRelatorios.GetOrdinal("UsuarioId"))) { clsRelatorio.UsuarioId = drdRelatorios.GetInt32(drdRelatorios.GetOrdinal("UsuarioId")); } else { clsRelatorio.UsuarioId = 0; }
            return clsRelatorio;
        }
        #endregion

        #region .: Search :.
        internal static RolesTO GetByUserId(int intUserId, string strConnection)
        {
            RolesTO clsRoles = new RolesTO();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT roles.RoleId,roles.UserId,roles.Envio,roles.RelatoriosTodos FROM roles WHERE roles.UserId=@UserId");

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", intUserId));

                msc.Open();

                using (IDataReader drdRoles = cmdUsers.ExecuteReader())
                {
                    if(drdRoles.Read())
                    {
                        clsRoles = Load(drdRoles);
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsRoles;
        }
        #endregion

        #region .: Serach Custom :.
        internal static List<RolesTO> GetByRedeId(int intRedeId, string strConnection)
        {
            List<RolesTO> clsRoles = new List<RolesTO>();

            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT roles.RoleId,roles.UserId,roles.Envio,roles.RelatoriosTodos FROM roles LEFT JOIN farmacias ON roles.UserId = farmacias.ProprietarioID WHERE farmacias.idRede =@idRede GROUP BY RoleId");

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@idRede", intRedeId));

                msc.Open();

                using (IDataReader drdRoles = cmdUsers.ExecuteReader())
                {
                    while (drdRoles.Read())
                    {
                        clsRoles.Add(Load(drdRoles));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsRoles;
        }

        internal static List<RelatoriosTO> GetRelatoriosByUserId(int UserId, string strConnection)
        {
            List<RelatoriosTO> clsRelatorios = new List<RelatoriosTO>();
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT relatorios.RelatorioId,relatorios.RelatorioTipoId,relatorios.UsuarioId FROM relatorios WHERE relatorios.UsuarioId = @UsuarioId");

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL.ToString();
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", UserId));

                msc.Open();

                using (IDataReader drdRelatorios = cmdUsers.ExecuteReader())
                {
                    while (drdRelatorios.Read())
                    {
                        clsRelatorios.Add(LoadRelatorios(drdRelatorios));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsRelatorios;
        }


        #endregion

        #region .: Persistence :.

        internal static bool Insert(RolesTO clsRole, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                String strSQL = "INSERT INTO roles (UserId,Envio,RelatoriosTodos) VALUES (@UserId,@Envio,@RelatoriosTodos)";

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL;
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsRole.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Boolean, "@Envio", clsRole.Envio));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Boolean, "@RelatoriosTodos", clsRole.RelatoriosTodos));

                msc.Open();
                cmdUsers.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }
            finally
            {
                msc.Close();
            }
        }
        internal static void Insert(RelatoriosTO clsRelatorio, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                String strSQL = "INSERT INTO relatorios (RelatorioTipoId,UsuarioId) VALUES (@RelatorioTipoId,@UsuarioId)";

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL;
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@RelatorioTipoId", clsRelatorio.RelatorioTipoId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", clsRelatorio.UsuarioId));

                msc.Open();
                cmdUsers.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }

        internal static void Update(RolesTO clsRole, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                String strSQL = "UPDATE roles SET UserId=@UserId,Envio=@Envio,RelatoriosTodos=@RelatoriosTodos WHERE RoleId=@RoleId";

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL;
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@RoleId", clsRole.RoleId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UserId", clsRole.UserId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Boolean, "@Envio", clsRole.Envio));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Boolean, "@RelatoriosTodos", clsRole.RelatoriosTodos));

                msc.Open();
                cmdUsers.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }


        internal static void Update(RelatoriosTO clsRelatorio, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                String strSQL = "UPDATE relatorios SET RelatorioTipoId=@RelatorioTipoId,UsuarioId=@UsuarioId WHERE RelatorioId=@RelatorioId";

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL;
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@RelatorioId", clsRelatorio.RelatorioId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@RelatorioTipoId", clsRelatorio.RelatorioTipoId));
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", clsRelatorio.UsuarioId));

                msc.Open();
                cmdUsers.ExecuteNonQuery();
            }
            finally
            {
                msc.Close();
            }
        }

        internal static void Delete(RelatoriosTO clsRelatorios, string strConnection)
        {
            MySqlConnection msc = new MySqlConnection(strConnection);

            try
            {
                String strSQL = "DELETE FROM relatorios WHERE UsuarioId=@UsuarioId";

                DbCommand cmdUsers = msc.CreateCommand();

                cmdUsers.CommandText = strSQL;
                cmdUsers.Parameters.Clear();
                cmdUsers.Parameters.Add(DbHelper.GetParameter(cmdUsers, DbType.Int32, "@UsuarioId", clsRelatorios.UsuarioId));

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
