using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SIAO.SRV;
using System.Configuration;
using SIAO.SRV.BLL;

namespace SIAO.Controls
{
    public partial class wucListFiles : System.Web.UI.UserControl
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDados();
        }

        private void LoadDados()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes(scn);

            if (ds.Tables.Count > 0)
            {
                ddlRedes.DataSource = ds.Tables[0];
                ddlRedes.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedes.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedes.DataBind();
                ddlRedes.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRedes.Items.Insert(1, new ListItem("Independentes", "0"));
                ddlRedes.SelectedIndex = 0;
            }

            ddlAno.Items.Add(new ListItem(String.Empty, String.Empty));
            for (int i = 0; i < 3; i++)
            {
                ddlAno.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddlAno.SelectedIndex = 0;
        }

        protected void lvwFiles_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hfCnpj = (HiddenField)e.Item.FindControl("hfCnpj");
            ListView lvwMeses = (ListView)e.Item.FindControl("lvwMeses");

            lvwMeses.DataSource = FilesBLL.GetByCnpj(hfCnpj.Value, ddlAno.SelectedValue, scn);
            lvwMeses.DataBind();
        }

        protected void lvwMeses_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HiddenField hfMes = (HiddenField)e.Item.FindControl("hfMes");
            Label lblCheck = (Label)e.Item.FindControl("lblCheck");

            if (Convert.ToInt32(hfMes.Value) > 0)
                lblCheck.Text = "X";
            else
                lblCheck.Text = "";
                
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlRedes.SelectedValue)) {
                if (!String.IsNullOrEmpty(ddlAno.SelectedValue))
                {
                    int intRedeId = Convert.ToInt32(ddlRedes.SelectedValue);
                    int intAno = Convert.ToInt32(ddlAno.SelectedValue);

                    lvwFiles.DataSource = FilesBLL.GetByYearAndRedeId(intAno, intRedeId, scn);
                    lvwFiles.DataBind();
                }
            }
        }
    }
}