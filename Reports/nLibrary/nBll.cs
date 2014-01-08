using Ionic.Zip;
using Microsoft.Reporting.WinForms;
using suLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace nLibrary
{
    public class nBll
    {
        IList<Stream> m_streams;
        private int m_currentPageIndex;

        public static string zpFiles(object networkId, object storeId, DateTime dtFrom, DateTime dtTo, bool zip)
        {
            List<relatory> lst = new List<relatory>();
            List<grafic> lst2 = new List<grafic>();
            List<grafic> lst3 = new List<grafic>();
            List<grafic> lst4 = new List<grafic>();

            int nId = 0, sId = 0;

            String strPath = String.Empty;

            if (!int.TryParse(networkId.ToString(), out nId))
                return string.Empty;

            if (!int.TryParse(storeId.ToString(), out sId))
                return string.Empty;

            object o = getRelatory(dtFrom, dtTo, nId, sId);

            if (!o.GetType().Name.ToLower().Equals("string") && !o.GetType().Name.ToLower().Equals("object"))
                lst = (List<relatory>)o;

            object o2 = getGrafic(dtFrom, dtTo, nId, sId, true);
            if (!o2.GetType().Name.ToLower().Equals("string") && !o2.GetType().Name.ToLower().Equals("object"))
                lst2 = (List<grafic>)o2;
            
            object o3 = getGrafic(dtFrom, dtTo, nId, sId, false);
            if (!o3.GetType().Name.ToLower().Equals("string") && !o3.GetType().Name.ToLower().Equals("object"))
                lst3 = (List<grafic>)o3;

            object o4 = getGrafic4(dtFrom, dtTo, nId, sId);
            if (!o4.GetType().Name.ToLower().Equals("string") && !o4.GetType().Name.ToLower().Equals("object"))
                lst4 = (List<grafic>)o4;
            
            if (lst.Count > 0 || lst2.Count > 0 || lst3.Count > 0 || lst4.Count > 0)
            {
                strPath = MultPdf(lst, lst2, lst3, lst4,zip);
            }

            return strPath;
        }

        private static string MultPdf(List<relatory> lst, List<grafic> lst2, List<grafic> lst3, List<grafic> lst4, bool zip)
        {
            string stResult = string.Empty;
            string strDir = setDirName(lst);

            if (zip)
            {
                stResult = setPdf(lst, "Modelo_2", Directory.GetCurrentDirectory() + "\\sources\\rptCross2.rdlc", strDir);
                setPdf(lst2, "Grafico_1", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic.rdlc", strDir);
                setPdf(lst3, "Grafico_1_2", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic.rdlc", strDir);
                setPdf(lst3, "Grafico_2", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic2.rdlc", strDir);
                setPdf(lst4, "Grafico_4", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic4.rdlc", strDir);

                return zipFolder(stResult);
            }
            else
            {
                nBll n = new nBll();

                stResult = n.setPrint(lst, "Modelo_2", Directory.GetCurrentDirectory() + "\\sources\\rptCross2.rdlc", strDir);
                n.setPrint(lst2, "Grafico_1", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic.rdlc", strDir);
                n.setPrint(lst3, "Grafico_1_2", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic.rdlc", strDir);
                n.setPrint(lst3, "Grafico_2", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic2.rdlc", strDir);
                n.setPrint(lst4, "Grafico_4", Directory.GetCurrentDirectory() + "\\sources\\rptGrafic4.rdlc", strDir);

                return "";
            }
        }

        private static string zipFolder(string stResult)
        {
            try
            {
                using (ZipFile zf = new ZipFile())
                {
                    zf.AddDirectory(stResult);
                    zf.Save(stResult + ".zip");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return stResult + ".zip";
        }

        private static string setPdf(Object lr1, String strName, String strPath, string strDir)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", lr1);
            String filename = String.Empty;
            ReportViewer rv = new ReportViewer();

            if (lr1.GetType() == typeof(List<relatory>))
                filename = setFileName(strName, (lr1 as List<relatory>));
            else
                filename = setFileName(strName, (lr1 as List<grafic>));

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            rv.Reset();
            rv.LocalReport.Dispose();
            rv.LocalReport.DataSources.Add(rds);

            rv.LocalReport.ReportPath = strPath;
            rv.LocalReport.DisplayName = filename;
            //rv.DataBind();

            byte[] b = rv.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            return saveFile(filename, b, strDir);
        }

        private string setPrint(Object lr1, String strName, String strPath, string strDir)
        {
            ReportDataSource rds = new ReportDataSource("DataSet1", lr1);
            String filename = String.Empty;
            ReportViewer rv = new ReportViewer();
            
            if (lr1.GetType() == typeof(List<relatory>))
                filename = setFileName(strName, (lr1 as List<relatory>));
            else
                filename = setFileName(strName, (lr1 as List<grafic>));

            Warning[] warnings;
            

            rv.Reset();
            rv.LocalReport.Dispose();
            rv.LocalReport.DataSources.Add(rds);

            rv.LocalReport.ReportPath = strPath;
            rv.LocalReport.DisplayName = filename;

            ReportPageSettings rps = rv.LocalReport.GetDefaultPageSettings();

            string deviceInfo = string.Empty;

            if (rps.IsLandscape)
                deviceInfo =
                  @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>30.7cm</PageWidth>
                <PageHeight>22cm</PageHeight>
                <MarginTop>0.5cm</MarginTop>
                <MarginLeft>0.5cm</MarginLeft>
                <MarginRight>0.5cm</MarginRight>
                <MarginBottom>0.5cm</MarginBottom>
            </DeviceInfo>";
            else
                deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>21cm</PageWidth>
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>1cm</MarginTop>
                <MarginLeft>1cm</MarginLeft>
                <MarginRight>1cm</MarginRight>
                <MarginBottom>1cm</MarginBottom>
            </DeviceInfo>";

            m_streams = new List<Stream>();

            rv.LocalReport.Render("Image", deviceInfo, createStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            Print(rps.IsLandscape);

            return "saveFile(filename, b, strDir)";
        }
        
        private Stream createStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Print(bool b)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                if (b)
                    printDoc.DefaultPageSettings.Landscape = true;
                else
                    printDoc.DefaultPageSettings.Landscape = false;

                printDoc.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private static string saveFile(string filename, byte[] b, string strDir)
        {
            string strFile = filename + ".pdf";
            string strPath = Directory.GetCurrentDirectory() + "\\Results\\" + strDir;

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            using (FileStream fs = new FileStream(strPath + "/" + strFile, FileMode.Create))
            {
                fs.Write(b, 0, b.Length);
            }

            return strPath;
        }

        private static string setFileName(string strName, List<grafic> lr)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "", b = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            sbName.Append(strName);

            if (lr.Count > 0)
            {
                a = lr[0].Nome_Fantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append("_" + a);

                b = lr[0].Periodo.Replace(" ", "");
                b = r.Replace(b, String.Empty);
                sbName.Append("_" + b);
            }

            return sbName.ToString();
        }

        private static string setFileName(string strName, List<relatory> lr)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "", b = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            sbName.Append(strName);

            if (lr.Count > 0)
            {
                a = lr[0].NomeFantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append("_" + a);

                b = lr[0].Periodo.Replace(" ", "");
                b = r.Replace(b, String.Empty);
                sbName.Append("_" + b);
            }

            return sbName.ToString();
        }

        public static string setDirName(object lr)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            if (lr.GetType() == typeof(List<relatory>))
            {
                a = (lr as List<relatory>)[0].NomeFantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append(a);
            }
            else
            {
                a = (lr as List<grafic>)[0].Nome_Fantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append(a);
            }

            return sbName.ToString();
        }

        private static List<grafic> getGrafic4(DateTime dtFrom, DateTime dtTo, int nId, int sId)
        {
            List<grafic> clsList = new List<grafic>();
            string strIni = string.Empty, strFim = string.Empty;

            if (String.IsNullOrEmpty(dtFrom.ToString("MM yyyy")) && String.IsNullOrEmpty(dtTo.ToString("MM yyyy")))
            {
                strFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                strIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
            }
            else
            {
                strIni = dtFrom.ToString("MM yyyy");
                strFim = dtTo.ToString("MM yyyy");
            }

            List<grafic> clsGrafic;

            if (nId > 0)
                clsGrafic = nDal.getGrafic(strIni, strFim, nId, sId);
            else
                clsGrafic = nDal.getGrafic(strIni, sId, strFim);

            List<IndicesGraficTO> clsIndicesGrafic = getIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(grafic _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new grafic()
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

        private static object getGrafic(DateTime dtFrom, DateTime dtTo, int nId, int sId, bool blnLast)
        {
            List<grafic> clsList = new List<grafic>();
            string strAIni = string.Empty, strAFim = string.Empty;

            if (String.IsNullOrEmpty(dtFrom.ToString("MM/yyyy")) && String.IsNullOrEmpty(dtTo.ToString("MM/yyyy")))
            {
                if (blnLast)
                {
                    strAFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strAIni = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.AddMonths(-1).Year.ToString();
                }
                else
                {
                    strAFim = DateTime.Now.AddMonths(-1).Month.ToString() + " " + DateTime.Now.Year.ToString(); ;
                    strAIni = DateTime.Now.AddMonths(-6).Month.ToString() + " " + DateTime.Now.AddMonths(-6).Year.ToString();
                }
            }
            else
            {
                if (blnLast)
                    strAIni = strAFim = dtTo.ToString("MM yyyy");
                else
                {
                    strAIni = dtFrom.ToString("MM yyyy");
                    strAFim = dtTo.ToString("MM yyyy");
                }
            }

            List<grafic> clsGrafic = nDal.getGrafic(strAIni, strAFim, nId, sId);
            List<IndicesGraficTO> clsIndicesGrafic = getIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(grafic _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new grafic()
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

        private static List<IndicesGraficTO> getIndicesAll()
        {
            return nDal.getIndicesALL();
        }

        private static object getRelatory(DateTime dtFrom, DateTime dtTo, int nId, int sId)
        {
            List<relatory> lst = new List<relatory>();
            object o = nDal.getRelatory(dtFrom, dtTo, nId, sId);

            if (o.GetType().Name.ToLower().Equals("string"))
            {
                return o.ToString();
            }
            else if (!o.GetType().Name.ToLower().Equals("object"))
            {
                lst = (List<relatory>)o;

                lst.ForEach(delegate(relatory report)
                {
                    report.SomaDeValorBruto = Math.Round(report.SomaDeValorBruto, 0);
                    report.SomaDeValorLiquido = Math.Round(report.SomaDeValorLiquido, 0);
                    report.SomaDeValorDesconto = Math.Round(report.SomaDeValorDesconto, 0);
                });

                return lst;
            }
            else
                return o;
        }

    }
}
