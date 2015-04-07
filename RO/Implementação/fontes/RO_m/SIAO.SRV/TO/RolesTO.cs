using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIAO.SRV
{
	[Serializable]
	public class RolesTO
	{
		public Int32 RoleId { get; set; }
		public Int32 UserId { get; set; }
		public Boolean Envio{ get; set; }
		public Boolean RelatoriosTodos { get; set; }
		public Boolean Add { get; set; }
	}

	[Serializable]
	public class RelatoriosTO
	{
		public int RelatorioId { get; set; }
		public int RelatorioTipoId { get; set; }
		public int Rede_Id { get; set; }
		public bool All_rpt { get; set; }
	}
}

