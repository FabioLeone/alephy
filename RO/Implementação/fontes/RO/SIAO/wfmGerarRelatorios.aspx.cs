using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;
using System.Data;

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack)
            {
                getAno();
                getLojas();
                if(!clsUser.Access.Equals("adm"))
                    ValidaAcesso();
            }
            Global.LocalPage = "";

        }

        private void ValidaAcesso()
        {
            Block();
            this.Relatorios = RolesBLL.GetRelatoriosByUserId(clsUser.UserId, scn);
            this.Todos = RolesBLL.GetByUserId(clsUser.UserId, scn).RelatoriosTodos;
            if (this.Todos)
            {
                M1.Disabled = false;
                M1.Visible = true;
                M2.Disabled = false;
                M2.Visible = true;
                G1.Disabled = false;
                G1.Visible = true;
                G2.Disabled = false;
                G2.Visible = true;
            }
            else if (this.Relatorios.Count > 0)
            {
                this.Relatorios.ForEach(delegate(RelatoriosTO _relatorio)
                {
                    switch (_relatorio.RelatorioTipoId)
                    {
                        case (int)RolesBLL.Relatorio.Grafico1:
                            G1.Disabled = false;
                            G1.Visible = true;
                            break;
                        case (int)RolesBLL.Relatorio.Grafico2:
                            G2.Disabled = false;
                            G2.Visible = true;
                            break;
                        case (int)RolesBLL.Relatorio.Modelo1:
                            M1.Disabled = false;
                            M1.Visible = true;
                            break;
                        case (int)RolesBLL.Relatorio.Modelo2:
                            M2.Disabled = false;
                            M2.Visible = true;
                            break;
                        default:
                            break;
                    }
                });
            }
        }

        private void Block()
        {
            M1.Disabled = true;
            M1.Visible = false;
            M2.Disabled = true;
            M2.Visible = false;
            G1.Disabled = false;
            G1.Visible = false;
            G2.Disabled = false;
            G2.Visible = false;
        }

        private void getLojas()
        {
            ddlLojas.DataSource = oc.GetLojaByUserId(scn, clsUser);
            ddlLojas.DataTextField = "NomeFantasia";
            ddlLojas.DataValueField = "Cnpj";
            ddlLojas.DataBind();
            ddlLojas.Items.Insert(0, new ListItem("Selecione", string.Empty));
            ddlLojas.SelectedIndex = 0;

            ddlLojaRelatorios.DataSource = oc.GetLojaByUserId(scn, clsUser);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        private void getAno()
        {
            ddlAno.Items.Add(new ListItem(String.Empty, String.Empty));
            for (int i = 0; i < 3; i++)
            {
                ddlAno.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddlAno.SelectedIndex = 0;

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

        protected void btnAdm_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();

            lr1 = oc.GetCross(scn, clsUser, ddlAno.SelectedItem.Value, ddlLojaRelatorios.SelectedItem.Value);

            Session["cross"] = lr1;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

            Response.Redirect("wfmRelatMod1.aspx");
        }

        protected void btnRelat2_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();

            lr1 = oc.GetCross(scn, clsUser, ddlAno.SelectedItem.Value, ddlLojaRelatorios.SelectedItem.Value);

            Session["cross"] = lr1;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

            Response.Redirect("wfmRelatMod2.aspx");
        }

        protected void ibtnGrafic1_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic;

            if (ddlMes.SelectedValue != "")
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(ddlMes.SelectedValue), clsUser, scn, ddlLojas.SelectedValue);
            else
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(DateTime.Now.Month), clsUser, scn, ddlLojas.SelectedValue);

            Session["grafic"] = clsGrafic;

            Global.LocalPage = "wfmGerarRelatorios.aspx";

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

            Response.Redirect("wfmRelatorio.aspx");
        }
    }
}