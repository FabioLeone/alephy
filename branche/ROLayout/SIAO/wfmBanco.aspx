<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmBanco.aspx.cs" Inherits="SIAO.wfmBanco" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("section").addClass("sectionb");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="width:65px; margin-top:1%;">
                <legend>
                    <h3>Outros</h3>
                </legend>
                <div>
                    <asp:Button ID="btnIndices" CssClass="button gray" runat="server" 
                        Text="Indices" onclick="btnIndices_Click" />
                </div>
            </fieldset>
            <fieldset style="width: 33%; margin-left:31%; margin-Top:2%;">
                <legend>
                    <h2>Manutenção do Banco de Dados</h2>
                </legend>
                <div id="divForm" style="height: 70px; width: 100%;">
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="fuArquivo" runat="server" Width="300px"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Button ID="btnEnviar" CssClass="button gray"
                                    runat="server" Text="Enviar" onclick="btnEnviar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
