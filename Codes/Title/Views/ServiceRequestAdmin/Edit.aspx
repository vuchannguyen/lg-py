<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%using (Html.BeginForm("Edit", "ServiceRequestAdmin", FormMethod.Post, 
      new { @id="frmSRForm", @enctype ="multipart/form-data", @class="form"}))
  { %>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
<%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=ServiceRequestPageInfo.FuncNameEdit + CommonPageInfo.AppSepChar + 
        ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=ServiceRequestPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl) + ServiceRequestPageInfo.FuncNameEdit%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
