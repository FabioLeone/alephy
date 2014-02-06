<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function btnSelect_onclick() {
            var txt = document.getElementById("txtCnpj");
            var ddl = document.getElementById("<%=ddlLojaRelatorios.ClientID %>");

            if (ddl.options.length > 0) {
                for (var i = 0; i < ddl.options.length; i++) {
                    if (ddl.options[i].value.toLowerCase().indexOf(txt.value) != -1) {
                        ddl.selectedIndex = i;
                        txt.value = "";
                        break;
                    }
                }
            } else { 
                txt.value = "selecione uma rede";
            }
        }
    </script>
    <div class="hdFilter">
        <h2>
            Filtro</h2>
        <div id="dvFiltro" runat="server">
            <div id="dvRedes" runat="server">
                Redes:<asp:DropDownList ID="ddlRedesRelatorios" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlRedesRelatorios_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div id="dvLoja" runat="server">
                Loja:
                <input type="text" id="txtCnpj" class="txt_search" />
                <input type="button" id="btnSelect" class="btnsearch" onclick="return btnSelect_onclick()" />
                <asp:DropDownList ID="ddlLojaRelatorios" runat="server" Style="min-width: 243px;">
                </asp:DropDownList>
            </div>
        </div>
        <div>
            <div>
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
            <div>
                <asp:RadioButton ID="rbtMes" GroupName="filtro" AutoPostBack="true" Text="ultimos 6 meses"
                    runat="server" OnCheckedChanged="rbtMes_CheckedChanged" />
            </div>
        </div>
    </div>
    <div class="ctApp">
        <div id="divForm" class="dvList">
            <h2>
                Relatórios</h2>
            <ul class="iconList">
                <li>
                    <asp:LinkButton ID="lbtnRel1" runat="server" OnClick="btnAdm_Click" OnClientClick="ToggleCursor(1);">
                        <div id="M1" runat="server" class="imgRel">
                            <p>
                                Modelo 1</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnRel2" runat="server" OnClick="btnRelat2_Click" OnClientClick="ToggleCursor(1);">
                        <div id="M2" runat="server" class="imgRel">
                            <p>
                                Modelo 2</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnGroup" runat="server" OnClick="lbtnGroup_Click" OnClientClick="ToggleCursor(1);" Visible="false">
                        <div id="Div4" runat="server" class="imgRel">
                            <p>
                                Consolidado</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnParticipation" runat="server" 
                        OnClientClick="ToggleCursor(1);" onclick="lbtnParticipation_Click">
                        <div id="Div5" runat="server" class="imgRel">
                            <p>Porcentagem de Participação</p>
                        </div>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="divGrafic" class="dvList" style="margin-left: 0.5%; margin-right: 0.5%;">
            <h2>
                Gráficos</h2>
            <ul class="iconList">
                <li>
                    <asp:LinkButton ID="lbtnGra1" runat="server" OnClick="lbtnGra1_Click" OnClientClick="ToggleCursor(1);">
                        <div id="G1" runat="server" class="imgGra">
                            <p>
                                Gráfico 1</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnGra2" runat="server" OnClick="lbtnGra2_Click" OnClientClick="ToggleCursor(1);">
                        <div id="G2" runat="server" class="imgGra">
                            <p>
                                Gráfico 2</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnGra3" runat="server" OnClick="lbtnGra3_Click" OnClientClick="ToggleCursor(1);">
                        <div id="P1" runat="server" class="imgGra">
                            <p>
                                Gráfico 3</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li>
                    <asp:LinkButton ID="lbtnGra4" runat="server" OnClick="lbtnGra4_Click" OnClientClick="ToggleCursor(1);">
                        <div id="Div3" runat="server" class="imgGra">
                            <p>
                                Gráfico de desconto</p>
                        </div>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="div1" class="dvList">
            <h2>
                Análise</h2>
            <ul class="iconList">
                <li>
                    <asp:LinkButton ID="lbtnAna1" runat="server" OnClick="lbtnAna1_Click" OnClientClick="ToggleCursor(1);"
                        ToolTip="Os dados exibidos neste relatório, são referentes apenas ao último mês do período selecionado.">
                        <div id="Div2" runat="server" class="imgAna">
                            <p>
                                Análise</p>
                        </div>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>
    <rsweb:ReportViewer ID="rv" runat="server" Visible="false">
    </rsweb:ReportViewer>
</asp:Content>
