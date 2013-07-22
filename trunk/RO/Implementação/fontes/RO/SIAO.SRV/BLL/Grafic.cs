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

        public static List<GraficTO> GraficList(string strIni, UsersTO clsUsers, string strLoja, string strFim)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficMes(strIni, clsUsers, strLoja,strFim);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

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
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2),
                                Periodo = String.Format("{0} à {1}", strIni, strFim),
                                Nome_Fantasia = _Grafic.Nome_Fantasia
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        public static List<GraficTO> GraficList(string strIni, UsersTO clsUser, string strFim, int idRede)
        {
            List<GraficTO> clsList = new List<GraficTO>();
            List<GraficTO> clsGrafic = GraficDAL.GetGraficMes(strIni, clsUser, strFim, idRede);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

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
                                Desconto = Decimal.Round((_Grafic.Desconto / _IndicesGrafic.desconto) * 100, 2),
                                Periodo = String.Format("{0} à {1}",strIni,strFim),
                                Nome_Fantasia = _Grafic.Nome_Fantasia
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        public static List<Grafic2TO> GraficListByAno(string strIni, string strFim, UsersTO clsUsers, string strLoja)
        {
            List<Grafic2TO> clsList = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic = GraficDAL.GetGraficAno(strIni, strFim, clsUsers, strLoja);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].quantidade;

                clsGrafic.ForEach(delegate(Grafic2TO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new Grafic2TO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Liquido = Decimal.Round(((_Grafic.quantidade / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Ano = _Grafic.Ano,
                                CNPJ = _Grafic.CNPJ,
                                Nome_Fantasia = _Grafic.Nome_Fantasia,
                                Periodo = String.Format("{0} à {1}", strIni, strFim)
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        public static List<Grafic2TO> GraficListByAno(string strIni, string strFim, UsersTO clsUsers, int intRedeId)
        {
            List<Grafic2TO> clsList = new List<Grafic2TO>();
            List<Grafic2TO> clsGrafic = GraficDAL.GetGraficAno(strIni, strFim, clsUsers, intRedeId);
            List<IndicesGraficTO> clsIndicesGrafic = GetIndicesAll();

            if (clsGrafic.Count > 0)
            {
                decimal dcmTotal = clsGrafic[clsGrafic.Count - 1].quantidade;

                clsGrafic.ForEach(delegate(Grafic2TO _Grafic)
                {
                    clsIndicesGrafic.ForEach(delegate(IndicesGraficTO _IndicesGrafic)
                    {
                        if (_Grafic.Sub_Consultoria.ToUpper() == _IndicesGrafic.categoria.ToUpper() && _Grafic.Grupo.ToUpper() == _IndicesGrafic.grupo.ToUpper())
                        {
                            clsList.Add(new Grafic2TO()
                            {
                                Sub_Consultoria = _Grafic.Sub_Consultoria,
                                Razao_Social = _Grafic.Razao_Social,
                                Mes = _Grafic.Mes,
                                Liquido = Decimal.Round(((_Grafic.quantidade / dcmTotal) / _IndicesGrafic.venda) * 100, 2),
                                Ano = _Grafic.Ano,
                                CNPJ = _Grafic.CNPJ,
                                Nome_Fantasia = _Grafic.Nome_Fantasia,
                                Periodo = String.Format("{0} à {1}", strIni, strFim)
                            });
                        }
                    });
                });
            }

            return clsList;
        }

        #endregion

        #region .: Search :.

        public static TotaisGraficMesTO GetTotalMes(int intMes, string strConnection)
        {
            return GraficDAL.GetTotalMes(intMes, strConnection);
        }

        public static List<IndicesGraficTO> GetIndicesAll()
        {
            return GraficDAL.GetIndicesALL();
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

        public static List<Grafic2TO> Grafic31ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            return GraficDAL.Grafic31ByPeriodoAndRedeId(strIni, strFim, clsUser, intRedeId);
        }

        public static List<Grafic2TO> Grafic32ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            return GraficDAL.Grafic32ByPeriodoAndRedeId(strIni, strFim, clsUser, intRedeId);
        }

        public static List<Grafic2TO> Grafic33ByPeriodoAndRedeId(string strIni, string strFim, UsersTO clsUser, int intRedeId)
        {
            return GraficDAL.Grafic33ByPeriodoAndRedeId(strIni, strFim, clsUser, intRedeId);
        }

        public static List<Grafic2TO> Grafic31ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            return GraficDAL.Grafic31ByPeriodo(strIni, strFim, clsUser, strLoja);
        }

        public static List<Grafic2TO> Grafic32ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            return GraficDAL.Grafic32ByPeriodo(strIni, strFim, clsUser, strLoja);
        }

        public static List<Grafic2TO> Grafic33ByPeriodo(string strIni, string strFim, UsersTO clsUser, string strLoja)
        {
            return GraficDAL.Grafic33ByPeriodo(strIni, strFim, clsUser, strLoja);
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
