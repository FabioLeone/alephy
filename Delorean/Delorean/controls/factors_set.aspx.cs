using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class factors_set : System.Web.UI.Page
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                floadData();

            dpgFactors.Attributes.Add("class", "button-bar");
        }

        protected void txtcond_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtcond").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[item.DataItemIndex].Value;
                    if (buy_baseBLL.upCost(id, tb.Text))
                        floadData();
                    break;
                }
            }
        }

        protected void txtmarg_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtmarg").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[item.DataItemIndex].Value;
                    if (buy_baseBLL.upCost(id, tb.Text))
                        floadData();
                    break;
                }
            }
        }

        protected void txtdesc_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtdesc").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[item.DataItemIndex].Value;
                    if (buy_baseBLL.upCost(id, tb.Text))
                        floadData();
                    break;
                }
            }
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            lvwFactors.DataSource = sales_factorBLL.getAll();
            lvwFactors.DataBind();
        }

        #endregion
    }
}