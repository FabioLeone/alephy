using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class sales_factorBLL
    {
        #region .:Searches:.
        public static List<sales_factorTO> getAll()
        {
            return sales_factorDAL.getAll();
        }
        #endregion
    }
}
