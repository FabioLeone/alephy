using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;
using Ionic.Zip;

namespace SIAO.SRV.BLL
{
	public class FilesBLL
	{
		public FilesBLL ()
		{
		}

		#region .:Searches:.
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
	}
}

