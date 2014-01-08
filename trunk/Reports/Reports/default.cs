using nLibrary;
using suLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uLibrary;

namespace Reports
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Util.clearSession();
            Environment.Exit(0);
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            acesscontrol ac = new acesscontrol();
            if (!Users.cAccess())
            {
                ac.ShowDialog();
            }
         
            getNetworks();

            dtpFrom.Value = DateTime.Now.AddMonths(-7);
            dtpTo.Value = DateTime.Now.AddMonths(-1);
        }

        private void getNetworks()
        {
            object o = Networks.getAll();
            if (o.GetType().Name.ToLower().Equals("string"))
            {
                MessageBox.Show(o.ToString());
            }
            else{
                cbxNetworks.DataSource = ((DataSet)o).Tables[0];
                cbxNetworks.DisplayMember = ((DataSet)o).Tables[0].Columns[1].ToString();
                cbxNetworks.ValueMember = ((DataSet)o).Tables[0].Columns[0].ToString();
                cbxNetworks.SelectedIndex = 0;
            }
        }

        private void cbxNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            object o = Stores.getByNetworkId(((ComboBox)sender).SelectedValue);

            if (o.GetType().Name.ToLower().Equals("string"))
            {
                MessageBox.Show(o.ToString());
            }
            else if(!o.GetType().Name.ToLower().Equals("object"))
            {
                cbxStores.DataSource = ((DataSet)o).Tables[0];
                cbxStores.DisplayMember = ((DataSet)o).Tables[0].Columns[2].ToString();
                cbxStores.ValueMember = ((DataSet)o).Tables[0].Columns[0].ToString();
                cbxStores.SelectedIndex = 0;
            }
        }

        private void btnZip_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Process.Start(nBll.zpFiles(cbxNetworks.SelectedValue,cbxStores.SelectedValue, dtpFrom.Value, dtpTo.Value,true));
            Cursor.Current = Cursors.Default;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            nBll.zpFiles(cbxNetworks.SelectedValue, cbxStores.SelectedValue, dtpFrom.Value, dtpTo.Value, false);
            Cursor.Current = Cursors.Default;
        }

    }
}
