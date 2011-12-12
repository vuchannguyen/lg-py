<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
<%=Html.Hidden("ID") %>
<%=Html.Hidden("UpdateDate") %>
<%=Html.Hidden("hidCallerPage", Request.UrlReferrer == null ? Url.Action("Index") : Request.UrlReferrer.ToString()) %>
<% SR_ServiceRequest sr = ViewData.Model as SR_ServiceRequest; %>
<table class="edit" id="list" width="1024px" style="margin-top:10px">
    <tr>
        <td class="label required">Category<span>*</span></td>
        <td class="input" style="width:200px">
            <%=Html.DropDownList(CommonDataKey.SR_CATEGORY_LIST, null, Constants.SR_LIST_CATEGORY_LABEL, new { @style="width:170px"})%>
        </td>
        <td class="label required">Sub Category<span>*</span></td>
        <td class="input"  style="width:200px">
            <%=Html.DropDownList(CommonDataKey.SR_SUB_CATEGORY_LIST, null, Constants.SR_LIST_SUB_CATEGORY_LABEL, new { @style="width:170px"})%>
        </td>
        <td style="width:400px"></td>
    </tr>
    <tr>
        <td class="label required">Title<span>*</span></td>
        <td class="input" colspan="4">
            <%=Html.TextBox("Title", null, new { @maxlength = "255", @style = "width:850px;" })%>
        </td>
    </tr>
    <tr>
        <td class="label required" style="vertical-align:top">Description<span>*</span></td>
        <td class="input" colspan="4">
            <%=Html.TextArea("Description", null, new { @maxlength = "2000", @style = "width:850px; height: 100px" })%>
        </td>
    </tr>
    <tr>
        <td class="label">Submitter</td>
        <td class="input">
            <%=Html.TextBox("SubmitUser", sr == null ? HttpContext.Current.User.Identity.Name : null, 
                new { @style="width:190px", @readonly="readonly"})%>
        </td>
        <td class="label required">Requestor<span>*</span></td>
        <td class="input">
            <%=Html.TextBox("RequestUser", sr == null ? HttpContext.Current.User.Identity.Name : null,
                new { @style = "width:165px" })%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">CC List</td>
        <td class="input" colspan="4" style="padding-right:45px">
            <%=Html.TextBox("txtCCList", ViewData.Model==null ? null : 
                (ViewData.Model as SR_ServiceRequest).CCList, new { @style="width:200px" })%>
            <%=Html.Hidden("CCList")%>
            <br/><%= Constants.SAMPLE_AUTO_COMPLETE %>
        </td>
    </tr>
    <tr>
        <td class="label">Urgency</td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.SR_URGENCY_LIST, null, Constants.SR_LIST_URGENCY_LABEL, new { @style="width:190px"})%>
        </td>
        <td class="label">Related SR</td>
        <td class="input">
            <%=Html.TextBox("ParentID", sr != null ?
            (sr.ParentID.HasValue ? Constants.SR_SERVICE_REQUEST_PREFIX + sr.ParentID : "") : "",
                new { @style = "width:190px", @readonly = "readonly" })%>
        </td>
        <td style="text-align:left">
            <button class="icon select" 
                onclick="CRM.popup('/Common/ListServiceRequest/true', 'Select Service Request' ,'940')"
                title="Select related SR" type="button"> </button>
            <button class="icon remove" onclick="$('#ParentID').val('');" 
                title="Remove Related SR" type="button"> </button>
        </td>
    </tr>
    <tr>
        <td class="label">Due Date</td>
        <td class="input">
            <%  
                string duedate = (sr!=null && sr.DueDate.HasValue) ? sr.DueDate.Value.ToString("dd/MM/yyyy") : "";
                Response.Write(Html.TextBox("DueDate", duedate, new { @style = "width:65px" }));
                string selectedHour = "";
                if (sr != null && sr.DueDate.HasValue)
                    selectedHour = sr.DueDate.Value.ToString(Constants.SR_DUE_HOUR_FORMAT);
                var hourList = new SelectList(CommonFunc.GetHoursList(0, 24, 15, Constants.SR_DUE_HOUR_FORMAT), "Text", "Text",selectedHour);
                Response.Write(Html.DropDownList(CommonDataKey.SR_HOURS_LIST, hourList, new { @style="width:75px"}));
            %>
        </td>
    </tr>
     <tr>
        <td class="label required">Status<span>*</span></td>
        <td class="input" style="width:200px">
            <%=Html.DropDownList(CommonDataKey.SR_STATUS_LIST, null, Constants.SR_LIST_STATUS_LABEL, new { @style="width:170px"})%>
        </td>
        <td class="label required assignLabel">Assigned To<span>*</span></td>
        <td class="input"  style="width:200px">
            <%=Html.DropDownList(CommonDataKey.SR_ASSIGNED_TO_LIST, null, Constants.SR_LIST_ASSIGNED_TO_LABEL, new { @style = "width:170px" })%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Attachments</td>
        <td class="input" colspan="2">
            <%
                int fileCount = 0;
                string fileButtonDisplay = "none";
                if (sr != null)
                {
                    Response.Write(Html.Hidden("hidRemovedFiles"));
                    fileCount = CommonFunc.SR_ShowUploadedFile(sr.Files, 35);
                }
                if (fileCount < Constants.SR_UPLOAD_MAX_QUANTITY)
                    fileButtonDisplay = "";
            %>
            <input class="attachedFile uploadMore" style="display:<%=fileButtonDisplay%>" type="file" name="fFile" size="32"/>
        </td>
        <td colspan="2"  style="text-align:left; vertical-align:top;">
            <div style="display:<%=fileButtonDisplay%>" class="uploadMore">
                <button class="icon plus" id="btnAddFile" title="Add New Upload" type="button"> </button>
                <button class="icon minus" id="btnRemoveFile" title="Remove" type="button"> </button>
            </div>
        </td>
    </tr>
    <tr>
        <td style="text-align:center; vertical-align:bottom; padding-top:10px" colspan="5">
            <input class="submit" type="submit" alt="" value="" />
            <input class="cancel" id="btnCancel" type="button" alt="" value="" />
        </td>
    </tr>
