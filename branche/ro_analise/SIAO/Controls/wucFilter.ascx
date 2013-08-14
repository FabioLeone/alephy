<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucFilter.ascx.cs" Inherits="SIAO.Controls.wucFilter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<div id="dvFilter" runat="server">
    <fieldset style="width: 96.2%;">
        <legend>
            <h2>
                Filtro</h2>
        </legend>
        <div id="dvFiltro" style="height: 30px; width: 100%; display: table-row;">
            <div id="dvRedes" runat="server" style="width: 50%; display: table-cell;">
                Redes:<asp:DropDownList ID="ddlRedesRelatorios" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRedesRelatorios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div id="dvLoja" runat="server" style="display: table-cell;">
                Loja:
                <asp:DropDownList ID="ddlLojaRelatorios" runat="server" Style="min-width: 200px;">
                </asp:DropDownList>
            </div>
        </div>
        <div style="display: table-row; width: 100%;">
            <div style="display: table-cell;">
                <asp:RadioButton ID="rbtPeriodo" GroupName="filtro" Text="período" runat="server"
                    AutoPostBack="true" OnCheckedChanged="rbtPeriodo_CheckedChanged" />
                de:
                <asp:TextBox ID="txtInicio" runat="server" Width="48px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtInicio_MaskedEditExtender" runat="server" CultureDateFormat="MM/yyyy"
                    Enabled="True" TargetControlID="txtInicio" Mask="99/9999" ClearMaskOnLostFocus="false">
                </asp:MaskedEditExtender>
                até:
                <asp:TextBox ID="txtFim" runat="server" Width="48px"></asp:TextBox>
                <asp:MaskedEditExtender ID="txtFim_MaskedEditExtender1" runat="server" CultureDateFormat="MM/yyyy"
                    Enabled="True" TargetControlID="txtFim" Mask="99/9999" ClearMaskOnLostFocus="false">
                </asp:MaskedEditExtender>
            </div>
            <div style="display: table-cell;">
                <asp:RadioButton ID="rbtMes" GroupName="filtro" AutoPostBack="true" Text="ultimos 6 meses"
                    runat="server" OnCheckedChanged="rbtMes_CheckedChanged" />
            </div>
        </div>
    </fieldset>
</div>
