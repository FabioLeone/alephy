<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="competitors.aspx.cs" Inherits="Delorean.controls.competitors" %>

<%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="lvwCompetitors" runat="server" DataKeyNames="Ean" 
        OnItemDataBound="lvwCompetitors_ItemDataBound" OnPagePropertiesChanging="lvwCompetitors_PagePropertiesChanging">
        <LayoutTemplate>
            <table class="sortable striped" cellspacing="0" cellpadding="0">
                <thead>
                    <tr id="Th1" runat="server" class="alt first last">
                    </tr>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td id="barcod" runat="server"><%# Eval("Ean") %></td>
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
