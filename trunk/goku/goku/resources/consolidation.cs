using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goku.resources
{
    class consolidation
    {
        public consolidation(DataTable dt) {
            Boolean blnOk = false;
            DataTable auxDt = new DataTable();
            List<String> lst = new List<string>();

            auxDt = dt.DefaultView.ToTable(true, "cnpj", "ano", "mes");
            lst = auxDt.AsEnumerable().Select(r => r[0].ToString()).Distinct().ToList();

            dal.deleteDuplicate(auxDt);

            dal.dataTransfer(lst);

            if (lst.Count > 0)
            {
                lst.ForEach(delegate(String _cnpj)
                {
                    blnOk = dal.dataConsolidation(_cnpj);

                    if (blnOk)
                        blnOk = dal.updateClientProdut(_cnpj);
                });
            }

            if (blnOk)
            {
                dal.DeletData(lst);
            }
        }
    }
}
