<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ListCourse</title>
    <script type="text/javascript">
        var courseType = '<%=Request["courseType"]%>';
        function getListTargetUrl() {
            var url = '<%=Url.Action("GetListCourseJQGrid")%>' +
                '/?name=' + encodeURIComponent($("#txtSearch").val()) +
                '&courseType=' + courseType +
                '&type=' + $('#<%=CommonDataKey.TRAINING_CENTER_COURSE_TYPE%>').val();
            return url;
        }
        function chooseCourse(courseName, courseId) {
            $("#CourseName").val(courseName);
            $("#CourseName").valid();
            $("#CourseId").val(courseId);
            if ('<%=Request["n"]%>' != '')
                CRM.pInPopupClose();
            else
                CRM.closePopup();
        }
        $(document).ready(function () {

            $("#txtSearch").keypress(function (event) {
                if (event.keyCode == 13) {
                    $("#btnFilterCourse").click();
                }
            });
            jQuery("#courselist").jqGrid({
                url: getListTargetUrl(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['RealID', 'ID', 'Name', 'Type', 'Status', 'Active', 'Key Trainers'],
                colModel: [
                  { name: 'ID', index: 'ID', align: "center", hidden: true },
                  { name: 'CourseId', index: 'CourseId', align: "left", width: 50, sortable: true },
                  { name: 'CourseName', index: 'CourseName', align: "left", width: 70, sortable: true },
                  { name: 'TypeName', index: 'TypeName', align: "center", width: 30, sortable: true },
                  { name: 'Status', index: 'Status', align: "center", width: 30, sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 30, sortable: false },
                  { name: 'KeyTrainers', index: 'KeyTrainers', align: "left", width: 70, sortable: false }
                  ],
                pager: jQuery('#coursepager'),
                rowList: [20, 30, 50, 100, 200],
                viewrecords: true,
                width: 770, height: "auto",
                multiselect: false,
                grouping: false,
                sortname: 'CourseId',
                sortorder: 'asc',
                rowNum: 20,
                page: 1,
                imgpath: '/scripts/grid/themes/basic/images',
                loadui: 'block',
                loadComplete: function () {
                    if (courseType == '<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH %>')
                        $("#courselist").hideCol("TypeName");
                }
            });

            $("#btnFilterCourse").click(function () {
                var url_send = getListTargetUrl();
                $('#courselist').setGridParam({ url: url_send, page: 1 });
                $("#courselist").trigger('reloadGrid');
            });
        });

    </script>
</head>
<body>
    <div>
        <div id="cfilter">
        <table>
            <tr>
                <td>
                    <input type="text" maxlength="50" style="width: 150px" value="<%=Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL%>"
                        id="txtSearch" onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL%>')"
                        onblur="ShowOnBlur(this,'<%= Constants.TRAINING_CENTER_COURSE_LIST_TXT_KEYWORD_LABEL  %>')" />
                </td>
                <td>
                    <%if(Request["courseType"] == Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL.ToString()) %>
                    <%=Html.DropDownList(CommonDataKey.TRAINING_CENTER_COURSE_TYPE, null,
                        Constants.TRAINING_CENTER_LIST_COURSE_TYPE_LABEL, new { @style = "width:150px" })%>
                </td>
                <td>
                    <button type="button" id="btnFilterCourse" title="Filter" style="float: left" class="button filter">
                        Filter</button>
                </td>
            </tr>
        </table>
        </div>
        <div class="clist">
            <table id="courselist" class="scroll">
            </table>
            <div id="coursepager" class="scroll" style="text-align: center;">
            </div>
        </div>
    </div>
</body>
</html>
