using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using SIAO.SRV;
using System.Configuration;

namespace SIAO
{
    public partial class wfmLabPage : System.Web.UI.Page
    {
        SRV.clsControl oc = new SRV.clsControl();
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        private UsersTO clsUser = new UsersTO();

        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser = UsersBLL.GetUserSession(new UsersTO());
            if (clsUser.UserId == 0) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack)
                PopulaDados();
        }

        private void PopulaDados()
        {
            //Ano
            ddlAno.Items.Add(new ListItem("Escolha o Ano", String.Empty));
            for (int i = 0; i < 3; i++)
            {
                ddlAno.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddlAno.SelectedIndex = 0;

            //Rede
            ddlRede.DataSource = clsControl.GetRedes();
            ddlRede.DataTextField = "Descricao";
            ddlRede.DataValueField = "Id";
            ddlRede.DataBind();
            ddlRede.Items.Insert(0, new ListItem("Escolha a rede", String.Empty));
            ddlRede.SelectedIndex = 0;

            //Grupos
            cblGrupos.DataSource = clsControl.GetGrupos(scn);
            cblGrupos.DataTextField = "Grupo";
            cblGrupos.DataValueField = "Grupo";
            cblGrupos.DataBind();

            //Subgrupos
            cblCategoria.DataSource = clsControl.GetSubCategorias(scn);
            cblCategoria.DataTextField = "Sub_Consultoria";
            cblCategoria.DataValueField = "Sub_Consultoria";
            cblCategoria.DataBind();
        }

        protected void btnGerarGrafico_Click(object sender, EventArgs e)
        {
            List<GraficoLabTO> clsGraficoLab;
            List<string> lstGrupos = new List<string>();
            List<string> lstSubGrupos = new List<string>();

            foreach (ListItem li in cblGrupos.Items)
            {
                if (li.Selected)
                    lstGrupos.Add(li.Value);
            }
            foreach (ListItem li in cblCategoria.Items)
            {
                if (li.Selected)
                    lstSubGrupos.Add(li.Value);
            }

            if (!string.IsNullOrEmpty(ddlAno.SelectedValue))
                clsGraficoLab = GraficoLabBLL.GetListaTotaisMesByFilter(scn, Convert.ToInt32(ddlRede.SelectedValue), Convert.ToInt32(ddlAno.SelectedValue), lstGrupos, lstSubGrupos);
            else
                clsGraficoLab = GraficoLabBLL.GetListaTotaisMesByFilter(scn, Convert.ToInt32(ddlRede.SelectedValue), Convert.ToInt32(DateTime.Now.Year), lstGrupos, lstSubGrupos);

            Global.LocalPage = "wfmLabPage.aspx";
            Session["graficoLab"] = clsGraficoLab;

            Response.Redirect("wfmLabRelat.aspx");
        }
    }
}