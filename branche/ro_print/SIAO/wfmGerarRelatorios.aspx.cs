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
            this.User = clsUser = UsersBLL.GetUserSession();

            if (this.User.UserId == 0) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack)
            {
                getRedes();
                
                ValidaAcesso();
                
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
                    lr1 = RelatoriosBLL.GetCross(clsUser, txtInicio.Text, txtFim.Text, this.RedeId, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""));
                else
                    lr1 = RelatoriosBLL.GetCross(clsUser, txtInicio.Text, txtFim.Text, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    lr1 = RelatoriosBLL.GetCross(clsUser, this.RedeId, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""));
                else
                    lr1 = RelatoriosBLL.GetCross(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));
            }

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
            if (this.RedeId > 0)
                lr1 = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, this.RedeId, rbtPeriodo, rbtMes, ddlLojaRelatorios);
            else
                lr1 = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, ddlRedesRelatorios, ddlLojaRelatorios, rbtPeriodo, rbtMes);

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

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue, false);
                else
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, ddlLojaRelatorios.SelectedValue, txtFim.Text, false);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, string.Empty, this.RedeId, ddlLojaRelatorios.SelectedValue, false);
                else
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, ddlLojaRelatorios.SelectedValue, string.Empty, false);
            }

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

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue, false);
                else
                    clsGrafic = GraficBLL.GraficList(txtInicio.Text, clsUser, ddlLojaRelatorios.SelectedValue, txtFim.Text, false);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, string.Empty, this.RedeId, ddlLojaRelatorios.SelectedValue, false);
                else
                    clsGrafic = GraficBLL.GraficList(string.Empty, clsUser, ddlLojaRelatorios.SelectedValue, string.Empty, false);
            }

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

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodoAndRedeId(txtInicio.Text, txtFim.Text, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
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
                    clsGrafic = GraficBLL.Grafic31ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodoAndRedeId(string.Empty, string.Empty, clsUser, this.RedeId, ddlLojaRelatorios.SelectedValue);
                }
                else
                {
                    clsGrafic = GraficBLL.Grafic31ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic2 = GraficBLL.Grafic32ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                    clsGrafic3 = GraficBLL.Grafic33ByPeriodo(string.Empty, string.Empty, clsUser, ddlLojaRelatorios.SelectedValue);
                }
            }

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

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.Grafic4(txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue);
                else
                    clsGrafic = GraficBLL.Grafic4(txtInicio.Text, clsUser, txtFim.Text, 0, ddlLojaRelatorios.SelectedValue);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    clsGrafic = GraficBLL.Grafic4(string.Empty, clsUser, string.Empty, this.RedeId, ddlLojaRelatorios.SelectedValue);
                else
                    clsGrafic = GraficBLL.Grafic4(string.Empty, clsUser, string.Empty, 0, ddlLojaRelatorios.SelectedValue);
            }

            if (clsGrafic.Count > 0)
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Grafico4",
                    UserId = this.User.UserId
                }, scn);

            cursor();
            clsFuncs.Redirect(setPdf(clsGrafic, "Grafico_4", "Relatory/rptGrafic4.rdlc"), "_blank", "");
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
                LojasBLL.getLojasApp(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
        }

        protected void lbtnAna1_Click(object sender, EventArgs e)
        {
            String strFile = String.Empty;

            if (this.RedeId > 0)
                strFile = RelatoriosBLL.GetAnalise(ResultData(), this.RedeId);
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

        protected void lbtnGroup_Click(object sender, EventArgs e)
        {
            List<clsRelat1> lst = new List<clsRelat1>();
            List<GraficTO> lst2 = new List<GraficTO>();
            List<GraficTO> lst3 = new List<GraficTO>();
            List<GraficTO> lst4 = new List<GraficTO>();
            
            String strPath = String.Empty;

            if (this.RedeId > 0)
                lst = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, this.RedeId, rbtPeriodo, rbtMes, ddlLojaRelatorios);
            else
                lst = RelatoriosBLL.GetMod2(clsUser, txtInicio, txtFim, ddlRedesRelatorios, ddlLojaRelatorios, rbtPeriodo, rbtMes);

            lst2 = GraficBLL.GraficList(rbtPeriodo, rbtMes, txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue, true);
            lst3 = GraficBLL.GraficList(rbtPeriodo, rbtMes, txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue, false);

            if (rbtPeriodo.Checked)
            {
                if (this.RedeId > 0)
                    lst4 = GraficBLL.Grafic4(txtInicio.Text, clsUser, txtFim.Text, this.RedeId, ddlLojaRelatorios.SelectedValue);
                else
                    lst4 = GraficBLL.Grafic4(txtInicio.Text, clsUser, txtFim.Text, 0, ddlLojaRelatorios.SelectedValue);
            }
            else if (rbtMes.Checked)
            {
                if (this.RedeId > 0)
                    lst4 = GraficBLL.Grafic4(string.Empty, clsUser, string.Empty, this.RedeId, ddlLojaRelatorios.SelectedValue);
                else
                    lst4 = GraficBLL.Grafic4(string.Empty, clsUser, string.Empty, 0, ddlLojaRelatorios.SelectedValue);
            }

            if (lst.Count > 0 || lst2.Count > 0 || lst3.Count > 0 || lst4.Count > 0)
            {
                RelatoriosVisualizadosBLL.Insert(new RelatoriosVisualizadosTO()
                {
                    Relatorio = "Consolidado",
                    UserId = this.User.UserId
                }, scn);

                strPath = MultPdf(lst, lst2, lst3, lst4, "Modelo_2", "Grafico_1", "Grafico_1_2", "Grafico_2"
                    , "Grafico_4", "Relatory/rptCross2.rdlc", "Relatory/rptGrafic.rdlc"
                    , "Relatory/rptGrafic.rdlc", "Relatory/rptGrafic2.rdlc", "Relatory/rptGrafic4.rdlc");

                FilesBLL.MultPrintPDFs(strPath);

                cursor();
                clsFuncs.Redirect(strPath, "_blank", "");
            }
            else
            {
                cursor();
                divErro("Não há itens a serem listados.");
            }
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
            if (UsersBLL.ValidaAcesso(UsersBLL.GetUserSession(), dvRedes, dvLoja, dvFiltro, lbtnGroup))
                LojasBLL.getLojasApp(this.User,ddlLojaRelatorios,dvFiltro, dvLoja);
                
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
                lic.Add(new System.Web.UI.WebControls.ListItem("de", String.Empty));
                lic.Add(new System.Web.UI.WebControls.ListItem("ate", String.Empty));
            }
            else
            {
                divErro("Selecione o periodo!");
                lic.Add(new System.Web.UI.WebControls.ListItem("st", "false"));
            }

            if (lic.FindByText("st") == null)
                lic.Add(new System.Web.UI.WebControls.ListItem("st", "true"));

            return lic;
        }

        private string setPdf(Object lr1, String strName, String strPath)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", lr1);
            String filename = String.Empty;

            if(lr1.GetType() == typeof(List<clsRelat1>))
                filename = clsFuncs.SetFileName(strName, (lr1 as List<clsRelat1>));
            else if(lr1.GetType() == typeof(List<GraficTO>))
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
        #endregion

    }
}