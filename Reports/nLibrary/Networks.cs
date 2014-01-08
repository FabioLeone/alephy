using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nLibrary
{
    public class Networks
    {
        public static object getAll()
        {
            object o = nDal.getAll();

            if (!o.GetType().Name.Equals("string"))
            {
                DataRow dr = ((DataSet)o).Tables[0].NewRow();
                DataRow dr1 = ((DataSet)o).Tables[0].NewRow();

                if (((DataSet)o).Tables[0].Columns.Count > 0)
                {
                    for (int i = 0; i < ((DataSet)o).Tables[0].Columns.Count; i++) {
                        if (i == 0)
                            dr1[i] = 0;
                        else if (i == 1)
                            dr1[i] = "Independentes";
                        else
                            dr1[i] = DBNull.Value;

                        dr[i] = DBNull.Value;
                    }

                    ((DataSet)o).Tables[0].Rows.InsertAt(dr, 0);
                    ((DataSet)o).Tables[0].Rows.InsertAt(dr1,1);
                }
            }

            return o;
        }
    }
}
