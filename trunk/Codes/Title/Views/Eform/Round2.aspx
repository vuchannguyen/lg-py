<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="InterviewForm/UCInterviewR2.ascx" TagName="UCInterviewR2" TagPrefix="uc1" %>
     <%= TempData["Message"]%>
    <br />
    <div class="form" style="height:500px;overflow-x:scroll;">
    <uc1:UCInterviewR2 ID="UCInterviewR21" runat="server" />
    </div>