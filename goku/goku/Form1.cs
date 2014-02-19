using consolidate.resources;
using goku.resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goku
{
    public partial class Form1 : Form
    {
        monitor m;

        public Form1()
        {
            InitializeComponent();

            getIni();

            contention.cleanFiles();

            m = new monitor();
            this.Hide();
        }

        private void getIni()
        {
            string s = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "iniRO.ini";

            if (File.Exists(s))
            {
                monitor.fileConfig(iniFile.ReadValue(s, "security", "mail"), iniFile.ReadValue(s, "security", "mail2"));
            }
            else {
                Confg c = new Confg();
                c.Show();
            }
        }

        private void tSMenuItem1_Click(object sender, EventArgs e)
        {
            Confg c = new Confg();
            c.Show();
        }

        private void tSMenuItem3_Click(object sender, EventArgs e)
        {
            m.t.Abort();
            this.Close();
        }

    }
}
