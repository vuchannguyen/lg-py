<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="clist">
        <table id="list1" class="scroll">
        </table>
        <div id="pager1" class="scroll" style="text-align: center;">
        </div>
 </div>  
 <script type="text/javascript">
     function getListTargetUrl1() {
         var url = '/ServiceRequestAdmin/GetListJQGridReportActive/?' +
                'startDate=' + $('#Fromdate').val() +
                '&endDate=' + $('#Todate').val();
         return url;
     }
     $(document).ready(function () {

         jQuery("#list1").jqGrid({
             url: getListTargetUrl1(),
             datatype: 'json',
             mtype: 'GET',
             colNames: ['Assigned to / Status ', 'Active', 'New', 'Open', 'To be Approved', 'Closed', 'Verified closed', 'Pending', 'Postponed', 'Approved', 'Rejected'],
             colModel: [
                  { name: 'DisplayName', index: 'DisplayName', align: "left", width: 160, sortable: true },
                  { name: 'Active', index: 'Active', align: "center", width: 80, sortable: true },
                  { name: 'New', index: 'New', align: "center", width: 60, sortable: true },
                  { name: 'Open', index: 'Open', align: "center", width: 60, sortable: true },
                  { name: 'ToBeApproved', index: 'ToBeApproved', align: "center", width: 120, sortable: true },
                  { name: 'Closed', index: 'Closed', align: "center", width: 80, sortable: true },
                  { name: 'VerifiedClose', index: 'VerifiedClose', align: "center", width: 120, sortable: true },
                  { name: 'Pending', index: 'Pending', align: "center", width: 80, sortable: true },
                  { name: 'Postponed', index: 'Postponed', align: "center", width: 80, sortable: true },
                  { name: 'Approved', index: 'Approved', align: "center", width: 80, sortable: true },
                  { name: 'Rejected', index: 'Rejected', align: "center", width: 80, sortable: true}],
             viewrecords: true,
             width: 1024, height: "auto",
             rownumbers: true,
             grouping: false,
             sortname: '<%= (string)ViewData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_COLUMN]%>',
             sortorder: '<%= (string)ViewData[Constants.SR_REPORT_ACTIVE_LIST_ADMIN_ORDER]%>',
             imgpath: '/scripts/grid/themes/basic/images',
             loadui: 'block'             
         });

     });
 </script>