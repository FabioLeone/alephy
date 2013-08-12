using System;
using System.Web.Security;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;
using System.Collections.Generic;
using System.Configuration;

namespace SIAO
{
    public partial class Siao : System.Web.UI.MasterPage
    {
        #region .: Variables :.
        private string strConnectionString = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        private RolesTO clsRole = new RolesTO();
        private UsersTO clsUser = new UsersTO();
        #endregion

        #region .: Properties :.
        public RolesTO Role { get {
            if (this.ViewState["role"] == null) return this.clsRole;
            else return (RolesTO)this.ViewState["role"];
        }
            set {
                this.ViewState["role"] = value;
                this.clsRole = value;
            }
        }
        public UsersTO User { get {
            if (this.ViewState["user"] == null) return this.clsUser;
            else return (UsersTO)this.ViewState["user"];
        }
            set {
                this.ViewState["user"] = value;
                this.clsUser = value;
            }
        }
        #endregion

        #region .: Events :.
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogoff_Click1(object sender, EventArgs e)
        {
            Session["user"] = null;
            FormsAuthentication.SignOut();
            Global.TId = 0;
            Response.Redirect("Logon.aspx");
        }
        #endregion

        #region .: Metodos :.
        public bool VerificaEnvio() {
            if (Global.TId == null || Global.TId == 0)
            {
                return false;
                Response.Redirect("Logon.aspx");
            }
            else
                return UsersBLL.ValidaEnvio(Global.TId);
        }

        public bool VerificaRelatorio() {
            if (Global.TId == null || Global.TId == 0)
            {
                return false;
                Response.Redirect("Logon.aspx");
            }
            else
                return UsersBLL.ValidaRelatorio(Global.TId);
        }
        #endregion
    }
}