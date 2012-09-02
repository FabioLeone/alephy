<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true"
    CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%; margin-left: 1.25%;">
                <tr>
                    <td style="width: 50%;">
                        <fieldset style="width: 93%;">
                            <legend>
                                <h2>
                                    Relatórios</h2>
                            </legend>
                            <div id="divForm" style="height: 330px; width: 100%;">
                                <p>
                                    Selecione o ano, para consulta.
                                    <asp:DropDownList ID="ddlAno" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                    Loja:
                                    <asp:DropDownList ID="ddlLojaRelatorios" runat="server">
                                    </asp:DropDownList>
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
                                </p>
                            </div>
                        </fieldset>
                    </td>
                    <td style="width: 50%;">
                        <fieldset style="width: 93%;">
                            <legend>
                                <h2>
                                    Gráficos</h2>
                            </legend>
                            <div id="divGrafic" style="height: 330px; width: 100%;">
                                <p>
                                    Selecione o mês, para consulta.
                                    <asp:DropDownList ID="ddlMes" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Fev</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Abr</asp:ListItem>
                                        <asp:ListItem Value="5">Mai</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Set</asp:ListItem>
                                        <asp:ListItem Value="8">Ago</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="10">Out</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dez</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    Loja:
                                    <asp:DropDownList ID="ddlLojas" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                    Selecione o ano, para consulta (válido apenas para o Grafico2).
                                    <asp:DropDownList ID="ddlAnoG" runat="server">
                                    </asp:DropDownList>
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
                                    </ul>
                                </p>
                            </div>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
