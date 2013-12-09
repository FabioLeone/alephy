using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace SIAO.SRV.BLL
{
    public class FilesBLL
    {
        #region .:Searches:.
        public static List<FilesTO> GetByYear(int intAno, string strConnection)
        {
            return FilesDAL.GetByYear(intAno, strConnection);
        }

        public static List<FilesTO> GetByYearAndRedeId(int intAno, int intRedeId, string scn)
        {
            return FilesDAL.GetByYearAndRedeId(intAno, intRedeId, scn);
        }

        public static List<FilesTO> GetByCnpj(string strCnpj, string strAno, string scn)
        {
            List<FilesTO> clsFiles = FilesDAL.GetByCnpj(strCnpj, strAno, scn);
            List<FilesTO> clsAux = new List<FilesTO>();
            for (int i = 1; i < 13; i++)
            {
                if (clsFiles.Find(f => f.data.Month == i) == null)
                    clsAux.Add(new FilesTO() { mes = 0, id = i });
                else
                    clsAux.Add(new FilesTO() { mes = 1, id = i });
            }

            clsAux.OrderBy(f => f.id);

            return clsAux;
        }
        #endregion

        #region .:Methods:.

        public static string LoadArquivo(UsersTO clsUser, System.Web.UI.WebControls.FileUpload fuArquivo)
        {
            string msg = String.Empty;
            SRV.clsFuncs of = new SRV.clsFuncs();
            SRV.clsControl oc = new SRV.clsControl();

            if (fuArquivo.PostedFile.FileName == "")
            {
                msg = "Selecione um arquivo.";
            }
            else if (of.ValidaExt(fuArquivo.PostedFile.FileName))
            {
                if (fuArquivo.HasFile)
                {
                    if (System.IO.Path.GetExtension(fuArquivo.PostedFile.FileName).ToUpper() == ".XML")
                    {
                        XmlDocument xd = new XmlDocument();

                        xd.Load(fuArquivo.FileContent);

                        try
                        {
                            DataTable dt = ConvertXML(xd);

                            if (!DataValidation(dt))
                                msg = "CNPJ não cadastrado";
                            else
                                msg = oc.NewAddXml(dt, clsUser);
                        }
                        catch
                        {
                            msg = "Erro ao converter o xml. Favor verificar o arquivo.";
                        }

                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = of.txtDtConvert(fuArquivo.FileContent);

                        msg = oc.NewAddTxt(dt, clsUser);
                    }
                }
                else { msg = "Selecione apenas arquivos com extenção '.XML' ou '.TXT'."; }
            }
            else
            {
                msg = "Selecione apenas arquivos com extenção '.XML' ou '.TXT'.";
            }

            return msg;
        }

        /// <summary>
        /// Verifica se o cnpj do arquivo está cadastrado na base.
        /// </summary>
        /// <param name="dt">DataTable com os dados</param>
        /// <returns>True/False</returns>
        private static bool DataValidation(DataTable dt)
        {
            DataTable auxDt = new DataTable();
            int count = 0;

            auxDt = dt.DefaultView.ToTable(true, "cnpj");
            if (auxDt.Rows.Count > 0)
            {
                for (int i = 0; i < auxDt.Rows.Count; i++)
                {
                    if (clsControl.GetLojaByCNPJ(auxDt.Rows[i][0].ToString()).Id > 0) count++;
                }
            }

            return (count > 0);
        }

        /// <summary>
        /// Converte arquivo .xml para um objeto DataTable.
        /// </summary>
        /// <param name="xd">Aquivo a ser convertido</param>
        /// <returns>DataTable populado</returns>
        private static DataTable ConvertXML(XmlDocument xd)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(new XmlNodeReader(xd));
            return ds.Tables[0];
        }

        internal static bool CreatePDFFromHTMLFile(List<string> lstHtml, string FileName)
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                PdfWriter w = PdfWriter.GetInstance(doc, new FileStream(ConfigurationManager.AppSettings["PATH_DOWNLOAD"] + FileName + ".pdf", FileMode.Create));

                iTextSharp.text.Image imageHeader = iTextSharp.text.Image.GetInstance(ConfigurationManager.AppSettings["PATH_LOGO"]);

                w.PageEvent = new iTPageEventHandler() { ImageHeader = imageHeader };

                doc.Open();

                lstHtml.ForEach(delegate(string pag)
                {
                    doc.NewPage();

                    iTextSharp.text.html.simpleparser.HTMLWorker hw =
                                 new iTextSharp.text.html.simpleparser.HTMLWorker(doc);

                    hw.Parse(new StringReader(pag));
                });

                doc.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static String GetFiles(string strReport, int intId, bool bln)
        {
            String strName;
            String[] files;
            StringBuilder sb = new StringBuilder();

            if (bln)
                strName = clsFuncs.GetPartFileName(strReport, clsControl.GetLojaById(intId).NomeFantasia);
            else
                strName = clsFuncs.GetPartFileName(strReport, clsControl.GetRedeById(intId).RedeName);

            files = Directory.GetFiles(ConfigurationManager.AppSettings["PATH_DOWNLOAD"]);

            foreach (String file in files)
            {
                if (file.Contains(strName))
                    sb.Append(String.Format("<li><a href='uploads/{0}' target='_blank'><div class='imgAna'><p>{0}</p></div></a></li>", file.Replace("/", ";").Split(';').Last()));
            }

            if (String.IsNullOrEmpty(sb.ToString()))
                sb.Append("Não há itens a serem listados.");

            return sb.ToString();
        }

        public static string UploadFile(UsersTO clsUser, FileUpload fuArquivo)
        {
            string msg = String.Empty;
            SRV.clsFuncs of = new SRV.clsFuncs();
            SRV.clsControl oc = new SRV.clsControl();

            if (fuArquivo.PostedFile.FileName == "")
            {
                msg = "Selecione um arquivo.";
            }
            else if (of.ValidaExt(fuArquivo.PostedFile.FileName))
            {
                if (fuArquivo.HasFile)
                {
                    if (System.IO.Path.GetExtension(fuArquivo.PostedFile.FileName).ToUpper() == ".XML")
                    {
                        XmlDocument xd = new XmlDocument();

                        xd.Load(fuArquivo.FileContent);

                        try
                        {
                            DataTable dt = ConvertXML(xd);

                            if (!DataValidation(dt))
                                msg = "CNPJ não cadastrado";
                            else
                                msg = oc.NewAddXml(dt, clsUser);
                        }
                        catch
                        {
                            msg = "Erro ao converter o xml. Favor verificar o arquivo.";
                        }
                    }
                    else
                    {
                        string strPath = ConfigurationManager.AppSettings["PATH_UPLOAD"] + fuArquivo.PostedFile.FileName;
                        fuArquivo.PostedFile.SaveAs(strPath);

                        msg = FilesDAL.Insert(strPath);

                        if (String.IsNullOrEmpty(msg))
                            clsControl.AddTxtData(of.txtDtConvert(fuArquivo.FileContent), clsUser);

                        if (!String.IsNullOrEmpty(strPath))
                            File.Delete(strPath);
                    }
                }
                else { msg = "Selecione apenas arquivos com extenção '.XML' ou '.TXT'."; }
            }
            else
            {
                msg = "Selecione apenas arquivos com extenção '.XML' ou '.TXT'.";
            }

            return msg;
        }

        public static string SaveFile(string filename, byte[] b)
        {
            string strFile = filename + ".pdf";
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["PATH_DOWNLOAD"] + strFile, FileMode.Create))
            {
                fs.Write(b, 0, b.Length);
            }

            return "uploads/" + strFile;
        }

        public static string SaveFile(string filename, byte[] b, string strDirName)
        {
            string strFile = filename + ".pdf";
            string strPath = ConfigurationManager.AppSettings["PATH_DOWNLOAD"] + strDirName;

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            using (FileStream fs = new FileStream(strPath + "/" + strFile, FileMode.Create))
            {
                fs.Write(b, 0, b.Length);
            }

            return strPath;
        }

        public static string ZipFolder(string strPath)
        {
            try
            {
                using (ZipFile zf = new ZipFile())
                {
                    zf.AddDirectory(strPath);
                    zf.Save(strPath + ".zip");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return strPath.Replace("SIAO/", "|").Split('|')[1] + ".zip";
        }

        #endregion

        public static void MultPrintPDFs(string strPath)
        {
            string[] s = Directory.GetFiles(strPath.Replace("uploads/", ConfigurationManager.AppSettings["PATH_DOWNLOAD"]).Replace(".zip", ""));
            foreach (var item in s)
            {
                PrintPDFs(item.Replace("\\","/"));
            }
        }

        public static Boolean PrintPDFs(string pdfFileName)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.Verb = "print";

                proc.StartInfo.FileName = acrobat();
                proc.StartInfo.Arguments = String.Format(@"/p /h {0}", pdfFileName);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (proc.HasExited == false)
                {
                    proc.WaitForExit(10000);
                }

                proc.EnableRaisingEvents = true;

                proc.Close();
                FindAndKillProcess("AcroRd32");
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string acrobat() {
            var adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("App Paths").OpenSubKey("AcroRd32.exe");
            var path = adobe.GetValue("");

            /*var adobeOtherWay = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Classes").OpenSubKey("acrobat").OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command");
            var pathOtherWay = adobeOtherWay.GetValue("");*/

            return path.ToString();
        }

        public static bool FindAndKillProcess(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.StartsWith(name))
                {
                    clsProcess.Kill();
                    return true;
                }
            }
            return false;
        }

    }
}
