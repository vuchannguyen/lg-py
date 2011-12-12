<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<script type="text/javascript">
    /////////////////////////////////
    jQuery(document).ready(function () {

        jQuery.validator.addMethod(
            "validatePhone", function (value) {
                return CRM.isPhone(value);
            }, 'Only input letters are : 0123456789.,()-+ ');
        $("#editFormCandidate").validate({
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
                VnFirstName: { required: true },
                VnLastName: { required: true },
                LastName: { required: true },
                FirstName: { required: true },
                TitleId: { required: true },
                DOB: { required: false, checkDate: true, checkBirthDate: true },
                Email: { required: true, email: true },
                SearchDate: { required: true, checkDate: true },
                Source: { required: true },
                titleId: { required: true }
            }
        });
        $("#Telephone").keypress(function (event) {
            return CRM.onKeyPress(event, this, '[0-9]', '20');
        });
        $("#Telephone").blur(function () {
            return CRM.onKeyUp(this, 'Number');
        });
        $("#Telephone").mousemove(function () {
            return CRM.onKeyUp(this, 'Number');
        });

        $("#DOB").datepicker({
            yearRange: "-100:100",
            onClose: function () { $(this).valid(); }
        });
        $("#SearchDate").datepicker({
            onClose: function () { $(this).valid(); }
        });
        $("#btnCancel").click(function () {
            CRM.closePopup();
        });
    });
</script>
<%if (ViewData.Model == null)
  {
      Response.Write(Html.Hidden("empFullName", ""));
  }
  else
  {
      Response.Write(Html.Hidden("empFullName", (((CRM.Models.Candidate)ViewData.Model).FirstName
          + " " + ((CRM.Models.Candidate)ViewData.Model).MiddleName + " "
          + ((CRM.Models.Candidate)ViewData.Model).LastName).Replace(" ", "_")));


  } %>
