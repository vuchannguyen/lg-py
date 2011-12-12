<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    var formSubmit = 0;
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
                UserName: {
                    required: true
                }
            },
            submitHandler: function (form) {
                if (formSubmit == 0) {
                    form.submit();
                    formSubmit++;
                }
            }
        });
    });
</script>
<%using (Html.BeginForm("Edit", "UserAdmin", FormMethod.Post, new { id = "editForm",@class="form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
