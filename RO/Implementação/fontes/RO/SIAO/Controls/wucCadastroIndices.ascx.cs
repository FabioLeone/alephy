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
        private IndicesGraficTO Indices {
            get {
                if (this.ViewState["Indices"] == null) return new IndicesGraficTO();
                else
                    return (IndicesGraficTO)this.ViewState["Indices"];
            }
            set {
                this.ViewState["Indices"] = value;
                this._indices = value;
            }
        }
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void PopulaDados(IndicesGraficTO clsIndices) {
            List<IndicesGraficTO> lstIndices = GraficBLL.GetIndicesAll(strConnection);

            ddlCadastro.DataSource = lstIndices;
        }
        #endregion
    }
}