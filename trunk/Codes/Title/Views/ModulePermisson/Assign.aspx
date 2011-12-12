<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%using (Html.BeginForm("Assign", "ModulePermisson", FormMethod.Post, new { id = "assignForm",@class="form" }))
      {
      %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <%  List<ModulePermission> permissonList = (List<ModulePermission>)ViewData.Model;
        foreach (int id in Enum.GetValues(typeof(Permissions)))
        {
    %>
    <tr>
        <td style="width:15px">
            <%=Html.CheckBox("chkActive" + id, (permissonList.Where(q => q.PermissionId == id).FirstOrDefault()) != null ? true : false)%>
        </td>
        <td>
            <%  Response.Write(Html.Hidden("hidID",id));
                Response.Write(((Permissions)id).ToString()); %>
        </td>
    </tr>
    <% }
    %>
    <tr>
        <td colspan="2" align="center">
        <button class="save" type="submit"  />
        <button type="button" class="close" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>

<%} %>
