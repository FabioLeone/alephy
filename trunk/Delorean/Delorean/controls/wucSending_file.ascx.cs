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
            HttpPostedFile hpf = upfile.PostedFile;
            if (hpf != null && hpf.ContentLength > 0) {
                string fn = Path.GetFileName(hpf.FileName);
                hpf.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fn)));
            }
        }
        #endregion
    }
}