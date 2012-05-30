using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

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

            if (!IsPostBack) { getGerente(); getRedes(); }

            ddlGerente.Focus();
        }

        private void getRedes()
        {
            DataSet ds = new DataSet();

            ds = o.GetRedes(scn);

            if (ds.Tables.Count > 0) {
                ddlRede.Items.Add(new ListItem(String.Empty, String.Empty));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlRede.Items.Add(new ListItem(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString()));
                }
                ddlRede.SelectedIndex = 0;
            }
        }

        private void getGerente()
        {
            DataSet ds = new DataSet();

            ds = o.GetUser(scn,"nvg");

            if (ds.Tables.Count > 0) {
                ddlGerente.Items.Add("");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListItem li = new ListItem(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
                    ddlGerente.Items.Add(li);
                }
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
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "26%");
            divInfo.InnerHtml = "<p>Rede cadastrada com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlGerente.SelectedValue == "") { divErro("Selecione o Gerente da Rede."); 
            } else if (txtRede.Text == "") { divErro("Entre com o nome da Rede."); } else {

                if (rede.RedeId > 0) {
                    rede.Gerente = ddlGerente.SelectedValue;
                    rede.RedeName = txtRede.Text;

                    //msg = o.UpdateRede(rede);
                    //ed = true;
                }
                rede = new SRV.Rede() { 
                    Gerente = ddlGerente.SelectedItem.Text,
                    RedeName = txtRede.Text,
                    UserId = Convert.ToInt32(ddlGerente.SelectedValue.ToString())
                };

                string msg = o.AddRede(scn, rede);

                if (msg == "") { divSces(); Clear(); } else { divErro(msg); }
            }
        }

        private void Clear()
        {
            ddlGerente.SelectedIndex = 0;
            txtRede.Text = "";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SRV.Rede or = new SRV.Rede();
            or = o.GetRedesEdit(scn, ddlRede.SelectedValue);

            if (or.RedeId > 0) {
                ddlGerente.SelectedValue = or.UserId.ToString();
                txtRede.Text = or.RedeName;
                Session["editR"] = or.RedeId;
            }
        }
    }
}