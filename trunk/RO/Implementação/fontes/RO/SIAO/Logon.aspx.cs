using System;
using SIAO.SRV;
using System.Configuration;
using System.Web.Security;

namespace SIAO
{
    public partial class Logon : System.Web.UI.Page
    {
        string cns = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblAlert.Visible = false;
            txtNome.Focus();
        }

        protected void btnEnter_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();

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
                clsAccess o = new clsAccess();

                u.AcsName = txtNome.Text;
                u.Password = txtSenha.Text;

                u = o.VerifAcesso(u, cns);

                if (u.UserId > 0)
                {
                    if (!u.Inactive && u.ExpirationDate > DateTime.Today)
                    {
                        Session["user"] = u;
                        FormsAuthentication.SetAuthCookie(u.Name, false);
                        Global.Acs = u.Access;

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
    }
}