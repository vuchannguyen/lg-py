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
                    <input type="text" maxlength="100" style="width: 190px" value="<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_CERTIFICATION_MASTER_SEARCH_NAME  %>')" autocomplete="off" />
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
                url: '/TrainingCertification/GetListJQGrid?certificationName=' + encodeURIComponent($('#txtKeyword').val()),
                    
                datatype: 'json',
                colNames: ['HiddenId', 'ID', 'Name', 'Description', 'Create Date', 'Create By', 'Update Date', 'Update By', 'Active'],
                colModel: [
                  { name: 'HiddenId', index: 'HiddenId', align: "center", width: 50, hidden: true },
                  { name: 'ID', index: 'ID', align: "left", width: 30, title: false },
                  { name: 'Name', index: 'Name', align: "left", width: 130, title: false },
                  { name: 'Description', index: 'Description', align: "left", width: 215, title: false },
                  { name: 'Create Date', index: 'CreateDate', align: "center", width: 100, title: false },
                  { name: 'Create By', index: 'CreatedBy', align: "left", width: 90, title: false },
                  { name: 'Update Date', index: 'UpdateDate', align: "center", width: 100, title: false },
                  { name: 'Update By', index: 'UpdatedBy', align: "left", width: 90, title: false },
                  { name: 'Active', index: 'IsAvtive', align: "center", width: 50, title: false}],
                rowList: [20, 30, 50, 100, 200],
                width: 1200,
                height: "100%",
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.TRAINING_CERTIFICATION_MASTER_LIST_PAGE_INDEX]%>',
                viewrecords: true,
                sortorder: "asc",
                multiselect: true,
                loadui: 'block',
                loadComplete: function () {
//                    ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/TrainingCertification/TrainingCertificationToolTip");
                }
            });
            $("#btnFilter").click(function () {
                var name = $('#txtKeyword').val();
                $('#list').setGridParam({ url: '/TrainingCertification/GetListJQGrid?certificationName=' + encodeURIComponent($('#txtKeyword').val())
                }).trigger('reloadGrid');
            });
            $("#btnRefresh").click(function () {
                window.location = "/TrainingCertification/Refresh";
            });
            $('#btnAddNew').click(function () {
                CRM.popup('/TrainingCertification/Create', 'Add New', 470);
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'HiddenId', '/TrainingCertification/DeleteList');
            });
            $("#btnExport").click(function () {
                CRM.loading();
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox('<%= Resources.Message.E0027 %>', "300");
                }
                else {
                    window.location = "/TrainingCertification/ExportToExcel/?Active=" + '<%=Constants.TRAINING_CERTIFICATION_MASTER_ACTIVE %>';
                }
                CRM.completed();
            });
        });
        
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=TrainingCertificationPageInfo.MenuName + CommonPageInfo.AppSepChar + TrainingCertificationPageInfo.TrainingCertification + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= TrainingCertificationPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
 <%= CommonFunc.GetCurrentMenu(Request.RawUrl, false) + Constants.TRAINING_CERTIFICATION_MASTER_PAGE_TITLE%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
