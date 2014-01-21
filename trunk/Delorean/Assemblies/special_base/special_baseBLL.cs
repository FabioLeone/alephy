using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class special_baseBLL
    {
        #region .:Searches:.
        public static object getProducts()
        {
            return special_baseDAL.getByFarmaciaId(helpers.GetSession().FarmaciaId);
        }
        #endregion
    }
}
