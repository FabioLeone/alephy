using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Configuration;
using System.Data;
using SIAO.SRV;
using SIAO.SRV.BLL;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace SIAO.Controls
{
    public partial class wucRoles : System.Web.UI.UserControl
    {
        #region .: Variables :.
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        private List<UsersTO> clsUsers = new List<UsersTO>();
        private List<RolesTO> clsRoles = new List<RolesTO>();
        private List<RelatoriosTO> clsRelatorios = new List<RelatoriosTO>();
        #endregion

        #region .: Properties :.
        public List<UsersTO> Users
        {
            get
            {
                if (this.ViewState["users"] == null) return this.clsUsers;
                else return (List<UsersTO>)this.ViewState["users"];
            }
            set
            {
                this.ViewState["users"] = value;
                this.clsUsers = value;
            }
        }
        public List<RolesTO> Roles { 
            get {
            if (this.ViewState["roles"] == null) return this.clsRoles;
            else return (List<RolesTO>)this.ViewState["roles"];
        }
            set {
                this.ViewState["roles"] = value;
                this.clsRoles = value;
            }
        }
        public List<RelatoriosTO> Relatorios { get {
            if (this.ViewState["relatorios"] == null) return this.clsRelatorios;
            else return (List<RelatoriosTO>)this.ViewState["relatorios"];
        }
            set {
                this.ViewState["relatorios"] = value;
                this.clsRelatorios = value;
            }
        }
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PopulaDados();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            int intRedeId = 0;
            if (!String.IsNullOrEmpty(ddlRedes.SelectedValue))
            {
                intRedeId = Convert.ToInt32(ddlRedes.SelectedValue);
                this.Users = UsersBLL.GetByRedeId(intRedeId, scn);
                LoadList(intRedeId);
                btnPlus.Enabled = true;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwRole.Items)
            {
                CheckBox chbEnvio = (CheckBox)item.FindControl("chbEnvio");
                CheckBox chbAll = (CheckBox)item.FindControl("chbAll");
                CheckBoxList cblReportsGrafics = (CheckBoxList)item.FindControl("cblReportsGrafics");
                HiddenField hfRoleId = (HiddenField)item.FindControl("hfRoleId");
                DropDownList ddlUsuarios = (DropDownList)item.FindControl("ddlUsuarios");

                int intRoleId = String.IsNullOrEmpty(hfRoleId.Value) ? 0 : Convert.ToInt32(hfRoleId.Value);
                int intUsuarioId = String.IsNullOrEmpty(ddlUsuarios.SelectedValue) ? 0 : Convert.ToInt32(ddlUsuarios.SelectedValue);

                this.Roles.Find(r => r.RoleId == intRoleId).UserId = intUsuarioId;

                if (chbEnvio.Checked)
                    this.Roles.Find(r => r.RoleId == intRoleId).Envio = true;

                if (chbAll.Checked)
                    this.Roles.Find(r => r.RoleId == intRoleId).RelatoriosTodos = true;
                else
                    RecuperaDados();
            }

            if (RolesBLL.InsertUpdate(this.Roles, this.Relatorios, scn))
            {
                div("Alterações salvas com sucesso.");
            }
            else {
                divErro("Falha ao atualizar as informações.");
            }
        }
        protected void lvwRole_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            DropDownList ddlUsuarios = (DropDownList)e.Item.FindControl("ddlUsuarios");
            HiddenField hfRoleId = (HiddenField)e.Item.FindControl("hfRoleId");
            CheckBox chbEnvio = (CheckBox)e.Item.FindControl("chbEnvio");
            CheckBox chbAll = (CheckBox)e.Item.FindControl("chbAll");
            CheckBoxList cblReportsGrafics = (CheckBoxList)e.Item.FindControl("cblReportsGrafics");

            ddlUsuarios.DataSource = this.Users;
            ddlUsuarios.DataTextField = "Name";
            ddlUsuarios.DataValueField = "UserId";
            ddlUsuarios.DataBind();
            ddlUsuarios.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlUsuarios.SelectedIndex = 0;

            int intRoleId = String.IsNullOrEmpty(hfRoleId.Value)? 0 : Convert.ToInt32(hfRoleId.Value);
            if (this.Roles.Find(r => r.RoleId == intRoleId).UserId > 0)
            {
                ddlUsuarios.SelectedValue = this.Roles.Find(r => r.RoleId == intRoleId).UserId.ToString();
                chbEnvio.Checked = this.Roles.Find(r => r.RoleId == intRoleId).Envio;
                if (this.Roles.Find(r => r.RoleId == intRoleId).RelatoriosTodos)
                {
                    chbAll.Checked = true;
                    cblReportsGrafics.Style.Add("display", "none");
                }
                else {
                    cblReportsGrafics.Style.Add("display", "block");
                    //PopulaCheckList(cblReportsGrafics, this.Roles.Find(r => r.RoleId == intRoleId).UserId);
                }
            }
        }

        protected void chbEnvio_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwRole.Items)
            {
                CheckBox chbEnvio = (CheckBox)item.FindControl("chbEnvio");
                HiddenField hfRoleId = (HiddenField)item.FindControl("hfRoleId");
                DropDownList ddlUsuarios = (DropDownList)item.FindControl("ddlUsuarios");
                if (chbEnvio.Checked) {
                    int intRoleId = String.IsNullOrEmpty(hfRoleId.Value)? 0 : Convert.ToInt32(hfRoleId.Value);
                    int intUsuarioId = String.IsNullOrEmpty(ddlUsuarios.SelectedValue)? 0 : Convert.ToInt32(ddlUsuarios.SelectedValue);
                    this.Roles.Find(r => r.RoleId == intRoleId).Envio = true;
                    this.Roles.Find(r => r.RoleId == intRoleId).UserId = intUsuarioId;
                }
            }
        }
        protected void chbAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwRole.Items)
            {
                CheckBox chbAll = (CheckBox)item.FindControl("chbAll");
                CheckBoxList cblReportsGrafics = (CheckBoxList)item.FindControl("cblReportsGrafics");
                HiddenField hfRoleId = (HiddenField)item.FindControl("hfRoleId");
                DropDownList ddlUsuarios = (DropDownList)item.FindControl("ddlUsuarios");
                if (chbAll.Checked)
                {
                    cblReportsGrafics.Style.Add("display", "none");
                    int intRoleId = String.IsNullOrEmpty(hfRoleId.Value) ? 0 : Convert.ToInt32(hfRoleId.Value);
                    int intUsuarioId = String.IsNullOrEmpty(ddlUsuarios.SelectedValue) ? 0 : Convert.ToInt32(ddlUsuarios.SelectedValue);
                    this.Roles.Find(r => r.RoleId == intRoleId).RelatoriosTodos = true;
                    if(this.Roles.Find(r => r.RoleId == intRoleId).UserId == 0)
                        this.Roles.Find(r => r.RoleId == intRoleId).UserId = intUsuarioId;
                }
                else {
                    cblReportsGrafics.Style.Add("display", "block");
                }
            }
        }
        protected void btnPlus_Click(object sender, EventArgs e)
        {
            AddRole();
        }
        #endregion

        #region .: Metodos :.
        private void PopulaDados()
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes();

            if (ds.Tables.Count > 0)
            {
                ddlRedes.DataSource = ds.Tables[0];
                ddlRedes.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRedes.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRedes.DataBind();
                ddlRedes.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRedes.Items.Insert(1, new ListItem("Independentes", "0"));
                ddlRedes.SelectedIndex = 0;
            }

            btnPlus.Enabled = false;
        }
        private void div(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "28%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            upAcesso.ContentTemplateContainer.Controls.Add(divInfo);
        }

        private void divErro(string strMsg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgError";
            divInfo.Attributes.Add("class", "error");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divInfo.InnerHtml = "<p>" + strMsg + "</p>";

            upAcesso.ContentTemplateContainer.Controls.Add(divInfo);
        }

        private void LoadList(int intRedeId)
        {
            this.Roles = RolesBLL.GetByRedeId(intRedeId, scn);
            if (this.Roles.Count > 0)
                lvwRole.DataSource = this.Roles;
            else
            {
                AddRole();
            }
            lvwRole.DataBind();
        }
        private void AddRole()
        {
            if (this.Roles.Count > 0)
            {
                RecuperaDados();
                this.Roles.Add(new RolesTO()
                {
                    RoleId = this.Roles.Count + 1,
                    Add = true
                });
            }
            else
                this.Roles.Add(new RolesTO()
                {
                    RoleId = 1,
                    Add = true
                });
            lvwRole.DataSource = this.Roles;
            lvwRole.DataBind();
        }

        private void RecuperaDados()
        {
            this.Relatorios.Clear();
            foreach (ListViewItem item in lvwRole.Items)
            {
                CheckBoxList cblReportsGrafics = (CheckBoxList)item.FindControl("cblReportsGrafics");
                DropDownList ddlUsuarios = (DropDownList)item.FindControl("ddlUsuarios");
                foreach (ListItem li in cblReportsGrafics.Items)
                {
                    RelatoriosTO clsRelatorio = new RelatoriosTO()
                    {
                        RelatorioTipoId = li.Selected ? Convert.ToInt32(li.Value) : 0,
                        Rede_Id = Convert.ToInt32(ddlUsuarios.SelectedValue)
                    };

                    this.Relatorios.Remove(clsRelatorio);
                    this.Relatorios.Add(clsRelatorio);
                }
            }
        }

        /*private void PopulaCheckList(CheckBoxList cblReportsGrafics, int UserId)
        {
            List<RelatoriosTO> clsRelatorios = new List<RelatoriosTO>();
            if (this.Relatorios.FindAll(r=>r.Rede_Id == UserId).Count > 0)
                clsRelatorios = this.Relatorios;
            else
                clsRelatorios = this.Relatorios = RolesBLL.GetRelatoriosByUserId(UserId, scn);

            if (clsRelatorios.Count > 0)
                clsRelatorios.ForEach(delegate(RelatoriosTO _relatorio) {
                    if (_relatorio.Rede_Id.Equals(UserId) && _relatorio.RelatorioTipoId>0)
                        cblReportsGrafics.Items.FindByValue(_relatorio.RelatorioTipoId.ToString()).Selected = true;
                });
        }*/
        #endregion

    }
}