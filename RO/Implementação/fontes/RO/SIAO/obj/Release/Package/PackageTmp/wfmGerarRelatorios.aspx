<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmGerarRelatorios.aspx.cs" Inherits="SIAO.wfmGerarRelatorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="width: 96.5%;">
                <legend>
                    <h2>Relatórios</h2>
                </legend>
                <div id="divForm" style="height: 330px; width: 100%;">
                    <% if (SIAO.Global.Acs == "nvg")
                       { %>
                        <p>Gerente</p>
                       <%}
                       else if (SIAO.Global.Acs == "nvp")
                       { %>
                        <p>Proprietário</p>
                       <%}
                       else if (SIAO.Global.Acs == "adm")
                       { %>
                        <p>Administrador</p>
                       <%} %>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
