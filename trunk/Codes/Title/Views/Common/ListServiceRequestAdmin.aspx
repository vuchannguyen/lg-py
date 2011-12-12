<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div id="cfilter">
    <table>
        <tr>
            <td>
                <input type="text" maxlength="50" style="width: 130px" value="<%= (string)ViewData[Constants.SR_FIRST_KEY_WORD] %>"
                    id="txtText" onfocus="ShowOnFocus(this,'<%= Constants.SR_FIRST_KEY_WORD%>')"
                    onblur="ShowOnBlur(this,'<%= Constants.SR_FIRST_KEY_WORD  %>')" />
            </td>
            <td>
                <%=Html.DropDownList("Category", ViewData[Constants.SR_ADMIN_CATEGORY_LIST] as SelectList,
                                                                                        Constants.SR_FIRST_CATEGORY, new { @style = "width:130px" })%>
                <%=Html.DropDownList("SubCategory", ViewData[Constants.SR_ADMIN_SUBCATEGORY_LIST] as SelectList,
                                                                                            Constants.SR_FIRST_SUBCATEGORY, new { @style = "width:130px" })%>
                <%=Html.DropDownList("Status", ViewData[Constants.SR_ADMIN_STATUS_LIST] as SelectList,
                                                                                            Constants.SR_FIRST_STATUS, new { @style = "width:130px" })%>
                <%=Html.DropDownList("Assign", ViewData[Constants.SR_ADMIN_ASSIGNTO_LIST] as SelectList,
                                                                                            Constants.SR_FIRST_ASSIGNTO, new { @style = "width:130px" })%>
            </td>
            <td>
                <button type="button" class="icon plus" id="pexpand" title="Advanced Search">
                </button>
            </td>
            <td>
                <button type="button" id="pbtnFilter" title="Filter" style="float: left" class="button filter">
                    Filter</button>
            </td>
        </tr>
    </table>
    <table id="pexpandFilter" style="display: none">
        <tr height="35">
            <td>
                Request Date :
            </td>
            <td>
                From
            </td>
            <td>
                <% Response.Write(Html.TextBox("pFromdate", (string)ViewData[Constants.STT_LIST_STARTDATE_FROM], new { @style = "width:80px" }));%>
            </td>
            <td>
                To
            </td>
            <td>
                <% Response.Write(Html.TextBox("pTodate", (string)ViewData[Constants.STT_LIST_STARTDATE_TO], new { @style = "width:80px" })); %>
            </td>
        </tr>
    </table>
</div>
<div class="clist">
    <table id="list1" class="scroll">
    </table>
    <div id="pager1" class="scroll" style="text-align: center;">
    </div>
</div>
<div id="shareit-box1">
    <img src='../../Content/Images/loading3.gif' alt='' />
</div>
<script type="text/javascript">
    function getListTargetUrl() {
        var url = '/Common/GetListAdminJQGrid/?' +
                'name=' + encodeURIComponent($("#txtText").val()) +
                '&status=' + $('#Status').val() +
                '&category=' + $('#Category').val() +
                '&subcate=' + $('#SubCategory').val() +
                '&assignto=' + $("#Assign").val() +
                '&startDate=' + $('#pFromdate').val() +
                '&endDate=' + $('#pTodate').val();
        return url;
    }

    $(document).ready(function () {

        CRM.onEnterKeyword();

        if ($("#pFromdate").val() != "" || $("#pTodate").val() != "") {
            $('#pexpandFilter').css("display", "");
            $("#pexpand").attr("class", "icon minus");
        }

        $("#pFromdate").datepicker();
        $("#pTodate").datepicker();

        $("#pexpand").click(function () {
            var styl = $("#pexpand").attr("class");
            if (styl == "icon minus") {
                $('#pexpandFilter').css("display", "none");
                $("#pexpand").attr("class", "icon plus");
                $("#pFromdate").attr("value", "");
                $("#pTodate").attr("value", "");
            }
            else {
                $('#pexpandFilter').css("display", "");
                $("#pexpand").attr("class", "icon minus");
            }
        });

        jQuery("#list1").jqGrid({
            url: getListTargetUrl(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['', 'Alert', 'ID', 'Title', 'Category', 'Sub Category', 'Status', 'Assign to', 'Request Date'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 70, hidden: true },
                  { name: 'Icon', index: 'Urgency', align: "center", width: 40, sortable: false },
                  { name: 'Code', index: 'Code', align: "center", width: 70, sortable: true },
                  { name: 'Title', index: 'Title', align: "left", width: 160, sortable: true },
                  { name: 'Category', index: 'Category', align: "left", width: 140, sortable: true },
                  { name: 'SubCategory', index: 'SubCategory', align: "left", width: 140, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 100, sortable: true },
                  { name: 'AssignName', index: 'AssignName', align: "center", width: 110, sortable: true },
                  { name: 'RequestDate', index: 'RequestDate', align: "center", width: 100, sortable: true }],
            pager: jQuery('#pager1'),
            rowList: [20, 30, 50, 100, 200],
            viewrecords: true,
            width: 900, height: 300,
            grouping: false,
            sortname: 'ID',
            sortorder: 'desc',
            rowNum: '20',
            page: '1',
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                ShowTooltip($("a[class=showTooltip]"), $("#shareit-box1"), "/ServiceRequestAdmin/ShowTitleTooltip");
            }
        });

        $("#pbtnFilter").click(function () {

            var url_send = getListTargetUrl();

            $('#list1').setGridParam({ url: url_send });
            $("#list1").trigger('reloadGrid');
        });

        $('#Category').change(function () {
            $("#SubCategory").html("");
            var id = $("#Category").val();
            $("#SubCategory").append($("<option value=''><%= Constants.SR_FIRST_SUBCATEGORY%></option>"));
            if (id != "") {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + id + '&Page=Category', function (item) {
                    $.each(item, function () {
                        $("#SubCategory").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    });
                });
            }
            else {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=-1' + '&Page=Category', function (item) {
                    $.each(item, function () {
                        $("#SubCategory").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    });
                });
            }
        });
    });

</script>
<link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
<script src="/Scripts/Tooltip.js" type="text/javascript"></script>
