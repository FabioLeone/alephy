using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Assemblies
{
    public class competitors_baseBLL
    {
        public static DataTable getAll()
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            List<competitors_baseTO> lst = competitors_baseDAL.getAll();

            if (lst.Count > 0)
            {
                dc = new DataColumn("Ean");
                dt.Columns.Add(dc);

                dc = new DataColumn("Produto");
                dt.Columns.Add(dc);

                lst.ForEach(delegate(competitors_baseTO item)
                {
                    if(!dt.Columns.Contains(item.rede))
                    {
                        dc = new DataColumn(item.rede);
                        dt.Columns.Add(dc);
                    }
                });

                if (dt.Columns.Count > 0) {
                    lst.Select(l => l.barras).Distinct().ToList().ForEach(delegate(string bar) {
                        dr = dt.NewRow();
                        dr["Ean"] = bar;
                        dr["Produto"] = lst.Find(m => m.barras == bar).nomeprod;
                        lst.FindAll(n => n.barras == bar).ForEach(delegate(competitors_baseTO item) {
                            dr[item.rede] = item.valor_preco;
                        });
                        dt.Rows.Add(dr);
                    });
                }
            }

            return dt;
        }
    }
}
