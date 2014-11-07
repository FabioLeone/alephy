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
        public static List<clsRelat1> GetCross(ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<clsRelat1>();

            SRV.clsControl oc = new SRV.clsControl();
            UsersTO u = UsersBLL.GetUserSession();
            List<clsRelat1> lstReport = new List<clsRelat1>();

            int uf = 0;

            if (u.RedeId > 0){
                int.TryParse(licFilters.FindByText("uf").Value, out uf);
                
                lstReport = oc.GetCross(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf);
            }else{
                int id = 0;
                
                int.TryParse(licFilters.FindByText("rede").Value, out id);
                int.TryParse(licFilters.FindByText("uf").Value, out uf);
                
                lstReport = oc.GetCross(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, licFilters.FindByText("loja").Value, id, Convert.ToBoolean(licFilters.FindByText("sum").Value), licFilters.FindByText("city").Value, uf);
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
            Boolean bln = UsersBLL.CheckCssRede(u);


            int i = 0;
            int.TryParse(licFilters.FindByText("rede").Value, out i);

            if (!bln)
                bln = checkNetwork(i, licFilters.FindByText("loja").Value);

            int uf = 0;

            if (u.RedeId > 0)
            {
                int.TryParse(licFilters.FindByText("uf").Value, out uf);

                lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u.RedeId, licFilters.FindByText("loja").Value, Convert.ToBoolean(licFilters.FindByText("sum").Value), licFilters.FindByText("city").Value, uf, bln);
            }
            else
            {
                int.TryParse(licFilters.FindByText("uf").Value, out uf);

                lst = RelatoriosDAL.GetMod2(u, licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, i, licFilters.FindByText("loja").Value, Convert.ToBoolean(licFilters.FindByText("sum").Value), licFilters.FindByText("city").Value, uf, bln);
            }

            lst.ForEach(delegate(clsRelat1 report)
            {
                report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
            });

            return lst;
        }

        private static bool checkNetwork(int network_id, string store)
        {
            if (network_id > 0)
            {
                return clsControl.GetRedeById(network_id).RedeName.ToLower().Contains("multidrogas");
            }
            else {
                return clsControl.GetRedeById(LojasBLL.getByCnpj(store).idRede).RedeName.ToLower().Contains("multidrogas");
            }
        }

        public static string GetAnalise(ListItemCollection lstFiltro, int intId)
        {
            if (lstFiltro.FindByText("st").Value.Equals("false"))
                return String.Empty;

            int uf = 0;
            int.TryParse(lstFiltro.FindByText("uf").Value, out uf);

            if(intId > 0)
                return GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("de").Value, lstFiltro.FindByText("ate").Value, intId, lstFiltro.FindByText("city").Value, uf);
            else
                return GraficBLL.Analise(UsersBLL.GetUserSession(), lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("de").Value, lstFiltro.FindByText("ate").Value, 0, lstFiltro.FindByText("city").Value, uf);
        }
        
        public static List<PercReport> GetPercent(ListItemCollection lstFiltro)
        {
            if (lstFiltro.FindByText("st").Value.Equals("false"))
                return new List<PercReport>();

            string strIni = string.Empty, strFim = string.Empty;

            strIni = lstFiltro.FindByText("de").Value.Replace("/", " ");
            strFim = lstFiltro.FindByText("ate").Value.Replace("/", " ");

            UsersTO u = UsersBLL.GetUserSession();

            int uf = 0;
            int.TryParse(lstFiltro.FindByText("uf").Value, out uf);

            List<PercReport> lst = RelatoriosDAL.GetPercent(u, strIni, strFim, u.RedeId, lstFiltro.FindByText("loja").Value, lstFiltro.FindByText("city").Value, uf);
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
