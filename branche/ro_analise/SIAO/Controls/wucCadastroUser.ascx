<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCadastroUser.ascx.cs"
    Inherits="SIAO.Controls.wucCadastroUser" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Panel ID="Panel1" runat="server">
    <fieldset style="width: 372px;margin: 0 auto;">
        <legend>
            <h2>Cadastro de Usuários</h2>
        </legend>
        <div id="divForm" style="height: 288px; width: 366px; margin-left: 24px;">
            <table>
                <tr>
                    <td>
                        Nome:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" Width="188px"></asp:TextBox>
                        <asp:Label ID="lblN" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nome de acesso:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAcsName" runat="server" Width="188px"></asp:TextBox>
                        <asp:Label ID="lblAN" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Senha:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" Width="188px"></asp:TextBox>
                        <asp:Label ID="lblS" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirmar senha:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCfrSenha" runat="server" TextMode="Password" Width="188px"></asp:TextBox>
                        <asp:Label ID="lblCS" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="188px"></asp:TextBox>
                        <asp:Label ID="lblE" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Senha valida até:
                    </td>
                    <td>
                        <asp:TextBox ID="txtValidade" runat="server" ToolTip="Prazo de validade da senha."
                            Width="188px"></asp:TextBox>
                        <asp:Label ID="lblV" runat="server" ForeColor="Red"></asp:Label>
                        <asp:CalendarExtender ID="txtValidade_CalendarExtender" runat="server" Enabled="True"
                            TargetControlID="txtValidade" DaysModeTitleFormat="MMMM, yy" TodaysDateFormat="MMMM d, yy"
                            Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo de Acesso:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAcess" runat="server" ToolTip="Tipo de acesso." Width="194px"></asp:DropDownList>
                        <asp:Label ID="lblAcs" runat="server" ForeColor="Red"></asp:Label>
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
                            runat="server" Text="Salvar" onclick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
</asp:Panel>
