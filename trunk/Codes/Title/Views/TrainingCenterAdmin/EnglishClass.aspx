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
    <input type="text" class="hidden" />
    
    <div id="cfilter">
        <table>
            <tr>
                <td align="left">
                    <input type="text" maxlength="50" title="<%= Constants.TC_TEXT%>" style="width: 150px"
                        value="<%= ViewData[Constants.TC_PROFESSIONAL_TEXT] %>" id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TC_TEXT%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TC_TEXT  %>')" />
                </td>
                <td>
                    <%=Html.DropDownList("Course", ViewData[Constants.TC_PROFESSIONAL_COURSE] as SelectList,
                       Constants.FIRST_ITEM_COURSE, new { @style = "width:150px", @title = "Course" })%>
                </td>
                <%--<td>
                    <%=Html.DropDownList("Type", ViewData[Constants.TC_PROFESSIONAL_TYPE] as SelectList,
                                      Constants.FIRST_ITEM_TYPE, new { @style = "width:150px", @title = "Type" })%>
                </td>--%>
                <td>
                    <%=Html.DropDownList("Status", ViewData[Constants.TC_PROFESSIONAL_STATUS] as SelectList,
                           Constants.FIRST_ITEM_TRANING_STATUS, new { @style = "width:150px", @title = "Status" })%>
                </td>
                <td>
                    <%=Html.DropDownList("Intructor",ViewData[Constants.TC_PROFESSIONAL_INSTRUCTOR] as SelectList,
                                                             Constants.FIRST_ITEM_INTRUCTOR, new { @style = "width:150px", @title = "Instructors" })%>
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
    <%--<div id="shareit-box">
        <img src='~/Content/Images/loading3.gif' alt='' />
    </div>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=TrainingCenterPageInfo.ComName + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.FuncClassTraining + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
     function GetListTargetUrl() {
         var url = '/TrainingCenterAdmin/GetListClassJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val())
                    + '&Course=' + $('#Course').val() + '&Type=0' + '&Status=' + $('#Status').val()
                    + '&Intructor=' + $('#Intructor').val();
         return url;
     }
     $(document).ready(function () {
         $("#btnExport").click(function () {
             window.location = "/TrainingCenterAdmin/Export/Class";
         });
         $("#btnRefresh").click(function () {
             window.location = "/TrainingCenterAdmin/Refresh/Class";
         });
         $("#btnDelete").click(function () {
             CRM.deleteList('#list', 'RealID', '/TrainingCenterAdmin/DeleteListEngClass');
         });
         $("#btnAddNew").click(function () {
             CRM.popup("/TrainingCenterAdmin/CreateClass?type=<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH%>", "Create English Class", "700");
         });
         CRM.onEnterKeyword();
         $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=ClassPlaning&trainingStatus=' + $('#Status:selected').val()
            + '&TypeCourse=<%=Constants.TRAINING_CENTER_CLASS_TYPE_ENGLISH %>');
         jQuery("#list").jqGrid({
             url: GetListTargetUrl(),
             datatype: 'json',
             mtype: 'GET',
             colNames: ['#', 'RealID', 'Class ID', 'Course Name', 'Status', 'Duration', 'Start Date', 'Instructors', 'Class Time', '# of Attendees', 'Objectives', 'Action'],
             colModel: [
                 { name: '#', index: '#', align: "center", width: 15, sortable: false },
                 { name: 'RealID', index: 'RealID', align: "center", width: 15, sortable: false,hidden:true },
                  { name: 'ClassID', index: 'ClassID', align: "left", width: 50, sortable: true },
                 { name: 'Course', index: 'Course', align: "left", width: 80, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 30, sortable: true },
                  { name: 'Duration', index: 'Duration', align: "center", width: 40, sortable: true },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 50, sortable: true },
                  { name: 'Instructors', index: 'Instructors', width: 70, align: 'left', sortable: true },
                  { name: 'ClassTime', index: 'ClassTime', align: "left", width: 70, sortable: true },
                  { name: 'Attendess', index: 'Attendess', width: 50, align: 'center', sortable: true },
                  { name: 'Objectives', index: 'Objectives', width: 90, align: 'left', sortable: true},
                  { name: 'Action', index: 'Action', width: 20, align: 'center', sortable: false}],
             pager: jQuery('#pager'),
             rowList: [20, 30, 50, 100, 200],
             viewrecords: true,
             width: 1200, height: "auto",
             grouping: false,
             multiselect: true,
             rownumbers: false,
             sortname: '<%= (string)ViewData[Constants.TC_PROFESSIONAL_COLUMN]%>',
             sortorder: '<%= (string)ViewData[Constants.TC_PROFESSIONAL_ORDER]%>',
             rowNum: '<%= (string)ViewData[Constants.TC_PROFESSIONAL_ROW_COUNT]%>',
             page: '<%= (string)ViewData[Constants.TC_PROFESSIONAL_PAGE_INDEX]%>',
             imgpath: '/scripts/grid/themes/basic/images',
             loadui: 'block',
             loadComplete: function () {
                 var numberRow = $("#list").getGridParam("records");
                 for (var i = 1; i <= numberRow; i++) {
                     var status = $("#" + i).find("td").find(".row_active").attr("id");
                     if (status != undefined) {
                         $("#" + i).find("td").find(".row_active").parent().addClass("row_active");
                     }
                 }
             }
         });

         $("#btnFilter").click(function () {
             $('#list').setGridParam({ url: GetListTargetUrl() });
             $("#list").trigger('reloadGrid');
         });
     });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= TrainingCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl) + TrainingCenterPageInfo.FuncClasses + CommonPageInfo.AppDetailSepChar + 
     TrainingCenterPageInfo.FuncChildEng%>
    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
