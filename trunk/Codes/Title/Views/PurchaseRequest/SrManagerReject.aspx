<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("SrManagerReject", "PurchaseRequest", FormMethod.Post, new { id = "approvalForm" }))
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
            <% Response.Write(Html.Hidden("RequestId", (string)ViewData[CommonDataKey.PURCHASE_ID]));%>
            <%= Html.TextArea("Contents", "", 2, 133, new { @style = "width:280px"})%>
        </td>
    </tr>
    <tr>
        <td class="label required" style="width: 120px;">
            Resolution
        </td>
        <td class="input">
            <%=Html.DropDownList("WFResolutionID") %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Forward To
        </td>
        <td>
            <%=Html.DropDownList("Assign", null, new { @style = "width:204px" })%>
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
                Contents: { required: true, maxlength: 500 }
            },
            submitHandler: function (form) {
                    if (indexSubmit == 0) {
                        var assignID = $("#Assign").val();
                        var array = assignID.split('@');
                        jQuery.ajax({
                            url: "/PurchaseRequest/CheckOfficeStatus",
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

        $("#WFResolutionID").change(function () {
            $("#WFStatusID").html("");
            $("#Assign").html("");
            var resolutionId = $("#WFResolutionID").val();
            if (resolutionId != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=PurChaseAssign', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            if (this['ID'] == "<%=ViewData[CommonDataKey.PR_SELECTED_PURCHASING]%>" ||
                                    this['ID'] == "<%=ViewData[CommonDataKey.PR_SELECTED_REQUESTOR]%>")
                                $("#Assign").append($("<option value='" + this['ID'] + "' selected>" + this['Name'] + "</option>"));
                            else
                                $("#Assign").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
        });
    });
   </script>