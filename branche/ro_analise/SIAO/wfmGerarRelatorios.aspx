﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="hdFilter">
                <h2>
                    Filtro</h2>
                <div id="dvFiltro" runat="server">
                    <div id="dvRedes" runat="server">
                        Redes:<asp:DropDownList ID="ddlRedesRelatorios" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlRedesRelatorios_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div id="dvLoja" runat="server" style="display: block; float:left;">
                        Loja:
                        <asp:DropDownList ID="ddlLojaRelatorios" runat="server" Style="min-width: 243px;">
                        </asp:DropDownList>
                    </div>
                </div>
                <div>
                    <div style="display: block; float:left;">
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
                    <div style="display: block; float:left;">
                        <asp:RadioButton ID="rbtMes" GroupName="filtro" AutoPostBack="true" Text="ultimos 6 meses"
                            runat="server" OnCheckedChanged="rbtMes_CheckedChanged" />
                    </div>
                </div>
            </div>
            <div style="vertical-align:top; display: inline-block;width: 100% !important;margin-top: 3px;">
                <div id="divForm" style="width: 33%;background: rgba(62, 137, 233, 0.3);display: block;float: left;">
                    <h2>
                        Relatórios</h2>
                    <ul class="iconList">
                        <li>
                            <asp:LinkButton ID="lbtnRel1" runat="server" OnClick="btnAdm_Click">
                                <div id="M1" runat="server" class="imgRel">
                                    <p>
                                        Modelo 1</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtnRel2" runat="server" OnClick="btnRelat2_Click">
                                <div id="M2" runat="server" class="imgRel">
                                    <p>
                                        Modelo 2</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div id="divGrafic" style="width: 33%;background: rgba(62, 137, 233, 0.3);display: block;float: left;margin: 0 3px;">
                    <h2>
                        Gráficos</h2>
                    <ul class="iconList">
                        <li>
                            <asp:LinkButton ID="lbtnGra1" runat="server" OnClick="lbtnGra1_Click">
                                <div id="G1" runat="server" class="imgGra">
                                    <p>
                                        Grafico 1</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtnGra2" runat="server" OnClick="lbtnGra2_Click">
                                <div id="G2" runat="server" class="imgGra">
                                    <p>
                                        Grafico 2</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtnGra3" runat="server" OnClick="lbtnGra3_Click">
                                <div id="P1" runat="server" class="imgGra">
                                    <p>
                                        Grafico 3</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div id="div1" style="width: 33%;background: rgba(62, 137, 233, 0.3);display: block;float: left;">
                    <h2>
                        Análise</h2>
                    <ul class="iconList">
                        <li>
                            <asp:LinkButton ID="LinkButton1" runat="server">
                                <div id="Div2" runat="server" class="imgAna">
                                    <p>
                                        Análise</p>
                                </div>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
