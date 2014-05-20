using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Configuration;

namespace SIAO.Controls
{
    public partial class wucListaIndices : System.Web.UI.UserControl
    {
        #region .: Variables :.
        private string strConnection = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDados();
            }
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            gvwIndices.DataSource = GraficBLL.GetIndicesByFiltro(ddlGrupos.SelectedValue, ddlCategorias.SelectedValue, strConnection);
            gvwIndices.EmptyDataText = "Nenhum registro encontrado.";
            gvwIndices.DataBind();
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            LoadDados();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            IndicesGraficTO clsIndices = new IndicesGraficTO();
            wucCadastroIndices1.PopulaDados(clsIndices);
            Global.LocalPage = "wfmIndices.aspx";
            mvwIndices.ActiveViewIndex = 1;
        }

        protected void gvwIndices_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int intId = Convert.ToInt32(gvwIndices.DataKeys[e.RowIndex].Value);
            GraficBLL.DeleteIndiceGrafic(GraficBLL.GetIndicesById(intId, strConnection), strConnection);
            LoadDados();
        }

        protected void gvwIndices_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            int intId = Convert.ToInt32(gvwIndices.DataKeys[e.NewEditIndex].Value);
            wucCadastroIndices1.PopulaDados(GraficBLL.GetIndicesById(intId, strConnection));
            Global.LocalPage = "wfmIndices.aspx";
            mvwIndices.ActiveViewIndex = 1;
        }
        #endregion

        #region .: Metodos :.

        private void LoadDados()
        {
            ddlCategorias.DataSource = GraficBLL.GetCategorias(strConnection);
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem(string.Empty, ""));
            ddlCategorias.SelectedIndex = 0;

            ddlGrupos.DataSource = GraficBLL.GetGrupos(strConnection);
            ddlGrupos.DataBind();
            ddlGrupos.Items.Insert(0, new ListItem(string.Empty, ""));
            ddlGrupos.SelectedIndex = 0;

            gvwIndices.DataSource = GraficBLL.GetIndicesAll();
            gvwIndices.EmptyDataText = "Nenhum registro encontrado.";
            gvwIndices.DataBind();
        }

        #endregion

    }
}