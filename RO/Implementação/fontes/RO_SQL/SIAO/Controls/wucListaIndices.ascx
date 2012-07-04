<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListaIndices.ascx.cs"
    Inherits="SIAO.Controls.wucListaIndices" %>
<%@ Register src="wucCadastroIndices.ascx" tagname="wucCadastroIndices" tagprefix="uc1" %>
<asp:MultiView ID="mvwIndices" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwIndicesList" runat="server">
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="lblGrupo" runat="server" Text="Grupo" AssociatedControlID="ddlGrupos"></asp:Label>
                    <asp:DropDownList ID="ddlGrupos" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblCategoria" runat="server" AssociatedControlID="ddlCategorias" Text="Categoria"></asp:Label>
                    <asp:DropDownList ID="ddlCategorias" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar Todos" CssClass="rightButton gray"
                        OnClick="btnMostrarTodos_Click" />
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CssClass="LeftButton gray"
                        OnClick="btnFiltro_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:GridView ID="gvwIndices" AutoGenerateColumns="false" DataKeyNames="id" EmptyDataText="Registros não encontrados"
                        OnRowDeleting="gvwIndices_OnRowDeleting" OnRowEditing="gvwIndices_OnRowEditing"
                        Width="100%" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Grupo" SortExpression="grupo">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlgrupo" Text='<%# Eval("grupo") %>' runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Categoria" SortExpression="categoria">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlCategoria" Text='<%# Eval("categoria") %>' runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Venda" SortExpression="venda" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlVenda" Text='<%# Eval("venda") %>' runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desconto" SortExpression="desconto" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlDesconto" Text='<%# Eval("desconto") %>' runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNovo" runat="server" CssClass="button gray" Text="Novo" OnClick="btnNovo_Click" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="vwIndicesCadastro" runat="server">
            <uc1:wucCadastroIndices ID="wucCadastroIndices1" runat="server" />
    </asp:View>
</asp:MultiView>