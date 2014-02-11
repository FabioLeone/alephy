<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucSending_file.ascx.cs" Inherits="Delorean.controls.wucSending_file" %>

<div>
    <input type="file" id="upfile" runat="server" class="file" />
    <input type="button" id="btnSend" runat="server" value="Enviar" onserverclick="btnSend_ServerClick" />
</div>