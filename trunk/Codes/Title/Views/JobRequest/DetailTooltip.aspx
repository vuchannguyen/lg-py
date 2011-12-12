<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<JobRequestItem>>" %>
<%
    JobRequest jrObject = ViewData[CommonDataKey.JR_OBJECT] as JobRequest;
 %>
<style type="text/css">
    .div_jrdetail
    {
        max-height:300px;
        overflow-y:auto;
        overflow-x:hidden;
    }
    .div_jrdetail table
    {
        border-collapse:collapse;
        border-spacing:0;
        background-color: #eeeeee
    }
    .div_jrdetail table td, th
    {
        padding:2px;
        border: 1px solid #cccccc;
    }
    .div_jrdetail table th
    {
        background-color: #D4E9F9;
        text-align:center;
    }
    
</style>
<div class="div_jrdetail">
<table>
    <tr>
        <th style="width:60px">
            Reg#
        </th>
        <th style="width:150px">
            Candidate
        </th>
        <th style="width:50px">
            Emp ID
        </th>
        <th style="width:150px">
            Job Title
        </th>
        <th style="width:110px">
            Actual Start Date
        </th>
    </tr>
    <%
        foreach (JobRequestItem jrItem in Model)
        { 
    %>
            <tr>
                <td align="center"><%=Constants.JOB_REQUEST_ITEM_PREFIX + jrItem.ID %></td>
                <td><%=jrItem.Candidate == null ? "" : jrItem.Candidate%></td>
                <td align="center"><%=jrItem.EmpID == null ? "" : jrItem.EmpID%></td>
                <td><%=jrItem.JobTitleLevel == null ? "" : jrItem.JobTitleLevel.DisplayName%></td>
                <td><%=jrItem.ActualStartDate == null ? "" : jrItem.ActualStartDate.Value.ToString("dd-MMM-yyyy") %></td>
            </tr>        
    <%
        }
    %>
</table>
</div>