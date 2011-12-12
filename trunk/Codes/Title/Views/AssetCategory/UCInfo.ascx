<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required">
            Name <span>*</span>
        </td>
        <td class="input">
            <% 
                AssetCategory assCat = null;
                if (ViewData.Model != null)
                {
                    assCat = (AssetCategory)ViewData.Model;
                }
                
                if (assCat == null)
                {
                    Response.Write(Html.TextBox("Name", "", new { @maxlength = "200", @style = "width:300px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("Name", assCat.Name, new { @maxlength = "200", @style = "width:300px" }));
                    Response.Write(Html.Hidden("ID", assCat.ID));
                    Response.Write(Html.Hidden("UpdateDate", assCat.UpdateDate.ToString()));
                }
            %>
        </td>
    </tr>    
    <tr>
        <td class="label">
            Description
        </td>
        <td class="input">
            <% if (assCat == null)
               {
                   Response.Write(Html.TextArea("Description", "", new { @maxlength = "4000", @style = "width:302px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Description", assCat.Description, new { @maxlength = "4000", @style = "width:302px" }));
               }
            %>
        </td>
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
                   Response.Write(Html.CheckBox("IsActive", assCat.IsActive));                   
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
