<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="configurations.aspx.cs" Inherits="SIAO.configurations" %>

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
                    <a href="wfmUsers.aspx" title="">Cadastro de usuários</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-c fade fade-60">
                <div class="hex">
                    <a href="wfmCadastroRede.aspx" title="">Cadastro de redes</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-d fade fade-60">
                <div class="hex">
                    <a href="wfmCadastroLojas.aspx" title="">Cadastro de lojas</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-f fade fade-60">
                <div class="hex">
                    <a href="wfmVinculos.aspx" title="">Vincular usuário</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-e fade fade-60">
                <div class="hex">
                    <a href="wfmFiles.aspx"title="">Histórico de uploads</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
            <li class="nav-item-g fade fade-30">
                <div class="hex">
                    <a href="wfmBanco.aspx"title="">Gerênciamento do banco</a>
                    <div class="corner-1">
                    </div>
                    <div class="corner-2">
                    </div>
                </div>
            </li>
        </ul>
    </div>
</asp:Content>
