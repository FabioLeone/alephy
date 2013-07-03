using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Configuration;
using SIAO.SRV;

namespace SIAO
{
    public partial class wfmRelatMod2 : System.Web.UI.Page
    {
        List<SRV.clsRelat1> lr = new List<SRV.clsRelat1>();
        UsersTO clsUser = new UsersTO();
        SRV.clsControl oc = new SRV.clsControl();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }
            else
            {
                clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack)
            {
                lr = (List<SRV.clsRelat1>)Session["cross"];
                loadRelat(lr, clsUser);
            }
        }

        private void loadRelat(List<SRV.clsRelat1> lr, UsersTO clsUser)
        {
            //if (lr.Count == 0) { lr = oc.GetCross(scn, clsUser, ""); }

            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", lr);
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.DataSources.Add(Rds);
            ReportViewer1.LocalReport.ReportPath = "Relatory/rptCross2.rdlc";
            ReportViewer1.LocalReport.DisplayName = clsFuncs.SetFileName("Modelo_2", lr);
            ReportViewer1.DataBind();
        }
    }
}