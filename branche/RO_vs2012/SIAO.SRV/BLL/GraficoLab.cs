using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class GraficoLabBLL
    {
        public static List<GraficoLabTO> GetByFilter(string strConnection, int intId, int intAno, List<string> lstGrupos, List<string> lstSubGrupos) {
            return GraficoLabDAL.GetByFilter(strConnection, intId, intAno, lstGrupos, lstSubGrupos);
        }

        public static List<GraficoLabTO> GetListaTotaisMesByFilter(string strConnection, int intId, int intAno, List<string> lstGrupos, List<string> lstSubGrupos)
        {
            List<GraficoLabTO> clsGraficoLab = GetByFilter(strConnection, intId, intAno, lstGrupos, lstSubGrupos);
            List<GraficoLabTO> clsTotaisGraficoLab = GraficoLabDAL.GetListaTotaisMesByFilter(strConnection, intId, intAno, lstGrupos, lstSubGrupos);
            List<GraficoLabTO> clsResult = new List<GraficoLabTO>();

            clsGraficoLab.ForEach(delegate(GraficoLabTO _GraficoLab) {
                clsTotaisGraficoLab.ForEach(delegate(GraficoLabTO _totalMes) {
                    if (_GraficoLab.Mes == _totalMes.Mes) {
                        decimal decResult = 0;
                        decResult = Decimal.Round((_GraficoLab.Valor_Liquido / _totalMes.Valor_Liquido)*100, 2);
                        clsResult.Add(new GraficoLabTO() { 
                            Ano = _GraficoLab.Ano,
                            Descricao = _GraficoLab.Descricao,
                            Id = _GraficoLab.Id,
                            Mes = _GraficoLab.Mes,
                            NomeLab = _GraficoLab.NomeLab,
                            Valor_Liquido = decResult
                        });
                    }
                });
            });

            return clsResult;
        }
    }
}
