<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div style="background-color:#EEEEEE">
<%using (Html.BeginForm("AddActivity", "ServiceRequestAdmin", FormMethod.Post,
      new { @id = "frmActivity", @class = "form" }))
  { 
    var hourList = new SelectList(CommonFunc.GetHoursList(0, 24, 15, Constants.SR_DUE_HOUR_FORMAT), "Text", "Text", "08:00 AM");
    %>
    <%=Html.Hidden("srId", Page.RouteData.Values["id"]) %>
    <table class="edit" width="100%">
        <tr>
            <td class="label required" style="vertical-align:text-top">
                User<span>*</span>
            </td>
            <td class="input" colspan="3">
                <%=Html.TextBox("UserName", HttpContext.Current.User.Identity.Name, new { @style = "width:165px"})%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Date<span>*</span>
            </td>
            <td class="input" style="width:120px" >
                <%=Html.TextBox("StartTime", null, new { @style = "width:65px;", @maxlength="10", @class="far_error"})%>
                <%=Html.Hidden("hidError_StartTime")%>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="label required">
                From
            </td>
            <td class="input" >
                <%=Html.DropDownList(CommonDataKey.SR_HOURS_LIST_START_TIME, hourList, new { @style = "width:90px" })%>
            </td>
            <td class="label required" style="width:50px !important">
                To
            </td>
            <td class="input" >
                <%=Html.DropDownList(CommonDataKey.SR_HOURS_LIST_END_TIME, hourList, new { @style = "width:90px" })%>
            </td>
        </tr>
        <tr>
            <td class="label required" style="vertical-align:text-top">
                Total<span>*</span>
            </td>
            <td class="input" >
                <%=Html.TextBox("Total", null, new { @style = "width:85px" })%>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="label" style="vertical-align:text-top">
                Description
            </td>
            <td class="input" colspan="3">
                <%=Html.TextArea("Description", new { @style = "width:100%; height: 100px" })%>
            </td>
        </tr>
    </table>
    <div style="text-align:center">
        <input class="save" type="submit" alt="" value="" />
        <input class="cancel" type="button" onclick="CRM.closePopup();" alt="" value="" />
    </div>
<%} %>
</div>
<script type="text/javascript">
    function UpdateTotal() {
        if (isValidUpdateTotal()) {
            var dFrom = $("#StartTime").val().split('/');
            var dTo = $("#StartTime").val().split('/');
            var startTime = Date.parse(dFrom[1] + "/" + dFrom[0] + "/" + dFrom[2] + " " + $("#<%=CommonDataKey.SR_HOURS_LIST_START_TIME%>").val());
            var endTime = Date.parse(dTo[1] + "/" + dTo[0] + "/" + dTo[2] + " " + $("#<%=CommonDataKey.SR_HOURS_LIST_END_TIME%>").val());
            var oneMinute = 1000 * 60; //1000 miliseconds * 60 seconds
            var sTotal = Math.abs(endTime - startTime) / oneMinute;
            var sSign = endTime - startTime < 0 ? "-" : "";
            if (sTotal.toString() != 'NaN') {
                var sHour = Math.floor(sTotal / 60);
                var sMinute = Math.round(sTotal % 60);
                $("#Total").val(sSign + (sHour < 10 ? "0" + sHour : sHour) + '<%=Constants.SR_ACTIVITY_TOTAL_SEPARATE%>' +
                (sMinute < 10 ? "0" + sMinute : sMinute));
            }
        }
    }
    function isValidUpdateTotal() {
//        if ($("#StartTime").hasClass("error") || $("#StartTime").val() == "" ||
//            $("#EndTime").hasClass("error") || $("#EndTime").val() == "")

        if (!$("#StartTime").valid() || $("#StartTime").val() == "")
            return false;
        return true;
    }
    $(document).ready(function () {
        $.validator.addMethod(
            "validHour",
            function (value, element) {
                var re = new RegExp("^[0-9][0-9]:[0-5][0-9]$");
                if (!re.test(value))
                    return false;
                var sHour = parseInt(value.split(':')[0]);
                var sMinute = parseInt(value.split(':')[1]);
                if (sHour == sMinute && sHour == 0)
                    return false;
                return true;
            },
            CRM.format(E0030, "Total")
        );
        $("#StartTime").datepicker();
        $("#frmActivity").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                if (element.hasClass("far_error")) {
                    var placement = $("#hidError_" + element.attr("id"));
                    error.insertBefore(placement);
                }
                else
                    error.insertAfter(element);

            },
            rules: {
                Description: { maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_Activity(), "Description")%>') },
                UserName: { required: true, maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_Activity(), "UserName")%>') },
                StartTime: { required: true, checkDate: true },
                //Total: { required: true, regex: "^\\d{2}:\\d[1-9]$"}
                Total: { required: true, validHour: true }
            }
        });
        $("#UserName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=ServiceRequest',
            { employee: true });
        $("#StartTime").change(function () {
            UpdateTotal();
        });
        $("#<%=CommonDataKey.SR_HOURS_LIST_START_TIME%>").change(function () {
            UpdateTotal();
        });
        $("#<%=CommonDataKey.SR_HOURS_LIST_END_TIME%>").change(function () {
            UpdateTotal();
        });
        $("#frmActivity").submit(function () {
            if ($(this).valid())
                $("input[type='submit']").attr("disabled", "disabled");
        });
    });
</script>
