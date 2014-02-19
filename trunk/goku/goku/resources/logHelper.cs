using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace goku.resources
{
    public class logHelper
    {
        public enum logType
        {
            info,
            error
        }

        public static void log(logType type, string info)
        {
            StringBuilder sb = new StringBuilder();
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "sflog.xml";

            using (StringWriter sw = new StringWriter(sb))
            {
                using (XmlTextWriter xw = new XmlTextWriter(sw))
                {
                    switch (type)
                    {
                        case logType.info:
                            xw.WriteStartElement("LogInfo");
                            xw.WriteElementString("Time", DateTime.Now.ToString());
                            xw.WriteElementString("Info",info);
                            xw.WriteEndElement();
                            break;
                        case logType.error:
                            xw.WriteStartElement("LogError");
                            xw.WriteElementString("Time", DateTime.Now.ToString());
                            xw.WriteElementString("Error",info);
                            xw.WriteEndElement();
                            break;
                    }
                }
            }

            using (StreamWriter s = new StreamWriter(strPath, true, Encoding.UTF8))
            {
                s.WriteLine(sb.ToString());
            }
        }
    }
}
