using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;

namespace SIAO.SRV
{
    public class RelatoriosBLL
    {
        // Utilizado pelo modelo1
        public static List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, int intRedeId, string strCnpj)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, strInicio, strFim, intRedeId, strCnpj);

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, string strCnpj, int intRedeId)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, strInicio, strFim, strCnpj, intRedeId);

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetCross(UsersTO clsUser, int intRedeId, string strCnpj)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, intRedeId, strCnpj);

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetCross(UsersTO clsUser, string strCnpj, int intRedeId)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, strCnpj, intRedeId);

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetMod2(UsersTO clsUser, TextBox txtInicio, TextBox txtFim, DropDownList ddlRedesRelatorios, DropDownList ddlLojaRelatorios, RadioButton rbtPeriodo, RadioButton rbtMes)
        {
            List<clsRelat1> lst = new List<clsRelat1>();
            if (rbtPeriodo.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, txtInicio.Text, txtFim.Text, (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)), (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""));
            else if (rbtMes.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));

            lst.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lst;
        }

        public static List<clsRelat1> GetMod2(UsersTO clsUser, TextBox txtInicio, TextBox txtFim, int intRedeId, RadioButton rbtPeriodo, RadioButton rbtMes, DropDownList ddlLojaRelatorios)
        {
            List<clsRelat1> lst = new List<clsRelat1>();
            if (rbtPeriodo.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, txtInicio.Text, txtFim.Text, intRedeId, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""));
            else if (rbtMes.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), intRedeId);

            lst.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lst;
        }

        public static List<GraficTO> GetAnalise(ListItemCollection lstFiltro, int intId)
        {
            List<GraficTO> lstGrafic = new List<GraficTO>();

            if(intId > 0)
                lstGrafic = GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("ate").Value, intId);
            else
                lstGrafic = GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("ate").Value, 0);



            return lstGrafic;
        }
    }
}
