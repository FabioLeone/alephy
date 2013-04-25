using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using SIAO.SRV;

namespace SIAO.Controls
{
    public partial class wucVinculos : System.Web.UI.UserControl
    {
        #region .:Variables:.
        public List<VinculoTO> lstVinculos = new List<VinculoTO>();
        #endregion

        #region .:Properties:.
        public List<VinculoTO> Vinculos
        {
            get
            {
                if (this.ViewState["vinculos"] == null) return this.lstVinculos;
                else
                    return (List<VinculoTO>)this.ViewState["vinculos"];
            }
            set
            {
                this.ViewState["vinculos"] = value;
                this.lstVinculos = value;
            }
        }
        #endregion

        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTipos();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Vinculos = VinculoBLL.GetByTipoIdAndSearch(Convert.ToInt32(ddlTipos.SelectedValue), txtSearch.Text);
            //LoadList();
            lvwVinculos.DataSource = this.Vinculos;
            lvwVinculos.DataBind();
        }
        protected void txtCNPJ_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCNPJ = sender as TextBox;
            VinculoTO clsVinculo;
            foreach (ListViewDataItem item in lvwVinculos.Items)
            {
                foreach (Control ctrl in item.Controls)
                {
                    TextBox txt = ctrl as TextBox;
                    if (txt != null && txt.ClientID == txtCNPJ.ClientID)
                    {
                        HiddenField hfVinculoId = (HiddenField)item.FindControl("hfVinculoId");
                        HiddenField hfUsuarioTipoId = (HiddenField)item.FindControl("hfUsuarioTipoId");
                        HiddenField hfLinkId = (HiddenField)item.FindControl("hfLinkId");
                        Literal litEmpresa = (Literal)item.FindControl("litEmpresa");

                        clsVinculo = new VinculoTO();
                        clsVinculo = this.Vinculos.Find(v => v.LinkId == Convert.ToInt32(hfVinculoId.Value));
                        clsVinculo.CNPJ = txtCNPJ.Text;

                        switch (Convert.ToInt32(hfUsuarioTipoId.Value))
                        {
                            case 2:
                                {
                                    Loja clsLoja = clsControl.GetLojaByCNPJ(txtCNPJ.Text);
                                    clsVinculo.Empresa = clsLoja.NomeFantasia;
                                    clsVinculo.LinkId = clsLoja.Id;
                                    break;
                                }
                            case 3:
                                {
                                    Rede clsRede = clsControl.GetRedeByCNPJ(txtCNPJ.Text);
                                    clsVinculo.Empresa = clsRede.RedeName;
                                    clsVinculo.LinkId = clsRede.RedeId;
                                    break;
                                }
                        }

                        if (clsVinculo.id > 0)
                            VinculoBLL.Update(clsVinculo);
                        else
                            VinculoBLL.Insert(clsVinculo);
                    }
                }
            }
            LoadList();
        }
        protected void lvwVinculos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            i++;
        }
        #endregion

        #region .:Methods:.

        private void LoadTipos()
        {
            ddlTipos.DataSource = UsersBLL.GetTiposAll();
            ddlTipos.DataTextField = "Tipo";
            ddlTipos.DataValueField = "id";
            ddlTipos.DataBind();

            ddlTipos.Items.Insert(0, new ListItem("Selecione", "0"));
            ddlTipos.SelectedIndex = 0;
        }

        private void LoadList()
        {
            lvwVinculos.DataSource = this.Vinculos;
            lvwVinculos.DataBind();
        }
        #endregion





    }
}