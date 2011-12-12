<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    jQuery(document).ready(function () {
        $(function () {
            $("#txtResignedDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
        });               

        $("#resignForm").validate({
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
                txtResignedDate: {
                    required: true,
                    checkDate: true,
                    compareDate: ["#resignForm input[name='StartDate']", "get", "Resigned Date", "Start Date"]
                },
                txtResignedAllowance: {
                    number: true,
                    min: 10
                },
                txtResignedReason: {
                    maxlength: 500
                }
            }
        });
    });
</script>
<%using (Html.BeginForm("Resign", "Employee", FormMethod.Post, new { id = "resignForm" }))
  {%>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required" style="width:130px">
            Resigned Date <span>*</span>
        </td>
        <td align="left">
            <input type="text" style="width: 150px" maxlength="10" id="txtResignedDate" name="txtResignedDate" />
            <%= Html.Hidden("StartDate", ((CRM.Models.Employee)ViewData.Model).StartDate)%>
            <%= Html.Hidden("ID", ((CRM.Models.Employee)ViewData.Model).ID)%>
            <%= Html.Hidden("UpdateDate", ((CRM.Models.Employee)ViewData.Model).UpdateDate.ToString())%>
        </td>
    </tr>
    <tr>
        <td class="label">
            Resigned Allowance
        </td>
        <td align="left">
            
            <input type="text" style="width: 150px" maxlength="10" id="txtResignedAllowance"
                name="txtResignedAllowance" />
        </td>
    </tr>
    <tr>
        <td class="label">
            Resigned Reason :
        </td>
        <td align="left">
            <textarea cols="27" maxlength="500" rows="2" id="txtResignedReason" name="txtResignedReason"> </textarea>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" id="btnSave" />
            <input type="button" class="cancel" value="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<% } %>
