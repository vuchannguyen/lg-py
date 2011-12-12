<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<CRM.Models.JobRequestItem>" %>
<style type="text/css">
    .jrdetail td.label
    {
        width: 130px !important
    }
</style>
<table style="margin:5px; padding-left:10px;width:300px; max-width:600px" class="view jrdetail">
    <tr>
        <td class="label">
            Reg#
        </td>
        <td class="input">
            <%=Constants.JOB_REQUEST_ITEM_PREFIX + Model.ID %>
        </td>
    </tr>
    <tr>
        <td  class="label">Candidate</td>
        <td class="input"><%=Model.Candidate %></td>
    </tr>
    <tr>
        <td class="label">Emp ID</td>
        <td class="input"><%=Model.EmpID %></td>
    </tr>
    <tr>
        <td  class="label">Job Tilte</td>
        <td class="input"><%=Model.JobTitleLevel.DisplayName %></td>
    </tr>
    <tr>
        <td class="label">Issue Date</td>
        <td class="input"><%=Model.IssueDate.Value.ToString("yyyy-MMM-dd") %></td>
    </tr>
    <tr>
        <td  class="label">Gender</td>
        <td class="input"><%= Constants.Gender.Single(p=>bool.Parse(p.Value) == Model.Gender).Text %></td>
    </tr>
    <tr>
        <td class="label">Actual Start Date</td>
        <td class="input"><%=Model.ActualStartDate.Value.ToString("yyyy-MMM-dd")%></td>
    </tr>
    <tr>
        <td class="label">Probation Salary</td>
        <td class="input"><%=(bool)ViewData[CommonDataKey.JR_CAN_VIEW_SALARY] ?  
                (!string.IsNullOrEmpty(Model.ProbationSalary) ? EncryptUtil.Decrypt(Model.ProbationSalary) : "") : 
                Constants.PRIVATE_DATA%>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="label">Contracted Salary</td>
        <td class="input"><%=(bool)ViewData[CommonDataKey.JR_CAN_VIEW_SALARY] ?
                (!string.IsNullOrEmpty(Model.ContractedSalary) ? EncryptUtil.Decrypt(Model.ContractedSalary) : "") : 
                Constants.PRIVATE_DATA%>&nbsp;
        </td>
    </tr>
     <tr>
        <td class="label">Probation Salary Note</td>
        <td class="input"><%= Model.ProbationSalaryNote %>&nbsp;</td>
    </tr>
    <tr>
        <td class="label">Contracted Salary Note</td>
        <td class="input"><%= Model.ContractedSalaryNote %>&nbsp;</td>
    </tr>
</table>
