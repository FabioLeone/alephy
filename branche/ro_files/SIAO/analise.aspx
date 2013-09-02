<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="analise.aspx.cs" Inherits="SIAO.analise" %>
<%@ Register src="Controls/wucAnalise.ascx" tagname="wucAnalise" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function pageLoad() {
        $("section").addClass("sectionb");
    }

    window.onload(pageLoad);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucAnalise ID="wucAnalise1" runat="server" />
</asp:Content>
