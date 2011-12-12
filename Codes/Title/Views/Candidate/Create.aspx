<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= ViewData["Message"]%>
    <%= TempData["Message"]%>
    <br />
    <%using (Html.BeginForm("Create", "Candidate", FormMethod.Post, new { id = "addFormCandidate" }))
      { %>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.FuncAddNewCandidate + CommonPageInfo.AppSepChar  + CommonPageInfo.AppName%> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= HiringCenterPageInfo.ComName%> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
 <%= CommonFunc.GetCurrentMenu(Request.RawUrl) + HiringCenterPageInfo.FuncAddNewCandidate%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
