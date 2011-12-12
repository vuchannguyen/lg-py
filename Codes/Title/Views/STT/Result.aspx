<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="UCResult.ascx" TagName="UCResult" TagPrefix="uc1" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#Enddate").datepicker({ onClose: function () { $(this).valid(); } });

        $("#editSTTForm").validate({
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
                Enddate: { required: true, checkDate: true, compareDate: ["#editSTTForm input[name='StartDate']", "get", "End Date", "Start Date"] },
                ResultId: { required: true }
            },
            submitHandler: function (form) {
                CRM.summary('', 'none', '');
                $("#btnSubmit").attr("disabled", "disabled");
                $(form).ajaxSubmit({
                    url: form.action,
                    dataType: 'json',
                    iframe: true,
                    success: function (result) {
                        if (result.MsgType == 1) {
                            CRM.summary(result.MsgText, 'block', 'msgError');
                        }
                        else {
                            CRM.message(result.MsgText, 'block', 'msgSuccess');
                            $('#list').setGridParam({ url: '/STT/GetListJQGrid?name=' + $('#txtKeyword').val()
                        + '&cls=' + $('#Cls').val() + '&statusId=' + $('#StatusId').val() + '&resultId=' + $('#ResultId').val()
                        + '&startDateBegin=' + $('#FromStartdate').val() + '&startDateEnd=' + $('#ToStartdate').val()
                        + '&endDateBegin=' + $('#FromEnddate').val() + '&endDateEnd=' + $('#ToEnddate').val()
                            }).trigger('reloadGrid');
                            CRM.closePopup();
                        }
                    }
                });
            }
        });
    });
</script>
<form id="editSTTForm" action="<%= Url.Action("Result", "STT")%>" method="post" enctype="multipart/form-data"
class="form">
<% STT_RefResult stt = (STT_RefResult)ViewData.Model;%>
<%= Html.Hidden("SttID", ViewData["STTID"].ToString())%>
<%= Html.Hidden("StartDate",ViewData["ViewStartDate"].ToString())%>
<uc1:UCResult ID="UCResult" runat="server" />
</form>
