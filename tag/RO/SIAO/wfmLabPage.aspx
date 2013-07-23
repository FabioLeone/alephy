<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmLabPage.aspx.cs" Inherits="SIAO.wfmLabPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="pnl">
        <table style="width: 100%;">
            <tr>
                <td style="width: 49%">
                    <asp:DropDownList ID="ddlRede" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvRede" ControlToValidate="ddlRede" ForeColor="Red"
                        ValidationGroup="Filtro" runat="server" ErrorMessage="Selecione a rede">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 49%">
                    <asp:DropDownList ID="ddlAno" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblSelecioneGrupo" runat="server" Text="Selecione o Grupo"></asp:Label>
                        </legend>
                        <div class="CheckBoxDiv">
                            <asp:CheckBoxList ID="cblGrupos" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </fieldset>
                </td>
                <td>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblCategoria" runat="server" Text="Selecione o Subgrupo"></asp:Label>
                        </legend>
                        <div class="CheckBoxDiv">
                            <asp:CheckBoxList ID="cblCategoria" runat="server">
                            </asp:CheckBoxList>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right">
                    <asp:Button ID="btnGerarGrafico" runat="server" CssClass="button gray" ToolTip="Gerar grafico"
                        ValidationGroup="Filtro" Text="Gerar Grafico" OnClick="btnGerarGrafico_Click" />
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="vsFiltro" ValidationGroup="Filtro" CssClass="vsError"
            runat="server" />
    </asp:Panel>
</asp:Content>
