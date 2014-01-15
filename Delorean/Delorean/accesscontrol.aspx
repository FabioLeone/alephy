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
            <label for="name">Username:</label>
            <input type="name" id="txtName" runat="server" />
            <label for="username">Password:</label>
            <p><a href="#">Forgot your password?</a></p>
            <input type="password" id="txtPassword" runat="server" />
            <div id="lower">
                <input type="checkbox" /><label class="check" for="checkbox">Keep me logged in</label>
                <input type="submit" value="Login" id="" />
            </div>
        </div>
    </form>
</body>
</html>
