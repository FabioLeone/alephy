using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLibrary
{
    public class Stores
    {
        public static object getByNetworkId(object id)
        {
            int intId = 0;
            if (int.TryParse(id.ToString(), out intId))
            {
                object o = sDal.getByNetworkId(intId);
                if (!o.GetType().Name.Equals("string"))
                {
                    DataRow dr = ((DataSet)o).Tables[0].NewRow();

                    if (((DataSet)o).Tables[0].Columns.Count > 0)
                    {
                        for (int i = 0; i < ((DataSet)o).Tables[0].Columns.Count; i++) {
                            if (i == 0)
                                dr[i] = 0;
                            else if (i == 1)
                                dr[i] = "Todas";
                            else
                                dr[i] = DBNull.Value;
                        }

                        ((DataSet)o).Tables[0].Rows.InsertAt(dr,0);
                    }
                }

                return o;
            }
            else
                return new object();
        }
    }
}
