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
            usersBLL.ChkMenu(l2);
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
            string s = HttpContext.Current.Request.Url.AbsoluteUri;

            if (s.Contains("?") && s.Contains("&")) {
                s = s.Replace("f=" + Request.QueryString["f"], "");
                s = s.Replace("&", "");
                s += "&";
            }
            else if (s.Contains("?") && s.Contains("s="))
                s += "&";
            else if (s.Contains("?")) {
                s = s.Replace("f=" + Request.QueryString["f"], "");
            }else
                s += "?";

            switch (((System.Web.UI.HtmlControls.HtmlAnchor)sender).ID)
            {
                case "fall":
                    Response.Redirect(s + "f=a");
                    break;
                case "ffulfilled":
                    Response.Redirect(s + "f=f");
                    break;
                case "funfilled":
                    Response.Redirect(s + "f=u");
                    break;
                default:
                    break;
            }
        }

        protected void schbtn_Click(object sender, EventArgs e)
        {
            string s = HttpContext.Current.Request.Url.AbsoluteUri;

            if (s.Contains("?") && s.Contains("&"))
            {
                s = s.Replace("s=" + Request.QueryString["s"], "");
                s = s.Replace("&", "");
                s += "&";
            }
            else if (s.Contains("?") && s.Contains("f="))
                s += "&";
            else if (s.Contains("?"))
            {
                s = s.Replace("s=" + Request.QueryString["s"], "");
            }
            else
                s += "?";


            Response.Redirect(s + "s=" + Request.Form[ulmenu.FindControl("psearch").UniqueID]);
        }
        #endregion
    }
}