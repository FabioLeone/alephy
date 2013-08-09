<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmIndices.aspx.cs" Inherits="SIAO.wfmIndices" %>
<%@ Register src="Controls/wucListaIndices.ascx" tagname="wucListaIndices" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
     function pageLoad() {
         $("section").addClass("sectionb");
     }

     window.onload(pageLoad);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucListaIndices ID="wucListaIndices1" runat="server" />
</asp:Content>
