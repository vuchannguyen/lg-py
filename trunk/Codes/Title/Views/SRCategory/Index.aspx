<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    
    <div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div id="cfilter">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <input type="text" class="keyword" value="<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_TEXT] %>"
                        id="txtKeyword"  onfocus="ShowOnFocus(this,'<%=Constants.SR_TXT_KEYWORD_LABEL  %>')" 
                        onblur="ShowOnBlur(this,'<%=Constants.SR_TXT_KEYWORD_LABEL  %>')" style="width:200px" autocomplete="off" />
                </td>
                <%--<td>
                    <%= Html.DropDownList(CommonDataKey.SR_CATEGORY_SEARCH_CATEGORY, null, Constants.SR_LIST_CATEGORY_LABEL,
                        new { @style="width:150px" })%>
                </td>
                <td>
                    <%= Html.DropDownList(CommonDataKey.SR_CATEGORY_SEARCH_STATUS, null, Constants.SR_LIST_STATUS_LABEL,
                        new { @style="width:150px" })%>
                </td>--%>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="button filter">Filter</button>
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
<%=ServiceRequestPageInfo.FuncNameCategory + CommonPageInfo.AppSepChar +
       ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script src="/Scripts/Grid/js/grid.subgrid.js" type="text/javascript"></script>
<style type="text/css">
    #list tr.ui-subgrid td{
        border-bottom: 1px solid #AAAAAA;
    }
    #list tr.ui-subgrid .ui-jqgrid-btable tr.jqgfirstrow td{
        border: none;
    }
    .ui-jqgrid tr.jqgrow td {
        white-space: normal !important;
    }
    .ui-subgrid div.ui-jqgrid-hdiv, .ui-subgrid tr.jqgfirstrow
    {
        height: 0px;
        border: none;
    }
    
    .ui-subgrid .ui-widget-content .ui-row-alt {
        background: none;
    }
    .ui-subgrid div.ui-corner-all {
        border-radius: 0px;
    }
    .ui-subgrid .tablediv .ui-widget-content {
        background: url("images/ui-bg_flat_75_ffffff_40x100.png") repeat-x scroll 50% 50% #FFFFFF;
        border: 0px solid #AAAAAA;
        color: #222222;
    }
    .ui-subgrid .tablediv .ui-state-hover{
        background: url("images/ui-bg_flat_75_ffffff_40x100.png") repeat-x scroll 50% 50% #FFFFFF !important;
    }
    .ui-subgrid table{
        border-top: 1px solid #AAAAAA;
    }
    .ui-subgrid .ui-jqgrid tr.jqgrow td {
        height: 18px;
    }
    .ui-jqgrid .ui-subgrid td.subgrid-data {
        border-left-width: 0;
        padding: 0px;
        border: 0px !important;
    }
    
