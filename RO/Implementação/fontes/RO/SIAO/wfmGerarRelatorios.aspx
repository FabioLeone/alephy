<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="width: 96.5%;">
                <legend>
                    <h2>Relatórios</h2>
                </legend>
                <div id="divForm" style="height: 330px; width: 100%;">
                    <p>Selecione o ano, para consulta.
                    <asp:DropDownList ID="ddlAno" runat="server">
                    </asp:DropDownList>&nbsp;
                    <asp:Button ID="btnAdm" runat="server" Text="Gerar" onclick="btnAdm_Click" /></p>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
