<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required" style = "vertical-align:top">
            User Name <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("UserName", "", new { @maxlength = "500", @style = "width:200px" }));
                   Response.Write(Html.Hidden("hidUserName"));
               }
               else
               {
                   Response.Write(Html.TextBox("UserName", ((User_Group)ViewData.Model).UserAdmin.UserName,
                       new { @maxlength = "500", @readonly = true, @style = "width:200px" }));
                   Response.Write(Html.Hidden("hidUserName"));
                   Response.Write(Html.Hidden("ID", ((User_Group)ViewData.Model).ID));
                   Response.Write(Html.Hidden("UpdateDate", ((User_Group)ViewData.Model).UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Group
        </td>
        <td valign="bottom" class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("GroupId", null, new { @style = "width:207px" }));
               }
               else
               {
                   Response.Write(Html.DropDownList("GroupId", null, new { @style = "width:207px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Active
        </td>
        <td valign="bottom" class="input">
            <% 
                string check = "checked=\"checked\"";
               if (ViewData.Model != null)               
               {
                   check = ((User_Group)ViewData.Model).IsActive ? "checked=\"checked\"" : "";
               }
               Response.Write("<input type=\"checkbox\" value=\"true\" name=\"IsActive\" id=\"IsActive\" " + check + " />");
            %>
                   
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <input type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
