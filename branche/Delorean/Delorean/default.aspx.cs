using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usersBLL.IsValid(Request.QueryString["k"]);

            if (!IsPostBack)
            {
                Control ul = Master.FindControl("ulmenu");

                foreach (Control item in ul.Controls)
                {
                    string s = string.Empty;
                    if (item != null && item.GetType().Name == "HtmlGenericControl")
                    {
                        if (((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Count > 0)
                        {

                            s = ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes["class"].Replace("current ", "");

                            if (((System.Web.UI.HtmlControls.HtmlControl)item).ID.Equals("l2"))
                            {
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Add("class", s);
                            }
                            else if (((System.Web.UI.HtmlControls.HtmlControl)item).ID.Equals("l1"))
                            {
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Add("class", "current " + s);
                            } 
                        }
                    }
                }
            }
        }
    }
}