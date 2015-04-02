using System;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;
using System.Web.Script.Serialization;
using System.Web;

namespace SIAO.SRV.BLL
{
	public class UsersBLL
	{
		public UsersBLL ()
		{
		}

		#region .: Custom Search :.
		public static bool CheckCssRede(UsersTO u){
			Rede r = clsControl.GetRedeByUserId(u.UserId);

			if (r.RedeId > 0)
				return r.RedeName.ToUpper().Contains("MULTIDROGAS");
			else{
				r = RedesBLL.GetByLojaId(u.FarmaciaId);
				if (r.RedeId > 0)
					return r.RedeName.ToUpper().Contains("MULTIDROGAS");
				else return false;
			}
		}

		public static UsersTO GetByNameAndPassword(string strName, string strPassword)
		{
			UsersTO u = UsersDAL.GetByNameAndPassword(strName, strPassword);
			u.RedeId = clsControl.GetRedeByUserId(u.UserId).RedeId;
			return u;
		}
		#endregion

		#region .: Métodos :.
		public static bool ValidaRelatorio(int intTId)
		{
			// Retorna true se o usuário for Administrador ou Drogaria.
			return (intTId.Equals(1) || intTId.Equals(2) || intTId.Equals(3));
		}

		public static bool ValidaEnvio(int intTId)
		{
			// Retorna true se o usuário for Administrador ou Drogaria.
			return (intTId.Equals(1) || intTId.Equals(2));
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
	}
}

