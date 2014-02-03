<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="competitors.aspx.cs" Inherits="Delorean.controls.competitors" %>

<%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="lvwCompetitors" runat="server" DataKeyNames="barras" OnItemDataBound="lvwCompetitors_ItemDataBound">
        <LayoutTemplate>
            <table class="sortable striped" cellspacing="0" cellpadding="0">
                <thead>
                    <tr id="Th1" runat="server" class="alt first last">
                        <th rel="0" value="Ean">Ean</th>
                        <th rel="1" value="Produto">Produto</th>
                    </tr>
                    <tr id="Th2" runat="server"></tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td id="barcod" runat="server"><%# Eval("barras") %></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            Não há itens para serem exibidos
        </EmptyDataTemplate>
    </asp:ListView>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="../resources/scripts/kickstart.js"></script>
</asp:Content>
