using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIAO
{
    public partial class configurations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            Control ul = Master.FindControl("navlist");

            foreach (Control item in ul.Controls)
            {
                if (item != null)
                    ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
            }

            Control li = Master.FindControl("l2");
            if (li != null)
                ((System.Web.UI.HtmlControls.HtmlControl)li).Attributes.Add("class", "active");
        }
    }
}