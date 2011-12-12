<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<style type="text/css">
    div.ac_results
    {
        width: auto !important;    
        overflow-y: scroll !important;
    }
    div.ac_results ul
    {
        overflow: visible !important;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        if ('<%=ViewData.Model%>' == '') {
            $("#EmployeeName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?func=Employee&type=2&suffixId=true',
               { employee: true, subField: "#EmployeeId", multiData: true });
        }
        $("#ExpireDate").datepicker();
        $("#frmEnglishInfo").validate({
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
                EmployeeName: { required: true },
                Score: { required: true, number: true, maxlength: 6 },
                '<%=CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST%>': { required: true },
                ExpireDate: { maxlength: 10, checkDate: true },
                Notes: { maxlength: '<%=CommonFunc.GetLengthLimit(new Training_EmpEnglishInfo(), "Notes")%>' }
            }
        });
        $("#frmEnglishInfo").submit(function () {
            if ($(this).valid())
                $(this).find("input[type='submit']").attr("disabled", "disabled");
        });
        //        $("#Score").keydown(function (e) {
        //            var key = e.charCode || e.keyCode || 0;
        //            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
        //            return (
        //                key == 8 ||
        //                key == 9 ||
        //                key == 46 ||
        //                (key >= 37 && key <= 40) ||
        //                (key >= 48 && key <= 57) ||
        //                (key >= 96 && key <= 105));
        //        })
    });
</script>

<%
    var eei = ViewData.Model as Training_EmpEnglishInfo;
    string empName = "";
    if (eei != null)
    {
        empName = eei != null ? new EmployeeDao().FullName(eei.EmployeeId, Constants.FullNameFormat.FirstMiddleLast) : null;
        Response.Write(Html.Hidden("eeiId", eei.ID));
    }
%>
<%=Html.Hidden("UpdateDate")%>

<table width="100%" class="edit">
    <tr>
        <td class="label required">Employee<span>*</span></td>
        <td class="input">
            <%
                if (eei == null)
                    Response.Write(Html.TextBox("EmployeeName", "", new { @style = "width:135px" }));
                else
                    Response.Write(Html.TextBox("EmployeeName", empName, new { @style = "width:135px", @readonly = "readonly" }));
            %>
        </td>
        <td class="label" style="width:70px !important">ID</td>
        <td class="input" style="width:125px !important">
            <%=Html.TextBox("EmployeeId", null, new { @style="width:70px", @readonly = "readonly" })%>
        </td>
    </tr>
    <tr>
        <td class="label required">Type<span>*</span></td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.TRAINING_EEI_SKILL_TYPE_LIST, null, Constants.TRAINING_EEI_LIST_LABEL, new { @style = "width:140px" })%>
        </td>
        <td class="label" style="width:70px !important">Expire Date</td>
        <td class="input">
            <%=Html.TextBox("ExpireDate", eei==null || !eei.ExpireDate.HasValue ? null : 
                eei.ExpireDate.Value.ToString(Constants.DATETIME_FORMAT), 
                new { @style="width:70px", @maxlength = "10" })%>
        </td>
    </tr>
    <tr>
        <td class="label required">Score<span>*</span></td>
        <td class="input">
            <%=Html.TextBox("Score", null, new { @style="width:50px", @maxlength = "6"})%>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Notes</td>
        <td class="input" colspan="3">
            <%=Html.TextArea("Notes", null, new { @style="width:332px; height:80px"})%>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align:center; vertical-align:bottom; padding-top:20px">
            <input class="save" type="submit" value="" alt=""/>
            <input class="cancel" type="button" value="" alt="" onclick="CRM.closePopup();" />
        </td>
    </tr>
</table>