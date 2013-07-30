<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmIndices.aspx.cs" Inherits="SIAO.wfmIndices" %>
<%@ Register src="Controls/wucListaIndices.ascx" tagname="wucListaIndices" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucListaIndices ID="wucListaIndices1" runat="server" />
</asp:Content>
