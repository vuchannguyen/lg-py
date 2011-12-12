<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PurchaseRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <style type="text/css">
        .urgent_row
        {
            background: none;
            background-color: #ffbbbb;    
        }
        
        a.justification
        {
            color: Black;
            text-decoration: none;
            cursor: default;    
        }
        #shareit-box {
            background: none repeat scroll 0 0 #FFFFDD;
            border-width: 1px;
            padding: 2px;
            max-width:300px;
        }
        .ui-state-highlight a, .ui-widget-content .ui-state-highlight a {
            color: Black;
        }
    </style>
    
    <script type="text/javascript">
        
        box_color = "Black";
        box_sticky_color = "Black";
        tooltipoffsets = [0, 30];
        jQuery(document).ready(function () {

            jQuery.validator.addMethod("checkCC", function (value, element) {
                var isvalid = true;
                var word = value.split(';');
                var keyName = "";
                for (var i in word) {
                    if (i < word.length - 1) {
                        if (keyName.indexOf(word[i]) < 0) {
                            keyName += word[i] + ',';
                        }
                        else {
                            jQuery.validator.messages.checkCC = '<%=string.Format(Resources.Message.E0020,  "' + word[i] + '" , "CC List.")%>'; ;
                            isvalid = false;
                            break;
                        }
                    }
                }
                return isvalid;
            }, jQuery.validator.messages.checkCC);
            //Dropdownlist 
            $("#Role").change(function () {
                navigateWithReferrer("/PurchaseRequest/ChangeRole/?RoleId=" + $('#Role').val());
            });
            CRM.onEnterKeyword();
            jQuery("#list").jqGrid({
                url: '/PurchaseRequest/GetListJQGrid/?purchaseId=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&resolutionId=' + $('#resolutionId').val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ["ID", "Priority", "Req#", 'Request Date', 'Expected Date', 'Requestor', 'Department', 'Status', 'Forwarded To', 'Approval', 'Justification', 'Action'],
                colModel: [
                    { name: 'RealID', index: 'RealID', align: "left", width: 10, hidden: true },
                    { name: 'Priority', index: 'Priority', align: "left", hidden: true },
                    { name: 'ID', index: 'ID', align: "left", width: 54, sortable: true },
                    { name: 'RequestDate', index: 'RequestDate', align: "center", width: 90, sortable: true },
                    { name: 'ExpectedDate', index: 'ExpectedDate', align: "center", width: 90, sortable: true },
                    { name: 'RequestorName', index: 'RequestorName', align: "left", width: 110, sortable: true },
                    { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 90, sortable: true },
                    { name: 'ResolutionName', index: 'ResolutionName', align: "left", width: 110, sortable: true },
                    { name: 'AssignName', index: 'AssignName', align: "left", width: 170, sortable: true },
                    { name: 'PurchaseApproval', index: 'PurchaseApproval', align: "left", width: 70, sortable: true },
                     { name: 'Justification', index: 'Justification', align: "left", width: 220, sortable: true },
                    { name: 'Action', index: 'Action', editable: false, width: 54, align: 'center', sortable: false}],
                pager: '#pager',
                sortname: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_COLUMN]%>',
                sortorder: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_ORDER]%>',
                rowNum: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_ROW_COUNT]%>',
                page: '<%= (string)ViewData[Constants.PURCHASE_REQUEST_PAGE_INDEX]%>',
                rowList: [20, 30, 50, 100, 200],
                multiselect: true,
                width: 1200, height: "auto",
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    CRM.highlightRow(this, 'Priority', '<%=Constants.URGENT_VALUE%>', '#ffcccc');
                    ShowTooltip($("a.justification"), $("#shareit-box"), "/PurchaseRequest/JustificationTooltip");
                }
            });
            $("#btnDelete").click(function () {
                CRM.deleteList('#list', 'RealID', '/PurchaseRequest/DeleteList');
            });
            $("#btnRefresh").click(function () {
                window.location = "/PurchaseRequest/Refresh";
            });
            $('#btnAddNew').click(function () {
                window.location = '/PurchaseRequest/Create';
            });
            $('#btnReport').click(function () {
                window.location = '/PurchaseRequest/Report';
            });
            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox("Have no data for Export !", "300");
                }
                else {
                    var name = $('#txtKeyword').val();
                    if (name == '<%= Constants.PURCHASE_REQUEST  %>') {
                        name = "";
                    }
                    window.location = '/PurchaseRequest/ExportToExcel/?purchaseId=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&statusId=' + $('#resolutionId').val()
                }

            });

            $("#Department").change(function () {
                $("#SubDepartment").html("");
                var department = $("#Department").val();
                $("#SubDepartment").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#SubDepartment").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            });

            //Filter
            $("#btnFilter").click(function () {
                var name = encodeURIComponent($('#txtKeyword').val());
                if (name == '<%= Constants.PURCHASE_REQUEST  %>') {
                    name = "";
                };
                $('#list').setGridParam({ url: '/PurchaseRequest/GetListJQGrid/?purchaseId=' + name
                     + '&department=' + $('#Department').val() + '&subdepartment=' + $('#SubDepartment').val()
                     + '&requestorId=' + $('#requestorId').val() + '&assignId=' + $('#assignId').val() + '&resolutionId=' + $('#resolutionId').val()
                }).trigger('reloadGrid');
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PurchaseRequestPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%= CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginRoles" runat="server">
    <% if (ViewData[CommonDataKey.PR_ROLE] != null)
       {%>
    <span class="bold">Login As: </span>
    <%=Html.DropDownList("Role", null, new { @style = "width:180px" })%>
    <%} %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td>
                    <button type="button" id="btnRefresh" title="Refresh" class="button refresh">
                        Refresh</button>
                     <button type="button" id="btnReport" title="Report" class="button">
                        Report</button>
                    <button type="button" id="btnExport" title="Export" class="button export">
                        Export</button>
                    <%                       
                        //Only Requestor Role has permission to add new and delete                          
                        if (ViewData.Model == Constants.PR_REQUESTOR_ID)
                        {%>
                    <button type="button" id="btnDelete" title="Delete" class="button delete">
                        Delete</button>
                    <button type="button" id="btnAddNew" title="Add New" class="button addnew">
                        Add New</button>
                    <%} %>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="cfilter">
        <% Response.Write(Html.Hidden("IsViewAll", ViewData[CommonDataKey.PR_IS_VIEW_ALL])); %>
        <% Response.Write(Html.Hidden("LoginId", ViewData[CommonDataKey.PR_USER_LOGIN_ID])); %>
        
        <% Response.Write(Html.Hidden("LoginName", ViewData[CommonDataKey.PR_USER_LOGIN_NAME])); %>
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.PURCHASE_REQUEST_KEYWORD]  %>"
                        onfocus="ShowOnFocus(this,'<%= Constants.PURCHASE_REQUEST  %>')" onblur="ShowOnBlur(this,'<%= Constants.PURCHASE_REQUEST  %>')" autocomplete="off" />
                </td>
                <td>
                    <%=Html.DropDownList("Department", ViewData[Constants.PURCHASE_REQUEST_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
                </td>
                <td>
                    <%=Html.DropDownList("SubDepartment", ViewData[Constants.PURCHASE_REQUEST_SUB_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT)%>
                </td>
                <td>
                    <%=Html.DropDownList("requestorId", ViewData[Constants.PURCHASE_REQUEST_REQUESTOR_ID] as SelectList, Constants.PURCHASE_REQUEST_REQUESTOR_FIRST_ITEM)%>
                </td>

                <td>
                    <%=Html.DropDownList("assignId", ViewData[Constants.PURCHASE_REQUEST_ASSIGN_ID] as SelectList, Constants.PURCHASE_REQUEST_ASSIGN_FIRST_ITEM)%>
                </td>
                <td>
                    <%=Html.DropDownList("resolutionId", ViewData[Constants.PURCHASE_REQUEST_RESOLUTION_ID] as SelectList, Constants.PURCHASE_REQUEST_STATUS_FIRST_ITEM)%>
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
