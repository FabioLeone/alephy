using System;
using System.Data;
using System.Data.SqlClient;

namespace SIAO.SRV
{
    public class clsAccess
    {
        clsDB db = new clsDB();
        SqlCommand cmm = new SqlCommand();
        SqlConnection cnn = new SqlConnection();
        clsFuncs o = new clsFuncs();

        public Usuario VerifAcesso(Usuario user, string scn)
        {
            cnn.ConnectionString = scn;
            cmm.Connection = cnn;
            DataSet ds = new DataSet();

            string lsSenha = o.encr(user.Password);
            string lsNome = o.encr(user.AcsName.ToUpper());

            cmm.CommandText = "SELECT Memberships.*, Users.UserName"
                + " FROM  Memberships INNER JOIN"
                + " Users ON Memberships.UserId = Users.UserId"
                + " WHERE (Memberships.Password = '" + lsSenha + "') AND (Memberships.Name = '" + lsNome + "')";

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
                    user.Inactive = (bool)ds.Tables["User"].Rows[0]["Inactive"];
                    user.Name = ds.Tables["User"].Rows[0]["UserName"].ToString();
                    user.UserId = (int)ds.Tables["User"].Rows[0]["UserId"];
                    user.Access = o.denc(ds.Tables["User"].Rows[0]["Access"].ToString());

                }
            }

            return user;
        }
    }
}
