using Assemblies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class wucSending_file : System.Web.UI.UserControl
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_ServerClick(object sender, EventArgs e)
        {
            string msg = buy_baseBLL.upFile(upfile.PostedFile);

            if (String.IsNullOrEmpty(msg)) success();
            else alert(msg);
        }
        #endregion

        #region .:Methods:.
        private void alert(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            div.ID = "msgError";
            div.Attributes.Add("class", "notice error");
            div.InnerHtml = string.Format(@"<i class='icon-remove-sign icon-large'></i>{0}
            <a href='#close' class='icon-remove'></a>", msg);

            Page.Controls.Add(div);
        }

        private void success()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            div.ID = "msgSuccess";
            div.Attributes.Add("class", "notice success");
            div.InnerHtml = @"<i class='icon-remove-sign icon-large'></i>Envio realizado com sucesso.
            <a href='#close' class='icon-remove'></a>";

            Page.Controls.Add(div);
        }
        #endregion
    }
}