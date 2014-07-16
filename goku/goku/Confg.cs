using consolidate.resources;
using goku.resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace goku
{
    public partial class Confg : Form
    {
        private bool first;

        public Confg()
        {
            InitializeComponent();
            txtEmail.Text = ConfigurationManager.AppSettings["EMAIL_SEND"];
            txtEmail2.Text = ConfigurationManager.AppSettings["EMAIL_SEND_SEC"];
            first = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (first)
            {
                if (isEmail(txtEmail.Text))
                {
                    string s = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "iniRO.ini";
                    iniFile.WriteValue(s, "security", "mail", txtEmail.Text);

                    if(isEmail(txtEmail2.Text))
                        iniFile.WriteValue(s, "security", "mail2", txtEmail2.Text);

                    monitor.fileConfig(txtEmail.Text, txtEmail2.Text, String.Empty);

                    btnSave.Text = "Fechar";
                    first = false;
                }
            }
            else
                this.Close();
        }

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
    }
}
