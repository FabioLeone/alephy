using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suLibrary
{
    public class cdm
    {
        public static string cript(string strText)
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

        public static string desc(string strText)
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
