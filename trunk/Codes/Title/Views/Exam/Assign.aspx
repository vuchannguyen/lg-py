<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("Assign", "Exam", FormMethod.Post, new { id = "AssignCandidateForm", @class = "form" }))
  {%>
<div id="cfilter">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <%=Html.Hidden(CommonDataKey.EXAM_ID, ViewData[CommonDataKey.EXAM_ID])%>
                <%=Html.Hidden("AssignIDs")%>                  
                <input type="text" maxlength="100" style="width: 120px"
                    id="txtKeyword" onfocus="ShowOnFocus(this,'<%= Constants.CANDIDATE_NAME  %>')"
                    onblur="ShowOnBlur(this,'<%= Constants.CANDIDATE_NAME  %>')" />                              
            </td>
            <td>
                <%=Html.DropDownList("Source", ViewData[Constants.CANDIDATE_LIST_SOURCE] as SelectList, Constants.CANDIDATE_SOURCE, new { @style = "width:100px" })%>
            </td>
            <td>
                <%=Html.DropDownList("TitleId", ViewData[Constants.CANDIDATE_LIST_JOB_TITLE] as SelectList, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:150px" })%>
            </td>
            <td>
                <%=Html.DropDownList("Status", ViewData[Constants.CANDIDATE_LIST_STATUS] as SelectList, Constants.FIRST_ITEM_STATUS, new { @style = "width:150px" })%>
            </td>           
            <td>
                Searched date from
            </td>
            <td>
                <%=Html.TextBox("FromDate", string.Empty, new { @style = "width:70px" })%>
            </td>
            <td>
                to
            </td>
            <td>
                <%=Html.TextBox("ToDate", string.Empty, new { @style = "width:70px" })%>
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
    <img src='../../Content/Images/loading3.gif' alt='' />
</div>
<script type="text/javascript">
    $(document).ready(function () {
        CRM.onEnterKeyword();
        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=AssignCandidate' + '&ExamId=' + $('#<%=CommonDataKey.EXAM_ID%>').val());
        $("#FromDate").datepicker();
        $("#ToDate").datepicker();

        jQuery("#list2").jqGrid({
            url: '/Exam/GetCandidateListJQGrid?examID=' + $('#ExamID').val() + ' &can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&source=' + $('#Source').val()
                    + '&titleId=' + $('#TitleId').val() + '&status=' + $('#Status').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val(),
            datatype: 'json',
            mtype: 'POST',
            colNames: ['No', 'ID', 'Candidate Name', 'DOB', 'Telephone', 'Gender', 'Searched date', 'Source', 'Position', 'Status', 'Remarks'],
            colModel: [
                  { name: 'No', index: 'No', align: "center", width: 20, hidden: false, sortable: false },
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'CandidateName', index: 'CandidateName', align: "left", sortable: true },
                  { name: 'DOB', index: 'DOB', align: "center", width: 60, sortable: true },
                  { name: 'CellPhone', index: 'CellPhone', align: "center", width: 60, sortable: true },
                  { name: 'Gender', index: 'Gender', align: "center", width: 45, sortable: true },
                  { name: 'SearchDate', index: 'SearchDate', align: "center", width: 80, sortable: true },
                  { name: 'Source', index: 'Source', align: "center", width: 90, sortable: true },
                  { name: 'Title', index: 'Title', align: "center", width: 100, sortable: true },
                  { name: 'Status', index: 'Status', editable: false, width: 50, align: 'center', sortable: true },
                  { name: 'Note', index: 'Note', align: "left", width: 100, sortable: true}],
            pager: jQuery('#pager2'),
            rowList: [20, 30, 50, 100, 200],
            rowNum: 10,
            sortname: 'ID',
            sortorder: "asc",
            multiselect: true,
            viewrecords: true,
            width: 1010, height: 300,
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });

        $("#list2").focus();
        $("#btnFilter").click(function () {
            var isValidFromDate = true;
            var isValidToDate = true;
            var fromDate = "";
            var toDate = "";
            if ($('#FromDate').val() != "") {
                isValidFromDate = isDate($('#FromDate').val());
                fromDate = $('#FromDate').val();
            }
            if ($('#ToDate').val() != "") {
                isValidToDate = isDate($('#ToDate').val());
                toDate = $('#ToDate').val();
            }

            if (isValidFromDate && isValidToDate) {
                var isValid = true;
                //Check From Date can not greater than To Date
                if ($('#FromDate').val() != "" && $('#ToDate').val() != "" && isValidFromDate && isValidToDate) {
                    if (fromDate > toDate) {
                        isValid = false;
                        alert('From Date must be less than To Date');
                    }
                }

                if (isValid) {
                    var url_send = '/Exam/GetCandidateListJQGrid?examID=' + $('#ExamID').val() + ' &can_name=' + encodeURIComponent($('#txtKeyword').val()) + '&source=' + $('#Source').val();
                    url_send = url_send + '&titleId=' + $('#TitleId').val() + '&status=' + $('#Status').val() + '&from_date=' + $('#FromDate').val() + '&to_date=' + $('#ToDate').val();
                    $('#list2').setGridParam({ page: 1, url: url_send });
                    $("#list2").trigger('reloadGrid');
                }
            }
            else {
                if (!isValidFromDate) {
                    alert('Search From Date is invalid.');
                } else if (!isValidToDate) {
                    alert('Search To Date is invalid');
                }
            }            
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
            $("#AssignCandidateForm").submit();
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