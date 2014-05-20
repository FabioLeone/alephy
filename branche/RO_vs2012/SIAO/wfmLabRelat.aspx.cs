using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;

namespace SIAO.Controls
{
    public partial class wfmTesteRelat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<GraficoLabTO> clsGrafic = (List<GraficoLabTO>)Session["graficoLab"];
                loadRelat(clsGrafic);
            }
        }


        private void loadRelat(List<GraficoLabTO> clsGrafic)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.DataSources.Add(Rds);
            ReportViewer1.LocalReport.ReportPath = "Relatory/rptGraficoLab.rdlc";
            ReportViewer1.DataBind();
        }
    }
}