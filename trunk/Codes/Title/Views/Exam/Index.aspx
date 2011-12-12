<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
    </div>
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px" maxlength="200"
                        value="<%= (string)ViewData[Constants.EXAM_TEXT] %>" onfocus="ShowOnFocus(this,'<%= Constants.EXAM_TITLE  %>')"
                        onblur="ShowOnBlur(this,'<%= Constants.EXAM_TITLE  %>')" autocomplete="off"  />
                </td>
                <td>
                    <%=Html.DropDownList(CommonDataKey.EXAM_QUESTION, ViewData[Constants.EXAM_QUESTION] as SelectList, Constants.FIRST_ITEM_EXAM_QUESTION, new { @style = "width:180px" })%>
                </td>
                <td>
                    Exam Date From
                </td>
                <td>
                    <%=Html.TextBox("ExamFromDate",(string)ViewData[Constants.EXAM_DATE_FROM],new { @style = "width:70px", @maxlength = "10" })%>
                </td>
                <td>
                    To
                </td>
                <td>
                    <%=Html.TextBox("ExamToDate", (string)ViewData[Constants.EXAM_DATE_TO], new { @style = "width:70px", @maxlength = "10" })%>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div class="clist">
        <table id="list" class="scroll" style="width: 1024px">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
    <div id="divEdit" style="display: none; cursor: default;">
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=LOTPageInfo.MenuName + CommonPageInfo.AppSepChar + LOTPageInfo.Exam + CommonPageInfo.AppSepChar + LOTPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CRM.onEnterKeyword();
            $("#ExamFromDate").datepicker();
            $("#ExamToDate").datepicker();
            jQuery("#list").jqGrid({
                url: '/Exam/GetExamListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&examQuestionId=' + $("#ExamQuestion").val() + '&examDateFrom=' + $("#ExamFromDate").val() + '&ExamDateTo=' + $("#ExamToDate").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['ID', 'Title', 'Exam Question', 'Exam Type', 'Exam Date', 'Writing Mark Status', 'Programming Mark Status', 'Action'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'Title', index: 'Title', align: "left", width: 140, sortable: true },
                  { name: 'ExamQuestion', index: 'ExamQuestion', align: "left", width: 140, sortable: true },
                  { name: 'ExamType', index: 'ExamType', align: "center", width: 50, sortable: true },
                  { name: 'ExamDate', index: 'ExamDate', align: "center", width: 50, sortable: true },
                  { name: 'MarkStatus', index: 'MarkStatus', align: "center", width: 75, sortable: true },
                  { name: 'ProgrammingMarkStatus', index: 'ProgrammingMarkStatus', align: "center", width: 92, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
                pager: jQuery('#pager'),
                rowList: [20, 30, 50, 100, 200],
                sortname: '<%= (string)ViewData[Constants.EXAM_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.EXAM_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.EXAM_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.EXAM_PAGE_INDEX]%>',
                multiselect: { required: false, width: 15 },
                viewrecords: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block'
            });
            //set width for checkbox column
            jQuery("#jqgh_cb").parent().css("width", "15px");
            jQuery("tr#_empty > td:first").css("width", "15px");

            $('#addnew').click(function () {
                CRM.popup('/Exam/Create', 'Add New', 400);
            });

            //if mask is clicked            
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'ID', '/Exam/DeleteList');
            });

            $("#btnRefresh").click(function () {
                window.location = "/Exam/Refresh";
            });

            $("#btnFilter").click(function () {
                var isValidFromDate = true;
                var isValidToDate = true;
                var fromDate = "";
                var toDate = "";
                if ($('#ExamFromDate').val() != "") {
                    isValidFromDate = isDate($('#ExamFromDate').val());
                    fromDate = $('#ExamFromDate').val();
                }
                if ($('#ExamToDate').val() != "") {
                    isValidToDate = isDate($('#ExamToDate').val());
                    toDate = $('#ExamToDate').val();
                }

                if (isValidFromDate && isValidToDate) {
                    var isValid = true;
                    //Check From Date can not greater than To Date
                    if ($('#ExamFromDate').val() != "" && $('#ExamToDate').val() != "" && isValidFromDate && isValidToDate) {
                        if (fromDate > toDate) {
                            isValid = false;
                            alert('From Date must be less than To Date');
                        }
                    }

                    if (isValid) {
                        var url_send = '/Exam/GetExamListJQGrid/?text=' + encodeURIComponent($("#txtKeyword").val()) + '&examQuestionId=' + $("#ExamQuestion").val() + '&examDateFrom=' + fromDate + '&examDateTo=' + toDate;
                        $('#list').setGridParam({ page: 1, url: url_send });
                        $("#list").trigger('reloadGrid');
                    }
                }
                else {
                    if (!isValidFromDate) {
                        alert('Exam From Date is invalid.');
                    } else if (!isValidToDate) {
                        alert('Exam To Date is invalid');
                    }
                }
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
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
