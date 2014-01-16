using Assemblies.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies.users
{
    public class usersBLL
    {
        #region .:Methods:.
        public static bool Authentication(string sName, string sPassword)
        {
            usersTO objUser = GetByNameAndPassword(sName, sPassword);

            if (objUser.UserId > 0)
            {
                helpers.SetSession(objUser);
                return true;
            }
            else return false;
        }
        #endregion

        #region .:Search:.
        private static usersTO GetByNameAndPassword(string sName, string sPassword)
        {
            return usersDAL.GetByNameAndPassword(sName, sPassword);
        }
        #endregion
    }
}
