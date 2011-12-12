<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="UCInfo.ascx" tagname="UCInfo" tagprefix="uc1" %>
<%using (Html.BeginForm("Create", "TrainingMaterial", FormMethod.Post, new { @id = "frmCourse", @class = "form",@style="width:100%" }))
    { %>
    <uc1:UCInfo ID="UCInfo1" runat="server" />
<%} %>