using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SIAO.SRV.TO;
using SIAO.SRV.BLL;

namespace SIAO
{
    public partial class wfmGerarRelatorios : System.Web.UI.Page
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        UsersTO clsUser = new UsersTO();
        SRV.clsControl oc = new SRV.clsControl();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); } else {
                clsUser = (UsersTO)Session["user"];
            }

            if (!IsPostBack) { getAno(); getLojas(); }
            Global.LocalPage = "";

        }

        private void getLojas()
        {
            ddlLojas.DataSource = oc.GetLojaByUserId(scn, clsUser);
            ddlLojas.DataTextField = "NomeFantasia";
            ddlLojas.DataValueField = "Cnpj";
            ddlLojas.DataBind();
            ddlLojas.Items.Insert(0, new ListItem("Selecione", string.Empty));
            ddlLojas.SelectedIndex = 0;
        }

        private void getAno()
        {
            ddlAno.Items.Add(new ListItem(String.Empty, String.Empty));
            for (int i = 0; i < 3; i++)
            {
                ddlAno.Items.Add(new ListItem(System.DateTime.Now.AddYears(-i).Year.ToString(), System.DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddlAno.SelectedIndex = 0;
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "error");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces()
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "32%");
            divInfo.InnerHtml = "<p>Loja cadastrada com sucesso.</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divInfo);
        }

        protected void btnAdm_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();

            lr1 = oc.GetCross(scn, clsUser, ddlAno.SelectedItem.Value);

            Session["cross"] = lr1;

            Global.LocalPage = "Relat";

            Response.Redirect("wfmRelatMod1.aspx");
        }

        protected void btnRelat2_Click(object sender, EventArgs e)
        {
            List<SRV.clsRelat1> lr1 = new List<SRV.clsRelat1>();

            lr1 = oc.GetCross(scn, clsUser, ddlAno.SelectedItem.Value);

            Session["cross"] = lr1;

            Global.LocalPage = "Relat";

            Response.Redirect("wfmRelatMod2.aspx");
        }

        protected void ibtnGrafic1_Click(object sender, ImageClickEventArgs e)
        {
            List<GraficTO> clsGrafic;

            if(ddlMes.SelectedValue != "")
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(ddlMes.SelectedValue), clsUser, scn, ddlLojas.SelectedValue);
            else
                clsGrafic = GraficBLL.GraficList(Convert.ToInt32(DateTime.Now.Month), clsUser, scn, ddlLojas.SelectedValue);

            Session["grafic"] = clsGrafic;

            Global.LocalPage = "Relat";

            Response.Redirect("wfmRelatorio.aspx");
        }

    }
}