<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<script type="text/javascript">
    jQuery(document).ready(function () {
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
            },
            rules: {
                Assign: {
                    required: true
                }
            }
        });

    });
</script>
<%using (Html.BeginForm("ForwardTo", "JobRequest", FormMethod.Post, new { id = "editForm" }))
  {%>
  <%
      JobRequest jr = (JobRequest)ViewData.Model;
      
      Response.Write(Html.Hidden("ID", jr.ID));
      Response.Write(Html.Hidden("UpdateDate", jr.UpdateDate.ToString()));
      if (Request.UrlReferrer != null)
      {
          Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
      }
  %>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required" style="width: 130px">
            Forward To <span>*</span>
        </td>
        <td>
            <% Response.Write(Html.DropDownList("Assign", null, " -Choose-", new { @style = "width:136px" })); %>
        </td>        
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<% } %>
