<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var classObj = ViewData.Model as Training_Class;
    int type = 0;
    string courseName = "";
    string instructors = "";
    string startDate = "";
    string resultType = "";
    bool isActive = true;
    if (classObj != null)
    {
        isActive = classObj.Active;
        type = classObj.Training_Course.TypeOfCourse;
        courseName = classObj.Training_Course.Name;
        instructors = classObj.Instructors;
        resultType = classObj.ResultType.HasValue ? classObj.ResultType.Value.ToString() : "";
        startDate = classObj.StartDate.ToString(Constants.DATETIME_FORMAT);
        Response.Write(Html.Hidden("UpdateDate"));
        Response.Write(Html.Hidden("RealId", classObj.ID));
    }
    else
    {
        type = ConvertUtil.ConvertToInt(Request["type"]);
        Response.Write(Html.Hidden("type"));
    }
%>
<table width="100%" class="edit">
    <tr>
        <td class="label required" style="width:120px !important">Class ID<span>*</span></td>
        <td class="input" style="width:120px !important" >
            <%=Html.TextBox("ClassId", null, new { @style="width:110px"})%>
        </td>
        <td class="label required">Course Name<span>*</span></td>
        <td class="input">
            <%=Html.TextBox("CourseName", courseName, new { @style = "width:130px", @readonly = "readonly" })%>
            <button class="icon select" 
                onclick="CRM.pInPopup('/TrainingCenterAdmin/ListCourse/?courseType=<%=type%>', 'Select Course' ,'800')" 
                title="Select Course" type="button"></button>
            <button class="icon remove" 
                onclick="$('#CourseName').val(''); $('#CourseId').val('');" 
                title="Remove Course" type="button"></button>
            <%=Html.Hidden("CourseId") %>
        </td>
    </tr>
    <tr>
        <td class="label required">Registration Status<span>*</span></td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.TRAINING_CENTER_REG_STATUS, null,
                Constants.TRAINING_CENTER_LIST_REG_STATUS_LABEL, new { @style = "width:115px" })%>
        </td>
        <td class="label required">Start Date<span>*</span></td>
        <td class="input"  style="width:170px">
            <%=Html.TextBox("StartDate", startDate, new { @style = "width:110px" })%>
        </td>
       
    </tr>
    <tr>
        <td class="label required">Instructors<span>*</span></td>
        <td class="input" colspan = "3">
            <span style="display:inline-block"><%=Html.TextBox("txtInstructors", instructors, new { @style = "width:450px" })%></span>
            <span style="display:inline-block; position:relative;top:-15px"><%=Html.Hidden("Instructors", instructors)%></span>
        </td>
    </tr>
    <%--<%if (type == Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH)
      {%>--%>
    <tr>
        <td class="label">Date Time</td>
        <td class="input" colspan = "3">
            <%=Html.TextBox("ClassTime", null, new { @style = "width:460px" })%>
        </td>
    </tr>
    <%--<%} %>--%>
    <tr>
        <td class="label">Active</td>
        <td class="input">
            <%=Html.CheckBox("Active", isActive)%>
        </td>
        <td class="label"># Of Attendees</td>
        <td class="input">
            <%=Html.TextBox("AttendeeQuantity", null, new { @style = "width:70px" })%>
        </td>
        
    </tr>
    <tr>
        <td class="label required" style="vertical-align:top">Result Form<span>*</span></td>
        <td class="input" colspan ="3">
            <span style="padding-right:10px"  onclick="checkRadio(0);">
                <%=Html.RadioButton("ResultType", Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_SCORE)%> Score
            </span>
            <span style="padding-right:10px"  onclick="checkRadio(1);">
                <%=Html.RadioButton("ResultType", Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_PASS_FAIL)%> Pass/Fail
            </span>
            <span style="padding-right:10px"  onclick="checkRadio(2);">
                <%=Html.RadioButton("ResultType", Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_COMMENT)%> Comment
            </span>
            <span style="padding-right:10px" onclick="checkRadio(3);">
                <%=Html.RadioButton("ResultType", Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_NO_RESULT)%> No Result
            </span>
            <span>
                <%=Html.Hidden("hidResultType", resultType)%>
            </span>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Notes</td>
        <td class="input" colspan ="3">
            <%=Html.TextArea("Notes", new { @style = "width:460px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align:center; vertical-align:bottom; padding-top:20px">
            <input class="save" type="submit" value="" alt="" />
            <input class="cancel" type="button" value="" alt="" onclick="CRM.closePopup();" />
            <%=Html.Hidden("page",Request["page"]) %>
        </td>
    </tr>
</table>
<script src="../../Scripts/TrainingCenter.js" type="text/javascript"></script>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    ul.as-selections
    {
        width: 460px;    
    }
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
    function checkRadio(radIndex){
        var rads = $("input[name='ResultType']");
        $(rads[radIndex]).attr("checked", "checked");
    }
    $(document).ready(function () {

        <%
            var newClass = new Training_Class();
        %>
        $("#txtInstructors").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=UserAdmin',
            { max: 100, highlightItem: true, multiple: true, multipleSeparator: ";",
                faceBook: true, hidField: "#Instructors", employee: true
            });

        $("#StartDate").datepicker();

        $("#frmCourse").validate({
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
                ClassId: { required: true, validID: true, maxlength: '<%=CommonFunc.GetLengthLimit(newClass, "ClassId")%>' },
                CourseId: { required: true},
                StartDate: { required: true, checkDate:true, maxlength:10},
                Instructors: { required: true},
                hidResultType: { required: true},
                AttendeeQuantity: { maxlength: 3, integer:true },
                '<%=CommonDataKey.TRAINING_CENTER_REG_STATUS%>': { required: true },
                Notes: { maxlength: '<%=CommonFunc.GetLengthLimit(newClass, "Notes")%>' }
            }
        });
        $("#frmCourse").submit(function () {
            if ($(this).valid())
                $(this).find("input[type='submit']").attr("disabled", "disabled");
        });
        $.each($("input[name='ResultType']"), function(){
            $(this).click(function(){
                $("#hidResultType").val($(this).val());
            });
        });
    });
</script>