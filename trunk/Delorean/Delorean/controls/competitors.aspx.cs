using Assemblies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class competitors : System.Web.UI.Page
    {
        #region .:Variables:.
        private string oldbar = string.Empty;
        private List<ListItem> thd;
        #endregion

        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                floadData();

            dpgCompetitors.Attributes.Add("class", "button-bar");
        }

        protected void lvwCompetitors_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HtmlTableRow Th1 = (HtmlTableRow)lvwCompetitors.FindControl("Th1");
            HtmlTableRow Tr1 = (HtmlTableRow)e.Item.FindControl("Tr1");
            HtmlTableCell barcod = (HtmlTableCell)e.Item.FindControl("barcod");

            if (Tr1 != null)
            {
                if (!Tr1.Cells[0].InnerText.Equals(oldbar))
                {
                    DataTable dt = (DataTable)ViewState["cachedTable"];
                    HtmlTableCell tc;

                    oldbar = barcod.InnerText;

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (!(thd.FindAll(k => k.Value == dt.Columns[i].ColumnName).Count > 0))
                            {
                                thd.Add(new ListItem(oldbar, dt.Columns[i].ColumnName));

                                tc = new HtmlTableCell("th");
                                tc.ID = dt.Columns[i].ColumnName;
                                tc.InnerText = dt.Columns[i].ColumnName;
                                Th1.Cells.Add(tc);
                            }

                            if (thd[i].Value == dt.Columns[i].ColumnName && i > 0)
                            {
                                tc = new HtmlTableCell();
                                tc.InnerText = dt.Select("Ean = " + barcod.InnerText)[0][i].ToString();
                                Tr1.Cells.Add(tc);
                            }
                        }
                    }
                }
                else
                    e.Item.Controls.Remove(Tr1);
            }
        }

        protected void lvwCompetitors_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgCompetitors.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            floadData();
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            oldbar = string.Empty;
            thd = new List<ListItem>();
            ViewState["cachedTable"] = competitors_baseBLL.getAll();
            lvwCompetitors.DataSource = (DataTable)ViewState["cachedTable"];
            lvwCompetitors.DataBind();

            if (lvwCompetitors.Items.Count > 0)
                dpgCompetitors.Visible = true;
            else
                dpgCompetitors.Visible = false;
        }
        #endregion
    }
}