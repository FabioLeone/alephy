using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;
using System.Data;
using SIAO.SRV;

namespace SIAO
{
    public partial class wfmGerarRelatorios : System.Web.UI.Page
    {
        #region .: Variables :.
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        UsersTO clsUser = new UsersTO();
        SRV.clsControl oc = new SRV.clsControl();
        Boolean blnTodos;
        List<RelatoriosTO> clsRelatorios = new List<RelatoriosTO>();
        int intRedeId = 0;
        #endregion

        #region .: Properties :.
        public Boolean Todos
        {
            get
            {
                if (this.ViewState["todos"] == null) return this.blnTodos;
                else return (Boolean)this.ViewState["todos"];
            }
            set
            {
                this.ViewState["todos"] = value;
                this.blnTodos = value;
            }
        }
        public List<RelatoriosTO> Relatorios
        {
            get
            {
                if (this.ViewState["relatorios"] == null) return this.clsRelatorios;
                else return (List<RelatoriosTO>)this.ViewState["relatorios"];
            }
            set
            {
                this.ViewState["relatorios"] = value;
                this.clsRelatorios = value;
            }
        }
        public UsersTO User
        {
            get
            {
                if (this.ViewState["user"] == null) return this.clsUser;
                else return (UsersTO)this.ViewState["user"];
            }
            set
            {
                this.ViewState["user"] = value;
                this.clsUser = value;
            }
        }
        public int RedeId
        {
            get
            {
                if (this.ViewState["redeId"] == null) return this.intRedeId;
                else return (int)this.ViewState["redeId"];
            }
            set
            {
                this.ViewState["redeId"] = value;
                this.intRedeId = value;
            }
        }
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                this.User = clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack)
            {
                getRedes();
                if (!clsUser.TipoId.Equals(1))
                    ValidaAcesso();
                getLojas();
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
            Global.LocalPage = "";
            Control ul = Master.FindControl("navlist");

            foreach (Control item in ul.Controls)
            {
                if (item != null)
                    ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
            }

            Control li = Master.FindControl("l3");
            if (li != null)
                ((System.Web.UI.HtmlControls.HtmlControl)li).Attributes.Add("class", "active");

        }