</table>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    //var IS_FORM_SUBMITTED = false;
    function reloadAssignList(newStatus) {
        if (isManagerData != (newStatus == '<%=Constants.SR_STATUS_TO_BE_APPROVED%>'))
            return true;
        return false;
    }
    function displayAssignList(isShow) {
        var assignList = $("#<%=CommonDataKey.SR_ASSIGNED_TO_LIST%>");
        if (isShow) {
            assignList.removeAttr("disabled");
        }
        else {
            assignList.attr("disabled", "disabled");
            assignList.val("");
        }
    }
    var isManagerData = false;
    $(document).ready(function () {
        isManagerData = $("#<%=CommonDataKey.SR_STATUS_LIST%>").val() == '<%=Constants.SR_STATUS_TO_BE_APPROVED%>';
        $("#DueDate").datepicker();
        $("#frmSRForm").validate({
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
                '<%=CommonDataKey.SR_CATEGORY_LIST%>': { required: true },
                '<%=CommonDataKey.SR_SUB_CATEGORY_LIST%>': { required: true },
                '<%=CommonDataKey.SR_STATUS_LIST%>': { required: true },
                '<%=CommonDataKey.SR_ASSIGNED_TO_LIST%>': { required: true },
                RequestUser: { required: true },
                DueDate: { maxlength: 10, checkDate: true },
                Title: { required: true, maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_ServiceRequest(), "Title")%>') },
                Description: { required: true, maxlength: parseInt('<%=CommonFunc.GetLengthLimit(new SR_ServiceRequest(), "Description")%>') }
            }
        });
        $("#RequestUser").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=EmployeeWithID',
            { employee: true });
        $("#txtCCList").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=EmployeeWithID',
            { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";",
                faceBook: true, hidField: "#CCList", employee: true
            });
        $("#<%=CommonDataKey.SR_CATEGORY_LIST%>").change(function () {
            var selectedValue = $(this).val();
            var optionTemplate = "<option value='{0}'>{1}</option>";
            var labelOption = $.format(optionTemplate, "", "<%=Constants.SR_LIST_SUB_CATEGORY_LABEL%>");
            var subCateObj = $("#<%=CommonDataKey.SR_SUB_CATEGORY_LIST%>");
            subCateObj.html(labelOption);
            $.ajax({
                async: false,
                cache: false,
                type: "GET",
                dataType: "json",
                timeout: 1000,
                url: '<%=Url.Action("CategoryOnChange") + "/"%>' + selectedValue,
                error: function () {
                    newOption = $.format(optionTemplate, "", "Error");
                    subCateObj.html(newOption);
                },
                success: function (data) {
                    $(data.subCategories).each(function () {
                        var newOption = $.format(optionTemplate, this["ID"], this["Name"]);
                        subCateObj.append(newOption);
                    });
                }
            });
        });
        $("#<%=CommonDataKey.SR_STATUS_LIST%>").change(function () {
            var selectedValue = $(this).val();
            var optionTemplate = "<option value='{0}'>{1}</option>";
            var optionTemplateSelected = "<option value='{0}' selected>{1}</option>";
            var labelOption = $.format(optionTemplate, "", "<%=Constants.SR_LIST_ASSIGNED_TO_LABEL%>");
            var assignedToObj = $("#<%=CommonDataKey.SR_ASSIGNED_TO_LIST%>");

            if (selectedValue == '') {
                displayAssignList(true);
            }
            else if (selectedValue == '<%=Constants.SR_STATUS_CLOSED%>') {
                displayAssignList(false);
            }
            else {
                displayAssignList(true);
                if (reloadAssignList(selectedValue)) {
                    CRM.loading();
                    assignedToObj.html(labelOption);
                    var requestor = $("#RequestUser").val() == "" ? $("#SubmitUser").val() : $("#RequestUser").val();
                    $.ajax({
                        async: false,
                        cache: false,
                        type: "GET",
                        dataType: "json",
                        timeout: 1000,
                        url: '<%=Url.Action("StatusOnChange") + "/"%>' + selectedValue + "?requestor=" + requestor,
                        error: function () {
                            var newOption = $.format(optionTemplate, "", "Error");
                            assignedToObj.html(newOption);
                        },
                        success: function (data) {
                            $(data.users).each(function () {
                                var newOption = "";
                                if (this != data.selected)
                                    newOption = $.format(optionTemplate, this, this);
                                else
                                    newOption = $.format(optionTemplateSelected, this, this);
                                assignedToObj.append(newOption);
                                isManagerData = selectedValue == '<%=Constants.SR_STATUS_TO_BE_APPROVED%>';
                            });
                            CRM.completed();
                        }
                    });
                }
            }
            currentStatus = selectedValue;
        });
        $("#btnAddFile").click(function () {
            var numFile = $("input[type='file']").length + $("table.attachedFile").length;
            if (numFile < '<%=Constants.SR_UPLOAD_MAX_QUANTITY%>') {
                $("input[type='file']").last().after(
                '<input class=\"attachedFile uploadMore\" style="display:block" type="file" name="fFile' + numFile + '" size="32"/>');
            }
        });
        $("#btnRemoveFile").click(function () {
            var numFile = $("input[type='file']").length;
            if (numFile > 1)
                $("input[type='file']").last().remove();
        });
        $("#frmSRForm").submit(function () {
            if ($(this).valid())
                $("input.submit").attr("disabled", "disabled");
        });
        $("#btnCancel").click(function () {
            var returnUrl = $("#hidCallerpage").val();
            if (returnUrl == undefined || returnUrl == "")
                returnUrl = '<%=Url.Action("Index")%>';
            window.location = returnUrl;
        });
    });
    
</script>