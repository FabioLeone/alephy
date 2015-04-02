using System;

namespace SIAO.SRV.TO
{
	[Serializable]
	public class GraficTO
	{
		public GraficTO ()
		{
		}

		public string Razao_Social { get; set; }
		public int Mes { get; set; }
		public int Ano { get; set; }
		public string Grupo { get; set; }
		public string Sub_Consultoria { get; set; }
		public decimal Liquido { get; set; }
		public decimal Desconto { get; set; }
		public string Nome_Fantasia { get; set; }
		public string Periodo { get; set; }
		public long quantidade { get; set; }

		public string Sub_ConsultoriaAP { get; set; }
	}

	[Serializable]
	public class Grafic2TO : GraficTO
	{
		public string CNPJ { get; set; }
	}

	[Serializable]
	public class TotaisGraficMesTO
	{
		public int Mes { get; set; }
		public decimal Liquido { get; set; }
		public decimal Desconto { get; set; }
	}

	[Serializable]
	public class IndicesGraficTO
	{
		public int id { get; set; }
		public string grupo { get; set; }
		public string categoria { get; set; }
		public decimal venda { get; set; }
		public decimal desconto { get; set; }
	}
}

