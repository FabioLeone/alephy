using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;
using System.Web.UI.WebControls;

namespace SIAO.SRV.BLL
{
    public class UsersBLL
    {
        #region .: Métodos :.

        public static bool ValidaEnvio(int intTId)
        {
            // Retorna true se o usuário for Administrador ou Drogaria.
            return (intTId.Equals(1) || intTId.Equals(2));
        }

        public static bool ValidaRelatorio(int intTId)
        {
            // Retorna true se o usuário for Administrador ou Drogaria.
            return (intTId.Equals(1) || intTId.Equals(2) || intTId.Equals(3));
        }

        public static void SetUserSession(UsersTO objUser)
        {
            String jssObject = new JavaScriptSerializer().Serialize(objUser);
            clsFuncs of = new clsFuncs();

            HttpContext.Current.Session[of.encr(objUser.UserName)] = of.encr(jssObject);
        }

        public static UsersTO GetUserSession()
        {
            UsersTO objUser = new UsersTO();
            JavaScriptSerializer jssObject = new JavaScriptSerializer();
            clsFuncs of = new clsFuncs();
            Type t = objUser.GetType();
            String strName = HttpContext.Current.User.Identity.Name;

            if (HttpContext.Current.Session[of.encr(strName)] != null)
                return (UsersTO)jssObject.Deserialize(of.denc(HttpContext.Current.Session[of.encr(strName)].ToString()), t);
            else
                return objUser;
        }

        public static void ClearUserSession()
        {
            clsFuncs of = new clsFuncs();
            String strName = HttpContext.Current.User.Identity.Name;
            HttpContext.Current.Session[of.encr(strName)] = null;
        }

        public static bool ValidaAcesso(UsersTO users, System.Web.UI.HtmlControls.HtmlGenericControl dvRedes, System.Web.UI.HtmlControls.HtmlGenericControl dvLoja, System.Web.UI.HtmlControls.HtmlGenericControl dvFiltro)
        {
            switch (users.TipoId)
            {
                case 1:
                    switch (users.Nivel)
                    {
                        case 1:
                        case 2:
                            dvFiltro.Visible = true;
                            dvLoja.Visible = true;
                            dvRedes.Visible = false;
                            return true;
                        default:
                            dvFiltro.Visible = true;
                            dvLoja.Visible = true;
                            dvRedes.Visible = true;
                            break;
                    }
                    return false;
                default:
                    dvFiltro.Visible = true;
                    dvLoja.Visible = true;
                    dvRedes.Visible = false;
                    return true;
            }
        }

        public static void ValidaAcesso(UsersTO oUser, System.Web.UI.HtmlControls.HtmlGenericControl dvRede, System.Web.UI.HtmlControls.HtmlGenericControl dvLoja, DropDownList ddlRede, DropDownList ddlEdRedes, DropDownList ddlLoja, CheckBox cbxAtivo)
        {
            switch (oUser.TipoId)
            {
                case 1:
                    switch (oUser.Nivel)
                    {
                        case 1:
                        case 2:
                            dvLoja.Visible = true;
                            dvRede.Visible = false;
                            LojasBLL.LoadLojas(oUser, ddlLoja);
                            ddlRede.Enabled = false;
                            cbxAtivo.Visible = false;
                            break;
                        default:
                            dvLoja.Visible = true;
                            dvRede.Visible = true;
                            RedesBLL.LoadRedes(ddlEdRedes);
                            cbxAtivo.Visible = true;
                            break;
                    }
                    break;
                default:
                    dvLoja.Visible = true;
                    dvRede.Visible = false;
                    LojasBLL.LoadLojas(oUser, ddlLoja);
                    cbxAtivo.Visible = false;
                    break;
            }
        }

