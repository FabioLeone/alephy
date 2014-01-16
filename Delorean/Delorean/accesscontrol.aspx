<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accesscontrol.aspx.cs" Inherits="Delorean.accesscontrol" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />

    <title>Delorian - Controle de acesso.</title>

    <link href="resources/reset.css" rel="stylesheet" />
    <link href="resources/animate.css" rel="stylesheet" />
    <link href="resources/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="corpo">
        <div id="container">
            <label for="txtName">Username:</label>
            <input type="text" id="txtName" runat="server" required/>
            <label for="username">Password:</label>
            <input type="password" id="txtPassword" runat="server" required/>
            <div id="lower">
                <input type="checkbox" id="cboxKeep" runat="server"/><label class="check" for="checkbox">Mantenha-me conectado</label>
                <input type="submit" value="Login" id="smtLogin" runat="server" onserverclick="smtLogin_ServerClick"/>
            </div>
        </div>
    </form>
</body>
</html>
