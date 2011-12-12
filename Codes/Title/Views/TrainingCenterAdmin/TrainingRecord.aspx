<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= TempData["Message"]%>
<% Response.Write(Html.Hidden("ID", ViewData[CommonDataKey.TRAINING_CENTER_EMP_ID])); %>
<%
    EmployeeDao empDao = new EmployeeDao();
    DepartmentDao depDao = new DepartmentDao ();
    string currentSkillScore = ViewData[CommonDataKey.TRAINING_CENTER_SCORE_SKILL].ToString();
    string currentVerbalScore = ViewData[CommonDataKey.TRAINING_CENTER_SCORE_VERBAL].ToString();
    var currentVerbalLevel = ViewData[CommonDataKey.TRAINING_CENTER_LEVEL_VERBAL];

%>
<% 
    List<sp_TC_GetClassEmpAttendResult> listEClass = (List<sp_TC_GetClassEmpAttendResult>)ViewData[Constants.TRAINING_CENTER_LIST_E_CLASS_ATTEND];
    List<sp_TC_GetClassEmpAttendResult> listProfClass = (List<sp_TC_GetClassEmpAttendResult>)ViewData[Constants.TRAINING_CENTER_LIST_PROF_CLASS_ATTEND];
    List<sp_TC_GetClassEmpNotAttendResult> listEClassNotAtt = (List<sp_TC_GetClassEmpNotAttendResult>)ViewData[Constants.TRAINING_CENTER_LIST_E_CLASS_NOT_ATTEND];
    List<sp_TC_GetClassEmpNotAttendResult> listProfClassNotAtt = (List<sp_TC_GetClassEmpNotAttendResult>)ViewData[Constants.TRAINING_CENTER_LIST_PROF_CLASS_NOT_ATTEND];
    var listExam = ViewData[Constants.TRAINING_CENTER_LIST_EXAM_ATTENDANCE] as List<sp_GetListEnglishExamOfEmployeeResult>;
    Employee emp = ViewData.Model as Employee;
%>

<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="Button5" class="minus icon" onclick="toggleContent(this)" type="button"> </button>
        Employee Information
    </legend>
    <div>
         <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label" style="width: 150px;">
                    Full Name
                </td>
                <td class="input" colspan="2">
                    <%=emp.FirstName + " " + emp.MiddleName + " " +  emp.LastName%>
                </td>
                <td class="label" rowspan="9" style="border-bottom:none;text-align:center; vertical-align:middle">
                    <div class="chart">
                        <div id="chart-container" style="width: auto; height: 200px;">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Department
                </td>
                <td class="input" colspan="2">
                    <%=emp.Department.ParentId.HasValue ? depDao.GetById(emp.Department.ParentId.Value).DepartmentName : ""%>
                </td>
                
            </tr>
            <tr>
                <td class="label">
                    Direct Manager
                </td>
                <td class="input" colspan="2">
                    <%= empDao.FullName(emp.ManagerId , Constants.FullNameFormat.FirstMiddleLast)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Project
                </td>
                <td class="input" colspan="2">
                    <%=emp.Project%>
                </td>
            </tr>
            <tr>
                <td colspan="3" class="input" style="color: #0066CC; font-weight:bold; padding: 6px;">
                    Current English Score
                </td>
            </tr>
            <tr >
                <td class="label" style="border-right: 1px solid #CCCCCC"></td>
                <td class="label" style="text-align:center;width:150px;border-right: 1px solid #CCCCCC">Score</td>
                <td class="label" style="text-align:center;width:150px;border-right: 1px solid #CCCCCC">Level</td>
            </tr>
            <tr >
                <td class="label" style="border-right: 1px solid #CCCCCC">
                    Skill
                </td>
                <td class="input"  style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentSkillScore%>
                </td>
                <td class="input"  style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%=CommonFunc.GetEngLishSkillLevel(ConvertUtil.ConvertToInt(currentSkillScore))%>
                </td>
            </tr>
            <tr>
                <td class="label" style="border-right: 1px solid #CCCCCC">
                    Verbal
                </td>
                <td class="input" style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentVerbalScore%>
                </td>
                <td class="input" style="text-align:center;border-right: 1px solid #CCCCCC">
                    <%= currentVerbalLevel != null ? currentVerbalLevel : ""%>
                </td>
            </tr>
            <tr class="last">
                <td class="input" colspan = "3" style="padding-right:10px; text-align:right">
                    <a href="/TrainingCenterAdmin/EnglishLevelMapping" >View English Level Mapping Table</a>
                </td>
            </tr>
        </table>
        
    </div>
