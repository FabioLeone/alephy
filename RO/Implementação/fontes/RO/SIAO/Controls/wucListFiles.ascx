<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListFiles.ascx.cs" Inherits="SIAO.Controls.wucListFiles" %>
<div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Selecione a Rede"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlRedes" runat="server">
                </asp:DropDownList>
            </td>
            <td rowspan="2">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" 
                    CssClass="button gray" onclick="btnFiltrar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Selecione o ano"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlAno" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:ListView ID="lvwFiles" runat="server" 
        onitemdatabound="lvwFiles_ItemDataBound">
        <LayoutTemplate>
            <table>
                <tr>
                    <th>
                        Loja
                    </th>
                    <th>
                    1
                    </th>
                    <th>
                    2
                    </th>
                    <th>
                    3
                    </th>
                    <th>
                    4
                    </th>
                    <th>
                    5
                    </th>
                    <th>
                    6
                    </th>
                    <th>
                    7
                    </th>
                    <th>
                    8
                    </th>
                    <th>
                    9
                    </th>
                    <th>
                    10
                    </th>
                    <th>
                    11
                    </th>
                    <th>
                    12
                    </th>
                </tr>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:HiddenField ID="hfCnpj" Value='<%# Eval("cnpj") %>' runat="server" />
                    <asp:Label ID="lblLojaNome" runat="server" Text='<%# Eval("NomeFantasia") %>'></asp:Label>
                </td>
                <asp:ListView ID="lvwMeses" runat="server" onitemdatabound="lvwMeses_ItemDataBound">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <td>
                            <asp:HiddenField ID="hfMes" runat="server" Value='<%# Eval("mes") %>' />
                            <asp:Label ID="lblCheck" runat="server"></asp:Label>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</div>