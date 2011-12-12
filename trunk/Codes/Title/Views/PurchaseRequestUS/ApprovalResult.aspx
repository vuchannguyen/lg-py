<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("ApprovalResult", "PurchaseRequestUS", FormMethod.Post, new { id = "approvalForm" }))
  {
      if (ViewData.Model != null)
      {
          PurchaseRequest request = (PurchaseRequest)ViewData.Model;
          Response.Write(Html.Hidden("UpdateDate", request.UpdateDate));
      }
      if (Request.UrlReferrer != null)
      {
          Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
      }
%>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required">
            Comment <span>*</span>
        </td>
        <td>
            <% Response.Write(Html.Hidden("ResolutionId", (string)ViewData[CommonDataKey.RESOLUTION_ID]));
               Response.Write(Html.Hidden("RequestId", (string)ViewData[CommonDataKey.PURCHASE_ID]));
               Response.Write(Html.TextArea("Contents", "", 2, 133, new { @style = "width:280px"}));
               Response.Write(Html.Hidden("CaseResolution", (string)ViewData[CommonDataKey.GROUP_APPROVAL_RESOLUTION]));%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Forward To
        </td>
        <td>
            <%=Html.DropDownList("Assign", null, Constants.PURCHASE_REQUEST_APPROVAL_MAN, new { @style = "width:284px" })%>
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
    var indexSubmit = 0;
    $(document).ready(function () {
        $("#approvalForm").validate({
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
                Contents: { required: true, maxlength: 500 },
                Assign: { required:true }
            },
            submitHandler: function (form) {
                    if (indexSubmit == 0) {
                        var assignID = $("#Assign").val();
                        var array = assignID.split('@');
                        jQuery.ajax({
                            url: "/PurchaseRequestUS/CheckOfficeStatus",
                            type: "POST",
                            datatype: "json",
                            data: ({
                                'userID': array[0]
                            }),
                            success: function (mess) {
                                $("span[htmlfor='Assign']").remove();
                                var objectError = $('<span htmlfor="Assign" class="error" generated="true" style="display: inline-block;">' + mess.Holders + '</span>');
                                if (mess.MsgType == <%=(int)MessageType.Error%>) {
                                    objectError.tooltip({
                                        bodyHandler: function () {
                                            return objectError.html();
                                        }
                                    });

                                    objectError.insertAfter($("#Assign"));
                                }
                                else {
                                    form.submit();
                                    indexSubmit++;
                                }
                            }
                        });
                    }
                }
        });        
    });
</script>