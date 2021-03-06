﻿using goku.resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace consolidate.resources
{
    public class monitor
    {
        private List<int> lstMeses = new List<int>();
        private List<string> lstCnpj = new List<string>();
        private string strPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "files\\";
        public Thread t;
        private readonly string[] columns = { "RazaoSocial", "Cnpj", "Mes", "Ano", "Barras", "Descricao", "Fabricante", "Grupo", "Totalcusto", "Quantidade", "Valorbruto", "Valorliquido", "Valordesconto" };


        public monitor()
        {
            t = new Thread(getFiles);
            t.IsBackground = true;
            t.Start();

        }

        private void getFiles()
        {
            string r;
            string p;

            while (true)
            {
                r = string.Empty;
                p = string.Empty;
                try
                {
                    if (Directory.GetFiles(strPath).Count() > 0)
                    {
                        p = Directory.GetFiles(strPath).First();
                        r = fileProcess(p);

                        if (!String.IsNullOrEmpty(r.Replace("\r\n", "")))
                            sendErrorEmail(r, p);
                        else
                            sendEmail(r, p);
                    }
                    else
                        Thread.Sleep(3000);
                }
                catch (IOException)
                {
                    Thread.Sleep(3000);
                }
            }
        }

        private void sendErrorEmail(string r, string p)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("pdev.envio@casbrasil.com.br");
            message.To.Add("pdev.envio@casbrasil.com.br");
            message.To.Add(ConfigurationManager.AppSettings["EMAIL_SEND"]);

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EMAIL_SEND_SEC"]))
                message.To.Add(ConfigurationManager.AppSettings["EMAIL_SEND_SEC"]);

            message.Subject = "Erro na carga arquivo.";

            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append(string.Format("Arquivo {0} contém erros {1}", System.IO.Path.GetFileName(p), Environment.NewLine));
            sbMsg.Append(string.Format("Data: {0}{1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm"), Environment.NewLine));
            sbMsg.Append(string.Format("Erros: {0}{1}", Environment.NewLine, r));

            message.Body = sbMsg.ToString();

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.casbrasil.com.br";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("pdev.envio@casbrasil.com.br", "ale0112358");

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in sendEmail(): {0}",
                      ex.ToString());
                logHelper.log(logHelper.logType.error, string.Format("Exception caught in sendEmail(): {0}",
                      ex.ToString()));
            }
            finally
            {
                client.Dispose();
            }
        }

        private void sendEmail(string r, string p)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("pdev.envio@casbrasil.com.br");
            message.To.Add(ConfigurationManager.AppSettings["EMAIL_SEND"]);

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EMAIL_SEND_SEC"]))
                message.To.Add(ConfigurationManager.AppSettings["EMAIL_SEND_SEC"]);

            message.Subject = "Arquivo enviado com sucesso.";

            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append(string.Format("Arquivo {0} foi enviado com sucesso {1}", System.IO.Path.GetFileName(p), Environment.NewLine));
            sbMsg.Append(string.Format("Data: {0}{1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm"), Environment.NewLine));

            message.Body = sbMsg.ToString();

            SmtpClient client = new SmtpClient();

            client.Host = "smtp.casbrasil.com.br";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("pdev.envio@casbrasil.com.br", "ale0112358");

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in sendEmail(): {0}",
                      ex.ToString());
                logHelper.log(logHelper.logType.error, string.Format("Exception caught in sendEmail(): {0}",
                      ex.ToString()));
            }
            finally
            {
                client.Dispose();
            }
        }

        private string fileProcess(string strP)
        {
            StringBuilder msg = new StringBuilder();
            List<string> lst = new List<string>();
            String s = String.Empty;
            DataTable dt = new DataTable();

            if (validaExt(strP))
            {
                if (System.IO.Path.GetExtension(strP).ToUpper() == ".XML")
                {
                    XmlDocument xd = new XmlDocument();

                    try
                    {
                        xd.Load(XmlFix(strP));

                        dt = ConvertXML(xd);

                        msg.Append(cnpjValidation(dt));

                        charRemotion(dt);

                        lst = charValidation(dt);
                        if (lst.Count > 0)
                        {
                            msg.Append(string.Format("{0}O arquivo contém os seguintes caracteres especiais: {1}", Environment.NewLine, lst.Aggregate((i, j) => i + Environment.NewLine + j)));
                            logHelper.log(logHelper.logType.error, string.Format("{0}O arquivo contém os seguintes caracteres especiais: {1}", Environment.NewLine, lst.Aggregate((i, j) => i + Environment.NewLine + j)));
                        }
                        else
                        {
                            if (dt.Rows.Count <= 4000)
                            {
                                msg.Append(string.Format("{0}{1}", Environment.NewLine, dal.insertXml(dt)));
                            }
                            else {
                                DataSet ds = dtPart(dt,4000);
                                for (int i = 0; i < ds.Tables.Count; i++)
                                {
                                    msg.Append(string.Format("{0}{1}", Environment.NewLine, dal.insertXml(ds.Tables[i])));
                                }
                            }
                            s = dt.Rows[0]["cnpj"].ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        msg.Append(e.Message);
                        logHelper.log(logHelper.logType.error, e.Message);
                    }

                }
                else
                {
                    Object o = new Object();

                    o = txtDtConvert(strP);

                    if (o.GetType() == typeof(List<string>))
                    {
                        msg.Append(string.Format("O arquivo deve conter as seguintes colunas: {0}{1}", Environment.NewLine, ((List<string>)o).Aggregate((i, j) => i + Environment.NewLine + j)));
                        logHelper.log(logHelper.logType.error, string.Format("O arquivo deve conter as seguintes colunas: {0}{1}", Environment.NewLine, ((List<string>)o).Aggregate((i, j) => i + Environment.NewLine + j)));
                    }
                    else if (o.GetType() == typeof(string))
                    {
                        msg.Append(string.Format("O arquivo contem o(s) seguinte(s) erros: {0}{1}", Environment.NewLine, (string)o));
                        logHelper.log(logHelper.logType.error, string.Format("O arquivo contem o(s) seguinte(s) erros: {0}{1}", Environment.NewLine, (string)o));
                    }
                    else
                    {
                        dt = (DataTable)o;
                        msg.Append(cnpjValidation(dt));
                        
                        charRemotion(dt);
                        
                        lst = charValidation(dt);

                        if (lst.Count > 0)
                        {
                            msg.Append(string.Format("O arquivo contém os seguintes caracteres especiais: {0}", lst.Aggregate((i, j) => i + " " + j)));
                            logHelper.log(logHelper.logType.error, string.Format("O arquivo contém os seguintes caracteres especiais: {0}", lst.Aggregate((i, j) => i + " " + j)));
                        }
                        else
                        {
                            if (dt.Rows.Count <= 4000)
                            {
                                msg.Append(dal.inserTxt(dt));
                                s = dt.Rows[0]["cnpj"].ToString();
                            }
                            else { 
                                DataSet ds = dtPart(dt, 4000);
                                for (int i = 0; i < ds.Tables.Count; i++)
                                {
                                    msg.Append(dal.inserTxt(ds.Tables[i]));
                                    s = dt.Rows[0]["cnpj"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                logHelper.log(logHelper.logType.error, "arquivo com extensão inválida.");
                msg.Append("Arquivo com extensão inválida.");
            }

            if (string.IsNullOrEmpty(msg.ToString().Replace("\r\n", "")))
            {
                fileMove(strP,s);
                consolidation c = new consolidation(dt);
            }
            else
                fileErrorMove(strP);

            return msg.ToString();
        }

        private XmlReader XmlFix(string strP)
        {
            StreamReader sr = new StreamReader(strP, Encoding.GetEncoding("Windows-1252"), true);
            String s = String.Empty;

            s = sr.ReadToEnd();
            s = s.Replace("<3", "");

            var set = new XmlReaderSettings { ConformanceLevel = ConformanceLevel.Document, IgnoreWhitespace = true, IgnoreComments = true };
            var xr = XmlReader.Create(new StringReader(s), set);
            xr.Read();
            return xr;
        }

        private void fileErrorMove(string strP)
        {
            string s = string.Empty;

            if (File.Exists(strP))
            {
                s = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "errorfiles";
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);

                s = s + "\\" + System.IO.Path.GetFileName(strP);

                File.Copy(strP, s, true);

                if (File.Exists(s))
                    File.Delete(strP);
            }
        }

        private void fileMove(string strP, string sNew)
        {
            string s = string.Empty;

            if (File.Exists(strP))
            {
                s = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "sentfiles";
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);

                s = s + "\\" + System.IO.Path.GetFileNameWithoutExtension(strP) + "_" + sNew + "_" + DateTime.Now.ToString("yyyy.MM.dd") + System.IO.Path.GetExtension(strP);

                File.Copy(strP, s, true);

                if (File.Exists(s))
                    File.Delete(strP);
            }
        }

        private dynamic txtDtConvert(string stream)
        {
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("Windows-1252"), true);
            string[] line;
            int i = 0;
            int intMes = 0;
            string strCnpj = string.Empty;
            string fLine;
            List<string> lstc = new List<string>();

            while (!sr.EndOfStream)
            {
                fLine = sr.ReadLine();
                line = fLine.Split(';');

                try
                {
                    if (!String.IsNullOrEmpty(fLine))
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < line.Length; j++)
                            {
                                dt.Columns.Add(RemoveSpecialChar(line[j].Trim()));
                            }
                            i++;

                            if (dt.Columns.Count != 13)
                                lstc = columns.Where(c => !dt.Columns.Contains(c)).ToList();
                        }
                        else
                        {
                            if (lstc.Count > 0)
                            {
                                sr.Dispose();
                                return lstc;
                            }

                            DataRow dr = dt.NewRow();

                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (j.Equals(2))
                                {
                                    if (!intMes.Equals(Convert.ToInt32(line[j])))
                                    {
                                        intMes = Convert.ToInt32(line[j]);
                                        lstMeses.Add(Convert.ToInt32(line[j]));
                                    }
                                }
                                if (j.Equals(1))
                                {
                                    line[j] = RemoveMaskCnpj(line[j]);

                                    if (!strCnpj.Equals(line[j]))
                                    {
                                        strCnpj = line[j];
                                        lstCnpj.Add(strCnpj);
                                    }
                                }
                                if (j > 2)
                                {
                                    line[j] = line[j].Replace('°', ' ');
                                    line[j] = line[j].Replace('º', ' ');
                                    line[j] = line[j].Replace('ª', ' ');
                                    line[j] = line[j].Replace('\'', ' ');
                                    line[j] = line[j].Replace('\\', '/');
                                    line[j] = line[j].Replace('´', ' ');
                                    line[j] = line[j].Replace('§', ' ');
                                    line[j] = line[j].Replace(".", "");
                                    line[j] = line[j].Replace('Í', 'I');
                                    line[j] = line[j].Replace('í', 'i');
                                    line[j] = line[j].Replace('Ç', 'C');
                                    line[j] = line[j].Replace('ç', 'c');
                                    line[j] = line[j].Replace('Ó', 'O');
                                    line[j] = line[j].Replace('ó', 'o');
                                    line[j] = line[j].Replace('Ô', 'O');
                                    line[j] = line[j].Replace('ô', 'o');
                                    line[j] = line[j].Replace('Ã', 'A');
                                    line[j] = line[j].Replace('ã', 'a');
                                    line[j] = line[j].Replace('Á', 'A');
                                    line[j] = line[j].Replace('á', 'a');
                                    line[j] = line[j].Replace('É', 'E');
                                    line[j] = line[j].Replace('é', 'e');
                                    line[j] = line[j].Replace('Ê', 'E');
                                    line[j] = line[j].Replace('ê', 'e');
                                }

                                dr[j] = line[j].Trim();
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                catch (Exception e)
                {
                    return e.Message + " - linha: " + fLine;
                }
                
            }

            sr.Dispose();
            
            return dt;
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

        public static string RemoveMaskCnpj(string p)
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

        private List<string> charValidation(DataTable dt)
        {
            Regex r = new Regex(@"(?:[^a-z0-9 _\-\.\,\()\/\%\+\*\$\:\=\&\""\?\`\[\]\@\\]|(?<=['""])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            List<string> lstr = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                foreach (var d in row.ItemArray)
                {
                    if (r.IsMatch(d.ToString()))
                        if (lstr.FindAll(i => i == r.Match(d.ToString()).ToString()).Count.Equals(0)) lstr.Add(r.Match(d.ToString()).ToString());
                }
            }

            return lstr;
        }

        private void charRemotion(DataTable dt)
        {
            Regex r = new Regex(@"(?:[^a-z0-9 _\-\.\,\()\/\%\+\*\$\:\=\&\""\?\`\[\]\@\\]|(?<=['""])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            List<string> lstr = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                int k = 0;
                foreach (var d in row.ItemArray)
                {
                    if (r.IsMatch(d.ToString()))
                        dt.Rows[dt.Rows.IndexOf(row)][k] = d.ToString().Replace(r.Match(d.ToString()).ToString(), "");
                    k++;
                }
            }
        }

        public bool validaExt(string strP)
        {
            if (System.IO.Path.GetExtension(strP).ToUpper() == ".XML" || System.IO.Path.GetExtension(strP).ToUpper() == ".TXT") { return true; } else { return false; }
        }

        private static DataTable ConvertXML(XmlDocument xd)
        {
            DataSet ds = new DataSet();
            int i = 0;

            ds.ReadXml(new XmlNodeReader(xd));

            ds.Tables[0].Columns["cnpj"].SetOrdinal(0);
            ds.Tables[0].Columns["ean"].SetOrdinal(1);
            ds.Tables[0].Columns["nprod"].SetOrdinal(2);
            if (ds.Tables[0].Columns.Contains("grupo")) { ds.Tables[0].Columns["grupo"].SetOrdinal(3); i++; }
            ds.Tables[0].Columns["fab"].SetOrdinal(3 + i);
            ds.Tables[0].Columns["ano"].SetOrdinal(4 + i);
            ds.Tables[0].Columns["mes"].SetOrdinal(5 + i);
            ds.Tables[0].Columns["quant"].SetOrdinal(6 + i);
            ds.Tables[0].Columns["vbruto"].SetOrdinal(7 + i);
            ds.Tables[0].Columns["vliquido"].SetOrdinal(8 + i);
            ds.Tables[0].Columns["desconto"].SetOrdinal(9 + i);

            return ds.Tables[0];
        }

        private static string cnpjValidation(DataTable dt)
        {
            DataTable auxDt = new DataTable();
            List<cvretun> lst = new List<cvretun>();
            cvretun r;

            auxDt = dt.DefaultView.ToTable(true, "cnpj");
            if (auxDt.Rows.Count > 0)
            {
                for (int i = 0; i < auxDt.Rows.Count; i++)
                {
                    r = new cvretun() { cnpj = auxDt.Rows[i][0].ToString(), isValid = false };
                    if (dal.validationByCNPJ(auxDt.Rows[i][0].ToString()) > 0) r.isValid = true;

                    lst.Add(r);
                }
            }

            if (lst.FindAll(s => s.isValid == false).Count > 0)
                return string.Format("CNPJ(s) não cadastrado(s) ou inativos: {0}{1}", Environment.NewLine, lst.FindAll(s => s.isValid == false).Select(v => v.cnpj).Aggregate((t, u) => t + ";" + Environment.NewLine + u));
            else
                return string.Empty;
        }

        internal static void fileConfig(string p, string s, string e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["EMAIL_SEND"].Value = p;
            config.AppSettings.Settings["EMAIL_SEND_SEC"].Value = s;
            if(!String.IsNullOrEmpty(e))
                config.AppSettings.Settings["ENV_S"].Value = e;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public DataSet dtPart(DataTable dt, int max)
        {
            int i = 0;
            int j = 1;
            DataSet newDs = new DataSet();
            DataTable newDt = dt.Clone();
            newDt.TableName = "Table_" + j;
            newDt.Clear();
            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = newDt.NewRow();
                newRow.ItemArray = row.ItemArray;

                newDt.Rows.Add(newRow);
                i++;
                if (i == max)
                {
                    newDs.Tables.Add(newDt);
                    j++;

                    int m = (dt.Rows.Count - (newDs.Tables.Count * newDt.Rows.Count));
                    if (m < max)
                        max = m;

                    newDt = dt.Clone();
                    newDt.TableName = "Table_" + j;
                    newDt.Clear();
                    i = 0;
                }
            }
            return newDs;
        }
    }
}
