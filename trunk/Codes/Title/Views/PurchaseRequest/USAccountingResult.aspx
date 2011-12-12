<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("USAccountingResult", "PurchaseRequest", FormMethod.Post, new { id = "usAccountingForm", @class = "form" }))
      {
          sp_GetPurchaseRequestResult obj = (sp_GetPurchaseRequestResult)ViewData.Model;
          Response.Write(Html.Hidden("UpdateDate", obj.UpdateDate));
          Response.Write(Html.Hidden("PurchaseID", obj.ID));
    %>
    <%=TempData["Message"]%>
    <%
        if (!string.IsNullOrEmpty(Request["action"]))
        {
            Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, 1));
        }
    %>
    <div id="list" style="width: 1024px">
        <h2 class="heading">
            PR Information</h2>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label" style="width: 10%">
                    Requestor:
                </td>
                <td class="input" style="width: 25%">
                    <%=Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).RequestorName)%>
                </td>
                <td class="label" style="width: 10%">
                    Department:
                </td>
                <td class="input" style="width: 20%">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).Department)%>
                </td>
                <td class="label" style="width: 15%">
                    Sub Department:
                </td>
                <td class="input" style="width: 20%">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).SubDepartmentName)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Request Date:
                </td>
                <td class="input">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW))%>
                </td>
                <td class="label">
                    Expected Date:
                </td>
                <td class="input">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).ExpectedDate.HasValue ?
    ((sp_GetPurchaseRequestResult)ViewData.Model).ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "")%>
                </td>
                <td class="label">
                    Priority
                </td>
                <td class="input">
                    <%
                        if (obj.Priority.HasValue)
                        {
                            string sPriority = Constants.PURCHASE_REQUEST_PRIORITY.
                                Where(p => int.Parse(p.Value) == obj.Priority.Value).FirstOrDefault().Text;
                            Response.Write("<b>" + sPriority + "</b>");
                        }
                    %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Forwarded to
                </td>
                <td class="input">
                    <span style="color: Red">
                        <%=Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).AssignName)%>
                    </span>
                </td>
                <td class="label">
                    Status
                </td>
                <td class="input">
                    <span style="color: Red">
                        <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).StatusName)%>
                    </span>
                </td>
                <td class="label">
                    Resolution
                </td>
                <td class="input">
                    <span style="color: Red">
                        <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).ResolutionName)%>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Justification
                </td>
                <td class="input" colspan="3" style="width: 563px;">
                    <div id="justicaton" style="width: 563px; overflow-y: auto; overflow-x: auto; word-wrap: break-word;">
                        <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).Justification != null ?
                             Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).Justification) : "&nbsp;");%>
                    </div>
                </td>
                <td class="label">
                    Billable to Client
                </td>
                <td class="input">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).BillableToClient ? "Yes":"No")%>
                </td>
            </tr>
            <tr>
                <td class="input" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="label">
                    Vendor/Supplier:
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorName != null ?
                             Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).VendorName) : "&nbsp;");%>
                </td>
                <td class="label">
                    Phone:
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorPhone != null ?
                             ((sp_GetPurchaseRequestResult)ViewData.Model).VendorPhone : "&nbsp;");%>
                </td>
                <td class="label">
                    Email:
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorEmail != null ?
                             ((sp_GetPurchaseRequestResult)ViewData.Model).VendorEmail : "&nbsp;");%>
                </td>
            </tr>
            <tr>
                <td class="label last">
                    Address:
                </td>
                <td class="input last" colspan="5">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorAddress != null ?
                             Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).VendorAddress) : "&nbsp;"); %>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="left" class="input bold" style="width: 60%; color: #0066CC; font-size: 1.4em">
                    Item(s)
                </td>
                <td style="width: 20%;" align="left">
                    Payment Method:
                    <% if (obj.PaymentID == Constants.TYPE_PAYMENT_CASH)
                           Response.Write(Constants.TYPE_PAYMENT_CASH_STRING);
                       else if (obj.PaymentID == Constants.TYPE_PAYMENT_TRANFER)
                           Response.Write(Constants.TYPE_PAYMENT_TRANFER_STRING);
                    %>
                </td>
                <td style="width: 20%;" align="right">
                    Money Type:
                    <% if (obj.MoneyType == Constants.TYPE_MONEY_USD)
                           Response.Write(Constants.TYPE_MONEY_USD_STRING);
                       else
                           Response.Write(Constants.TYPE_MONEY_VND_STRING);
                    %>
                </td>
            </tr>
        </table>
        <div class="form">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="view_item">
                <tr>
                    <th class="header left" style="width: 102px;">
                        No #
                    </th>
                    <th class="header" style="width: 461px;">
                        Description
                    </th>
                    <th class="header" style="width: 102px;">
                        Quantity
                    </th>
                    <th class="header" style="width: 154px;">
                        Price Per Unit
                    </th>
                    <th class="header last_right" style="width: 205px;">
                        Total Cost
                    </th>
                </tr>
                <% if (ViewData[CommonDataKey.LIST_PURCHASE_ITEM] != null)
                   {
                       List<PurchaseItem> items = (List<PurchaseItem>)ViewData[CommonDataKey.LIST_PURCHASE_ITEM];

                       int index = 1;
                       double result = 0;
                       foreach (PurchaseItem item in items)
                       {
                           string str = "<tr>";

                           result += item.TotalPrice;
                           str += "<td class=\"label\">" + index + "</td>"
                           + "<td class=\"label\" style='width:460px;'>" + Html.Encode(item.ItemName) + "</td>"
                           + "<td class=\"label\">" + item.Quantity + "</td>"
                           + "<td class=\"label\">" + CommonFunc.FormatCurrency(Math.Round(item.Price, 1)) + "</td>"
                           + "<td class=\"label last_right\">" + CommonFunc.FormatCurrency(Math.Round(item.TotalPrice, 1)) + "</td>" +
                           "</tr>";

                           Response.Write(str);
                           index++;
                       }
                   }                  
                %>
            </table>
        </div>
        <div style="width: 1024px">
            <% if (ViewData[CommonDataKey.LIST_APPROVAL] != null)
               {%>
            <div style="float: left;">
                <fieldset style="width: 500px; float: left; background-color: White">
                    <legend>Approval(s)</legend>
                    <table>
                        <%
                            List<sp_GetListApprovalAssignResult> listApproval = (List<sp_GetListApprovalAssignResult>)ViewData[CommonDataKey.LIST_APPROVAL];
                            foreach (sp_GetListApprovalAssignResult approval in listApproval)
                            {
                                SelectList roleValue = new SelectList(listApproval, "ApproverGroup", "Role", approval.ApproverGroup);
                                SelectList nameValue = new SelectList(listApproval, "UserAdminId", "UserName", approval.UserAdminId);
                        %>
                        <tr>
                            <td style="width: 120px">
                                Approved by:
                            </td>
                            <td>
                                <%= Html.DropDownList("Role", roleValue, new { @style = "width:150px;", @disabled = "disabled" })%>
                            </td>
                            <td style="padding-left: 10px">
                                <%= Html.DropDownList("Name", nameValue, new { @style = "width:150px;", @disabled = "disabled" })%>
                            </td>
                        </tr>
                        <%}%>
                    </table>
                </fieldset>
            </div>
            <%  }    %>
            <div class="total_items" style="float: right; width: 363px !important">
                <table border="0" cellpadding="0" cellspacing="0" width="363px" class="view_item">
                    <tr>
                        <td class="label" style="width: 154px;">
                            Sub Total
                        </td>
                        <td class="label last_right" style="width: 205px;">
                            <%= CommonFunc.FormatCurrency((double)ViewData[CommonDataKey.SUB_TOTAL_ITEM])%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Other
                        </td>
                        <td class="label last_right">
                            <%=CommonFunc.FormatCurrency(Math.Round(obj.OtherCost.Value, 1))%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            VAT(VN)
                        </td>
                        <td class="label last_right">
                            <%=CommonFunc.FormatCurrency(Math.Round(obj.SaleTaxValue.Value, 1))%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Shipping
                        </td>
                        <td class="label last_right">
                            <%=CommonFunc.FormatCurrency(Math.Round(obj.Shipping.Value, 1))%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <span class="bold">Total </span>
                        </td>
                        <td class="label last_right">
                            <span class="bold red">
                                <%=CommonFunc.FormatCurrency(Math.Round(((double)ViewData[CommonDataKey.SUB_TOTAL_ITEM] + obj.OtherCost + obj.SaleTaxValue + obj.Shipping).Value, 1)) + " " + (obj.MoneyType == Constants.TYPE_MONEY_USD ? Constants.TYPE_MONEY_USD_STRING : Constants.TYPE_MONEY_VND_STRING)%></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="5" align="right" id="tblapproval" style="padding: 5px 0px;">
                    <table cellpadding="0" cellspacing="0" style="width: 363px">
                        <tr>
                            <td class="label">
                                Purchase Approval #
                            </td>
                            <td class="input tdlast">
                                <%=ViewData.Model != null ? obj.PurchaseAppoval : ""%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label last">
                                Payment Approval #
                            </td>
                            <td class="input tdlast last">
                                <%=ViewData.Model != null ? obj.PaymentAppoval : ""%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="8" align="right">
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
                        <tr>
                            <td class="label required">
                                Comment <span>*</span>
                            </td>
                            <td>
                                <% Response.Write(Html.TextArea("Contents", "", 2, 133, new { @style = "width:280px", @maxlength = "500" }));%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label required">
                                Resolution
                            </td>
                            <td>
                                <%=Html.DropDownList("WFResolutionID", null, new { @style = "width:280px" })%>
                            </td>
                        </tr>
                        <tr id="trAssign">
                            <td class="label required">
                                Forward To
                            </td>
                            <td>
                                <%=Html.DropDownList("Assign", null, new { @style = "width:280px" })%>
                            </td>
                        </tr>
                        <tr id="trStatus">
                            <td class="label required">
                                Status
                            </td>
                            <td>
                                <%=Html.DropDownList("WFStatusID", null, new { @style = "width:280px" })%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="padding: 5px 0px;" align="right">
                    <table id="tblInvoice" border="0" cellpadding="0" cellspacing="0" style="width: 570px;
                        text-align: left;" class="grid">
                        <tr>
                            <th style="width: 170px" class="gray">
                                Invoice Date
                            </th>
                            <th style="width: 200px" class="gray">
                                Invoice Number
                            </th>
                            <th style="width: 200px" class="gray">
                                Invoice Value
                            </th>
                        </tr>
                        <% if (ViewData[CommonDataKey.LIST_INVOICE] != null)
                           {
                               List<PurchaseInvoice> listInvoice = (List<PurchaseInvoice>)ViewData[CommonDataKey.LIST_INVOICE];
                               foreach (PurchaseInvoice item in listInvoice)
                               {
                        %>
                        <tr>
                            <td style="width: 150px">
                                <%= item.InvoiceDate.HasValue ? item.InvoiceDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty%>
                            </td>
                            <td style="width: 150px">
                                <%= item.InvoiceNumber%>
                            </td>
                            <td style="width: 150px">
                                <%= item.InvoiceValue%>
                            </td>
                        </tr>
                        <%}
                           }%>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="form" style="padding-top: 20px" colspan="5" align="center">
                    <input type="submit" class="save" value="" alt="save" />
                    <input type="button" class="cancel" value="" alt="cancel" onclick="window.location='/PurchaseRequest/'" />
                </td>
            </tr>
        </table>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PurchaseRequestPageInfo.FuncUsAccountingProcess + CommonPageInfo.AppSepChar + PurchaseRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
    var indexSubmit = 0;
        $(document).ready(function () {
            $("#usAccountingForm").validate({
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
                        var resolutionId = $("#WFResolutionID").val();
                        if(resolutionId == 15 || resolutionId == 16)
                        {
                            $("span[htmlfor='Assign']").remove();
                            form.submit();
                            indexSubmit++;
                        }
                        else
                        {
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
                                        CRM.msgBox(I0009, 500);
                                    }
                                    else {
                                        form.submit();
                                        indexSubmit++;
                                    }
                                }
                            });
                        }
                     }
                }
            });

            $("#WFResolutionID").change(function () {
                $("#WFStatusID").html("");
                $("span[htmlfor='Assign']").remove();
                var resolutionId = $("#WFResolutionID").val();
                if (resolutionId != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Status', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#WFStatusID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }

                        });
                    });
                    if (resolutionId == '<%= Constants.PR_RESOLUTION_COMPLETE_ID %>' || resolutionId == '<%= Constants.PR_RESOLUTION_CANCEL %>') {
                        $("#trAssign").css("display", "none");
                        
                    }
                    else {
                        $("#trAssign").css("display", "");
                    }
                    if (resolutionId == '<%= Constants.PR_RESOLUTION_TO_BE_PROCESSED %>' || resolutionId == '<%= Constants.PR_RESOLUTION_COMPLETE_ID %>') {
                        $("#Contents").rules("remove");
                        $("span[htmlfor='Assign']").remove();
                    }
                    else {
                        $("#Contents").rules("add", { required: true, maxlength: 500 });
                    }

                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=PurchaseRequestPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% sp_GetPurchaseRequestResult obj = (sp_GetPurchaseRequestResult)ViewData.Model;
       string value = string.Empty;
       value = CommonFunc.GetCurrentMenu(Request.RawUrl) + " <a href='/PurchaseRequest/Detail/" + obj.ID + "'>" + Constants.PR_REQUEST_PREFIX + obj.ID + "</a> » " + PurchaseRequestPageInfo.FuncUsAccountingProcess;
    %>
    <%= value%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
