<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="clist">
    <div id="cfilter">
        <table>
            <tr>
                <td >
                    <input type="text" id="txtKeyword"  style="width: 120px;height:17px"/>
                    <%=Html.DropDownList("Department", ViewData["Department"] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
                    <%=Html.DropDownList("SubDepartment", ViewData["SubDepartment"] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT)%>
                    <%=Html.DropDownList("positionId", ViewData["positionId"] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:120px" })%>  
                    <%=Html.DropDownList("RequestType", ViewData["RequestType"] as SelectList, Constants.JOB_REQUEST_REQUEST_FIRST_ITEM)%>
                    <%--<input type="text" id="txtDate"  style="width: 120px;height:17px"/>--%>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="filter">Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <table id="list1" class="scroll">
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>
    <%= Html.Hidden("Func", (string)ViewData["Func"])%>
    <%= Html.Hidden("IsOnPopup", (string)ViewData["IsOnPopup"])%>
</div>
<script type="text/javascript">
    function ChooseJR(id, isOnPopup) {
        var choose = $("#IsOnPopup").val();
        if (choose == '1')
            CRM.pInPopupClose();
        else
            CRM.closePopup();
        $("#JR").attr('value', ($("#" + id).attr('id')));
        $("#JRApproval").attr('value', ($("#" + id).attr('approval')));
        $("#JR").valid();
    }
    jQuery(document).ready(function () {
        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JR&Action=interview');
        CRM.onEnterKeyword();
        $("#Department").change(function () {
            $("#SubDepartment").html("");
            $("#positionId").html("");
            $("#positionId").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
            var department = $("#Department").val();
            $("#SubDepartment").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#positionId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#SubDepartment").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
        });
//        $("#txtDate").datepicker();
        $("#btnFilter").click(function () {
            var name = encodeURIComponent($('#txtKeyword').val());
            if (name.indexOf('<%=Constants.JOB_REQUEST_ITEM_PREFIX %>') == 0) {
                name = name.substring(3);
            }
            var urls = '/Common/GetListJRGridInterview/?text=' + name
                     + '&department=' + $('#Department').val() + '&sub=' + $('#SubDepartment').val() +
                      '&positionId=' + $('#positionId').val()  + '&request=' + $('#RequestType').val();
            $('#list1').setGridParam({ url: urls
            }).trigger('reloadGrid');
        });
        jQuery("#list1").jqGrid({
            url: '/Common/GetListJRGridInterview/',
            datatype: 'json',
            mtype: 'GET',
            colNames: ["ID", "Req#", 'Request Date', 'Position', /*'Quantity',*/ 'Department', 'Sub Department', 'Expected Start Date', 'Request Type', 'Justification'],
            colModel: [
                    { name: 'RealID', index: 'RealID', align: "center", width: 10, hidden: true },
                    { name: 'ID', index: 'ID', align: "center", width: 30, sortable: false },
                    { name: 'RequestDate', index: 'RequestDate', align: "center", width: 50, sortable: false },
                    { name: 'Position', index: 'Position', align: "left", width: 70, sortable: false },
                    /*{ name: 'Quantity', index: 'Quantity', align: "center", width: 30, sortable: false },*/
                    { name: 'DepartmentName', index: 'DepartmentName', align: "left", width: 60, sortable: false },
                    { name: 'SubDepartment', index: 'SubDepartment', align: "left", width: 70, sortable: false },
                    { name: 'ExpectedStartDate', index: 'ExpectedStartDate', align: "left", width: 70, sortable: false },                    
                    { name: 'Request', index: 'Request', align: "center", width: 50, sortable: false },
                    { name: 'Justification', index: 'Justification', align: "left", width: 100, sortable: false }
                    ],
            pager: '#pager',
            rowList: [20, 30, 50, 100, 200],
            sortname: 'ID',
            sortorder: "desc",
            viewrecords: true,
            width: 990,
            height: "300",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
    });
</script>
