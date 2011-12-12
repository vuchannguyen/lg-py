<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="~/Views/TrainingEmployeeCertification/UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<%using (Html.BeginForm("Edit", "TrainingEmployeeCertification", FormMethod.Post, new { id = "TrainingEmployeeCertificationForm", @class = "form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
