using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace SIAO.SRV
{
    public class clsFuncs
    {
        private List<int> lstMeses = new List<int>();
        private List<string> lstCnpj = new List<string>();

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
            StreamReader sr = new StreamReader(stream);
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
                        dt.Columns.Add(line[j]);
                    }
                    i++;
                }
                else {
                    DataRow dr = dt.NewRow();
                  
                    for (int j = 0; j < line.Length; j++)
                    {
                        dr[j] = line[j];

                        if (j.Equals(2)) {
                            if (!intMes.Equals(Convert.ToInt32(line[j])))
                            {
                                intMes = Convert.ToInt32(line[j]);
                                lstMeses.Add(Convert.ToInt32(line[j]));
                            }
                        }
                        if (j.Equals(1)) {
                            if (!strCnpj.Equals(line[j])) {
                                strCnpj = line[j];
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
