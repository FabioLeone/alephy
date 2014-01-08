using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;


namespace suLibrary
{
    public class Util
    {
        public static void clearSession()
        {
            sAccess.Default.Session = null;
            sAccess.Default.Save();
        }

        public static void setSession<T>(T u)
        {
            string s = new JavaScriptSerializer().Serialize(u);
            sAccess.Default.Session = cdm.cript(s);
            sAccess.Default.Save();
        }

        public static T getSession<T>()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            if (!string.IsNullOrEmpty(sAccess.Default.Session))
                return (T)jss.Deserialize(cdm.desc(sAccess.Default.Session), typeof(T));
            else
                return default(T);
        }

        public static string MaskCnpj(string p)
        {
            string cnpj = "";
            if (string.IsNullOrEmpty(p))
                return cnpj;
            else
            {
                cnpj = p.Substring(0, 2) + ".";
                cnpj += p.Substring(2, 3) + ".";
                cnpj += p.Substring(5, 3) + "/";
                cnpj += p.Substring(8, 4) + "-";
                cnpj += p.Substring(12, 2);

                return cnpj;
            }
        }
    }
}
