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
            <tr>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= TrainingCenterPageInfo.FuncEnglishScoreSheet + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName + 
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
    .ui-jqgrid .ui-jqgrid-hdiv {
        overflow-y: hidden !important;
    }
    .ui-jqgrid .ui-jqgrid-htable th.jq-master div {
        height: auto !important;
        white-space: normal;
    }
</style>
    <script type="text/javascript">

        function GetListTargetUrl() {
            var url = 'GetListEnglishScoreSheet/?text=' + encodeURIComponent($("#txtKeyword").val())
                    + '&title=' + $('#TitleId').val() + '&department=' + $('#Department').val() + '&manager=' + $('#Manager').val();
            return url;
        }



        $(document).ready(function () {
            $("#btnExport").click(function () {
                window.location = "/TrainingCenterAdmin/ExportEnglishScoreSheet";
            });
            $("#btnRefresh").click(function () {
                window.location = "/TrainingCenterAdmin/RefreshEnglishScoreSheet";
            });
            CRM.onEnterKeyword();

            //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Employee&IsActive=1', { hidField: "#txtEmpID" });
            jQuery("#list").jqGrid({
                url: GetListTargetUrl(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['No', 'Employee ID', 'Full Name', 'Current Title', 'Department', 'Manager', 'Exam Date', 'Score', 'TOEIC', 'Level', 'Exam Date', 'TOEIC', 'Level', 'Write Level', 'Verbal Level', 'Satisfication'],
                colModel: [
                 { name: 'No', index: 'No', align: "center", width: 20, sortable: false },
                 { name: 'ID', index: 'ID', align: "center", width: 95, sortable: true },
                 { name: 'FullName', index: 'FullName', align: "left", width: 120, sortable: true },
                 { name: 'TitleName', index: 'TitleName', align: "left", width: 95, sortable: true, title: true },
                 { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 95, sortable: true, title: true },
                 { name: 'ManagerName', index: 'ManagerName', align: "left", width: 95, sortable: true, title: true },
                 { name: 'WriteExamDate', index: 'WriteExamDate', align: "center", width: 70, sortable: false },
                 { name: 'WriteScore', index: 'WriteScore', align: "center", width: 70, sortable: false },
                 { name: 'WriteToeic', index: 'WriteToeic', align: "center", width: 70, sortable: false },
                 { name: 'CurrentWriteLevel', index: 'CurrentWriteLevel', align: "center", width: 70, sortable: false },
                 { name: 'VerbalExamDate', index: 'VerbalExamDate', align: "center", width: 70, sortable: false },
                 { name: 'VerbalToeic', index: 'VerbalToeic', align: "center", width: 70, sortable: false },
                 { name: 'VerbalLevel', index: 'VerbalLevel', align: "center", width: 70, sortable: false },
                 { name: 'MappingWriteLevel', index: 'MappingWriteLevel', align: "center", width: 70, sortable: false },
                 { name: 'MappingVerbalLevel', index: 'MappingVerbalLevel', align: "center", width: 70, sortable: false },
                 { name: 'Mapping', index: 'Mapping', align: "center", width: 70, sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                viewrecords: true,
                width: 1200, height: "auto",
                rownumbers: false,
                shrinkToFit: false,
                sortname: '<%= (string)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ORDER]%>',
                rowNum: '<%= ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_ROW]%>',
                page: '<%= ViewData[Constants.TRAINING_CENTER_LIST_ENG_COURSE_ATTEND_PAGE_INDEX]%>',
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    $(this).find("td[title]").each(function () {
                        $(this).tooltip({
                            bodyHandler: function () {
                                return $(this).text();
                            }
                        });
                    });
                }

            });
            CRM.mergeJQGridHeader("#list", 6, 9, "Current WRITTING skill (entrance)");
            CRM.mergeJQGridHeader("#list", 10, 12, "Current VERBAL skill (entrance)");
            CRM.mergeJQGridHeader("#list", 13, 14, "Matching Scale (Expected Level)");

            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: GetListTargetUrl(), page: 1 });
                $("#list").trigger('reloadGrid');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= TrainingCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl, false) + TrainingCenterPageInfo.FuncEnglishScoreSheet%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
