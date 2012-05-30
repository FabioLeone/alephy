<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroUsuario.aspx.cs" Inherits="SIAO.wfmCadastroUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="width:352px; margin-left:28%;">
                <legend><h2>
                    Cadastro de Usuários</h2></legend>
            <div id="divForm" style="height: 300px; width: 350px; margin-left: 20px;">
                <table>
                    <tr>
                        <td>
                            Nome:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nome de acesso:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAcsName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Senha:
                        </td>
                        <td>
                            <asp:TextBox ID="txtSenha" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Confirmar senha:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCfrSenha" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Email:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Senha valida até:
                        </td>
                        <td>
                            <asp:TextBox ID="txtValidade" runat="server" ToolTip="Prazo de validade da senha."></asp:TextBox>
                            <asp:CalendarExtender ID="txtValidade_CalendarExtender" runat="server" Enabled="True"
                                TargetControlID="txtValidade" DaysModeTitleFormat="MMMM, yy" TodaysDateFormat="MMMM d, yy"
                                Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rblAccess" runat="server" RepeatDirection="Horizontal" ToolTip="Tipo de acesso."
                                Font-Size="Smaller">
                                <asp:ListItem Value="adm">Administrador</asp:ListItem>
                                <asp:ListItem Value="nvg">Gerente de Rede</asp:ListItem>
                                <asp:ListItem Selected="True" Value="nvp">Propietário</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cbxAtivo" Text="Ativo:" TextAlign="Left" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right;">
                            <asp:Button ID="btnSave" CssClass="button gray" ToolTip="Salvar as informações do usuário."
                                runat="server" Text="Salvar" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
