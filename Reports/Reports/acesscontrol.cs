using suLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uLibrary;

namespace Reports
{
    public partial class acesscontrol : Form
    {
        public acesscontrol()
        {
            InitializeComponent();
            lblAlert.Text = string.Empty;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            lblAlert.Text = string.Empty;

            if (string.IsNullOrEmpty(txtUser.Text))
                lblAlert.Text = "Digite o nome do usuário.";
            else if (string.IsNullOrEmpty(txtPassword.Text))
                lblAlert.Text = "Digite a senha.";
            else
            {
                string m = Users.vAccess(txtUser, txtPassword);

                if (string.IsNullOrEmpty(m))
                    this.Close();
                else
                    lblAlert.Text = m;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
                btnEnter_Click(sender, e);
        }
    }
}
