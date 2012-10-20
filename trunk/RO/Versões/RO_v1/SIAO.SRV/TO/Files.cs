using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV.TO
{
    public class FilesTO
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public int UserId { get; set; }
        public String cnpj { get; set; }
        public String tipo { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public String NomeFantasia { get; set; }
    }
}
