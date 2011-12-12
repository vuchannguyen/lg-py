<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <%using (Html.BeginForm("FillData", "PurchaseRequest", FormMethod.Post, new { id = "purchaseForm", @class = "form" }))
    {
          sp_GetPurchaseRequestResult obj = (sp_GetPurchaseRequestResult)ViewData.Model;
          Response.Write(Html.Hidden("UpdateDate", obj.UpdateDate));
          Response.Write(Html.Hidden("ReqID", obj.ID));
          Response.Write(Html.Hidden("hidValue", 1));
        if(obj.BillableToClient)
        {
            if (obj.USAccounting.HasValue)
            {
                Response.Write(Html.Hidden("hidUserAdminId", obj.USAccounting.Value.ToString()));
                UserAdmin objUserAdmin = new UserAdminDao().GetById(obj.USAccounting.Value);
                if (objUserAdmin != null)
                {
                    Response.Write(Html.Hidden("hidUserAdminName", objUserAdmin.UserName + "(US Accounting)"));
                }
            }
        }
        if (Request.UrlReferrer != null)
        {
            Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
        }
       %>
    <%=TempData["Message"]%>
    
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
                            Response.Write("<b style='color:red'>" + sPriority + "</b>");
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
                        <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).ResolutionName)%>
                    </span>
                </td>
                
                <td class="label">
                    
                </td>
                <td class="input">
                    
                </td>
            </tr>
            <tr>
                <td class="label">
                    Justification
                </td>
                <td class="input" colspan="3" style="width:563px;">
                    <div id="justicaton" style="width:563px;overflow-y:auto;overflow-x:auto;word-wrap:break-word;">
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
                    Payment Method: <% if (obj.PaymentID == Constants.TYPE_PAYMENT_CASH)
                                           Response.Write(Constants.TYPE_PAYMENT_CASH_STRING);
                                       else if (obj.PaymentID == Constants.TYPE_PAYMENT_TRANFER)
                                           Response.Write(Constants.TYPE_PAYMENT_TRANFER_STRING);
                    %>
                </td>
                <td style="width: 20%;" align="right">
                    Money Type: <% if (obj.MoneyType == Constants.TYPE_MONEY_USD)
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
            <div class="total_items" style="float: right;width:363px !important">
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
                            Discount
                        </td>
                        <td class="label last_right">
                            <%=CommonFunc.FormatCurrency(Math.Round(obj.Discount.Value, 1))%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            Service Charge
                        </td>
                        <td class="label last_right">
                            <%=CommonFunc.FormatCurrency(Math.Round(obj.ServiceCharge.Value, 1))%>
                        </td>
                    </tr>
                    <tr>
                        <td class="label">
                            <span class="bold">Total </span>
                        </td>
                        <td class="label last_right">
                            <span class="bold red">
                                <%=CommonFunc.FormatCurrency(Math.Round(((double)ViewData[CommonDataKey.SUB_TOTAL_ITEM] +
                                    obj.OtherCost + obj.SaleTaxValue + obj.Shipping + obj.ServiceCharge - obj.Discount).Value, 1)) + 
                                    " " + (obj.MoneyType == Constants.TYPE_MONEY_USD ? Constants.TYPE_MONEY_USD_STRING : Constants.TYPE_MONEY_VND_STRING)%></span>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="5" align="right" id="tblapproval" style="padding:5px 0px;">
                        <table cellpadding="0" cellspacing="0" style="width:363px">
                            <tr>
                                <td class="label">
                                    Purchase Approval #
                                </td>
                                <td class="input tdlast">
                                    <%=Html.TextBox("PurchaseAppoval", ViewData.Model != null ? obj.PurchaseAppoval : "", new { @style = "width:150px",@maxlength=50 })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label last">
                                    Payment Approval #
                                </td>
                                <td class="input tdlast last">
                                    <%=Html.TextBox("PaymentAppoval", ViewData.Model != null ? obj.PaymentAppoval : "", new { @style = "width:150px", @maxlength = 50 })%>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td colspan="8" align="right">
                    <button type="button" onclick="AddRow();" class="icon plus">
                    </button>
                    <button type="button" onclick="RemoveRow();" class="icon minus">
                    </button>
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top">
                    <table class="form">
                        <tr>
                            <td class="label" style="width: 120px;">
                                Resolution
                            </td>
                            <td class="input" style="width: 220px;">
                                <%=Html.DropDownList("WFResolutionID", null, new { @style = "width:190px;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Status
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("WFStatusID", null, new { @style="width:120px;" })%>
                            </td>
                        </tr>
                        <tr id="trAssign">
                            <td class="label">
                                Assign to
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("Assign", null, new { @style = "width:190px;" })%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="padding:5px 0px;" align="right">
                    <table id="tblInvoice" border="0" cellpadding="0" cellspacing="0" style="width:570px; text-align:left;" class="grid">
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
                        <%if (ViewData[CommonDataKey.LIST_INVOICE] == null)
                          {  %>
                        <tr>
                            <td>
                                <input type="text" id='ivcDate1' name="ivcDate1" class="tempInvoiceDate text" style="width: 80px" />
                            </td>
                            <td>
                                <input type="text" id="ivcNumber1" name="ivcNumber1" class="tempInvoiceNumber text" style="width: 120px" maxlength="50" />
                            </td>
                            <td>
                                <input type="text" id="ivcValue1" name="ivcValue1" class="tempInvoiceValue text" style="width: 120px" maxlength="50" />
                            </td>
                        </tr>
                        <%}
                          else
                          {
                              int i = 1;
                              List<PurchaseInvoice> listInvoice = (List<PurchaseInvoice>)ViewData[CommonDataKey.LIST_INVOICE];
                              foreach (PurchaseInvoice item in listInvoice)
                              {
                        %>
                        <tr>
                            <td style="width: 150px">
                                <%= Html.TextBox("ivcDate" + i,item.InvoiceDate.HasValue?item.InvoiceDate.Value.ToString(Constants.DATETIME_FORMAT):string.Empty, new { @class = "tempInvoiceDate text", @style = "width: 80px" })%>
                            </td>
                            <td style="width: 150px">
                            <%= Html.TextBox("ivcNumber" + i, item.InvoiceNumber, new { @class = "tempInvoiceNumber text", @style = "width: 120px", @maxlength=50 })%>
                            </td>
                            <td style="width: 150px">
                            <%= Html.TextBox("ivcValue" + i, item.InvoiceValue, new { @class = "tempInvoiceValue text", @style = "width: 120px", @maxlength = 50 })%>
                            </td>
                        </tr>
                        <% i++;
                              }
                          }    
                        %>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="form" style="padding-top: 20px" colspan="5" align="center">
                    <input type="submit" class="save" value="" alt="save" />
                    <input type="button" class="cancel" value="" alt="cancel" id="btnCancel"  />
                </td>
            </tr>
        </table>
        </div>
        
        <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
 <%= PurchaseRequestPageInfo.FuncPurchasingFillData + CommonPageInfo.AppSepChar + PurchaseRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    var sDateOffInputSelector = "input[name^='ivcDate']";
    var sInvoiceNumberSelector = "input[name^='ivcNumber']";
    var sInvoiceValueSelector = "input[name^='ivcValue']";
    var indexSubmit = 0;
    function AddRow() {
        var index = $("#tblInvoice").find("tr").length;
        
        $("#hidValue").val(index);
        $("#tblInvoice").append(
            "<tr><td>" +
                   "<input style='width:80px;margin:0px;' type='text' name='ivcDate" + index + "' id='ivcDate" + index + "'/>" +
                "</td>" +
                "<td>" +
                    "<input style='width:120px' maxlength='50' type='text' name='ivcNumber" + index + "' id='ivcNumber" + index + "'/>" +
                "</td>" +
                "<td>" +
                    "<input style='width:120px' maxlength='50' type='text' name='ivcValue" + index + "' id='ivcValue" + index + "'/>" +
                "</td></tr>");
        
        $("#ivcDate" + index).rules("add", { checkDate: true, required: true });
        $("#ivcDate" + index).datepicker(
            {
                onClose: function () { $(this).valid(); }
            }
        );
        $("#ivcNumber" + index).rules("add", { required: true, maxlength:50 });
        $("#ivcValue" + index).rules("add", { required: true, maxlength: 50 });
        
        $("#hidValue").val(index);
    }
    function RemoveRow() {
        var index = $("#tblInvoice").find("tr").length;    
        
        if (index > 1) {
            $("#tblInvoice tr:last").remove();                        
            $("#hidValue").val(index-2);
        }
    }

    $(document).ready(function () {
        $("#btnCancel").click(function () {

            var returnUrl = document.referrer;
            if (returnUrl != "") {
                window.location = returnUrl;
            }
            else {
                window.location = "/PurchaseRequest/";
            }
        });
        var index = $("#tblInvoice").find("tr").length;
        $("#hidValue").val(index - 1);

        $("#purchaseForm").validate({
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
        });

        // add date picker and validate
        $("#tblInvoice tr").find(sDateOffInputSelector).datepicker();
        var items = $("#tblInvoice tr").find(sDateOffInputSelector);
        for (i = 0; i < items.length; i++) {
            $(items[i]).rules("add", { checkDate: true, required: true });
        }

        items = $("#tblInvoice tr").find(sInvoiceNumberSelector);
        for (i = 0; i < items.length; i++) {
            $(items[i]).rules("add", { required: true, maxlength: 50 });
        }

        items = $("#tblInvoice tr").find(sInvoiceValueSelector);
        for (i = 0; i < items.length; i++) {
            $(items[i]).rules("add", { required: true, maxlength: 50 });
        }

        $("#WFResolutionID").change(function () {
            $("#WFStatusID").html("");
            $("#Assign").html("");
            var resolutionId = $("#WFResolutionID").val();
            if (resolutionId != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Status', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#WFStatusID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }

                    });
                });
                if (resolutionId == '<%= Constants.PR_RESOLUTION_TO_BE_FILL_DATA_BY_PURCHASING %>' || resolutionId == '<%= Constants.PR_RESOLUTION_COMPLETE_WAITING_CLOSE_ID %>') {
                    $("#trAssign").css("display", "");
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=PurChaseAssign', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#Assign").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }
                        });
                    });
                }
                else if (resolutionId == '<%= Constants.PR_RESOLUTION_TO_BE_PROCESSED %>') {
                    $("#trAssign").css("display", "");
                    if ($("#hidUserAdminId").val() == undefined) {
                        $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=USAccouting', function (item) {
                            $.each(item, function () {
                                if (this['ID'] != undefined) {
                                    $("#Assign").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                                }
                            });
                        });
                    }
                    else {
                        $("#Assign").append($("<option value='" + $("#hidUserAdminId").val() + "'>" + $("#hidUserAdminName").val() + "</option>"));
                    }
                }
                else {
                    $("#trAssign").css("display", "none");
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
       value = CommonFunc.GetCurrentMenu(Request.RawUrl)+" <a href='/PurchaseRequest/Detail/" + obj.ID + "'>" +Constants.PR_REQUEST_PREFIX+ obj.ID + "</a> » " + PurchaseRequestPageInfo.FuncPurchasingFillData;
    %>
    <%= value%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
