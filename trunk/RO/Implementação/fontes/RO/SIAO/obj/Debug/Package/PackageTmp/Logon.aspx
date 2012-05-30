<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="SIAO.Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>R.O.</title>
    <link href="Content/ssSIAO.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmLogon" runat="server">
    <div>
        <fieldset>
            <legend>Login</legend>
            <table>
                <tr>
                    <td style="text-align: right;">
                        <label>
                            Nome:</label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtNome" runat="server" Width="152px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <label>
                            Senha:</label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtSenha" runat="server" Width="152px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:Label ID="lblAlert" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Button ID="btnEnter" runat="server" Text="Entrar" OnClick="btnEnter_Click" CssClass="button gray" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    </form>
</body>
</html>
