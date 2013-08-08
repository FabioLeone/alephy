<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
            <script type="text/javascript">
                $(document).ready(function () {
                    $("section").addClass("sectionb");
                });
            </script>
            <table style="width: 100%; margin-left: 0.25%;">
                <tr id="trFiltro" runat="server">
                    <td colspan="2">
                        <fieldset style="width: 96.5%;">
                            <legend>
                                <h2>Filtro</h2>
                            </legend>
                            <div id="dvFiltro" style="height: 30px; width: 100%;display:table-row;">
                                <div id="dvRedes" runat="server" style="width:50%;display:table-cell;">
                                    Redes:<asp:DropDownList ID="ddlRedesRelatorios" runat="server" AutoPostBack="true"
                                        onselectedindexchanged="ddlRedesRelatorios_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="dvLoja" runat="server" style="display:table-cell;">
                                    Loja:
                                    <asp:DropDownList ID="ddlLojaRelatorios" runat="server" style="min-width:200px;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div style="display:table-row;width:100%;">
                                <div style="display:table-cell;">
                                <asp:RadioButton ID="rbtPeriodo" GroupName="filtro" Text="período" runat="server" 
                                            AutoPostBack="true" oncheckedchanged="rbtPeriodo_CheckedChanged" />
                                        de: <asp:TextBox ID="txtInicio" runat="server" Width="48px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtInicio_MaskedEditExtender" runat="server" CultureDateFormat="MM/yyyy"
                                    Enabled="True" TargetControlID="txtInicio" Mask="99/9999" ClearMaskOnLostFocus="false">
                                </asp:MaskedEditExtender>
                                        até: <asp:TextBox ID="txtFim" runat="server" Width="48px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtFim_MaskedEditExtender1" runat="server" CultureDateFormat="MM/yyyy"
                                    Enabled="True" TargetControlID="txtFim" Mask="99/9999" ClearMaskOnLostFocus="false">
                                    </asp:MaskedEditExtender>
                                </div>
                                <div style="display:table-cell;">
                                <asp:RadioButton ID="rbtMes" GroupName="filtro" AutoPostBack="true" 
                                    Text="ultimos 6 meses" runat="server" 
                                    oncheckedchanged="rbtMes_CheckedChanged" />
                                </div>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%;">
                        <fieldset style="width: 93%;">
                            <legend>
                                <h2>
                                    Relatórios</h2>
                            </legend>
                            <div id="divForm" style="height: 300px; width: 100%;">
                                    <ul class="iconList">
                                        <li>
                                            <p id="M1" runat="server" class="imgBtn">
                                                <asp:ImageButton ID="ibtnRelat1" runat="server" OnClick="btnAdm_Click" AlternateText="Modelo1"
                                                    ImageUrl="~/Content/document.png" ToolTip="Modelo1" />
                                                <asp:Label ID="lblRelat1" runat="server" AssociatedControlID="ibtnRelat1">Modelo1</asp:Label>
                                            </p>
                                        </li>
                                        <li>
                                            <p id="M2" runat="server" class="imgBtn">
                                                <asp:ImageButton ID="ibtnRelat2" runat="server" OnClick="btnRelat2_Click" AlternateText="Modelo2"
                                                    ImageUrl="~/Content/document.png" ToolTip="Modelo2" />
                                                <asp:Label ID="Label1" runat="server" AssociatedControlID="ibtnRelat2">Modelo2</asp:Label>
                                            </p>
                                        </li>
                                    </ul>
                            </div>
                        </fieldset>
                    </td>
                    <td style="width: 50%;">
                        <fieldset style="width: 93%;">
                            <legend>
                                <h2>Gráficos</h2>
                            </legend>
                            <div id="divGrafic" style="height: 300px; width: 100%;">
                                <ul class="iconList">
                                    <li>
                                        <p id="G1" runat="server" class="imgBtn">
                                            <asp:ImageButton ID="ibtnGrafic1" runat="server" AlternateText="Grafico1" ImageUrl="~/Content/graphic.png"
                                                ToolTip="Grafico1" OnClick="ibtnGrafic1_Click" />
                                            <asp:Label ID="lblGrafic1" runat="server" AssociatedControlID="ibtnGrafic1">Grafico1</asp:Label>
                                        </p>
                                    </li>
                                    <li>
                                        <p id="G2" runat="server" class="imgBtn">
                                            <asp:ImageButton ID="ibtnGrafic2" runat="server" AlternateText="Grafico2" ImageUrl="~/Content/graphic.png"
                                                ToolTip="Grafico2" OnClick="ibtnGrafic2_Click" />
                                            <asp:Label ID="lblGrafic2" runat="server" AssociatedControlID="ibtnGrafic2">Grafico2</asp:Label>
                                        </p>
                                    </li>
                                    <li>
                                        <p id="P1" runat="server" class="imgBtn">
                                            <asp:ImageButton ID="ibtnGrafic3" runat="server" AlternateText="Grafico3" ImageUrl="~/Content/graphic.png"
                                                ToolTip="Grafico3" onclick="lblGrafic3_Click" />
                                            <asp:Label ID="Label2" runat="server" AssociatedControlID="ibtnGrafic3">Grafico3</asp:Label>
                                        </p>
                                    </li>
                                </ul>
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
