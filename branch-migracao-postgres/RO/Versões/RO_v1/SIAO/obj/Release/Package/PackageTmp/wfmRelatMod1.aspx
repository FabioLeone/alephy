<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmRelatMod1.aspx.cs" Inherits="SIAO.wfmRelatMod1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        InteractiveDeviceInfos="(Collection)" CssClass="relat" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Width="100%" Height="100%">
        <LocalReport ReportPath="Relatory\rptCross.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
</asp:Content>
