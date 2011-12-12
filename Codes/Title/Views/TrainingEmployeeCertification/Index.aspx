<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnExport" type="button" title="Export" class="button export">
            Export</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button id="btnAddNew" type="button" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0"> 
            <tr>
                <td>                    
                    <!-- tan.tran: add attribute 'autocomplete="off"' to avoid bug when click on back button -->
                    <input type="text" maxlength="100" style="width: 150px" value="<%= Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_EMPLOYEE_CERTIFICATION_SEARCH_NAME  %>')" autocomplete="off" />
                </td>
                 <td>
                    <%=Html.DropDownList("TitleId", ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("DirectManager", ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_MANAGER] as SelectList, Constants.TRAINING_EMPLOYEE_CERTIFICATION_FIRST_ITEM_MANAGER, new { @style = "width:165px" })%>
                </td>
                <td>
                    <%=Html.DropDownList("Certification", ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_CERTIFICATION] as SelectList, Constants.TRAINING_EMPLOYEE_CERTIFICATION_FIRST_ITEM_CERTIFICATION, new { @style = "width:170px" })%>
                </td> 
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">
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
<asp:Content ID="Content7" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ac_results
        {
            width: 270px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/TrainingEmployeeCertification/GetListJQGrid?searchName=' + encodeURIComponent($('#txtKeyword').val()) + '&jobTitle=' + $('#TitleId').val()
                 + '&manager=' + $('#DirectManager').val() + '&certification=' + $('#Certification').val(),

                datatype: 'json',
                colNames: ['HiddenId', 'ID', 'Name', 'JobTitle', 'Direct Manager', 'Certification Name', 'Remark', 'Action'],
                colModel: [
                  { name: 'HiddenId', index: 'HiddenId', align: "center", width: 50, hidden: true },
                  { name: 'ID', index: 'ID', align: "left", width: 30, title: false },
                  { name: 'Name', index: 'DisplayName', align: "left", width: 130, title: false },
                  { name: 'JobTitle', index: 'JobTitleName', align: "left", width: 120, title: false },
                  { name: 'Direct Manager', index: 'ManagerName', align: "left", width: 120, title: false },
                  { name: 'Certification Name', index: 'Name', align: "center", width: 100, title: false },
                  { name: 'Remark', index: 'Remark', align: "left", width: 195, title: false},
                  { name: 'Action', index: 'Action', editable: false, width: 54, align: 'center', sortable: false}],
                 
                rowList: [20, 30, 50, 100, 200],
                width: 1200,
                height: "100%",
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.TRAINING_EMPLOYEE_CERTIFICATION_LIST_PAGE_INDEX]%>',
                viewrecords: true,
                sortorder: "asc",
                multiselect: true,
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    //                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/TrainingCertification/TrainingCertificationToolTip");
                }
            });
            $("#btnFilter").click(function () {
                var name = $('#txtKeyword').val();
                $('#list').setGridParam({ url: '/TrainingEmployeeCertification/GetListJQGrid?searchName=' + encodeURIComponent($('#txtKeyword').val()) + '&jobTitle=' + $('#TitleId').val()
                 + '&manager=' + $('#DirectManager').val() + '&certification=' + $('#Certification').val()
                }).trigger('reloadGrid');
            });
            $("#btnRefresh").click(function () {
                window.location = "/TrainingEmployeeCertification/Refresh";
            });
            $('#btnAddNew').click(function () {
                CRM.popup('/TrainingEmployeeCertification/Create', 'Add New', 400);
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'HiddenId', '/TrainingEmployeeCertification/DeleteList');
            });
            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/TrainingEmployeeCertification/ExportToExcel/?Active=" + '<%=Constants.TRAINING_EMPLOYEE_CERTIFICATION_ACTIVE %>';
                }
                CRM.completed();
            })
        });
        
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=TrainingEmployeeCertificationPageInfo.MenuName + CommonPageInfo.AppSepChar + TrainingEmployeeCertificationPageInfo.TrainingEmployeeCertification + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= TrainingEmployeeCertificationPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
 <%= CommonFunc.GetCurrentMenu(Request.RawUrl, false) + Constants.TRAINING_EMPLOYEE_CERTIFICATION_PAGE_TITLE%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
