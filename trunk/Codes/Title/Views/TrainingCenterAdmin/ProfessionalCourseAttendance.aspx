<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">
            Export</button>
    </div>
    <div id="cfilter">
        <table>
            <tr height="35">
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_EEI_TXT_KEYWORD_LABEL  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_EEI_TXT_KEYWORD_LABEL  %>')" />
                    <% Response.Write(Html.Hidden("txtEmpID")); %>
                </td>
                <td>
                    <%=Html.DropDownList("TitleId", ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("Department", ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_DEPT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
                </td>
                <td>
                    <%=Html.DropDownList("Manager", ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_MANAGER] as SelectList, Constants.FIRST_MANAGER)%>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" style="float: left" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
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
    <%= TrainingCenterPageInfo.ComName + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.FuncProCourseAttend + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function getListTargetUrl() {
            var url = '/TrainingCenterAdmin/GetListProfessionalhCourseAttend/?' +
                'text=' + $('#txtKeyword').val() +
                '&title=' + $('#TitleId').val() +
                '&department=' + $('#Department').val() +
                '&manager=' + $('#Manager').val();

            return url;
        }
        $(document).ready(function () {
            //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Employee&IsActive=1', { hidField: "#txtEmpID" });
            CRM.onEnterKeyword();
            $("#btnRefresh").click(function () {
                window.location = "/TrainingCenterAdmin/RefreshProfessionalCourseAttend";
            });
            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: getListTargetUrl(), page: 1 }).trigger('reloadGrid');
            });

            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/TrainingCenterAdmin/ExportEnglishCourseAttend/<%=Constants.TRAINING_CENTER_CLASS_TYPE_PRO_SKILL %>";
                }
                CRM.completed();
            });
        });
    </script>
    <%
        string urlLink = "/TrainingCenterAdmin/GetListProfessionalhCourseAttend/?" +
       "text=" + (string)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_NAME] +
       "&title=" + (string)ViewData["TextDept"] +
       "&department=" + (string)ViewData["TextTitle"] +
       "&manager=" + (string)ViewData["TextManager"];

        List<Training_Course> empList = ViewData.Model == null ? null : (List<Training_Course>)ViewData.Model;
        int countColumn = 5;
        if (empList != null)
        {
            countColumn += empList.Count;
        }
        CRM.Library.Controls.GridViewControls grid = new CRM.Library.Controls.GridViewControls();
        grid.Width = 1200;
        grid.RowNum = 20;
        grid.RowList = "[20,50,100,200,300]";
        grid.LoadComplete = "ShowTooltip($('a[class=showTooltip]'), $('#shareit-box'), '/TrainingCenterAdmin/EmpClassTooltip');"
            + "CRM.setupFreezeColumn(['ID','FullName','TitleName','DepartmentName','ManagerName'])";

        string[] colNames = new string[countColumn];
        colNames[0] = "Emp ID";
        colNames[1] = "Name";
        colNames[2] = "Current Title";
        colNames[3] = "Department";
        colNames[4] = "Direct Manager";
        for (int i = 0; i < empList.Count; i++)
        {
            colNames[i + 5] = "<div title=\"" + HttpUtility.HtmlEncode(empList[i].Name) + "\">" + HttpUtility.HtmlEncode(empList[i].CourseId) + "</div>";
        }
        string[] colModels = new string[countColumn];
        colModels[0] = "{name: 'ID', index: 'ID', align: 'center', width: 70}";
        colModels[1] = "{name: 'FullName', index: 'FullName', align: 'left', width: 180}";
        colModels[2] = "{name: 'TitleName', index: 'TitleName', align: 'left', width: 150}";
        colModels[3] = "{name: 'DepartmentName', index: 'DepartmentName', align: 'left', width: 150}";
        colModels[4] = "{name: 'ManagerName', index: 'ManagerName', align: 'left', width: 180}";
        for (int i = 0; i < empList.Count; i++)
        {
            colModels[i + 5] = "{name: '" + empList[i].Name + "', index: '" + empList[i].Name + "', align: 'center', width: 80,sortable:false}";
        }
        grid.SortOrder = CommonFunc.SetSortOrder(ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER]);
        grid.SortName = (string)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN];
        grid.Page = (int)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX];
        grid.RowNum = (int)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW];
        grid.MultiSelect = false;
        grid.HoverRows = false;
        Response.Write(grid.ShowGrid(urlLink, colNames, colModels));
                
    %>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= TrainingCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl)%>Professional Courses Attendance
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
