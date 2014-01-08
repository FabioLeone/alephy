using suLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uLibrary
{
    public class Users
    {
        public static string vAccess(global::System.Windows.Forms.TextBox txtUser, global::System.Windows.Forms.TextBox txtPassword)
        {
            User u = uDal.getByNameAndPassword(txtUser.Text, txtPassword.Text);

            if (u.UserId > 0)
            {
                Util.setSession<User>(u);
                return string.Empty;
            }
            else {
                return "Usuário/senha inválido.";
            }
        }

        public static bool cAccess()
        {
            User u = Util.getSession<User>();

            if (u != null)
                return (u.UserId > 0);
            else
                return false;
        }
    }
}
