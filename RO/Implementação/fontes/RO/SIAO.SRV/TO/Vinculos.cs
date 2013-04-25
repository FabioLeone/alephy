using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV
{
    [Serializable]
    public class VinculoTO
    {
        public int id { get; set; }
        public int UsuarioId { get; set; }
        public int LinkId { get; set; }
        public String UserName { get; set; }
        public String Empresa { get; set; }
        public String CNPJ { get; set; }
        public int TipoId { get; set; }
    }
}
