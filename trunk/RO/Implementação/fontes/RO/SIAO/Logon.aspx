<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="SIAO.Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>R.O.</title>
    <link href="Content/ssSIAO.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmLogon" runat="server">
    <div style="margin: auto; vertical-align: middle;">
        <div id="login-box">
            <h2>
                Login</h2>
            Seja bem-vindo ao Investigador CasBrasil. Digite seu usu&aacute;rio e senha para
            entrar!<br />
            <br />
            <div class="login-box-name" style="margin-top: 20px;">
                Usu&aacute;rio:</div>
            <div class="login-box-field" style="margin-top: 20px;">
                <asp:TextBox name="q" class="form-login"  ID="txtNome" runat="server" size="30" maxlength="2048"></asp:TextBox>
                </div>
            <div class="login-box-name">
                Senha:</div>
            <div class="login-box-field">
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" size="30"
                    maxlength="2048" name="q" class="form-login"></asp:TextBox>
                    </div>
            <p>
                <br />
            </p>
            <p>
                <br />
                <br />
                <asp:LinkButton ID="lbtnLogin" runat="server" onclick="lbtnLogin_Click"><img src="Content/login-btn.png" width="103" height="42" alt="" style="margin-left: 90px;" /></asp:LinkButton>
                </p>
        </div>
    </div>
    </form>
</body>
</html>
