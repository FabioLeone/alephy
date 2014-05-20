<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="aplicativos.aspx.cs" Inherits="SIAO.aplicativos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Content/css/Animations.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="no-js">
        <ul class="conav" role="navigation">
            <li class="nav-item-a fade fade-50">
            </li>
            <li class="nav-item-b fade fade-60">
                <div class="hex">
                    <a href="wfmGerarRelatorios.aspx" title="">Relatórios e Gráficos</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-c fade fade-60">
                <div class="hex">
                    <a href="analise.aspx" title="" style="margin: 14px auto;">Análise</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
        </ul>
    </div>
</asp:Content>
