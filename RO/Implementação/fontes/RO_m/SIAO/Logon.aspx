<%@ Page Language="C#" Inherits="SIAO.Logon" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<meta charset="utf-8">
    <title>R.O.</title>
    <link href="Content/ssSIAO.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Content/js/modernizr.custom.86080.js"></script>
    <!-- Include jQuery -->
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            if (/http:\/\/(www\.)?multi\.pdev\.com\.br\/ro/.test(document.URL)) { window.location = "http://multi.pdev.com.br/ro_multi"; }
        }
    </script>
</head>
<body>
	<form id="frmLogon" runat="server">
		<div id="fundo">
    	</div>
		<div>
			<section>
				<div id="login">
					<h1>Login</h1>
					<p>Seja bem-vindo ao R.O.</p>
					<div>
						<label>Usuário </label><asp:TextBox name="q" ID="txtNome" runat="server" size="30" MaxLength="2048"></asp:TextBox>
						<asp:RequiredFieldValidator ControlToValidate="txtNome" ValidationGroup="Login" ID="rfvNome"
                    		runat="server" ErrorMessage="Entre com o nome.">*</asp:RequiredFieldValidator>
					</div>
					<div>
						<label>Senha </label><asp:TextBox ID="txtSenha" runat="server" TextMode="Password" size="30" MaxLength="2048"
                        	name="q" style="margin-left: 9px;"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSenha" ValidationGroup="Login" runat="server"
                        	ControlToValidate="txtSenha" ErrorMessage="Entre com a senha.">*</asp:RequiredFieldValidator>
					</div>
					<asp:Button ID="btnLogin" ValidationGroup="Login" runat="server" Style="margin-left:68%;" Text="Logar"
	                	OnClick="lbtnLogin_Click" />
	                <asp:Label ID="lblVersion" runat="server" Text="Kokiri - Rv.152"></asp:Label>
				</div>
			</section>
		</div>
		<asp:ValidationSummary ID="vsmLogin" ValidationGroup="Login" CssClass="alerta" runat="server" />
	</form>
</body>
</html>

