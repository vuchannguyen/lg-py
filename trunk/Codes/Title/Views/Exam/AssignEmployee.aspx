<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("AssignEmployee", "Exam", FormMethod.Post, new { id = "AssignEmployeeForm", @class = "form" }))
  {%>
<div id="cfilter">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <input type="text" maxlength="100" style="width: 150px" value="<%= (string)ViewData[Constants.EMPLOYEE_LIST_NAME] %>"
                    id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.FULLNAME_OR_USERID  %>')"
                    onblur="ShowOnBlur(this,'<%= Constants.FULLNAME_OR_USERID  %>')" />
                <%=Html.Hidden(CommonDataKey.EXAM_ID, ViewData[CommonDataKey.EXAM_ID])%>
                <%=Html.Hidden("AssignIDs")%>
            </td>
            <td>
                <%=Html.DropDownList("DepartmentName", ViewData[Constants.EMPLOYEE_LIST_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_DEPARTMENT)%>
            </td>
            <td>
                <%=Html.DropDownList("DepartmentId", ViewData[Constants.EMPLOYEE_LIST_SUB_DEPARTMENT] as SelectList, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:180px" })%>
            </td>
            <td>
                <%=Html.DropDownList("TitleId", ViewData[Constants.EMPLOYEE_LIST_JOB_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:180px" })%>
            </td>
            <td>
                <button type="button" id="btnFilter" title="Filter" class="button filter">
                    Filter</button>
            </td>
        </tr>
    </table>
</div>
<div class="clist">
    <table id="list2" class="scroll">
    </table>
    <div id="pager2" class="scroll" style="text-align: center;">
    </div>
</div>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center">
        </td>
    </tr>
    <tr>
        <td align="center">
            <input type="button" id="btnSubmit" class="save" value="" alt="Update" onclick="onSubmit()"/>
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<div id="shareit-box">
    <img src='../../../../Content/Images/loading3.gif' alt='' />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=AssignEmployee' + '&ExamId=' + $('#<%=CommonDataKey.EXAM_ID%>').val());
        CRM.onEnterKeyword();
        jQuery("#list2").jqGrid({
            url: '/Exam/GetEmployeeListJQGrid?examId=' + $('#ExamID').val() + '&name=' + encodeURIComponent($('#txtKeyword').val())
                     + '&department=' + $('#DepartmentName').val() + '&subDepartment=' + $('#DepartmentId').val() + '&titleId=' + $('#TitleId').val(),
            datatype: 'json',
            colNames: ['ID', 'Full Name', 'Job Title', 'Department', 'Sub Department', 'Start Date', 'Status'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 30, title: false },
                  { name: 'DisplayName', index: 'DisplayName', align: "left", sortable: true, title: false },
                  { name: 'JobTitle', index: 'JobTitle', align: "center", width: 120, title: false },
                  { name: 'Department', index: 'Department', align: "center", width: 100, title: false },
                  { name: 'SubDepartment', index: 'SubDepartment', align: "center", width: 120, title: false },
                  { name: 'StartDate', index: 'StartDate', align: "center", width: 75, title: false },
                  { name: 'Status', index: 'Status', align: "center", width: 65, title: false },                  
                  ],
            pager: jQuery('#pager2'),
            rowList: [20, 30, 50, 100, 200],
            rowNum: 10,
            sortname: 'ID',
            sortorder: "asc",
            multiselect: true,
            viewrecords: true,
            width: 1010, height: 300,
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block',
            loadComplete: function () {
                ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
            }
        });

        $("#DepartmentName").change(function () {
            $("#TitleId").html("");
            $("#DepartmentId").html("");
            var department = $("#DepartmentName").val();
            $("#TitleId").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_JOBTITLE %>' + "</option>"));
            $("#DepartmentId").append($("<option value=''>" + '<%= Constants.FIRST_ITEM_SUB_DEPARTMENT %>' + "</option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#TitleId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
        });


        $("#btnFilter").click(function () {
            var name = $('#txtKeyword').val();
            $('#list2').setGridParam({ page: 1, url: '/Exam/GetEmployeeListJQGrid?examId=' + $('#ExamID').val() + '&name=' + encodeURIComponent(name)
                     + '&department=' + $('#DepartmentName').val() + '&subDepartment=' + $('#DepartmentId').val() + '&titleId=' + $('#TitleId').val()
            }).trigger('reloadGrid');
        });
    });

    function onSubmit() {
        var arrID = getJqgridSelectedIDs('#list2', 'ID');        
        if (arrID == '') {
            CRM.summary("Please select row(s) to assign!", 'block', 'msgError');
        }
        else {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#AssignIDs").val(arrID);
            $("#AssignEmployeeForm").submit();
        }
    }
</script>
<style>
    .ui-jqgrid tr.jqgrow td
    {
        white-space: normal;
    }
</style>
<link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
<script src="/Scripts/Tooltip.js" type="text/javascript"></script>
<% } %>