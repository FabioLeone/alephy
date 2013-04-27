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

        public static List<GraficTO> GraficList(int intMes, UsersTO clsUsers, string strConnection, string strLoja, int intAno)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficMes(intMes, clsUsers, strConnection, strLoja,intAno);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll(strConnection);

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new GraficTO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Liquido = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Grupo = _Grafic.Grupo,
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2)
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        public static List<GraficTO> GraficListByAno(int intAno, UsersTO clsUsers, string strConnection, string strLoja)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficAno(intAno, clsUsers, strConnection, strLoja);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll(strConnection);

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].Liquido;

                clsGrafic.ForEach(delegate(GraficTO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new GraficTO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Liquido = Decimal.Round(((_Grafic.Liquido / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Grupo = _Grafic.Grupo,
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2)
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        #endregion

        #region .: Search :.

        public static List<GraficTO> GetGraficMes(int intMes, UsersTO clsUsers, string strConnection, string strLoja, int intAno)
        {
            return GraficDAL.GetGraficMes(intMes, clsUsers, strConnection, strLoja, intAno);
        }

        public static TotaisGraficMesTO GetTotalMes(int intMes, string strConnection)
        {
            return GraficDAL.GetTotalMes(intMes, strConnection);
        }

        public static List<IndicesGraficTO> GetIndicesAll(string strConnection)
        {
            return GraficDAL.GetIndicesALL(strConnection);
        }
        public static IndicesGraficTO GetIndicesById(int intId, string strConnection)
        {
            return GraficDAL.GetIndicesById(intId, strConnection);
        }
        public static List<IndicesGraficTO> GetIndicesByFiltro(string strGrupo, string strSub_Categoria, string strConnection)
        {
            return GraficDAL.GetIndicesByFiltro(strGrupo, strSub_Categoria, strConnection);
        }

        public static List<string> GetCategorias(string strConnection)
        {
            return GraficDAL.GetCategorias(strConnection);
        }

        public static List<string> GetGrupos(string strConnection)
        {
            return GraficDAL.GetGrupos(strConnection);
        }

        #endregion

        #region .: Persistence :.

        public static IndicesGraficTO InsertIndice(IndicesGraficTO clsIndiceGrafic, string strConnection)
        {
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

    }
}
