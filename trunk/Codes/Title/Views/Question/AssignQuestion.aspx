<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<style type="text/css">
    .ui-jqgrid tr.jqgrow td
    {
        white-space: normal;
    }
</style>
<script type="text/javascript" src="/Scripts/CRM.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        CRM.onEnterKeyword();
        jQuery("#listenlist").jqGrid({
            url: '/Question/GetListJQGrid_Assign?ids=' + '<%=Request["ids"]%>'
                + '&sectionID=' + '<%=Request["sectionID"]%>'
                + '&id=' + '<%=Request["id"]%>',
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Question ID', 'Question'],
            colModel: [
                  { name: 'QuestionID', index: 'ID', align: "center", width: 50, hidden: true },
                  { name: 'Question', index: 'QuestionContent', align: "justify", width: 550, sortable: false}],
            pager: jQuery('#listenpager'),
            rowList: [20, 30, 50, 100, 200],
            sortname: 'QuestionID',
            sortorder: "asc",
            multiselect: { required: false, width: 24 },
            viewrecords: true,
            width: 750, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });

        $("#btnFilter").click(function () {
            var name = encodeURIComponent($('#txtKeyword').val());
            if (name == encodeURIComponent('<%= Constants.QUESTIONNAME  %>')) {
                name = "";
            }
            var targetUrl = '/Question/GetListJQGrid_Assign?questionName=' + name + '&ids=' + '<%=Request["ids"]%>'
                + '&id=' + '<%=Request["id"]%>' + '&sectionID=' + '<%=Request["sectionID"]%>';
            $('#listenlist').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
        });

        $("#btnSave").click(function () {
            $(this).attr("disabled", true);
            var arrSelectedRow = getJqgridSelectedRows("#listenlist").split(",");

            $.each(arrSelectedRow, function (i, rowID) {
                if (rowID) {
                    var lastRowID = ($("#questionlist tr").length > 1)
                        ? parseInt($("#questionlist tr:last").attr("id")) : 0;
                    var questionID = $("#listenlist").getCell(rowID, "QuestionID");
                    var addedRow = {
                        cb: "<input role=\"checkbox\" type=\"checkbox\" id=\"jqg_questionlist_" + (lastRowID + 1)
                            + "\" class=\"cbox\" name=\"jqg_questionlist_" + (lastRowID + 1) + "\">",
                        QuestionID: questionID + "<input type='hidden' name='hidListQuestionID' value='"
                            + questionID + "'/>",
                        QuestionOrder: "" + $("#questionlist tr").length,
                        Question: $("#listenlist tr:eq(" + rowID + ")")
                            .find("td[aria-describedby|='listenlist_Question']").html(),
                        Action: "<input type=\"button\" class=\"icon detailview\" title=\"Details view\" "
                            + "onclick=\"CRM.popup('/Question/Details?id="
                            + $("#listenlist").getCell(rowID, "QuestionID") + "', 'View', "
                            + '<%=Constants.DETAILS_POPUP_WIDTH %>' + ")\" />"
                    };
                    $("#questionlist").jqGrid('addRowData', lastRowID + 1, addedRow, "last");
                    //Set the class for the added row
                    var rowClass = "ui-widget-content jqgrow ui-row-ltr";
                    var rowClass_alt = "ui-widget-content jqgrow ui-row-ltr ui-row-alt";
                    if (lastRowID % 2 == 0)
                        $("#questionlist tr:last").attr("class", rowClass);
                    else
                        $("#questionlist tr:last").attr("class", rowClass_alt);
                }
            });
            CRM.closePopup();
        });
    }); 
</script>
<%using (Html.BeginForm("AssignQuestion", "Question", FormMethod.Post, 
      new { id = "assignForm", @class = "form" }))
  {%>
<div id="cfilter">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <input type="text" maxlength="100" style="width: 150px" value="<%=Constants.QUESTIONNAME%>"
                    id="txtKeyword" onfocus="ShowOnFocus(this,'<%=Constants.QUESTIONNAME%>')" 
                    onblur="ShowOnBlur(this,'<%=Constants.QUESTIONNAME%>')" />
            </td>
            <td>
                <button type="button" id="btnFilter" title="Filter" class="button filter">
                    Filter</button>
            </td>
        </tr>
    </table>
</div>
<div class="clist">
    <table id="listenlist" class="scroll">
    </table>
    <div id="listenpager" class="scroll" style="text-align: center;">
    </div>
</div>
<div id="divEdit" style="display: none; cursor: default;">
</div>
<div id="div1" style="padding-top: 10px; text-align: center">
    <input type="button" id="btnSave" class="button save" value="" title="Update" />
    <input type="button" class="button cancel" value="" title="Cancel" onclick="CRM.closePopup()" />
</div>
<%
    } 
%>