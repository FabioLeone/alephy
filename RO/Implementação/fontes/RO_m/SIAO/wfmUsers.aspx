<%@ Page Language="C#" Inherits="SIAO.wfmUsers" MasterPageFile="~/Siao.master" %>

<%@ Register Src="Controls/wucListaUsers.ascx" TagName="wucListaUsers" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function pageLoad() {
        $("section").addClass("sectionb");
    }

    window.onload(pageLoad);
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceHolder1" runat="server">
    <div>
        <uc1:wucListaUsers ID="wucListaUsers1" runat="server" />
    </div>
</asp:Content>
