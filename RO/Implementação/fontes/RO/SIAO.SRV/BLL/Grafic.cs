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

        public static List<GraficTO> GraficList(int intMes, UsersTO clsUsers, string strConnection)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficMes(intMes, clsUsers, strConnection);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll(strConnection);

            decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

            clsGrafic.ForEach(delegate(GraficTO _Grafic) {
                clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic) {
                    if (_Grafic.Sub_Consultoria == _IndicesGrafic.categoria && _Grafic.Grupo == _IndicesGrafic.grupo) {
                        clsList.Add(new GraficTO() { 
                            Sub_Consultoria = _Grafic.Sub_Consultoria,
                            Razao_Social = _Grafic.Razao_Social,
                            Mes = _Grafic.Mes,
                            Liquido = ((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda),
                            Grupo = _Grafic.Grupo,
                            Desconto = (_Grafic.Desconto / _IndicesGrafic.desconto)
                        });
                    }
                });
                //if (_Grafic.Grupo == "zzzzzz") { clsList.Add(_Grafic); }  
            });

            return clsList;
        }
        
        #endregion
        
        #region .: Search :.

        public static List<GraficTO> GetGraficMes(int intMes, UsersTO clsUsers, string strConnection)
        {
            return GraficDAL.GetGraficMes(intMes, clsUsers, strConnection);
        }

        public static TotaisGraficMesTO GetTotalMes(int intMes, string strConnection)
        {
            return GraficDAL.GetTotalMes(intMes, strConnection);
        }

        public static List<IndicesGraficTO> GetIndicesAll(string strConnection)
        {
            return GraficDAL.GetIndicesALL(strConnection);
        }

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsertIndice(IndicesGraficTO clsIndiceGrafic, string strConnection) {
            return GraficDAL.InsetIndices(clsIndiceGrafic, strConnection);
        }

        public static Boolean UpdateIndiceGrafic(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
            return GraficDAL.UpdateIndices(clsIndiceGrafic, strConnection);
        }

        public static Boolean DeleteIndiceGrafic(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
            return GraficDAL.DeleteIndice(clsIndiceGrafic, strConnection);
        }

        #endregion

        public static object GetIndicesById(int intId, string strConnection)
        {
            throw new NotImplementedException();
        }
    }
}
