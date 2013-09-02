<%@ Page Title="" Language="C#" MasterPageFile="~/Siao.Master" AutoEventWireup="true" CodeBehind="wfmEnvio.aspx.cs" Inherits="SIAO.wfmEnvio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            $("section").addClass("sectionb");
        }

        window.onload(pageLoad);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="width: 33%; margin-left:31%;">
                <legend>
                    <h2>
                        Envio de Arquivos</h2>
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
