using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Assemblies.utilities
{
    class helpers
    {
        internal static void SetSession(users.usersTO objUser)
        {
            String jssObject = new JavaScriptSerializer().Serialize(objUser);

            HttpContext.Current.Session[cdModel.cript(objUser.UserName)] = cdModel.cript(jssObject);
        }
    }
}
