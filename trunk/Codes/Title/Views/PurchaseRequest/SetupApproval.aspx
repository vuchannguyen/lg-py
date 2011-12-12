<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= PurchaseRequestPageInfo.FuncSetupApproval + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    <%= PurchaseRequestPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=TempData["Message"]%>
    <%using (Html.BeginForm("SetupApproval", "PurchaseRequest", FormMethod.Post, new { id = "setupForm" }))
      {
          sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
          Response.Write(Html.Hidden("UpdateDate", request.UpdateDate));

          Response.Write(Html.Hidden("ReqID", request.ID));
          Response.Write(Html.Hidden("hidValue", ViewData[CommonDataKey.PR_LIST_COUNT]));

          if (Request.UrlReferrer != null)
          {
              Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
          }
    %>
    <div id="list" style="width: 1024px">
        <h2 class="heading">
            Set up Approval</h2>
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
                    <% if (((sp_GetPurchaseRequestResult)ViewData.Model).PaymentID == Constants.TYPE_PAYMENT_CASH)
                           Response.Write(Constants.TYPE_PAYMENT_CASH_STRING);
                       else if (((sp_GetPurchaseRequestResult)ViewData.Model).PaymentID == Constants.TYPE_PAYMENT_TRANFER)
                           Response.Write(Constants.TYPE_PAYMENT_TRANFER_STRING);
                    %>
                </td>
                <td style="width: 20%;" align="right">
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
            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="view_item">
                <tr>
                    <th class="header" style="width: 10%;">
                        No #
                    </th>
                    <th class="header" style="width: 45%;">
                        Description
                    </th>
                    <th class="header" style="width: 10%;">
                        Quantity
                    </th>
                    <th class="header" style="width: 15%;">
                        Price Per Unit
                    </th>
                    <th class="header last_right" style="width: 20%;">
                        Total Cost
                    </th>
                </tr>
                <% if (ViewData[CommonDataKey.LIST_PURCHASE_ITEM] != null) %>
                <%  {
                        List<PurchaseItem> items = (List<PurchaseItem>)ViewData[CommonDataKey.LIST_PURCHASE_ITEM];

                        int count = 1;
                        double result = 0;
                        foreach (PurchaseItem item in items)
                        {
                            string str = "<tr>";
                            result += item.TotalPrice;
                            str += "<td class=\"label\">" + count + "</td>"
                             + "<td style='width:460px' class=\"label\">" + Html.Encode(item.ItemName) + "</td>"
                             + "<td class=\"label\">" + item.Quantity + "</td>"
                             + "<td class=\"label\">" + CommonFunc.FormatCurrency(Math.Round(item.Price, 1)) + "</td>"
                             + "<td class=\"label last_right\">" + CommonFunc.FormatCurrency(Math.Round(item.TotalPrice, 1)) + "</td>" +
                             "</tr>";

                            Response.Write(str);
                            count++;
                        }                   
                %>
            </table>
        </div>
        <div class="total_items" style="float: right">
            <table border="0" cellpadding="0" cellspacing="0" width="360px" class="view_item">
                <tr>
                    <td class="label" style="width: 143px;">
                        Sub Total
                    </td>
                    <td class="label last_right">
                        <%= CommonFunc.FormatCurrency(Math.Round(result, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Other
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency( Math.Round(request.OtherCost.Value, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        VAT(VN)
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(  Math.Round(request.SaleTaxValue.Value, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Shipping
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency( Math.Round(request.Shipping.Value, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Discount
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.Discount.Value, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Service Charge
                    </td>
                    <td class="label last_right">
                        <%=CommonFunc.FormatCurrency(Math.Round(request.ServiceCharge.Value, 1))%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <span class="bold">Total </span>
                    </td>
                    <td class="label last_right">
                        <span class="bold red">
                            <%=CommonFunc.FormatCurrency(Math.Round((result + request.OtherCost + request.SaleTaxValue + request.Shipping).Value, 1))%>
                            <% if (request.MoneyType == Constants.TYPE_MONEY_USD)
                                   Response.Write(Constants.TYPE_MONEY_USD_STRING);
                               else
                                   Response.Write(Constants.TYPE_MONEY_VND_STRING);
                            %>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <% } %>
        <br />
        <div class="setup_approval" style="margin-top: 130px">
            <table id="tblSetup" border="0" cellpadding="0" cellspacing="0" width="560px">
                <tr class="last">
                    <td class="bold" style="width: 20%; border-right: none; padding-left: 5px;">
                        Approval(s)
                    </td>
                    <td colspan="2" align="right">
                        <button id="add" type="button" onclick="AddRow();" class="icon plus">
                        </button>
                        <button id="remove" type="button" onclick="RemoveRow();" class="icon minus">
                        </button>
                    </td>
                </tr>
            </table>
            <%
        
                // Get list approval of purchase request
                int max = Convert.ToInt32(ViewData[CommonDataKey.PR_MAX_APPROVAL]);
                int listCount = Convert.ToInt32(ViewData[CommonDataKey.PR_LIST_COUNT]);
                List<sp_GetListApprovalAssignResult> listAppro = (List<sp_GetListApprovalAssignResult>)ViewData[CommonDataKey.LIST_APPROVAL];
                for (int i = 1; i <= max; i++)
                {
                    sp_GetListApprovalAssignResult approval = null;
                    if (null != listAppro && i <= listAppro.Count)
                        approval = listAppro[i - 1];
                    string styleCSS = string.Empty;
                    if (i <= listCount)
                    {
                        styleCSS = "show form";
                    }
                    else
                    {
                        styleCSS = "hidden form";
                    }
            %>
            <div id="div<%=i %>" class="<%=styleCSS%>">
                <table>
                    <tr>
                        <td class="header" style="width: 151px">
                            Approved by:
                        </td>
                        <% 
var principal = User as AuthenticationProjectPrincipal;
CommonDao cmDao = new CommonDao();
List<WFRole> list = cmDao.GetRoleListForApproval(Constants.WORK_FLOW_PURCHASE_REQUEST);
list = list.Where(q => q.ID != Constants.PR_US_ACCOUNTING).ToList();
List<UserAdmin> adminList = null;
if (approval != null)
    adminList = cmDao.GetUserAdminByRole(approval.ApproverGroup);
                        %>
                        <% SelectList listApp = new SelectList(list, "ID", "Name", approval != null ? approval.ApproverGroup : 0);%>
                        <% 
SelectList listUser = new SelectList(new List<string>());
if (approval != null && i <= listCount)
    listUser = new SelectList(adminList, "UserAdminId", "UserName", approval != null ? approval.UserAdminId : 0);%>
                        <td class="header" style="width: 175px">
                            <% if (approval != null && i <= listCount)
                               { %>
                            <%=Html.DropDownList("Approval" + i.ToString(), listApp, Constants.FIRST_ITEM_CHOOSE, new { @style = "width:130px" })%>
                            <% }
                               else
                               { %>
                            <%=Html.DropDownList("Approval" + i.ToString(), ViewData[CommonDataKey.PR_ROLE] as SelectList, Constants.FIRST_ITEM_CHOOSE, new { @style = "width:130px" })%>
                            <%} %>
                        </td>
                        <td class="header" style="width: 175px">
                            <%  
string str = "Assign" + i.ToString();
if (approval == null && i <= listCount)
{ %>
                            <% 
                                SelectList temp = new SelectList(new List<string>()); %>
                            <%=Html.DropDownList(str, temp, Constants.FIRST_ITEM_CHOOSE, new { @style = "width:130px" })%>
                            <% }
else
{ %>
                            <%=Html.DropDownList(str, listUser, new { @style = "width:130px" })%>
                            <% } %>
                        </td>
                        <td class="header" style="width: 100px">
                            <%  
string strCheck = "isImmediate" + i.ToString();
if (approval == null && i <= listCount)
{
    //do nothing
}
else
{
    Response.Write(Html.CheckBox(strCheck));
    Response.Write("<label for='" + strCheck + "'>&nbsp;Immediately</label>");
}
                                
                            %>
                        </td>
                    </tr>
                </table>
            </div>
            <% } %>
        </div>
    </div>
    <div id="forwardto">
        <% string strMessage = "After this PR is approved by step(s) above, it will be forwarded to step below:";
        %>
        <p>
            <%=strMessage %></p>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
            <tr style="margin-bottom: 10px">
                <td colspan="2">
                    <table cellspacing="0" cellpadding="0" border="0" class="view">
                        <tr>
                            <td class="label required ">
                                Purchasing :
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("Forward", null, Constants.FIRST_ITEM, new { @style = "width:200px" })%>
                            </td>
                        </tr>
                        <%if (request.BillableToClient)
                          { %>
                        <tr>
                            <td class="label required last">
                                US Accounting :
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("USAccounting", null, Constants.FIRST_ITEM, new { @style = "width:200px" })%>
                            </td>
                        </tr>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#USAccounting").rules("add", "required");
                            });
                        </script>
                        <%} %>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="submit" class="save" value="" id="btnSave" />
                    <input type="button" class="cancel" value="" id="btnCancel" onclick="window.location = '/PurchaseRequest'" />
                </td>
            </tr>
        </table>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
            

        var index = <%=ViewData[CommonDataKey.PR_LIST_COUNT] %>;  

        function AddRow() {
            if (index < 3) {
                index++;
                var s = "#div" + index.toString();               
                $(s).removeClass();
                $(s).addClass("show form");
                
                $("#hidValue").val(index);
                $("#remove").removeAttr("class");                
                $("#remove").addClass("icon minus");
                $("#Assign"+ index.toString()).rules("add", "required");
                
                $("#Approval"+ index.toString()).rules("add", "required");
                if (index > 1)  {
                    $("#Approval"+ index).rules("add", "compareRole");                
                }
            }   
            
            if (index == 3) {
                $("#add").removeClass();
                $("#add").addClass("icon plus_off");                            
            }
            
        }

        function RemoveRow() {
            if (index > 0) {
                var s = "#div" + index.toString();               
                $(s).removeClass();
                $(s).addClass("hidden");
                $("span[htmlfor=Assign" + index.toString() +"]").remove();
                $("span[htmlfor=Approval" + index.toString() +"]").remove();
                $("#Assign"+ index).val("");
                $("#Approval"+ index).val("");
                $("#Assign"+ index).rules("remove");
                $("#Approval"+ index).rules("remove");
                for (var i =1 ;i<index;i++) {
                    $("span[htmlfor=Approval" + i.toString() +"]").remove();
                }
                    
                index--;
                $("#hidValue").val(index);
                $("#add").removeAttr("class");                
                $("#add").addClass("icon plus");
                
            }            

            if(index == 0) {
                
                $("#remove").removeAttr("class");                
                $("#remove").addClass("icon minus_off");
            }
        }

        function setValidate() {    
            $("#Forward").rules("add", {required:true});
            for(var i=1; i<= index;i++)
            {
                $("#Assign"+ i.toString()).rules("add", {required:true});
                $("#Approval"+ i.toString()).rules("add", {required:true});                
                if(index >1) {
                    $("#Approval"+ i).rules("add", "compareRole");                
                }
            }
        }

        $(document).ready(function () {
          
            $("#Approval1").change(function () {
                $("#Assign1").html("");   
                          
                var ApprovalId = $("#Approval1").val();
                if (ApprovalId != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + ApprovalId + '&Page=AssignListByRole', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#Assign1").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }

                        });
                    });                
                }
            });

            $("#Approval2").change(function () {
                $("#Assign2").html("");            
                var ApprovalId = $("#Approval2").val();
                if (ApprovalId != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + ApprovalId + '&Page=AssignListByRole', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#Assign2").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }

                        });
                    });                
                }
            });

            $("#Approval3").change(function () {
                $("#Assign3").html("");            
                var ApprovalId = $("#Approval3").val();
                if (ApprovalId != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + ApprovalId + '&Page=AssignListByRole', function (item) {
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#Assign3").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }

                        });
                    });                
                }
            });

            var USAccounting = '';
            if($("#USAccounting").length > 0)
            {
                USAccounting = $("#USAccounting").val();
            }
            
            $("#setupForm").validate({
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
                    jQuery.ajax({
                        url: '/PurchaseRequest/CheckMultiOfficeStatus',
                        dataType: 'json',
                        data: ({
                                'Assign1': $("#Assign1").val(),
                                'Assign2': $("#Assign2").val(),
                                'Assign3': $("#Assign3").val(),
                                'Forward': $("#Forward").val(),
                                'USAccounting': $("#USAccounting").val()
                            }),
                        success: function (result) {
                            if (result.MsgType == 1) {
                                var obj = result.Holders[1];
                                $("span[htmlfor='"+obj+"']").remove();
                                var objectError = $('<span htmlfor="'+obj+'" class="error" generated="true" style="display: inline-block;">' + result.Holders[0] + '</span>');
                                objectError.tooltip({
                                        bodyHandler: function () {
                                            return objectError.html();
                                        }
                                    });
                                    objectError.insertAfter($("#"+obj));
                                    CRM.msgBox(I0009, 500);
                            }
                            else {
                                 form.submit();
                            }
                        }
                    });
                }
            });
            setValidate();        
        });
       
        jQuery.validator.addMethod(
        "compareRole", function (value, element) {
    
        var role1 = $("#Approval1" + " option:selected").text();
        var role2 = $("#Approval2" + " option:selected").text();
        var role3 = $("#Approval3" + " option:selected").text();
        
        var isValid = false;
        if (index == 3)
            { 
                isValid = (role1 != role2) && (role1 != role3) && (role2 != role3);
                jQuery.validator.messages.compareRole = "The selected roles must difference";
            }        
        else if (index == 2)
        { 
            isValid = role1 != role2;
            jQuery.validator.messages.compareRole = "The selected roles must difference";
        }        
        else 
        {
            isValid = true;
        }
        return isValid;
        }, jQuery.validator.messages.compareRole);        
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
       string funcTitle = string.Empty;
       funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) +
           "<a href='/PurchaseRequest/Detail/" + request.ID + "'>" + Constants.PR_REQUEST_PREFIX + request.ID + "</a> » " + PurchaseRequestPageInfo.FuncSetupApproval;
    %>
    <%= funcTitle%>
</asp:Content>
