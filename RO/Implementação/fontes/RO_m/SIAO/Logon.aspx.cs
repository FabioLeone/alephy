using System;
using System.Web;
using System.Web.UI;
using SIAO.SRV.BLL;
using System.Web.Security;
using SIAO.SRV.TO;

namespace SIAO
{
	
	public partial class Logon : System.Web.UI.Page
	{
		#region .: Eventos :.
		protected void Page_Load(object sender, EventArgs e)
		{
			txtNome.Focus();
		}

		protected void lbtnLogin_Click(object sender, EventArgs e)
		{
			UsersTO clsUser = UsersBLL.GetByNameAndPassword(txtNome.Text, txtSenha.Text);

			if (clsUser.UserId > 0)
			{
				if (!clsUser.Inactive && clsUser.ExpirationDate >= DateTime.Today)
				{
					UsersBLL.SetUserSession(clsUser);

					FormsAuthentication.SetAuthCookie(clsUser.UserName, false);

					UsersBLL.UpdateActivity(clsUser);

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

