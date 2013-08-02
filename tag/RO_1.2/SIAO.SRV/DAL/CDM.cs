using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.IO;

namespace SIAO.SRV.DAL
{
    public class CDM
    {
        public static string Cript(string strText)
        {
            Char[] a;
            a = strText.ToCharArray();
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

        public static string Desc(string strText)
        {
            Char[] a;
            a = strText.ToCharArray();
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
    }
}
