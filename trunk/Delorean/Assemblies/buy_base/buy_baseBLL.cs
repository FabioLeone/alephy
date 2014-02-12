using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Assemblies
{
    public class buy_baseBLL
    {
        #region .:Methods:.
        public static string upFile(System.Web.HttpPostedFile hPF)
        {
            string msg = String.Empty;

            if (hPF != null && hPF.ContentLength > 0)
            {
                if (helpers.checkExt(hPF.FileName))
                {
                    if (System.IO.Path.GetExtension(hPF.FileName).ToUpper() == ".XML")
                    {
                        XmlDocument xd = new XmlDocument();
                        
                        xd.Load(hPF.InputStream);
                        
                        try
                        {
                            DataTable dt = ConvertXML(xd);

                            if (!DataValidation(dt))
                                msg = "CNPJ não cadastrado";
                            else
                            {
                                dt = addColumn(dt);
                                buy_baseDAL.removeDuplicate(dt);
                                msg = buy_baseDAL.addXml(dt);
                            }
                        }
                        catch
                        {
                            msg = "Erro ao converter o xml. Favor verificar o arquivo.";
                        }

                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = txtDtConvert(hPF.InputStream);

                        if (!DataValidation(dt))
                            msg = "CNPJ não cadastrado";
                        else
                        {
                            buy_baseDAL.removeDuplicate(dt);
                            msg = buy_baseDAL.addTxt(addColumn(dt));
                        }
                    }
                }
                else
                {
                    msg = "Selecione apenas arquivos com extenção '.XML' ou '.TXT'.";
                }
            }
            else
            {
                msg = "Selecione um arquivo.";
            }

            return msg;
        }

        private static DataTable addColumn(DataTable dt)
        {
            DataColumn dc = new DataColumn("data");
            string s = string.Empty;
            dt.Columns.Add(dc);
            int id = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (s != dt.Rows[i]["cnpj"].ToString())
                {
                    s = dt.Rows[i]["cnpj"].ToString();
                    id = usersBLL.Verify_registration(s);
                    dt.Rows[i]["cnpj"] = id;
                    dt.Rows[i]["data"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else {
                    dt.Rows[i]["cnpj"] = id;
                    dt.Rows[i]["data"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            return dt;
        }

        private static DataTable txtDtConvert(System.IO.Stream stream)
        {
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("Windows-1252"), true);
            
            string[] line;
            int i = 0;
            string strCnpj = string.Empty;

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine().Split(';');

                if (!line.Length.Equals(4))
                    break;

                if (i == 0)
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        dt.Columns.Add(RemoveSpecialChar(line[j].Trim()));
                    }
                    i++;
                }
                else
                {
                    DataRow dr = dt.NewRow();

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j.Equals(0))
                        {
                            line[j] = RemoveMaskCnpj(line[j]);

                            if (!strCnpj.Equals(line[j]))
                            {
                                strCnpj = line[j];
                            }
                        }
                        dr[j] = line[j].Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        private static string RemoveMaskCnpj(string p)
        {
            string cnpj = "";
            if (string.IsNullOrEmpty(p))
                return cnpj;
            else
            {
                cnpj = p.Replace(".", "");
                cnpj = cnpj.Replace("/", "");
                cnpj = cnpj.Replace("-", "");

                return cnpj;
            }
        }

        private static string RemoveSpecialChar(string p)
        {
            string normalized = p.Normalize(NormalizationForm.FormD);

            StringBuilder resultBuilder = new StringBuilder();
            foreach (var character in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.LowercaseLetter
                    || category == UnicodeCategory.UppercaseLetter
                    || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }

            return Regex.Replace(resultBuilder.ToString(), @"\s+", "");
        }

        private static bool DataValidation(DataTable dt)
        {
            DataTable auxDt = new DataTable();
            int count = 0;

            auxDt = dt.DefaultView.ToTable(true, "cnpj");
            if (auxDt.Rows.Count > 0)
            {
                for (int i = 0; i < auxDt.Rows.Count; i++)
                {
                    if (usersBLL.Verify_registration(auxDt.Rows[i][0].ToString()) > 0) count++;
                }
            }

            return (count > 0);
        }

        private static DataTable ConvertXML(XmlDocument xd)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            Boolean head = true;
            

            for (int i = 0; i < xd.SelectNodes("xmldata/produto").Count; i++)
            {
                if (head)
                {
                    for (int j = 0; j < xd.SelectNodes("xmldata/produto")[i].ChildNodes.Count; j++)
                    {
                        dc = new DataColumn(xd.SelectNodes("xmldata/produto")[i].ChildNodes[j].Name);
                        dt.Columns.Add(dc);
                    }
                    head = false;
                }

                dr = dt.NewRow();

                for (int j = 0; j < xd.SelectNodes("xmldata/produto")[i].ChildNodes.Count; j++)
                    dr[j] = xd.SelectNodes("xmldata/produto")[i].ChildNodes[j].InnerXml;
                
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static List<base_viewer> upCost(int id, string vcost)
        {
            List<base_viewer> lst = new List<base_viewer>();
            base_viewer o;

            lst = helpers.GetFromCache<List<base_viewer>>("products" + helpers.GetSession().UserId);

            decimal dCost = 0;
            if (decimal.TryParse(vcost, out dCost))
            {
                o = buy_baseDAL.upCost(id, dCost);
                if(lst.Count > 0)
                    lst[lst.FindIndex(i=>i.bcid == o.bcid)] = o;
            }

            return lst;
        }

        public static List<base_viewer> setCost(int id, string vcost, string s)
        {
            List<base_viewer> lst = new List<base_viewer>();

            if (id > 0)
                lst = upCost(id, vcost);
            else {
                base_viewer o;
                usersTO u = helpers.GetSession();

                lst = helpers.GetFromCache<List<base_viewer>>("products" + u.UserId);

                decimal dCost = 0;
                if (decimal.TryParse(vcost, out dCost))
                {
                    o = lst.Find(i => i.barras == s);
                    o.valor_custo = dCost;
                    o = buy_baseDAL.insert(o, u.FarmaciaId);
                    if (lst.Count > 0)
                        lst[lst.FindIndex(i => i.bcid == o.bcid)] = o;
                }
            }

            return lst;
        }

        #endregion
    }
}
