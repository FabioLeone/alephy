using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblies
{
    [Serializable]
    public class usersTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Boolean Inactive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Access { get; set; }
        public int TipoId { get; set; }
        public string Name { get; set; }
        public int FarmaciaId { get; set; }
        public int RedeId { get; set; }
        public List<string> Cnpj { get; set; }
        public string Tipo { get; set; }
        public int Nivel { get; set; }
        public int owner { get; set; }
    }
}
