<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    var i = 0;
    var resultOrder = true;
    var result = true;
    $(document).ready(function () {
        jQuery.validator.addMethod("CheckExistGroupName", function (value, element) {
            var validator = this;
            jQuery.ajax({
                url: "/Group/IsGroupNameExistOnEdit",
                type: "POST",
                datatype: "json",
                data: ({
                    'id': $('#GroupId').val(),
                    'groupName': $('#GroupName').val()
                }),
                success: function (mess) {
                    if (mess.MsgType == 1) {
                        var errors = {};
                        errors[element.name] = mess.MsgText;
                        validator.showErrors(errors);
                        result = false;                        
                    }
                    else {
                        result = true;                        
                    }
                }
            })
            return result;
        }, "&nbsp;");

        jQuery.validator.addMethod("CheckExistOrder", function (value, element) {
            var validator = this;
            jQuery.ajax({
                url: "/Group/IsOrderExistOnEdit",
                type: "POST",
                datatype: "json",
                data: ({
                    'id': $('#GroupId').val(),
                    'displayOrder': $('#DisplayOrder').val()
                }),
                success: function (mess) {
                    if (mess.MsgType == 1) {
                        var errors = {};
                        errors[element.name] = mess.MsgText;
                        validator.showErrors(errors);
                        resultOrder = false;                        
                    }
                    else {
                        resultOrder = true;                        
                    }
                }
            })
            return resultOrder;
        }, "&nbsp;");

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
                GroupName: {
                    required: true,
                    CheckExistGroupName: true
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
<%using (Html.BeginForm("Edit", "Group", FormMethod.Post, new { id = "editForm",@class="form" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>