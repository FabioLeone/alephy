using Assemblies.users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean
{
    public partial class accesscontrol : System.Web.UI.Page
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void smtLogin_ServerClick()
        {
            if (usersBLL.Authentication(Request.Form[txtName.UniqueID], Request.Form[txtPassword.UniqueID]))
            {
                FormsAuthentication.RedirectFromLoginPage(Request.Form[txtName.UniqueID], cboxKeep.Checked);
                //Response.Redirect("~/Default.aspx");
            }
            else
            {
            }
        }
        #endregion
    }
}