using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using System.IO;

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

        public static List<clsRelat1> GetCross(UsersTO clsUser, string strInicio, string strFim, string strCnpj, int intRedeId, CheckBox cbxSum)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, strInicio, strFim, strCnpj, intRedeId, cbxSum.Checked);

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

        public static List<clsRelat1> GetCross(UsersTO clsUser, string strCnpj, int intRedeId, CheckBox cbxSum)
        {
            SRV.clsControl oc = new SRV.clsControl();
            List<clsRelat1> lstReport = oc.GetCross(clsUser, strCnpj, intRedeId, cbxSum);

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetCross(ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<clsRelat1>();

            SRV.clsControl oc = new SRV.clsControl();
            UsersTO u = UsersBLL.GetUserSession();
            List<clsRelat1> lstReport = new List<clsRelat1>();

            if (u.RedeId > 0)
                lstReport = oc.GetCross(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u.RedeId, licFilters.FindByText("loja").Value);
            else{
                int id = 0;
                int.TryParse(licFilters.FindByText("rede").Value, out id);
                lstReport = oc.GetCross(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, licFilters.FindByText("loja").Value, id, Convert.ToBoolean(licFilters.FindByText("sum").Value));
            }

            lstReport.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lstReport;
        }

        public static List<clsRelat1> GetMod2(ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<clsRelat1>();

            List<clsRelat1> lst = new List<clsRelat1>();
            UsersTO u = UsersBLL.GetUserSession();

            if (String.IsNullOrEmpty(licFilters.FindByText("de").Value) && String.IsNullOrEmpty(licFilters.FindByText("ate").Value))
            {
                if (u.RedeId > 0)
                    lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("loja").Value, u.RedeId, Convert.ToBoolean(licFilters.FindByText("sum").Value));
                else
                {
                    int i = 0;
                    int.TryParse(licFilters.FindByText("rede").Value, out i);
                    lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("loja").Value, i, Convert.ToBoolean(licFilters.FindByText("sum").Value));
                }
            }
            else
            {
                if (u.RedeId > 0)
                    lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u.RedeId, licFilters.FindByText("loja").Value, Convert.ToBoolean(licFilters.FindByText("sum").Value));
                else
                {
                    int i = 0;
                    int.TryParse(licFilters.FindByText("rede").Value, out i);
                    lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, i, licFilters.FindByText("loja").Value, Convert.ToBoolean(licFilters.FindByText("sum").Value));
                }
            
            }

            lst.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lst;
        }

        public static string GetAnalise(ListItemCollection lstFiltro, int intId)
        {
            if (lstFiltro.FindByText("st").Value.Equals("false"))
                return String.Empty;

            if(intId > 0)
                return GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("de").Value, lstFiltro.FindByText("ate").Value, intId);
            else
                return GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("de").Value, lstFiltro.FindByText("ate").Value, 0);
        }

        public static List<PercReport> GetPercent(UsersTO clsUser, string strIni, string strFim, int intRedeId, string strCnpj)
        {
            if (String.IsNullOrEmpty(strIni) && String.IsNullOrEmpty(strFim))
            {
                strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
            }
            else
            {
                strIni = strIni.Replace("/", " ");
                strFim = strFim.Replace("/", " ");
            }

            List<PercReport> lst = RelatoriosDAL.GetPercent(clsUser, strIni, strFim, intRedeId, strCnpj);
            if(lst.Count > 0)
                lst.ForEach(i=>i.Periodo = String.Format("{0} à {1}", strIni, strFim));

            return lst;
        }

        public static List<PercReport> GetPercent(ListItemCollection lstFiltro)
        {
            if (lstFiltro.FindByText("st").Value.Equals("false"))
                return new List<PercReport>();

            string strIni = string.Empty, strFim = string.Empty;

            if (String.IsNullOrEmpty(lstFiltro.FindByText("de").Value) && String.IsNullOrEmpty(lstFiltro.FindByText("ate").Value))
            {
                strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
            }
            else
            {
                strIni = lstFiltro.FindByText("de").Value.Replace("/", " ");
                strFim = lstFiltro.FindByText("ate").Value.Replace("/", " ");
            }

            UsersTO u = UsersBLL.GetUserSession();

            List<PercReport> lst = RelatoriosDAL.GetPercent(u, strIni, strFim, u.RedeId, lstFiltro.FindByText("loja").Value);
            if (lst.Count > 0)
                lst.ForEach(i => i.Periodo = String.Format("{0} à {1}", strIni, strFim));

            return lst;
        }

        #region .:Enum:.
        internal enum Type
        {
            MODELO1 = 1,
            MODELO2 = 2,
            GRAFICO1 = 3,
            GRAFICO2 = 4,
            GRAFICO_COMPARATIVO = 5,
            PORCENTAGEM_PARTICIPACAO = 6,
            GRAFICO3 = 7,
            GRAFICO_DESCONTO = 8,
            ANALISE = 9
        }
        #endregion
    }
}
