using System;
using System.Web.UI;
using System.Xml;
using System.Configuration;
using System.Data;

namespace SIAO
{
    public partial class wfmEnvio : System.Web.UI.Page
    {
        SRV.clsFuncs of = new SRV.clsFuncs();
        SRV.clsControl oc = new SRV.clsControl();
        SRV.Usuario ou = new SRV.Usuario();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); } else {
                ou = (SRV.Usuario)Session["user"];
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
            if (fuArquivo.PostedFile.FileName == "") { 
                divErro("Selecione um arquivo."); 
            } else if (of.ValidaExt(fuArquivo.PostedFile.FileName)) {
                if (fuArquivo.HasFile)
                {
                    if (System.IO.Path.GetExtension(fuArquivo.PostedFile.FileName).ToUpper() == ".XML")
                    {
                        XmlDocument xd = new XmlDocument();

                        xd.Load(fuArquivo.FileContent);

                        string msg = oc.AddXml(scn, xd, ou);
                        if (msg == "")
                        {
                            divSces();
                        }
                        else { divErro(msg); }
                    }
                    else {
                        DataTable dt = new DataTable();
                        dt = of.txtDtConvert(fuArquivo.FileContent);

                        string msg = oc.AddTxt(scn, dt, ou);
                        if (msg == "")
                        {
                            divSces();
                        }
                        else { divErro(msg); }
                    }
                }
                else { divErro("Selecione apenas arquivos com extenção '.XML' ou '.TXT'."); }
            } else {
                divErro("Selecione apenas arquivos com extenção '.XML' ou '.TXT'.");
            }
        }
    }
}