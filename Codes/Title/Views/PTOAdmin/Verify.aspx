<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CRM.Models.PTO>" %>
    <style type="text/css">
        .datetime
        {
            width:70px
        }
        .datetimecell
        {
            width:120px;
        }
    </style>
    <script type="text/javascript">
        var sDateOffTableSelector = "#tblDateOff";
        var sDateOffInputNamePrefix = "txtDateOff_";
        var sDateOffInputSelector = "input[name^='" + sDateOffInputNamePrefix + "']";
        var sHoursInputNamePrefix = "txtHours_";
        var isHoursType = '<%=Model.PTO_Type.IsHourType%>' == 'True';

        function setValidation(row) {
            row.find(sDateOffInputSelector).each(function () {
                $(this).rules("add", { required: true, checkDate: true });
            });
//            row.find("input[name^='txtDateOffFrom']").rules("add", { required: true, checkDate: true });
//            row.find("input[name^='txtDateOffTo']").rules("add", { required: true, checkDate: true });
        }
        $(document).ready(function () {
            $("#verifyForm").validate({
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
                    HRComment: { maxlength: 500 },
                    PTOType_ID: { required: true },
                    txtDateOffFrom: { required: true, checkDate: true, compareDate: ["#txtDateOffTo", "lt", "From Date", "To Date"] },
                    txtDateOffTo: { required: true, checkDate: true }
                },
                submitHandler: function (form) {
                    formSubmitHandler(form);
                }
            });
            //setInputDateOffType(ptoTypeOf($("#type").val()));
            //alert($("#type").val());
            var iType = ptoTypeOf('<%=Model.PTOType_ID%>');
            //Set the current pto type id to hidden field
            $("#hidPTO_Type").val(iType);
            //setInputDateOffType(iType);
            //disableAllControl();
            //setLastRowValidation(iType);
            $("#txtDateOffFrom").datepicker();
            $("#txtDateOffTo").datepicker();
            $("#<%=CommonDataKey.PTO_TYPE_LIST%>").change(function () {
                var oldType = $("#hidPTO_Type").val();
                var typeID = $(this).val();
                if (typeID == "") {
                    disableAllControl();
                }
                else if (ptoTypeOf(typeID) != oldType) {
                    setInputDateOffType(ptoTypeOf(typeID), false);
                }
                else {
                    enableAllControl();
                }
            });
            $("#<%=CommonDataKey.PTO_TYPE_PARENT_ID%>").change(function () {
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
            
            //setNumberOfDateOff();
            $(function () {
                var dateoff = '<%=ViewData["ptoDateOff"]%>'.split(',');
                var datehourfrom = '<%=ViewData["ptoHourFrom"]%>'.split(',');
                var datehourto = '<%=ViewData["ptoHourTo"]%>'.split(',');
                var dateispay = '<%=ViewData["ptoIsPay"]%>'.split(',');
                if (dateoff.length > 0 && dateoff[0] != '') {
                    $("tr.trIsPayTitle").show();
                    for (index = 0; index < dateoff.length; index++) {
                        if (dateoff[index] != '') {
                            addDateOff(index == 0 ? null : $(sDateOffTableSelector).find("tr:last").attr("id"),
                            dateoff[index], datehourfrom[index], datehourto[index], Boolean(parseInt(dateispay[index])));
                        }
                    }
                }
                $(sDateOffTableSelector).find(sDateOffInputSelector).each(function () {
                    $(this).datepicker();
                });
                $(sDateOffTableSelector + " tr").each(function (index) {
                    if ((index > 0 && isHoursType) || !isHoursType) {
                        setValidation($(this));
                    }
                });
//                var monthYear = $("#txtFilterDate").val();
//                $("#verifyForm").append("<input type='hidden' name='hidMonthYear' value='" + monthYear + "' />");
                ShowTooltip($("a.man_desc"), $("#shareit-box"), "/Portal/TrainingCenter/EmployeeToolTip");
                ShowTooltip($("a.submit_desc"), $("#shareit-box"), "/Portal/TrainingCenter/EmployeeToolTip");
            });
        });
    </script>
    <%= Html.ValidationSummary() %>
    <%if (Model == null) %>
    <%{ %>
        <div id="summary" class="msgError">
            <%=String.Format(MessageConstants.E0005, "The PTO item", "System") %>
        </div>
    <%} %>
    <%else %>
    <%{ %>
    <%using (Html.BeginForm("Verify", "PTOAdmin", FormMethod.Post,
        new { id = "verifyForm", @class = "form" }))
      {
          string empName = Model.Employee.FirstName +
              (string.IsNullOrEmpty(Model.Employee.MiddleName) ? "" : " " + Model.Employee.MiddleName) + " " +
              Model.Employee.LastName;
          string managerLoginName = new EmployeeDao().FullName(Model.SubmitTo, Constants.FullNameFormat.FirstMiddleLast);
          List<PTO_Detail> listPTO_Detail = ((List<PTO_Detail>)ViewData[CommonDataKey.PTO_DETAILS]);
    %>
    <input type="hidden" name="hidPTO_ID" value='<%=Model.ID %>' />
    <input type="hidden" id="hidIsCompanyPay" value='<%=Model.PTO_Type.IsCompanyPay %>' />
    <input type="hidden" id="hidPTO_Type"/>
    <input type="hidden" id="hidPTO_TypeIDs_IsHour" value='<%= ViewData[CommonDataKey.PTO_IDS_IS_HOUR_TYPE] %>'/> 
    <input type="hidden" id="hidNumberOfDateOff" name="hidNumberOfDateOff" value='<%=listPTO_Detail.Count%>'/>
    <input type="hidden" name="hidUpdateDate" value='<%=Model.UpdateDate.ToString()%>'/>
    <input type="hidden" id="hIndex" name="hIndex" value="0" />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
        <tr>
            <td class="label required" >
                Employee
            </td>
            <td class="input" style="width:180px">
                <input id="txtEmployeeName" size="24" name="txtEmployeeName" value='<%=empName + " - " + Model.Employee.ID%>' 
                    disabled="disabled" type="text" />
            </td>
            <td class="label" colspan="2" style="text-align:left !important;">
                <span id="submitDesc" style="vertical-align:middle; margin-left:0px">
                    More details about <a id='<%=Model.Submitter%>' class='man_desc' href='#'><%=CommonFunc.GetUserNameLoginByEmpID(Model.Submitter) %></a>
                </span>
            </td>
        </tr>
        <tr >
            <td class="label">
                Balance YTD
            </td>
            <td class="input">
                <b style='color:red'><%=ViewData[CommonDataKey.PTO_VACATION_BALANCE]%></b> hour(s)
            </td>
            <td class="label">
                Used this month
            </td>
            <td class="input">
                <b style='color:red'><%=ViewData[CommonDataKey.PTO_USED_HOURS]%></b>  hour(s)
            </td>
        </tr>
        
        <tr>
            <td class="label required">
                Submitted to
            </td>
            <td class="input">
                <input disabled="disabled" size="24" value='<%=managerLoginName %>' 
                    name ="txtManagerLoginName" type="text" />
            </td>
            <td class="label" colspan="2" style="text-align:left !important;">
                <span id="manDesc" style="vertical-align:middle; margin-left:0px">
                    More details about <a id='<%=Model.SubmitTo%>' class='man_desc' href='#'><%=CommonFunc.GetUserNameLoginByEmpID(Model.SubmitTo) %></a>
                </span>
            </td>
        </tr>
        <tr>
            <td class="label required" >
                Reason
            </td>
            <td class="input" colspan="3">
                <%= Html.TextArea("Reason", new { @style = "width: 370px;height:50px;", @disabled = "" })%>
            </td>
        </tr>
        <tr>
            <td class="label required" >
                Manager's comment
            </td>
            <td class="input" colspan="3">
                <%= Html.TextArea("ManagerComment", new { @style = "width: 370px;height:50px;", @disabled = "" })%>
            </td>
        </tr>
        <tr>
            <td class="label required" >
                Type
            </td>
            <td colspan="3" class="input">
               <%= Html.DropDownList("PTOType_Parent_ID", null, Constants.PTO_PARENT_FIRST_TYPE, new { @style = "width:200px"})%>
            <%= Html.DropDownList(CommonDataKey.PTO_TYPE_LIST, null, Constants.PTO_FIRST_TYPE, new { @style = "width:185px"})%>
            </td>
            
        </tr>
        <tr class="trIsPayTitle label required" style="display:none">
            <td colspan="4" style="padding-right:40px">
                Company Pay
            </td>
        </tr>
        <tr>
            <td colspan="4" align="left" style="padding-left:0px">
                <table id="tblDateOff" style="margin-left:0px; padding-left:0px; border-width: 0px">
                  
                    <%  
                        bool isCompanyPay = false;
                        string isCompanyPayDisplay = "none";
                        if (!Model.PTO_Type.IsHourType) %>
                    <%  {
                            PTO_Detail ptoDetail = listPTO_Detail[0];
                            isCompanyPay = ptoDetail.IsCompanyPay;
                            isCompanyPayDisplay = "";
                    %>
                    <tr>
                        <td class="label required" style="margin-left:0">
                            From Date<span>*</span>
                        </td>
                        <td class = "input" style="width:125px !important">
                            <input type="text"  name="txtDateOffFrom" id="txtDateOffFrom" 
                                value='<%=ptoDetail.DateOffFrom.Value.ToString(Constants.DATETIME_FORMAT) %>' 
                                class="datetime" maxlength="10"/>
                        </td>
                        <td class="label required" style="width:50px">
                            To Date<span>*</span>
                        </td>
                        <td class = "input" style="width:125px !important">
                            <input type="text"  name="txtDateOffTo" id="txtDateOffTo" 
                                value='<%=ptoDetail.DateOffTo.Value.ToString(Constants.DATETIME_FORMAT) %>' 
                                class="datetime" maxlength="10"/>
                        </td>
                    </tr>
                    
                    <%  } %>
                    
                </table>
            </td>
        </tr>
        <tr class="trIsPay_NotHourType" style="display:<%=isCompanyPayDisplay%>">
            <td class="label"  >
                Company pay
            </td>
            <td class="label" style="text-align:left">
                <%=Html.CheckBox("ckbIsCompanyPay_Date", isCompanyPay, 
                    new { @title = "Company pay"})%>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr class="trTotal" style="display:none">
            <td class="label required" colspan="4" style="color: Red; padding-left: 295px; text-align: left">
                Total Hours Submitted:
                <label id="totalHoursOff">
                </label>
            </td>
        </tr>
        
        <tr>
            <td class="label required" >
                Comment
            </td>
            <td class="input" colspan="3">
                <%= Html.TextArea("HRComment", new { @style = "width:370px;height:50px;" })%>
            </td>
        </tr>
        <tr>
            <td class="label"  >
                
            </td>
            <td class="input" style="text-align:left" colspan="3">
                <%=Html.CheckBox("ckbVerified", Model.Status_ID == Constants.PTO_STATUS_VERIFIED, new { @title = "Company pay"})%> Verified
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" >
                <input type="submit" class="verify" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup();" />
            </td>
        </tr>
    </table>
    <% } %>
    <%} %>
