using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;
using System.Net;
using System.IO;
using System.Configuration;

namespace SIAO.SRV.BLL
{
    public class GraficBLL
    {
        #region .: Method :.

        public static string Analise(UsersTO clsUsers, string strLoja, string strIni, string strFim, int intRedeId, string strCity, int intUf)
        {
            List<GraficTO> clsList = new List<GraficTO>();

            if (String.IsNullOrEmpty(strFim))
                strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString();
            else
                strFim = strFim.Replace("/", " ");

            if (String.IsNullOrEmpty(strIni))
                strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
            else
                strIni = strIni.Replace("/", " ");

            List<GraficTO> clsGrafic = GraficDAL.GetLastMonth(clsUsers, strIni, strFim, strLoja, intRedeId, strCity, intUf);

            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            List<AnaliseTO> lstAnalise = AnaliseBLL.GetAnaliseAll();

            List<String> lstHtml = new List<string>();

            WebClient client = new WebClient();
            String htmlCode = client.DownloadString(ConfigurationManager.AppSettings["PATH_ANALISE"]);

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;
                StringBuilder sb = new StringBuilder();
                String strAux = String.Empty;
                int i = 0;

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            if (!strAux.ToUpper().Equals(_Grafic.Sub_Consultoria.ToUpper()))
                            {
                                if(i > 0)
                                    lstHtml.Add(htmlCode.Replace("{content}", sb.ToString()));

                                sb = new StringBuilder();
                                sb.Append(String.Format("<h3>{0}</h3>",_Grafic.Sub_Consultoria.ToUpper()));
                                strAux = _Grafic.Sub_Consultoria.ToUpper();
                                i++;
                            }

                            sb.Append("<div>");

                            decimal l = 0, d = 0;
                            l = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2);
                            d = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2);

