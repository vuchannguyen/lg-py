<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% STT emp = (STT)ViewData.Model;%>
<style type="text/css">
    .ac_results
    {
        width: 250px !important;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("#Manager").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=EditPosition',
            { hidField: "#ManagerId", multiData: true, employee: true });

        $("#DepartmentName").change(function () {
            $("#DepartmentId").html("");
            var department = $("#DepartmentName").val();
            $("#DepartmentId").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT %></option>"));
            if (department != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
        });

        $("#LaborUnion").click(function () {
            if ($("#LaborUnion").attr("checked") == true) {
                $("#LaborUnionDate").attr("disabled", "");
                $("#LaborUnionDate").datepicker("enable");
                $("#LaborUnionDate").change(function () {
                    $("#LaborUnionDate").rules("add", "checkDate");
                    $("#LaborUnionDate").rules("add", { checkAge: ["#editForm input[name='DOB']"] });
                });
                $("#LaborUnionDate").blur(function () {
                    if ($("#LaborUnionDate").val() == "") {
                        $("span[htmlfor=LaborUnionDate]").remove();
                    }
                });
            }
            else {
                $("#LaborUnionDate").val("");
                $("#LaborUnionDate").attr("disabled", "disabled");
                $("#LaborUnionDate").rules("remove");
                $("span[htmlfor=LaborUnionDate]").remove();
                $("#LaborUnionDate").datepicker("disable");
            }
        });

        $(function () {
            $("#StartDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#ExpectedEndDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#LaborUnionDate").datepicker({
                yearRange: "-50:50",
                onClose: function () { $(this).valid(); }
            });
            if ($("#LaborUnion").attr("checked") == false && $("#LaborUnionDate").val() == '') {
                $("#LaborUnionDate").datepicker("disable");
            }
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
            },
            rules: {
                StartDate: {
                    required: true,
                    checkAge: ["#editForm input[name='DOB']"]
                },
                TaxIssueDate: { checkDate: true },
                ExpectedEndDate: {
                    checkDate: true,
                    compareExcDate: ["#editForm input[name='StartDate']", "get", "Expected End Date", "Start Date"]
                },
                departmentId: {
                    required: true
                },
                TitleId: {
                    required: true
                },
                JR: {
                    number: true,
                    remote: {
                        url: '/Employee/CheckJRExits',
                        data: {
                            jr: function () {
                                return $('#JR').val();
                            },
                            empID: function () {
                                return $("#ID").val();
                            }
                        }
                    }
                }
            }
        });
        $(function () {
            showWorkLocationTooltip();
        });
    });
</script>
<%using (Html.BeginForm("EditCompanyInfo", "STT", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<%=Html.Hidden("ID", emp.ID)%>
<%=Html.Hidden("DOB",emp.DOB.HasValue? emp.DOB.Value.ToString(Constants.DATETIME_FORMAT):"")%>
<%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
<table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td class="label" style="width: 130px">
                STT ID
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("ID", "", new { @style = "width:150px", @maxlength = "10", @readonly = true }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("ID", emp.ID, new { @style = "width:150px", @maxlength = "10", @readonly = true }));
                   }
                %>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 130px">
                <%=Constants.JOB_REQUEST_ITEM_PREFIX%>
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("JR", "", new { @style = "width:150px", @maxlength = "10", @readonly = true }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("JR", emp.JR, new { @style = "width:150px", @maxlength = "10", @readonly = true }));
                   }
                %>
                <button type="button" class="icon select" title="Select JR" onclick="CRM.pInPopup('/Common/ListJRInterview/?isOnPopup=1', 'Select Job Request', 1024); return false;">
                                                         </button>
                <button type="button" class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR','#JRApproval'); return false;">
                </button>
            </td>
        </tr>
        <tr>
            <td class="label">
                JR Approval #
            </td>
            <td class="input">
                <%=Html.TextBox("JRApproval", emp.JRApproval, new { @Style = "width: 150px", @maxlength = "10" })%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Start Date <span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("StartDate", emp.StartDate.ToString(Constants.DATETIME_FORMAT), new { @Style = "width: 150px", @maxlength = "10" })%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Expected End Date
            </td>
            <td class="input">
                <%=Html.TextBox("ExpectedEndDate",emp.ExpectedEndDate.HasValue? emp.ExpectedEndDate.Value.ToString(Constants.DATETIME_FORMAT):"", new { @Style = "width: 150px", @maxlength = "10" })%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Work Location
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("WorkLocation", "", new { @style = "width:150px", @maxlength = "100", @readonly = "readonly" }));
                   }
                   else
                   {
                       string seatCodeID = CommonFunc.GetLocation(emp.LocationCode, LocationType.SeatCode);
                       int iSeatCodeID = 0;
                       if (!string.IsNullOrEmpty(seatCodeID) && CheckUtil.IsInteger(seatCodeID))
                           iSeatCodeID = int.Parse(seatCodeID);
                       Response.Write(Html.TextBox("WorkLocation", CommonFunc.GetWorkLocationText(iSeatCodeID),
                           new { @style = "width:150px", @maxlength = "100", @readonly = "readonly" }));
                   }
                   Response.Write(Html.Hidden("LocationCode"));
                %>
                <button type="button" class="icon select" title="Select Work Location" onclick="CRM.pInPopup('/Common/ListSeatCode/?isOnPopup=1', 'Select Work Location' ,'825')">
                </button>
                <button type="button" class="icon remove" title="Remove Work Location" onclick="$('#WorkLocation').val(''); $('#LocationCode').val(''); showWorkLocationTooltip(); ">
                </button>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Department <span>*</span>
            </td>
            <td class="input">
                <%=Html.DropDownList("DepartmentName", null, new { @style = "width:156px" })%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Sub-Department <span>*</span>
            </td>
            <td class="input">
                <%=Html.DropDownList("DepartmentId", null, new { @style = "width:156px" })%>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Job Title <span>*</span>
            </td>
            <td class="input">
                <%= Html.TextBox("Title", "STT", new { @style = "width:150px", @maxlength = "10",@readonly=true })%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Project
            </td>
            <td class="input">
                <%= Html.TextBox("Project", emp.Project, new { @style = "width:150px", @maxlength = "100" })%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Manager
            </td>
            <td class="input">
                <%= Html.TextBox("Manager", CommonFunc.GetEmployeeFullName(emp.Employee,
                                        Constants.FullNameFormat.FirstMiddleLast), new { @style = "width:150px", @maxlength = "100" })%>
                <%= Html.Hidden("ManagerId") %>
            </td>
        </tr>
        <tr>
            <td class="label">
                Labor Union
            </td>
            <td class="input">
                <%=Html.CheckBox("LaborUnion",emp.LaborUnion)%>
                <%
                    if (emp.LaborUnion.HasValue)
                    {
                        if (emp.LaborUnion.Value)
                        {
                            Response.Write(Html.TextBox("LaborUnionDate", emp.LaborUnionDate.HasValue ? emp.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @Style = "width: 134px" }));
                        }
                        else
                        {
                            Response.Write(Html.TextBox("LaborUnionDate", emp.LaborUnionDate.HasValue ? emp.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @Style = "width: 134px", @disabled = "disabled" }));
                        }
                    }
                %>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" valign="middle" class="cbutton">
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
    </tbody>
</table>
<% } %>