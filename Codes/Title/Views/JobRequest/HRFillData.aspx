<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    jQuery(document).ready(function () {
        $(function () {
            $("#IssueDateView").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#ActualStartDateView").datepicker({
                onClose: function () { $(this).valid(); }
            });
        });

        $("#editForm").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            }
        });
        if( $("#StatusID").val() != '<%= Constants.JR_ITEM_STATUS_SUCCESS %>')
        {
            $("#Approval").rules("remove");
        }
        else
        {
             $("#Approval").rules("add", { required: true, remote: '<%= Url.Action("IsApprovalExist", "JobRequest") %>' });
        }

        $("#StatusID").change(function () {
            if ($("#StatusID").val() != '<%= Constants.JR_ITEM_STATUS_SUCCESS %>') {
                $("#Candidate").rules("remove");
                $("#EmpID").rules("remove");
                $("#FinalTitleId").rules("remove");
                $("#IssueDate").rules("remove");
                $("#Gender").rules("remove");
                $("#ActualStartDate").rules("remove");
                $("#Approval").rules("remove");
            }
            else {
                $("#Candidate").rules("add", { required: true });
                $("#EmpID").rules("add", { required: true,
                    remote: {
                        url: "/Employee/CheckIDExits",
                        type: "post",
                        data: {
                            id: function () {
                                return $("#EmpID").val();
                            }
                        }
                    }
                });
                $("#FinalTitleId").rules("add", { required: true });
                $("#IssueDate").rules("add", { required: true,
                    checkDate: true,
                    compareDate: ["#editForm input[name='RequestDate']", "get", "Issue Date", "Request Date"]
                });
                $("#Gender").rules("add", { required: true });

                $("#ActualStartDate").rules("add", { required: true,
                    checkDate: true,
                    compareDate: ["#editForm input[name='IssueDate']", "get", "Actual Start Date", "Issue Date"]
                });

                $("#Approval").rules("add", { required: true, remote: '<%= Url.Action("IsApprovalExist", "JobRequest") %>' });
            }
        });
        //$("#StatusID").change();
    });
