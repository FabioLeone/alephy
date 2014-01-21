using Assemblies;
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
            if(!IsPostBack)
                txtName.Focus();
        }

        protected void smtLogin_ServerClick(object sender, EventArgs e)
        {
            string s = usersBLL.Authentication(Request.Form[txtName.UniqueID], Request.Form[txtPassword.UniqueID], cboxKeep.Checked);
            if (!string.IsNullOrEmpty(s))
                alert(s);
        }
        #endregion

        #region
        private void alert(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            div.ID = "msgError";
            div.Attributes.Add("class", "notice error");
            div.InnerHtml = string.Format(@"<i class='icon-remove-sign icon-large'></i>{0}
            <a href='#close' class='icon-remove'></a>", msg);

            form1.Controls.Add(div);
        }
        #endregion
    }
}