                            if (l > 100)
                            { 
                                sb.Append(String.Format("<p>{0}</p>",lstAnalise.Find(a=>a.reference == 101 && a.itemid == 0).text.Replace("{grupo}",_Grafic.Grupo)));
                            }
                            else if (l <= 100 && l > 80) {
                                sb.Append(String.Format("<p>{0}</p>", lstAnalise.Find(a => (a.reference <= 100 && a.reference > 80) && a.itemid == 0).text.Replace("{grupo}", _Grafic.Grupo)));
                                sb.Append(SetOthers(d, lstAnalise));
                            }
                            else if (l <= 80 && l > 60)
                            {
                                sb.Append(String.Format("<p>{0}</p>", lstAnalise.Find(a => (a.reference <= 80 && a.reference > 60) && a.itemid == 0).text.Replace("{grupo}", _Grafic.Grupo)));
                                sb.Append(SetOthers(d, lstAnalise));
                            }
                            else {
                                sb.Append(String.Format("<p>{0}</p>", lstAnalise.Find(a => a.reference <= 60 && a.itemid == 0).text.Replace("{grupo}", _Grafic.Grupo)));
                                sb.Append(SetOthers(d, lstAnalise));
                            }
                            sb.Append("</div><br />");

                        }
                        _Grafic.Periodo = String.Format("{0}/{1}", _Grafic.Mes, _Grafic.Ano);
                    });
                });
            }

            string strFile = clsFuncs.SetFileName("Analise", clsGrafic);

            if (clsGrafic.Count > 0)
                if (FilesBLL.CreatePDFFromHTMLFile(lstHtml, strFile))
                {
                    return strFile;
                }
                else
                    return string.Empty;
            else
                return string.Empty;
        }

        private static string SetOthers(decimal d, List<AnaliseTO> lstAnalise)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(@"<ol><li>Estratégia<wbr />");

            if (d > 100)
            {
                sb.Append(String.Format("<p>{0}</p></li>", lstAnalise.Find(a => a.reference == 101 && a.itemid == 1).text));
            }
            else if (d <= 100 && d > 80)
            {
                sb.Append(String.Format("<p>{0}</p></li>", lstAnalise.Find(a => (a.reference <= 100 && a.reference > 80) && a.itemid == 1).text));
            }
            else if (d <= 80 && d > 60)
            {
                sb.Append(String.Format("<p>{0}</p></li>", lstAnalise.Find(a => (a.reference <= 80 && a.reference > 60) && a.itemid == 1).text));
            }
            else
            {
                sb.Append(String.Format("<p>{0}</p></li>", lstAnalise.Find(a => a.reference <= 60 && a.itemid == 1).text));
            }

            List<AnaliseItemTO> lstItens = AnaliseBLL.GetItensAll();

            lstItens.ForEach(delegate(AnaliseItemTO _item) {
                if (_item.id > 1)
                {
                    sb.Append(String.Format("<li><p>{0}</p></li>", _item.item));
                }
            });

            sb.Append(String.Format(@"</ol>"));

            return sb.ToString();
        }
        
        public static List<GraficTO> GraficList(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<GraficTO>();

            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = new List<GraficTO>();

            string strAIni = string.Empty, strAFim = string.Empty;

            strAIni = licFilters.FindByText("de").Value.Replace("/", " ");
            strAFim = licFilters.FindByText("ate").Value.Replace("/", " ");

            UsersTO u = UsersBLL.GetUserSession();
            int id = 0;
            int uf = 0;

            int.TryParse(licFilters.FindByText("rede").Value, out id);
            int.TryParse(licFilters.FindByText("uf").Value, out uf);

            if (u.RedeId > 0)
                clsGrafic = GraficDAL.GetGraficMes(strAIni, u, strAFim, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value,uf);
            else
                clsGrafic = GraficDAL.GetGraficMes(strAIni, u, strAFim, id, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf);
            
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic.Where(g => g.Grupo == "zzzzzz").Sum(l => l.Liquido);

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new GraficTO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Ano = _Grafic.Ano,
                                Liquido = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Grupo = _Grafic.Grupo,
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2),
                                Periodo = String.Format("{0} à {1}", strAIni, strAFim),
                                Nome_Fantasia = _Grafic.Nome_Fantasia,
                                quantidade = _Grafic.quantidade
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        public static List<GraficTO> Grafic2List(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<GraficTO>();

            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = new List<GraficTO>();

            string strAIni = string.Empty, strAFim = string.Empty;

            if (String.IsNullOrEmpty(licFilters.FindByText("de").Value) && String.IsNullOrEmpty(licFilters.FindByText("ate").Value))
            {
                strAFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString();
                strAIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
            }
            else
            {
                strAIni = licFilters.FindByText("de").Value.Replace("/", " ");
                strAFim = licFilters.FindByText("ate").Value.Replace("/", " ");
            }

            UsersTO u = UsersBLL.GetUserSession();
            
            int uf = 0;
            
            int.TryParse(licFilters.FindByText("uf").Value, out uf);

            if (u.RedeId > 0)
                clsGrafic = GraficDAL.GetGrafic2Mes(strAIni, u, strAFim, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf);
            else
                clsGrafic = GraficDAL.GetGrafic2Mes(strAIni, u, licFilters.FindByText("loja").Value, strAFim, licFilters.FindByText("city").Value, uf);

            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new GraficTO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Ano = _Grafic.Ano,
                                Liquido = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Grupo = _Grafic.Grupo,
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2),
                                Periodo = String.Format("{0} à {1}", strAIni, strAFim),
                                Nome_Fantasia = _Grafic.Nome_Fantasia,
                                quantidade = _Grafic.quantidade
                            });
                        }
                    });
                });
            }

            return clsList;
        }
        
        public static List<GraficTO> Grafic4(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<GraficTO>();

            List<GraficTO> clsList = new List<GraficTO>();
            string strIni = string.Empty, strFim = string.Empty;

            strIni = licFilters.FindByText("de").Value.Replace("/", " ");
            strFim = licFilters.FindByText("ate").Value.Replace("/", " ");

            List<GraficTO> clsGrafic;
            UsersTO u = UsersBLL.GetUserSession();
            int id = 0;
            int uf = 0;

            int.TryParse(licFilters.FindByText("uf").Value, out uf);
            int.TryParse(licFilters.FindByText("rede").Value, out id);

            if (u.RedeId > 0)
                clsGrafic = GraficDAL.GetGrafic2Mes(strIni, u, strFim, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf);
            else
                clsGrafic = GraficDAL.GetGrafic2Mes(strIni, u, strFim, id, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf);

            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new GraficTO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Ano = _Grafic.Ano,
                                Liquido = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Grupo = _Grafic.Grupo,
                                Desconto = _Grafic.Desconto * 100,
                                Periodo = String.Format("{0} à {1}", strIni, strFim),
                                Nome_Fantasia = _Grafic.Nome_Fantasia,
                                quantidade = _Grafic.quantidade
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        #endregion

        #region .: Search :.

        public static List<IndicesGraficTO> GetIndicesAll()
        {
            return GraficDAL.GetIndicesALL();
        }
        public static IndicesGraficTO GetIndicesById(int intId, string strConnection)
        {
            return GraficDAL.GetIndicesById(intId, strConnection);
        }
        public static List<IndicesGraficTO> GetIndicesByFiltro(string strGrupo, string strSub_Categoria, string strConnection)
        {
            return GraficDAL.GetIndicesByFiltro(strGrupo, strSub_Categoria, strConnection);
        }

        public static List<string> GetCategorias(string strConnection)
        {
            return GraficDAL.GetCategorias(strConnection);
        }

        public static List<string> GetGrupos(string strConnection)
        {
            return GraficDAL.GetGrupos(strConnection);
        }

        public static List<Grafic2TO> Grafic31ByFilter(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<Grafic2TO>();

            string strIni = string.Empty, strFim = string.Empty;

            strIni = licFilters.FindByText("de").Value.Replace("/", " ");
            strFim = licFilters.FindByText("ate").Value.Replace("/", " ");

            UsersTO u = UsersBLL.GetUserSession();
            int uf = 0;
            int.TryParse(licFilters.FindByText("uf").Value, out uf);

            if(u.RedeId > 0)
                return GraficDAL.Grafic31ByPeriodoAndRedeId(strIni, strFim, u, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", strIni, strFim); return g; }).ToList();
            else
                return GraficDAL.Grafic31ByPeriodo(strIni, strFim, u, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", strIni, strFim); return g; }).ToList();
        }

        public static List<Grafic2TO> Grafic32ByFilter(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<Grafic2TO>();

            UsersTO u = UsersBLL.GetUserSession();
            
            int uf = 0;
            int.TryParse(licFilters.FindByText("uf").Value, out uf);
            
            if(u.RedeId > 0)
                return GraficDAL.Grafic32ByPeriodoAndRedeId(licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value); return g; }).ToList();
            else
                return GraficDAL.Grafic32ByPeriodo(licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value); return g; }).ToList();
        }

        public static List<Grafic2TO> Grafic33ByFilter(System.Web.UI.WebControls.ListItemCollection licFilters)
        {
            if (licFilters.FindByText("st").Value.Equals("false"))
                return new List<Grafic2TO>();

            UsersTO u = UsersBLL.GetUserSession();
            
            int uf = 0;
            int.TryParse(licFilters.FindByText("uf").Value, out uf);

            if(u.RedeId > 0)
                return GraficDAL.Grafic33ByPeriodoAndRedeId(licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u, u.RedeId, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value); return g; }).ToList();
            else
                return GraficDAL.Grafic33ByPeriodo(licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value, u, licFilters.FindByText("loja").Value, licFilters.FindByText("city").Value, uf).Select(g => { g.Periodo = String.Format("{0} à {1}", licFilters.FindByText("de").Value, licFilters.FindByText("ate").Value); return g; }).ToList();
        }
        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsertIndice(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
            return GraficDAL.InsetIndices(clsIndiceGrafic, strConnection);
        }

        public static Boolean UpdateIndiceGrafic(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
            return GraficDAL.UpdateIndices(clsIndiceGrafic, strConnection);
        }

        public static Boolean DeleteIndiceGrafic(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
            return GraficDAL.DeleteIndice(clsIndiceGrafic, strConnection);
        }

        #endregion

    }
}
