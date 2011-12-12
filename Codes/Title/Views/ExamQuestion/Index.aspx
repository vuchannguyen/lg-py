<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= LOTPageInfo.MenuName + CommonPageInfo.AppSepChar+ LOTPageInfo.ExamQuestion + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.TRAINING_EXAM_QUESTIONS_KEYWORD] %>"
                        id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_EXAM_QUESTIONS_DEFAULT_VALUE  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_EXAM_QUESTIONS_DEFAULT_VALUE  %>')"
                        />
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
    <div id="divEdit" style="display: none; cursor: default;">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/ExamQuestion/GetListJQGrid/?filterClassName=' + encodeURIComponent($("#txtKeyword").val()),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Title', 'Time (Minute)', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50 },
                  { name: 'Title', index: 'Title', align: "left", width: 200, sortable: true },
                  { name: 'Time', index: 'Time', align: "center", width: 50, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.TRAINING_EXAM_QUESTIONS_SORT_PAGE_INDEX]%>',
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1024, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {
                CRM.popup('/ExamQuestion/Create', 'Add New', 800);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/ExamQuestion/DeleteList');
            });

            $("#btnRefesh").click(function () {
                window.location = "/ExamQuestion/Index";
            });

            $("#btnFilter").click(function () {
                $('#list').setGridParam({ url: '/ExamQuestion/GetListJQGrid/?filterClassName=' + encodeURIComponent($("#txtKeyword").val())
                }).trigger('reloadGrid');
            });
        });        
    </script>
    <style>
        .ui-jqgrid tr.jqgrow td
        {
            white-space: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= LOTPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
