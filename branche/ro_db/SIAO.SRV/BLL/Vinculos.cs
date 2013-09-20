using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.DAL;
using SIAO.SRV.TO;

namespace SIAO.SRV.BLL
{
    public class VinculoBLL
    {
        #region .:Search:.
        public static List<VinculoTO> GetByTipoIdAndSearch(int intTipoId, string strSearch)
        {
            List<VinculoTO> lstVinculos = new List<VinculoTO>();
            UsersTO oUser = UsersBLL.GetUserSession();

            if (oUser.Nivel.Equals((int)UsersBLL.Nivel.a))
                lstVinculos = VinculoDAL.GetByTipoIdAndSearch(intTipoId, strSearch);
            else
                lstVinculos = VinculoDAL.GetByTipoIdAndSearch(intTipoId, strSearch, oUser);
                
            lstVinculos.ForEach(delegate(VinculoTO v) {
                if (v.redeid > 0) v.LinkId = v.redeid;
                else if (v.farmaciaid > 0) v.LinkId = v.farmaciaid;
                else v.LinkId = v.industriaid;
            });

            return lstVinculos;
        }

        public static List<VinculoTO> GetByUsuarioId(int intUsuarioId)
        {
            return VinculoDAL.GetByUsuarioId(intUsuarioId);
        }

        public static VinculoTO GetByCNPJ(string strCNPJ, int intTipoId)
        {
            return VinculoDAL.GetByCNPJ(strCNPJ, intTipoId);
        }
        #endregion

        #region .:Persistence:.
        public static void Insert(VinculoTO clsVinculo)
        {
            VinculoDAL.Insert(clsVinculo);
        }

        public static void Update(VinculoTO clsVinculo)
        {
            VinculoDAL.Update(clsVinculo);
        }
        public static void Delete(VinculoTO clsVinculo)
        {
            VinculoDAL.Delete(clsVinculo);
        }
        #endregion

        #region .:Methods:.

        public static string SetVinculo(List<VinculoTO> lstVinculos, System.Web.UI.WebControls.HiddenField hfLinkId, System.Web.UI.WebControls.Literal litID, System.Web.UI.WebControls.TextBox txtCNPJ, System.Web.UI.WebControls.HiddenField hfUsuarioTipoId)
        {
            VinculoTO clsVinculo = new VinculoTO();

            clsVinculo = lstVinculos.Find(v => v.LinkId == Convert.ToInt32(hfLinkId.Value) && v.UsuarioId == Convert.ToInt32(litID.Text));

            if (String.IsNullOrEmpty(clsVinculo.CNPJ))
            {
                clsVinculo.CNPJ = txtCNPJ.Text;

                switch (Convert.ToInt32(hfUsuarioTipoId.Value))
                {
                    case 1:
                        {
                            switch (clsVinculo.nivel)
                            {
                                case 1:
                                    {
                                        Rede clsRede = clsControl.GetRedeByCNPJ(txtCNPJ.Text);
                                        if (clsRede.RedeId == 0)
                                        {
                                            txtCNPJ.Text = String.Empty;
                                            txtCNPJ.Focus();
                                            return "CNPJ não corresponde a uma Rede ou não cadastrado";
                                        }

                                        clsVinculo.Empresa = clsRede.RedeName;
                                        clsVinculo.redeid = clsRede.RedeId;
                                        break;
                                    }
                                case 2:
                                    {
                                        Loja clsLoja = clsControl.GetLojaByCNPJ(txtCNPJ.Text);
                                        if (clsLoja.Id == 0)
                                        {
                                            txtCNPJ.Text = String.Empty;
                                            txtCNPJ.Focus();
                                            return "CNPJ não corresponde a uma Drogaria ou não cadastrado";
                                        }

                                        clsVinculo.Empresa = clsLoja.NomeFantasia;
                                        clsVinculo.farmaciaid = clsLoja.Id;
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2:
                        {
                            Loja clsLoja = clsControl.GetLojaByCNPJ(txtCNPJ.Text);
                            if (clsLoja.Id == 0)
                            {
                                txtCNPJ.Text = String.Empty;
                                txtCNPJ.Focus();
                                return "CNPJ não corresponde a uma Drogaria ou não cadastrado";
                            }

                            clsVinculo.Empresa = clsLoja.NomeFantasia;
                            clsVinculo.farmaciaid = clsLoja.Id;
                            break;
                        }
                    case 3:
                        {
                            Rede clsRede = clsControl.GetRedeByCNPJ(txtCNPJ.Text);
                            if (clsRede.RedeId == 0)
                            {
                                txtCNPJ.Text = String.Empty;
                                txtCNPJ.Focus();
                                return "CNPJ não corresponde a uma Rede ou não cadastrado";
                            }

                            clsVinculo.Empresa = clsRede.RedeName;
                            clsVinculo.redeid = clsRede.RedeId;
                            break;
                        }
                }

                if (clsVinculo.id > 0)
                    VinculoBLL.Update(clsVinculo);
                else
                    VinculoBLL.Insert(clsVinculo);
            }
            return String.Empty;
        }

        public static bool ValidaVinculo(System.Web.UI.WebControls.TextBox txtCNPJ, System.Web.UI.WebControls.HiddenField hfUsuarioTipoId)
        {
            if (VinculoBLL.GetByCNPJ(txtCNPJ.Text, Convert.ToInt32(hfUsuarioTipoId.Value)).id > 0)
            {
                txtCNPJ.Focus();
                return false;
            }
            else
                return true;
        }
        #endregion

    }
}
