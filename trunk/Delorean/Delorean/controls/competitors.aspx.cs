using Assemblies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class competitors : System.Web.UI.Page
    {
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
            if (hdr != null) {
                Literal ltrl = new Literal();
                List<competitors_baseTO> lst = (List<competitors_baseTO>)ViewState["cachedTable"];

                if (lst.Count > 0) {
                    ltrl.Text += "<th>" + "testc" + "</th>";                
                }
            }
        }
        #endregion

        #region .:Methods:.
        private void floadData()
        {
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