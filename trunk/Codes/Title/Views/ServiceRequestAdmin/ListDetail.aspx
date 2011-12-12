<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<div id="cactionbutton">           
        <button id="btnExport1" type="button" title="Export" class="button export">Export</button>              
</div>
<div style="background-color:#EEEEEE;height:300px;overflow-x:auto;overflow-y:auto;">
<%
    ArrayList list = ViewData.Model == null ? null : (ArrayList)ViewData.Model;    
%>
<table cellspacing="0" cellpadding="0" border="0" class="grid">
        <tr>
        <th style="text-align:center;width:80"">
            ID
        </th>
        <th style="text-align:center;width:100">
            Title
        </th>        
        <th style="text-align:center;width:60">
            Request User
        </th>
        <th style="text-align:center;width:80">
            Request Date
        </th>
        <th style="text-align:center;width:80">
            Status
        </th>
        <th style="text-align:center;width:40">
            Total time
        </th>
        </tr>
        <%--Item detail--%>
        <% if (list != null)
           {
               //row
               foreach (string[] item in list)
               {
                   %>
                   <tr>
                       <td class="input" style="text-align:center">
                        <% Response.Write(Constants.SR_SERVICE_REQUEST_PREFIX + item[0]); %>
                       </td>                                      
                       <td class="input" style="text-align:left">
                        <% Response.Write(CommonFunc.Link(item[0], "/ServiceRequestAdmin/Detail/" + item[0] + "", (item[1].Length > Constants.SR_MAX_LENGTH_TITLE ?
                                Server.HtmlEncode(item[1].Substring(0, Constants.SR_MAX_LENGTH_TITLE)) + "..." : Server.HtmlEncode(item[1])), true));  %>                        
                       </td> 
                       <td class="input" style="text-align:center">
                        <% Response.Write(item[2]); %>
                       </td>                                      
                       <td class="input" style="text-align:center">
                        <% Response.Write(item[3]); %>
                       </td>                                   
                       <td class="input" style="text-align:center">
                        <% Response.Write(item[4]); %>
                       </td>                                      
                       <td class="input" style="text-align:center">
                        <% Response.Write(item[5]); %>
                       </td>                                      
                   </tr>
            <% } %>
        <% } %>
</table>
</div>
<div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
</div>
<head>
<link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
 <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
 <script>
     $(document).ready(function () {
         $("#btnExport1").click(function () {
             window.location = "/ServiceRequestAdmin/ExportListDetail";
         });
         ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/ServiceRequestAdmin/ShowTitleTooltip");
     });
 </script>
</head>