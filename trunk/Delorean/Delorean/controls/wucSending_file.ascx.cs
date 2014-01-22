using Assemblies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delorean.controls
{
    public partial class wucSending_file : System.Web.UI.UserControl
    {
        #region .:Events:.
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_ServerClick(object sender, EventArgs e)
        {
            string msg = buy_baseBLL.upFile(upfile.PostedFile);

            /*if (String.IsNullOrEmpty(msg)) divSces();
            else divErro(msg);*/
        }
        #endregion
    }
}