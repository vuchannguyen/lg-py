<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<div class="clist">
    <div id="cfilter">
        <table>
            <tr>
                <td >
                    <input type="text" id="<%=CommonDataKey.LOCATION_TEXTBOX_KEYWORD%>1" 
                        style="width: 120px;height:17px" value="<%=Constants.LOCATION_TEXTBOX_KEYWORD%>" 
                        onfocus="ShowOnFocus(this,'<%= Constants.LOCATION_TEXTBOX_KEYWORD  %>')" 
                        onblur="ShowOnBlur(this,'<%= Constants.LOCATION_TEXTBOX_KEYWORD  %>')" />
                    <%=Html.DropDownList(CommonDataKey.LOCATION_LIST_BRANCH, null, Constants.LOCATION_LIST_BRANCH_LABEL, new {@style="width:130px"})%>
                    <%=Html.DropDownList(CommonDataKey.LOCATION_LIST_OFFICE, null, Constants.LOCATION_LIST_OFFICE_LABEL, new { @style = "width:130px" })%>
                    <%=Html.DropDownList(CommonDataKey.LOCATION_LIST_FLOOR, null, Constants.LOCATION_LIST_FLOOR_LABEL, new { @style = "width:130px" })%>
                    <%=Html.DropDownList(CommonDataKey.LOCATION_LIST_AVAILABLE, null, Constants.LOCATION_LIST_AVAILABLE_LABEL, new { @style = "width:130px" })%>
                </td>
                <td>
                    <button type="button" id="btnFilter1" title="Filter" class="button filter">Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <table id="list1" class="scroll">
    </table>
    <div id="pager1" class="scroll" style="text-align: center;">
    </div>

</div>
<script type="text/javascript">
    function chooseSeatCode(id, name, isOnPopup) {
        if (Boolean(isOnPopup))
            CRM.pInPopupClose();
        else
            CRM.closePopup();
        $("#WorkLocation").val(name);
        $("#LocationCode").val(id);
        showWorkLocationTooltip();
    }
    function getFilterParams() {
        var urlParams = "";
        var isOnPopupParam = '&isOnPopup=<%=Request["isOnPopup"]%>';
        urlParams += 'text=' + encodeURIComponent($('#<%=CommonDataKey.LOCATION_TEXTBOX_KEYWORD%>1').val()) +
                '&branch=' + $('#<%=CommonDataKey.LOCATION_LIST_BRANCH%>').val() +
                '&office=' + $('#<%=CommonDataKey.LOCATION_LIST_OFFICE%>').val() +
                '&floor=' + $('#<%=CommonDataKey.LOCATION_LIST_FLOOR%>').val() +
                '&isAvailable=' + $('#<%=CommonDataKey.LOCATION_LIST_AVAILABLE%>').val() + isOnPopupParam;
        return urlParams;
    }
    jQuery(document).ready(function () {
        //$("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=Manager');
        $(function () {
            $("#txtKeyword1").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#btnFilter1").click();
                }
            });
        });
        $("#btnFilter1").click(function () {
            var urls = '/Common/GetListWorkLocationGrid/?' + getFilterParams();
            $('#list1').setGridParam({ page: 1, url: urls }).trigger('reloadGrid');
        });
        jQuery("#list1").jqGrid({
            url: '/Common/GetListWorkLocationGrid/?' + getFilterParams(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ["Seat Code", 'Floor', 'Office', 'Branch', "Owner"],
            colModel: [
                    { name: 'Name', index: 'Name', align: "center", width: 30, sortable: true },
                    { name: 'Floor', index: 'Floor', align: "center", width: 20, sortable: true },
                    { name: 'Office', index: 'Office', align: "left", width: 50, sortable: true },
                    { name: 'Branch', index: 'Branch', align: "left", width: 30, sortable: true },
                    { name: 'Owner', index: 'Owner', align: "left", width: 90, sortable: false}],
            pager: '#pager1',
            rowList: [20, 30, 50, 100, 200],
            rowNum: 20,
            sortname: 'ID',
            sortorder: "desc",
            viewrecords: true,
            width: 800,
            height: "300",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
        $("#<%=CommonDataKey.LOCATION_LIST_BRANCH%>").change(function () {
            var selectedBranch = $(this).val();
            var url = "/Common/BranchListOnChange?branchID=" + selectedBranch;
            $.ajax({
                async: false,
                cache: false,
                type: "GET",
                dataType: "json",
                timeout: 1000,
                url: url,
                error: function () {
                    CRM.message("error", "block", "msgError");
                },
                success: function (data) {
                    if (Boolean(data.success)) {
                        var newOptionTemplate = "<option value='{0}'>{1}</option>";
                        var newOption = "";
                        $("#<%=CommonDataKey.LOCATION_LIST_OFFICE%>").html(
                            $.format(newOptionTemplate, "", "<%=Constants.LOCATION_LIST_OFFICE_LABEL%>"));
                        $("#<%=CommonDataKey.LOCATION_LIST_FLOOR%>").html(
                            $.format(newOptionTemplate, "", "<%=Constants.LOCATION_LIST_FLOOR_LABEL%>"));
                        $.each(data.offices, function () {
                            newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                            $("#<%=CommonDataKey.LOCATION_LIST_OFFICE%>").append(newOption);
                        });
                        $.each(data.floors, function () {
                            newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                            $("#<%=CommonDataKey.LOCATION_LIST_FLOOR%>").append(newOption);
                        });
                    }
                    else {
                        CRM.message("error", "block", "msgError");
                    }
                }
            });
        });
        $("#<%=CommonDataKey.LOCATION_LIST_OFFICE%>").change(function () {
            var selectedBranch = $("#<%=CommonDataKey.LOCATION_LIST_BRANCH%>").val();
            var selectedOffice = $(this).val();
            var url = "/Common/OfficeListOnChange?branchID=" + selectedBranch + "&officeID=" + selectedOffice;
            $.ajax({
                async: false,
                cache: false,
                type: "GET",
                dataType: "json",
                timeout: 1000,
                url: url,
                error: function () {
                    CRM.message("error", "block", "msgError");
                },
                success: function (data) {
                    if (Boolean(data.success)) {
                        var newOptionTemplate = "<option value='{0}'>{1}</option>";
                        var newOption = "";
                        $("#<%=CommonDataKey.LOCATION_LIST_FLOOR%>").html(
                            $.format(newOptionTemplate, "", "<%=Constants.LOCATION_LIST_FLOOR_LABEL%>"));
                        $.each(data.floors, function () {
                            newOption = $.format(newOptionTemplate, this["ID"], this["Name"]);
                            $("#<%=CommonDataKey.LOCATION_LIST_FLOOR%>").append(newOption);
                        });
                    }
                    else {
                        CRM.message("error", "block", "msgError");
                    }
                }
            });
        });
    });
</script>