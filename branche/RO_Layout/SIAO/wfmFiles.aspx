<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmFiles.aspx.cs" Inherits="SIAO.wfmFiles" %>
<%@ Register src="Controls/wucListFiles.ascx" tagname="wucListFiles" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:wucListFiles ID="wucListFiles1" runat="server" />

</asp:Content>
