<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmCadastroRede.aspx.cs" Inherits="SIAO.wfmCadastroRede" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="height: 35px; padding: 10px;">
                
            </div>
            <fieldset style="width:402px; margin-left:26%;">
                <legend><h2>
                    Cadastro de Redes</h2></legend>
            <div style="height: 126px; width: 400px; margin-left: 10px;">
                <table>
                    <tr>
                        <td>
                            Gerente da Rede:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlGerente" Width="254px" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:HyperLink ID="hl" NavigateUrl="~/wfmCadastroUsuario.aspx" runat="server">Cadastrar um novo Gerente.</asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nome da Rede:
                        </td>
                        <td>
                            <asp:TextBox ID="txtRede" runat="server" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: right;">
                            <asp:Button ID="btnSave" CssClass="button gray" ToolTip="Salvar as informações da rede."
                                runat="server" Text="Salvar" onclick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
