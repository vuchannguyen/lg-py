<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCClearLog.ascx" TagName="UCClearLog" TagPrefix="uc1" %>
<html>
 <script type="text/javascript">
     $(document).ready(function () {
         $("#editForm").validate({
             debug: false,
             errorElement: "span",
             errorPlacement: function (error, element) {
                 error.tooltip({
                     bodyHandler: function () {
                         return error.html();
                     }
                 });
                 error.insertAfter(element);
             },
             rules: {
                 TimeToClearLog: {
                     required: true
                 }
             }
         });
     });

     </script>

     <%= Html.ValidationSummary() %>
<%using (Html.BeginForm("ClearLog", "UserLog", FormMethod.Post, new { id = "editForm", @class = "form" }))
  {%>    
    <uc1:UCClearLog ID="UCClearLog1" runat="server" />
  <% } %>
</html>