</fieldset>
<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="Button6" class="plus icon" onclick="toggleContent(this)" type="button"> </button>
        English Exam Attendance Record
    </legend>
    <div id="listExam" style="width: 100%; display:none">        
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
            <tr>
                <th class="gray" width="45%">
                    Exam
                </th>
                <th class="gray" width="15%">
                    Date
                </th>
                <th class="gray" style="width:10%;text-align:center;">
                    Skill Score
                </th>
                <th class="gray" style="width:10%;text-align:center;">
                    Skill Level
                </th>
                <th class="gray" style="width:10%;text-align:center;">
                    Verbal Score
                </th>
                <th class="gray" style="width:10%;text-align:center;">
                    Verbal Level
                </th>
            </tr>
            <% foreach (var item in listExam)
                {
                    double? skillScore = CommonFunc.Average(item.ScoreComprehensionSkill, item.ScoreListeningSkill,
                        item.ScoreMultipleChoice, item.ScoreSentenceCorrection, item.ScoreWritingSkill);
                    int skill = 0;
                    if(skillScore.HasValue)
                        skill = (int)Math.Floor(skillScore.Value + 0.5);
                    string verbal = "";
                    if (item.VerbalMarkType == Constants.LOT_VERBAL_MARK_TYPE_TOEIC && item.ScoreVerbal.HasValue)
                        verbal = item.ScoreVerbal.Value.ToString();
                    float? verbalLevel = CommonFunc.GetVerbalLevel(item);
            %>
            <tr>
                <td>
                    <%=CommonFunc.Link("", "", Url.Action("CandidateTestDetail", "Exam", 
                        new { @id = item.CandidateExamId, @urlback = Request.RawUrl}), HttpUtility.HtmlEncode(item.ExamName))%>
                </td>
                <td style="text-align:center">
                    <%:item.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW) %>
                </td>
                <td style="text-align:center">
                    <%:skill != 0 ? skill.ToString() : ""%>
                </td>
                <td style="text-align:center">
                    <%:skillScore.HasValue ? CommonFunc.GetEngLishSkillLevel((int)skill).ToString() : ""%>
                </td>
                <td style="text-align:center">
                    <%: verbal%>
                </td>
                <td style="text-align:center">
                    <%:verbalLevel.HasValue ? verbalLevel.Value.ToString() : ""%>
                </td>
            </tr>
            <% } %>
            
        </table>
    </div>
</fieldset>
<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="btnActive0" class="plus icon" onclick="toggleContent(this)" type="button"> </button>
        English Class Attendance Record
    </legend>
    <div id="listECAtten" style="width: 100%; display:none">        
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
            <tr>
                <th class="gray" width="10%">
                    Class ID
                </th>
                <th class="gray" width="30%">
                    Course Name
                </th>
                <th class="gray" style="width:10%;text-align:center;">
                    Start Date
                </th>
                <th class="gray" width="20%">
                    Result
                </th>
                <th class="gray" width="30%">
                    Remark
                </th>
            </tr>
            <% foreach (sp_TC_GetClassEmpAttendResult item in listEClass)
               { %>
               <tr>
                    <td>
                        <% Response.Write(CommonFunc.Link(item.ID.ToString(), "/TrainingCenterAdmin/EngClassDetail/" + item.ID.ToString(), item.ClassId, false)); %>
                    </td>
                    <td>
                        <% Response.Write(CommonFunc.Link(item.ID.ToString(), "/TrainingCenterAdmin/EngClassDetail/" + item.ID.ToString(), Server.HtmlEncode(item.CourseName).Replace("\r\n", "<br>"), false)); %>
                    </td>
                    <td style="text-align:center;">
                        <% Response.Write(item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW)); %>
                    </td>
                    <td>
                        <% Response.Write(item.Result); %>
                    </td>
                    <td>
                        <% Response.Write(Server.HtmlEncode(item.Remark).Replace("\r\n", "<br>")); %>
                    </td>
                    
                </tr>
               <% } %>
            
       </table>
  </div>
