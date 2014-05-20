using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIAO.SRV;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;

namespace SIAO.Controls
{
    public partial class wucFilter : System.Web.UI.UserControl
    {
        #region .: Variables :.
        UsersTO clsUser = new UsersTO();
        int intRedeId = 0;
        #endregion

        #region .: Properties :.
        public UsersTO User
        {
            get
            {
                if (this.ViewState["user"] == null) return this.clsUser;
                else return (UsersTO)this.ViewState["user"];
            }
            set
            {
                this.ViewState["user"] = value;
                this.clsUser = value;
            }
        }

        public int RedeId
        {
            get
            {
                if (this.ViewState["redeId"] == null) return this.intRedeId;
                else return (int)this.ViewState["redeId"];
            }
            set
            {
                this.ViewState["redeId"] = value;
                this.intRedeId = value;
            }
        }
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            this.User = UsersBLL.GetUserSession();

            if (this.User.UserId == 0) { Response.Redirect("Logon.aspx"); }
            
            if (!IsPostBack)
            {
                getRedes();
                if (!clsUser.TipoId.Equals(1))
                    ValidaAcesso();
                
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
        }

        protected void rbtPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPeriodo.Checked)
            {
                txtInicio.Enabled = true;
                txtFim.Enabled = true;
            }
            else
            {
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
        }
       
        protected void rbtMes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMes.Checked)
            {
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
            else
            {
                txtInicio.Enabled = true;
                txtFim.Enabled = true;
            }
        }

        protected void ddlRedesRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
                getLojas(Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
        }
        #endregion

        #region .: Methods :.
        private void getRedes()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes();

            if (ds.Tables.Count > 0)
            {
                ddlRedesRelatorios.DataSource = ds.Tables[0];
                ddlRedesRelatorios.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedesRelatorios.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedesRelatorios.DataBind();
                ddlRedesRelatorios.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRedesRelatorios.Items.Insert(1, new ListItem("Independentes", "0"));
                ddlRedesRelatorios.SelectedIndex = 0;
            }
        }

        private void getLojas()
        {
            switch (this.User.TipoId)
            {
                case 2:
                    ddlLojaRelatorios.DataSource = clsControl.GetLojaByUserId(clsUser.UserId);
                    ddlLojaRelatorios.DataTextField = "NomeFantasia";
                    ddlLojaRelatorios.DataValueField = "Cnpj";
                    ddlLojaRelatorios.DataBind();
                    ddlLojaRelatorios.Items.Insert(0, new ListItem("Todas", string.Empty));
                    ddlLojaRelatorios.SelectedIndex = 0;
                    break;
                case 3:
                    this.RedeId = clsControl.GetRedeByUserId(this.User.UserId).RedeId;
                    getLojas(this.RedeId);
                    break;
            }
        }

        private void getLojas(int intRedeId)
        {
            ddlLojaRelatorios.DataSource = clsControl.GetLojaByRedeId(intRedeId);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divError.Style.Add("bottom", "3.5%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            dvFilter.Controls.Add(divError);
        }

        public ListItemCollection ResultData()
        {
            ListItemCollection lic = new ListItemCollection();

            if (ddlRedesRelatorios.Visible && String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
            {
                divErro("Selecione a rede!");
                return lic;
            }
            else
                lic.Add(new ListItem("rede", ddlRedesRelatorios.SelectedValue));

            lic.Add(new ListItem("loja", ddlLojaRelatorios.SelectedValue));

            if (rbtPeriodo.Checked)
            {
                if (txtInicio.Text != "__/____" && txtFim.Text != "__/____")
                {
                    lic.Add(new ListItem("de", txtInicio.Text));
                    lic.Add(new ListItem("ate", txtFim.Text));
                }
                else {
                    divErro("Preencha os campos \"de\" e \"até\"!");
                    return lic;
                }
            }
            else if (rbtMes.Checked)
            {
                lic.Add(new ListItem("de", String.Empty));
                lic.Add(new ListItem("ate", String.Empty));
            }
            else
                divErro("Selecione o periodo!");

            return lic;
        }

        private void ValidaAcesso()
        {
            if (this.User.TipoId.Equals(3))
            {
                ddlLojaRelatorios.Visible = false;
                dvLoja.Visible = false;
                dvFiltro.Visible = false;
            }
            else
            {
                ddlLojaRelatorios.Visible = true;
                dvLoja.Visible = true;
                dvFiltro.Visible = true;
            }
            dvRedes.Visible = false;
        }
        #endregion
    }
}