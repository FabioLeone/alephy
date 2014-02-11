<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="factors_set.aspx.cs" Inherits="Delorean.controls.factors_set" %>

    <%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ListView ID="lvwFactors" runat="server" DataKeyNames="id" OnItemDataBound="lvwFactors_ItemDataBound"
        OnPagePropertiesChanging="lvwFactors_PagePropertiesChanging">
        <LayoutTemplate>
            <table class="sortable striped" cellspacing="0" cellpadding="0">
                <thead>
                    <tr class="alt first last">
                        <th rel="0" value="Ean">Ean</th>
                        <th rel="1" value="Produto">Produto</th>
                        <th rel="2" value="Condcxs">Cond Cxs</th>
                        <th rel="3" value="MargEsperada">Margem Esperada</th>
                        <th rel="4" value="DescProm">Desc. Promo</th>
                        <th rel="5" value="DemCxs">Demais Cxs</th>
                    </tr>
                </thead>
                <tbody>
                    <tr runat="server" id="itemPlaceholder" />
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td id="barcod" runat="server"><%# Eval("barras") %></td>
                <td><%# Eval("prod_name") %></td>
                <td style="padding: 0; width: 120px;">
                    <asp:TextBox ID="txtcond" runat="server" AutoPostBack="false" CssClass="cond"
                        TabIndex='<%# Convert.ToInt16(Eval("id")) %>'
                        Text='<%# Eval("cont_bxs") %>' Style="margin: 0; box-shadow: none; width: 100%; height: inherit; border: 0; background: inherit;"></asp:TextBox>
                </td>
                <td style="padding: 0; width: 120px;">
                    <asp:TextBox ID="txtmarg" runat="server" OnTextChanged="txtmarg_TextChanged" AutoPostBack="true"
                        TabIndex='<%# ((ListViewDataItem)Container).DisplayIndex %>'
                        Text='<%# Eval("expected_margin") %>' Style="margin: 0; box-shadow: none; width: 100%; height: inherit; border: 0; background: inherit;"></asp:TextBox>
                </td>
                <td style="padding: 0; width: 120px;">
                    <asp:TextBox ID="txtdesc" runat="server" OnTextChanged="txtdesc_TextChanged" AutoPostBack="true"
                        TabIndex='<%# ((ListViewDataItem)Container).DisplayIndex %>'
                        Text='<%# Eval("discount") %>' Style="margin: 0; box-shadow: none; width: 100%; height: inherit; border: 0; background: inherit;"></asp:TextBox>
                </td>
                <td><%# Eval("other_bxs") %></td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            Não há itens para serem exibidos
        </EmptyDataTemplate>
    </asp:ListView>

    <ale:ulDataPager ID="dpgFactors" runat="server" PagedControlID="lvwFactors" PageSize="15">
        <Fields>
            <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="" />
            <asp:NumericPagerField />
            <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="" />
        </Fields>
    </ale:ulDataPager>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="../resources/scripts/kickstart.js"></script>
    <script type="text/javascript" src="../resources/scripts/handlejs.js"></script>
</asp:Content>
