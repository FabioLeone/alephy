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
                    clsGrafic = (List<GraficTO>)Session["grafic2"];
                    loadRelat2(clsGrafic, clsUser);
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

        private void loadRelat2(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic2.rdlc";
            ReportViewer2.DataBind();
        }
    }
}