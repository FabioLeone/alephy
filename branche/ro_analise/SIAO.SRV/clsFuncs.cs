using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Text;
using SIAO.SRV.TO;
using System.Web;
using System.Web.UI;
using System.Globalization;

namespace SIAO.SRV
{
    public class clsFuncs
    {
        #region .: Variaveis :.
        private List<int> lstMeses = new List<int>();
        private List<string> lstCnpj = new List<string>();
        #endregion

        #region .: Criptografia :.
        public string encr(string text)
        {
            Char[] a;
            a = text.ToCharArray();
            Char[] k = ("alephy").ToCharArray();
            string c = "";

            for (int i = 0; i < a.Length; i++)
            {
                uint b = Convert.ToUInt32(a.GetValue(i));
                uint l;

                if (i < k.Length)
                {
                    l = Convert.ToUInt32(k.GetValue(i));
                }
                else { l = 1; }

                c += (Char)(b + l);
            }
            return c;
        }

        public string denc(string text)
        {
            Char[] a;
            a = text.ToCharArray();
            Char[] k = ("alephy").ToCharArray();
            string c = "";

            for (int i = 0; i < a.Length; i++)
            {
                uint b = Convert.ToUInt32(a.GetValue(i));
                uint l;

                if (i < k.Length)
                {
                    l = Convert.ToUInt32(k.GetValue(i));
                }
                else { l = 1; }

                c += (Char)(b - l);
            }

            return c;
        }
        #endregion

        #region .: Metodos :.
        public static string MaskCnpj(string p)
        {
            string cnpj = "";
            if (string.IsNullOrEmpty(p))
                return cnpj;
            else
            {
                cnpj = p.Substring(0, 2) + ".";
                cnpj += p.Substring(2, 3) + ".";
                cnpj += p.Substring(5, 3) + "/";
                cnpj += p.Substring(8, 4) + "-";
                cnpj += p.Substring(12, 2);

                return cnpj;
            }
        }
        
        public static string RemoveMaskCnpj(string p)
        {
            string cnpj = "";
            if (string.IsNullOrEmpty(p))
                return cnpj;
            else
            {
                cnpj = p.Replace(".","");
                cnpj = cnpj.Replace("/", "");
                cnpj = cnpj.Replace("-", "");

                return cnpj;
            }
        }

        public static string RemoveSpecialChar(string strText)
        {
            string normalized = strText.Normalize(NormalizationForm.FormD);

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

        public static string SetFileName(string strRelatorio, List<clsRelat1> lr)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "",b = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            sbName.Append(strRelatorio);

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

        public static string SetFileName(string strGrafico, List<GraficTO> clsGrafic)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "", b = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            sbName.Append(strGrafico);

            if (clsGrafic.Count > 0)
            {
                a = clsGrafic[0].Nome_Fantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append("_" + a);

                b = clsGrafic[0].Periodo.Replace(" ", "");
                b = r.Replace(b, String.Empty);
                sbName.Append("_" + b);
            }

            return sbName.ToString();
        }

        public static string SetFileName(string strGrafico, List<Grafic2TO> clsGrafic)
        {
            StringBuilder sbName = new StringBuilder();
            string a = "", b = "";
            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            sbName.Append(strGrafico);

            if (clsGrafic.Count > 0)
            {
                a = clsGrafic[0].Nome_Fantasia.Replace(" ", "");
                a = r.Replace(a, String.Empty);
                sbName.Append("_" + a);

                b = clsGrafic[0].Periodo.Replace(" ", "");
                b = r.Replace(b, String.Empty);
                sbName.Append("_" + b);
            }

            return sbName.ToString();
        }

        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
 
            if ((String.IsNullOrEmpty(target) ||
                target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
                String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException(
                        "Cannot redirect to new window outside Page context.");
                }
                url = page.ResolveClientUrl(url);
 
                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open('{0}','{1}', '{2}');";
                }
                else
                {
                    script = @"window.open('{0}', '{1}');";
                }
 
                script = String.Format(script, url, target, windowFeatures);
 
                ScriptManager scm = ScriptManager.GetCurrent(page);
                if(scm != null)
                    ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
                else
                    page.ClientScript.RegisterStartupScript(page.GetType(), "Redirect", script, true);
               
            }
        }

        #endregion

        #region .: Validações :.
        public bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        public bool ValidaCnpj(string cnpj)
        {
            if (cnpj.Equals("__.___.___/____-__")) { return false; }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            cnpj = cnpj.Trim();

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)

                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;

            soma = 0;

            for (int i = 0; i < 13; i++)

                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)

                resto = 0;

            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }
        
        public bool ValidaExt(string arq) {
            if (System.IO.Path.GetExtension(arq).ToUpper() == ".XML" || System.IO.Path.GetExtension(arq).ToUpper() == ".TXT") { return true; } else { return false; }
        }

        public bool ValidaDBFExt(string arq)
        {
            if (System.IO.Path.GetExtension(arq).ToUpper() == ".DBF") { return true; } else { return false; }
        }
        #endregion

        #region .: Converções :.
        public System.Data.DataTable txtDtConvert(System.IO.Stream stream)
        {
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("Windows-1252"), true);
            string[] line;
            int i = 0;
            int intMes = 0;
            string strCnpj = string.Empty;

            while (! sr.EndOfStream)
            {
                line = sr.ReadLine().Split(';');
                
                if (i == 0)
                {
                    for (int j = 0; j < line.Length; j++)
                    {
                        dt.Columns.Add(RemoveSpecialChar(line[j].Trim()));
                    }
                    i++;
                }
                else {
                    DataRow dr = dt.NewRow();
                  
                    for (int j = 0; j < line.Length; j++)
                    {
                        dr[j] = line[j].Trim();

                        if (j.Equals(2)) {
                            if (!intMes.Equals(Convert.ToInt32(line[j])))
                            {
                                intMes = Convert.ToInt32(line[j]);
                                lstMeses.Add(Convert.ToInt32(line[j]));
                            }
                        }
                        if (j.Equals(1)) {
                            if (!strCnpj.Equals(line[j])) {
                                strCnpj = RemoveMaskCnpj(line[j]);
                                lstCnpj.Add(strCnpj);
                            }
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        public List<int> Meses() { return lstMeses; }
        public List<string> Cnpj() { return lstCnpj; }
        #endregion

    }
}
