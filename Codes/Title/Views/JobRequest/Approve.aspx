<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#approveForm").validate({
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
                Approval: {
                    required: true,
                    remote: '<%= Url.Action("IsApprovalExist", "JobRequest") %>'
                }
            }
        });
    });
</script>
<%using (Html.BeginForm("Approve", "JobRequest", FormMethod.Post, new { id = "approveForm" }))
      if (Request.UrlReferrer != null)
      {
          Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
      }
  {%>

<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required">
            Approval :
        </td>
        <td>
            <% Response.Write(Html.TextBox("Approval", "", new { @maxlength = "10", @style = "width:150px" }));%>
            <% Response.Write(Html.Hidden("RequestId", ViewData["RequestId"].ToString()));%>
            <% Response.Write(Html.Hidden("ResolutionId", ViewData["ResolutionId"].ToString()));%>
            <% Response.Write(Html.Hidden("UpdateDate", ViewData["UpdateDate"].ToString()));%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Forward to :
        </td>
        <td>
            <%=Html.DropDownList("Assign", ViewData["Assign"] as SelectList, new { @style = "width:156px" })%>
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
