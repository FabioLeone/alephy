using System;
using System.Web.UI;
using System.Xml;
using System.Configuration;
using System.Data;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;

namespace SIAO
{
    public partial class wfmEnvio : System.Web.UI.Page
    {
        UsersTO clsUser = new UsersTO();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); } else {
                clsUser = (UsersTO)Session["user"];
            }
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "error");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "31%");
            divInfo.InnerHtml = "<p>Arquivo enviado com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string msg = FilesBLL.LoadArquivo(clsUser,fuArquivo);

            if (String.IsNullOrEmpty(msg)) divSces();
            else divErro(msg);

        }
    }
}