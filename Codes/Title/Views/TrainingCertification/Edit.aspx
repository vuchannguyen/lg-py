<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="~/Views/TrainingCertification/UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<%using (Html.BeginForm("Edit", "TrainingCertification", FormMethod.Post, new { id = "TrainingCertificationForm", @class = "form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
