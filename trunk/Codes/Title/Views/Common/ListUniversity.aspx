<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="clist">
    <div id="cfilter">
        <table>
            <tr>
                <td >
                    <input type="text" id="txtKeyword"  style="width: 120px;height:17px"/>
                </td>
                <td>
                    <button type="button" id="btnFilter" title="Filter" class="filter">Filter</button>
                </td>
            </tr>
        </table>
    </div>
    <table id="list" class="scroll">
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>

</div>
<script type="text/javascript">
    function ChooseUniversity(id) {
        CRM.closePopup();
        $("#UniversityDisplay").val(($("#lnk" + id).attr("university")));
        $("#UniversityId").val(id);
    }
    jQuery(document).ready(function () {
        $("#txtKeyword").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=University');
        CRM.onEnterKeyword();
        $("#btnFilter").click(function () {
            var urls = '/Common/GetListUniversityGrid/?text=' + encodeURIComponent($('#txtKeyword').val())
            $('#list').setGridParam({ url: urls
            }).trigger('reloadGrid');
        });
        jQuery("#list").jqGrid({
            url: '/Common/GetListUniversityGrid/',
            datatype: 'json',
            mtype: 'GET',
            colNames: ["ID", "Name", 'Address'],
            colModel: [
                    { name: 'ID', index: 'ID', align: "center", width: 10, sortable: false },
                    { name: 'FullName', index: 'FullName', align: "left", width: 50, sortable: false },
                    { name: 'SubDepartment', index: 'SubDepartment', align: "left", width: 70, sortable: false }],
            pager: '#pager',
            rowList: [20, 30, 50, 100, 200],
            sortname: 'ID',
            sortorder: "desc",
            viewrecords: true,
            width: 800,
            height: "300",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
    });
</script>
