<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    $(document).ready(function () {
        $(function () {
            $("#DOB").datepicker({
                yearRange: "-100:100",
                onClose: function () { $(this).valid(); }
            });
            $("#IssueDate").datepicker({
                yearRange: "-50:50"
            });
        });
        $.validator.addMethod('idNumber', function (value, element) {
            if (value.length == 0)
                return true;
            return /^[a-z0-9]+$/i.test(value);
        }, "Space and special characters are not allowed!");
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
                FirstName: {
                    required: true
                },
                LastName: {
                    required: true
                },
                IDNumber: {
                    idNumber: true
                },
                DOB: {
                    checkDate: true,
                    checkBirthDate: true
                },
                IssueDate: {
                    checkDate: true,
                    checkAgeIssue: ["#editForm input[name='DOB']"]
                },
                VnLastName: { required: true },
                VnFirstName: { required: true }
            }
        });

    });
</script>
<%using (Html.BeginForm("EditPersonalInfo", "Employee", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% Employee emp = (Employee)ViewData.Model;%>
<%=Html.Hidden("ID", emp.ID)%>
<%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tbody>
        <tr>
            <td valign="top" class="ccbox">
                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                    <tbody>
                        <tr>
                            <td valign="top" width="90%">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                    <tr>
                                        <td class="label required">
                                            First Name <span>*</span>
                                        </td>
                                        <td class="input">
                                            <% =Html.TextBox("FirstName", emp.FirstName, new { @style = "width:130px", @maxlength = "30" })%>
                                        </td>                                        
                                        <td class="label">
                                            Middle Name
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("MiddleName", emp.MiddleName, new { @style = "width:130px", @maxlength = "20" })%>
                                        </td>
                                        <td class="label required" style="width: 110px">
                                            Last Name <span>*</span>
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("LastName", emp.LastName, new { @style = "width:130px", @maxlength = "30" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label required">
                                            VN First Name <span>*</span>
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnFirstName", emp.VnFirstName, new { @style = "width:130px", @maxlength = "20" })%>
                                        </td>
                                        <td class="label">
                                            VN Middle Name
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnMiddleName", emp.VnMiddleName, new { @style = "width:130px", @maxlength = "20" })%>
                                        </td>
                                        <td class="label required">
                                            VN Last Name <span>*</span>
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnLastName", emp.VnLastName, new { @style = "width:130px", @maxlength = "30" }) %>
                                        </td>                                                                                
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Date of Birth
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("DOB",emp.DOB.HasValue?emp.DOB.Value.ToString(Constants.DATETIME_FORMAT):string.Empty, new { @style = "width:130px" })%>
                                        </td>
                                        <td class="label">
                                            Place of Birth
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("POB", emp.POB, new { @style = "width:130px", @maxlength = "50" })%>
                                        </td>
                                        <td class="label">
                                            VN Place of Birth
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnPOB", emp.VnPOB, new { @style = "width:130px", @maxlength = "50" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Place of Origin
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("PlaceOfOrigin", emp.PlaceOfOrigin, new { @style = "width:130px", @maxlength = "100" })%>
                                        </td>
                                        <td class="label">
                                            VN Place of Origin
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnPlaceOfOrigin", emp.VnPlaceOfOrigin, new { @style = "width:130px", @maxlength = "100" })%>
                                        </td>
                                        <td class="label">
                                            Nationality
                                        </td>
                                        <td class="input">
                                            <%=Html.DropDownList("Nationality", null, Constants.FIRST_ITEM, new { @style = "width:136px" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Gender
                                        </td>
                                        <td class="input">
                                            <%=Html.DropDownList("Gender", null, Constants.FIRST_ITEM, new { @style = "width:136px" })%>
                                        </td>
                                        <td class="label">
                                            Degree
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("Degree", emp.Degree, new { @style = "width:130px", @maxlength = "200" })%>
                                        </td>
                                        <td class="label">
                                            Other Degree
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("OtherDegree", emp.OtherDegree, new { @style = "width:130px", @maxlength = "200" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            ID Number
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("IDNumber", emp.IDNumber, new { @style = "width:130px", @maxlength = "20" })%>
                                        </td>
                                        <td class="label">
                                            Issue Date
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("IssueDate",emp.IssueDate.HasValue?emp.IssueDate.Value.ToString(Constants.DATETIME_FORMAT):string.Empty, new { @style = "width:130px" })%>
                                        </td>
                                        <td class="label">
                                            Issue Location
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("IDIssueLocation", emp.IDIssueLocation, new { @style = "width:130px", @maxlength = "200" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Race
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("Race", emp.Race, new { @style = "width:130px", @maxlength = "50" }) %>
                                        </td>
                                        <td class="label">
                                            Religion
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("Religion", emp.Religion, new { @style = "width:130px", @maxlength = "50" })%>
                                        </td>
                                        <td class="label">
                                            VN Issue Location
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("VnIDIssueLocation", emp.VnIDIssueLocation, new { @style = "width:130px", @maxlength = "200" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Current Status
                                        </td>
                                        <td class="input">
                                            <%=Html.DropDownList("EmpStatusId", null, Constants.FIRST_ITEM, new { @style = "width:136px" })%>
                                        </td>
                                        <td class="label">
                                            Married Status
                                        </td>
                                        <td class="input">
                                            <%=Html.DropDownList("MarriedStatus", null, Constants.FIRST_ITEM, new { @style = "width:136px" })%>
                                        </td>
                                        <td class="label">
                                             Major
                                        </td>
                                        <td class="input">
                                            <%=Html.TextBox("Major", emp.Major, new { @style = "width:130px", @maxlength = "255" })%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="cbutton">
                                            <input type="submit" class="save" value="" alt="" />
                                            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
<% } %>
