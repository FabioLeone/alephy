using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;

namespace SIAO.Controls
{
    public partial class wucCadastroUser : System.Web.UI.UserControl
    {
        #region .: Variables :.
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        SRV.clsFuncs of = new SRV.clsFuncs();
        SRV.clsControl oCtl = new SRV.clsControl();
        UsersTO clsUser = new UsersTO();
        #endregion

        #region .: Properties :.
        public UsersTO User 
        {
            get {
                if (this.ViewState["Users"] == null) return new UsersTO();
                else return (UsersTO)this.ViewState["Users"];
            }
            set {
                this.ViewState["Users"] = value;
                this.clsUser = value;
            }
        }
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack)
            {
                clear();
                LoadData();
            }
            txtNome.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                if (lblAN.Visible) { lblAN.Visible = false; }
                if (lblN.Visible) { lblN.Text = "*"; } else { lblN.Text = "*"; lblN.Visible = true; }
                divErro("Entre com o nome do usuário.");
            }
            else if (txtAcsName.Text == "")
            {
                if (lblN.Visible) { lblN.Visible = false; }
                if (lblAN.Visible) { lblAN.Text = "*"; } else { lblAN.Text = "*"; lblAN.Visible = true; }
                if (lblE.Visible) { lblE.Visible = false; }
                divErro("Entre com o nome de acesso.");
            }
            else if (!of.isEmail(txtEmail.Text))
            {
                if (lblN.Visible) { lblN.Visible = false; }
                if (lblAN.Visible) { lblAN.Visible = false; }
                if (lblE.Visible) { lblE.Text = "*"; } else { lblE.Text = "*"; lblE.Visible = true; }
                divErro("Entre com o e-mail do usuário.");
            }
            else if (txtValidade.Text != "")
            {
                if (lblS.Visible) { lblS.Visible = false; }

                if (this.User.UserId == 0 && txtSenha.Text == "")
                {
                    if (lblV.Visible) { lblV.Visible = false; }
                    if (lblE.Visible) { lblE.Visible = false; }
                    if (lblS.Visible) { lblS.Text = "*"; } else { lblS.Text = "*"; lblS.Visible = true; }
                    divErro("Entre com a senha do usuário.");
                }
                else if (txtSenha.Text != txtCfrSenha.Text)
                {
                    if (lblCS.Visible) { lblCS.Text = "*"; } else { lblCS.Text = "*"; lblCS.Visible = true; }
                    divErro("Senha e confirmação, não conferem.");
                }
                else if(ddlAcess.SelectedIndex == 0){
                    if (lblAcs.Visible) { lblAcs.Text = "*"; } else { lblAcs.Text = "*"; lblCS.Visible = true; }
                    divErro("Selecione o tipo de acesso.");
                }
                else
                {

                    if (lblV.Visible) { lblV.Visible = false; }
                    if (lblAcs.Visible) lblAcs.Visible = false;
                    try
                    {
                        Convert.ToDateTime(txtValidade.Text);
                    }
                    catch
                    {
                        if (lblV.Visible) { lblE.Text = "*"; } else { lblV.Text = "*"; lblV.Visible = true; }
                        divErro("Data inválida.");
                        return;
                    }

                    string msg = "";

                    bool ed = false;

                    if (this.User.UserId > 0)
                    {
                        clsUser.UserId = this.User.UserId;
                        clsUser.TipoId = Convert.ToInt32(ddlAcess.SelectedValue);
                        clsUser.Name = txtAcsName.Text;
                        clsUser.CreateDate = DateTime.Today;
                        clsUser.Email = txtEmail.Text;
                        clsUser.Inactive = !cbxAtivo.Checked;
                        clsUser.UserName = txtNome.Text;

                        if (!string.IsNullOrEmpty(txtSenha.Text))
                            clsUser.Password = txtSenha.Text;
                        else
                            clsUser.Password = this.User.Password;

                        clsUser.ExpirationDate = Convert.ToDateTime(txtValidade.Text);

                        msg = oCtl.UpdateUser(clsUser, scn);
                        ed = true;
                    }
                    else
                    {
                        clsUser = new UsersTO()
                        {
                            TipoId = Convert.ToInt32(ddlAcess.SelectedValue),
                            Name = txtAcsName.Text,
                            CreateDate = DateTime.Today,
                            Email = txtEmail.Text,
                            Inactive = !cbxAtivo.Checked,
                            UserName = txtNome.Text,
                            Password = txtSenha.Text,
                            ExpirationDate = Convert.ToDateTime(txtValidade.Text),
                        };
                        msg = oCtl.AddUser(clsUser, scn);
                    }

                    if (msg != "")
                    {
                        divErro(msg);
                    }
                    else
                    {
                        clear();

                        if (ed)
                        {
                            div("Alterações salvas com sucesso."); Global.LocalPage = string.Empty;
                            Response.Redirect("wfmUsers.aspx");
                        }
                        else
                        {
                            div("Usuário cadastrado com sucesso."); Global.LocalPage = string.Empty;
                            Response.Redirect("wfmUsers.aspx");
                        }
                    }
                }
            }
            else
            {
                if (lblS.Visible) { lblS.Visible = false; }
                if (lblV.Visible) { lblV.Text = "*"; } else { lblV.Text = "*"; lblV.Visible = true; }
                divErro("Entre com a data de validade da senha.");
            }
        }

        #endregion

        #region .: Methods :.
        private void clear()
        {
            txtNome.Text = "";
            txtAcsName.Text = string.Empty;
            txtSenha.Text = "";
            txtValidade.Text = "";
            txtCfrSenha.Text = "";
            txtEmail.Text = "";
            Session["editU"] = null;
        }
        private void div(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "28%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            Panel1.Controls.Add(divInfo);
        }

        private void divErro(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgError";
            divInfo.Attributes.Add("class", "error");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            Panel1.Controls.Add(divInfo);
        }
        public void PopulaDados(UsersTO clsUser) {
            if (clsUser.UserId > 0)
            {
                cbxAtivo.Checked = (bool)(clsUser.Inactive.ToString().ToUpper() == "FALSE" ? true : false);
                txtAcsName.Text = clsUser.Name;
                txtEmail.Text = clsUser.Email;
                txtNome.Text = clsUser.UserName;
                txtValidade.Text = clsUser.ExpirationDate.ToShortDateString();
                txtSenha.Text = clsUser.Password;
                txtCfrSenha.Text = clsUser.Password;
                ddlAcess.SelectedValue = clsUser.TipoId.ToString();
                this.User = clsUser;
            }
        }
        private void LoadData()
        {
            ddlAcess.DataSource = UsersBLL.GetTiposAll();
            ddlAcess.DataTextField = "Tipo";
            ddlAcess.DataValueField = "id";
            ddlAcess.DataBind();
            ddlAcess.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlAcess.SelectedIndex = 0;
        }
        #endregion
    }
}