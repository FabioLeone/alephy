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
        #region .:Properties:.
        private int _oldidx = 0;
        private string _nxtfld = string.Empty;

        public int oldidx
        {
            get {
                if (ViewState["oldidx"] == null) return this._oldidx;
                else return (int)ViewState["oldidx"];
            }

            set {
                ViewState["oldidx"] = value;
                this._oldidx = value;
            }
        }

        public string nxtfld {
            get {
                if (ViewState["oldfld"] == null) return this._nxtfld;
                else return ViewState["oldfld"].ToString();
            }
            set {
                ViewState["oldfld"] = value;
                this._nxtfld = value;
            }
        }
        #endregion

        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            usersBLL.IsValid(Request.QueryString["k"]);

            if (!IsPostBack)
            {
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
                    this.nxtfld = "txtmarg";
                    this.oldidx = tb.TabIndex;
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
                    this.nxtfld = "txtdesc";
                    this.oldidx = tb.TabIndex;
                    id = (int)lvwFactors.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwFactors.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(sales_factorBLL.setMargin(id, tb.Text.Replace("%",""), s));
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
                    this.nxtfld = string.Empty;
                    this.oldidx = -1;
                    id = (int)lvwFactors.DataKeys[tb.TabIndex].Value;
                    s = ((System.Web.UI.HtmlControls.HtmlTableCell)lvwFactors.Items[tb.TabIndex].FindControl("barcod")).InnerText;
                    loadData(sales_factorBLL.setDesc(id, tb.Text.Replace("%", ""), s));
                    break;
                }
            }
        }

        protected void lvwFactors_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgFactors.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            floadData();
        }

        protected void lvwFactors_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(this.oldidx >= 0)
                if (((ListViewDataItem)e.Item).DataItemIndex == this.oldidx) {
                    if (lvwFactors.Items.Count > 0)
                        ((TextBox)lvwFactors.Items[this.oldidx].FindControl(this.nxtfld)).Focus();
                }
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            lvwFactors.DataSource = sales_factorBLL.getAll();
            lvwFactors.DataBind();

            if (lvwFactors.Items.Count > 0)
                dpgFactors.Visible = true;
            else
                dpgFactors.Visible = false;
        }

        private void loadData(List<sales_factorTO> lst)
        {
            lvwFactors.DataSource = lst;
            lvwFactors.DataBind();

            if (lvwFactors.Items.Count > 0)
                dpgFactors.Visible = true;
            else
                dpgFactors.Visible = false;
        }

        private void checkFilter()
        {
            if (Request.QueryString["f"] == null && Request.QueryString["s"] == null)
                floadData();
            else
            {
                helpers.ClearCache<List<base_viewer>>("factors" + helpers.GetSession().UserId);

                loadData(sales_factorBLL.getByFilter(Request.QueryString["f"], Request.QueryString["s"]));
            }
        }
        #endregion
    }
}