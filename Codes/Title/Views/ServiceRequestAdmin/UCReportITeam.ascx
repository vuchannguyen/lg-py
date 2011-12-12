<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <div class="clist">
        <table id="list2" class="scroll">
        </table>
        <div id="pager2" class="scroll" style="text-align: center;">
        </div>
 </div>  
 <script type="text/javascript">
     function getListTargetUrl3() {
         var url = '/ServiceRequestAdmin/GetListJQGridReportITeam/?' +
                'startDate=' + $('#Fromdate').val() +
                '&endDate=' + $('#Todate').val();
         return url;
     }

     $(document).ready(function () {
         
         jQuery("#list2").jqGrid({
             url: getListTargetUrl3(),
             datatype: 'json',
             mtype: 'GET',
             colNames: ['Name Helpdesk', 'Amount time work for 1 week', 'Total request opened', 'Total request closed', 'Total request doing'],
             colModel: [
                  { name: 'DisplayName', index: 'DisplayName', align: "left", width: 120, sortable: true },
                  { name: 'AmountTime', index: 'AmountTime', align: "center", width: 100, sortable: true },
                  { name: 'TotalOpen', index: 'TotalOpen', align: "center", width: 80, sortable: true },
                  { name: 'TotalClose', index: 'TotalClose', align: "center", width: 80, sortable: true },
                  { name: 'TotalDoing', index: 'TotalDoing', align: "center", width: 80, sortable: true}],
             viewrecords: true,
             width: 1024, height: "auto",
             rownumbers: true,
             grouping: false,
             sortname: '<%= (string)ViewData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_COLUMN]%>',
             sortorder: '<%= (string)ViewData[Constants.SR_REPORT_ITEAM_LIST_ADMIN_ORDER]%>',
             imgpath: '/scripts/grid/themes/basic/images',
             loadui: 'block'
         });
         
     });
 </script>