</script>
<%using (Html.BeginForm("HRFillData", "JobRequest", FormMethod.Post, new { id = "editForm" }))
  {%>
<% 
    JobRequestItem jri = (JobRequestItem)ViewData.Model;
    Response.Write(Html.Hidden("RequestDate", jri.JobRequest.RequestDate.ToString(Constants.DATETIME_FORMAT))); 
    Response.Write(Html.Hidden("AssignID", jri.AssignID));
    Response.Write(Html.Hidden("AssignRole", jri.AssignRole));
    Response.Write(Html.Hidden("InvolveID", jri.JobRequest.InvolveID));
    Response.Write(Html.Hidden("InvolveRole", jri.JobRequest.InvolveRole));
    Response.Write(Html.Hidden("InvolveDate", jri.JobRequest.InvolveDate));
    Response.Write(Html.Hidden("ID", jri.ID));
    Response.Write(Html.Hidden("UpdateDate", jri.UpdateDate.ToString()));
    Employee objEmployee = (Employee)ViewData["ObjEmployee"];
    STT objSTT = (STT)ViewData["ObjSTT"];
%>

<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required" style="width: 130px">
            Candidate <span>*</span>
        </td>
        <td >
            <%
                if (objEmployee != null)
                {
                    Response.Write(Html.TextBox("Candidate", objEmployee.FirstName + " " + objEmployee.MiddleName + " " + objEmployee.LastName, new { @readonly="readonly",@maxlength = "70", @style = "width:130px" }));
                }
                else if (objSTT != null)
                {
                    Response.Write(Html.TextBox("Candidate", objSTT.FirstName + " " + objSTT.MiddleName + " " + objSTT.LastName, new { @readonly = "readonly", @maxlength = "70", @style = "width:130px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("Candidate", !string.IsNullOrEmpty(jri.Candidate) ? jri.Candidate : string.Empty, new { @readonly = "readonly", @maxlength = "70", @style = "width:130px" }));
                }
               %>
        </td>
        <td class="label required">
            Emp ID <span>*</span>
        </td>
        <td>
            <%
                if (objEmployee != null)
                {
                    Response.Write(Html.TextBox("EmpID", objEmployee.ID, new { @readonly = "readonly", @maxlength = "4", @style = "width:130px" }));
                }
                else if (objSTT != null)
                {
                    Response.Write(Html.TextBox("EmpID", objSTT.ID, new { @readonly = "readonly", @maxlength = "10", @style = "width:130px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("EmpID", !string.IsNullOrEmpty(jri.Candidate) ? jri.EmpID : string.Empty, new { @readonly = "readonly", @maxlength = "4", @style = "width:130px" }));
                }
               %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Approval <span>*</span></td>
        <td><%Response.Write(Html.TextBox("Approval", !string.IsNullOrEmpty(jri.Approval) ? jri.Approval : string.Empty, new { @maxlength = "10", @style = "width:130px" }));%></td>
        <td>
            </td>
        <td>
            &nbsp;</td>
        
    </tr>
    <tr>
        <td class="label required">
            Job Title <span>*</span>
        </td>
        <td>
            <%
                if (objEmployee != null)
                {
                    Response.Write(Html.DropDownList("FinalTitleView", null, Constants.JOB_REQUEST_TITLE_SELECT, new { @disabled = "disabled", @style = "width:136px" }));
                    Response.Write(Html.Hidden("FinalTitleId", objEmployee.TitleId));
                }
                else if (objSTT != null)
                {
                    Response.Write(Html.TextBox("Title", Constants.STT_DEFAULT_VALUE, new { @style = "width:130px", @readonly = "readonly" }));
                }
                else
                {
                    Response.Write(Html.DropDownList("FinalTitleId", null, Constants.JOB_REQUEST_TITLE_SELECT, new { @disabled = "disabled", @style = "width:136px" }));
                }
               %>
        </td>
        <td class="label required" style="width: 120px">
            Issue Date <span>*</span>
        </td>
        <td>
            <%
                if (objEmployee != null)
                {
                    Response.Write(Html.TextBox("IssueDateView", objEmployee.IssueDate.HasValue?objEmployee.IssueDate.Value.ToString(Constants.DATETIME_FORMAT):string.Empty
                        , new { @maxlength = "10", @style = "width:136px", @readonly = "readonly" }));
                    Response.Write(Html.Hidden("IssueDate", objEmployee.IssueDate.HasValue ? objEmployee.IssueDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty));
                    Response.Write("<script>$('#IssueDateView').datepicker('disable')</script>");
                }
                else if (objSTT != null)
                {
                    Response.Write(Html.TextBox("IssueDateView", objSTT.IssueDate.HasValue ? objSTT.IssueDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty
                        , new { @maxlength = "10", @style = "width:136px", @readonly = "readonly"}));
                    Response.Write(Html.Hidden("IssueDate", objSTT.IssueDate.HasValue ? objSTT.IssueDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty));
                    Response.Write("<script>$('#IssueDateView').datepicker('disable')</script>");
                }
                else
                {
                    Response.Write(Html.TextBox("IssueDateView", jri.IssueDate != null ?
                       jri.IssueDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty,
                       new { @maxlength = "10", @style = "width:130px", @readonly = "readonly" }));
                    Response.Write("<script>$('#IssueDateView').datepicker('disable')</script>");
                }
               %>
        </td>
        
    </tr>
    <tr>
        <td class="label required">
            Gender <span>*</span>
        </td>
        <td>
            <%
                if (objEmployee != null)
                {
                    SelectList selectList = new SelectList(Constants.Gender, "Value", "Text",objEmployee.Gender);
                    Response.Write(Html.DropDownList("GenderView", selectList, " -Choose-", new { @disabled = "disabled", @style = "width:136px" }));
                    Response.Write(Html.Hidden("Gender", objEmployee.Gender));
                }
                else if (objSTT != null)
                {
                    SelectList selectList = new SelectList(Constants.Gender, "Value", "Text", objSTT.Gender);
                    Response.Write(Html.DropDownList("GenderView", selectList, " -Choose-", new { @disabled = "disabled", @style = "width:136px" }));
                    Response.Write(Html.Hidden("Gender", objSTT.Gender));
                }
                else
                {
                    SelectList selectList = new SelectList(Constants.Gender, "Value", "Text", jri.Gender);
                    Response.Write(Html.DropDownList("Gender", selectList, " -Choose-", new { @disabled = "disabled", @style = "width:136px" }));
                }
               %>
        </td>
        <td class="label required">
            Actual Start Date <span>*</span>
        </td>
        <td>
            <%
                if (objEmployee != null)
                {
                    Response.Write(Html.TextBox("ActualStartDateView", objEmployee.StartDate.ToString(Constants.DATETIME_FORMAT)
                        , new { @maxlength = "10", @style = "width:136px", @readonly = "readonly" }));
                    Response.Write(Html.Hidden("ActualStartDate", objEmployee.StartDate.ToString(Constants.DATETIME_FORMAT)));
                    Response.Write("<script>$('#ActualStartDateView').datepicker('disable')</script>");
                }
                else if (objSTT != null)
                {
                    Response.Write(Html.TextBox("ActualStartDateView", objSTT.StartDate.ToString(Constants.DATETIME_FORMAT)
                        , new { @maxlength = "10", @style = "width:136px", @readonly = "readonly" }));
                    Response.Write(Html.Hidden("ActualStartDateView", objSTT.StartDate.ToString(Constants.DATETIME_FORMAT)));
                    Response.Write("<script>$('#ActualStartDateView').datepicker('disable')</script>");
                }
                else
                {
                    Response.Write(Html.TextBox("ActualStartDateView", jri.ActualStartDate != null ?
                       jri.ActualStartDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty,
                       new { @maxlength = "10", @style = "width:130px", @readonly = "readonly" }));
                    Response.Write(Html.Hidden("ActualStartDateView", jri.ActualStartDate != null ?
                       jri.ActualStartDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty));
                    Response.Write("<script>$('#ActualStartDateView').datepicker('disable')</script>");
                }
               %>
            <%%>
        </td>
    </tr>

    <tr>
        <td class="label">
            Probation Salary
        </td>
        <td>
            <%Response.Write(Html.TextBox("ProbationSalary", !string.IsNullOrEmpty(jri.ProbationSalary) ? EncryptUtil.Decrypt(jri.ProbationSalary) : string.Empty, new { @maxlength = "9", @style = "width:130px" }));%>
        </td>
        <td class="label">
            Contracted Salary
        </td>
        <td>
            <%Response.Write(Html.TextBox("ContractedSalary",!string.IsNullOrEmpty(jri.ContractedSalary) ? EncryptUtil.Decrypt(jri.ContractedSalary) : string.Empty, new { @maxlength = "9", @style = "width:130px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Probation Salary Note
        </td>
        <td colspan="3">
            <% if (ViewData.Model != null)
               {
                   Response.Write(Html.TextBox("ProbationSalaryNote", !string.IsNullOrEmpty(jri.ProbationSalaryNote) ? jri.ProbationSalaryNote : string.Empty, new { @maxlength = "150", @style = "width:460px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Contracted Salary Note
        </td>
        <td colspan="3">
            <%Response.Write(Html.TextBox("ContractedSalaryNote",!string.IsNullOrEmpty(jri.ContractedSalaryNote) ? jri.ContractedSalaryNote : string.Empty, new { @maxlength = "150", @style = "width:460px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Probation Months
        </td>
        <td colspan="3">
            <% Response.Write(Html.TextBox("ProbationMonths", jri.ProbationMonths.HasValue ? jri.ProbationMonths.ToString() : string.Empty, new { @maxlength = "2", @style = "width:130px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Status
        </td>
        <td>
            <% Response.Write(Html.DropDownList("StatusID", null, new { @style = "width:136px" }));%>
        </td>
        <td>
            
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<% } %>