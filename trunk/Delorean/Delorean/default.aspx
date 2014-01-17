<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Delorean.controls._default" %>

<%@ Register src="controls/wucListing.ascx" tagname="wucListing" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wucListing ID="wucListing1" runat="server" />
</asp:Content>
