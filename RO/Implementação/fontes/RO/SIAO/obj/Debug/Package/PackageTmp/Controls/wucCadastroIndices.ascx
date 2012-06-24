<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCadastroIndices.ascx.cs" Inherits="SIAO.Controls.wucCadastroIndices" %>

<table>
    <tr>
        <td>
            <asp:Label ID="lblGrupo" runat="server" Text="Grupo" AssociatedControlID="ddlGrupo"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ErrorMessage="Selecione o grupo."
            ControlToValidate="ddlGrupo" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlGrupo" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCategoria" runat="server" Text="Categoria" AssociatedControlID="ddlCategoria"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" 
            ErrorMessage="Selecione a categoria." ControlToValidate="ddlCategoria" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCadastro" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblVenda" runat="server" Text="Venda" AssociatedControlID="txtVenda"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvVenda" runat="server" ErrorMessage="Entre com a porcentagem referente a venda."
            ControlToValidate="txtVenda" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revValor" ControlToValidate="txtValor" ValidationExpression="^\$[0-9]+(\.[0-9][0-9])?$" 
            ValidationGroup="Cadastro" runat="server" ErrorMessage="Apenas números">*</asp:RegularExpressionValidator>
            <asp:TextBox ID="txtVenda" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblDesconto" runat="server" Text="Desconto"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvDesconto" runat="server" 
            ErrorMessage="Entre com a pocentagem referente ao desconto" ControlToValidate="txtDesconto"
            ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDesconto" ControlToValidate="txtDesconto" 
            ValidationExpression="^\$[0-9]+(\.[0-9][0-9])?$" 
            ValidationGroup="Cadastro" runat="server" ErrorMessage="Apenas números">*</asp:RegularExpressionValidator>
            <asp:TextBox ID="txtDesconto" runat="server"></asp:TextBox>
        </td>
    </tr>
</table>