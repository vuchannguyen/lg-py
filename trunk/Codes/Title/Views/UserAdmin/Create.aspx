<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    var i = 0;
    $(document).ready(function () {
        $("#addForm").validate({
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
                hidUserName: {
                    required: true
                }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    form.submit();
                    i++;
                }
            }
        });
        $("#UserName").autocomplete('Library/GenericHandle/AutoCompleteHandler.ashx/?Page=UserAdmin',
            { max: 50, highlightItem: true, multiple: true, faceBook: true,
                multipleSeparator: "<%=Constants.SEPARATE_USER_ADMIN_USERNAME%>", hidField: "#hidUserName", employee: true
            });
    });
</script>
<%= Html.ValidationSummary() %>
<%using (Html.BeginForm("Create", "UserAdmin", FormMethod.Post, new { id = "addForm",@class="form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
