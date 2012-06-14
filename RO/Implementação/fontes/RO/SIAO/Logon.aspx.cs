using System;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Configuration;
using System.Web.Security;

namespace SIAO
{
    public partial class Logon : System.Web.UI.Page
    {
        #region .: Varables :.
        private string strConnectionString = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAlert.Visible = false;
            txtNome.Focus();
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                lblAlert.Visible = true;
                lblAlert.Text = "Entre com o nome.";
            }
            else if (txtSenha.Text == "")
            {
                lblAlert.Visible = true;
                lblAlert.Text = "Entre com a senha.";
                txtSenha.Focus();
            }
            else
            {
                UsersTO clsUser = UsersBLL.GetByNameAndPassword(txtNome.Text, txtSenha.Text, strConnectionString);

                if (clsUser.UserId > 0)
                {
                    if (!clsUser.Inactive && clsUser.ExpirationDate > DateTime.Today)
                    {
                        Session["user"] = clsUser;
                        FormsAuthentication.SetAuthCookie(clsUser.UserName, false);
                        Global.Acs = clsUser.Access;

                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        lblAlert.Visible = true;
                        lblAlert.Text = "Prazo da senha expirou.";
                    }
                }
                else
                {
                    lblAlert.Visible = true;
                    lblAlert.Text = "Usuário e/ou senha invalido(s).";
                }
            }
        }
        #endregion
    }
}