﻿using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using SIAO.SRV.TO;
using System.Text;

namespace SIAO
{
    public partial class wfmCadastroUsuario : System.Web.UI.Page
    {
        string scn = ConfigurationManager.ConnectionStrings["SIAOConnectionString"].ConnectionString;
        SRV.clsFuncs of = new SRV.clsFuncs();
        SRV.clsControl oCtl = new SRV.clsControl();
        UsersTO clsUser = new UsersTO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null) { Response.Redirect("Logon.aspx"); }

            if (Session["editU"] != null) { clsUser.UserId = (int)Session["editU"]; }

            if (!IsPostBack)
            {
                clear();
                getUsers();
            }
            txtNome.Focus();
        }

        private void getUsers()
        {
            DataSet ds = new DataSet();
            ds = oCtl.GetUsers(scn);

            if (ds.Tables.Count > 0)
            {
                ddlUser.DataSource = ds.Tables[0];
                ddlUser.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlUser.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlUser.SelectedIndex = 0;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                if (lblAN.Visible) { lblAN.Visible = false; }
                if (lblN.Visible) { lblN.Text = "*"; } else { lblN.Text = "*"; lblN.Visible = true; }
                divErro("Entre com o nome do usuário.");
            }
            else if (txtAcsName.Text == "")
            {
                if (lblN.Visible) { lblN.Visible = false; }
                if (lblAN.Visible) { lblAN.Text = "*"; } else { lblAN.Text = "*"; lblAN.Visible = true; }
                if (lblE.Visible) { lblE.Visible = false; }
                divErro("Entre com o nome de acesso.");
            }
            else if (!of.isEmail(txtEmail.Text))
            {
                if (lblN.Visible) { lblN.Visible = false; }
                if (lblAN.Visible) { lblAN.Visible = false; }
                if (lblE.Visible) { lblE.Text = "*"; } else { lblE.Text = "*"; lblE.Visible = true; }
                divErro("Entre com o e-mail do usuário.");
            }
            else if (txtValidade.Text != "")
            {
                if (lblS.Visible) { lblS.Visible = false; }
                
                if (clsUser.UserId == 0 && txtSenha.Text == "")
                {
                    if (lblV.Visible) { lblV.Visible = false; }
                    if (lblE.Visible) { lblE.Visible = false; }
                    if (lblS.Visible) { lblS.Text = "*"; } else { lblS.Text = "*"; lblS.Visible = true; }
                    divErro("Entre com a senha do usuário.");
                }
                else if (txtSenha.Text == txtCfrSenha.Text)
                {
                    if (lblV.Visible) { lblV.Visible = false; }
                    try
                    {
                        Convert.ToDateTime(txtValidade.Text);
                    }
                    catch
                    {
                        if (lblV.Visible) { lblE.Text = "*"; } else { lblV.Text = "*"; lblV.Visible = true; }
                        divErro("Data inválida.");
                        return;
                    }

                    string msg = "";

                    bool ed = false;

                    if (clsUser.UserId > 0)
                    {

                        clsUser.Access = rblAccess.SelectedValue;
                        clsUser.Name = txtAcsName.Text;
                        clsUser.CreateDate = DateTime.Today;
                        clsUser.Email = txtEmail.Text;
                        clsUser.Inactive = !cbxAtivo.Checked;
                        clsUser.UserName = txtNome.Text;

                        if (!string.IsNullOrEmpty(txtSenha.Text))
                            clsUser.Password = txtSenha.Text;
                        else
                            clsUser.Password = Session["editUP"].ToString();
                        
                        clsUser.ExpirationDate = Convert.ToDateTime(txtValidade.Text);
                        
                        msg = oCtl.UpdateUser(clsUser, scn);
                        ed = true;
                    }
                    else
                    {
                        clsUser = new UsersTO()
                        {
                            Access = rblAccess.SelectedValue,
                            Name = txtAcsName.Text,
                            CreateDate = DateTime.Today,
                            Email = txtEmail.Text,
                            Inactive = !cbxAtivo.Checked,
                            UserName = txtNome.Text,
                            Password = txtSenha.Text,
                            ExpirationDate = Convert.ToDateTime(txtValidade.Text),
                        };
                        msg = oCtl.AddUser(clsUser, scn);
                    }

                    if (msg != "")
                    {
                        divErro(msg);
                    }
                    else
                    {
                        clear();

                        if (ed) { div("Alterações salvas com sucesso."); } else { div("Usuário cadastrado com sucesso."); }
                    }
                }
                else
                {
                    if (lblCS.Visible) { lblCS.Text = "*"; } else { lblCS.Text = "*"; lblCS.Visible = true; }
                    divErro("Senha e confirmação, não conferem.");
                }
            }
            else
            {
                if (lblS.Visible) { lblS.Visible = false; }
                if (lblV.Visible) { lblV.Text = "*"; } else { lblV.Text = "*"; lblV.Visible = true; }
                divErro("Entre com a data de validade da senha.");
            }
        }

        protected void clear()
        {
            txtNome.Text = "";
            txtAcsName.Text = string.Empty;
            txtSenha.Text = "";
            txtValidade.Text = "";
            txtCfrSenha.Text = "";
            txtEmail.Text = "";
            Session["editU"] = null;
            ddlUser.SelectedIndex = 0;
            Session["editUP"] = null;
        }

        private void div(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgInfo";
            divInfo.Attributes.Add("class", "success");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "28%");
            divInfo.InnerHtml = "<p>" + msg + "</p>";

            Panel1.Controls.Add(divInfo);
        }

        private void divErro(string msg)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divInfo = new System.Web.UI.HtmlControls.HtmlGenericControl("div");

            divInfo.ID = "msgError";
            divInfo.Attributes.Add("class", "error");
            divInfo.Style.Add(HtmlTextWriterStyle.MarginLeft, "4%");
            divInfo.InnerHtml = "<p>" + msg + "</p>";

            Panel1.Controls.Add(divInfo);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            clsUser = oCtl.GetUserEdit(scn, ddlUser.SelectedValue);

            if (clsUser.UserId > 0)
            {
                cbxAtivo.Checked = (bool)(clsUser.Inactive.ToString().ToUpper() == "FALSE" ? true : false);
                txtAcsName.Text = clsUser.Name;
                txtEmail.Text = clsUser.Email;
                txtNome.Text = clsUser.UserName;
                txtValidade.Text = clsUser.ExpirationDate.ToShortDateString();
                rblAccess.SelectedValue = clsUser.Access;
                Session["editU"] = clsUser.UserId;
                Session["editUP"] = clsUser.Password;
            }
            txtSearch.Text = string.Empty;
            ddlUser.SelectedIndex = 0;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            clear();   
        }

    }
}