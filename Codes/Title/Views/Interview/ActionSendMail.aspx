<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<% string ids = (string)ViewData["IDs"];
   Response.Write(Html.Hidden("ids", ids));
     %>
     <script type="text/javascript">
         $(document).ready(function () {
             $("#SendCandidate").click(function () {
                 CRM.popup("/Interview/SendCandidateMail/?ids=" + $("#ids").val() + "&page=", "Send Email To Candidate", 800);
             });
             $("#SendInterview").click(function () {
                 CRM.popup("/Interview/SendInterviewMail/?ids=" + $("#ids").val() + "&page=", "Send Meeting Request", 800);
             });
             $("#btnClose").click(function () {
                 window.location = "/InterView";
             });
         });
     </script>
     <html4f>
     <p align="justify"><br />Now you can send email to this candidate by clicking on '<b>Send Email to Candidate</b>' or send the meeting request to interviewer by clicking '<b>Send Meeting Request</b>'. Click '<b>Close</b>' if you want to do this later. </p>
<table class="form"  width="100%" align="center">   
    <tr height="20px">
        <td style="width:50px"></td>
        <td><button type="button" class="icon sendcandidate" id="SendCandidate"></button></td>
        <td><button type="button" class="icon sendmeeting" id="SendInterview"></button></td>
        <td><input type="button" value="" class="close" id="btnClose"  title="Cancel" /></td>
        <td style="width:50px"></td>
    </tr>
</table>
</html4f>