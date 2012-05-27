<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroUsuario.aspx.cs" Inherits="SIAO.wfmCadastroUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="edUser">
                <p>
                    <input type="button" onClick="show()" value="Editar usuário." class="button gray">
                </p>
                <div id="user" style="visibility:hidden">
                    <p>Usuário: 
                        <asp:DropDownList ID="ddlUser" runat="server">
                        </asp:DropDownList>
                    <div style="text-align:right">
                        <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="button gray" 
                            onclick="btnEdit_Click" />
                    </div>
                        <p>
                        </p>
                    </p>
                </div>
            </div>
            <script type="text/javascript">
                var browserType;

                if (document.layers) { browserType = "nn4" }
                if (document.all) { browserType = "ie" }
                if (window.navigator.userAgent.toLowerCase().match("gecko")) {
                    browserType = "gecko"
                }
                
                function show() {
                    if (browserType == "gecko")
                        document.poppedLayer =
         eval('document.getElementById("user")');
                    else if (browserType == "ie")
                        document.poppedLayer =
        eval('document.getElementById("user")');
                    else
                        document.poppedLayer =
         eval('document.layers["user"]');
                    document.poppedLayer.style.visibility = "visible";
                }
            </script>
            <fieldset style="width: 372px; margin-left: 28%;">
                <legend>
                    <h2>
                        Cadastro de Usuários</h2>
                </legend>
                <div id="divForm" style="height: 356px; width: 366px; margin-left: 24px;">
                    <table>
                        <tr>
                            <td>
                                Farmacia:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFarmacia" runat="server" Width="188px">
                                </asp:DropDownList>
                                <asp:Label ID="lblF" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HyperLink ID="hl" NavigateUrl="~/wfmCadastroLojas.aspx" runat="server">Cadastrar uma nova Farmacia.</asp:HyperLink>
                            </td>
                        </tr>
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
                                <asp:Label ID="lblCS" runat="server" ForeColor="Red"></asp:Label>
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
