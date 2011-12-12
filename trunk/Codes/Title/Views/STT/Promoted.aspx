<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">

    $(document).ready(function () {
        $("#StartDate").datepicker({
            onClose: function () { $(this).valid(); }
        });
        $("#ContractedDate").datepicker({
            onClose: function () { $(this).valid(); }
        });

        $("#updateSTTForm").validate({
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
                ContractedDate: { required: true, checkDate: true, compareDate: ["#updateSTTForm input[name='StartDate']", "get", "Contracted Date", "Start Date"] },
                TitleId: { required: true },
                StartDate: { required: true, checkDate: true, checkAge: ["#updateSTTForm input[name='DOB']"], compareDate: ["#EndDate", "get", "Start Date", "End Date"] },
                EmpStatusId: { required: true },
                NewID: { required: true,
                    remote: {
                        url: "/Employee/CheckIDExits",
                        type: "post",
                        data: {
                            Id: function () {
                                return $("#NewID").val();
                            }
                        }
                    }
                }
            },
            submitHandler: function (form) {
                $(form).ajaxSubmit({
                    success: function (result) {
                        if (result.MsgType == 1) {
                            CRM.summary(result.msg.MsgText, 'block', 'msgError');
                        }
                        else {
                            CRM.message(result.MsgText, 'block', 'msgSuccess');
                            $('#list').setGridParam({ url: '/STT/GetListJQGrid?name=' + name
                                    + '&cls=' + $('#Cls').val() + '&statusId=' + $('#StatusId').val() + '&resultId=' + $('#ResultId').val()
                                    + '&startDateBegin=' + $('#FromStartdate').val() + '&startDateEnd=' + $('#ToStartdate').val()
                                    + '&endDateBegin=' + $('#FromEnddate').val() + '&endDateEnd=' + $('#ToEnddate').val()
                            }).trigger('reloadGrid');
                            CRM.closePopup();
                        }
                    },
                    url: form.action,
                    dataType: 'json'
                });
            }
        });
    });
</script>
<form id="updateSTTForm" action="<%= Url.Action("Promoted", "STT")%>" method="post"
enctype="multipart/form-data" class="form">
<% STT stt = (STT)ViewData.Model;%>
<%= Html.Hidden("ID",stt.ID) %>
<%= Html.Hidden("DOB",stt.DOB.HasValue?stt.DOB.Value.ToString(Constants.DATETIME_FORMAT):string.Empty)%>
<%= Html.Hidden("EndDate", ViewData["ViewEndDate"].ToString())%>
<%= Html.Hidden("STTUpdateDate", ViewData["STTUpdateDate"].ToString())%>
<table id="tbl" cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td class="label required" style="width: 120px">
                Employee ID <span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("NewID", "", new { @style = "width:120px", @maxlength = "10" })%>
            </td>
        </tr>
        <tr>
            <td class="label required" style="width: 120px">
                Job Title <span>*</span>
            </td>
            <td class="input">
                <%=Html.DropDownList("TitleId", ViewData["TitleId"] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:125px" })%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Start Date <span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("StartDate", "", new { @style = "width:120px" })%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Contracted Date <span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("ContractedDate", "", new { @style = "width:120px" })%>
            </td>
        </tr>
        <tr class="last">
            <td class="label required">
                Current Status <span>*</span>
            </td>
            <td class="input">
                <%=Html.DropDownList("EmpStatusId", ViewData["EmpStatusId"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:125px" })%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup();" />
            </td>
        </tr>
    </tbody>
</table>
</form>
