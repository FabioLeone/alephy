using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using SIAO.SRV;
using SIAO.SRV.TO;

namespace SIAO.Controls
{
    public partial class wucAnalise : System.Web.UI.UserControl
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UsersBLL.GetUserSession().UserId == 0) { Response.Redirect("Logon.aspx"); }
        }

        protected void btnAnalise_ServerClick(object sender, EventArgs e)
        {
            ListItemCollection licFiltro = wucFilter1.ResultData();
            int intId = 0;
            intId = wucFilter1.RedeId;
            if (intId > 0)
                RelatoriosBLL.GetAnalise(licFiltro, intId);
            else
                RelatoriosBLL.GetAnalise(licFiltro,0);
            
        }
        #endregion

    }
}