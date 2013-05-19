﻿using System;
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
        public int RedeId { get{
            if(this.ViewState["redeId"] == null) return this.intRedeId;
            else return (int)this.ViewState["redeId"];
        }
            set {
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
                getAno();
                getRedes();
                if (!clsUser.TipoId.Equals(1))
                    ValidaAcesso();
                getLojas();
                txtInicio.Enabled = false;
                txtFim.Enabled = false;
            }
            Global.LocalPage = "";

        }

        protected void btnAdm_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();
            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    lr1 = oc.GetCross(clsUser, txtInicio.Text, txtFim.Text, this.RedeId);
                else
                    lr1 = oc.GetCross(clsUser, txtInicio.Text, txtFim.Text, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    lr1 = oc.GetCross(clsUser, this.RedeId);
                else
                    lr1 = oc.GetCross(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
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

                Response.Redirect("wfmRelatMod1.aspx");
            }
        }

        protected void btnRelat2_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();
            if (rbtPeriodo.Checked)
            {
                if (ddlRedesRelatorios.SelectedIndex > 0)
                    lr1 = oc.GetCross(scn, clsUser, "2013", Convert.ToInt32(ddlRedesRelatorios.SelectedItem.Value), false);
                else
                    lr1 = oc.GetCross(clsUser, txtInicio.Text, txtFim.Text, ddlLojaRelatorios.SelectedItem.Value, 0);

            }
            else if (rbtMes.Checked)
            {
                if (ddlRedesRelatorios.SelectedIndex > 0)
                    lr1 = oc.GetCross(scn, clsUser, String.Empty, Convert.ToInt32(ddlRedesRelatorios.SelectedItem.Value), true);
                else
                    lr1 = oc.GetCross(clsUser, ddlLojaRelatorios.SelectedItem.Value, 0);
            }

            if (lr1.Count > 0)
            {
                Session["cross"] = lr1;

                Global.LocalPage = "wfmGerarRelatorios.aspx";

                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Modelo2",
                    UserId = this.User.UserId
                }, scn);

                Response.Redirect("wfmRelatMod2.aspx");
            }
        }

        protected void ibtnGrafic1_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic;
            int intAno = 0;
            if (ddlAnoG.SelectedValue != "")
                intAno = Convert.ToInt32(ddlAnoG.SelectedValue);
            else
                intAno = Convert.ToInt32(DateTime.Now.Year);

            if (ddlMes.SelectedValue != "")
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(ddlMes.SelectedValue), clsUser, scn, ddlLojas.SelectedValue, intAno);
            else
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(DateTime.Now.Month), clsUser, scn, ddlLojas.SelectedValue, intAno);

            Session["grafic"] = clsGrafic;

            Global.LocalPage = "wfmGerarRelatorios.aspx";
            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico1",
                    UserId = this.User.UserId
                }, scn);

            Response.Redirect("wfmRelatorio.aspx");
        }

        protected void ibtnGrafic2_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic;

            if (ddlAnoG.SelectedValue != "")
                clsGrafic = GraficBLL.GraficListByAno(Convert.ToInt32(ddlAnoG.SelectedValue), clsUser, scn, ddlLojas.SelectedValue);
            else
                clsGrafic = GraficBLL.GraficListByAno(Convert.ToInt32(DateTime.Now.Year), clsUser, scn, ddlLojas.SelectedValue);

            Session["grafic"] = null;
            Session["grafic2"] = clsGrafic;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico2",
                    UserId = this.User.UserId
                }, scn);

            Response.Redirect("wfmRelatorio.aspx");
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
                ddlLojaRelatorios.Visible = false;
            else
                ddlLojaRelatorios.Visible = true;

            dvRedes.Visible = false;
        }

        private void getLojas()
        {
            DataSet dsLojas = oc.GetLojaByUserId(clsUser.UserId);
            ddlLojas.DataSource = dsLojas;
            ddlLojas.DataTextField = "NomeFantasia";
            ddlLojas.DataValueField = "Cnpj";
            ddlLojas.DataBind();
            ddlLojas.Items.Insert(0, new ListItem("Selecione", string.Empty));
            ddlLojas.SelectedIndex = 0;

            switch (this.User.TipoId)
            {
                case 2:
                    ddlLojaRelatorios.DataSource = dsLojas;
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

        private void getAno()
        {
            ddlAnoG.Items.Add(new ListItem(String.Empty, String.Empty));
            for (int i = 0; i < 3; i++)
            {
                ddlAnoG.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddlAnoG.SelectedIndex = 0;
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "error");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "32%");
            divInfo.InnerHtml = "<p>Loja cadastrada com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }
        #endregion
    }
}