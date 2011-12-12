<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>

<%using (Html.BeginForm("Edit", "AssetMaster", FormMethod.Post, new { id = "AssetMasterForm", @class = "form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
