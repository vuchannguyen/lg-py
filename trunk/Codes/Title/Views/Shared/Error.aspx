<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Error
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorTitle" ContentPlaceHolderID="FunctionTitle" runat="server">
    Error Page
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <img src="../../Content/Images/ExtraIcons/error-icon-1928115.jpg" align="left" /><br/><br/><h3 style="font-weight:bold; color:Red"><%: ViewData["ErrorMessage"] %></h3>
    
</asp:Content>
