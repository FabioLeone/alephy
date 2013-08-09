using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using SIAO.SRV.BLL;
using SIAO.SRV.TO;
using System.Collections.Generic;
using SIAO.SRV;

namespace SIAO
{
    public partial class wfmCadastroLojas : System.Web.UI.Page
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        SRV.clsControl o = new SRV.clsControl();
        SRV.clsFuncs of = new SRV.clsFuncs();
        SRV.Loja clsLoja = new SRV.Loja();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            if (Session["editL"] != null) { clsLoja.Id = (int)Session["editL"]; }

            if (!IsPostBack)
            {
                LoadRedes();
                LoadUf();
                txtNomeFantasia.Focus();
            }

        }

        private void getLojas()
        {
            ddlLoja.DataSource = o.GetFarmaciasByRedeId(scn, ddlEdRedes.SelectedValue);
            ddlLoja.DataTextField = "NomeFantasia";
            ddlLoja.DataValueField = "Id";
            ddlLoja.DataBind();
            ddlLoja.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlLoja.SelectedIndex = 0;
        }

        private void LoadUf()
        {
            DataSet ds = new DataSet();
            ds = o.GetUf(scn);

            if (ds.Tables.Count > 0)
            {
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
            ds = clsControl.GetRedes(scn);

            if (ds.Tables.Count > 0)
            {
                ddlRede.DataSource = ds.Tables[0];
                ddlRede.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlRede.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlRede.DataBind();
                ddlRede.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRede.SelectedIndex = 0;

                ddlEdRedes.DataSource = ds.Tables[0];
                ddlEdRedes.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlEdRedes.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlEdRedes.DataBind();
                ddlEdRedes.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlEdRedes.Items.Insert(1, new ListItem("Independentes", "0"));
                ddlEdRedes.SelectedIndex = 0;
            }
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divError = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divError.ID = "msgError";
            divError.Attributes.Add("class", "alerta");
            divError.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divError.Style.Add("bottom", "3.5%");
            divError.InnerHtml = "<p>" + msg + "</p>";

            UpdatePanel1.ContentTemplateContainer.Controls.Add(divError);
        }

        private void divSces(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "-17%");
            divInfo.Style.Add("bottom", "4.6%");
            divInfo.InnerHtml = "<p>" + msg + "</p>";

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
            else if (txtNomeFantasia.Text == "")
            {
                divErro("Entre com o nome fantasia.");
                txtNomeFantasia.Focus();
            }
            else if (txtRazao.Text == "")
            {
                divErro("Entre com a razão social.");
                txtRazao.Focus();
            }
            else
            {
                string msg = "";

                bool ed = false;

                if (clsLoja.Id > 0)
                {
                    clsLoja.Ativo = cbxAtivo.Checked;
                    clsLoja.Bairro = txtBairro.Text;
                    clsLoja.Celular = txtCel.Text;
                    clsLoja.Cidade = txtCidade.Text;
                    clsLoja.Cnpj = txtCnpj.Text;
                    clsLoja.Complemento = txtComp.Text;
                    clsLoja.Email = txtEmail.Text;
                    clsLoja.Email2 = txtEmail2.Text;
                    clsLoja.Endereco = txtEndereco.Text;
                    clsLoja.EndNumero = Convert.ToInt32(txtNum.Text);
                    clsLoja.Fone = txtFone.Text;
                    clsLoja.Fone2 = txtFone2.Text;
                    clsLoja.Gerente = txtGerente.Text;
                    clsLoja.idRede = ddlRede.SelectedValue == "" ? 0 : Convert.ToInt32(ddlRede.SelectedValue);
                    clsLoja.Msn = txtMsn.Text;
                    clsLoja.NomeFantasia = txtNomeFantasia.Text;
                    clsLoja.Proprietario = txtProprietario.Text;
                    clsLoja.Razao = txtRazao.Text;
                    clsLoja.Rede = ddlRede.SelectedItem.Text;
                    clsLoja.Site = txtSite.Text;
                    clsLoja.Skype = txtSkype.Text;
                    clsLoja.Uf = ddlUf.SelectedValue;
                    clsLoja.CEP = txtCep.Text;

                    msg = o.UpdateLoja(scn, clsLoja);
                    ed = true;
                }
                else
                {
                    clsLoja = new SRV.Loja()
                    {
                        Ativo = cbxAtivo.Checked,
                        Bairro = txtBairro.Text,
                        Celular = txtCel.Text,
                        Cidade = txtCidade.Text,
                        Cnpj = txtCnpj.Text,
                        Complemento = txtComp.Text,
                        Email = txtEmail.Text,
                        Email2 = txtEmail2.Text,
                        Endereco = txtEndereco.Text,
                        EndNumero = txtNum.Text == "" ? 0 : Convert.ToInt32(txtNum.Text),
                        Fone = txtFone.Text,
                        Fone2 = txtFone2.Text,
                        Gerente = txtGerente.Text,
                        idRede = ddlRede.SelectedValue == "" ? 0 : Convert.ToInt32(ddlRede.SelectedValue),
                        Msn = txtMsn.Text,
                        NomeFantasia = txtNomeFantasia.Text,
                        Proprietario = txtProprietario.Text,
                        Razao = txtRazao.Text,
                        Rede = ddlRede.SelectedItem.Text,
                        Site = txtSite.Text,
                        Skype = txtSkype.Text,
                        Uf = ddlUf.SelectedValue,
                        CEP = txtCep.Text
                    };

                    msg = o.AddLoja(scn, clsLoja);
                }

                if (msg != "") { divErro(msg); }
                else
                {
                    if (ed)
                    {
                        divSces("Alterações salvas com sucesso.");
                        Clear();
                    }
                    else { divSces("Loja cadastrada com sucesso."); Clear(); }
                }

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
            txtProprietario.Text = "";
            txtMsn.Text = "";
            txtNomeFantasia.Text = "";
            txtNum.Text = "";
            txtGerente.Text = "";
            txtRazao.Text = "";
            txtSite.Text = "";
            txtSkype.Text = "";
            txtCep.Text = "";
            Session["editL"] = null;

            txtNomeFantasia.Focus();
        }

        protected void btnLoadLoja_Click(object sender, EventArgs e)
        {
            if (ddlEdRedes.SelectedValue != "") { getLojas(); }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SRV.Loja ol = new SRV.Loja();
            ol = o.GetLojaEdit(scn, ddlLoja.SelectedValue);

            if (ol.Id > 0)
            {
                try
                {
                    ddlRede.SelectedValue = ol.idRede.ToString();
                    ddlUf.SelectedValue = ol.Uf;
                }
                catch
                {
                }

                txtBairro.Text = ol.Bairro;
                txtCel.Text = ol.Celular;
                txtCidade.Text = ol.Cidade;
                txtCnpj.Text = ol.Cnpj;
                txtComp.Text = ol.Complemento;
                txtEmail.Text = ol.Email;
                txtEmail2.Text = ol.Email2;
                txtEndereco.Text = ol.Endereco;
                txtFone.Text = ol.Fone;
                txtFone2.Text = ol.Fone2;
                txtGerente.Text = ol.Gerente;
                txtMsn.Text = ol.Msn;
                txtNomeFantasia.Text = ol.NomeFantasia;
                txtNum.Text = ol.EndNumero.ToString();
                try
                {
                    txtProprietario.Text = ol.Proprietario;
                }
                catch 
                {
                }
                txtRazao.Text = ol.Razao;
                txtSite.Text = ol.Site;
                txtSkype.Text = ol.Skype;
                txtCep.Text = ol.CEP;

                Session["editL"] = ol.Id;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}