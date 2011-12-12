<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("EditRemark", "Employee", FormMethod.Post, new { @id = "editForm", @class = "form" }))
  { %>
<% Employee emp = (Employee)ViewData.Model;%>
<table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
    <tbody>
        <tr>
            <td valign="top">
                <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
                <%=Html.TextArea("Remarks", emp.Remarks, new { @style = "width: 350px; height: 127px;", @maxlength = "1000" })%>
            </td>
        </tr>
        <tr>
            <td class="cbutton">
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
    </tbody>
</table>
<% } %>
<script type="text/javascript">
    $(document).ready(function () {
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
            }
        });
    });
    </script>