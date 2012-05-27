using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

namespace SIAO
{
    public partial class wfmCadastroLojas : System.Web.UI.Page
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        SRV.clsControl o = new SRV.clsControl();
        SRV.clsFuncs of = new SRV.clsFuncs();

       protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            if (!IsPostBack) {
                LoadRedes();
                LoadUf();
                txtNomeFantasia.Focus();
            }
        }

       private void LoadUf()
       {
           DataSet ds = new DataSet();
           ds = o.GetUf(scn);

           if (ds.Tables.Count > 0) {
               ddlUf.DataSource = ds.Tables[0];
               ddlUf.DataValueField = ds.Tables[0].Columns[0].ToString();
               ddlUf.DataTextField = ds.Tables[0].Columns[1].ToString();
               ddlUf.DataBind();
               ddlUf.Items.Insert(0, new ListItem(String.Empty, String.Empty));
               ddlUf.SelectedIndex = 0;
           }
       }

       private void LoadRedes()
       {
           DataSet ds = new DataSet();
           ds = o.GetRedes(scn);

           if (ds.Tables.Count > 0) {
               ddlRede.DataSource = ds.Tables[0];
               ddlRede.DataTextField = ds.Tables[0].Columns[1].ToString();
               ddlRede.DataValueField = ds.Tables[0].Columns[0].ToString();
               ddlRede.DataBind();
               ddlRede.Items.Insert(0, new ListItem(String.Empty, String.Empty));
               ddlRede.SelectedIndex = 0;
           }
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

       protected void btnSave_Click(object sender, EventArgs e)
       {
           if (txtCidade.Text == "") { divErro("Entre com a cidade."); txtCidade.Focus(); }
           else if (!of.ValidaCnpj(txtCnpj.Text))
           {
               divErro("CNPJ invalido.");
               txtCnpj.Focus();
           }
           else if (!of.isEmail(txtEmail.Text)) { divErro("Email inválido."); txtEmail.Focus(); }
           else if (txtFone.Text == "")
           {
               divErro("Entre com o número do telefone.");
               txtFone.Focus();
           }
           else if (txtGerente.Text == "") { divErro("Entre com o nome do Gerente da Loja."); txtGerente.Focus(); }
           else if (txtNomeFantasia.Text == "")
           {
               divErro("Entre com o nome fantasia.");
               txtNomeFantasia.Focus();
           }
           else if (txtProprietario.Text == "") { divErro("Entre com o nome do proprietário."); txtProprietario.Focus(); }
           else if (txtRazao.Text == "")
           {
               divErro("Entre com a razão social.");
               txtRazao.Focus();
           }
           else {
               SRV.Loja ol = new SRV.Loja() { 
                Ativo = cbxAtivo.Checked,
                Bairro = txtBairro.Text,
                Celular = txtCel.Text,
                Cidade = txtCidade.Text,
                Cnpj = txtCnpj.Text,
                Complemento = txtComp.Text,
                Email = txtEmail.Text,
                Email2 = txtEmail2.Text,
                Endereco = txtEndereco.Text,
                EndNumero = Convert.ToInt32(txtNum.Text),
                Fone = txtFone.Text,
                Fone2 = txtFone2.Text,
                Gerente = txtGerente.Text,
                idRede = Convert.ToInt32(ddlRede.SelectedValue),
                Msn = txtMsn.Text,
                NomeFantasia = txtNomeFantasia.Text,
                Proprietario = txtProprietario.Text,
                Razao = txtRazao.Text,
                Rede = ddlRede.SelectedItem.Text,
                Site = txtSite.Text,
                Skype = txtSkype.Text,
                Uf = ddlUf.SelectedValue
               };

               string msg = o.AddLoja(scn, ol);

               if (msg != "") { divErro(msg); } else { divSces(); Clear(); }
               
           }
       }

       private void Clear()
       {
           cbxAtivo.Checked = false;
           ddlRede.SelectedIndex = 0;
           ddlUf.SelectedIndex = 0;
           txtBairro.Text = "";
           txtCel.Text = "";
           txtCidade.Text = "";
           txtCnpj.Text = "";
           txtComp.Text = "";
           txtEmail.Text = "";
           txtEmail2.Text = "";
           txtEndereco.Text = "";
           txtFone.Text = "";
           txtFone2.Text = "";
           txtGerente.Text = "";
           txtMsn.Text = "";
           txtNomeFantasia.Text = "";
           txtNum.Text = "";
           txtProprietario.Text = "";
           txtRazao.Text = "";
           txtSite.Text = "";
           txtSkype.Text = "";

           txtNomeFantasia.Focus();
       }
    }
}