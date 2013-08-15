using System;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;

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
            txtNome.Focus();
        }

        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            UsersTO clsUser = UsersBLL.GetByNameAndPassword(txtNome.Text, txtSenha.Text, strConnectionString);

            if (clsUser.UserId > 0)
            {
                if (!clsUser.Inactive && clsUser.ExpirationDate > DateTime.Today)
                {
                    UsersBLL.SetUserSession(clsUser);
                    
                    FormsAuthentication.SetAuthCookie(clsUser.UserName, false);
                    Global.TId = clsUser.TipoId;

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    divErro("Prazo da senha expirou.");
                }
            }
            else
            {
                divErro("Usuário e/ou senha invalido(s).");
            }
        }
        #endregion

        #region .: Metodos :.
        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.InnerHtml = "<p>" + msg + "</p>";

            frmLogon.Controls.Add(divError);
        }
        #endregion
    }
}