</fieldset>
<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="Button1" class="plus icon" onclick="toggleContent(this)" type="button"> </button>
        English Course Not Yet Taken
    </legend>
    <div id="listECNotAttend" style="width: 100%; display:none">        
            <table cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
                <tr>
                    <th class="gray"  width="10%">
                        Course ID
                    </th>
                    <th class="gray" width="20%">
                        Course Name
                    </th>
                    <th class="gray" width="35%">
                        Objective
                    </th>
                    <th class="gray" width="35%">
                        Registration Requirement
                    </th>
                
                </tr>
                <% foreach (sp_TC_GetClassEmpNotAttendResult item in listEClassNotAtt)
                   { %>
                   <tr>
                        <td>
                            <% Response.Write(CommonFunc.Link(item.CourseId.ToString(), "/TrainingCenterAdmin/CoursesDetail/" + item.ID.ToString(), item.CourseId, false));%>                        
                        </td>
                        <td>
                            <% Response.Write(CommonFunc.Link(item.CourseId.ToString(), "/TrainingCenterAdmin/CoursesDetail/" + item.ID.ToString(), Server.HtmlEncode(item.Name).Replace("\r\n", "<br>"), false)); %>
                        
                        </td>
                        <td>
                        
                            <% Response.Write(item.Objectives != null ?Server.HtmlEncode(item.Objectives).Replace("\r\n", "<br>"):""); %>
                        </td>                
                        <td>
                        
                            <% Response.Write(item.Requirements != null ? Server.HtmlEncode(item.Requirements).Replace("\r\n", "<br>"):""); %>
                        </td>                
                    </tr>
                   <% } %>            
           </table>
      </div>
</fieldset>
<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="Button2" class="plus icon" onclick="toggleContent(this)" type="button"> </button>
        Professional Class Attendance Record
    </legend>
    <div style="width: 100%; display:none">        
        <div id="cfilter">
            <table>
                <tr>                
                    <td>
                    Type
                    </td>
                    <td>
                        <%=Html.DropDownList("skillType", ViewData[Constants.TC_PROFESSIONAL_TYPE] as SelectList,
                                            Constants.FIRST_ITEM_TYPE, new { @style = "width:150px", @title = "Type" })%>
                    </td>
                </tr>
                </table>
        </div>
        <table id="listProAttend"  cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
            <tr>
                <th class="gray"  width="10%">
                    Class ID
                </th>
                <th class="gray"  width="30%">
                    Course Name
                </th>
                <th class="gray"  style="width:10%;text-align:center;">
                    Start Date
                </th>
                <th class="gray"  width="20%"> 
                    Result
                </th>
                <th class="gray"  width="30%">
                    Remark
                </th>
            </tr>
            <% foreach (sp_TC_GetClassEmpAttendResult item in listProfClass)
                { %>
                <tr class="tr_data">
                    <td>
                        <% Response.Write(CommonFunc.Link(item.ID.ToString(), "/Portal/TrainingCenter/ProClassDetail/" + item.ID.ToString(), item.ClassId, false));%>
                    </td>
                    <td>
                        <% Response.Write(CommonFunc.Link(item.ID.ToString(), "/Portal/TrainingCenter/ProClassDetail/" + item.ID.ToString(), Server.HtmlEncode(item.CourseName).Replace("\r\n", "<br>"), false)); %>
                    </td>
                    <td style="width:10%;text-align:center;">
                        <% Response.Write(item.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW)); %>
                    </td>
                    <td>
                        <% Response.Write(item.Result); %>
                    </td>
                    <td>
                        <% Response.Write(Server.HtmlEncode(item.Remark).Replace("\r\n", "<br>")); %>
                    </td>
                    
                </tr>
                <% } %>
            
        </table>
  </div>
</fieldset>
<fieldset style="width:1024px; background-color:White">
    <legend>
        <button id="Button3" class="plus icon" onclick="toggleContent(this)" type="button"> </button>
        Professtional Course Not Yet Taken
    </legend>
