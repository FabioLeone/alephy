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
                loadData();
        }

        private void loadData()
        {
            lvwProducts.DataSource = special_baseBLL.getProducts();
        }
        #endregion
    }
}