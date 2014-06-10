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
            XmlDocument xd = new XmlDocument();
            XmlNode root;

            if (File.Exists(strPath))
            {
                xd.Load(strPath);
            }
            else { 
                root = xd.CreateElement("xmldata");
                xd.AppendChild(root);
            }

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

            xd.SelectSingleNode("xmldata").InnerXml += sb.ToString();
            xd.Save(strPath);
        }

        public static void checklog() {
            string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "sflog.xml";
            if (!File.Exists(strPath))
                return;

            XmlDocument xd = new XmlDocument();
            
            xd.Load(strPath);
            XmlNodeList nl = xd.GetElementsByTagName("LogInfo");

            foreach (XmlNode item in nl)
            {
                if (Convert.ToDateTime(item.SelectNodes("Time").Item(0).InnerXml).Date < DateTime.Now.AddMonths(-3).Date)
                {
                    xd.SelectNodes("xmldata").Item(0).RemoveChild(item);
                    xd.Save(strPath);
                }
            }
        }
    }
}
