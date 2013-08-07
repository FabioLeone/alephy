<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="configurations.aspx.cs" Inherits="SIAO.Views.configurations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("li").click(function () {
                $("#leftbox").removeClass("leftIni");
                $("#leftbox").addClass("leftNav");
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="leftbox" class="leftIni">
    <span>Menu</span>
    <div>
        <ul>
            <li><a href="#">Cadastro de usuários</a></li>
            <li><a href="#">Cadastro de redes</a></li>
            <li><a href="#">Cadastro de lojas</a></li>
            <li><a href="#">Vincular usuário</a></li>
            <li><a href="#">Histórico de uploads</a></li>
        </ul>
    </div>
</div>
</asp:Content>
