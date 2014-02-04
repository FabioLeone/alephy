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
        public static List<base_viewer> getProducts()
        {
            List<base_viewer> lst;
            lst = helpers.GetFromCache<List<base_viewer>>("products" + helpers.GetSession().UserId);
            
            if (lst == null || lst.Count == 0)
            {
                lst = special_baseDAL.getByFarmaciaId(helpers.GetSession().FarmaciaId);
                helpers.SetCache("products" + helpers.GetSession().UserId, lst);
            }

            return lst;
        }

        public static List<base_viewer> getByFilter(string p1, string p2)
        {
            List<base_viewer> lst;
            lst = helpers.GetFromCache<List<base_viewer>>("products" + helpers.GetSession().UserId);

            if (lst == null)
            {
                lst = special_baseDAL.getByFilter(helpers.GetSession().FarmaciaId, p1, p2);
                helpers.SetCache("products" + helpers.GetSession().UserId, lst);
            }

            return lst;
        }
        #endregion
    }
}
