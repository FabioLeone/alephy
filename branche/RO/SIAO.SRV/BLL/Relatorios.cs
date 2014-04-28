using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using System.IO;
using System.Net;
using System.Globalization;

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
            else
            {
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

        public static string GetNMod2(ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return string.Empty;

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

            List<String> lstHtml = new List<String>();

            WebClient client = new WebClient();
            String htmlCode = client.DownloadString("D:/Projects/branche/RO/SIAO/Relatory/rptmod2.html");

            if (lst.Count > 0)
            { 
                String t = @"<thead style=""border:1px solid black; text-align: center; background-color:#ccc;"">
				    <tr>
					    <td colspan=""2"">&nbsp;</td>
					    <td colspan=""{qc}"">{titulo}</td>
				    </tr>
				    <tr>
					    <td>Grupo</td>
					    <td>Sub Consultoria</td>";
                String t1 = @"</tr>
			    </thead>
                <tbody>";
                String c = @"<tr>
					    <td rowspan=""{qsc}"" style=""-webkit-transform:rotate(90deg);"">{grupo}</td>";
                
                List<String> asc = new List<string>();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                StringBuilder sb5 = new StringBuilder();

                int l = 0, qsc = 0;
                List<int> qc = new List<int>(); 

                (from cn in lst select cn.Cnpj).Distinct().ToList().ForEach(delegate(string _cn) {
                    
                    if (l == 0)
                    {
                        sb1.Append(t.Replace("{titulo}", "Quantidade"));
                        sb2.Append(t.Replace("{titulo}", "Valor Bruto"));
                        sb3.Append(t.Replace("{titulo}", "Liquido"));
                        sb4.Append(t.Replace("{titulo}", "Valor Desconto"));
                        sb5.Append(t.Replace("{titulo}", "Percentual Desconto"));

                        (from a in lst select a.Ano).Distinct().ToList().ForEach(delegate(int _a)
                        {
                            (from m in lst.FindAll(r => r.Ano == _a) select m.Mes).Distinct().ToList().ForEach(delegate(int _m)
                            {
                                sb1.Append(String.Format("<td>{0}</td>", Convert.ToDateTime(_m + "/" + _a).ToString("MMM/yyyy")));
                                sb2.Append(String.Format("<td>{0}</td>", Convert.ToDateTime(_m + "/" + _a).ToString("MMM/yyyy")));
                                sb3.Append(String.Format("<td>{0}</td>", Convert.ToDateTime(_m + "/" + _a).ToString("MMM/yyyy")));
                                sb4.Append(String.Format("<td>{0}</td>", Convert.ToDateTime(_m + "/" + _a).ToString("MMM/yyyy")));
                                sb5.Append(String.Format("<td>{0}</td>", Convert.ToDateTime(_m + "/" + _a).ToString("MMM/yyyy")));
                                qc.Add(_m);
                            });
                        });

                        sb1.Replace("{qc}", qc.Count.ToString()).Append(t1);
                        sb2.Replace("{qc}", qc.Count.ToString()).Append(t1);
                        sb3.Replace("{qc}", qc.Count.ToString()).Append(t1);
                        sb4.Replace("{qc}", qc.Count.ToString()).Append(t1);
                        sb5.Replace("{qc}", qc.Count.ToString()).Append(t1);

                        l++;
                    }

                    (from g in lst select g.Grupo).Distinct().ToList().ForEach(delegate(string _g)
                    {

                        sb1.Append(c.Replace("{grupo}", _g));
                        sb2.Append(c.Replace("{grupo}", _g));
                        sb3.Append(c.Replace("{grupo}", _g));
                        sb4.Append(c.Replace("{grupo}", _g));
                        sb5.Append(c.Replace("{grupo}", _g));

                        qsc = 0;
                        (from s in lst.FindAll(fg => fg.Grupo == _g) select s.SubConsultoria).Distinct().ToList().ForEach(delegate(string _s)
                        {
                            if (qsc > 0)
                                sb1.Append("<tr>");

                            sb1.Append(String.Format("<td>{0}</td>", _s));
                            sb2.Append(String.Format("<td>{0}</td>", _s));
                            sb3.Append(String.Format("<td>{0}</td>", _s));
                            sb4.Append(String.Format("<td>{0}</td>", _s));
                            sb5.Append(String.Format("<td>{0}</td>", _s));

                            lst.FindAll(g => g.Grupo == _g && g.SubConsultoria == _s).ForEach(delegate(clsRelat1 _r)
                            {
                                for (int id = 0; id < qc.Count; id++)
                                {
                                    if (_r.Mes == qc[id]) sb1.Append(String.Format("<td>{0}</td>", _r.SomaDeQuantidade)); else sb1.Append(String.Format("<td>{0}</td>", 0));
                                    if (_r.Mes == qc[id]) sb2.Append(String.Format("<td>{0}</td>", _r.SomaDeValorBruto)); else sb2.Append(String.Format("<td>{0}</td>", 0));
                                    if (_r.Mes == qc[id]) sb3.Append(String.Format("<td>{0}</td>", _r.SomaDeValorLiquido)); else sb3.Append(String.Format("<td>{0}</td>", 0));
                                    if (_r.Mes == qc[id]) sb4.Append(String.Format("<td>{0}</td>", _r.SomaDeValorDesconto)); else sb4.Append(String.Format("<td>{0}</td>", 0));
                                    if (_r.Mes == qc[id]) sb5.Append(String.Format("<td>{0}</td>", _r.PercentualDesconto)); else sb5.Append(String.Format("<td>{0}</td>", 0));
                                }
                            });

                            sb1.Append("</tr>");
                            sb2.Append("</tr>");
                            sb3.Append("</tr>");
                            sb4.Append("</tr>");
                            sb5.Append("</tr>");

                            qsc++;
                        });

                        sb1.Replace("{qsc}", qsc.ToString());
                        sb2.Replace("{qsc}", qsc.ToString());
                        sb3.Replace("{qsc}", qsc.ToString());
                        sb4.Replace("{qsc}", qsc.ToString());
                        sb5.Replace("{qsc}", qsc.ToString());
                    });

                    clsRelat1 re = lst.Find(cnp => cnp.Cnpj == _cn);

                    for (int pg = 0; pg < 5; pg++)
                    {
                        lstHtml.Add(htmlCode.Replace("{drogaria}", String.Format("{0} - {1}", re.NomeFantasia, re.Cnpj)).Replace("{periodo}", String.Format("{0} à {1}", licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value)));

                        switch (pg)
                        {
                            case 0:
                                lstHtml[pg] = lstHtml[pg].Replace("{conteudo}", sb1.ToString());
                                break;
                            case 1:
                                lstHtml[pg] = lstHtml[pg].Replace("{conteudo}", sb2.ToString());
                                break;
                            case 2:
                                lstHtml[pg] = lstHtml[pg].Replace("{conteudo}", sb3.ToString());
                                break;
                            case 3:
                                lstHtml[pg] = lstHtml[pg].Replace("{conteudo}", sb4.ToString());
                                break;
                            case 4:
                                lstHtml[pg] = lstHtml[pg].Replace("{conteudo}", sb5.ToString());
                                break;
                        }
                    }
                });
            }

            return string.Empty;
        }

        public static string GetAnalise(ListItemCollection lstFiltro, int intId)
        {
            if (lstFiltro.FindByText("st").Value.Equals("false"))
                return String.Empty;

            if (intId > 0)
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
            if (lst.Count > 0)
                lst.ForEach(i => i.Periodo = String.Format("{0} à {1}", strIni, strFim));

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
    }
}
