using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    public class competitors_baseBLL
    {
        public static List<competitors_baseTO> getAll()
        {
            return competitors_baseDAL.getAll();
        }
    }
}
