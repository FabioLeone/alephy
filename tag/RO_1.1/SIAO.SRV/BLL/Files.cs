using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class FilesBLL
    {
        public static List<FilesTO> GetByYear(int intAno, string strConnection) {
            return FilesDAL.GetByYear(intAno, strConnection);
        }

        public static List<FilesTO> GetByYearAndRedeId(int intAno, int intRedeId, string scn)
        {
            return FilesDAL.GetByYearAndRedeId(intAno, intRedeId, scn);
        }

        public static List<FilesTO> GetByCnpj(string strCnpj, string strAno, string scn)
        {
            List<FilesTO> clsFiles = FilesDAL.GetByCnpj(strCnpj, strAno, scn);
            List<FilesTO> clsAux = new List<FilesTO>();
            for (int i = 1; i < 13; i++)
            {
                if (clsFiles.Find(f => f.data.Month == i) == null)
                    clsAux.Add(new FilesTO() { mes = 0, id = i });
                else
                    clsAux.Add(new FilesTO() { mes = 1, id = i });
            }

            clsAux.OrderBy(f => f.id);

            return clsAux;
        }
    }
}
