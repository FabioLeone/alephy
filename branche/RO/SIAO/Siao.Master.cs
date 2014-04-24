using System;
using System.Web.Security;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using SIAO.SRV;

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

            if (!IsPostBack)
            { 
                if (UsersBLL.ValidaAcesso(this.User, dvRedes, dvLoja, dvFiltro))
                {
                    LojasBLL.getLojas(this.User, ddlLojaRelatorios, dvFiltro, ulArq);
                }
                else
                    getRedes();
            }
        }

        protected void lbtnLogoff_Click1(object sender, EventArgs e)
        {
            UsersBLL.ClearUserSession();
            FormsAuthentication.SignOut();
            Response.Redirect("Logon.aspx");
        }

        protected void ddlRedesRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue))
                LojasBLL.getLojas(ddlLojaRelatorios, Convert.ToInt32(ddlRedesRelatorios.SelectedValue));
        }

        protected void btnBusca_Click(object sender, EventArgs e)
        {
            LojasBLL.getFiles(ddlLojaRelatorios.SelectedValue, ulArq);
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
        
        private void getRedes()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes();

            if (ds.Tables.Count > 0)
            {
                ddlRedesRelatorios.DataSource = ds.Tables[0];
                ddlRedesRelatorios.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedesRelatorios.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedesRelatorios.DataBind();
                ddlRedesRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem(String.Empty, String.Empty));
                ddlRedesRelatorios.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Independentes", "0"));
                ddlRedesRelatorios.SelectedIndex = 0;
            }
        }

        #endregion

    }
}