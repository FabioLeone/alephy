<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListaIndices.ascx.cs"
    Inherits="SIAO.Controls.wucListaIndices" %>
<table>
    <tr>
        <td>
            <asp:Label ID="lblCategoria" runat="server" AssociatedControlID="ddlCategorias" Text="Categoria"></asp:Label>
            <asp:DropDownList ID="ddlCategorias" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label ID="lblGrupo" runat="server" Text="Grupo" AssociatedControlID="ddlGrupos"></asp:Label>
            <asp:DropDownList ID="ddlGrupos" runat="server">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" CssClass="leftButton gray" />
            <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar Todos" CssClass="rightButton gray" />
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
                    <asp:TemplateField HeaderText="Venda" SortExpression="venda">
                        <ItemTemplate>
                            <asp:Literal ID="ltlVenda" Text='<%# Eval("venda") %>'
                                runat="server"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Desconto" SortExpression="desconto">
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
</table>
