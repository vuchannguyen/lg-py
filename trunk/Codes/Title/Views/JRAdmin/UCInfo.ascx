<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        //$("#UserName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=SendMail', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CC" });
        $("#WFID").change(function () {
            $("#WFRoleID").html("");
            var wfId = $("#WFID").val();
            if (wfId != 0) {
                $("#WFRoleID").append($("<option value=''>-Select Group Name-</option>"));
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + wfId + '&Page=JRAdmin', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#WFRoleID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
        });
        <% 
        if(ViewData.Model == null)
        {
        %>
            $("#UserName").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=JRAdmin',
                { max: 50, highlightItem: true, multiple: true, faceBook: true, 
                multipleSeparator: "<%=Constants.SEPARATE_USER_ADMIN_USERNAME%>", hidField: "#hidUserName", employee: true
            });
        <%
        }
        %>
    });
</script>
<div class="form">
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" style="vertical-align:top">
            User Name <span>*</span>
        </td>
        <td class="input">
            <% 
               string textBoxWidth = "200px";
               string dropdownWidth = "207px";
               if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("UserName", "", new { @maxlength = "500", @style = "width:" + textBoxWidth + ";"}));
                   Response.Write(Html.Hidden("hidUserName"));
               }
               else
               {
                   Response.Write(Html.TextBox("UserName", ((UserAdmin_WFRole)ViewData.Model).UserAdmin.UserName.ToString(),
                       new { @maxlength = "500", @style = "width:" + textBoxWidth + ";", @readonly = "readonly" }));
                   Response.Write(Html.Hidden("hidUserName"));
                   Response.Write(Html.Hidden("ID", ((UserAdmin_WFRole)ViewData.Model).ID));
                   Response.Write(Html.Hidden("UpdateDate", ((UserAdmin_WFRole)ViewData.Model).UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Workflow <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("WFID", null, Constants.FIRST_ITEM_WORKFLOW, new { @style = "width:" + dropdownWidth }));
               }
               else
               {
                   Response.Write(Html.DropDownList("WFID", null, new { @style = "width:" + dropdownWidth }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Group <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("WFRoleID", null, Constants.FIRST_ITEM_GROUP_NAME, new { @style = "width:" + dropdownWidth }));
               }
               else
               {
                   Response.Write(Html.DropDownList("WFRoleID", null, new { @style = "width:" + dropdownWidth }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Active
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsActive", true));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsActive", ((CRM.Models.UserAdmin_WFRole)ViewData.Model).IsActive));
               }
            %>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="cbutton">
            <input type="submit" class="save" value="" id="btnSave" alt="Update" />
            <input type="button" class="cancel" id="btnCancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
</div>