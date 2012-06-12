using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using SIAO.SRV.DAL;

namespace SIAO.SRV.BLL
{
    public class GraficBLL
    {
        #region .: Method :.

        public static List<GraficTO> GraficList(int intMes)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficMes(intMes);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            clsGrafic.ForEach(delegate(GraficTO _Grafic) { 
            
            });

            decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

            clsGrafic.ForEach(delegate(GraficTO _Grafic) {
                clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic) {
                    if (_Grafic.Sub_Consultoria == _IndicesGrafic.categoria && _Grafic.Grupo == _IndicesGrafic.grupo) {
                        clsList.Add(new GraficTO() { 
                            Sub_Consultoria = _Grafic.Sub_Consultoria,
                            Razao_Social = _Grafic.Razao_Social,
                            Mes = _Grafic.Mes,
                            Liquido = ((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100,
                            Grupo = _Grafic.Grupo,
                            Desconto = (_Grafic.Desconto / _IndicesGrafic.desconto)
                        });
                    }
                });
                if (_Grafic.Grupo == "zzzzzz") { clsList.Add(_Grafic); }  
            });

            return clsList;
        }
        
        #endregion
        
        #region .: Search :.

        public static List<GraficTO> GetGraficMes(int intMes) {
            return GraficDAL.GetGraficMes(intMes);
        }

        public static TotaisGraficMesTO GetTotalMes(int intMes) {
            return GraficDAL.GetTotalMes(intMes);
        }

        public static List<IndicesGraficTO> GetIndicesAll() {
            return GraficDAL.GetIndicesALL();
        }

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsertIndice(IndicesGraficTO clsIndiceGrafic) {
            return GraficDAL.InsetIndices(clsIndiceGrafic);
        }

        public static Boolean UpdateIndiceGrafic(IndicesGraficTO clsIndiceGrafic) {
            return GraficDAL.UpdateIndices(clsIndiceGrafic);
        }

        public static Boolean DeleteIndiceGrafic(IndicesGraficTO clsIndiceGrafic) {
            return GraficDAL.DeleteIndice(clsIndiceGrafic);
        }

        #endregion
    }
}
