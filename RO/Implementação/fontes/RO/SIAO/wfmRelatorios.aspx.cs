using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Web.Security;
using SIAO.SRV.BLL;

namespace SIAO
{
    public partial class wfmRelatorios : System.Web.UI.Page
    {
        UsersTO clsUser = new UsersTO();
        List<GraficTO> clsGrafic = new List<GraficTO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack)
            {
                clsGrafic = (List<GraficTO>)Session["grafic"];
                loadRelat(clsGrafic, clsUser);
            }
        }

        private void loadRelat(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            if (clsGrafic.Count == 0) { clsGrafic = GraficBLL.GraficList(DateTime.Now.Month); }

            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.DataSources.Add(Rds);
            ReportViewer1.LocalReport.ReportPath = "Relatory/rptGrafic.rdlc";
            ReportViewer1.DataBind();
        }

        protected void btnLogoff_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            FormsAuthentication.SignOut();
            Global.Acs = "";
            Response.Redirect("Logon.aspx");
        }
    }
}