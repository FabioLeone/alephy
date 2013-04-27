using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV
{
    public class Loja
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Razao { get; set; }
        public string Proprietario { get; set; }
        public int ProprietarioId { get; set; }
        public string Gerente { get; set; }
        public int GerenteId { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public int EndNumero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Fone { get; set; }
        public string Fone2 { get; set; }
        public string Celular { get; set; }
        public string Site { get; set; }
        public string Skype { get; set; }
        public string Msn { get; set; }
        public string Rede { get; set; }
        public int idRede { get; set; }
        public bool Ativo { get; set; }
        public string CEP { get; set; }

    }
}