        public static bool ValidaAcesso(UsersTO users, System.Web.UI.HtmlControls.HtmlGenericControl dvRedes, System.Web.UI.HtmlControls.HtmlGenericControl dvLoja, System.Web.UI.HtmlControls.HtmlGenericControl dvFiltro, System.Web.UI.HtmlControls.HtmlGenericControl li3)
        {
            switch (users.TipoId)
            {
                case 1:
                    switch (users.Nivel)
                    {
                        case 1:
                        case 2:
                            dvFiltro.Visible = true;
                            dvLoja.Visible = true;
                            dvRedes.Visible = false;
                            li3.Visible = false;
                            return true;
                        default:
                            dvFiltro.Visible = true;
                            dvLoja.Visible = true;
                            dvRedes.Visible = true;
                            li3.Visible = true;
                            break;
                    }
                    return false;
                default:
                    dvFiltro.Visible = true;
                    dvLoja.Visible = true;
                    dvRedes.Visible = false;
                    li3.Visible = false;
                    return true;
            }
        }

        public static void GetNiveis(ref DropDownList ddlNivel, ref System.Web.UI.HtmlControls.HtmlTableCell tdNA)
        {
            if (GetUserSession().Nivel.Equals((int)Nivel.a))
            {
                ddlNivel.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlNivel.Items.Insert(1, new ListItem("A", "0"));
                ddlNivel.Items.Insert(2, new ListItem("R", "1"));
                ddlNivel.Items.Insert(3, new ListItem("L", "2"));
                ddlNivel.Items.Insert(4, new ListItem("I", "3"));
                ddlNivel.SelectedIndex = 0;
                ddlNivel.Visible = true;
                tdNA.Visible = true;
            }
            else
            {
                ddlNivel.Visible = false;
                tdNA.Visible = false;
            }
        }

        public static void GetTiposAll(ref DropDownList ddlAcess)
        {
            switch (GetUserSession().Nivel)
            {
                case (int)Nivel.a:
                    ddlAcess.DataSource = UsersBLL.GetTiposAll();
                    ddlAcess.DataTextField = "Tipo";
                    ddlAcess.DataValueField = "id";
                    ddlAcess.DataBind();
                    ddlAcess.Items.Insert(0, new ListItem("Selecione", "0"));
                    ddlAcess.SelectedIndex = 0;
                    break;
                case (int)Nivel.r:
                    ddlAcess.Items.Insert(0, new ListItem("Selecione", "0"));
                    ddlAcess.Items.Insert(1, new ListItem("Drogaria", "2"));
                    ddlAcess.Items.Insert(2, new ListItem("Rede", "3"));
                    ddlAcess.SelectedIndex = 0;
                    break;
            }

        }

        public static string AddUser(DropDownList ddlAcess, TextBox txtAcsName, TextBox txtEmail, CheckBox cbxAtivo, TextBox txtNome, TextBox txtSenha, TextBox txtValidade, DropDownList ddlNivel)
        {
            UsersTO clsUser = new UsersTO()
            {
                TipoId = Convert.ToInt32(ddlAcess.SelectedValue),
                Name = txtAcsName.Text,
                CreateDate = DateTime.Today,
                Email = txtEmail.Text,
                Inactive = !cbxAtivo.Checked,
                UserName = txtNome.Text,
                Password = txtSenha.Text,
                ExpirationDate = Convert.ToDateTime(txtValidade.Text)
            };

            if (ddlNivel.Visible && !String.IsNullOrEmpty(ddlNivel.SelectedValue))
                clsUser.Nivel = Convert.ToInt32(ddlNivel.SelectedValue);
            else
                clsUser.Nivel = 0;

            if (!UsersBLL.GetUserSession().Nivel.Equals((int)UsersBLL.Nivel.a))
                clsUser.owner = UsersBLL.GetUserSession().UserId;

            return clsControl.AddUser(clsUser);
        }

