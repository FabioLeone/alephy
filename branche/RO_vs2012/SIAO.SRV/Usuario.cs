using System;
using System.Collections.Generic;

namespace SIAO.SRV
{
    public class Usuario
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string AcsName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Inactive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Access { get; set; }
        public int FarmaciaId { get; set; }
        public List<string> Cnpj { get; set; }
    }
}
