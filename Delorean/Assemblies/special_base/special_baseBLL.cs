using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

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
