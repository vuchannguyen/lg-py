<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="../Common/UCPRDetail.ascx" TagName="UC1" TagPrefix="uc1" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%
    if (!(bool)ViewData[CommonDataKey.IS_ACCESSIBLE])
    {
        Html.RenderPartial("../Common/UCDoNotHavePermission");
    }
    else
    {
%>
    <uc1:uc1 ID="UCR1" runat="server" />
<%
    }
%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=PerformanceReviewPageInfo.ModName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("#btnRefresh").click(function () {
            window.location.reload(true);
        });
    });
</script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<% PerformanceReview obj = (PerformanceReview)ViewData.Model;
   string value = string.Empty;
   value +=CommonFunc.GetCurrentMenu(Request.RawUrl) + obj.ID ;
    %>
    <%= value%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
