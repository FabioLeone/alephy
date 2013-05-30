using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Configuration;
using Microsoft.Reporting;

namespace SIAO
{
    public partial class wfmRelatorio : System.Web.UI.Page
    {
        UsersTO clsUser = new UsersTO();
        List<GraficTO> clsGrafic = new List<GraficTO>();
        List<Grafic2TO> clsGrafic1 = new List<Grafic2TO>();
        List<Grafic2TO> clsGrafic2 = new List<Grafic2TO>();
        List<Grafic2TO> clsGrafic3 = new List<Grafic2TO>();

        string strConnection = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack)
            {
                if (Session["grafic"] != null)
                {
                    clsGrafic = (List<GraficTO>)Session["grafic"];
                    loadRelat(clsGrafic, clsUser);
                }
                else if (Session["grafic2"] != null) {
                    clsGrafic2 = (List<Grafic2TO>)Session["grafic2"];
                    loadRelat2(clsGrafic2, clsUser);
                }
                else if (Session["grafic31"] != null && Session["grafic32"] != null && Session["grafic33"] != null)
                {
                    clsGrafic1 = (List<Grafic2TO>)Session["grafic31"];
                    clsGrafic2 = (List<Grafic2TO>)Session["grafic32"];
                    clsGrafic3 = (List<Grafic2TO>)Session["grafic33"];
                    loadRelat3(clsGrafic1, clsGrafic2, clsGrafic3, clsUser);
                }
            }
        }

        private void loadRelat(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic.rdlc";
            ReportViewer2.DataBind();
        }

        private void loadRelat2(List<Grafic2TO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic2.rdlc";
            ReportViewer2.DataBind();
        }

        private void loadRelat3(List<Grafic2TO> clsGrafic1, List<Grafic2TO> clsGrafic2, List<Grafic2TO> clsGrafic3, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic1);
            Microsoft.Reporting.WebForms.ReportDataSource Rds1 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet2", clsGrafic2);
            Microsoft.Reporting.WebForms.ReportDataSource Rds2 = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet3", clsGrafic3);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);
            ReportViewer2.LocalReport.DataSources.Add(Rds1);
            ReportViewer2.LocalReport.DataSources.Add(Rds2);

            ReportViewer2.LocalReport.ReportPath = "";
            ReportViewer2.DataBind();
        }
    }
}