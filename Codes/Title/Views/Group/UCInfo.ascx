<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required">
            Group Name <span>*</span>
        </td>
        <td class="input"><% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("GroupName", "", new { @maxlength = "50", @style = "width:150px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("GroupName", ((CRM.Models.Group)ViewData.Model).GroupName, new { @maxlength = "50", @style = "width:150px" }));
                   Response.Write(Html.Hidden("GroupId", ((CRM.Models.Group)ViewData.Model).GroupId));
                   Response.Write(Html.Hidden("UpdateDate", ((CRM.Models.Group)ViewData.Model).UpdateDate.ToString()));
               }
            %>
            </td>
    </tr>
    <tr>
        <td class="label">
            Display Order
        </td>
        <td class="input"><% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("DisplayOrder","", new {  @maxlength = "4", @style = "width:150px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("DisplayOrder", ((CRM.Models.Group)ViewData.Model).DisplayOrder.HasValue ? ((CRM.Models.Group)ViewData.Model).DisplayOrder.Value.ToString() : "", new {  @maxlength = "4", @style = "width:150px" }));                   
               }
            %></td>
    </tr>
    <tr>
        <td class="label">
            Description
        </td>
        <td class="input"><% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Description", "", new { @style = "width:152px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Description", ((CRM.Models.Group)ViewData.Model).Description, new { @style = "width:152px" }));                   
               }
            %></td>
    </tr>
    <tr>
        <td class="label">
            Active
        </td>
        <td valign="bottom" class="input"><% if (ViewData.Model == null)
               {
                   Response.Write(Html.CheckBox("IsActive",true));
               }
               else
               {
                   Response.Write(Html.CheckBox("IsActive", ((CRM.Models.Group)ViewData.Model).IsActive));                   
               }
            %></td>
    </tr>
    <tr>
        <td></td>
        <td>
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
