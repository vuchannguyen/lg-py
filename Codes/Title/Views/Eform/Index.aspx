<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register src="InterviewForm/UCInterViewR1.ascx" tagname="UCInterViewR1" tagprefix="uc1" %>

 <%= TempData["Message"]%>
    <br />
    <div class="form" style="height:500px;overflow-x:auto;">
    <uc1:UCInterViewR1 ID="UCInterViewR11" runat="server" />
</div>


