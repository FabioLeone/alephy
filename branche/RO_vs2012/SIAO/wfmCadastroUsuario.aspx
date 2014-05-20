<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroUsuario.aspx.cs" Inherits="SIAO.wfmCadastroUsuario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var browserType;

        if (document.layers) { browserType = "nn4" }
        if (document.all) { browserType = "ie" }
        if (window.navigator.userAgent.toLowerCase().match("gecko")) {
            browserType = "gecko"
        }

        function show() {
            if (browserType == "gecko")
                document.poppedLayer = eval('document.getElementById("user")');
            else if (browserType == "ie")
                document.poppedLayer = eval('document.getElementById("user")');
            else
                document.poppedLayer = eval('document.layers["user"]');
            document.poppedLayer.style.visibility = "visible";
        }

        var ddlText, ddlValue, ddl;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=ddlUser.ClientID %>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }

        window.onload = CacheItems;

        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }

        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <div id="edUser">
            <p>
                <input type="button" onclick="show()" value="Editar usuário." class="button gray">
            </p>
            <div id="user" style="visibility: hidden">
                <p>
                    Usuário:
                    <asp:TextBox ID="txtSearch" runat="server" onkeydown="javascript:FilterItems(this.value);"></asp:TextBox><br />
                    <asp:DropDownList ID="ddlUser" runat="server">
                    </asp:DropDownList>
                    <div style="text-align: right">
                        <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="button gray" OnClick="btnEdit_Click" />
                    </div>
                    <p>
                    </p>
                </p>
            </div>
        </div>
        <fieldset style="width: 372px; margin-left: 36%;">
            <legend>
                <h2>
                    Cadastro de Usuários</h2>
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
                            <asp:Button ID="btnSave" CssClass="rightButton gray" ToolTip="Salvar as informações do usuário."
                                runat="server" Text="Salvar" OnClick="btnSave_Click" />
                            <asp:Button ID="btnLimpar" runat="server" Text="Limpar" CssClass="LeftButton gray"
                                ToolTip="Limpar todos os campos" OnClick="btnLimpar_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </asp:Panel>
</asp:Content>
