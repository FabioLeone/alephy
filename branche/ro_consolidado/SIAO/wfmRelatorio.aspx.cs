using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Configuration;
using SIAO.SRV;
using SIAO.SRV.BLL;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace SIAO
{
    public partial class _wfmRelatorio : System.Web.UI.Page
    {
        #region .: Variables :.
        UsersTO clsUser = new UsersTO();
        List<GraficTO> clsGrafic = new List<GraficTO>();
        List<Grafic2TO> clsGrafic31 = new List<Grafic2TO>();
        List<GraficTO> clsGrafic2 = new List<GraficTO>();
        List<Grafic2TO> clsGrafic32 = new List<Grafic2TO>();
        List<Grafic2TO> clsGrafic33 = new List<Grafic2TO>();

        string strConnection = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser = UsersBLL.GetUserSession();
            if (clsUser.UserId == 0) { Response.Redirect("Logon.aspx"); }


            if (!IsPostBack)
            {
                if (Session["grafic"] != null)
                {
                    clsGrafic = (List<GraficTO>)Session["grafic"];
                    loadRelat(clsGrafic, clsUser);
                }
                else if (Session["grafic2"] != null)
                {
                    clsGrafic2 = (List<GraficTO>)Session["grafic2"];
                    loadRelat2(clsGrafic2, clsUser);
                }
                else if (Session["grafic31"] != null && Session["grafic32"] != null && Session["grafic33"] != null)
                {
                    clsGrafic31 = (List<Grafic2TO>)Session["grafic31"];
                    clsGrafic32 = (List<Grafic2TO>)Session["grafic32"];
                    clsGrafic33 = (List<Grafic2TO>)Session["grafic33"];
                    loadRelat3(clsGrafic31, clsGrafic32, clsGrafic33, clsUser);
                }
                else if (Session["grafic4"] != null)
                {
                    clsGrafic = (List<GraficTO>)Session["grafic4"];
                    loadRelat4(clsGrafic, clsUser);
                }
            }
        }

        #endregion

        #region .: Methods :.
        private void loadRelat(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic.rdlc";
            ReportViewer2.LocalReport.DisplayName = clsFuncs.SetFileName("Grafico_1", clsGrafic);
            ReportViewer2.DataBind();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension,out streamids, out warnings);

            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["PATH_DOWNLOAD"] + "FileName" + ".pdf", FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private void loadRelat2(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic2.rdlc";
            ReportViewer2.LocalReport.DisplayName = clsFuncs.SetFileName("Grafico_2", clsGrafic);
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

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic3.rdlc";
            ReportViewer2.LocalReport.DisplayName = clsFuncs.SetFileName("Grafico_3", clsGrafic1);
            ReportViewer2.DataBind();
        }

        private void loadRelat4(List<GraficTO> clsGrafic, UsersTO clsUser)
        {
            Microsoft.Reporting.WebForms.ReportDataSource Rds = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", clsGrafic);
            ReportViewer2.Reset();
            ReportViewer2.LocalReport.Dispose();
            ReportViewer2.LocalReport.DataSources.Add(Rds);

            ReportViewer2.LocalReport.ReportPath = "Relatory/rptGrafic4.rdlc";
            ReportViewer2.LocalReport.DisplayName = clsFuncs.SetFileName("Grafico_4", clsGrafic);
            ReportViewer2.DataBind();
        }
        #endregion
    }
}