<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListing.ascx.cs" Inherits="Delorean.controls.wucListing" %>
<%@ Register Assembly="Ctrls" Namespace="Ctrls" TagPrefix="ale" %>
<asp:ListView ID="lvwProducts" runat="server">
    <LayoutTemplate>
        <table class="sortable striped" cellspacing="0" cellpadding="0">
            <thead>
                <tr class="alt first last">
                    <th rel="0" value="Ean">Ean</th>
                    <th rel="1" value="Produto">Produto</th>
                    <th rel="2" value="CustoReal">Custo Real</th>
                    <th rel="3" value="Unidade">Unidade</th>
                    <th rel="4" value="UpX">Acima de X</th>
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
            <td><%# Eval("barras") %></td>
            <td><%# Eval("nomeprod") %></td>
            <td contenteditable><%# Eval("valor_custo") %></td>
            <td><%# Eval("one_unit") %></td>
            <td><%# Eval("upx") %></td>
            <td><%# Eval("actual_margin") %></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
        Não há itens para serem exibidos
    </EmptyDataTemplate>
</asp:ListView>

<ale:ulDataPager ID="dpgProducts" runat="server" PagedControlID="lvwProducts" PageSize="15">
    <Fields>
        <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="" />
        <asp:NumericPagerField />
        <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="" />
    </Fields>
</ale:ulDataPager>
<!--
<ul class="button-bar">
    <li class="first"><a href="#"><i class="icon-caret-left"></i></a></li>
    <li><a href="#">1</a></li>
    <li><a href="#">2</a></li>
    <li><a href="#">3</a></li>
    <li><a href="#">4</a></li>
    <li><a href="#">5</a></li>
    <li><a href="#">6</a></li>
    <li><a href="#">7</a></li>
    <li><a href="#">8</a></li>
    <li><a href="#">9</a></li>
    <li><a href="#">10</a></li>
    <li class="last"><a href="#"><i class="icon-caret-right"></i></i></li>
</ul>
-->