<div style="width: 1024px; display:none">        
        <table id="listProNotAttend" cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
            <tr>
                <th class="gray"  width="10%">
                    Course ID
                </th>
                <th class="gray"  width="25%">
                    Course Name
                </th>
                <th class="gray"  width="25%">
                    Objective
                </th>
                <th class="gray"  width="15%">
                    Course Overview
                </th>
                <th class="gray"  width="25%">
                    Registration Requirement
                </th>
                
            </tr>
            <% foreach (sp_TC_GetClassEmpNotAttendResult item in listProfClassNotAtt)
               { %>
               <tr  class="tr_data">
                    <td>
                        <% Response.Write(CommonFunc.Link(item.CourseId.ToString(), "/TrainingCenterAdmin/CoursesDetail/" + item.ID.ToString(), item.CourseId, false));%>                        
                    </td>
                    <td>
                        <% Response.Write(CommonFunc.Link(item.CourseId.ToString(), "/TrainingCenterAdmin/CoursesDetail/" + item.ID.ToString(), Server.HtmlEncode(item.Name).Replace("\r\n", "<br>"), false)); %>
                        
                    </td>
                    <td>                        
                        <% Response.Write(item.Objectives != null ?Server.HtmlEncode(item.Objectives).Replace("\r\n", "<br>"):""); %>
                    </td>                
                    <td>                        
                        <% Response.Write(item.Overview != null ? Server.HtmlEncode(item.Overview).Replace("\r\n", "<br>") : ""); %>
                    </td>                
                    <td>                        
                        <% Response.Write(item.Requirements != null ? Server.HtmlEncode(item.Requirements).Replace("\r\n", "<br>"):""); %>
                    </td>                
                </tr>
               <% } %>            
       </table>
  </div>
</fieldset>
<div id="shareit-box">    
    <img src='../../Content/Images/loading3.gif' alt='' />    
</div></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=TrainingCenterPageInfo.FuncTrainingRecord + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName +
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        div.chart
        {
            text-align:center;
            vertical-align:middle;
        }
        fieldset
        {
            border-style:dotted;
        }
    </style>
    <script type="text/javascript" src="/Scripts/hightchart/highcharts.js"></script>
    <script type="text/javascript">
        function toggleContent(elm) {
            var divContent = $(elm).parent().parent().find("div:first");
            if ($(elm).hasClass("minus")) {
                $(elm).attr("class", "plus icon");
            }
            else {
                $(elm).attr("class", "minus icon");
            }
            divContent.toggle(500);
        }
        $(document).ready(function () {
            $("#skillType").change(function () {
                var url = '/TrainingCenterAdmin/TrainingRecord/?userid=<%=Request["userid"] %>&id=' + $(this).val();
                window.location = url;
            });
            var skillData = new Array();
            var verbalData = new Array();
            var skill = '<%= ViewData["Chart_Skills"]  %>';
            var verbal = '<%= ViewData["Chart_Verbals"]  %>';

            var skillArr = skill.split('<%=Constants.TRAINING_CENTER_MY_PROFILE_CHART_SEPARATOR%>');
            $.each(skillArr, function (no, val) {
                var tmp = val.split(',');
                var data = new Array();
                data.push(Date.UTC(tmp[0], tmp[1], tmp[2]));
                data.push(parseFloat(tmp[3]));
                skillData.push(data);
            });

            var verbalArr = verbal.split('<%=Constants.TRAINING_CENTER_MY_PROFILE_CHART_SEPARATOR%>');
            $.each(verbalArr, function (no, val) {
                var tmp = val.split(',');
                var data = new Array();
                data.push(Date.UTC(tmp[0], tmp[1], tmp[2]));
                data.push(parseFloat(tmp[3]));
                verbalData.push(data);
            });

            var options = {
                chart: {
                    renderTo: 'chart-container',
                    defaultSeriesType: 'line'
                    //borderColor: '#bbbbbb',
                    //borderWidth: 1,
                    //spacingRight: 20
                },
                title: {
                    text: 'English Improvement Process'
                },
                xAxis: {
                    type: 'datetime'
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Level'
                    },
                    tickInterval: 1
                },
                series: [
                    {
                        name: 'Skill',
                        data: skillData.length > 0 ? skillData : null
                        //data: null
                    },
                    {
                        name: 'Verbal',
                        data: verbalData.length > 0 ? verbalData : null
                        //data: null
                    }
                ]
            };

            // Create the chart
            var chart = new Highcharts.Chart(options);
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=TrainingCenterPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl, false) + TrainingCenterPageInfo.FuncTrainingRecord +
        CommonPageInfo.AppDetailSepChar + 
        new EmployeeDao().FullName(ViewData[CommonDataKey.TRAINING_CENTER_EMP_ID] as string, Constants.FullNameFormat.FirstMiddleLast)
            + " - " + ViewData[CommonDataKey.TRAINING_CENTER_EMP_ID]%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
