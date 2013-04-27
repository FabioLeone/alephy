<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroRede.aspx.cs" Inherits="SIAO.wfmCadastroRede" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
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
                <div style="height: 85px; width: 400px; margin-left: 10px;">
                    <table>
                        <tr>
                            <td>
                                Nome da Rede:
                            </td>
                            <td>
                                <asp:TextBox ID="txtRede" runat="server" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                CNPJ:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCNPJ" runat="server" Width="250px"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtCnpj_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" TargetControlID="txtCNPJ" Mask="99,999,999/9999-99" ClearMaskOnLostFocus="false">
                                </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right;">
                                <asp:Button ID="btnSave" CssClass="rightButton gray" ToolTip="Salvar as informações da rede."
                                    runat="server" Text="Salvar" OnClick="btnSave_Click" />
                                <asp:Button ID="btnLimpar" CssClass="LeftButton gray" ToolTip="Limpa todos os campos" runat="server" 
                                Text="Limpar" onclick="btnLimpar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
