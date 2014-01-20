using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Assemblies
{
    class helpers
    {
        internal static void SetSession(usersTO objUser)
        {
            String jssObject = new JavaScriptSerializer().Serialize(objUser);

            HttpContext.Current.Session[cdModel.cript(objUser.UserName)] = cdModel.cript(jssObject);
        }

        internal static usersTO GetSession()
        {
            usersTO objUser = new usersTO();
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            Type t = objUser.GetType();
            String strName = HttpContext.Current.User.Identity.Name;

            if (HttpContext.Current.Session[cdModel.cript(strName)] != null)
                return (usersTO)jssObject.Deserialize(cdModel.desc(HttpContext.Current.Session[cdModel.cript(strName)].ToString()), t);
            else
                return objUser;
        }

        internal static void ClearSession()
        {
            String strName = HttpContext.Current.User.Identity.Name;
            HttpContext.Current.Session[cdModel.cript(strName)] = null;
        }
    }
}
