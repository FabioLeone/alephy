<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListing.ascx.cs" Inherits="Delorean.controls.wucListing" %>
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
            <td>789555454</td>
            <td>Produto X</td>
            <td contenteditable>5,30</td>
            <td>2,3</td>
            <td>2,1</td>
            <td>23%</td>
        </tr>
    </ItemTemplate>
</asp:ListView>

    <div class="col_12 column">
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
    </div>