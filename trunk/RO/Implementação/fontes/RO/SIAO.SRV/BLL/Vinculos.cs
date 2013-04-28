using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class VinculoBLL
    {
        #region .:Search:.
        public static List<VinculoTO> GetByTipoIdAndSearch(int intTipoId, string strSearch)
        {
            return VinculoDAL.GetByTipoIdAndSearch(intTipoId, strSearch);
        }

        public static List<VinculoTO> GetByUsuarioId(int intUsuarioId)
        {
            return VinculoDAL.GetByUsuarioId(intUsuarioId);
        }

        public static VinculoTO GetByCNPJ(string strCNPJ)
        {
            return VinculoDAL.GetByCNPJ(strCNPJ);
        }
        #endregion

        #region .:Persistence:.
        public static void Insert(VinculoTO clsVinculo)
        {
            VinculoDAL.Insert(clsVinculo);
        }

        public static void Update(VinculoTO clsVinculo)
        {
            VinculoDAL.Update(clsVinculo);
        }
        public static void Delete(VinculoTO clsVinculo)
        {
            VinculoDAL.Delete(clsVinculo);
        }
        #endregion

    }
}
