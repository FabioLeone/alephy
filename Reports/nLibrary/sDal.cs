using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace nLibrary
{
    class sDal
    {
        internal static object getByNetworkId(int intId)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection cnn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONECTIONSTRING"].ConnectionString);
            NpgsqlCommand cmm = new NpgsqlCommand();

            cmm.Connection = cnn;
            cmm.CommandText = @"SELECT id, Cnpj, NomeFantasia, idRede FROM farmacias
            WHERE farmacias.idRede = @idRede ORDER BY NomeFantasia";

            cmm.Parameters.Clear();
            cmm.Parameters.Add("@idRede", NpgsqlDbType.Integer).Value = intId;

            try
            {
                cnn.Open();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmm);

                da.Fill(ds, "Farmacias");
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                cnn.Close();
            }

            return ds;
        }
    }
}
