using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies.utilities
{
    class cdModel
    {
        internal static string cript(string strText)
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

        internal static string desc(string strText)
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
