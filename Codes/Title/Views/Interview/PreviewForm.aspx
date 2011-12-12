<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="../EForm/InterviewForm/UCInterViewR1.ascx" TagName="UCInterViewR1"
    TagPrefix="uc1" %>
<%@ Register Src="../EForm/InterviewForm/UCInterviewR2.ascx" TagName="UCInterviewR2"
    TagPrefix="uc2" %>
<%@ Register Src="../EForm/InterviewForm/UCInterviewR3.ascx" TagName="UCInterviewR3"
    TagPrefix="uc3" %>
<script type="text/javascript">    
    /////////////////////////////////
    jQuery(document).ready(function () {

        $("#btnClose").click(function () {
            CRM.closePopup();
        });
    });
</script>
<div class="form" style="height:500px;overflow-x:scroll;">

<% if (ViewData["Round"].ToString() == "1") %>
<% { %>
<uc1:UCInterViewR1 ID="UCInterViewR11" runat="server" />
<% }
    else if (ViewData["Round"].ToString() == "2") %>
<%  { %>
<uc2:UCInterviewR2 ID="UCInterviewR21" runat="server" />
<% }
    else if (ViewData["Round"].ToString() == "3") %>
<% { %>
<uc3:UCInterviewR3 ID="UCInterviewR31" runat="server" />
<% } %>   
<br />
<table width="940px">
    <tr>
        <td style="text-align:center">
        <input type="button" title="Close" id="btnClose" class="close" value="" />
        </td>
    </tr>
</table>
</div>


