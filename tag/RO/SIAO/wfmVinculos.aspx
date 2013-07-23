<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmVinculos.aspx.cs" Inherits="SIAO.wfmVinculos" %>
<%@ Register src="Controls/wucVinculos.ascx" tagname="wucVinculos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucVinculos ID="wucVinculos1" runat="server" />
</asp:Content>
