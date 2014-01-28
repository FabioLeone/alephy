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
            usersBLL.IsValid(Request.QueryString["k"]);

            if (!IsPostBack){
                checkFilter();
                Control ul = Master.FindControl("ulmenu");

                foreach (Control item in ul.Controls)
                {
                    string s = string.Empty;
                    if (item != null && item.GetType().Name == "HtmlGenericControl")
                    {
                        if (((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Count > 0)
                        {
                            s = ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes["class"].Replace("current ", "");

                            if (((System.Web.UI.HtmlControls.HtmlControl)item).ID.Equals("l1"))
                            {
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Add("class", s);
                            }
                            else if (((System.Web.UI.HtmlControls.HtmlControl)item).ID.Equals("l2"))
                            {
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Remove("class");
                                ((System.Web.UI.HtmlControls.HtmlControl)item).Attributes.Add("class", "current " + s);
                            }
                        }   
                    }
                }
            }

            dpgFactors.Attributes.Add("class", "button-bar");
        }

        protected void txtcond_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            string s = string.Empty;

            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtcond").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwFactors.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(sales_factorBLL.setCond(id, tb.Text, s));
                    break;
                }
            }
        }

        protected void txtmarg_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            string s = string.Empty;

            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtmarg").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwFactors.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(sales_factorBLL.setMargin(id, tb.Text, s));
                    break;
                }
            }
        }

        protected void txtdesc_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int id = 0;
            string s = string.Empty;

            foreach (ListViewItem item in lvwFactors.Items)
            {
                if (item.FindControl("txtdesc").UniqueID == tb.UniqueID)
                {
                    id = (int)lvwFactors.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwFactors.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(sales_factorBLL.setDesc(id, tb.Text, s));
                    break;
                }
            }
        }

        protected void lvwFactors_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgFactors.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            floadData();
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            lvwFactors.DataSource = sales_factorBLL.getAll();
            lvwFactors.DataBind();
        }

        private void loadData(List<sales_factorTO> lst)
        {
            lvwFactors.DataSource = lst;
            lvwFactors.DataBind();
        }

        private void checkFilter()
        {
            if (Request.QueryString["f"] == null)
                floadData();
            else
            {
                helpers.ClearCache<List<base_viewer>>("factors" + helpers.GetSession().UserId);
                switch (Request.QueryString["f"])
                {
                    case "a":
                        floadData();
                        break;
                    case "f":
                        loadData(sales_factorBLL.getByFilter("f"));
                        break;
                    case "u":
                        loadData(sales_factorBLL.getByFilter("u"));
                        break;
                    default:
                        floadData();
                        break;
                }
            }
        }
        #endregion
    }
}