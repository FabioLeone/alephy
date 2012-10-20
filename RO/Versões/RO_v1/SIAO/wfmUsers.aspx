<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmUsers.aspx.cs" Inherits="SIAO.wfmUsers" %>

<%@ Register Src="Controls/wucListaUsers.ascx" TagName="wucListaUsers" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <uc1:wucListaUsers ID="wucListaUsers1" runat="server" />
    </div>
</asp:Content>
