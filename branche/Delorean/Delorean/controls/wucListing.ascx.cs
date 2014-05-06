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
                checkFilter();

            dpgProducts.Attributes.Add("class", "button-bar");
        }

        protected void txtvcost_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            string s = string.Empty;

            foreach (ListViewItem item in lvwProducts.Items)
            {
                if (item.FindControl("txtvcost").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwProducts.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwProducts.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(buy_baseBLL.setCost(id, tb.Text, s));
                    
                    break;
                }
            }
        }

        protected void lvwProducts_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgProducts.SetPageProperties(e.StartRowIndex, e.MaximumRows,false);
            floadData();
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            lvwProducts.DataSource = special_baseBLL.getProducts();
            lvwProducts.DataBind();

            if (lvwProducts.Items.Count > 0)
                dpgProducts.Visible = true;
            else
                dpgProducts.Visible = false;
        }

        private void loadData(List<base_viewer> lst)
        {
            lvwProducts.DataSource = lst;
            lvwProducts.DataBind();

            if (lvwProducts.Items.Count > 0)
                dpgProducts.Visible = true;
            else
                dpgProducts.Visible = false;
        }

        private void checkFilter()
        {
            if (Request.QueryString["f"] == null && Request.QueryString["s"] == null)
                floadData();
            else {
                helpers.ClearCache<List<base_viewer>>("products" + helpers.GetSession().UserId);

                loadData(special_baseBLL.getByFilter(Request.QueryString["f"], Request.QueryString["s"]));
            }
        }
        #endregion

    }
}