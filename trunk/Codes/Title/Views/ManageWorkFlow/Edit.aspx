<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>

<%using (Html.BeginForm("Edit", "ManageWorkFlow", FormMethod.Post, new { id = "manageWorkFlowForm",@class="form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
