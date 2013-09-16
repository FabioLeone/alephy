using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using SIAO.SRV;
using System.Web.UI.HtmlControls;

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
            {
                LoadTipos();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Vinculos = VinculoBLL.GetByTipoIdAndSearch(Convert.ToInt32(ddlTipos.SelectedValue), txtSearch.Text);
            LoadList();
        }

        protected void txtCNPJ_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCNPJ = sender as TextBox;
            String msg = String.Empty;

            if (txtCNPJ.Text.Equals("__.___.___/____-__"))
            {
                txtCNPJ.Focus();
                return;
            }

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
                        Literal litID = (Literal)item.FindControl("litID");

                        if (VinculoBLL.ValidaVinculo(txtCNPJ, hfUsuarioTipoId))
                            msg = VinculoBLL.SetVinculo(this.Vinculos, hfLinkId, litID, txtCNPJ, hfUsuarioTipoId);
                        else
                            return;

                        if (!String.IsNullOrEmpty(msg))
                        {
                            divErro(msg);
                            return;
                        }
                    }
                }
            }
            LoadList();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsuarioId.Text))
            {
                VinculoTO objVinculo = new VinculoTO();
                VinculoTO objAux = new VinculoTO();

                objAux = this.Vinculos.Find(v => v.UsuarioId == Convert.ToInt32(txtUsuarioId.Text));

                if (objAux != null)
                {
                    objVinculo.UsuarioId = Convert.ToInt32(txtUsuarioId.Text);
                    objVinculo.UserName = objAux.UserName;
                    objVinculo.TipoId = objAux.TipoId;
                    this.Vinculos.Add(objVinculo);
                    this.Vinculos.OrderBy(v => v.UsuarioId);

                    LoadList();
                }
                else
                {
                    divErro("Usuário não cadastrado!");
                    return;
                }
            }
        }

        protected void lbtnExcluir_Click(object sender, EventArgs e)
        {
            LinkButton lbtnExcluir = sender as LinkButton;

            foreach (ListViewDataItem item in lvwVinculos.Items)
            {
                foreach (Control ctrl in item.Controls)
                {
                    LinkButton lbtn = ctrl as LinkButton;
                    if (lbtn != null && lbtn.ClientID == lbtnExcluir.ClientID)
                    {
                        VinculoBLL.Delete(this.Vinculos.Find(v => v.id == Convert.ToInt32(lbtnExcluir.CommandArgument)));

                        this.Vinculos = VinculoBLL.GetByTipoIdAndSearch(Convert.ToInt32(ddlTipos.SelectedValue), txtSearch.Text);
                        LoadList();
                    }
                }
            }
        }

        protected void lvwVinculos_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            LinkButton lbtnExcluir = (LinkButton)e.Item.FindControl("lbtnExcluir");

            if (lbtnExcluir != null)
                lbtnExcluir.OnClientClick = "return confirm('Você deseja realmente excluir este registro?')";
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

        private void div(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "4.6%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            upVinculos.ContentTemplateContainer.Controls.Add(divInfo);
        }

        private void divErro(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgError";
            divInfo.Attributes.Add("class", "alerta");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "3.5%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            upVinculos.ContentTemplateContainer.Controls.Add(divInfo);
        }
        #endregion

    }
}