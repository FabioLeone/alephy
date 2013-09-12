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

        public static bool ValidaAcesso(UsersTO users,ref System.Web.UI.HtmlControls.HtmlGenericControl dvRedes,ref System.Web.UI.HtmlControls.HtmlGenericControl dvLoja,ref System.Web.UI.HtmlControls.HtmlGenericControl dvFiltro)
        {
            switch (users.TipoId)
            {   
                case 1:
                    dvFiltro.Visible = true;
                    dvLoja.Visible = true;
                    dvRedes.Visible = true;
                    return false;
                case 2:
                    dvFiltro.Visible = true;
                    dvLoja.Visible = true;
                    dvRedes.Visible = false;
                    return true;
                default:
                    dvFiltro.Visible = true;
                    dvLoja.Visible = true;
                    dvRedes.Visible = false;
                    return false;
            }
        }

        public static void GetNiveis(ref System.Web.UI.WebControls.DropDownList ddlNivel)
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
            }
            else
                ddlNivel.Visible = false;
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
                    ddlAcess.Items.Insert(0, new ListItem("Drogaria", "2"));
                    ddlAcess.SelectedIndex = 0;
                    ddlAcess.Enabled = false;
                    break;
            }
            
        }
        #endregion
        
        #region .: Search :.

        public static List<UsersTO> GetAll(string strConnection) {
            return UsersDAL.GetAll(strConnection);
        }

        public static UsersTO GetById(int intUserId, string strConnection) {
            return UsersDAL.GetById(intUserId, strConnection);
        }

        #endregion

        #region .: Custom Search :.

        public static UsersTO GetByNameAndPassword(string strName, string strPassword, string strConnectionString)
        {
            return UsersDAL.GetByNameAndPassword(strName, strPassword, strConnectionString);
        }

        public static object GetByName(string strNome, string strConnection)
        {
            return UsersDAL.GetByName(strNome, strConnection);
        }

        public static List<UsersTO> GetByAccessType(string strAccess, string strConnection)
        {
            return UsersDAL.GetByAccessType(strAccess, strConnection);
        }

        public static object GetIndicesByFiltro(string strNome, string strConnection)
        {
            return UsersDAL.GetIndicesByFiltro(strNome, strConnection);
        }

        public static List<UsersTO> GetByRedeId(int intRedeId, string strConnection)
        {
            return UsersDAL.GetByRedeId(intRedeId, strConnection);
        }

        public static List<Usuarios_TiposTO> GetTiposAll()
        {
            return UsersDAL.GetTiposAll();
        }
        #endregion

        #region .: Persistence :.

        public static UsersTO Insert(UsersTO clsUsers, string strConnection) {
            return UsersDAL.Insert(clsUsers, strConnection);
        }

        public static Boolean Update(UsersTO clsUsers, string strConnection)
        {
            return UsersDAL.Update(clsUsers, strConnection);
        }

        public static Boolean Delete(UsersTO clsUsers, string strConnection)
        {
            return UsersDAL.Delete(clsUsers, strConnection);
        }

        #endregion

        #region .: Enum :.
        internal enum Nivel { 
            a = 0,
            r = 1,
            l = 2,
            i = 4
        }
        #endregion

    }
}
