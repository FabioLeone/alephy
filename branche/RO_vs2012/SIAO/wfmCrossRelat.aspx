<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfmCrossRelat.aspx.cs"
    Inherits="SIAO.wfmCrossRelat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>R.O.</title>
    <link href="Content/ssSIAO.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <header>
        <h1>
            R.O.</h1>
        <p>
            CasBrasil RELATÓRIO ONLINE</p>
        <asp:Button ID="btnLogoff" CssClass="rightButton blue" runat="server" Text="Logoff"
            OnClick="btnLogoff_Click" />
        <input type="button" onclick="window.location = 'wfmGerarRelatorios.aspx'" value="Voltar"
            class="LeftButton blue" title="Voltar" />
    </header>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
        InteractiveDeviceInfos="(Collection)" CssClass="relat" WaitMessageFont-Names="Verdana"
        WaitMessageFont-Size="14pt" Width="81.34%">
        <LocalReport ReportPath="Relatory\rptCross.rdlc">
        </LocalReport>
    </rsweb:ReportViewer>
    </form>
</body>
</html>
