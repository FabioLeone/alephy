using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Web.Security;
using SIAO.SRV;
using System.Collections.Generic;
using System.Data;

namespace SIAO
{
	public partial class Siao : System.Web.UI.MasterPage
	{
		#region .: Variables :.
		private UsersTO clsUser = new UsersTO();
		private RolesTO clsRole = new RolesTO();
		private Control dvRedes, dvLoja, dvFiltro;
		private HtmlGenericControl ulArq;
		private DropDownList ddlLojaRelatorios;
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

		public RolesTO Role { get {
				if (this.ViewState["role"] == null) return this.clsRole;
				else return (RolesTO)this.ViewState["role"];
			}
			set {
				this.ViewState["role"] = value;
				this.clsRole = value;
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

		private void getRedes()
		{
			DataSet ds = new DataSet();
			ds = clsControl.GetRedes();

			if (ds.Tables.Count > 0)
			{
				/*ddlRedesRelatorios.DataSource = ds.Tables[0];
				ddlRedesRelatorios.DataTextField = ds.Tables[0].Columns[1].ToString();
				ddlRedesRelatorios.DataValueField = ds.Tables[0].Columns[0].ToString();
				ddlRedesRelatorios.DataBind();
				ddlRedesRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
				ddlRedesRelatorios.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Independentes", "0"));
				ddlRedesRelatorios.SelectedIndex = 0;*/
			}
		}
		#endregion

		#region .: Events :.
		protected void lbtnLogoff_Click1(object sender, EventArgs e)
		{
			UsersBLL.ClearUserSession();
			FormsAuthentication.SignOut();
			Response.Redirect("Logon.aspx");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.User = UsersBLL.GetUserSession();
			if (this.User.UserId == 0) { Response.Redirect("Logon.aspx"); }

			dvRedes = FindControl("dvRedes");
			dvLoja = FindControl("dvLoja");
			dvFiltro = FindControl("dvFiltro");
			ddlLojaRelatorios = (DropDownList)FindControl("ddlLojaRelatorios");
			ulArq = (HtmlGenericControl)FindControl("ulArq");

			if (!IsPostBack)
			{ 
				if (UsersBLL.ValidaAcesso(this.User, dvRedes, dvLoja, dvFiltro))
				{
					LojasBLL.getLojas(this.User, ddlLojaRelatorios, dvFiltro, ulArq);
				}
				else
					getRedes();
			}
		}

		protected void ddlRedesRelatorios_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddlRedesRelatorios = (DropDownList)FindControl("ddlRedesRelatorios");

			if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
				LojasBLL.getLojas(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
		}

		protected void btnBusca_Click(object sender, EventArgs e)
		{
			LojasBLL.getFiles(ddlLojaRelatorios.SelectedValue, ulArq);
		}
		#endregion
	}
}

