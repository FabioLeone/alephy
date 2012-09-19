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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogoff_Click1(object sender, EventArgs e)
        {
            Session["user"] = null;
            FormsAuthentication.SignOut();
            Global.Acs = "";
            Response.Redirect("Logon.aspx");
        }

        #region .: Metodos :.
        public bool VerificaEnvio() {
            bool blnOk = false;
            if(Global.Acs == null)
                Response.Redirect("Logon.aspx");

            if (Global.Acs.Equals("adm"))
                blnOk = true;
            else if (Session["user"] != null)
            {
                this.User = (UsersTO)Session["user"];
                this.Role = RolesBLL.GetByUserId(this.User.UserId,strConnectionString);
                if (this.Role.Envio)
                    blnOk = true;
                else blnOk = false;
            }
            return blnOk;
        }
        public bool VerificaRelatorio() {
            bool blnOK = false;
            if (Global.Acs.Equals("adm"))
                blnOK = true;
            else if (this.Role.RoleId > 0)
            {
                List<RelatoriosTO> clsRelatorios = RolesBLL.GetRelatoriosByUserId(this.User.UserId,strConnectionString);
                if (clsRelatorios.Count > 0)
                {
                    if (clsRelatorios.FindAll(r => r.RelatorioTipoId > 0).Count > 0)
                    {
                        blnOK = true;
                    }
                    else if (clsRole.RelatoriosTodos)
                    {
                        blnOK = true;
                    }
                }
                else if (clsRole.RelatoriosTodos)
                {
                    blnOK = true;
                }
            }
            return blnOK;
        }
        #endregion
    }
}