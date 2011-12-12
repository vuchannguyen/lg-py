<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div class="clist">
        <table id="list" class="scroll">
        </table>
        <div id="pager" class="scroll" style="text-align: center;">
        </div>
    </div>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#list").jqGrid({
                url: '/Employee/GetListHospitalGrid/',
                datatype: 'json',
                mtype: 'GET',
                colNames: ["ID", "Name", 'Address', 'Is Public'],
                colModel: [
                    { name: 'ID', index: 'ID', align: "center", width: 10, sortable: true },
                    { name: 'Name', index: 'Name', align: "left", width: 50, sortable: true },
                    { name: 'Address', index: 'Address', align: "left", width: 60, sortable: true },
                    { name: 'IsPublic', index: 'IsPublic', align: "center", width: 15, sortable: true }],
                pager: '#pager',
                rowNum: 20,
                rowList: [30, 50, 100, 200],
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