<%using (Html.BeginForm("EditPop", "Candidate", FormMethod.Post, new { @id = "editFormCandidate", @class = "form" }))
  { %>
<% Response.Write(Html.Hidden("ID", ((CRM.Models.Candidate)ViewData.Model).ID)); %>
<table cellspacing="0" cellpadding="0" border="0" width="900" class="edit">
    <tbody>
        <tr>
            <td class="label required">
                 First Name <span>*</span>                
            </td>
            <td class="input" style="width: 170px">
                
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("FirstName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "1" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("FirstName", ((CRM.Models.Candidate)ViewData.Model).FirstName, new { @maxlength = "50", @style = "width:120px", @tabindex = "1" }));

                   }
                %>
            </td>
            <td class="label" style="width: 110px">
                Middle Name
            </td>
            <td class="input" style="width: 180px">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("MiddleName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "2" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("MiddleName", ((CRM.Models.Candidate)ViewData.Model).MiddleName, new { @maxlength = "50", @style = "width:120px", @tabindex = "2" }));
                   }
                %>
            </td>
            <td class="label required" style="width: 140px">
               Last Name <span>*</span>
            </td>
            <td class="input" style="width: 180px">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("LastName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "3" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("LastName", ((CRM.Models.Candidate)ViewData.Model).LastName, new { @maxlength = "50", @style = "width:120px", @tabindex = "3" }));
                   }
                %>
            </td>            
        </tr>
        <tr>
            <td class="label required">
                VN First Name <span>*</span>               
            </td>
            <td class="input">
                
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnFirstName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "4" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("VnFirstName", ((CRM.Models.Candidate)ViewData.Model).VnFirstName, new { @maxlength = "50", @style = "width:120px", @tabindex = "4" }));
                   }
                %>
            </td>
            <td class="label">
                VN Middle Name
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnMiddleName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "5" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("VnMiddleName", ((CRM.Models.Candidate)ViewData.Model).VnMiddleName, new { @maxlength = "50", @style = "width:120px", @tabindex = "5" }));
                   }
                %>
            </td>
            <td class="label required">
                 VN Last Name <span>*</span>
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("VnLastName", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "6" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("VnLastName", ((CRM.Models.Candidate)ViewData.Model).VnLastName, new { @maxlength = "50", @style = "width:120px", @tabindex = "6" }));
                   }
                %>
            </td>            
        </tr>
        <tr>
            <td class="label required">
                Date of Birth 
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("DOB", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "7" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("DOB", ((CRM.Models.Candidate)ViewData.Model).DOB.HasValue ? ((CRM.Models.Candidate)ViewData.Model).DOB.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @maxlength = "50", @style = "width:120px", @tabindex = "7" }));
                   }
                %>
            </td>
            <td class="label">
                Telephone
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("CellPhone", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "8" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("CellPhone", ((CRM.Models.Candidate)ViewData.Model).CellPhone, new { @maxlength = "50", @style = "width:120px", @tabindex = "8" }));
                   }
                %>
            </td>
            <td class="label required">
                Email <span>*</span>
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("Email", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "9" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("Email", ((CRM.Models.Candidate)ViewData.Model).Email, new { @maxlength = "50", @style = "width:120px", @tabindex = "9" }));
                   }
                %>
            </td>
        </tr>
        <tr>
            <td class="label">
                Gender
            </td>
            <td class="input">
                <% Response.Write(Html.DropDownList("Gender", null, new { @style = "width:125px", @tabindex = "10" })); %>
            </td>
            <td class="label required">
                Searched date <span>*</span>
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("SearchDate", "", new { @maxlength = "50", @style = "width:120px", @tabindex = "11" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("SearchDate", ((CRM.Models.Candidate)ViewData.Model).SearchDate.ToString(Constants.DATETIME_FORMAT), new { @maxlength = "50", @style = "width:120px", @tabindex = "11" }));
                   }
                %>
            </td>
            <td class="label required">
                Source <span>*</span>
            </td>
            <td class="input">
                <% Response.Write(Html.DropDownList("SourceId", null, new { @style = "width:125px", @tabindex = "12" })); %>
            </td>
        </tr>
        <tr>
            <td class="label required">
                Position <span>*</span>
            </td>
            <td class="input">
                <%=Html.DropDownList("TitleId", null, Constants.FIRST_ITEM, new { @style = "width:125px", @tabindex = "13" })%>
            </td>
            <td class="label required">
                Status
            </td>
            <td class="input">
                <% Candidate candidateObj = (Candidate)ViewData.Model;
                   if ((candidateObj.Status != (int)CRM.Library.Common.CandidateStatus.Available) && (candidateObj.Status != (int)CRM.Library.Common.CandidateStatus.Unavailable))
                   {
                       Response.Write(((CRM.Library.Common.CandidateStatus)candidateObj.Status).ToString());
                       Response.Write(Html.Hidden("Status", candidateObj.Status));
                   }
                   else
                       Response.Write(Html.DropDownList("Status", null, new { @maxlength = "50", @style = "width:125px", @tabindex = "14" }));
                %>
            </td>
            <td class="label">
            </td>
            <td class="input">
            </td>
        </tr>
        <tr>
            <td class="label">
                Address
            </td>
            <td colspan="5" class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextBox("Address", "", new { @maxlength = "500", @class = "fn-address", @tabindex = "15" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("Address", ((CRM.Models.Candidate)ViewData.Model).Address, new { @maxlength = "500", @class = "fn-address", @tabindex = "15" }));
                   }
                %>
            </td>
        </tr>
        <tr>
            <td class="label">
                Note
            </td>
            <td colspan="5" class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.TextArea("Note", new { @class = "fn-note", @tabindex = "15" }));
                   }
                   else
                   {
                       Response.Write(Html.TextArea("Note", ((CRM.Models.Candidate)ViewData.Model).Note, new { @class = "fn-note", @tabindex = "15" }));
                   }
                %>
            </td>
        </tr>
        <tr>
            <td colspan="6" valign="middle" class="cbutton">
                <input type="submit" title="Save" class="save" value="" />
                <input type="button" title="Cancel" id="btnCancel" class="cancel" value="" />
            </td>
        </tr>
    </tbody>
</table>
<% } %>