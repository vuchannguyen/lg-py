<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        Training_Course course = ViewData.Model as Training_Course;
        TrainingCenterDao traincendao = new TrainingCenterDao();

        if (course == null)
            Response.Redirect("/Course/1");
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";

        List<int> courseList = new List<int>();
        courseList = (List<int>)ViewData["ListInter"];
        int lastID = 0;
        int firstID = 0;
        int nextID = 0;
        int preID = 0;
        int number = 0;
        int total = 0;

        int index = 0;
        total = courseList.Count;
        if (courseList.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = courseList.IndexOf(course.ID);
            if (index == 0)
            {
                styleFirst = "class=\"first first_off\"";
                stylePrev = "class=\"prev prev_off\"";
            }
            else if (index == courseList.Count - 1)
            {
                styleLast = "class=\"last last_off\"";
                styleNext = "class=\"next next_off\"";
            }
            number = index + 1;
        }
        else if (courseList.Count == 1)
        {
            number = courseList.Count;
        }

        if (courseList != null && courseList.Count > 0)
        {
            lastID = courseList[courseList.Count - 1]; ;
            firstID = courseList[0];
            nextID = 0;
            if (index + 1 < courseList.Count)
                nextID = courseList[index + 1];
            else
                nextID = courseList[courseList.Count - 1];

            preID = 0;
            if ((index - 1) == -1)
                preID = courseList[0];
            else if (index > 0)
                preID = courseList[index - 1];

        }
    %>

     <%= TempData["Message"]%>
    <div id="cnavigation" style="width: 1024px">
        <button type="button" id="btnLast" value="<%=lastID %>" <%=styleLast %>>
        </button>
        <button type="button" id="btnNext" value="<%=nextID %>" <%=styleNext %>>
        </button>
        <span>
            <%= number + " of " + total%></span>
        <button type="button" id="btnPre" value="<%=preID %>" <%=stylePrev %>>
        </button>
        <button type="button" id="btnFirst" value="<%=firstID%>" <%=styleFirst %>>
        </button>
    </div>

    <div style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td>
                    <h2 class="heading">
                       Course Information
                    </h2>
                </td>
                <td align="right">
                    <%if (course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH)
                      {%>
                    <a href="javascript:void(0);" onclick="CRM.popup('/TrainingCenterAdmin/EditEnglishCourse?id=<%=course.ID %>&page=Detail', 'Edit Course <%=course.CourseId %>', 700);"
                        id="lnkEdit">Edit</a>
                   <%}
                      else
                      {%>
                   <a href="javascript:void(0);" onclick="CRM.popup('/TrainingCenterAdmin/EditProCourse?id=<%=course.ID %>&page=Detail', 'Edit Course <%=course.CourseId %>', 700);"
                        id="A1">Edit</a>
                        <%} %>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label">
                    Course ID
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=HttpUtility.HtmlEncode(course.CourseId) %>
                </td>
                <td class="label">
                    Course Name
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=HttpUtility.HtmlEncode(course.Name) %>
                </td>
                <td>
                </td>
            </tr>
            <%
                if (course.TypeOfCourse == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL)
                {%>
            <tr>
                <td class="label">
                    Type
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=HttpUtility.HtmlEncode(traincendao.GetTrainingSkillByID((int)course.TypeId).Name)%>
                </td>
                <td class="label">
                    Status
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <% 
                        string tranName = traincendao.GetTrainingStatusByID((int)course.StatusId).Name;
                        if (tranName == "Open")
                        {
                            Response.Write("<strong style='color:rgb(115,183,115)'>" + HttpUtility.HtmlEncode(tranName) + "</strong>");
                        }
                        else
                            Response.Write(HttpUtility.HtmlEncode(tranName));
                    %>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Duration(hrs)
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=HttpUtility.HtmlEncode(course.Duration)%>
                </td>
                <td class="label">
                    Active
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=course.Active == true ? "Yes" : "No"%>
                </td>
                <td>
                </td>
            </tr>
            <%}
                    else
                    {%>
            <tr>
                <td class="label">
                    Status
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                   <% 
                        string tranName = HttpUtility.HtmlEncode(traincendao.GetTrainingStatusByID((int)course.StatusId).Name);
                        if (tranName == "Open")
                        {
                            Response.Write("<strong style='color:rgb(115,183,115)'>" + tranName + "</strong>");
                        }
                        else
                            Response.Write(tranName);
                    %>
                </td>
                <td class="label">
                    Active
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=course.Active == true ? "Yes" : "No"%>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Duration(hrs)
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                    <%=HttpUtility.HtmlEncode(course.Duration)%>
                </td>
                <td>
                </td>
            </tr>
            <%} %>
            <tr>
                <td class="label">
                    Objectives
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input" colspan="4">
                    <%=HttpUtility.HtmlEncode(course.Objectives) %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Course Overview
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input" colspan="4">
                    <%=HttpUtility.HtmlEncode(course.Overview)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Registration Requirements
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input" colspan="4">
                    <%=HttpUtility.HtmlEncode(course.Requirements)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Key Trainers
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input" colspan="4">
                    <%=String.IsNullOrEmpty(course.KeyTrainers)?"":course.KeyTrainers.Trim(';') %>
                </td>
            </tr>
            <tr class="last">
                <td class="label">
                    Notes
                </td>
                <td style="padding-left: 10px; height: 30px; width: 300px" class="input" colspan="4">
                    <%=HttpUtility.HtmlEncode(course.Notes)%>
                </td>
            </tr>
        </table>
        <%=Html.Hidden("courseId", course.ID)%>
        <%=Html.Hidden("type", course.TypeOfCourse)%>
    </div>
    <h2 class="heading">
        Material
    </h2>
    <div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
     <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%Training_Course course = ViewData.Model as Training_Course; %>
    <%=TrainingCenterPageInfo.ComName + CommonPageInfo.AppSepChar +
                    (course.TypeOfCourse == 1 ? TrainingCenterPageInfo.FuncChildPro : TrainingCenterPageInfo.FuncChildEng) + 
                        CommonPageInfo.AppSepChar + HttpUtility.HtmlEncode(course.CourseId) + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link href="/Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
