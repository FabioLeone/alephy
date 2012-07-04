using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;
using System.Web.UI.HtmlControls;

namespace SIAO.SRV.BLL
{
    public class UsersBLL
    {
        #region .: Métodos :.

        public static List<HtmlGenericControl> GetMenu(int intUserId, string strConnection)
        {
            List<HtmlGenericControl> lhgc = new List<HtmlGenericControl>();

            if (UsersDAL.GetById(intUserId, strConnection).Access == "adm") {
                HtmlGenericControl h1 = new HtmlGenericControl();
                h1.InnerHtml = "<h2>Cadastros</h2>";
                lhgc.Add(h1);

                HtmlGenericControl ul1 = new HtmlGenericControl();
                ul1.InnerHtml = "<ul>";
                lhgc.Add(ul1);

                HtmlGenericControl li1 = new HtmlGenericControl();
                li1.InnerHtml = "<li><a href='Users/Default.aspx'>Usuários</li>";
                lhgc.Add(li1);

                HtmlGenericControl li2 = new HtmlGenericControl();
                li2.InnerHtml = "<li><a href='#'>Redes</li>";
                lhgc.Add(li2);

                HtmlGenericControl li3 = new HtmlGenericControl();
                li3.InnerHtml = "<li><a href='#'>Lojas</li>";
                lhgc.Add(li3);

                HtmlGenericControl li4 = new HtmlGenericControl();
                li4.InnerHtml = "<li><a href='#'>Manutenção do Banco de Dados</li>";
                lhgc.Add(li4);

                HtmlGenericControl eul1 = new HtmlGenericControl();
                eul1.InnerHtml = "</ul>";
                lhgc.Add(eul1);

            } else { }

            return lhgc;
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

    }
}
