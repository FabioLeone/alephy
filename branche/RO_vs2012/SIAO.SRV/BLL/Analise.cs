using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class AnaliseBLL
    {
        public static List<AnaliseTO> GetAnaliseAll() {
            return AnaliseDAL.GetAnaliseAll();
        }

        public static List<AnaliseItemTO> GetItensAll() {
            return AnaliseDAL.GetItemAll();
        }
    }
}
