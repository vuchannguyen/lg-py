<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("Assign", "ExamQuestion", FormMethod.Post, new { id = "AssignQuestionForm", @class = "form" }))
  {%>
<%= TempData["Message"]%>
<div class="clist">
    <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" id="txtKeyword" style="width: 180px; height: 17px" />
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="filter">
                        Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table id="list2">
        </table>
        <div id="pagertes" style="text-align: center;">
        </div>
        <%=Html.Hidden(CommonDataKey.EXAM_QUESTION_ID, ViewData[CommonDataKey.EXAM_QUESTION_ID])%>
        <%=Html.Hidden(CommonDataKey.EXAM_QUESTION_SECTION_ID, ViewData[CommonDataKey.EXAM_QUESTION_SECTION_ID])%>
        <%=Html.Hidden("AssignIDs")%>
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td align="center">
                <input type="button" id="btnSubmit" class="save" value="" alt="Update" onclick="onSubmit()" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    jQuery(document).ready(function () {
        CRM.onEnterKeyword();
        $("#btnFilter").click(function () {
            var name = encodeURIComponent($('#txtKeyword').val());            
            var urls = '/ExamQuestion/GetQuestionListJQGrid/?examQuestionSectionId=' + $('#ExamQuestionSectionID').val() + '&text=' + name;
            $('#list2').setGridParam({ page: 1, url: urls
            }).trigger('reloadGrid');
        });

        jQuery("#list2").jqGrid({
            url: '/ExamQuestion/GetQuestionListJQGrid/?examQuestionSectionId=' + $('#ExamQuestionSectionID').val() + '&text=' + encodeURIComponent($('#txtKeyword').val()),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['ID', 'Content'],
            colModel: [
                  { name: 'ID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'Content', index: 'Title', align: "left", width: 300, sortable: false}],
            pager: '#pagertes',            
            rowNum:10,
            sortname: 'ID',
            sortorder: "asc",
            multiselect: { required: false, width: 15 },
            viewrecords: true,
            width: 800, height: 300,
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
        //set width for checkbox column
        jQuery("#jqgh_cb").parent().css("width", "15px");
        jQuery("tr#_empty > td:first").css("width", "15px");
    });

    function onSubmit() {
        //var arrID = CRM.getSelectedIDs('#list2', 'ID');
        var arrID = getJqgridSelectedIDs('#list2', 'ID');        
        if (arrID == '') {
            CRM.summary("Please select row(s) to assign!", 'block', 'msgError');
        }
        else {
            $("#btnSubmit").attr("disabled", "disabled");            
            $("#AssignIDs").val(arrID);
            $("#AssignQuestionForm").submit();
        }
    }
</script>
<style>
    .ui-jqgrid tr.jqgrow td
    {
        white-space: normal;
    }
</style>
<% } %>