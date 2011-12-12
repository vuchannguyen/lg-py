<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>
<%using (Html.BeginForm("Edit", "TrainingMaterial", FormMethod.Post, new { @id = "frmCourse", @class = "form" }))
    { %>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
<%} %>
