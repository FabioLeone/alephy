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

        protected void edFactor_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/controls/factors_set.aspx");
        }

        protected void foption_ServerClick(object sender, EventArgs e)
        {
            string s = HttpContext.Current.Request.Url.AbsolutePath;
            switch (((System.Web.UI.HtmlControls.HtmlAnchor)sender).ID)
            {
                case "fall":
                    Response.Redirect(s + "?f=a");
                    break;
                case "ffulfilled":
                    Response.Redirect(s + "?f=f");
                    break;
                case "funfilled":
                    Response.Redirect(s + "?f=u");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}