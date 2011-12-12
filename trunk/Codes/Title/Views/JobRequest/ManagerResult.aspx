<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("ManagerResult", "JobRequest", FormMethod.Post, new { id = "rejectForm" }))
  {%>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required">
            Comment
        </td>
        <td>
            <% Response.Write(Html.Hidden("ResolutionId", ViewData["ResolutionId"].ToString()));
               Response.Write(Html.Hidden("RequestId", ViewData["RequestId"].ToString()));
               Response.Write(Html.Hidden("UpdateDate", ViewData["UpdateDate"].ToString()));
               if (Request.UrlReferrer != null)
               {
                Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
                }
            %>
            <%= Html.TextArea("Contents", "", 2, 133, new { @style = "width:280px"})%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Forward to  <span>*</span>
        </td>
        <td>
            <%=Html.DropDownList("Assign", null, Constants.FIRST_ITEM, new { @style = "width:204px" })%>
        </td>
    </tr>
    <tr>
        <td colspan ="2" align="center">
            <input type="submit" class="save" id="btnPost" value="" />
            <input type="button" class="cancel" value="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<% } %>
<script type="text/javascript">
    $(document).ready(function () {      
        $("#rejectForm").validate({
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
                Contents: {
                    maxlength: 500
                },
                Assign: {
                    required: true
                }
            }
        });
    });
   </script>

