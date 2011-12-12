<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    var i = 0;
    $(document).ready(function () {
        $.validator.addMethod("CheckRegular", function (value, element) {
            return this.optional(element) || /^[a-zA-Z 0-9]+$/i.test(value);
        }, "Name should be Alphanumeric.");

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
                GroupName: {
                    required: true,
                    maxlength: 50,
                    CheckRegular: true,
                    remote: {
                        url: "/Group/IsGroupNameExist",
                        type: "post",
                        data: {
                            groupName: function () {
                                return $("#GroupName").val();
                            }
                        }
                    }
                },
                DisplayOrder: {
                    number: true
                },
                Description: {
                    maxlength: 250
                }
            },
            messages: {
                DisplayOrder: {
                    number: "Invalid Number"
                }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    form.submit();
                    i++;
                }
            }
        });
    });
</script>
<%= Html.ValidationSummary() %>
<%using (Html.BeginForm("Create", "Group", FormMethod.Post, new { id = "addForm",@class="form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>