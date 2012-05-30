using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace SIAO.SRV
{
    public class clsAccess
    {
        clsDB db = new clsDB();
        MySqlCommand cmm = new MySqlCommand();
        MySqlConnection cnn = new MySqlConnection();
        clsFuncs o = new clsFuncs();

        public Usuario VerifAcesso(Usuario user, string scn)
        {
            cnn.ConnectionString = scn;
            cmm.Connection = cnn;
            DataSet ds = new DataSet();

            string lsSenha = o.encr(user.Password);
            string lsNome = o.encr(user.AcsName.ToUpper());

            cmm.CommandText = "SELECT memberships.*, users.UserName"
                + " FROM  memberships INNER JOIN"
                + " users ON memberships.UserId = users.UserId"
                + " WHERE (memberships.Password = '" + lsSenha + "') AND (memberships.Name = '" + lsNome + "')";

            if (db.openConnection(cmm))
            {
                ds = db.QueryDS(ref cmm, ref ds, "User");
            }
            else { user.Access = "n"; }
            db.closeConnection(cmm);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables["User"].Rows[0]["UserId"] != DBNull.Value)
                {
                    user.CreateDate = (DateTime)ds.Tables["User"].Rows[0]["CreateDate"];
                    user.Email = ds.Tables["User"].Rows[0]["Email"].ToString();
                    user.ExpirationDate = (DateTime)ds.Tables["User"].Rows[0]["ExpirationDate"];
                    user.Inactive = (bool)(ds.Tables["User"].Rows[0]["Inactive"].ToString() == "0" ? false : true);
                    user.Name = ds.Tables["User"].Rows[0]["UserName"].ToString();
                    user.UserId = (int)ds.Tables["User"].Rows[0]["UserId"];
                    user.Access = o.denc(ds.Tables["User"].Rows[0]["Access"].ToString());

                }
            }

            return user;
        }
    }
}
