<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmAcesso.aspx.cs" Inherits="SIAO.wfmAcesso" %>
<%@ Register src="Controls/wucRoles.ascx" tagname="wucRoles" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucRoles ID="wucRoles1" runat="server" />
</asp:Content>
