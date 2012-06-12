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
        
        public static List<HtmlGenericControl> GetMenu(int intUserId)
        {
            List<HtmlGenericControl> lhgc = new List<HtmlGenericControl>();

            if (UsersDAL.GetById(intUserId).Access == "adm") {
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

        public static List<UsersTO> GetAll() {
            return UsersDAL.GetAll();
        }

        public static UsersTO GetById(int intUserId) {
            return UsersDAL.GetById(intUserId);
        }

        #endregion

        #region .: Custom Search :.

        public static UsersTO GetByNameAndPassword(string strName, string strPassword) {
            return UsersDAL.GetByNameAndPassword(strName, strPassword);
        }

        public static object GetByName(string strNome)
        {
            return UsersDAL.GetByName(strNome);
        }

        #endregion

        #region .: Persistence :.

        public static UsersTO Insert(UsersTO clsUsers) {
            return UsersDAL.Insert(clsUsers);
        }

        public static Boolean Update(UsersTO clsUsers) {
            return UsersDAL.Update(clsUsers);
        }

        public static Boolean Delete(UsersTO clsUsers) {
            return UsersDAL.Delete(clsUsers);
        }

        #endregion

    }
}
