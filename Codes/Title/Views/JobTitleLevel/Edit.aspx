<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>

<%using (Html.BeginForm("Edit", "JobTitleLevel", FormMethod.Post, new { id = "JobTitleForm",@class="form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>