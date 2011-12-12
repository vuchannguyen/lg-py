<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return (value == parseInt(value, 10));
            }
        }, E0037);
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
                Name: {
                    required: true,
                    maxlength: 255
                },
                DisplayOrder: {
                    min: 1,
                    required: true,
                    integer: true,
                    maxlength:5
                },
                AssetCategoryId: {
                    required: true
                }
            },
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    success: function (result) {
                        if (result.msg.MsgType == 1) {
                            CRM.summary(result.msg.MsgText, 'block', 'msgError');
                        }
                        else {
                            CRM.message(result.msg.MsgText, 'block', 'msgSuccess');
                            $('#list').setGridParam({ page: 1 }).trigger('reloadGrid');
                            CRM.closePopup();
                        }
                    },
                    url: form.action,
                    dataType: 'json',
                    iframe: true
                });
            }
        });
    });
</script>
<%= Html.ValidationSummary() %>
<%using (Html.BeginForm("Create", "AssetProperty", FormMethod.Post, new { id = "addForm",@class="form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>