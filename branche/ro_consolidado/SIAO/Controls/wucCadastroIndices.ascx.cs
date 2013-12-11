using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;
using System.Configuration;

namespace SIAO.Controls
{
    public partial class wucCadastroIndices : System.Web.UI.UserControl
    {
        #region .: Variaveis :.
        private IndicesGraficTO _indices;
        private string strConnection = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        #endregion

        #region .: Propriedades :.
        private IndicesGraficTO Indices
        {
            get
            {
                if (this.ViewState["Indices"] == null) return new IndicesGraficTO();
                else
                    return (IndicesGraficTO)this.ViewState["Indices"];
            }
            set
            {
                this.ViewState["Indices"] = value;
                this._indices = value;
            }
        }
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region .: Metodos :.
        public void PopulaDados(IndicesGraficTO clsIndices)
        {
            lbxGrupo.DataSource = GraficBLL.GetGrupos(strConnection);
            lbxGrupo.DataBind();

            lbxCategoria.DataSource = GraficBLL.GetCategorias(strConnection);
            lbxCategoria.DataBind();

            if (clsIndices.id > 0)
            {
                Indices = clsIndices;
                txtCategoria.Text = clsIndices.categoria;
                txtDesconto.Text = clsIndices.desconto.ToString("N2");
                txtGrupo.Text = clsIndices.grupo;
                txtVenda.Text = clsIndices.venda.ToString("N2");
            }
        }
        protected void lbxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGrupo.Text = lbxGrupo.SelectedItem.Text;
        }

        protected void lbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCategoria.Text = lbxCategoria.SelectedItem.Text;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            IndicesGraficTO clsIndices = ResgataDados();
            if (Indices.id > 0)
            {
                clsIndices.id = Indices.id;
                GraficBLL.UpdateIndiceGrafic(clsIndices, strConnection);
            }
            else
            {
                GraficBLL.InsertIndice(clsIndices, strConnection);
            }

            Response.Redirect("wfmIndices.aspx");
        }

        private IndicesGraficTO ResgataDados()
        {
            return new IndicesGraficTO()
            {
                categoria = txtCategoria.Text,
                desconto = Convert.ToDecimal(txtDesconto.Text),
                grupo = txtGrupo.Text,
                venda = Convert.ToDecimal(txtVenda.Text)
            };
        }
        #endregion
    }
}