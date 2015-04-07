using System;
using System.Web;
using System.Web.UI;
using SIAO.SRV.BLL;

namespace SIAO
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (UsersBLL.GetUserSession().UserId == 0) { Response.Redirect("Logon.aspx"); }

			Control ul = Master.FindControl("navlist");

			foreach (Control item in ul.Controls)
			{
				if(item != null)
					((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
			}

			Control li = Master.FindControl("l1");
			if(li != null)
				((System.Web.UI.HtmlControls.HtmlControl)li).Attributes.Add("class", "active");
		}
	}
}

