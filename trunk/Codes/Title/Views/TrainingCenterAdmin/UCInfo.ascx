<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    var course = ViewData.Model as Training_Course;
    string keyTrainers = "";
    bool isActive = true;
    if (course == null)
    {
        Response.Write(Html.Hidden("TypeOfCourse", Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL));
    }
    else
    {
        keyTrainers = course.KeyTrainers;
        isActive = course.Active;
        Response.Write(Html.Hidden("TypeOfCourse"));
        Response.Write(Html.Hidden("UpdateDate"));
        Response.Write(Html.Hidden("RealId", course.ID));
    }
%>
<table width="100%" class="edit">
    <tr>
        <td class="label required">Course ID<span>*</span></td>
        <td class="input" style="width:170px">
            <%=Html.TextBox("CourseId", null, new { @style="width:130px"})%>
        </td>
        <td class="label required">Course Name<span>*</span></td>
        <td class="input" style="width:170px">
            <%=Html.TextBox("Name", null, new { @style = "width:130px" })%>
        </td>
        <td ></td>
    </tr>
    <tr>
        <td class="label required">Type<span>*</span></td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.TRAINING_CENTER_COURSE_TYPE, null,
                Constants.TRAINING_CENTER_LIST_COURSE_TYPE_LABEL, new { @style = "width:135px" })%>
        </td>
        <td class="label required">Status<span>*</span></td>
        <td class="input">
            <%=Html.DropDownList(CommonDataKey.TRAINING_CENTER_COURSE_STATUS,null,
                Constants.TRAINING_CENTER_LIST_COURSE_STATUS_LABEL, new { @style = "width:135px" })%>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label">Active</td>
        <td class="input">
            <%=Html.CheckBox("Active", isActive)%>
        </td>
        <td class="label">Duration</td>
        <td class="input">
            <%=Html.TextBox("Duration", null, new { @style = "width:50px" })%> Hour(s)
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Objectives</td>
        <td class="input" colspan ="4">
            <%=Html.TextArea("Objectives", new { @style = "width:497px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Course Overview</td>
        <td class="input" colspan ="4">
            <%=Html.TextArea("Overview", new { @style = "width:497px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Registration Requirements</td>
        <td class="input" colspan ="4">
            <%=Html.TextArea("Requirements", new { @style = "width:497px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td class="label">Key Trainers</td>
        <td class="input" colspan ="4">
            <span style="display:inline-block">
                <%=Html.TextBox("txtKeyTrainers", keyTrainers, new { @style = "width:130px" })%>
            </span>
            <span style="display:inline-block; position:relative; top:-15px">
                <%=Html.Hidden("KeyTrainers", keyTrainers) %>
            </span>
        </td>
    </tr>
    <tr>
        <td class="label" style="vertical-align:top">Notes</td>
        <td class="input" colspan ="4">
            <%=Html.TextArea("Notes", new { @style = "width:497px; height:50px" })%>
        </td>
    </tr>
    <tr>
        <td colspan="5" style="text-align:center; vertical-align:bottom; padding-top:20px">
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
        width: 496px;    
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
    $(document).ready(function () {
        <%
            var newCourse = new Training_Course();
        %>
        $("#txtKeyTrainers").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=UserAdmin',
            { max: 10, highlightItem: true, multiple: true, multipleSeparator: ";",
                faceBook: true, hidField: "#KeyTrainers", employee: true
            });
//        $.validator.addMethod(
//            "validID",
//            function (value, element) {
//                var re = new RegExp("^[0-9a-zA-Z_\-]+$");
//                if (!re.test(value))
//                    return false;
//                return true;
//            },
//            CRM.format(E0030, "Course ID") + 
//                "</br>Do not allow space and special character (except: \"_\" and \"-\").</br>" +
//                "<u>Example:</u> </br>  -Correct: \"<span style='color:red'>1-Xyz_2</span>\"</br>  -Incorrect: \"<span style='color:red'>a b</span>\""
//        );
//        if ('<%=ViewData.Model%>' == '') {
//            $("#EmployeeName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?func=Employee&type=2&suffixId=true',
//               { employee: true, subField: "#EmployeeId", multiData: true });
//        }
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
                CourseId: { required: true, validID: true, maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "CourseId")%>' },
                Name: { required: true, maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "Name")%>' },
                Duration: { number: true, min: 1 },
                Objectives:{maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "Objectives")%>'},
                Overview: { maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "Overview")%>' },
                Requirements: { maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "Requirements")%>' },
                KeyTrainers: { maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "KeyTrainers")%>' },
                '<%=CommonDataKey.TRAINING_CENTER_COURSE_TYPE%>': { required: true },
                '<%=CommonDataKey.TRAINING_CENTER_COURSE_STATUS%>': { required: true },
                Notes: { maxlength: '<%=CommonFunc.GetLengthLimit(newCourse, "Notes")%>' }
            }
        });
        $("#frmCourse").submit(function () {
            if ($(this).valid())
                $(this).find("input[type='submit']").attr("disabled", "disabled");
        });
    });
</script>