        public static string UpdateUser(UsersTO user, DropDownList ddlAcess, TextBox txtAcsName, TextBox txtEmail, CheckBox cbxAtivo, TextBox txtNome, TextBox txtSenha, TextBox txtValidade, DropDownList ddlNivel)
        {
            UsersTO clsUser = new UsersTO();

            clsUser.UserId = user.UserId;
            clsUser.TipoId = Convert.ToInt32(ddlAcess.SelectedValue);
            clsUser.Name = txtAcsName.Text;
            clsUser.CreateDate = DateTime.Today;
            clsUser.Email = txtEmail.Text;
            clsUser.Inactive = !cbxAtivo.Checked;
            clsUser.UserName = txtNome.Text;

            if (!string.IsNullOrEmpty(txtSenha.Text))
                clsUser.Password = txtSenha.Text;
            else
                clsUser.Password = user.Password;

            clsUser.ExpirationDate = Convert.ToDateTime(txtValidade.Text);

            if (ddlNivel.Visible && !String.IsNullOrEmpty(ddlNivel.SelectedValue))
                clsUser.Nivel = Convert.ToInt32(ddlNivel.SelectedValue);
            else
                clsUser.Nivel = 0;

            return clsControl.UpdateUser(clsUser);
        }

        public static void GetList(GridView gvwUsers)
        {
            List<UsersTO> lstUsers;
            UsersTO objUser = GetUserSession();

            switch (objUser.Nivel)
            {
                case (int)Nivel.r:
                    lstUsers = GetAllMinion(objUser);
                    break;
                default:
                    lstUsers = GetLst();
                    break;
            }

            gvwUsers.DataSource = lstUsers;
            gvwUsers.EmptyDataText = "Nenhum registro encontrado.";
            gvwUsers.DataBind();
        }

        #endregion

        #region .: Search :.

        private static List<UsersTO> GetLst()
        {
            return UsersDAL.GetLst();
        }

        public static UsersTO GetById(int intUserId)
        {
            return UsersDAL.GetById(intUserId);
        }

        #endregion

        #region .: Custom Search :.

        public static UsersTO GetByNameAndPassword(string strName, string strPassword, string strConnectionString)
        {
            UsersTO u = UsersDAL.GetByNameAndPassword(strName, strPassword, strConnectionString);
            u.RedeId = clsControl.GetRedeByUserId(u.UserId).RedeId;
            return u;
        }

        public static object GetIndicesByFiltro(string strNome)
        {
            List<UsersTO> lstUsers;
            UsersTO objUser = GetUserSession();

            switch (objUser.Nivel)
            {
                case (int)Nivel.a:
                    lstUsers = UsersDAL.GetIndicesByFiltro(strNome);
                    break;
                default:
                    lstUsers = UsersDAL.GetIndicesByFiltro(strNome, objUser);
                    break;
            }
            return lstUsers;
        }

        public static List<UsersTO> GetByRedeId(int intRedeId, string strConnection)
        {
            return UsersDAL.GetByRedeId(intRedeId, strConnection);
        }

        public static List<Usuarios_TiposTO> GetTiposAll()
        {
            List<Usuarios_TiposTO> lstUsers = UsersDAL.GetTiposAll();

            switch (GetUserSession().Nivel)
            {
                case (int)Nivel.r:
                    lstUsers.Remove(lstUsers.Find(u => u.id.Equals(1)));
                    lstUsers.Remove(lstUsers.Find(u => u.id.Equals(4)));
                    break;
                case (int)Nivel.l:
                    lstUsers.Remove(lstUsers.Find(u => u.id.Equals(1)));
                    lstUsers.Remove(lstUsers.Find(u => u.id.Equals(3)));
                    lstUsers.Remove(lstUsers.Find(u => u.id.Equals(4)));
                    break;
            }

            return lstUsers;
        }

        private static List<UsersTO> GetAllMinion(UsersTO owner)
        {
            return UsersDAL.GetAllMinion(owner);
        }
        #endregion

        #region .: Persistence :.

        public static void UpdateActivity(UsersTO clsUser)
        {
            clsUser.LastActivityDate = Convert.ToDateTime(DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + " 00:00:00");
            UsersDAL.UpdateActivity(clsUser);
        }

        public static Boolean Delete(UsersTO clsUsers, string strConnection)
        {
            return UsersDAL.Delete(clsUsers, strConnection);
        }

        #endregion

        #region .: Enum :.
        internal enum Nivel
        {
            a = 0,
            r = 1,
            l = 2,
            i = 4
        }
        #endregion

    }
}
