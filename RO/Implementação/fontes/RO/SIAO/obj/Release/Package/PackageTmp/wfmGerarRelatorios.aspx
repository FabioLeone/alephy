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
                    </asp:DropDownList><br />
                    <asp:Button ID="btnAdm" runat="server" Text="Modelo1" onclick="btnAdm_Click" CssClass="imgBtn" />&nbsp;
                    <asp:Button ID="btnRelat2" runat="server" Text="Modelo2" onclick="btnRelat2_Click"  CssClass="imgBtn" /></p>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
