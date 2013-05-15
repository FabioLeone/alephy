using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Data;
using Npgsql;
using System.Data.Common;

namespace SIAO.SRV.DAL
{
    public class FilesDAL
    {
        #region .: Load :.

        private static FilesTO Load(IDataReader drdFiles)
        {
            FilesTO clsFiles = new FilesTO();

            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("id"))) { clsFiles.id = drdFiles.GetInt32(drdFiles.GetOrdinal("id")); } else { clsFiles.id = 0; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("data"))) { clsFiles.data = drdFiles.GetDateTime(drdFiles.GetOrdinal("data")); } else { clsFiles.data = DateTime.MinValue; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("UserId"))) { clsFiles.UserId = drdFiles.GetInt32(drdFiles.GetOrdinal("UserId")); } else { clsFiles.UserId = 0; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("cnpj"))) { clsFiles.cnpj = drdFiles.GetString(drdFiles.GetOrdinal("cnpj")); } else { clsFiles.cnpj = string.Empty; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("tipo"))) { clsFiles.tipo = drdFiles.GetString(drdFiles.GetOrdinal("tipo")); } else { clsFiles.tipo = string.Empty; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("mes"))) { clsFiles.mes = drdFiles.GetInt32(drdFiles.GetOrdinal("mes")); } else { clsFiles.mes = 0; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("ano"))) { clsFiles.ano = drdFiles.GetInt32(drdFiles.GetOrdinal("ano")); } else { clsFiles.ano = 0; }

            return clsFiles;
        }
        private static FilesTO LoadII(IDataReader drdFiles)
        {
            FilesTO clsFiles = new FilesTO();

            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("cnpj"))) { clsFiles.cnpj = drdFiles.GetString(drdFiles.GetOrdinal("cnpj")); } else { clsFiles.cnpj = string.Empty; }
            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("NomeFantasia"))) { clsFiles.NomeFantasia = drdFiles.GetString(drdFiles.GetOrdinal("NomeFantasia")); } else { clsFiles.NomeFantasia = string.Empty; }

            return clsFiles;
        }
        private static FilesTO LoadIII(IDataReader drdFiles)
        {
            FilesTO clsFiles = new FilesTO();

            if (!drdFiles.IsDBNull(drdFiles.GetOrdinal("data"))) { clsFiles.data = drdFiles.GetDateTime(drdFiles.GetOrdinal("data")); } else { clsFiles.data = DateTime.MinValue; }

            return clsFiles;
        }
        #endregion

        #region .: Search :.
        public static List<FilesTO> GetByYear(int intAno, string strConnection)
        {
            List<FilesTO> clsFiles = new List<FilesTO>();

            NpgsqlConnection msc = new NpgsqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                //TODO - busca por ano
                strSQL.Append(String.Format("SELECT id,`data`,UserId,cnpj,tipo,mes,ano FROM arquivosenviados WHERE YEAR(`data`) = '{0}';", intAno));

                DbCommand cmdFile = msc.CreateCommand();
                cmdFile.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdFiles = cmdFile.ExecuteReader())
                {
                    while (drdFiles.Read())
                    {
                        clsFiles.Add(Load(drdFiles));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsFiles;
        }
        internal static List<FilesTO> GetByYearAndRedeId(int intAno, int intRedeId, string scn)
        {
            List<FilesTO> clsFiles = new List<FilesTO>();

            NpgsqlConnection msc = new NpgsqlConnection(scn);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                //TODO - busca por ano
                strSQL.Append(String.Format("SELECT DISTINCT arquivosenviados.cnpj,farmacias.NomeFantasia FROM arquivosenviados INNER JOIN farmacias ON arquivosenviados.cnpj = farmacias.Cnpj WHERE YEAR(`data`) = '{0}'", intAno));
                if (intRedeId > 0)
                    strSQL.Append(String.Format(" AND farmacias.idRede = {0}", intRedeId));
                else
                    strSQL.Append(" AND (farmacias.idRede = 0 OR farmacias.idRede IS NULL)");
                strSQL.Append(" ORDER BY cnpj;");

                DbCommand cmdFile = msc.CreateCommand();
                cmdFile.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdFiles = cmdFile.ExecuteReader())
                {
                    while (drdFiles.Read())
                    {
                        clsFiles.Add(LoadII(drdFiles));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsFiles;
        }

        internal static List<FilesTO> GetByCnpj(string strCnpj, string strAno, string scn)
        {
            List<FilesTO> clsFiles = new List<FilesTO>();

            NpgsqlConnection msc = new NpgsqlConnection(scn);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                //TODO - busca por ano
                strSQL.Append(String.Format("SELECT arquivosenviados.`data` FROM arquivosenviados WHERE YEAR(`data`) = '{0}' AND cnpj = '{1}' ORDER BY `data`;", strAno, strCnpj));

                DbCommand cmdFile = msc.CreateCommand();
                cmdFile.CommandText = strSQL.ToString();

                msc.Open();

                using (IDataReader drdFiles = cmdFile.ExecuteReader())
                {
                    while (drdFiles.Read())
                    {
                        clsFiles.Add(LoadIII(drdFiles));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsFiles;
        }
        #endregion

    }
}
