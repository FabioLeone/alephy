using System;
using System.Web;
using System.Web.UI;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Web.Security;

namespace SIAO
{
	public partial class Siao : System.Web.UI.MasterPage
	{
		#region .: Variables :.
		private UsersTO clsUser = new UsersTO();
		#endregion

		#region .: Properties :.
		public UsersTO User { get {
				if (this.ViewState["user"] == null) return this.clsUser;
				else return (UsersTO)this.ViewState["user"];
			}
			set {
				this.ViewState["user"] = value;
				this.clsUser = value;
			}
		}
		#endregion

		#region .: Metodos :.
		public bool CheckCss()
		{
			return UsersBLL.CheckCssRede(this.User);
		}

		public bool VerificaConfig() {
			return this.User.TipoId.Equals(1);
		}

		public bool VerificaRelatorio() {
			if (this.User.TipoId == null || this.User.TipoId == 0)
			{
				return false;
			}
			else
				return UsersBLL.ValidaRelatorio(this.User.TipoId);
		}

		public bool VerificaEnvio() {
			if (this.User.TipoId == null || this.User.TipoId == 0)
			{
				return false;
			}
			else
				return UsersBLL.ValidaEnvio(this.User.TipoId);
		}
		#endregion

		#region .: Events :.
		protected void lbtnLogoff_Click1(object sender, EventArgs e)
		{
			UsersBLL.ClearUserSession();
			FormsAuthentication.SignOut();
			Response.Redirect("Logon.aspx");
		}
		#endregion
	}
}

