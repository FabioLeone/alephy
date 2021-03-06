﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Configuration;
using SIAO.SRV;
using SIAO.SRV.BLL;

namespace SIAO
{
    public partial class wfmRelatMod : System.Web.UI.Page
    {
        #region .: Variables :.
        List<SRV.clsRelat1> lr = new List<SRV.clsRelat1>();
        UsersTO clsUser = new UsersTO();
        SRV.clsControl oc = new SRV.clsControl();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser = UsersBLL.GetUserSession();
            if (clsUser.UserId == 0) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack)
            {
                lr = (List<SRV.clsRelat1>)Session["cross"];
                loadRelat(lr, clsUser);
            }
        }
        #endregion

        #region .: Methods :.
        private void loadRelat(List<SRV.clsRelat1> lr, UsersTO clsUser)
        {
            //if (lr.Count == 0) { lr = oc.GetCross(clsUser, ""); }

            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", lr);
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.Dispose();
            ReportViewer1.LocalReport.DataSources.Add(Rds);
            ReportViewer1.LocalReport.ReportPath = "Relatory/rptCross.rdlc";
            ReportViewer1.LocalReport.DisplayName = clsFuncs.SetFileName("Modelo_1", lr);
            ReportViewer1.DataBind();
        }
        #endregion
    }
}