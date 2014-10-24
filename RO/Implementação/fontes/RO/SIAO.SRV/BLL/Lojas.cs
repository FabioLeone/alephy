using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIAO.SRV.BLL;
using System.Web.UI.WebControls;

namespace SIAO.SRV
{
    public class LojasBLL
    {

        public static void getLojas(TO.UsersTO ouser, System.Web.UI.WebControls.DropDownList ddlLojaRelatorios, System.Web.UI.HtmlControls.HtmlGenericControl dvFiltro, System.Web.UI.HtmlControls.HtmlGenericControl ulArq)
        {
            DataSet ds = new DataSet();

            switch (ouser.TipoId)
            {
                case 1:
                    switch (ouser.Nivel)
                    {
                        case (int)UsersBLL.Nivel.r:
                            getLojas(ddlLojaRelatorios, clsControl.GetRedeByUserId(ouser.UserId).RedeId);
                            break;
                        case (int)UsersBLL.Nivel.l:
                            {
                                ds = clsControl.GetLojaByUserId(ouser.UserId);

                                if (ds.Tables[0].Rows.Count > 1)
                                {
                                    ddlLojaRelatorios.DataSource = ds;
                                    ddlLojaRelatorios.DataTextField = "NomeFantasia";
                                    ddlLojaRelatorios.DataValueField = "id";
                                    ddlLojaRelatorios.DataBind();
                                    ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
                                    ddlLojaRelatorios.SelectedIndex = 0;
                                }
                                else
                                {
                                    dvFiltro.Visible = false;
                                    getFiles(ds.Tables[0].Rows[0]["id"], ulArq);
                                }

                                break;
                            }
                        default:
                            break;
                    }
                    break;
                case 2:
                    ds = clsControl.GetLojaByUserId(ouser.UserId);

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlLojaRelatorios.DataSource = ds;
                        ddlLojaRelatorios.DataTextField = "NomeFantasia";
                        ddlLojaRelatorios.DataValueField = "id";
                        ddlLojaRelatorios.DataBind();
                        ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
                        ddlLojaRelatorios.SelectedIndex = 0;
                    }
                    else
                    {
                        dvFiltro.Visible = false;
                        getFiles(ds.Tables[0].Rows[0]["id"], ulArq);
                    }

                    break;
                case 3:
                    getLojas(ddlLojaRelatorios, clsControl.GetRedeByUserId(ouser.UserId).RedeId);
                    break;
            }
        }

        public static void getLojasApp(TO.UsersTO ouser, System.Web.UI.WebControls.DropDownList ddlLojaRelatorios, System.Web.UI.HtmlControls.HtmlGenericControl dvFiltro, System.Web.UI.HtmlControls.HtmlGenericControl dvLoja)
        {
            DataSet ds = new DataSet();

            switch (ouser.TipoId)
            {
                case 1:
                    switch (ouser.Nivel)
                    {
                        case (int)UsersBLL.Nivel.r:
                            getLojasApp(ddlLojaRelatorios, clsControl.GetRedeByUserId(ouser.UserId).RedeId);
                            break;
                        case (int)UsersBLL.Nivel.l:
                            {
                                ddlLojaRelatorios.DataSource = clsControl.GetLojaByUserId(ouser.UserId);
                                ddlLojaRelatorios.DataTextField = "NomeFantasia";
                                ddlLojaRelatorios.DataValueField = "Cnpj";
                                ddlLojaRelatorios.DataBind();
                                ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
                                ddlLojaRelatorios.SelectedIndex = 0;
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                case 2:
                    ds = clsControl.GetLojaByUserId(ouser.UserId);

                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        ddlLojaRelatorios.DataSource = ds;
                        ddlLojaRelatorios.DataTextField = "NomeFantasia";
                        ddlLojaRelatorios.DataValueField = "Cnpj";
                        ddlLojaRelatorios.DataBind();
                        ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
                        ddlLojaRelatorios.SelectedIndex = 0;
                        
                        dvLoja.Visible = true;
                    }
                    else
                        dvLoja.Visible = false;

                    break;
                case 3:
                    getLojasApp(ddlLojaRelatorios, clsControl.GetRedeByUserId(ouser.UserId).RedeId);
                    break;
            }
        }

        public static void getFiles(object o, System.Web.UI.HtmlControls.HtmlGenericControl ulArq)
        {
            int intId = 0;
            if (int.TryParse(o.ToString(), out intId))
                ulArq.InnerHtml = FilesBLL.GetFiles("Analise", intId, true);
            else
                ulArq.InnerHtml = "Não há itens a serem listados.";
        }
        
        public static void getLojas(System.Web.UI.WebControls.DropDownList ddlLojaRelatorios, int intRedeId)
        {
            ddlLojaRelatorios.DataSource = clsControl.GetLojaByRedeId(intRedeId);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "id";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        public static void getLojasApp(System.Web.UI.WebControls.DropDownList ddlLojaRelatorios, int intRedeId)
        {
            ddlLojaRelatorios.DataSource = clsControl.GetLojaByRedeId(intRedeId);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        public static void getLojasApp(DropDownList ddlLojaRelatorios, int intRedeId, string city)
        {
            ddlLojaRelatorios.DataSource = clsControl.GetLojaByRedeIdAndCity(intRedeId, city);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        public static void getLojasApp(DropDownList ddlLojaRelatorios, int intRedeId, int ufId)
        {
            ddlLojaRelatorios.DataSource = clsControl.GetLojaByRedeIdAndUf(intRedeId, ufId);
            ddlLojaRelatorios.DataTextField = "NomeFantasia";
            ddlLojaRelatorios.DataValueField = "Cnpj";
            ddlLojaRelatorios.DataBind();
            ddlLojaRelatorios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todas", string.Empty));
            ddlLojaRelatorios.SelectedIndex = 0;
        }

        internal static void LoadLojas(TO.UsersTO oUser, System.Web.UI.WebControls.DropDownList ddlLoja)
        {
            ddlLoja.DataSource = clsControl.GetLojaByUserId(oUser.UserId);
            ddlLoja.DataTextField = "NomeFantasia";
            ddlLoja.DataValueField = "Id";
            ddlLoja.DataBind();
            ddlLoja.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlLoja.SelectedIndex = 0;
        }
    }
}
