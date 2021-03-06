﻿using consolidate.resources;
using goku.resources;
using System;
using System.IO;
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
                monitor.fileConfig(iniFile.ReadValue(s, "security", "mail"), iniFile.ReadValue(s, "security", "mail2"), iniFile.ReadValue(s, "security", "env_s"));
            }
            else {
                Confg c = new Confg();
                c.ShowDialog();
            }

            logHelper.checklog();
            logHelper.log(logHelper.logType.info, "aplicação iniciada.");
        }

        private void tSMenuItem1_Click(object sender, EventArgs e)
        {
            Confg c = new Confg();
            c.Show();
        }

        private void tSMenuItem3_Click(object sender, EventArgs e)
        {
            logHelper.log(logHelper.logType.info, "aplicação encerrada.");

            m.t.Abort();
            this.Close();
        }

    }
}
