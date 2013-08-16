<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucRoles.ascx.cs" Inherits="SIAO.Controls.wucRoles" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="upAcesso" runat="server">
    <ContentTemplate>
        <div>
            <table style="width:300px;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Redes" AssociatedControlID="ddlRedes"></asp:Label>
                        <asp:DropDownList ID="ddlRedes" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPlus" runat="server" Text="+" OnClick="btnPlus_Click" CssClass="rightButton gray" />
                        <asp:Button ID="btnOk" runat="server" Text="&#x25BC;" OnClick="btnOk_Click" CssClass="LeftButton gray" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ListView ID="lvwRole" runat="server" OnItemDataBound="lvwRole_ItemDataBound">
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="Label2" runat="server" Text="Envio de arquivo"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="Label3" runat="server" Text="Relatórios"></asp:Label>
                        </th>
                    </tr>
                    <div id="group">
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlUsuarios" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:HiddenField ID="hfRoleId" Value='<%# Eval("RoleId") %>' runat="server" />
                        <asp:CheckBox ID="chbEnvio" runat="server" OnCheckedChanged="chbEnvio_CheckedChanged"
                            AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chbAll" runat="server" Text="Todos" OnCheckedChanged="chbAll_CheckedChanged"
                            AutoPostBack="true" />
                        <asp:CheckBoxList ID="cblReportsGrafics" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Modelo1</asp:ListItem>
                            <asp:ListItem Value="2">Modelo2</asp:ListItem>
                            <asp:ListItem Value="3">Grafico1</asp:ListItem>
                            <asp:ListItem Value="4">Grafico2</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <div>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click"
                CssClass="button gray" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
