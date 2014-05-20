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
                    <asp:GridView ID="gvwIndices" AutoGenerateColumns="False" DataKeyNames="id" EmptyDataText="Registros não encontrados"
                        OnRowDeleting="gvwIndices_OnRowDeleting" OnRowEditing="gvwIndices_OnRowEditing"
                        Width="100%" runat="server" Font-Names="verdana" GridLines="None" 
                        HeaderStyle-BackColor="#0e9de3" HeaderStyle-ForeColor="White" CellPadding="4" 
                        ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Grupo" SortExpression="grupo">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlgrupo" runat="server" Text='<%# Eval("grupo") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Categoria" SortExpression="categoria">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlCategoria" runat="server" Text='<%# Eval("categoria") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Venda" ItemStyle-HorizontalAlign="Right" 
                                SortExpression="venda">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlVenda" runat="server" Text='<%# Eval("venda") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Desconto" ItemStyle-HorizontalAlign="Right" 
                                SortExpression="desconto">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlDesconto" runat="server" Text='<%# Eval("desconto") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" ButtonType="Image" ItemStyle-HorizontalAlign="Right" EditImageUrl="~/Content/edit.png" />
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Content/delete.png" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#0d87df" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
        <fieldset style="width:260px; margin:auto;">
            <legend>Novo Indice</legend>
            <uc1:wucCadastroIndices ID="wucCadastroIndices1" runat="server" />
        </fieldset>
    </asp:View>
</asp:MultiView>