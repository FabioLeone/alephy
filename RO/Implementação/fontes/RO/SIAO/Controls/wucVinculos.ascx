<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucVinculos.ascx.cs"
    Inherits="SIAO.Controls.wucVinculos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="upVinculos" runat="server">
    <ContentTemplate>
        <div>
            <table style="width: 300px;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Tipo do usuários" AssociatedControlID="ddlTipos"></asp:Label>
                        <asp:DropDownList ID="ddlTipos" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearch" Width="218px" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="button gray" Text="Buscar" OnClick="btnSearch_Click">
                        </asp:Button>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Usuário ID"></asp:Label>
                        <asp:TextBox ID="txtUsuarioId" Width="109px" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: center;">
                        <asp:Button ID="btnAdd" runat="server" CssClass="button gray" Text="+" Width="24px"
                            ToolTip="Adicionar registro." OnClick="btnAdd_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
        <center>
            <asp:ListView ID="lvwVinculos" runat="server" onitemdatabound="lvwVinculos_ItemDataBound">
                <LayoutTemplate>
                    <table width="80%;">
                        <tr class="header">
                            <th>
                                <asp:Label ID="Label1" runat="server" Text="Usuario ID"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="Label3" runat="server" Text="CNPJ"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="Label4" runat="server" Text="Empresa"></asp:Label>
                            </th>
                            <th>
                            </th>
                        </tr>
                        <div id="group">
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </div>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="rows">
                        <td style="text-align: center;">
                            <asp:HiddenField ID="hfVinculoId" Value='<%# Eval("id") %>' runat="server" />
                            <asp:Literal ID="litID" runat="server" Text='<%# Eval("UsuarioId") %>'></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:HiddenField ID="hfUsuarioTipoId" Value='<%# Eval("TipoId") %>' runat="server" />
                            <asp:Literal ID="litNome" runat="server" Text='<%# Eval("UserName") %>'></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:HiddenField ID="hfLinkId" Value='<%# Eval("LinkId") %>' runat="server" />
                            <asp:TextBox ID="txtCNPJ" runat="server" Text='<%# Eval("CNPJ") %>' Style="background-color: transparent;
                                border: 0 none; height: 100%; width: 100%;" OnTextChanged="txtCNPJ_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtCnpj_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                Enabled="True" TargetControlID="txtCNPJ" Mask="99,999,999/9999-99" ClearMaskOnLostFocus="false">
                            </asp:MaskedEditExtender>
                        </td>
                        <td style="text-align: center;">
                            <asp:Literal ID="litEmpresa" runat="server" Text='<%# Eval("Empresa") %>'></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:LinkButton ID="lbtnExcluir" runat="server"
                                ToolTip="Excluir registro." OnClick="lbtnExcluir_Click" CommandArgument='<%# Eval("id") %>'>
                                <asp:Image runat="server" ImageUrl="~/Content/delete.png" />
                                </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </center>
    </ContentTemplate>
</asp:UpdatePanel>
