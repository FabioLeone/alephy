using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Configuration;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Data;

namespace SIAO.Controls
{

	public partial class wucListaUsers : System.Web.UI.UserControl
	{
		#region .: Variables :.
		private string strConnection = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
		#endregion

		#region .: Events :.
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadDados();
				Local.LocalPage = String.Empty;
			}
		}

		protected void btnMostrarTodos_Click(object sender, EventArgs e)
		{
			txtNome.Text = string.Empty;
			LoadDados();
		}

		protected void btnFiltro_Click(object sender, EventArgs e)
		{
			gvwUsers.DataSource = UsersBLL.GetIndicesByFiltro(txtNome.Text);
			gvwUsers.EmptyDataText = "Nenhum registro encontrado.";
			gvwUsers.DataBind();
		}

		protected void gvwUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			int intId = Convert.ToInt32(gvwUsers.DataKeys[e.RowIndex].Value);
			UsersBLL.Delete(UsersBLL.GetById(intId), strConnection);
			LoadDados();
		}

		protected void gvwUsers_RowEditing(object sender, GridViewEditEventArgs e)
		{
			int intId = Convert.ToInt32(gvwUsers.DataKeys[e.NewEditIndex].Value);
			wucCadastroUser1.PopulaDados(UsersBLL.GetById(intId));
			mvwUsers.ActiveViewIndex = 1;
		}

		protected void btnNovo_Click(object sender, EventArgs e)
		{
			UsersTO clsUsers = new UsersTO();
			wucCadastroUser1.PopulaDados(clsUsers);
			mvwUsers.ActiveViewIndex = 1;
		}
		protected void gvwUsers_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			Literal ltlTipo = (Literal)e.Row.FindControl("ltlTipo");

			if (e.Row.RowIndex > -1)
			{
				int intUserId = (int)gvwUsers.DataKeys[e.Row.RowIndex].Value;
				UsersTO clsUser = UsersBLL.GetById(intUserId);

				if(clsUser.TipoId == 0)
					switch (clsUser.Access)
				{
				case "nvg": ltlTipo.Text = "Gerente"; break;
				case "nvp": ltlTipo.Text = "Proprietário"; break;
				case "adm": ltlTipo.Text = "Administrador"; break;
				}

				foreach (DataControlFieldCell cell in e.Row.Cells)
				{
					foreach (Control control in cell.Controls)
					{
						ImageButton del = control as ImageButton;
						if (del != null && del.CommandName == "Delete")
						{
							del.OnClientClick = "if (!confirm('Você deseja realmente excluir este usuário?')) return;";
						}
					}
				}
			}
		}
		protected void gvwUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			UsersBLL.GetList(gvwUsers);
			gvwUsers.PageIndex = e.NewPageIndex;
			gvwUsers.DataBind();
		}

		#endregion

		#region .: Metodos :.
		private void LoadDados()
		{
			UsersBLL.GetList(gvwUsers);
		}

		#endregion
	}
}

