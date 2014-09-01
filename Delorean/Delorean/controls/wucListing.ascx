<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListing.ascx.cs" Inherits="Delorean.controls.wucListing" %>
<%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>
<asp:ListView ID="lvwProducts" runat="server" DataKeyNames="bcid" OnPagePropertiesChanging="lvwProducts_PagePropertiesChanging">
    <LayoutTemplate>
        <table class="sortable striped" cellspacing="0" cellpadding="0">
            <thead>
                <tr class="alt first last">
                    <th rel="0" value="Ean">Ean</th>
                    <th rel="1" value="Produto">Produto</th>
                    <th rel="2" value="CustoReal">Custo Real</th>
                    <th rel="3" value="Unidade">Valor Unitário</th>
                    <th rel="4" value="UpX">Desconto Quantidade</th>
                    <th rel="5" value="MargemReal">Margem Real</th>
                    <th rel="6" value="Drogasil" class="hide">Drogasil</th>
                </tr>
            </thead>
            <tbody>
                <tr runat="server" id="itemPlaceholder" />
            </tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr runat="server">
            <td id="barcod" runat="server"><%# Eval("barras") %></td>
            <td><%# Eval("nomeprod") %></td>
            <td style="padding: 0; width: 120px;">
                <asp:TextBox ID="txtvcost" runat="server" OnTextChanged="txtvcost_TextChanged" AutoPostBack="true"
                    Text='<%# Eval("valor_custo") %>' TabIndex='<%# ((ListViewDataItem)Container).DisplayIndex %>'
                    Style="margin: 0; box-shadow: none; width: 100%; height: inherit; border: 0; background: inherit;"></asp:TextBox>
            </td>
            <td><%# Eval("one_unit") %></td>
            <td><%# Eval("upx") %></td>
            <td><%# Eval("actual_margin") + "%" %></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        <table class="sortable striped" cellspacing="0" cellpadding="0">
            <tr runat="server">
                <td>Não há itens para serem exibidos
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
</asp:ListView>

<ale:ulDataPager ID="dpgProducts" runat="server" PagedControlID="lvwProducts" PageSize="15">
    <Fields>
        <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="" />
        <asp:NumericPagerField />
        <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="" />
    </Fields>
</ale:ulDataPager>
