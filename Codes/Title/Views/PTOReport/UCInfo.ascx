<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return value.match('^(0|[1-9][0-9]*)$');                    
            }
        }, E0037);

        $("#pTOReportForm").validate({
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
                CarriedForward: {
                    required: true,
                    integer: true,
                    min: 0,
                    maxlength: '<%= Constants.PTO_BALANCE_MAX_LENGTH %>'
                },
                MonthlyVacation: {
                    required: true,
                    integer: true,
                    min: 0,
                    maxlength: '<%= Constants.PTO_BALANCE_MAX_LENGTH %>'
                },
                Comment: {
                    required: true
                }
            }
        });
    });
</script>
<%: Html.ValidationSummary(true) %>
<div>
<table class="edit" style = "width:100%">
    <tr>
        <td class="label required" style="width:30%">Carried Forward<span>*</span></td>
        <td class="input" style="width:70%">
            <%= Html.TextBox("CarriedForward", null, new { @style = "width:70%", @maxlength=3 })%> Hour(s)
            <%= Html.Hidden("ID") %>
            <%= Html.Hidden("UpdateDate")%>
        </td>
    </tr>
    <tr>
        <td class="label required" style="width:30%">Monthly Vacation<span>*</span></td>
        <td class="input" style="width:70%">
            <%= Html.TextBox("MonthlyVacation", null, new { @style = "width:70%", @maxlength=3 })%> Hour(s)
        </td>
    </tr>
    <tr>
        <td class="label required">Comment<span>*</span></td>
        <td class = "input">
            <%= Html.TextArea("Comment", new { @style = "width:280px;height:50px;", @maxlength=4000 })%>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <input type="submit" class="save" id="btnCreate" value="" title="Save" />
            <input type="button" class="cancel" value="" id="btnCancel" onclick="CRM.closePopup()" title="Cancel" />
        </td>
    </tr>
</table>
</div>
   