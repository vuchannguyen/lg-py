<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    List<Training_Course> empList = ViewData.Model == null ? null : (List<Training_Course>)ViewData.Model;
    string[][] list = ViewData[Constants.SR_REPORT_WEEKLY_REQUEST_CLOSED] == null ? null : (string[][])ViewData[Constants.SR_REPORT_WEEKLY_REQUEST_CLOSED];  
%>
<div style="width:1024px; overflow-x:auto;overflow-y:auto; ">
<table cellspacing="0" cellpadding="0" border="0" class="grid">
        <tr>
            <th align="center" style="width:30px;">
                Emp ID
            </th>
            <th align="center" style="width:30px;">
                Name
            </th>
            <th align="center" style="width:30px;">
                Current Title
            </th>
            <th align="center" style="width:30px;">
                Dept
            </th>
            <th align="center" style="width:30px;">
                Direct Manager
            </th>
            <%
                if (empList != null && empList.Count > 0 )
                {
                    %>
                    <% foreach (Training_Course item in empList)
                    { %>
                        <th align="center" style="width:50px">
                            <% Response.Write(item.Name); %>
                        </th>
                    <% }
                }%>
        </tr>
        <%--Item detail--%>
        <% if (list != null)
           {
               //row
               for (int i = 0; i < list.Count(); i++)
               {
                   %>
                   <tr>
                   <td class="input" style="text-align:center;">
                   <% Response.Write(i + 1); %>
                   </td>
                   <%--col--%>
                   <% for (int j = 0; j < empList.Count(); j++)
                      {%>
                        <td class="input"  style="text-align:center;">
                        <% Response.Write(CommonFunc.Link(list[i][j], "/ServiceRequestAdmin/Detail/" + list[i][j] + "",
                               (list[i][j]!=null && list[i][j]!="")? Constants.SR_SERVICE_REQUEST_PREFIX + list[i][j]:"", true)); %>
                        </td>
                      <% } %>
                   </tr>
            <% } %>
            <tr>
            <td class="input  bold" style="text-align:center;">
                   Total
             </td>
             <% if (empList.Count() > 0)
             { %>

                 <% for (int j = 0; j < empList.Count(); j++)
                      {%>
                        <td class="input  bold"  style="text-align:center;">
                        <% Response.Write(empList[j].arrID.Count()); %>
                        </td>
                      <% } %>
                    </tr>
             <% } %>            
            <% else 
            { %>
            <td class="input  bold"  style="text-align:center;">
                        0
            </td>
            </tr>
            <% } %>
         <% } %>
</table>

</div>
<script type="text/javascript">
    $(document).ready(function () {
        
    });
</script>