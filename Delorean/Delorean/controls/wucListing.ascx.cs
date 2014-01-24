using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class wucListing : System.Web.UI.UserControl
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                floadData();

            dpgProducts.Attributes.Add("class", "button-bar");
        }

        protected void txtvcost_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            foreach (ListViewItem item in lvwProducts.Items)
            {
                if (item.FindControl("txtvcost").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwProducts.DataKeys[item.DataItemIndex].Value;
                    if(buy_baseBLL.upCost(id, tb.Text))
                        floadData();
                    break;
                }
            }
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            lvwProducts.DataSource = special_baseBLL.getProducts();
            lvwProducts.DataBind();
        }

        #endregion
    }
}