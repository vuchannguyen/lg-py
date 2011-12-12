<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>

<script type="text/javascript">
    var i = 0;
    $(document).ready(function () {
        CRM.summary("", 'none', '');
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
                    required: true,
                    remote: '<%= Url.Action("CheckManyUserNameAvailable", "JRAdmin") %>',
                    maxlength: 500
                },
                WFID: {
                    required: true
                },
                WFRoleID: {
                    required: true
                }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    jQuery.ajax({
                        url: "/JRAdmin/CheckManyUserNameAndRoleExistOnCreate",
                        type: "POST",
                        datatype: "json",
                        data: ({
                            'username': $('#hidUserName').val(),
                            'roleid': $('#WFRoleID').val(),
                            'action': "Create"
                        }),
                        success: function (mess) {
                            if (mess.MsgType == 1) {
                                CRM.summary(mess.MsgText, 'block', 'msgError');
                                i--;
                                return false;
                            }
                            else {
                                form.submit();
                            }
                        }
                    });
                    i++;
                }
            }
        });
    });
</script>
<%= Html.ValidationSummary() %>
<%using (Html.BeginForm("Create", "JRAdmin", FormMethod.Post, new { id = "addForm" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>