        protected void btnAdm_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();
            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    lr1 = RelatoriosBLL.GetCross(clsUser, txtInicio.Text, txtFim.Text, this.RedeId);
                else
                    lr1 = RelatoriosBLL.GetCross(clsUser, txtInicio.Text, txtFim.Text, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    lr1 = RelatoriosBLL.GetCross(clsUser, this.RedeId);
                else
                    lr1 = RelatoriosBLL.GetCross(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));
            }

            if (lr1.Count > 0)
            {
                Session["cross"] = lr1;

                Global.LocalPage = "wfmGerarRelatorios.aspx";

                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Modelo1",
                    UserId = this.User.UserId
                }, scn);

                clsFuncs.Redirect("wfmRelatMod1.aspx","_blank", "");
            }
            else
            {
                divErro("Não há itens a serem listados.");
            }
        }

        protected void btnRelat2_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();
            if (this.RedeId > 0)
                lr1 = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, this.RedeId, rbtPeriodo, rbtMes);
            else
                lr1 = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, ddlRedesRelatorios, ddlLojaRelatorios, rbtPeriodo, rbtMes);
            
            if (lr1.Count > 0)
            {
                Session["cross"] = lr1;

                Global.LocalPage = "wfmGerarRelatorios.aspx";

                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Modelo2",
                    UserId = this.User.UserId
                }, scn);

                clsFuncs.Redirect("wfmRelatMod2.aspx", "_blank", "");
            }
            else
            {
                divErro("Não há itens a serem listados.");
            }
        }

        protected void ibtnGrafic1_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, txtFim.Text, this.RedeId);
                else
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, ddlLojaRelatorios.SelectedValue, txtFim.Text);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, string.Empty, this.RedeId);
                else
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, ddlLojaRelatorios.SelectedValue, string.Empty);
            }

            Session["grafic"] = clsGrafic;

            Global.LocalPage = "wfmGerarRelatorios.aspx";
            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico1",
                    UserId = this.User.UserId
                }, scn);

            clsFuncs.Redirect("wfmRelatorio.aspx", "_blank", "");
        }

        protected void ibtnGrafic2_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, txtFim.Text, this.RedeId);
                else
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, ddlLojaRelatorios.SelectedValue, txtFim.Text);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, string.Empty, this.RedeId);
                else
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, ddlLojaRelatorios.SelectedValue, string.Empty);
            }

            Session["grafic"] = null;
            Session["grafic2"] = clsGrafic;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico2",
                    UserId = this.User.UserId
                }, scn);

            clsFuncs.Redirect("wfmRelatorio.aspx", "_blank", "");
        }

        protected void lblGrafic3_Click(object sender, ImageClickEventArgs e)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic2 = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic3 = new List<Grafic2TO>();

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId);
                }
                else
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodo(txtInicio.Text, txtFim.Text, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodo(txtInicio.Text, txtFim.Text, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodo(txtInicio.Text, txtFim.Text, clsUser, ddlLojaRelatorios.SelectedValue);
                }
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId);
                }
                else
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                }
            }

            Session["grafic"] = null;
            Session["grafic2"] = null;
            Session["grafic31"] = clsGrafic;
            Session["grafic32"] = clsGrafic2;
            Session["grafic33"] = clsGrafic3;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico3",
                    UserId = this.User.UserId
                }, scn);

            clsFuncs.Redirect("wfmRelatorio.aspx", "_blank", "");

        }

        protected void rbtPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPeriodo.Checked)
            {
                txtInicio.Enabled = true;
                txtFim.Enabled = true;
            }
            else
            {
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
        }
        protected void rbtMes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtMes.Checked)
            {
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
            else
            {
                txtInicio.Enabled = true;
                txtFim.Enabled = true;
            }
        }

        protected void ddlRedesRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
                getLojas(Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
        }
        #endregion

        #region .: Methods :.
        private void getRedes()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes(scn);

            if (ds.Tables.Count > 0)
            {
                ddlRedesRelatorios.DataSource = ds.Tables[0];
                ddlRedesRelatorios.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedesRelatorios.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedesRelatorios.DataBind();
                ddlRedesRelatorios.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRedesRelatorios.Items.Insert(1, new ListItem("Independentes", "0"));
                ddlRedesRelatorios.SelectedIndex = 0;
            }
        }

        private void ValidaAcesso()
        {
            if (this.User.TipoId.Equals(3))
            {
                ddlLojaRelatorios.Visible = false;
                dvLoja.Visible = false;
                trFiltro.Visible = false;
            }
            else
            {
                ddlLojaRelatorios.Visible = true;
                dvLoja.Visible = true;
                trFiltro.Visible = true;
            }
            dvRedes.Visible = false;
        }

        private void getLojas()
        {
            switch (this.User.TipoId)
            {
                case 2:
                    ddlLojaRelatorios.DataSource = oc.GetLojaByUserId(clsUser.UserId);
                    ddlLojaRelatorios.DataTextField = "NomeFantasia";
                    ddlLojaRelatorios.DataValueField = "Cnpj";
                    ddlLojaRelatorios.DataBind();
                    ddlLojaRelatorios.Items.Insert(0, new ListItem("Todas", string.Empty));
                    ddlLojaRelatorios.SelectedIndex = 0;
                    break;
                case 3:
                    this.RedeId = clsControl.GetRedeByUserId(this.User.UserId).RedeId;
                    getLojas(this.RedeId);
                    break;
            }
        }

        private void getLojas(int intRedeId)
        {
            ddlLojaRelatorios.DataSource = oc.GetLojaByRedeId(intRedeId);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divError.Style.Add("bottom", "3.5%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom","4.6%");
            divInfo.InnerHtml = "<p>Loja cadastrada com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }
        #endregion

    }
}