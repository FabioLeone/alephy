using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIAO.SRV.TO;
using System.Web.UI.WebControls;

namespace SIAO.SRV
{
    public class RelatoriosBLL
    {
        public static List<clsRelat1> GetMod2(UsersTO clsUser, TextBox txtInicio, TextBox txtFim, DropDownList ddlRedesRelatorios, DropDownList ddlLojaRelatorios, RadioButton rbtPeriodo, RadioButton rbtMes)
        {
            List<clsRelat1> lst = new List<clsRelat1>();
            if (rbtPeriodo.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, txtInicio.Text, txtFim.Text, (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)), (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""));
            else if (rbtMes.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, (ddlLojaRelatorios.SelectedItem != null ? ddlLojaRelatorios.SelectedItem.Value : ""), (String.IsNullOrEmpty(ddlRedesRelatorios.SelectedValue) ? 0 : Convert.ToInt32(ddlRedesRelatorios.SelectedValue)));
            
            return lst;
        }

        public static List<clsRelat1> GetMod2(UsersTO clsUser, TextBox txtInicio, TextBox txtFim, int intRedeId, RadioButton rbtPeriodo, RadioButton rbtMes)
        {
            List<clsRelat1> lst = new List<clsRelat1>();
            if (rbtPeriodo.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, txtInicio.Text, txtFim.Text, intRedeId);
            else if (rbtMes.Checked)
                lst = RelatoriosDAL.GetMod2(clsUser, intRedeId);

            return lst;
        }
    }
}
