<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    var i = 0;
    $(document).ready(function () {
        CRM.summary("", 'none', '');
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
                        url: "/JRAdmin/CheckUserNameAndRoleExistOnCreate",
                        type: "POST",
                        datatype: "json",
                        data: ({
                            'username': $('#UserName').val(),
                            'id': $('#ID').val(),
                            'roleid': $('#WFRoleID').val(),
                            //                        'workflow': $('#WFID').val(),
                            'action': "Update"
                            //                        'isActive': $('#IsActive').attr('checked')
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
<%using (Html.BeginForm("Edit", "JRAdmin", FormMethod.Post, new { id = "editForm" }))
  {%>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>