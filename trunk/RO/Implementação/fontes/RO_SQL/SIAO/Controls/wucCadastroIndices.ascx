<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCadastroIndices.ascx.cs" Inherits="SIAO.Controls.wucCadastroIndices" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<table>
    <tr>
        <td>
            <asp:Label ID="lblGrupo" runat="server" Text="Grupo" AssociatedControlID="txtGrupo"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvGrupo" runat="server" ErrorMessage="Selecione ou digite o grupo."
            ControlToValidate="txtGrupo" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:TextBox ID="txtGrupo" runat="server"></asp:TextBox>
            <asp:DropDownExtender ID="txtGrupo_DropDownExtender" runat="server" 
                DropDownControlID="lbxGrupo" HighlightBackColor="WhiteSmoke" Enabled="True" TargetControlID="txtGrupo"></asp:DropDownExtender>
            <asp:ListBox ID="lbxGrupo" runat="server" AutoPostBack="true" 
                onselectedindexchanged="lbxGrupo_SelectedIndexChanged"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCategoria" runat="server" Text="Categoria" AssociatedControlID="txtCategoria"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" 
            ErrorMessage="Selecione a categoria." ControlToValidate="txtCategoria" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:TextBox ID="txtCategoria" runat="server"></asp:TextBox>
            <asp:DropDownExtender ID="txtCategoria_DropDownExtender" runat="server" DropDownControlID="lbxCategoria" 
                 Enabled="True" HighlightBackColor="WhiteSmoke" TargetControlID="txtCategoria">
            </asp:DropDownExtender>
            <asp:ListBox ID="lbxCategoria" runat="server" 
                onselectedindexchanged="lbxCategoria_SelectedIndexChanged"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblVenda" runat="server" Text="Venda" AssociatedControlID="txtVenda"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvVenda" runat="server" ErrorMessage="Entre com a porcentagem referente a venda."
            ControlToValidate="txtVenda" ValidationGroup="Cadastro">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revValor" ControlToValidate="txtVenda" ValidationExpression="[0-9]+(\,[0-9][0-9])?$" 
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
            ValidationExpression="[0-9]+(\,[0-9][0-9])?$" 
            ValidationGroup="Cadastro" runat="server" ErrorMessage="Apenas números">*</asp:RegularExpressionValidator>
            <asp:TextBox ID="txtDesconto" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSalvar" CssClass="button gray" runat="server" Text="Salvar" 
                onclick="btnSalvar_Click" ValidationGroup="Cadastro" />
        </td>
    </tr>
</table>
<asp:ValidationSummary ID="vsIndices" ValidationGroup="Cadastro" CssClass="vsError" runat="server" />
