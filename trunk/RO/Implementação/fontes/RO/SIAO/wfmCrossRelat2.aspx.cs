using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

namespace SIAO
{
    public partial class wfmCrossRelat2 : System.Web.UI.Page
    {
        List<SRV.clsRelat1> lr = new List<SRV.clsRelat1>();
        SRV.Usuario ou = new SRV.Usuario();
        SRV.clsControl oc = new SRV.clsControl();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                ou = (SRV.Usuario)Session["user"];
            }

            if (!IsPostBack)
            {
                lr = (List<SRV.clsRelat1>)Session["cross"];
                loadRelat(lr, ou);
            }
        }

        private void loadRelat(List<SRV.clsRelat1> lr, SRV.Usuario ou)
        {
            if (lr.Count == 0) { lr = oc.GetCross(scn, ou, ""); }

            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", lr);
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.DataSources.Add(Rds);
            ReportViewer1.LocalReport.ReportPath = "Relatory/rptCross2.rdlc";
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