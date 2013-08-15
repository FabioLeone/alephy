<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucAnalise.ascx.cs" Inherits="SIAO.Controls.wucAnalise" %>
<%@ Register src="wucFilter.ascx" tagname="wucFilter" tagprefix="uc1" %>

<div>
    <uc1:wucFilter ID="wucFilter1" runat="server" />
    <input type="button" id="btnAnalise" runat="server" value="Analisar" class="button gray" onserverclick="btnAnalise_ServerClick" />
</div>