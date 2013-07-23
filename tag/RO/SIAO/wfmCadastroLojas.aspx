<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmCadastroLojas.aspx.cs" Inherits="SIAO.wfmCadastroLojas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 99%;">
                <tr>
                    <td>
                        <div id="loja">
                            Redes:<asp:DropDownList ID="ddlEdRedes" Width="220px" runat="server">
                            </asp:DropDownList>
                            <asp:Button ID="btnLoadLoja" runat="server" Text=">" CssClass="button gray" 
                                onclick="btnLoadLoja_Click" />
                            &nbsp;
                            Loja:<asp:DropDownList ID="ddlLoja" Width="220px" runat="server" EnableViewState="true">
                            </asp:DropDownList>
                            <asp:Button ID="btnEdit" runat="server" Text="Editar" CssClass="button gray" OnClick="btnEdit_Click" />
                        </div>
                    </td>
                </tr>
            </table>
            <fieldset style="width: 96.5%;">
                <legend>
                    <h2>
                        Cadastro de Lojas</h2>
                </legend>
                <div id="divForm" style="min-height: 330px; width: 100%;">
                    <table>
                        <tr>
                            <td style="width: 140px; text-align: right;">
                                Nome Fantasia:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="255px" ID="txtNomeFantasia" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Razão:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="255px" ID="txtRazao" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Proprietário:
                            </td>
                            <td colspan="2">
                                <asp:TextBox width="255px" ID="txtProprietario" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Gerente:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="255px" ID="txtGerente" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Email:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="200px" ID="txtEmail" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Email 2:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="200px" ID="txtEmail2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Cnpj:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCnpj" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="txtCnpj_MaskedEditExtender" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" TargetControlID="txtCnpj" Mask="99,999,999/9999-99" ClearMaskOnLostFocus="false">
                                </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Endereço:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="255px" ID="txtEndereco" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Nº:
                            </td>
                            <td>
                                <asp:TextBox ID="txtNum" Width="100px" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Comp.:
                            </td>
                            <td>
                                <asp:TextBox Width="100px" ID="txtComp" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;">
                                CEP:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCep" runat="server"></asp:TextBox>
                                <asp:MaskedEditExtender ID="meeCEP" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" TargetControlID="txtCEP" Mask="99999-999" ClearMaskOnLostFocus="false">
                                </asp:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Bairro:
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtBairro" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Cidade:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCidade" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                UF:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlUf" Width="60px" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Telefone:
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtFone" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 100px; text-align: right;">
                                Telefone 2:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFone2" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Celular:
                            </td>
                            <td>
                                <asp:TextBox ID="txtCel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Site:
                            </td>
                            <td colspan="2">
                                <asp:TextBox Width="200px" ID="txtSite" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Skype:
                            </td>
                            <td>
                                <asp:TextBox ID="txtSkype" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                Msn:
                            </td>
                            <td>
                                <asp:TextBox ID="txtMsn" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Rede:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRede" Width="255px" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" style="text-align: right;">
                                <asp:CheckBox ID="cbxAtivo" Text="Ativo:" TextAlign="Left" runat="server" Checked="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: right;">
                                <asp:Button ID="btnSave" CssClass="rightButton gray" ValidationGroup="Cadastro" ToolTip="Salvar as informações da loja."
                                    runat="server" Text="Salvar" OnClick="btnSave_Click" />
                                <asp:Button ID="btnLimpar" CssClass="LeftButton gray" runat="server" Text="Limpar"
                                    ToolTip="Limpa todos os campos" OnClick="btnLimpar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <asp:ValidationSummary ID="vsCadastro" ValidationGroup="Cadastro" CssClass="vsError"
                runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
