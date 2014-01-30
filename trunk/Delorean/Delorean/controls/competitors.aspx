<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="competitors.aspx.cs" Inherits="Delorean.controls.competitors" %>

<%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="lvwCompetitors" runat="server" DataKeyNames="id" OnDataBound="lvwCompetitors_DataBound"
        OnPagePropertiesChanging="lvwCompetitors_PagePropertiesChanging">
        <LayoutTemplate>
            <table class="sortable striped" cellspacing="0" cellpadding="0">
                <thead>
                    <tr class="alt first last">
                        <th rel="0" value="Ean">Ean</th>
                        <th rel="1" value="Produto">Produto</th>
                        <asp:PlaceHolder ID="hdr" runat="server"></asp:PlaceHolder>
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td id="barcod" runat="server"><%# Eval("barras") %></td>
                <td><%# Eval("nomeprod") %></td>
                <td><%# Eval("valor_preco") %></td>
                <td><%# Eval("valor_desconto") %></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            Não há itens para serem exibidos
        </EmptyDataTemplate>
    </asp:ListView>

    <ale:ulDataPager ID="dpgCompetitors" runat="server" PagedControlID="lvwCompetitors" PageSize="15">
        <Fields>
            <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="" />
        </Fields>
    </ale:ulDataPager>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="../resources/scripts/kickstart.js"></script>
</asp:Content>
