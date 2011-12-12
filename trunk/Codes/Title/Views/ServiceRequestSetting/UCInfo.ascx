<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% SR_Setting obj = (SR_Setting)ViewData.Model;
   if (ViewData.Model != null)
   {
       Response.Write(Html.Hidden("UpdateDate", (string)ViewData["UpdateDate"]));
   } %>
   <style type="text/css">
 
    .ac_results
    {
        width:160px !important;
    }
</style>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required">
            Office <span>*</span>
        </td>
        <td class="input" style="width: 160px;">
            <% Response.Write(Html.DropDownList("OfficeID", ViewData[Constants.SR_LIST_SETTING_OFFICE] as SelectList, Constants.FIRST_ITEM_OFFICE, new { @style = "width:120px" })); %>
        </td>
        <td class="label">
            Project
        </td>
        <td class="input" style="width: 160px;">
            <%=Html.DropDownList("ProjectName", ViewData[Constants.SR_LIST_SETTING_PROJECT] as SelectList, Constants.FIRST_ITEM_PROJECT, new { @style = "width:120px" })%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            User Admin <span>*</span>
        </td>
        <td valign="bottom" class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("UserAdminText", string.Empty, new { @style = "width:110px" }));
                   Response.Write(Html.Hidden("UserAdminID", string.Empty));
               }
               else
               {
                   Response.Write(Html.TextBox("UserAdminText", obj.UserAdmin.UserName));
                   Response.Write(Html.Hidden("UserAdminID", obj.UserAdminID));
               }
            %>
        </td>
        <td class="label">
            Active
        </td>
        <td valign="bottom" class="input">
            <% 
                string check = "checked=\"checked\"";
                if (ViewData.Model != null)
                {
                    check = obj.IsActive ? "checked=\"checked\"" : "";
                }
                Response.Write("<input type=\"checkbox\" value=\"true\" name=\"IsActive\" id=\"IsActive\" " + check + " />");
            %>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <input type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<script type="text/javascript">
    var formSubmit = true;
    $(document).ready(function () {
        $("#UserAdminText").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=ServiceRequest', { subField: "#UserAdminID" });

        $("#settingForm").validate({
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
                OfficeID: { required: true },
                UserAdminText: { required: true }

            },
            submitHandler: function (form) {
                if (formSubmit == true) {
                    $(form).ajaxSubmit({
                        success: function (result) {
                            if (result.MsgType == 1) {
                                CRM.summary(result.MsgText, 'block', 'msgError');
                            }
                            else {
                                window.location = "/ServiceRequestSetting";
                            }
                        },
                        url: form.action,
                        dataType: 'json'
                    });
                }
            }
        });
    });
</script>
