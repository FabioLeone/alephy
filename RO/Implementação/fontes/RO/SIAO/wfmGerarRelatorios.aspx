﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
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

        function rsiz(e) {
            var ddl = document.getElementById("<%=ddlLojaRelatorios.ClientID %>");
            var btn = document.getElementById("btnSelect");
            var wth = (ddl.offsetWidth - 18);
            e.style.width = wth + "px";
            btn.style.marginLeft = (wth + 1) + "px";
        }
    </script>
    <div class="ctApp">
        <% if(CheckCss()){ %>
            <div class="hdFilter" style="background: rgba(62, 137, 233, 0.25);">
        <% }else{ %>
            <div class="hdFilter">
        <% } %>
            <h2>
                Filtro</h2>
            <div id="dvFiltro" runat="server" style="height: 119px;">
                <div id="dvRedes" runat="server" style="width: 99.1%;">
                    Redes:<asp:DropDownList ID="ddlRedesRelatorios" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRedesRelatorios_SelectedIndexChanged" Width="81%">
                    </asp:DropDownList>
                </div>
                <div id="dvUF" runat="server">
                    UF:<asp:DropDownList ID="ddlUF" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUF_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div id="dvCity" runat="server">
                    Cidade:<asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" Width="72%" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div id="dvLoja" runat="server" style="width: 99.45%;">
                    Loja:
                    <input type="text" id="txtCnpj" class="txt_search" onfocus="rsiz(this)" />
                    <input type="button" id="btnSelect" class="btnsearch" onclick="return btnSelect_onclick()" />
                    <asp:DropDownList ID="ddlLojaRelatorios" runat="server" Style="width: 85%;" OnSelectedIndexChanged="ddlLojaRelatorios_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:CheckBox ID="cbxSum" runat="server" Text="Soma" ToolTip="Consolida os valores dos relarórios caso a opção selecionada for 'Todas'" />
                </div>
            </div>
        </div>
        <% if(CheckCss()){ %>
            <div id="divForm" class="dvList" style="background: rgba(62, 137, 233, 0.25);width: 49.75%;margin-left: 0.5%;">
        <% }else{ %>
            <div id="divForm" class="dvList" style="margin-left: 0.5%;">
        <% } %>
            <h2>
                Relatórios</h2>
            <ul class="iconList">
                <li id="lm1" runat="server">
                    <asp:LinkButton ID="lbtnRel1" runat="server" OnClick="btnAdm_Click" OnClientClick="ToggleCursor(1);">
                        <div id="M1" runat="server" class="imgRel">
                            <p>
                                Modelo 1</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="lm2" runat="server">
                    <asp:LinkButton ID="lbtnRel2" runat="server" OnClick="btnRelat2_Click" OnClientClick="ToggleCursor(1);">
                        <div id="M2" runat="server" class="imgRel">
                            <p>
                                Modelo 2</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="li3" runat="server">
                    <asp:LinkButton ID="lbtnParticipation" runat="server" 
                        OnClientClick="ToggleCursor(1);" onclick="lbtnParticipation_Click">
                        <div id="Div5" runat="server" class="imgRel">
                            <p>Porcentagem de Participação</p>
                        </div>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <% if(CheckCss()){ %>
            <div id="div7" class="dvList" style="margin-left: 0.5%; width: 49.75%;margin-right:0; background: rgba(62, 137, 233, 0.25);">
        <% }else{ %>
            <div id="divGrafic" class="dvList" style="margin-left: 0.5%; margin-right: 0.5%;">
        <% } %>
            <h2>
                Gráficos</h2>
            <ul class="iconList">
                <li id="lg1" runat="server">
                    <asp:LinkButton ID="lbtnGra1" runat="server" OnClick="lbtnGra1_Click" OnClientClick="ToggleCursor(1);">
                        <div id="G1" runat="server" class="imgGra">
                            <p>
                                Gráfico 1</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="lg2" runat="server">
                    <asp:LinkButton ID="lbtnGra2" runat="server" OnClick="lbtnGra2_Click" OnClientClick="ToggleCursor(1);">
                        <div id="G2" runat="server" class="imgGra">
                            <p>
                                Gráfico 2</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="lg3" runat="server">
                    <asp:LinkButton ID="lbtnGra3" runat="server" OnClick="lbtnGra3_Click" OnClientClick="ToggleCursor(1);">
                        <div id="P1" runat="server" class="imgGra">
                            <p>
                                Gráfico 3</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="lg4" runat="server">
                    <asp:LinkButton ID="lbtnGra4" runat="server" OnClick="lbtnGra4_Click" OnClientClick="ToggleCursor(1);">
                        <div id="Div3" runat="server" class="imgGra">
                            <p>
                                Gráfico de desconto</p>
                        </div>
                    </asp:LinkButton>
                </li>
                <li id="lg5" runat="server">
                    <asp:LinkButton ID="lbtnCross24" runat="server" OnClick="lbtnCross24_Click" OnClientClick="ToggleCursor(1);">
                        <div id="Div6" runat="server" class="imgGra">
                            <p>Gráfico comparativo</p>
                        </div>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div id="dvFilter" runat="server" class="hdFilter hd0">
            <h2>Período</h2>
            <div class="hdRange">
                <div class="hdR1">
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
                <div class="hdR2">
                    <asp:RadioButton ID="rbtUAno" GroupName="filtro" AutoPostBack="true" Text="ultimo ano"
                        runat="server" OnCheckedChanged="rbtMes_CheckedChanged" />
                </div>
                <div class="hdR3">
                    <asp:RadioButton ID="rbtMes" GroupName="filtro" AutoPostBack="true" Text="ultimos 6 meses"
                        runat="server" OnCheckedChanged="rbtMes_CheckedChanged" />
                </div>
                <div class="hdR4">
                    <asp:RadioButton ID="rbtUMes" GroupName="filtro" AutoPostBack="true" Text="ultimo mês"
                        runat="server" OnCheckedChanged="rbtMes_CheckedChanged"/>
                </div>
            </div>
        </div>
        <div id="dvInf" runat="server" class="hdFilter hdInfo">
            <h2>Informações loja</h2>
            <div class="inf">
                <asp:Literal ID="sInf" runat="server"></asp:Literal>
            </div>
        </div>
        <div id="dvAn" runat="server" class="dvList">
            <h2>
                Análise</h2>
            <ul class="iconList">
                <li id="la1" runat="server">
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
