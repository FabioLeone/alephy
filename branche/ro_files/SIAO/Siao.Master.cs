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
            this.User = UsersBLL.GetUserSession();
            if (this.User.UserId == 0) { Response.Redirect("Logon.aspx"); }
        }

        protected void lbtnLogoff_Click1(object sender, EventArgs e)
        {
            UsersBLL.ClearUserSession();
            FormsAuthentication.SignOut();
            Response.Redirect("Logon.aspx");
        }
        #endregion

        #region .: Metodos :.
        public bool VerificaEnvio() {
            if (this.User.TipoId == null || this.User.TipoId == 0)
            {
                return false;
            }
            else
                return UsersBLL.ValidaEnvio(this.User.TipoId);
        }

        public bool VerificaRelatorio() {
            if (this.User.TipoId == null || this.User.TipoId == 0)
            {
                return false;
            }
            else
                return UsersBLL.ValidaRelatorio(this.User.TipoId);
        }

        public bool VerificaConfig() {
            return this.User.TipoId.Equals(1);
        }
        #endregion
    }
}