using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Assemblies
{
    public class usersBLL
    {
        #region .:Methods:.
        public static string Authentication(string sName, string sPassword, bool persistentCookie)
        {
            HttpContext ctx = HttpContext.Current;
            usersTO objUser = GetByNameAndPassword(sName, sPassword);

            if (objUser.UserId > 0)
            {
                helpers.SetSession(objUser);
                FormsAuthentication.RedirectFromLoginPage(objUser.UserName, persistentCookie);

                ctx.Response.Redirect("~/default.aspx");

                return string.Empty;
            }
            else return "usuário e/ou senha inválido(s).";
        }

        public static void IsValid()
        {
            HttpContext ctx = HttpContext.Current;

            if (helpers.GetSession().UserId == 0)
                ctx.Response.Redirect("~/accesscontrol.aspx");
        }

        public static void IsValid(string p)
        {
            HttpContext ctx = HttpContext.Current;

            if (String.IsNullOrEmpty(p))
            {
                if (helpers.GetSession().UserId == 0)
                    ctx.Response.Redirect("~/accesscontrol.aspx");
            }
            else
            {
                p = cdModel.desc(p);
                String[] q = p.Split(';');

                if (q.Length > 1)
                {
                    if (q[1].Equals(DateTime.Now.ToString("yyyyMMdd")))
                    {
                        int r = 0;
                        if (int.TryParse(q[0], out r))
                        {
                            usersTO u = GetById(r);

                            if (u.UserId > 0)
                                helpers.SetSession(u);
                            else
                                ctx.Response.Redirect("~/accesscontrol.aspx");
                        }
                    }
                    else
                        ctx.Response.Redirect("~/accesscontrol.aspx");
                }
                else
                    ctx.Response.Redirect("~/accesscontrol.aspx");
            }
        }

        public static void Signout()
        {
            HttpContext ctx = HttpContext.Current;
            helpers.ClearSession();
            FormsAuthentication.SignOut();
            
            ctx.Response.Redirect("~/accesscontrol.aspx");
        }

        internal static int Verify_registration(string p)
        {
            return usersDAL.Verify_registration(p);
        }

        #endregion

        #region .:Search:.
        private static usersTO GetByNameAndPassword(string sName, string sPassword)
        {
            return usersDAL.GetByNameAndPassword(sName, sPassword);
        }

        private static usersTO GetById(int id)
        {
            return usersDAL.GetById(id);
        }
        #endregion

    }
}
