using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goku.resources
{
    class consolidation
    {
        public consolidation() {
            Boolean blnOk = false;

            dal.deleteDuplicate();
            List<CNPJ> lstCnpj = dal.dataTransfer();
            if (lstCnpj.Count > 0)
            {
                lstCnpj.ForEach(delegate(CNPJ _cnpj)
                {
                    if (_cnpj.Return)
                    {
                        blnOk = dal.dataConsolidation(_cnpj);

                        if (blnOk)
                            blnOk = dal.updateClientProdut(_cnpj);
                    }
                });
            }

            if (blnOk)
            {
                dal.DeletData(lstCnpj);
            }
        }
    }
}
