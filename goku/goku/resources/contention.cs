using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goku.resources
{
    public class contention
    {
        public contention()
        {
        }

        internal static void cleanFiles()
        {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            FileInfo fi;

            if (Directory.Exists(strPath + "sentfiles"))
            {
                string[] ar1 = Directory.GetFiles(strPath + "sentfiles");

                if (ar1.Count() > 0)
                {
                    foreach (var item in ar1)
                    {
                        fi = new FileInfo(item);

                        if (fi.LastWriteTime.Month.Equals(DateTime.Now.AddMonths(-1).Month))
                            File.Delete(item);
                    }
                }
            }

            if (Directory.Exists(strPath + "errorfiles"))
            {
                string[] ar2 = Directory.GetFiles(strPath + "errorfiles");

                if (ar2.Count() > 0)
                {
                    foreach (var item in ar2)
                    {
                        fi = new FileInfo(item);

                        if (fi.LastWriteTime.Month.Equals(DateTime.Now.AddMonths(-1).Month))
                            File.Delete(item);
                    }
                }
            }
        }
    }
}
