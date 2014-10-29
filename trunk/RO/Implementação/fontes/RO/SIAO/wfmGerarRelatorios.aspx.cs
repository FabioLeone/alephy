using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using Microsoft.Reporting.WebForms;
using SIAO.SRV;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Linq;
using System.Drawing.Printing;

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
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            this.User = clsUser = UsersBLL.GetUserSession();

            if (this.User.UserId == 0) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack)
            {
                getRedes();
                
                ValidaAcesso();
                
                txtInicio.Enabled = false;
                txtFim.Enabled = false;

                LoadUf();
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
            lr1 = RelatoriosBLL.GetCross(ResultData());

            if (lr1.Count > 0)
            {
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Modelo1",
                    UserId = this.User.UserId
                }, scn);

                cursor();
                clsFuncs.Redirect(setPdf(lr1, "Modelo_1", "Relatory/rptCross.rdlc"), "_blank", "");
            }
            else
            {
                cursor();
                divErro("Não há itens a serem listados.");
            }
        }

        protected void btnRelat2_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();
            lr1 = RelatoriosBLL.GetMod2(ResultData());

            if (lr1.Count > 0)
            {
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Modelo2",
                    UserId = this.User.UserId
                }, scn);

                cursor();
                clsFuncs.Redirect(setPdf(lr1, "Modelo_2", "Relatory/rptCross2.rdlc"), "_blank", "");
            }
            else
            {
                cursor();
                divErro("Não há itens a serem listados.");
            }
        }

        protected void lbtnGra1_Click(object sender, EventArgs e)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();
            int id = 0;
            int.TryParse(ddlRedesRelatorios.SelectedValue, out id);

            clsGrafic = GraficBLL.GraficList(ResultData());

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico1",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, "Grafico_1", "Relatory/rptGrafic.rdlc"), "_blank", "");
        }

        protected void lbtnGra2_Click(object sender, EventArgs e)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();

            clsGrafic = GraficBLL.Grafic2List(ResultData());

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico2",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, "Grafico_2", "Relatory/rptGrafic2.rdlc"), "_blank", "");
        }

        protected void lbtnGra3_Click(object sender, EventArgs e)
        {
            List<Grafic2TO> clsGrafic = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic2 = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic3 = new List<Grafic2TO>();

            clsGrafic = GraficBLL.Grafic31ByFilter(ResultData());
            clsGrafic2 = GraficBLL.Grafic32ByFilter(ResultData());
            clsGrafic3 = GraficBLL.Grafic33ByFilter(ResultData());

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico3",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, clsGrafic2, clsGrafic3, "Grafico_3", "Relatory/rptGrafic3.rdlc"), "_blank", "");
        }

        protected void lbtnGra4_Click(object sender, EventArgs e)
        {
            List<GraficTO> clsGrafic = new List<GraficTO>();

            clsGrafic = GraficBLL.Grafic4(ResultData());

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico4",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, "Grafico_4", "Relatory/rptGrafic4.rdlc"), "_blank", "");
        }

        protected void lbtnCross24_Click(object sender, EventArgs e) {
            List<GraficTO> clsGrafic = new List<GraficTO>();

            clsGrafic = GraficBLL.Grafic4(ResultData());

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico_Comparativo",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, "Grafico_Comparativo", "Relatory/rptGfcCross24.rdlc"), "_blank", "");
        }

        protected void rbtPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPeriodo.Checked)
            {
                txtInicio.Enabled = true;
                txtFim.Enabled = true;
                txtInicio.Focus();
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
            if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
            {
                LojasBLL.getLojasApp(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
                LoadUf(Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
                if(ddlCity.Items.Count == 0)
                    LoadCity(Convert.ToInt32(ddlRedesRelatorios.SelectedValue), 0);
            }else
                LoadUf();
        }

        protected void lbtnAna1_Click(object sender, EventArgs e)
        {
            String strFile = String.Empty;

            if (this.User.RedeId > 0)
                strFile = RelatoriosBLL.GetAnalise(ResultData(), this.User.RedeId);
            else
                strFile = RelatoriosBLL.GetAnalise(ResultData(), 0);

            if (String.IsNullOrEmpty(strFile))
            {
                cursor();
                divErro("Não há itens a serem listados.");
            }
            else
            {
                cursor();
                clsFuncs.Redirect("uploads/" + strFile + ".pdf", "_blank", "");
            }
        }

        protected void lbtnParticipation_Click(object sender, EventArgs e)
        {
            List<PercReport> lst = new List<PercReport>();

            lst = RelatoriosBLL.GetPercent(ResultData());

            cursor();
            clsFuncs.Redirect(setPdf(lst, "Participacao", "Relatory/rptPartPerc.rdlc"), "_blank", "");
        }

        protected void ddlUF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) && !String.IsNullOrEmpty(ddlUF.SelectedValue))
            {
                LoadCity(Convert.ToInt32(ddlRedesRelatorios.SelectedValue), Convert.ToInt32(ddlUF.SelectedValue));
                LojasBLL.getLojasApp(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue), Convert.ToInt32(ddlUF.SelectedValue));
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) && !String.IsNullOrEmpty(ddlCity.SelectedValue))
            {
                LojasBLL.getLojasApp(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue), ddlCity.SelectedValue);
            }
        }

        protected void ddlLojaRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Loja l = LojasBLL.getByCnpj(ddlLojaRelatorios.SelectedValue);
            sInf.Text = String.Format("<p>Razão: {0} </p><p>CNPJ: {1} </p><p>Proprietário: {2}</p>",l.Razao, l.Cnpj, l.Proprietario);
        }
        #endregion

        #region .: Methods :.
        private void getRedes()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes();

            if (ds.Tables.Count > 0)
            {
                ddlRedesRelatorios.DataSource = ds.Tables[0];
                ddlRedesRelatorios.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedesRelatorios.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedesRelatorios.DataBind();
                ddlRedesRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
                ddlRedesRelatorios.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Independentes", "0"));
                ddlRedesRelatorios.SelectedIndex = 0;
            }
        }

        private void ValidaAcesso()
        {
            if(UsersBLL.ValidaAcesso(UsersBLL.GetUserSession(), dvRedes, dvLoja, dvFiltro,li3))
                LojasBLL.getLojasApp(this.User,ddlLojaRelatorios,dvFiltro, dvLoja);

            UsersBLL.CheckRptVew(UsersBLL.GetUserSession(),lm1,lm2,li3,lg1,lg2,lg3,lg4,lg5,la1,dvAn);
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divError.Style.Add("bottom", "3.5%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            this.Page.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "4.6%");
            divInfo.InnerHtml = "<p>Loja cadastrada com sucesso.</p>";

            this.Page.Controls.Add(divInfo);
        }

        public ListItemCollection ResultData()
        {
            ListItemCollection lic = new ListItemCollection();

            if (ddlRedesRelatorios.Visible && String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
            {
                divErro("Selecione a rede!");
                lic.Add(new System.Web.UI.WebControls.ListItem("st", "false"));
                return lic;
            }
            else
                lic.Add(new System.Web.UI.WebControls.ListItem("rede", ddlRedesRelatorios.SelectedValue));

            lic.Add(new System.Web.UI.WebControls.ListItem("loja", ddlLojaRelatorios.SelectedValue));

            if (rbtPeriodo.Checked)
            {
                if (txtInicio.Text != "__/____" && txtFim.Text != "__/____")
                {
                    lic.Add(new System.Web.UI.WebControls.ListItem("de", txtInicio.Text));
                    lic.Add(new System.Web.UI.WebControls.ListItem("ate", txtFim.Text));
                }
                else
                {
                    divErro("Preencha os campos \"de\" e \"até\"!");
                    lic.Add(new System.Web.UI.WebControls.ListItem("st", "false"));
                    return lic;
                }
            }
            else if (rbtMes.Checked)
            {
                lic.Add(new System.Web.UI.WebControls.ListItem("de", DateTime.Now.AddMonths(-6).ToString("MM/yyy")));
                lic.Add(new System.Web.UI.WebControls.ListItem("ate", DateTime.Now.AddMonths(-1).ToString("MM/yyy")));
            }
            else if (rbtUAno.Checked)
            {
                lic.Add(new System.Web.UI.WebControls.ListItem("de", DateTime.Now.AddMonths(-12).ToString("MM/yyyy")));
                lic.Add(new System.Web.UI.WebControls.ListItem("ate", DateTime.Now.AddMonths(-1).ToString("MM/yyyy")));
            }
            else if (rbtUMes.Checked)
            {
                lic.Add(new System.Web.UI.WebControls.ListItem("de", DateTime.Now.AddMonths(-1).ToString("MM/yyyy")));
                lic.Add(new System.Web.UI.WebControls.ListItem("ate", DateTime.Now.AddMonths(-1).ToString("MM/yyyy")));
            }
            else
            {
                divErro("Selecione o periodo!");
                lic.Add(new System.Web.UI.WebControls.ListItem("st", "false"));
            }

            if (lic.FindByText("st") == null)
                lic.Add(new System.Web.UI.WebControls.ListItem("st", "true"));

            lic.Add(new System.Web.UI.WebControls.ListItem("sum", cbxSum.Checked.ToString()));

            if (ddlUF.Visible && String.IsNullOrEmpty(ddlUF.SelectedValue)) {
                lic.Add(new System.Web.UI.WebControls.ListItem("uf", ddlUF.SelectedValue));
            }else
                lic.Add(new System.Web.UI.WebControls.ListItem("uf", "0"));

            if (String.IsNullOrEmpty(ddlCity.SelectedValue)) {
                lic.Add(new System.Web.UI.WebControls.ListItem("city", ddlCity.SelectedValue));
            }else
                lic.Add(new System.Web.UI.WebControls.ListItem("city", String.Empty));

            return lic;
        }

        private string setPdf(Object lr1, String strName, String strPath)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", lr1);
            String filename = String.Empty;

            if (lr1.GetType() == typeof(List<clsRelat1>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<clsRelat1>));
            else if (lr1.GetType() == typeof(List<GraficTO>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<GraficTO>));
            else if (lr1.GetType() == typeof(List<PercReport>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<PercReport>));
            else
                filename = clsFuncs.SetFileName(strName, (lr1 as List<Grafic2TO>));

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            rv.Reset();
            rv.LocalReport.Dispose();
            rv.LocalReport.DataSources.Add(rds);

            rv.LocalReport.ReportPath = strPath;
            rv.LocalReport.DisplayName = filename;
            rv.DataBind();

            byte[] b = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);


            return FilesBLL.SaveFile(filename,b);
        }

        private string setPdf(List<Grafic2TO> cGfc, List<Grafic2TO> cGfc2, List<Grafic2TO> cGfc3, string strName, string strPath)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", cGfc);
            ReportDataSource rds2 = new ReportDataSource("DataSet2", cGfc2);
            ReportDataSource rds3 = new ReportDataSource("DataSet3", cGfc3);

            String filename = filename = clsFuncs.SetFileName(strName, cGfc);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            rv.Reset();
            rv.LocalReport.Dispose();
            rv.LocalReport.DataSources.Add(rds);
            rv.LocalReport.DataSources.Add(rds2);
            rv.LocalReport.DataSources.Add(rds3);

            rv.LocalReport.ReportPath = strPath;
            rv.LocalReport.DisplayName = filename;
            rv.DataBind();

            byte[] b = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            return FilesBLL.SaveFile(filename, b);
        }

        private string MultPdf(List<clsRelat1> lst, List<GraficTO> lst2, List<GraficTO> lst3, List<GraficTO> lst4, string p, string p_2, string p_3, string p_4, string p_5, string p_6, string p_7, string p_8, string p_9, string p_10)
        {
            string stResult = string.Empty;
            string strDir = clsFuncs.SetDirName(lst);

            stResult = setPdf(lst, "Modelo_2", "Relatory/rptCross2.rdlc", strDir);
            setPdf(lst2, "Grafico_1", "Relatory/rptGrafic.rdlc", strDir);
            setPdf(lst3, "Grafico_1_2", "Relatory/rptGrafic.rdlc", strDir);
            setPdf(lst3, "Grafico_2", "Relatory/rptGrafic2.rdlc", strDir);
            setPdf(lst4, "Grafico_4", "Relatory/rptGrafic4.rdlc", strDir);

            return FilesBLL.ZipFolder(stResult);
        }

        private string setPdf(Object lr1, String strName, String strPath, string strDir)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", lr1);
            String filename = String.Empty;

            if (lr1.GetType() == typeof(List<clsRelat1>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<clsRelat1>));
            else if (lr1.GetType() == typeof(List<GraficTO>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<GraficTO>));
            else
                filename = clsFuncs.SetFileName(strName, (lr1 as List<Grafic2TO>));

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            rv.Reset();
            rv.LocalReport.Dispose();
            rv.LocalReport.DataSources.Add(rds);

            rv.LocalReport.ReportPath = strPath;
            rv.LocalReport.DisplayName = filename;
            rv.DataBind();

            byte[] b = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);


            return FilesBLL.SaveFile(filename, b, strDir);
        }

        private void cursor() {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key", "ToggleCursor(0);", true);
        }

        public bool CheckCss()
        {
            return UsersBLL.CheckCssRede(this.User);
        }

        private void LoadUf()
        {
            DataSet ds = new DataSet();
            ds = oc.GetUf(scn);

            if (ds.Tables.Count > 0)
            {
                dvUF.Visible = true;

                ddlUF.DataSource = ds.Tables[0];
                ddlUF.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlUF.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlUF.DataBind();
                ddlUF.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
                ddlUF.SelectedIndex = 0;
            }
        }

        private void LoadUf(int id)
        {
            DataSet ds = new DataSet();
            ds = oc.GetUf(id);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Columns.Count > 0 && ds.Tables[0].Rows.Count > 1)
                {
                    dvUF.Visible = true;

                    ddlUF.DataSource = ds.Tables[0];
                    ddlUF.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlUF.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlUF.DataBind();
                    ddlUF.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
                    ddlUF.SelectedIndex = 0;
                }
                else if (ds.Tables[0].Columns.Count > 0)
                {
                    dvUF.Visible = false;

                    if(ds.Tables[0].Rows[0][0] != DBNull.Value)
                        LoadCity(id, Convert.ToInt32(ds.Tables[0].Rows[0][0]));
                }
            }
        }

        private void LoadCity(int id, int ufId)
        {
            DataTable dt = new DataTable();
            dt = oc.GetCityByNetworkAndUF(id, ufId);

            if (dt.Columns.Count > 0)
            {
                ddlCity.DataSource = dt;
                ddlCity.DataValueField = dt.Columns[0].ToString();
                ddlCity.DataTextField = dt.Columns[0].ToString();
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
                ddlCity.SelectedIndex = 0;
            }
        }

        #endregion

    }
}