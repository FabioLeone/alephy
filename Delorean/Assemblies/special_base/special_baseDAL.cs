using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblies
{
    class special_baseDAL
    {
        #region .:Searches:.
        internal static List<base_viewer> getByFarmaciaId(int iFarmaciaId)
        {
            NpgsqlConnection nc = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["CONNECTION_STRING"].ConnectionString);
            List<base_viewer> lst = new List<base_viewer>();
            StringBuilder sb = new StringBuilder(@"select bc.id, b.barras, p.nomeprod, bc.valor_custo, (bc.valor_custo / (1 - f.margem_esperada)) as ""1 Unidade"",
	            ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)) as ""Acima de X"", 
	            ((((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto)) - bc.valor_custo) / ((bc.valor_custo / (1 - f.margem_esperada)) * (1 - f.desconto))) as ""Margem Real""
            from base_especial b
	            left join base_compra bc on b.barras = bc.barras
	            left join produtos_base p on b.barras = p.codbarra
	            left join fator_venda f on b.barras = f.barras
            where bc.farmaciaid = @farmaciaid and to_char(bc.data, 'MM yyyy') = to_char((
	            select data from base_compra where farmaciaid = bc.farmaciaid order by data desc limit 1), 'MM yyyy')");
            
            NpgsqlCommand ncmm = nc.CreateCommand();
            ncmm.CommandText = sb.ToString();
            ncmm.Parameters.Add("@farmaciaid", NpgsqlDbType.Integer).Value = iFarmaciaId;

            try
            {
                nc.Open();

                using (IDataReader drd = ncmm.ExecuteReader())
                {
                    while (drd.Read())
                    {
                        lst.Add(Load(drd));
                    }
                }
            }
            finally
            {
                nc.Close();
            }

            return lst;
        }

        #endregion

        #region .:Loads:.
        private static base_viewer Load(IDataReader drd)
        {
            base_viewer o = new base_viewer();

            if (!drd.IsDBNull(drd.GetOrdinal("id"))) o.bcid = drd.GetInt32(drd.GetOrdinal("id")); else o.bcid = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("barras"))) o.barras = drd.GetString(drd.GetOrdinal("barras")); else o.barras = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("nomeprod"))) o.nomeprod = drd.GetString(drd.GetOrdinal("nomeprod")); else o.nomeprod = string.Empty;
            if (!drd.IsDBNull(drd.GetOrdinal("valor_custo"))) o.valor_custo = drd.GetDecimal(drd.GetOrdinal("valor_custo")); else o.valor_custo = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("1 Unidade"))) o.one_unit = drd.GetDecimal(drd.GetOrdinal("1 Unidade")); else o.one_unit = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("Acima de X"))) o.upx = drd.GetDecimal(drd.GetOrdinal("Acima de X")); else o.upx = 0;
            if (!drd.IsDBNull(drd.GetOrdinal("Margem Real"))) o.actual_margin = drd.GetDecimal(drd.GetOrdinal("Margem Real")); else o.actual_margin = 0;

            return o;
        }
        #endregion
    }
}
