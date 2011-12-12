<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%= TempData["Message"]%>
    
    <div id="cactionbutton">
        <button type="button" id="btnRefresh" title="Refresh" class="button refresh">
            Refresh</button>
        <button type="button" id="btnExport" title="Export" class="button export">
            Export</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    
    <div id="cfilter">
        <table>
            <tr>
                <td align="left">
                    <input type="text" maxlength="50" title="<%= Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL%>" style="width: 150px"
                        value="<%=(string)ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_TEXT]%>" 
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL  %>')" />
                </td>
                <td>
                    <%if (ViewData[CommonDataKey.TRAINING_CENTER_COURSE_TYPE]!=null) %>
                    <%=Html.DropDownList(CommonDataKey.TRAINING_CENTER_COURSE_TYPE, null, Constants.TRAINING_CENTER_LIST_COURSE_TYPE_LABEL, 
                        new { @style = "width:150px", @title = "Course" })%>
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= TrainingCenterPageInfo.FuncCourses + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName
        + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    function GetListTargetUrl() {
        var url = '/TrainingCenterAdmin/GetListCourseAdminJQGrid/?name=' + encodeURIComponent($("#txtKeyword").val())
                    + '&type=<%=RouteData.Values["id"]%>' + '&skilltype=' + $('#<%=CommonDataKey.TRAINING_CENTER_COURSE_TYPE%>').val();
        return url;
    }
    $(document).ready(function () {
        $("#btnExport").click(function () {
            var params = '?name=' + encodeURIComponent($("#txtKeyword").val())
                    + '&type=<%=RouteData.Values["id"]%>' + '&skilltype=' + $('#<%=CommonDataKey.TRAINING_CENTER_COURSE_TYPE%>').val()
                    + '&sortOrder=' + $("#list").getGridParam("sortorder") + '&sortColumn='+ $("#list").getGridParam("sortname");
            window.location = "/TrainingCenterAdmin/ExportCourseList" + params;
        });
        $("#btnRefresh").click(function () {
            window.location = '/TrainingCenterAdmin/RefreshCourseList/<%=RouteData.Values["id"]%>';
        });
        $("#btnDelete").click(function () {
            CRM.deleteList('#list', 'ID', '/TrainingCenterAdmin/DeleteCourseList');
        });
        $("#btnAddNew").click(function () {
            var action = "CreateProCourse";
            var title = "Create Professional Skill Course";
            if ('<%=RouteData.Values["id"]%>' == '<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH%>') {
                action = "CreateEnglishCourse";
                title = "Create English Course";
            }
            CRM.popup("/TrainingCenterAdmin/" + action, title, "700");
        });
        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: GetListTargetUrl(),
            datatype: 'json',
            mtype: 'GET',
            colModel: [
                    { name: 'ID', index: 'ID', hidden: true },
                    { name: 'CourseId', index: 'CourseId', label: "ID", align: "left", width: 80, sortable: true },
                    { name: 'CourseName', index: 'CourseName', label: "Name", align: "left", width: 80, sortable: true },
                    { name: 'TypeName', index: 'TypeName', label: "Type", align: "center", width: 50, sortable: true,
                        hidden: '<%=RouteData.Values["id"]%>' == '<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH%>'
                    },
                    { name: 'StatusName', index: 'StatusName', label: "Status", align: "center", width: 30, sortable: true },
                    { name: 'Duration', index: 'Duration', align: "center", width: 30, sortable: true },
                    { name: 'Requirements', index: 'Requirements', width: 80, align: 'left', sortable: false },
                    { name: 'KeyTrainers', index: 'KeyTrainers', label: "Key Trainers", width: 80, align: 'left', sortable: false },
                    { name: 'Action', index: 'Action', width: 30, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 100, 200],
            viewrecords: true,
            width: 1200, height: "auto",
            multiselect: true,
            sortname: '<%= (string)ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_COLLUMN]%>',
            sortorder: '<%= (string)ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_SORT_ORDER]%>',
            rowNum: '<%= ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_ROW_COUNT]%>',
            page: '<%= ViewData[CommonDataKey.TRAINING_ADMIN_SESSION_FILTER_COURSE_PAGE_INDEX]%>',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });

        $("#btnFilter").click(function () {
            $('#list').setGridParam({ url: GetListTargetUrl() });
            $("#list").trigger('reloadGrid');
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=TrainingCenterPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%int courseType = ConvertUtil.ConvertToInt(RouteData.Values["id"]); %>
<%=CommonFunc.GetCurrentMenu(Request.RawUrl, false) + TrainingCenterPageInfo.FuncCourses + CommonPageInfo.AppDetailSepChar + 
    (courseType == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL ? 
    TrainingCenterPageInfo.FuncChildPro : TrainingCenterPageInfo.FuncChildEng)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
