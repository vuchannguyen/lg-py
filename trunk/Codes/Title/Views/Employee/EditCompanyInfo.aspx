<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<% Employee emp = (Employee)ViewData.Model;%>
<style type="text/css">
    .ac_results
    {
        width: 180px !important;
    }
</style>
<script type="text/javascript">
    var resultOrder = true;
    var result = true;
    $(document).ready(function () {
        $("#Manager").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=EditPosition',
            { hidField: "#ManagerId", multiData: true, employee: true });
        $("#DepartmentName").change(function () {
            var firstItem = '<%= Constants.FIRST_ITEM %>';
            $("#TitleId").html("");
            $("#DepartmentId").html("");
            var department = $("#DepartmentName").val();
            $("#TitleId").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
            $("#DepartmentId").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT %></option>"));
            if (department != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#TitleId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
        });

        $("#InsuranceHospitalID").change(function () {
            // Display textbox TitleEffectDate when change value of Title
            if ($("#InsuranceHospitalID").val() != '<%=emp.InsuranceHospitalID %>') {
                $("#trHospitalEffectDate").css("display", "");
                $("#hospitalEffectDate").rules("add", "required");
            } else {
                $("#trHospitalEffectDate").css("display", "none");
                $("#hospitalEffectDate").val("");
                $("#hospitalEffectDate").rules("remove");
            }
        });

        $("#DepartmentId").change(function () {
            // Display textbox DepartEffectDate when change value of Department                                
            if ($("#DepartmentId").val() != '<%=emp.DepartmentId %>') {
                $("#trdepartEffectDate").css("display", "");
                $("#departEffectDate").rules("add", "required");
            } else {
                $("#trdepartEffectDate").css("display", "none");
                $("#departEffectDate").val("");
                $("#departEffectDate").rules("remove");
            }
        });

        $("#TitleId").change(function () {
            // Display textbox TitleEffectDate when change value of Title
            if ($("#TitleId").val() != '<%=emp.TitleId %>') {
                $("#trTitleEffectDate").css("display", "");
                $("#titleEffectDate").rules("add", "required");
            } else {
                $("#trTitleEffectDate").css("display", "none");
                $("#titleEffectDate").val("");
                $("#titleEffectDate").rules("remove");
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
            $("#ContractedDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#LaborUnionDate").datepicker({
                yearRange: "-50:50",
                onClose: function () { $(this).valid(); }
            });

            $("#departEffectDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#titleEffectDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#TaxIssueDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#hospitalEffectDate").datepicker({
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
                    checkDate: true,
                    checkAge: ["#editForm input[name='DOB']"]
                },
                TaxIssueDate: { checkDate: true },
                ContractedDate: {
                    checkDate: true,
                    compareExcDate: ["#editForm input[name='StartDate']", "get", "Contracted Date", "Start Date"]
                },
                DepartmentId: {
                    required: true
                },
                TitleId: {
                    required: true
                },
                    JR: {
                        remote: {
                            url: "/Employee/CheckJRExits",
                            type: "post",
                            data: {
                                jr: function () {
                                    return $("#JR").val();
                                },
                                empID: function () {
                                    return $("#ID").val();
                                }
                            }
                        }
                    },
                departEffectDate: {
                    checkDate: true,
                    remote: {
                        url: "/Employee/CheckDepartEffectiveDate",
                        type: "post",
                        data: {
                            empId: function () {
                                return $("#ID").val();
                            },
                            effectDate: function () {
                                return $("#departEffectDate").val();
                            },
                            actionName: 'Department'
                        }
                    }

                },
                titleEffectDate: {
                    checkDate: true,
                    remote: {
                        url: "/Employee/CheckDepartEffectiveDate",
                        type: "post",
                        data: {
                            empId: function () {
                                return $("#ID").val();
                            },
                            effectDate: function () {
                                return $("#titleEffectDate").val();
                            },
                            actionName: 'JobTitle'
                        }
                    }

                },
                hospitalEffectDate: {
                    checkDate: true,
                    remote: {
                        url: "/Employee/CheckInsuranceHospitalEffectiveDate",
                        type: "post",
                        data: {
                            empId: function () {
                                return $("#ID").val();
                            },
                            effectDate: function () {
                                return $("#hospitalEffectDate").val();
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
<%using (Html.BeginForm("EditCompanyInfo", "Employee", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tbody>
        <tr>
            <td valign="top" class="ccbox">
                <%=Html.Hidden("ID", emp.ID)%>
                <%=Html.Hidden("DOB",emp.DOB.HasValue? emp.DOB.Value.ToString(Constants.DATETIME_FORMAT):"")%>
                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                    <tbody>
                        <tr>
                            <td class="label" style="width: 150px">
                                Employee ID
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
                            <td class="label" style="width: 150px">
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
                                ContractedDate
                            </td>
                            <td class="input">
                                <%=Html.TextBox("ContractedDate",emp.ContractedDate.HasValue?emp.ContractedDate.Value.ToString(Constants.DATETIME_FORMAT):string.Empty, new { @Style = "width: 150px", @maxlength = "10" })%>
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
                            <td class="label">
                                Department
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("DepartmentName", null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:156px" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label required">
                                Sub-Department <span>*</span>
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("DepartmentId", null, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:156px" })%>
                            </td>
                        </tr>
                        <tr id="trdepartEffectDate" style="display: none">
                            <td class="label required">
                                Effective Date <span>*</span>
                            </td>
                            <td class="input">
                                <%= Html.TextBox("departEffectDate", "", new { @style = "width:150px", @maxlength = "10" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label required">
                                Job Title <span>*</span>
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("TitleId", null, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:156px" })%>
                            </td>
                        </tr>
                        <tr id="trTitleEffectDate" style="display: none">
                            <td class="label required">
                                Effective Date <span>*</span>
                            </td>
                            <td class="input">
                                <%= Html.TextBox("titleEffectDate", "", new { @style = "width:150px", @maxlength = "10" })%>
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
                                <%= Html.TextBox("Manager", CommonFunc.GetEmployeeFullName(emp.Employee1,
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
                            <td class="label">
                                Tax ID
                            </td>
                            <td class="input">
                                <%=Html.TextBox("TaxID", emp.TaxID, new { @style = "width:150px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Issue Date
                            </td>
                            <td class="input">
                                <%     if (emp.TaxIssueDate.HasValue)
                                       {
                                           Response.Write(Html.TextBox("TaxIssueDate", emp.TaxIssueDate.Value.ToString(Constants.DATETIME_FORMAT), new { @style = "width:150px" }));
                                       }
                                       else
                                       {
                                           Response.Write(Html.TextBox("TaxIssueDate", "", new { @style = "width:150px" }));
                                       }
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Social Insurance Book No
                            </td>
                            <td class="input">
                                <%=Html.TextBox("SocialInsuranceNo", emp.SocialInsuranceNo, new { @style = "width:150px", @maxlength = "20" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Health Insurance Hospital
                            </td>
                            <td class="input">
                                <%
                                    if (!string.IsNullOrEmpty(emp.InsuranceHospitalID))
                                    {
                                        Response.Write(Html.DropDownList("InsuranceHospitalID", null, Constants.FIRST_ITEM, new { @style = "width:158px" }));
                                    }
                                    else
                                    {
                                        Response.Write(Html.DropDownList("InsuranceHospitalID", null, Constants.FIRST_ITEM, new { @style = "width:158px" }));
                                    }
                                %>
                            </td>
                        </tr>
                        <tr id="trHospitalEffectDate" style="display: none">
                            <td class="label required">
                                Effective Date <span>*</span>
                            </td>
                            <td class="input">
                                <%= Html.TextBox("hospitalEffectDate", "", new { @style = "width:150px", @maxlength = "10" })%>
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
            </td>
        </tr>
    </tbody>
</table>
<% } %>