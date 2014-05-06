<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accesscontrol.aspx.cs" Inherits="Delorean.accesscontrol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>Consulta Preço - Controle de acesso.</title>

    <link href="resources/css/reset.css" rel="stylesheet" />
    <link href="resources/css/animate.css" rel="stylesheet" />

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <!-- KICKSTART -->
    <script src="resources/scripts/kickstart.js"></script>
    <link rel="stylesheet" href="resources/css/kickstart.css" media="all" />
    <link href="resources/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="corpo">
        <div id="container">
            <div id="form">
                <label for="txtName">Username:</label>
                <input type="text" id="txtName" runat="server" required />
                <label for="username">Password:</label>
                <input type="password" id="txtPassword" runat="server" required />
                <div id="lower">
                    <input type="checkbox" id="cboxKeep" runat="server" /><label class="check" for="checkbox">Mantenha-me conectado</label>
                    <input type="submit" value="Login" id="smtLogin" runat="server" onserverclick="smtLogin_ServerClick" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
