<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<script type="text/javascript">
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
                Name: {
                    required: true,
                    maxlength: 200
                },
                Description: {
                    maxlength: 4000
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
                            $('#list').setGridParam({ url: '/AssetCategory/GetListJQGrid?searchText=' + encodeURI(name) }).trigger('reloadGrid');
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
<%using (Html.BeginForm("Create", "AssetCategory", FormMethod.Post, new { id = "addForm",@class="form" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>