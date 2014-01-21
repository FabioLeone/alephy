using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean
{
    public partial class site : System.Web.UI.MasterPage
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbLogout_ServerClick(object sender, EventArgs e)
        {
            usersBLL.Signout();
        }
        #endregion
    }
}