<script src="/Scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
            function showPopup(url, title, w) {
                CRM.loading();
                $.ajax({
                    async: false,
                    cache: false,
                    type: "GET",
                    dataType: "html",
                    timeout: 1000,
                    url: url,
                    success: function (msg) {
                        CRM.summary('', 'none', '');
                        $(CONST_POPUP_CONTENT).html(msg);
                        $(CONST_POPUP_TITLE).html(title);
                        $(CONST_POPUP_CLOSE).attr('href', 'javascript:void(0);').click(function () { CRM.closePopup(); });
                        jQuery.blockUI({
                            message: $(CONST_POPUP),
                            css: {
                                border: 'none',
                                backgroundColor: 'transparent',
                                opacity: 1,
                                textAlign: 'left',
                                top: ($(window).height() - $(CONST_POPUP).height() - 34) / 2 + 'px',
                                left: ($(window).width() - w) / 2 + 'px',
                                width: w + 'px'
                            },
                            onBlock: function () {
                                $('#fUpload').uploadify({
                                    'uploader': '<%= Url.Content("/Scripts/uploadify/uploadify.swf") %>',
                                    'script': '/Library/GenericHandle/UploadFileHandler.ashx?Page=Material',
                                    'fileExt': '<%=Constants.MATERIAL_PAGE_FILE_FORMAT_ALLOWED%>',
                                    'cancelImg': '<%= Url.Content("/Scripts/uploadify/cancel.png") %>',
                                    //'fileDesc': 'Image files (*.jpg; *.png; *.gif)',
                                    'sizeLimit': parseInt('<%=(int)Constants.MATERIAL_PAGE_FILE_MAX_SIZE%>') * 1024 * 1024,
                                    //'sizeLimit': '<%=(int)Constants.MENU_PAGE_IMAGE_MAX_SIZE * 1024 * 1024%>',
                                    'width': 16,
                                    'height': 16,
                                    'wmode': 'transparent',
                                    'removeCompleted': true,
                                    'auto': true,
                                    'buttonText': 'Choose File',
                                    'hideButton': true,
                                    'onComplete': function (event, ID, fileObj, response, data) {
                                        var jsonData = $.parseJSON(response);
                                        if (jsonData.isSuccess == 1) {
                                            CRM.summary("", "none", "");
                                            $("#ServerImageName").val(jsonData.fileName);
                                            showFile(fileObj.name);
                                            //$("#btnRemoveFile").show();
                                            //$("#fUploadUploader").hide();
                                            //$("#divImageName").show();

                                        }
                                        else {

                                            CRM.summary(jsonData.message, "block", "msgError");
                                            //removeFile();
                                            $("#fUploadQueue").remove();
                                            //$('#fUpload').uploadifyCancel($('.uploadifyQueueItem').first().attr('id').replace('fUpload',''));
                                        }
                                    },
                                    'onError': function (event, ID, fileObj, errorObj) {
                                        if (errorObj.type == 'File Size') {
                                            CRM.summary(CRM.format(E0012, '<%=Constants.MATERIAL_PAGE_FILE_MAX_SIZE%>'), "block", "msgError");
                                            removeFile();
                                            $("#fUploadQueue").remove();

                                            //$("#ServerImageName").val("");
                                            //$("#divImageName").hide();
                                        }
                                    }
                                });
                            }
                        });
                        $('.blockUI').css('cursor', 'default');
                        $(".blockOverlay").css("display", "");
                        CRM.completed();
                    }
                });
            };
         jQuery(document).ready(function () {
             /* Navigator */
             $('#btnFirst').click(function () {
                
                 var className = $('#btnFirst').attr('class');
                 if (className != "first first_off") {
                     window.location = "/TrainingCenterAdmin/CoursesDetail/" + $('#btnFirst').val();
                 }
             });
             $('#btnPre').click(function () {
                 
                 var className = $('#btnPre').attr('class');
                 if (className != "prev prev_off") {
                     window.location = "/TrainingCenterAdmin/CoursesDetail/" + $('#btnPre').val();
                 }
             });
             $('#btnNext').click(function () {
                 
                 var className = $('#btnNext').attr('class');
                 if (className != "next next_off") {
                     window.location = "/TrainingCenterAdmin/CoursesDetail/" + $('#btnNext').val();
                 }
             });
             $('#btnLast').click(function () {
                 
                 var className = $('#btnLast').attr('class');
                 if (className != "last last_off") {
                     window.location = "/TrainingCenterAdmin/CoursesDetail/" + $('#btnLast').val();
                 }
             });
             /*---------------------*/
             jQuery("#list").jqGrid({
                 url: '/TrainingCenterAdmin/GetSubListJQGrid/?courseId=' + $('#courseId').val() + '&type=' + $("#type").val(),
                 datatype: 'json',
                 mtype: 'GET',
                 colNames: ['ID', 'Title', 'Description', 'Action'],
                 colModel: [                  
                  { name: 'ID', index: 'ID', align: "center", width: 20, sortable: true },
                  { name: 'Title', index: 'title', align: "Left", width: 150, sortable: true },
                  { name: 'Description', index: 'description', align: "center", width: 300, sortable: true },                  
                  { name: 'Action', index: 'Action', editable: false, width: 30, align: 'center', sortable: false}],
                 pager: jQuery('#pager'),
                 rowList: [20, 30, 50, 80, 100],
                 viewrecords: true,
                 width: 1050, height: "auto",
                 rownumbers: false,
                 sortname: '<%= (string)ViewData[Constants.TC_COURSE_DETAIL_COLUMN]%>',
                 sortorder: '<%= (string)ViewData[Constants.TC_COURSE_DETAIL_ORDER]%>',
                 rowNum: '<%= (string)ViewData[Constants.TC_COURSE_DETAIL_ROW_COUNT]%>',
                 page: '<%= (string)ViewData[Constants.TC_COURSE_DETAIL_PAGE_INDEX]%>',
                 imgpath: '/scripts/grid/themes/basic/images',
                 loadui: 'block',
                 loadComplete: function () {
                     ShowTooltip($("a[class=courseTooltip]"), $("#shareit-box"), "/TrainingCenterAdmin/CourseDetailTooltip");
                 }
             });
             
         });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
   
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% 
        Training_Course course = ViewData.Model as Training_Course;
        Response.Write(CommonFunc.GetCurrentMenu(Request.RawUrl));
        Response.Write(TrainingCenterPageInfo.FuncCourses + CommonPageInfo.AppDetailSepChar);
        string CourseTypeLink="/TrainingCenterAdmin/Courses/";
        string CourseTypeName="";
        if(course.TypeOfCourse==Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH)
        {
            CourseTypeLink=CourseTypeLink+Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH;
            CourseTypeName = TrainingCenterPageInfo.FuncChildEng;
        }else{
            CourseTypeLink=CourseTypeLink+Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL;
            CourseTypeName = TrainingCenterPageInfo.FuncChildPro;
        }
        Response.Write("<a href='" + CourseTypeLink + "'>" + CourseTypeName + "</a> » ");
        Response.Write(HttpUtility.HtmlEncode(course.Name));
    %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
