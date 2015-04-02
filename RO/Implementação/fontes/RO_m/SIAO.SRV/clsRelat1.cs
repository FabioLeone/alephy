using System;

namespace SIAO.SRV
{
	public class clsRelat1
	{
		public clsRelat1 ()
		{
		}

		public string Razao { get; set; }
		public string Cnpj { get; set; }
		public string SubConsultoria { get; set; }
		public int Mes { get; set; }
		public int Ano { get; set; }
		public string Grupo { get; set; }
		public decimal SomaDeQuantidade { get; set; }
		public decimal SomaDeValorBruto { get; set; }
		public decimal SomaDeValorLiquido { get; set; }
		public decimal SomaDeValorDesconto { get; set; }
		public decimal PercentualDesconto { get; set; }
		public string NomeFantasia { get; set; }
		public string Importado { get; set; }
		public string Periodo { get; set; }
	}

	[Serializable]
	public class PercReport : clsRelat1 {
		public decimal Participacao { get; set; }
	}
}

