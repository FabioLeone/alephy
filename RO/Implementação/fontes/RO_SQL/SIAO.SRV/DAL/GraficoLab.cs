using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIAO.SRV.TO;
using System.Data.Common;
using System.Data.SqlClient;

namespace SIAO.SRV.DAL
{
    public class GraficoLabDAL
    {
        private static GraficoLabTO Load(IDataReader drdGrafic)
        {
            GraficoLabTO clsGraficoLab = new GraficoLabTO();

            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Id"))) { clsGraficoLab.Id = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Id")); } else { clsGraficoLab.Id= 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Descricao"))) { clsGraficoLab.Descricao = drdGrafic.GetString(drdGrafic.GetOrdinal("Descricao")); } else { clsGraficoLab.Descricao = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Ano"))) { clsGraficoLab.Ano = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Ano")); } else { clsGraficoLab.Ano = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Mes"))) { clsGraficoLab.Mes = drdGrafic.GetInt32(drdGrafic.GetOrdinal("Mes")); } else { clsGraficoLab.Mes = 0; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("NomeLab"))) { clsGraficoLab.NomeLab = drdGrafic.GetString(drdGrafic.GetOrdinal("NomeLab")); } else { clsGraficoLab.NomeLab = string.Empty; }
            if (!drdGrafic.IsDBNull(drdGrafic.GetOrdinal("Valor_Liquido"))) { clsGraficoLab.Valor_Liquido = drdGrafic.GetDecimal(drdGrafic.GetOrdinal("Valor_Liquido")); } else { clsGraficoLab.Valor_Liquido = 0; }

            return clsGraficoLab;
        }

        public static List<GraficoLabTO> GetByFilter(string strConnection, int intId, int intAno, List<string> lstGrupos, List<string> lstSubGrupos) {
            List<GraficoLabTO> clsGraficoLab = new List<GraficoLabTO>();
            SqlConnection msc = new SqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(" select redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,base_clientes.Ano,base_clientes.Mes,");
                strSQL.Append(" produtos_base.NomeLab,sum(base_clientes.Valor_Liquido) AS Valor_Liquido ");
                strSQL.Append(" from (((base_clientes join farmacias on((base_clientes.Cnpj = farmacias.Cnpj))) ");
                strSQL.Append(" join redesfarmaceuticas on((farmacias.idRede = redesfarmaceuticas.Id))) ");
                strSQL.Append(" join produtos_base on((produtos_base.CodBarra = base_clientes.Barras))) ");
                strSQL.Append(" where (base_clientes.Ano = "+ intAno +") ");

                if (intId > 0) { strSQL.Append(" and (redesfarmaceuticas.Id = "+ intId +")"); }
                if (lstGrupos.Count > 0) {
                    string strGrupo = string.Empty;
                    int i = 0;
                    lstGrupos.ForEach(delegate(string _grupo) {
                        if (i == 0) { strGrupo = " '" + _grupo + "'"; i++; } else { strGrupo += ", '" + _grupo + "'"; i++; }    
                    });
                    strSQL.Append(" and (produtos_base.Grupo in ("+ strGrupo +"))");
                }
                if (lstSubGrupos.Count > 0) {
                    string strSubGrupo = string.Empty;
                    int i = 0;
                    lstSubGrupos.ForEach(delegate(string _subGrupo) {
                        if (i == 0) { strSubGrupo = " '" + _subGrupo + "'"; i++; } else { strSubGrupo += ", '" + _subGrupo + "'"; i++; }
                    });
                    strSQL.Append(" and (produtos_base.Sub_Consultoria in (" + strSubGrupo + "))");
                }
                strSQL.Append(" group by redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,base_clientes.Ano,base_clientes.Mes,produtos_base.NomeLab");

                DbCommand cmdGraficoLab = msc.CreateCommand();
                cmdGraficoLab.CommandText = strSQL.ToString();
                msc.Open();
                using (IDataReader drdGraficoLab = cmdGraficoLab.ExecuteReader())
                {
                    while (drdGraficoLab.Read())
                    {
                        clsGraficoLab.Add(Load(drdGraficoLab));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGraficoLab;
        }

        internal static List<GraficoLabTO> GetListaTotaisMesByFilter(string strConnection, int intId, int intAno, List<string> lstGrupos, List<string> lstSubGrupos)
        {
            List<GraficoLabTO> clsGraficoLab = new List<GraficoLabTO>();
            SqlConnection msc = new SqlConnection(strConnection);

            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append(" select redesfarmaceuticas.Id,'TotalMes' as Descricao,base_clientes.Ano,base_clientes.Mes,");
                strSQL.Append(" '' as NomeLab,sum(base_clientes.Valor_Liquido) AS Valor_Liquido ");
                strSQL.Append(" from (((base_clientes join farmacias on((base_clientes.Cnpj = farmacias.Cnpj))) ");
                strSQL.Append(" join redesfarmaceuticas on((farmacias.idRede = redesfarmaceuticas.Id))) ");
                strSQL.Append(" join produtos_base on((produtos_base.CodBarra = base_clientes.Barras))) ");
                strSQL.Append(" where (base_clientes.Ano = " + intAno + ") ");

                if (intId > 0) { strSQL.Append(" and (redesfarmaceuticas.Id = " + intId + ")"); }
                if (lstGrupos.Count > 0)
                {
                    string strGrupo = string.Empty;
                    int i = 0;
                    lstGrupos.ForEach(delegate(string _grupo)
                    {
                        if (i == 0) { strGrupo = " '" + _grupo + "'"; i++; } else { strGrupo += ", '" + _grupo + "'"; i++; }
                    });
                    strSQL.Append(" and (produtos_base.Grupo in (" + strGrupo + "))");
                }
                if (lstSubGrupos.Count > 0)
                {
                    string strSubGrupo = string.Empty;
                    int i = 0;
                    lstSubGrupos.ForEach(delegate(string _subGrupo)
                    {
                        if (i == 0) { strSubGrupo = " '" + _subGrupo + "'"; i++; } else { strSubGrupo += ", '" + _subGrupo + "'"; i++; }
                    });
                    strSQL.Append(" and (produtos_base.Sub_Consultoria in (" + strSubGrupo + "))");
                }
                strSQL.Append(" group by redesfarmaceuticas.Id,redesfarmaceuticas.Descricao,base_clientes.Ano,base_clientes.Mes");

                DbCommand cmdGraficoLab = msc.CreateCommand();
                cmdGraficoLab.CommandText = strSQL.ToString();
                msc.Open();
                using (IDataReader drdGraficoLab = cmdGraficoLab.ExecuteReader())
                {
                    while (drdGraficoLab.Read())
                    {
                        clsGraficoLab.Add(Load(drdGraficoLab));
                    }
                }
            }
            finally
            {
                msc.Close();
            }

            return clsGraficoLab;
        }
    }
}
