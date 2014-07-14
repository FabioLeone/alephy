using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

namespace SIAO.SRV
{
    public class RedesBLL
    {
        internal static void LoadRedes(System.Web.UI.WebControls.DropDownList ddlEdRedes)
        {
            DataSet ds = new DataSet();
            ds = clsControl.GetRedes();

            ddlEdRedes.DataSource = ds.Tables[0];
            ddlEdRedes.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlEdRedes.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlEdRedes.DataBind();
            ddlEdRedes.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlEdRedes.Items.Insert(1, new ListItem("Independentes", "0"));
            ddlEdRedes.SelectedIndex = 0;
        }

        internal static Rede GetByLojaId(int loja_id)
        {
            return clsControl.GetRedeByLojaId(loja_id);
        }
    }
}
