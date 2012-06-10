<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroRede.aspx.cs" Inherits="SIAO.wfmCadastroRede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="edRede">
                <p>
                    <input type="button" onclick="show()" value="Editar rede." class="button gray">
                </p>
                <div id="rede" style="visibility: hidden">
                    <p>
                        Rede:
                        <asp:DropDownList ID="ddlRede" Width="220px" runat="server" EnableViewState="true">
                        </asp:DropDownList>
                        <div style="text-align: right">
                            <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="button gray" OnClick="btnEdit_Click" />
                        </div>
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
         eval('document.getElementById("rede")');
                    else if (browserType == "ie")
                        document.poppedLayer =
        eval('document.getElementById("rede")');
                    else
                        document.poppedLayer =
         eval('document.layers["rede"]');
                    document.poppedLayer.style.visibility = "visible";
                }
            </script>
            <fieldset style="width: 402px; margin-left: 26%;">
                <legend>
                    <h2>
                        Cadastro de Redes</h2>
                </legend>
                <div style="height: 126px; width: 400px; margin-left: 10px;">
                    <table>
                        <tr>
                            <td>
                                Gerente da Rede:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlGerente" Width="254px" runat="server" EnableViewState="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HyperLink ID="hl" NavigateUrl="~/wfmCadastroUsuario.aspx" runat="server">Cadastrar um novo Gerente.</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nome da Rede:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRede" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <asp:Button ID="btnSave" CssClass="button gray" ToolTip="Salvar as informações da rede."
                                    runat="server" Text="Salvar" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
