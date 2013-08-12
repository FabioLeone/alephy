using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using SIAO.SRV;

namespace SIAO
{
    public partial class wfmCadastroRede : System.Web.UI.Page
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        SRV.clsControl o = new SRV.clsControl();
        SRV.Rede rede = new SRV.Rede();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            if (Session["editR"] != null) { rede.RedeId = (int)Session["editR"]; }

            if (!IsPostBack) { getRedes(); }

        }

        private void getRedes()
        {
            DataSet ds = new DataSet();

            ds = clsControl.GetRedes(scn);

            if (ds.Tables.Count > 0)
            {
                ddlRede.Items.Add(new ListItem(String.Empty, String.Empty));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlRede.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString()));
                }
                ddlRede.SelectedIndex = 0;
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

        private void divSces(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "4.6%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtRede.Text == "") { divErro("Entre com o nome da Rede."); }
            else
            {
                string msg = "";

                bool ed = false;

                if (rede.RedeId > 0)
                {
                    rede.RedeName = txtRede.Text;

                    string scnpj = txtCNPJ.Text.Replace(".", "");
                    scnpj = scnpj.Replace("/", "");
                    scnpj = scnpj.Replace("-", "");

                    rede.CNPJ = scnpj;

                    msg = o.UpdateRede(scn, rede);
                    ed = true;
                }
                else
                {
                    string scnpj = txtCNPJ.Text.Replace(".", "");
                    scnpj = scnpj.Replace("/", "");
                    scnpj = scnpj.Replace("-", "");

                    rede = new SRV.Rede()
                    {
                        RedeName = txtRede.Text,
                        CNPJ = scnpj
                    };

                    if (o.GetByName(txtRede.Text) > 0)
                        msg = "Rede já cadastrada.";
                    else
                        msg = o.AddRede(scn, rede);
                }

                if (msg == "")
                {
                    if (ed) { divSces("Alterações salvas com sucesso."); }
                    else
                    {
                        divSces("Rede cadastrada com sucesso.");
                    }

                    Clear();
                }
                else { divErro(msg); }
            }
        }

        private void Clear()
        {
            txtRede.Text = "";
            ddlRede.SelectedIndex = 0;
            txtCNPJ.Text = "";
            Session["editR"] = null;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SRV.Rede or = new SRV.Rede();
            or = o.GetRedesEdit(scn, ddlRede.SelectedValue);

            if (or.RedeId > 0)
            {
                txtRede.Text = or.RedeName;
                Session["editR"] = or.RedeId;
                txtCNPJ.Text = or.CNPJ;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}