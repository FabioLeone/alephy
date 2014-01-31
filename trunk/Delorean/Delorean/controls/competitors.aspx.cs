using Assemblies;
using System;
using System.Collections.Generic;
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
        #endregion

        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                floadData();
        }

        protected void lvwCompetitors_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgCompetitors.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            floadData();
        }

        protected void lvwCompetitors_DataBound(object sender, EventArgs e)
        {
            PlaceHolder hdr = (PlaceHolder)lvwCompetitors.FindControl("hdr");
            if (hdr != null)
            {
                Literal ltrl = new Literal();
                List<competitors_baseTO> lst = (List<competitors_baseTO>)ViewState["cachedTable"];

                if (lst.Count > 0)
                {
                    string s = string.Empty;

                    lst.ForEach(delegate(competitors_baseTO item)
                    {
                        if (!ltrl.Text.Contains(item.rede))
                        {
                            ltrl.Text += "<th colspan='2'>" + item.rede + "</th>";
                            s += "<th>valor</th><th>desconto</th>";
                        }
                    });
                    ltrl.Text += "</tr><tr><th colspan='2'>&nbsp;</th>" + s + "</tr>";
                }

                if (hdr.Controls.Count > 0)
                {
                    if (!((Literal)hdr.Controls[0]).Text.Equals(ltrl.Text))
                    {
                        hdr.Controls.Remove(hdr.Controls[0]);
                        hdr.Controls.Add(ltrl);
                    }
                }
                else
                    hdr.Controls.Add(ltrl);
            }
        }

        protected void lvwCompetitors_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HtmlTableRow Tr1 = (HtmlTableRow)e.Item.FindControl("Tr1");
            HtmlTableCell barcod = (HtmlTableCell)e.Item.FindControl("barcod");

            if (Tr1 != null)
            {
                if (!Tr1.Cells[0].InnerText.Equals(oldbar))
                {
                    List<competitors_baseTO> lst = (List<competitors_baseTO>)ViewState["cachedTable"];
                    HtmlTableCell tc;

                    oldbar = barcod.InnerText;

                    if (lst.Count > 0)
                    {
                        lst.ForEach(delegate(competitors_baseTO item)
                        {
                            if (barcod.InnerText.Equals(item.barras))
                            {
                                if (Tr1.Cells.Count < 2)
                                {
                                    tc = new HtmlTableCell();
                                    tc.InnerText = item.nomeprod;
                                    Tr1.Cells.Add(tc);
                                }

                                tc = new HtmlTableCell();
                                tc.InnerText = item.valor_preco.ToString();
                                Tr1.Cells.Add(tc);

                                tc = new HtmlTableCell();
                                tc.InnerText = item.valor_desconto.ToString();
                                Tr1.Cells.Add(tc);
                            }
                        });
                    }
                }
                else
                    e.Item.Controls.Remove(Tr1);
            }
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
            oldbar = string.Empty;
            ViewState["cachedTable"] = competitors_baseBLL.getAll();
            lvwCompetitors.DataSource = (List<competitors_baseTO>)ViewState["cachedTable"];
            lvwCompetitors.DataBind();

            if (lvwCompetitors.Items.Count > 0)
                dpgCompetitors.Visible = true;
            else
                dpgCompetitors.Visible = false;
        }
        #endregion
    }
}