<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucListaUsers.ascx.cs" Inherits="SIAO.Controls.wucListaUsers" %>
<%@ Register src="wucCadastroUser.ascx" tagname="wucCadastroUser" tagprefix="uc1" %>
<asp:MultiView ID="mvwUsers" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwUserList" runat="server">
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Label ID="lblNome" runat="server" Text="Nome" AssociatedControlID="txtNome"></asp:Label>
                    <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnMostrarTodos" runat="server" Text="Mostrar Todos" 
                        CssClass="rightButton gray" onclick="btnMostrarTodos_Click" />
                    <asp:Button ID="btnFiltro" runat="server" Text="Filtrar" 
                        CssClass="LeftButton gray" onclick="btnFiltro_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvwUsers" AutoGenerateColumns="False" DataKeyNames="UserId" EmptyDataText="Registros não encontrados"
                        Width="948px" runat="server" Font-Names="verdana" GridLines="None" 
                        HeaderStyle-BackColor="#0e9de3" HeaderStyle-ForeColor="White" CellPadding="2" 
                        ForeColor="#333333" onrowdeleting="gvwUsers_RowDeleting" 
                        onrowediting="gvwUsers_RowEditing" onrowdatabound="gvwUsers_RowDataBound" 
                        Font-Size="Small" AllowPaging="true" 
                        EnableSortingAndPagingCallbacks="True" 
                        onpageindexchanging="gvwUsers_PageIndexChanging" PageSize="15">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Nome" SortExpression="UserName">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlName" runat="server" Text='<%# Eval("UserName") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="E-mail" SortExpression="Email">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlTipo" runat="server" Text='<%# Eval("Tipo") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acesso até" SortExpression="ExpirationDate">
                                <ItemTemplate>
                                   <asp:Literal ID="ltlExpirationDate" runat="server" 
                                        Text='<%# ((DateTime)Eval("ExpirationDate")).ToShortDateString() %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ativo" SortExpression="Inactive">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlInactive" runat="server" Text='<%# (Boolean)Eval("Inactive") == false? "SIM": "NÂO" %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" ButtonType="Image" 
                                ItemStyle-HorizontalAlign="Right" EditImageUrl="~/Content/edit.png" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" DeleteImageUrl="~/Content/delete.png">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:CommandField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <HeaderStyle BackColor="#0d87df" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#0d87df" ForeColor="White" HorizontalAlign="Center" />
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
                    <asp:Button ID="btnNovo" runat="server" CssClass="button gray" Text="Novo" 
                        onclick="btnNovo_Click" />
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="vwUserCadastro" runat="server">
        <uc1:wucCadastroUser ID="wucCadastroUser1" runat="server" />
    </asp:View>
</asp:MultiView>