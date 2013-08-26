﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;

namespace SIAO.SRV.BLL
{
    public class FilesBLL
    {
        #region .:Searches:.
        public static List<FilesTO> GetByYear(int intAno, string strConnection) {
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

                        DataTable dt = ConvertXML(xd);

                        if (!DataValidation(dt))
                            msg = "CNPJ não cadastrado";
                        else
                            msg = oc.AddXml(dt, clsUser);
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = of.txtDtConvert(fuArquivo.FileContent);

                        msg = oc.AddTxt(dt, clsUser, of.Meses(), of.Cnpj());
                    }
                }
                else { msg ="Selecione apenas arquivos com extenção '.XML' ou '.TXT'."; }
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

            auxDt = dt.DefaultView.ToTable(true,"cnpj");
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

        internal static MemoryStream CreatePDFFromMemoryStream(string htmlCode)
        {
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write(htmlCode);
            sw.Flush();
            ms.Position = 0;
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();
            doc.Add(new Paragraph("Some Text"));
            writer.CloseStream = false;
            doc.Close();
            ms.Position = 0;

            return ms;
        }
        #endregion

    }
}
