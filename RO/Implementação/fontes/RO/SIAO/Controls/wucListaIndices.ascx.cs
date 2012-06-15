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
            int intId = 0;
            if (Convert.ToInt32(ddlGrupos.SelectedValue) > 0)
            {
                intId = Convert.ToInt32(ddlGrupos.SelectedValue);
            }
            else if (Convert.ToInt32(ddlCategorias.SelectedValue) > 0) {
                intId = Convert.ToInt32(ddlCategorias.SelectedValue);
            }

            gvwIndices.DataSource = GraficBLL.GetIndicesById(intId, strConnection);
            gvwIndices.EmptyDataText = "Nenhum registro encontrado.";
            gvwIndices.DataBind();
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {

        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region .: Metodos :.

        private void LoadDados()
        {
            List<IndicesGraficTO> clsIndicesGrafic = GraficBLL.GetIndicesAll(strConnection);

            ddlCategorias.DataSource = clsIndicesGrafic;
            ddlCategorias.DataTextField = "categoria";
            ddlCategorias.DataValueField = "id";
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem(string.Empty, "0"));
            ddlCategorias.SelectedIndex = 0;

            ddlGrupos.DataSource = clsIndicesGrafic;
            ddlGrupos.DataTextField = "grupo";
            ddlGrupos.DataValueField = "id";
            ddlGrupos.DataBind();
            ddlGrupos.Items.Insert(0, new ListItem(string.Empty, "0"));
            ddlGrupos.SelectedIndex = 0;

            gvwIndices.DataSource = clsIndicesGrafic;
            gvwIndices.DataBind();
        }

        #endregion
    }
}