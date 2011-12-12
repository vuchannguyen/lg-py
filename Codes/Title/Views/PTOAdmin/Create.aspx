<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<title>Create</title>
<style type="text/css">
    .datetime
    {
        width:70px
    }
    .datetimecell
    {
        width:120px
    }
    .idcell
    {
        width:70px;    
    }
    .idlabel
    {
        width:70px !important;    
    }
    a.submit_desc, a.man_desc
    {
        font-weight:bold;    
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        isSubmited = false;
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
                txtEmployeeName: { required: true },
                PTOType_ID: { required: true },
                txtManagerName: { required: true },
                reason: { maxlength: 500, required: true },
                HRComment: { maxlength: 500 }
            },
            submitHandler: function (form) {
                formSubmitHandler(form);
            }
        });
        setInputDateOffType(ptoTypeOf($("#type").val()));
        $("#txtDateOffFrom").datepicker();
        $("#txtDateOffTo").datepicker();
        $("#txtEmployeeName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?func=Employee&type=2&suffixId=true');
        $("#txtEmployeeName").result(function (event, data, formatted) {
            var submitter = formatted.split(' - ')[1];
            $("#Submitter").val(submitter);
            if (submitter != "") {
                //$("a.submit_desc").attr("id", submitToId);
                //CRM.loading();
                var result = 'System Error';
                $.ajax({
                    async: true,
                    cache: false,
                    type: "GET",
                    dataType: "json",
                    timeout: 1000,
                    url: '<%=Url.Action("GetEmployeeInfo")%>?empId=' + submitter,
                    success: function (data) {
                        $("#submitDesc").show();
                        $("#submitDesc").html("More details about <a id=" + submitter + " class='submit_desc' href='#'>" + data.UserName + "</a>");
                        ShowTooltip($("a.submit_desc"), $("#shareit-box"), "/Portal/TrainingCenter/EmployeeToolTip");
                        $("tr.trBalance").show();
                        $("td.tdBalanceYTD").html("<b style='color:red'>" + data.BalanceYTD + "</b> hour(s)");
                        $("td.tdUsed").html("<b style='color:red'>" + data.UsedYTD + "</b>  hour(s)");
                    }
                });
                //CRM.completed();
            } else {
                $("#submitDesc").hide();
            }
        });
        $("#txtManagerName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=PTO_User');
        $("#txtManagerName").result(function (event, data, formatted) {
            var submitToId = formatted.split(' - ')[1];
            $("#SubmitTo").val(submitToId);
            if (submitToId != "") {
                //$("a.man_desc").attr("id", submitToId);
                //CRM.loading();
                var result = 'System Error';
                $.ajax({
                    async: true,
                    cache: false,
                    type: "GET",
                    dataType: "html",
                    timeout: 1000,
                    url: '<%=Url.Action("GetEmployeeLoginName")%>?empId=' + submitToId,
                    success: function (msg) {
                        $("#manDesc").show();
                        $("#manDesc").html("More details about <a id=" + submitToId + " class='man_desc' href='#'>" + msg + "</a>");
                        ShowTooltip($("a.man_desc"), $("#shareit-box"), "/Portal/TrainingCenter/EmployeeToolTip");
                    }
                });
                //CRM.completed();
            } else {
                $("#manDesc").hide();
            }
        });
        var iType = ptoTypeOf($("#type").val());
        //Set the current pto type id to hidden field
        $("#hidPTO_Type").val(iType);
        setInputDateOffType(iType);
        disableAllControl();
        
        $("#PTOType_ID").change(function () {
            var oldType = $("#hidPTO_Type").val();
            var typeID = $(this).val();
            if (typeID == "") {
                disableAllControl();
            }
            else if (ptoTypeOf(typeID) != oldType) {
                setInputDateOffType(ptoTypeOf(typeID));
            }
            else {
                enableAllControl();
            }
        });
        $("#PTOType_Parent_ID").change(function () {
            $("#PTOType_ID").html("");
            var id = $("#PTOType_Parent_ID").val();
            $("#PTOType_ID").append($("<option value=''><%= Constants.PTO_FIRST_TYPE%></option>"));
            if (id != "") {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + id + '&Page=PTOType', function (item) {
                    $.each(item, function () {
                        $("#PTOType_ID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    });
                });
            }
        });
        setNumberOfDateOff();
    });
</script>
    
<div id="summary" style="display: none" class=""></div>
<%using (Html.BeginForm("Create", "PTOAdmin", FormMethod.Post, 
    new { id = "addForm", @class = "form" })){%>
<input type="hidden" id="hidNumberOfDateOff" name="hidNumberOfDateOff" />
<input type="hidden" id="hidPTO_Type" />
<input type="hidden" id="hidPTO_TypeIDs_IsHour" value='<%= ViewData[CommonDataKey.PTO_IDS_IS_HOUR_TYPE] %>'/> 
<input type="hidden" id="hIndex" name="hIndex" value="0" />
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" >
            Employee<span>*</span>
        </td>
        <td class="input" >
            <input id="txtEmployeeName" style="width:160px !important" name="txtEmployeeName" type="text" />
            <%=Html.Hidden("Submitter")%>
        </td>
        <td class="label" colspan="2" style="text-align:left !important; width:200px !important">
            <span id="submitDesc" style="display:none; vertical-align:middle; margin-left:0px">
            </span>
        </td>
    </tr>
    <tr class="trBalance" style="display:none">
        <td class="label">
            Balance YTD
        </td>
        <td class="input tdBalanceYTD">
            
        </td>
        <td class="label">
            Used this month
        </td>
        <td class="input tdUsed">
            
        </td>
    </tr>
    <tr>
        <td class="label required">
            Submitted to<span>*</span>
        </td>
        <td class="input" >
            <input id="txtManagerName" style="width:160px !important" name="txtManagerName"  type="text"  />
            <%=Html.Hidden("SubmitTo")%>
            
        </td>
        <td class="label" colspan="2" style="text-align:left !important;">
            <span id="manDesc" style="display:none; vertical-align:middle; margin-left:0px">
            </span>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Type<span>*</span>
        </td>
        <td class="input" colspan="3">
                <%= Html.DropDownList("PTOType_Parent_ID", null, Constants.PTO_PARENT_FIRST_TYPE, new { @style = "width:200px" })%>
            <%= Html.DropDownList(CommonDataKey.PTO_TYPE_LIST, null, Constants.PTO_FIRST_TYPE, new { @style = "width:185px" })%>
            
        </td>
    </tr>
    <tr>
        <td class="label required">
            Reason<span>*</span>
        </td>
        <td class="input" colspan="3">
            <%= Html.TextArea("reason", new { @style = "width:94%;height:50px;" })%>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="left" style="padding-left:0px">
            <table id="tblDateOff">
            </table>
        </td>
    </tr>
    <tr class="trTotal" style="display:none">
        <td class="label required" colspan="4" style="color: Red; padding-left: 295px; text-align: left">
            Total Hours Submitted:
            <label id="totalHoursOff">
            </label>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Comment
        </td>
        <td class="input" colspan="3">
            <%= Html.TextArea("HRComment", new { @style = "width:94%;height:50px;" })%>
        </td>
    </tr>        
</table>
<div class="bottom_button">
    <input type="submit" class="submit" value="" />
    <input type="button" class="cancel" value="" alt=""  onclick="CRM.closePopup();" />
</div>
<% } %>