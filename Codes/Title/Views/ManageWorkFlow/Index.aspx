<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%= TempData["Message"]%>
<div id="cactionbutton">
        <button id="btnRefresh" type="button" title="Refresh" class="button refresh">
            Refresh</button>
        <button id="btnDelete" type="button" title="Delete" class="button delete">
            Delete</button>
        <button type="button" id="addnew" title="Add New" class="button addnew">
            Add New</button>
        
    </div>
<div id="cfilter">
        <table>
            <tr>
                <td>
                    <%=Html.DropDownList(CommonDataKey.MWF_WORKFLOW, null, Constants.FIRST_ITEM_REQUEST, new { @style = "width:180px" })%>
                </td>
                <td>
                    <%=Html.DropDownList(CommonDataKey.MWF_ROLE, null, Constants.FIRST_ITEM_ROLE, new { @style = "width:180px" })%>
                </td>
                 <td>
                    <%=Html.DropDownList(CommonDataKey.MWF_RESOLUTION, null, Constants.FIRST_ITEM_RESOLUTION, new { @style = "width:180px" })%>
                </td>
                 <td>
                    <%=Html.DropDownList(CommonDataKey.MWF_STATUS, null, Constants.FIRST_ITEM_MANAGE_STATUS, new { @style = "width:180px" })%>
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= ManageWorkFlowPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    jQuery(document).ready(function () {
        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JobTitleLevel');
        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: '/manageWorkFlow/GetListJQGrid/?wf=' + $("#wfID").val() + '&role=' + $("#roleID").val()
                +'&resolution=' + $("#resolutionID").val()+'&status=' + $("#statusID").val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['RoleID','ResolutionID','IsHold','Role', 'Resolution', 'Hold', 'WorkFlow', 'Action'],
            colModel: [
            { name: 'RoleID', index: 'Role', align: "left", width: 140, hidden: true },
            { name: 'ResolutionID', index: 'Role', align: "left", width: 140, hidden: true },
            { name: 'IsHold', index: 'Role', align: "left", width: 140, hidden: true },
                  { name: 'Role', index: 'Role', align: "left", width: 140, sortable: true },
                  { name: 'Resolution', index: 'Resolution', align: "left", width: 80, sortable: true },
                  { name: 'Hold', index: 'Hold', align: "center", width: 50, sortable: true },
                  { name: 'WorkFlow', index: 'WorkFlow', align: "left", width: 140, sortable: true },
                  { name: 'Action', index: 'Action', editable: false, width: 50, align: 'center', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [20, 30, 50, 100, 200],
            sortname: '<%= (string)ViewData[Constants.MWF_COLUMN]%>',
            sortorder: '<%= (string)ViewData[Constants.MWF_ORDER]%>',
            rowNum: '<%= (string)ViewData[Constants.MWF_ROW_COUNT]%>',
            page: '<%= (string)ViewData[Constants.MWF_PAGE_INDEX]%>',
            multiselect: { required: false, width: 15 },
            viewrecords: true,
            width: 1024, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
        $('#addnew').click(function () {
            CRM.popup('/ManageWorkFlow/Create', 'Add New', 400);
        });


        $("#btnDelete").click(function () {
            CRM.deletesForWorkFlow('#list', ['RoleID', 'ResolutionID', 'IsHold'], '/ManageWorkFlow/DeleteList');
        });

        $("#btnRefresh").click(function () {
            window.location = "/ManageWorkFlow/Refresh";
        });

        $("#btnFilter").click(function () {
            var url_send = '/ManageWorkFlow/GetListJQGrid/?wf=' + $("#wfID").val() + '&role=' + $("#roleID").val()
                + '&resolution=' + $("#resolutionID").val() + '&status=' + $("#statusID").val();
            $('#list').setGridParam({ page: 1, url: url_send });
            $("#list").trigger('reloadGrid');
        });

        $("#wfID").change(function () {
            $("#roleID").html("");
            $("#resolutionID").html("");
            var item = $("#wfID").val();
            $("#roleID").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_ROLE %>' + "</option>"));
            $("#resolutionID").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_RESOLUTION %>' + "</option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + item + '&Page=WFRole', function (item) {
                $.each(item, function () {
                    $("#roleID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                });
            });
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + item + '&Page=WFResolution', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#resolutionID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
        });
    });        
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%= ManageWorkFlowPageInfo.ComName  %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
