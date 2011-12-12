<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Purchase Request Detail - CRM
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PurchaseRequestPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    if (!(bool)ViewData[CommonDataKey.IS_ACCESSIBLE])
    {
        Html.RenderPartial("../Common/UCDoNotHavePermission");
    }
    else
    {
%>
    <%= TempData["Message"]%>
    <%  sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        int index = 0;
        List<sp_GetPurchaseRequestResult> listPR = (List<sp_GetPurchaseRequestResult>)ViewData["ListPR"];
        int number = 0;
        int totalPr = listPR.Count();
        if (listPR.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = listPR.IndexOf(listPR.Where(p => p.ID == request.ID).FirstOrDefault<sp_GetPurchaseRequestResult>());
                if (index == 0)
                {
                    styleFirst = "class=\"first first_off\"";
                    stylePrev = "class=\"prev prev_off\"";                    
                }
                else if (index == listPR.Count - 1)
                {
                    styleLast = "class=\"last last_off\"";
                    styleNext = "class=\"next next_off\"";
                }
                number = index + 1;
         }
        else if (listPR.Count == 1)
        {
            number = listPR.Count;
        }
        
    %>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                
                <td align="right">
                    <button onclick="javascript:CRM.popup('/PurchaseRequestUS/SendAnEmail/?ids=<%=request.ID %>&page=','Send mail', 860);" title="Send mail" class="button sendmail">Send mail</button>
                    <%
    string sAction = ViewData[CommonDataKey.PURCHASE_REQUEST_ACTIONS] as string;
    if (!string.IsNullOrEmpty(sAction))
    {
        Response.Write(sAction);
    }
                    %>
                </td>
            </tr>
        </table>
    </div>
    <% if(listPR != null && listPR.Count > 0 ) 
       {%>
    <div id="cnavigation" style="width:1024px">
                    <button type="button" id="btnLast" value="Last"  <%=styleLast %> ></button>
                    <button type="button" id="btnNext" value="Next" <%=styleNext %> ></button>
                    <span><%= number + " of " + totalPr%></span>        
                    <button type="button" id="btnPre" value="Prev" <%=stylePrev %> ></button>
                    <button type="button" id="btnFirst" value="First" <%=styleFirst %> ></button>                
                </div>   
      <%}%>
    <%=Html.Hidden("ID", request.ID)%>
    <% 
       double result = 0; %>
    <div id="list" style="width: 1024px">
        <h2 class="heading">
            PR Information</h2>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label" style="width: 102px;">
                    Requestor
                </td>
                <td class="input" style="width: 256px;">
                    <%=Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).RequestorName)%>
                </td>
                <td class="label" style="width: 102px;">
                    Department
                </td>
                <td class="input" style="width: 205px;">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).Department)%>
                </td>
                <td class="label" style="width: 153px;">
                    Sub Department
                </td>
                <td class="input" style="width: 205px;">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).SubDepartmentName)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Request Date
                </td>
                <td class="input">
                    <%=Html.Label(((sp_GetPurchaseRequestResult)ViewData.Model).RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW))%>
                </td>
                <td class="label">
                    Expected Date
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
    if (request.Priority.HasValue)
    {
        string sPriority = Constants.PURCHASE_REQUEST_PRIORITY.
            Where(p => int.Parse(p.Value) == request.Priority.Value).FirstOrDefault().Text;
        Response.Write("<b style='color:red'>" + sPriority + "</b>");
    }
                    %>
                </td>

            </tr>
            <tr>
                <td class="label">
                    Forwarded To
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
                <td class="label">
                </td>
            </tr>
            <tr>
                <td class="label" style="vertical-align:top">
                    Justification
                </td>
                <td class="input" colspan="5" style="width: 563px;">
                    <div id="justicaton" style="width: 563px; overflow-y: auto; overflow-x: auto; word-wrap: break-word;">
                        <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).Justification != null ?
                             Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).Justification).Replace("\r\n", "<br/>") : "&nbsp;");%>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="input" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="label">
                    Vendor/Supplier
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorName != null ?
                             Html.Encode(((sp_GetPurchaseRequestResult)ViewData.Model).VendorName) : "&nbsp;");%>
                </td>
                <td class="label">
                    Phone
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorPhone != null ?
                             ((sp_GetPurchaseRequestResult)ViewData.Model).VendorPhone : "&nbsp;");%>
                </td>
                <td class="label">
                    Email
                </td>
                <td class="input">
                    <% Response.Write(((sp_GetPurchaseRequestResult)ViewData.Model).VendorEmail != null ?
                             ((sp_GetPurchaseRequestResult)ViewData.Model).VendorEmail : "&nbsp;");%>
                </td>
            </tr>
            <tr>
                <td class="label last">
                    Address
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
                <td align="left" class="style2" style="color: #0066CC; font-size: 1.4em">
                    Item(s)
                </td>
                <td align="left">
                    Payment Method:
                    <%= ViewData["PaymentMethod"]%>
                </td>
                <td style="width: 205px;" align="right">
                    Money Type:
                    <% if (((sp_GetPurchaseRequestResult)ViewData.Model).MoneyType == Constants.TYPE_MONEY_USD)
                           Response.Write(Constants.TYPE_MONEY_USD_STRING);
                       else
                           Response.Write(Constants.TYPE_MONEY_VND_STRING);
                    %>
                </td>
            </tr>
        </table>
        <div class="form">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="view_item_miss">
                <tr>
                    <th class="header left" style="width: 102px;">
                        No #
                    </th>
                    <th class="header" style="width: 461px;">
                        Description
                    </th>
                    <th class="header" style="width: 80px;">
                        Quantity
                    </th>
                    <th class="header" style="width: 154px;">
                        Price Per Unit
                    </th>
                    <th class="header last_right" style="width: 205px;">
                        Total Cost
                    </th>
                </tr>
                <% if (ViewData[CommonDataKey.LIST_PURCHASE_ITEM] != null) %>
                <%  {
                        List<PurchaseItem> items = (List<PurchaseItem>)ViewData[CommonDataKey.LIST_PURCHASE_ITEM];

                        int count = 1;
                        foreach (PurchaseItem item in items)
                        {
                            string str = "<tr>";
                            result += item.TotalPrice;
                            str += "<td class=\"label left\">" + count + "</td>"
                             + "<td class=\"label\">" + Html.Encode(item.ItemName) + "</td>"
                             + "<td class=\"label\">" + item.Quantity + "</td>"
                             + "<td class=\"label\">" + CommonFunc.FormatCurrency(Math.Round(item.Price, Constants.ROUND_NUMBER)) + "</td>"
                             + "<td class=\"label last_right\">" + CommonFunc.FormatCurrency(Math.Round(item.TotalPrice, Constants.ROUND_NUMBER)) + "</td>" +
                             "</tr>";

                            Response.Write(str);
                            count++;
                        }
                    }                   
                %>
                <tr>
                    <td colspan="3" rowspan="9" class="clear" style="border-right: 1px solid #CCCCCC;">
                        <div class="purchase_history_header" style="top: 0px; color: #0066CC; font-size: 1.4em">
                            History
                        </div>
                        <div class="purchase_history" style="top: 0px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="view_item_miss">
                                <thead>
                                    <tr>
                                        <th class="header left">
                                            Name
                                        </th>
                                        <th class="header">
                                            Action
                                        </th>
                                        <th class="header last_right">
                                            Date
                                        </th>
                                    </tr>
                                </thead>
                                <% 
    string[] array = ((string)ViewData[CommonDataKey.PR_WORK_FLOW]).Split(Constants.SEPARATE_INVOLVE_CHAR);
    int z = 1;
    foreach (string item in array)
    {
        string[] arrayItem = item.Split(';');
        if (z == (array.Count() - 1))
        {
                                %>
                                <% }
        else
        { %>
                                <tr>
                                    <% }
        if (arrayItem.Count() > 1)
        {
                                    %>
                                    <td class="input left">
                                        <%  Response.Write(!string.IsNullOrEmpty(arrayItem[0]) ? arrayItem[0] : ""); %>
                                    </td>
                                    <td class="input">
                                        <%  Response.Write(!string.IsNullOrEmpty(arrayItem[1]) ? arrayItem[1] : "&nbsp;"); %>
                                    </td>
                                    <td class="input last_right">
                                        <%  Response.Write(!string.IsNullOrEmpty(arrayItem[2]) ? Convert.ToDateTime(arrayItem[2]).ToString(Constants.DATETIME_FORMAT_FULL) : "&nbsp;"); %>
                                    </td>
                                </tr>
                                <% 
    }
        z++;
    }                   
                                %>
                                <tr>
                                    <td class="input left">
                                        <%
                                            string name = new UserAdminDao().GetById(request.AssignID.Value).UserName;
                                            string role = new RoleDao().GetByID(request.AssignRole.Value).Name;
                                            Response.Write(name + " (" + role + ")");
                                        %>
                                    </td>
                                    <td class="input">
                                    </td>
                                    <td class="input last_right">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td class="label" style="width: 143px;">
                        Sub Total
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(result, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Other
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.OtherCost.Value, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <% if (request.SaleTaxName == "1")
                           { %>
                        TAX (US)
                        <% }
                           else if (request.SaleTaxName == "2")
                           { %>
                        VAT(VN)
                        <% }
                           else
                           { %>
                        No Sale Tax
                        <% } %>
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.SaleTaxValue.Value, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Shipping
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.Shipping.Value, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Discount
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.Discount.Value, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Service Charge
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.ServiceCharge.Value, Constants.ROUND_NUMBER))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <span class="bold">Total </span>
                    </td>
                    <td class="label last_right">
                        <span class="bold red">
                            <%=CommonFunc.FormatCurrency(Math.Round((result + request.OtherCost +
                                                                request.SaleTaxValue + request.Shipping + request.ServiceCharge - request.Discount).Value, Constants.ROUND_NUMBER))%>
                            <% if (request.MoneyType == Constants.TYPE_MONEY_USD)
                                   Response.Write(Constants.TYPE_MONEY_USD_STRING);
                               else
                                   Response.Write(Constants.TYPE_MONEY_VND_STRING);
                            %>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Purchase Approval #
                    </td>
                    <td class="label last_right">
                        <%= String.IsNullOrEmpty(request.PurchaseAppoval) ? "&nbsp;" : Html.Encode(request.PurchaseAppoval)%>
                    </td>
                </tr>
                <tr>
                    <td class="label bottom">
                        Payment Approval #
                    </td>
                    <td class="label bottom last_right">
                        <%= String.IsNullOrEmpty(request.PaymentAppoval) ? "&nbsp;" : Html.Encode(request.PaymentAppoval)%>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <% 
    index = 0;
    if (ViewData[CommonDataKey.LIST_INVOICE] != null)
    {
        List<PurchaseInvoice> invoice_items = (List<PurchaseInvoice>)ViewData[CommonDataKey.LIST_INVOICE];
        if (invoice_items.Count > 0)
        {%>
        <div class="invoice">
            <table border="0" cellpadding="0" cellspacing="0" width="462px;" class="view_item">
                <tr>
                    <td class="header" style="width: 104px">
                        Invoice Date
                    </td>
                    <td class="header" style="width: 157px">
                        Invoice Number
                    </td>
                    <td class="header last_right" style="width: 197px">
                        Invoice Value
                    </td>
                </tr>
                <%foreach (PurchaseInvoice item in invoice_items)
                  {
                      string str = "<tr>";
                      str += "<td class=\"label\">" + (item.InvoiceDate.HasValue ? item.InvoiceDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : string.Empty) + "</td>"
                      + "<td class=\"label\">" + item.InvoiceNumber + "</td>"
                      + "<td class=\"label last_right\">" + Html.Encode(item.InvoiceValue) + "</td>"
                      + "</tr>";

                      Response.Write(str);
                      index++;
                  }
        }%>
            </table>
        </div>
        <% } %>
        <div class="clrfix"> 
        </div>
        <div style="width:1024px">
            <% if (ViewData[CommonDataKey.PR_COMMENT_COUNT] != null)
               { %>
            <h2 class="heading">
                Comment(s)</h2>
            <div style="height: 170px; overflow-y: scroll; overflow-x: hidden;" class="view_comment">
                <table border="0" cellpadding="0" cellspacing="0" class="tb_comment">
                    <%
    int i = 0;
    foreach (PurchaseComment item in (IEnumerable)ViewData[CommonDataKey.PR_COMMENT])
    {
        string className = "";
        if (i % 2 != 0)
        {
            className = " class='even'";
        }
                    %>
                    <tr <%=className %> style="height: 100%">
                        <td>
                            <span class="bold">
                                <%= item.Poster%></span> <span class="gray">
                                    <%= "(" + item.PostTime + ")"%></span>
                            <br />
                            <% if (!string.IsNullOrWhiteSpace(item.Contents))
                               { %>
                            <%= Html.Encode(item.Contents).Replace("\r\n", "<br />")%>
                            <br />
                            <% } %>
                           <%= CommonFunc.SplitFileName(item.Files, Constants.PERFORMANCE_REVIEW_PATH, false)%>
                        </td>
                    </tr>
                    <%
    i++;
    }
                    %>
                </table>
            </div>
            <%  } %>
            <h2 class="heading">
                Post new comment</h2>
            <%using (Html.BeginForm("AddComment", "PurchaseRequestUS", FormMethod.Post, new { id = "addForm", @class = "form", enctype = "multipart/form-data" }))
              {%>
            <table border="0" cellpadding="0" cellspacing="0" class="edit" width="1024px">
                <tr>
                    <td>
                        <%= Html.Hidden("RequestId", (request.ID.ToString()))%>
                        <%= Html.TextArea("Contents", "", new { @Style = "width: 500px;height:46px", @maxlength = "500" })%>
                    </td>
                    <td>
                        <input type="submit" id="btnPost" class="btnPost" value="Post" />
                    </td>
                    <td valign="top">
                        <table id="tblUpload">
                            <tr>
                                <td>
                                    <input type="file" name="file" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="file" name="file" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top">
                        <button type="button" class="icon plus" title="Add New Upload" onclick="AddRowComment();">
                        </button>
                        <button type="button" class="icon minus" title="Remove" onclick="RemoveRowComment();">
                        </button>
                    </td>
                </tr>
            </table>
            <% } %>
        </div>
    </div>
    <%
    }
     %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        
        function AddRowComment() {
            $("#tblUpload").append('<tr><td><input type="file" name="file" /></td></tr>');
        }
        function RemoveRowComment() {
            if ($("#tblUpload tr").length > 1)
                $("#tblUpload tr:last").remove();
        }
        $(document).ready(function () {
            /* Navigator */
            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/PurchaseRequestUS/Navigation/?name=" + $('#btnFirst').val()
                + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/PurchaseRequestUS/Navigation/?name=" + $('#btnPre').val()
                    + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/PurchaseRequestUS/Navigation/?name=" + $('#btnNext').val()
                    + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {

                    window.location = "/PurchaseRequestUS/Navigation/?name=" + $('#btnLast').val()
                + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            /*---------------------End navigation*/
            $("#<%=CommonDataKey.PR_ROLE%>").change(function () {
                navigateWithReferrer("/PurchaseRequestUS/ChangeRole/?RoleId=" + $(this).val());
            });
            $("#addForm").validate({
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
                        required: true
                    }
                }
            });
        });
    </script>
    <style type="text/css">
        #cactionbutton button.button
        {
            float: none;
            padding-left: 25px;
            padding-right: 5px;
        }
        .style2
        {
            width: 56%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
       string funcTitle = string.Empty;
       funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + Constants.PR_REQUEST_PREFIX + request.ID;
    %>
    <%= funcTitle%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginRoles" runat="server">
    <% if (ViewData[CommonDataKey.PR_ROLE] != null)
       {%>
    <span class="bold">Login As: </span>
    <%=Html.DropDownList(CommonDataKey.PR_ROLE, null, new { @style = "width:180px" })%>
    <%} %>
</asp:Content>