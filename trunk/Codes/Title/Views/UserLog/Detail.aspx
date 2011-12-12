<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
 
<% Response.Write(Html.Hidden("UserName", ViewData["UserName"]));
   Response.Write(Html.Hidden("Date", ViewData["Date"]));
   Response.Write(Html.Hidden("Action", ViewData["Action"]));
   Response.Write(Html.Hidden("Count", ViewData["Count"]));
   Response.Write(Html.Hidden("Table", ViewData["Table"]));
   Response.Write(Html.Hidden("Type", ViewData["Type"]));  
    %>
   <div>
   <table width="100%" height="30px"><tr>
        <td class='log_detail'><span style="font-size: 11px; font-weight: normal;">UserName: </span><%=  Html.Encode(ViewData["UserName"]) %>
        <span style="font-size: 11px; font-weight: normal;">&nbsp;&nbsp;Date: </span>
        <%=  Html.Encode(DateTime.Parse(ViewData["date"].ToString()).ToString(Constants.DATETIME_FORMAT_VIEW)) %>
        
        </td>
   </tr> </table>
  
    </div>
   
<div class="clist" style="width:100%;height:400px" >
        <table id="listDetail" >
        </table>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        if ($("#Action").val() == '<%=Constants.INSERT %>') {
            jQuery("#listDetail").jqGrid({
                url: '/UserLog/GetListInsertJQGrid/?name=' + $("#UserName").val() + '&date=' + $("#Date").val() + '&table=' + $("#Table").val() + '&type=' + $("#Type").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['#', 'Table', 'Column Name', 'Value'],
                colModel: [
                  { name: '#', index: '#', align: "center", width: 50, title: false, sortable: false },
                  { name: 'Table', index: 'Table', align: "center", width: 150, sortable: false, title: false },
                  { name: 'ColumnName', index: 'ColumnName', align: "left", width: 250, sortable: false, title: false },
                  { name: 'Value', index: 'Department', align: "left", width: 300, sortable: false, title: false}],
                width: 750,
                height:340,
                sortname: 'Table',
                viewrecords: true,
                sortorder: "asc",
                loadui: 'block',
                caption: 'Insert (' + $("#Count").val() + ')'                
            });
        }
        else if ($("#Action").val() == '<%=Constants.UPDATE %>') {
            jQuery("#listDetail").jqGrid({
                url: '/UserLog/GetListUpdateJQGrid/?name=' + $("#UserName").val() + '&date=' + $("#Date").val() + '&table=' + $("#Table").val() + '&type=' + $("#Type").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['#', 'Table', 'Column Name', 'Old Value', 'New Value'],
                colModel: [
                  { name: '#', index: '#', align: "center", width: 50, title: false, sortable: false },
                  { name: 'Table', index: 'Table', align: "center", width: 150, title: false, sortable: false },
                  { name: 'ColumnName', index: 'ColumnName', align: "left", width: 150, sortable: false, title: false },
                  { name: 'OldValue', index: 'OldValue', align: "left", width: 200, sortable: false, title: false },
                  { name: 'NewValue', index: 'NewValue', align: "left", width: 200, sortable: false, title: false}],
                width: 750,
                height: 300,
                sortname: 'Table',
                viewrecords: true,
                sortorder: "asc",
                loadui: 'block',
                caption: 'Update (' + $("#Count").val() + ')'    
            });
        }
        else {
            jQuery("#listDetail").jqGrid({
                url: '/UserLog/GetListDeleteJQGrid/?name=' + $("#UserName").val() + '&date=' + $("#Date").val() + '&table=' + $("#Table").val() + '&type=' + $("#Type").val(),
                datatype: 'json',
                mtype: 'GET',
                colNames: ['#', 'Table', 'Column Name', 'Value'],
                colModel: [
                  { name: '#', index: '#', align: "center", width: 50, title: false, sortable: false },
                  { name: 'Table', index: 'Table', width: 150, align: "center", sortable: false, title: false },
                  { name: 'ColumnName', index: 'ColumnName', align: "left", width: 250, title: false, sortable: false },
                  { name: 'Value', index: 'Department', align: "left", width: 300, title: false, sortable: false}],
                width: 750,
                sortname: 'Table',
                height: 300,
                viewrecords: true,
                sortorder: "asc",
                multiselect: true,
                loadui: 'block',
                caption: 'Delete (' + $("#Count").val() + ')'   
            });
        }
    });
            
</script>
