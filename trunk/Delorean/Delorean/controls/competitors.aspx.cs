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
        private List<ListItem> thd;
        #endregion

        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                floadData();

            dpgCompetitors.Attributes.Add("class", "button-bar");
        }

        protected void lvwCompetitors_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpgCompetitors.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            floadData();
        }

        protected void lvwCompetitors_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            HtmlTableRow Th1 = (HtmlTableRow)lvwCompetitors.FindControl("Th1");
            HtmlTableRow Th2 = (HtmlTableRow)lvwCompetitors.FindControl("Th2");
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
                        int j = 0;

                        if (Th2.Cells.Count < 1)
                        {
                            tc = new HtmlTableCell();
                            tc.InnerText = "";
                            tc.ColSpan = 2;
                            Th2.Cells.Add(tc);
                        }

                        lst.ForEach(delegate(competitors_baseTO item)
                        {
                            if (!(thd.FindAll(i => i.Value == item.rede).Count > 0))
                            {
                                thd.Add(new ListItem(item.barras, item.rede));

                                tc = new HtmlTableCell("th");
                                tc.ID = item.rede;
                                tc.InnerText = item.rede;
                                tc.ColSpan = 2;
                                Th1.Cells.Add(tc);

                                tc = new HtmlTableCell("th");
                                tc.InnerText = "valor";
                                Th2.Cells.Add(tc);

                                tc = new HtmlTableCell("th");
                                tc.InnerText = "desconto";
                                Th2.Cells.Add(tc);
                            }

                            if (barcod.InnerText.Equals(item.barras))
                            {
                                if (Tr1.Cells.Count < 2)
                                {
                                    tc = new HtmlTableCell();
                                    tc.InnerText = item.nomeprod;
                                    Tr1.Cells.Add(tc);
                                }

                                if (thd[j].Value == item.rede)
                                {
                                    tc = new HtmlTableCell();
                                    tc.InnerText = item.valor_preco.ToString();
                                    Tr1.Cells.Add(tc);

                                    tc = new HtmlTableCell();
                                    tc.InnerText = item.valor_desconto.ToString();
                                    Tr1.Cells.Add(tc);
                                    j++;
                                }
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
            thd = new List<ListItem>();
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