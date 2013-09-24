using System;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.IO;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;

namespace SIAO
{
    public partial class wfmBanco : System.Web.UI.Page
    {
        SRV.clsFuncs of = new SRV.clsFuncs();
        SRV.clsControl oc = new SRV.clsControl();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsersBLL.GetUserSession().UserId == 0) { Response.Redirect("Logon.aspx"); }
            divSces();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string msg = "";

            if (fuArquivo.PostedFile.FileName == "")
            {
                divErro("Selecione um arquivo.");
            }
            else if (of.ValidaDBFExt(fuArquivo.PostedFile.FileName))
            {
                if (fuArquivo.HasFile)
                {
                    if (Directory.Exists(Server.MapPath("~/uploads")))
                    {
                        fuArquivo.SaveAs(Server.MapPath("~/uploads/") + System.IO.Path.GetFileName(fuArquivo.FileName));
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/uploads"));
                        fuArquivo.SaveAs(Server.MapPath("~/uploads/") + System.IO.Path.GetFileName(fuArquivo.FileName));
                    }
                    

                    DataTable dt = new DataTable();

                    try
                    {
                        using (System.Data.OleDb.OleDbConnection ocnn = new System.Data.OleDb.OleDbConnection())
                        {
                            ocnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/uploads") + ";Extended Properties=dBASE IV;";
                            ocnn.Open();

                            using (System.Data.OleDb.OleDbCommand ocmm = ocnn.CreateCommand())
                            {
                                ocmm.CommandText = @"SELECT * FROM " + fuArquivo.FileName;

                                dt.Load(ocmm.ExecuteReader());

                                ocnn.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    msg = oc.AddDbf(scn, dt);

                    if (msg == "")
                    {
                        divSces();
                    }
                    else { divErro(msg); }
                }
                else { divErro("Selecione apenas arquivos com extenção '.DBF'."); }
            }
            else
            {
                divErro("Selecione apenas arquivos com extenção '.DBF'.");
            }
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divError.Style.Add("bottom", "3.5%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "4.6%");
            divInfo.InnerHtml = "<p>Arquivo enviado com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }

        protected void btnIndices_Click(object sender, EventArgs e)
        {
            Global.LocalPage = "wfmBanco.aspx";
            Response.Redirect("wfmIndices.aspx");
        }

    }
}