</style>
<script type="text/javascript">
    var selectedCategories = new Array();
    function removeByElement(arrayName, arrayElement) {
        for (var i = 0; i < arrayName.length; i++) {
            if (arrayName[i] == arrayElement)
                arrayName.splice(i, 1);
        }
    }
    function deleteCategories(action) {
        CRM.summary("", 'none', '');
        var checked = jQuery("#list").find("tr > td > input[type=checkbox]:checked").length;
        if (checked > 0) {
            var arrID = selectedCategories.join(",");
            //var funcname = "window.location = '" + action + "/" + arrID + "'";
            var funcname = "deleteCategoryRows('#list','"+action+"','"+arrID+"')";
            CRM.msgConfirmBox('Are you sure you want to delete?', 350, funcname);
        }
        else {
            CRM.msgBox("Please select row(s) to delete!", 350);
        }
    }
    function deleteCategoryRows(listName, action, arrID) {
        $("button.button.ok").attr("disabled", "disabled");
        $("button.button.ok").addClass("dis");
        jQuery.ajax({
            url: action,
            type: "POST",
            datatype: "json",
            data: ({
                'id': arrID
            }),
            success: function (mess) {
                if (mess.MsgType != 1) {                    
                    CRM.message(mess.MsgText, 'block', 'msgSuccess');
                } else {
                    CRM.message(mess.MsgText, 'block', 'msgError');
                }
                CRM.closePopup();
                selectedCategories = new Array();
                $(listName).trigger('reloadGrid');
            }
        });
    }
    function deleteAllSubGridRows(action) {
        var arrID = selectedCategories.join(",");
        window.location = action + "/" + arrID;
    }
    function getFilterParams() {
        var url = '<%=Url.Action("GetListJQGrid")%>' +
            '?name=' + encodeURIComponent($("#txtKeyword").val());
        return url;
    }
    function bindCheckboxEvent(listId) {
        $.each($(listId).find(":checkbox"), function () {
            $(this).change(function () {
                var rowId = $(this).parent().parent().attr("id");
                var cateId = $(listId).getCell(rowId, "ID");
                if ($(this).attr("checked"))
                    selectedCategories.push(cateId);
                else
                    removeByElement(selectedCategories, cateId);
            });
        });
    }
    jQuery(document).ready(function () {
        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: getFilterParams(),
            datatype: 'json',
            scroll: 0,
            mtype: 'GET',
            colNames: ['ID', 'HasSubGrid', 'Name', 'Description', 'Order', 'Is Active', 'Action'],
            colModel: [
                  { name: 'ID', index: 'ID', hidden: true },
                  { name: 'HasSubGrid', index: 'HasSubGrid', hidden: true },
                  { name: 'Name', index: 'Name', align: "left", width: 100, sortable: true },
                  { name: 'Description', index: 'Description', align: "justify", width: 150, sortable: false },
                  { name: 'Order', index: 'Order', align: "center", width: 21, sortable: true },
                  { name: 'IsActive', index: 'IsActive', align: "center", width: 21, sortable: false },
                  { name: 'Action', index: 'Action', editable: false, width: 21, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 100, 200],
            sortname: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_COLUMN]%>',
            sortorder: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_ORDER]%>',
            rowNum: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_ROW_COUNT]%>',
            page: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_PAGE_INDEX]%>',
            multiselect: { required: false, width: 24 },
            viewrecords: true,
            width: 1024, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                bindCheckboxEvent("#list");
            },
            beforeSelectRow: function (rowid, e) {
                return false;
            },
            subGrid: true,
            subGridBeforeExpand: function (subgrid_id, row_id) {
                var hasSubGrid = $("#list").getCell(row_id, "HasSubGrid") == "True";
                if (!hasSubGrid) {
                    var row = $("#list").getInd(row_id, true);
                    $(row).find("span.ui-icon").removeClass("ui-icon-plus").addClass("ui-icon-minus");
                    return false;
                }
                return true;
            },
            subGridRowExpanded: function (subgrid_id, row_id) {
                //default width for firefox
                var wName = 287;
                var wDesc = 468;
                var wOrder = 67;
                var wIsActive = 66;
                var wAction = 66;
                //set width for IE 7
                if ($.browser.msie) {
                    var iVersion = parseFloat($.browser.version);
                    if (iVersion < 8) {
                        var wName = 286;
                        var wDesc = 468;
                        var wOrder = 67;
                        var wIsActive = 67;
                        var wAction = 66;
                    }
                }
                ////set width for IE schrome
                else if ($.browser.webkit) {
                    var wName = 286;
                    var wDesc = 467;
                    var wOrder = 65;
                    var wIsActive = 65;
                    var wAction = 65;
                }
                var subgrid_table_id;
                subgrid_table_id = subgrid_id + "_t";
                jQuery("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' style='border:0px !important' class='scroll'></table>");
                jQuery("#" + subgrid_table_id).jqGrid({
                    url: '<%=Url.Action("GetSubList")%>' + "/" + $("#list").getCell(row_id, "ID") +
                        "?name=" + encodeURIComponent($("#txtKeyword").val()),
                    datatype: "json",
                    colNames: ['ID', 'Name', 'Description', 'Order', 'Is Active', 'Action'],
                    colModel: [
                        { name: 'ID', index: 'ID', hidden: true },
                        { name: 'Name', index: 'Name', width: wName, align: "left", sortable: false },
                        { name: 'Description', index: 'Description', width: wDesc, align: "justify", sortable: false },
                        { name: 'Order', index: 'Order', width: wOrder, align: "center", sortable: false },
                        { name: 'IsActive', index: 'IsActive', width: wIsActive, align: "center", sortable: false },
                        { name: 'Action', index: 'Action', width: wAction, align: 'center', sortable: false }
                    ],
                    beforeSelectRow: function (rowid, e) {
                        return false;
                    },
                    viewrecords: true,
                    multiselect: { required: false, width: 24 },
                    width: "100%", height: "auto",
                    loadComplete: function () {
                        bindCheckboxEvent("#" + subgrid_table_id);
                    }
                });
            },
            gridComplete: function () {
                var rowIds = $("#list").getDataIDs();
                $.each(rowIds, function (index, rowId) {
                    $("#list").expandSubGridRow(rowId);
                });

            }
        });
        $('#btnAddNew').click(function () {
            CRM.popup('<%=Url.Action("Create")%>', "Add New Category", 430);

        });
        $("#btnDelete").click(function () {
            deleteCategories('<%=Url.Action("Delete")%>');
            //alert($("#list").getRowData(3).Description);
        });
        $("#btnRefresh").click(function () {
            window.location = '<%=Url.Action("Refresh")%>';
        });
        $("#btnFilter").click(function () {
            var targetUrl = getFilterParams();
            $('#list').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
        });
    });        
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=ServiceRequestPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl, true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
