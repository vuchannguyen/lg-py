<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<style type="text/css">
    .ac_results
    {
        width: 220px !important;
    }
</style>
<% PurchaseRequest obj = (PurchaseRequest)ViewData.Model;
   int quantityMaxlength = 5;
   Response.Write(Html.Hidden("hidItem", ViewData[CommonDataKey.COUNT_PURCHASE_ITEM]));
   if (ViewData.Model != null)
   {
       Response.Write(Html.Hidden("AssignID", obj.AssignID));
       Response.Write(Html.Hidden("AssignRole", obj.AssignRole));
       Response.Write(Html.Hidden("InvolveID", obj.InvolveID));
       Response.Write(Html.Hidden("InvolveRole", obj.InvolveRole));
       Response.Write(Html.Hidden("InvolveResolution", obj.InvolveResolution));
       Response.Write(Html.Hidden("InvolveDate", obj.InvolveDate));
       Response.Write(Html.Hidden("RequestorId", obj.Requestor));
       Response.Write(Html.Hidden("UserRole", (string)ViewData["UserRole"]));
       Response.Write(Html.Hidden("UpdateDate", obj.UpdateDate));
   }
   if (Request.UrlReferrer != null)
   {
       Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
   }
   var selectedRequestor = ViewData[CommonDataKey.PR_SELECTED_REQUESTOR] ?? "";
    
%>
<table cellspacing="0" cellpadding="0" border="0" width="1050px" class="form profile">
    <tr>
        <td colspan="6" class="ctbox">
            <h2>
                PR Information</h2>
        </td>
    </tr>
    <tr>
        <td valign="top" class="ccbox">
            <table cellspacing="0" cellpadding="0" border="0" width="1050px" class="edit">
                <tr>
                    <td class="label" style="width: 120px;">
                        Requestor
                    </td>
                    <td class="input">
                        <% if (ViewData.Model == null)
                           {
                               var principal = HttpContext.Current.User as AuthenticationProjectPrincipal;
                               Response.Write("<span class='bold'>" + principal.UserData.UserName + "</span>");
                           }
                           else
                           {
                               Response.Write("<span class='bold'>" + obj.UserAdmin1.UserName + "</span>");
                           }  
                        %>
                    </td>
                    <td class="label required" style="width: 120px;">
                        Department <span style="color: Red">*</span>
                    </td>
                    <td class="input" style="width: 170px">
                        <%=Html.DropDownList("DepartmentName", null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:140px" })%>
                    </td>
                    <td class="label required" style="width: 115px;">
                        Sub Department <span style="color: Red">*</span>
                    </td>
                    <td class="input" style="width: 190px">
                        <%=Html.DropDownList("SubDepartment", null, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:150px" })%>
                    </td>
                </tr>
                <tr>
                    <td class="label required" style="width: 120px;">
                        Request Date <span style="color: Red">*</span>
                    </td>
                    <td class="input" style="width: 190px">
                        <b>
                            <% if (ViewData.Model == null)
                               {                                   
                                   Response.Write(Html.Hidden("RequestDate", DateTime.Now.ToString(Constants.DATETIME_FORMAT)));
                                   Response.Write(DateTime.Now.ToString(Constants.DATETIME_FORMAT));
                               }
                               else
                               {                                   
                                   Response.Write(Html.Hidden("RequestDate", obj.RequestDate.ToString(Constants.DATETIME_FORMAT)));
                                   Response.Write(obj.RequestDate.ToString(Constants.DATETIME_FORMAT));
                               }                           
                            %>
                        </b>
                    </td>
                    <td class="label required" style="width: 120px">
                        Expected Date <span style="color: Red">*</span>
                    </td>
                    <td class="input">
                        <% if (ViewData.Model == null)
                           {
                               Response.Write(Html.TextBox("ExpectedDate", "", new { @maxlength = "10", @style = "width:115px" }));
                           }
                           else
                           {
                               Response.Write(Html.TextBox("ExpectedDate", obj.ExpectedDate.HasValue ? obj.ExpectedDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty, new { @maxlength = "10", @style = "width:115px" }));
                           }  
                        %>
                    </td>
                    <td class="label" style="width: 150px;">
                        Priority
                    </td>
                    <td class="input" style="width: 190px">
                        <%=Html.DropDownList(CommonDataKey.PURCHASE_REQUEST_PRIORITY, null, 
                            Constants.PURCHASE_REQUEST_PRIORITY_LABEL, new { @style = "width:150px" })%>
                    </td>
                </tr>
                <tr>
                    <td class="label required" rowspan="4" style="width: 120px; vertical-align: top">
                        Justification <span style="color: Red">*</span>
                    </td>
                    <td class="input" colspan="3" rowspan="4">
                        <%=Html.TextArea("Justification", ViewData.Model != null ? obj.Justification : "", new { @style = "width:500px; height:100px", @maxlength = "500" })%>
                    </td>
                    <td class="label required" style="width: 150px;">
                        Billable To Client <span style="color: Red">*</span>
                    </td>
                    <td class="input" style="width: 120px;">
                        <% if (ViewData.Model == null)
                           {
                               Response.Write("<label for=Yes>" + Html.RadioButton("chk_BillableGroup", "True", false, new { id = "Yes" }) + " Yes</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ");
                               Response.Write("<label for=No>" + Html.RadioButton("chk_BillableGroup", "False", false, new { id = "No" }) + " No</label>");
                               Response.Write(Html.Hidden("txt_BillableGroup", ""));
                           }
                           else
                           {
                               if (obj.BillableToClient)
                               {
                                   Response.Write("<label for=Yes>" + Html.RadioButton("chk_BillableGroup", "True", true, new { id = "Yes" }) + " Yes</label> ");
                                   Response.Write("<label for=No>" + Html.RadioButton("chk_BillableGroup", "False", false, new { id = "No" }) + " No</label>");
                               }
                               else
                               {
                                   Response.Write("<label for=Yes>" + Html.RadioButton("chk_BillableGroup", "True", false, new { id = "Yes" }) + " Yes</label> ");
                                   Response.Write("<label for=No>" + Html.RadioButton("chk_BillableGroup", "False", true, new { id = "No" }) + " No</label>");
                               }
                               Response.Write(Html.Hidden("txt_BillableGroup", obj.BillableToClient));
                           }
                        %>
                        <% Response.Write(Html.Hidden("txt_BillableGroup")); %>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 150px;">
                        Vendor/Supplier
                    </td>
                    <td class="input">
                        <%=Html.TextBox("VendorName", ViewData.Model != null ? obj.VendorName : "", new { @maxlength = "100", @style = "width:140px" })%>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 150px;">
                        Phone
                    </td>
                    <td class="input">
                        <%=Html.TextBox("VendorPhone", ViewData.Model != null ? obj.VendorPhone : "", new { @maxlength = "50", @style = "width:140px" })%>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 150px;">
                        Email
                    </td>
                    <td class="input" style="width: 190px">
                        <%=Html.TextBox("VendorEmail", ViewData.Model != null ? obj.VendorEmail : "", new { @maxlength = "100", @style = "width:140px" })%>
                    </td>
                </tr>
                <tr>
                    <td class="label" style="width: 120px;">
                        CC List
                    </td>
                    <td class="input" colspan="3" style="padding-right: 30px">
                        <% if (ViewData.Model == null)
                           {
                               Response.Write(Html.TextBox("fieldCC", "", new { @style = "width:140px" }));
                               Response.Write(Html.Hidden("CCList", ""));
                           }
                           else
                           {
                               Response.Write(Html.TextBox("fieldCC", obj.CCList, new { @style = "width:140px" }));
                               Response.Write(Html.Hidden("CCList", ""));
                           }              
                        %>
                        <br/><%= Constants.SAMPLE_AUTO_COMPLETE %>
                    </td>
                    <td class="label" style="width: 150px; vertical-align: top">
                        Address
                    </td>
                    <td class="input" style="vertical-align: top">
                        <%=Html.TextBox("VendorAddress", ViewData.Model != null ? obj.VendorAddress : "", new { @style = "width:140px;", @maxlength = "200" })%>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left">
                        <table width="420px">
                            <tr>
                                <td style="width: 80px">
                                    <span style="font-size: 1.4em; color: #0066CC; font-weight: bold">Item(s)</span>
                                </td>                                
                            </tr>
                        </table>
                    </td>
                    <td colspan="2" align="left">
                        <table  style="float: right;" width="100%">
                            <tr>                                
                                <td align="right">
                                    <button type="button" title="Add New Item" onclick="AddRow();" class="icon plus">
                                    </button>
                                    &nbsp;
                                    <button type="button" title="Remove Last Item" onclick="RemoveRow();" class="icon minus">
                                    </button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" valign="top">
                        <table id="tbl_purchase" border="0" cellpadding="0" cellspacing="0" class="view_item_miss">
                            <thead>
                                <tr>
                                    <th style="width: 30px; text-align: center" class="header">
                                        No
                                    </th>
                                    <th style="width: 300px; text-align: left" class="header">
                                        Description
                                    </th>
                                    <th style="width: 200px; text-align: left" class="header">
                                        Quantity
                                    </th>
                                    <th style="width: 200px; text-align: left" class="header">
                                        Price Per Unit
                                    </th>
                                    <th style="width: 200px; text-align: left" class="header">
                                        Total Cost
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <% if (ViewData.Model == null)
                                   { %>
                                <tr id='row1'>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        <input id='description1' class="temp_description" name='description1' type='text'
                                            maxlength="100" style="width: 150px;" />
                                    </td>
                                    <td>
                                        <input name='quantity1' class="temp_quantity" id='quantity1' type='text' style="width: 80px;"
                                            maxlength="<%=quantityMaxlength%>" value='0' />
                                    </td>
                                    <td>
                                        <input name='price1' class="temp_price" id='price1' type='text' style="width: 80px;"
                                            maxlength="10" value='0' />
                                    </td>
                                    <td>
                                        <input class='temp_total' id='temp_total1' type='text' readonly='readonly' value='0' />
                                    </td>
                                </tr>
                                <%  }
                                   else
                                   {
                                       List<PurchaseItem> listItem = (List<PurchaseItem>)ViewData[CommonDataKey.LIST_PURCHASE_ITEM];
                                       int i = 1;
                                       foreach (PurchaseItem item in listItem)
                                       {%>
                                <tr id='row<%=i %>'>
                                    <td>
                                        <%=i %>
                                        <%= Html.Hidden("Item"+i,item.ID) %>
                                    </td>
                                    <td>
                                        <%= Html.TextBox("description" + i, item.ItemName, new { @style = "width:150px", @class = "temp_description",@maxlength="100" })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBox("quantity" + i, item.Quantity, new { @style = "width:80px", @class = "temp_quantity", @maxlength = quantityMaxlength })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBox("price" + i, item.Price, new { @style = "width:80px", @class = "temp_price", @maxlength = "10" })%>
                                    </td>
                                    <td>
                                        <%= Html.TextBox("temp_total" + i, CommonFunc.FormatCurrency(item.TotalPrice), new { @class = "temp_total", @readonly = "readonly" })%>
                                    </td>
                                </tr>
                                <%     i++;
                                       }
                                   }
                                %>
                            </tbody>
                        </table>
                    </td>
                    <td colspan="2" valign="bottom" style="padding-left: 0px">
                        <table id="tbl_Summary" width="100%">
                            <tr>
                                <td class="label required" style="width: 150px;">
                                    Payment Method <span style="color: Red">*</span>
                                </td>                                
                                <td class="input">                                    
                                    <%=Html.DropDownList(CommonDataKey.DDL_PR_PAYMENTMETHOD, null, Constants.FIRST_ITEM_CHOOSE, new { @style = "width:115px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label required" style="width: 152px">
                                    Money Type <span style="color: Red">*</span>
                                </td>
                                <td id="Type" class="input">
                                    <% if (ViewData.Model == null)
                                       {
                                           Response.Write("<label hidValue='USD'>" + Html.RadioButton("chk_MoneyTypeGroup", "1", true) + " USD</label>&nbsp;");
                                           Response.Write("<label hidValue='VND'>" + Html.RadioButton("chk_MoneyTypeGroup", "2", false) + " VND</label>");
                                       }
                                       else
                                       {
                                           Response.Write("<label hidValue='USD'>" + Html.RadioButton("chk_MoneyTypeGroup", "1", obj.MoneyType == Constants.TYPE_MONEY_USD ? true : false) + " USD</label>&nbsp; ");
                                           Response.Write("<label hidValue='VND'>" + Html.RadioButton("chk_MoneyTypeGroup", "2", obj.MoneyType == Constants.TYPE_MONEY_VND ? true : false) + " VND</label>");
                                       } 
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 152px">
                                    Sub Total
                                </td>
                                <td class="input">
                                    <%= Html.TextBox("SubTotal", ViewData[CommonDataKey.SUB_TOTAL_ITEM] != null ? 
                                        CommonFunc.FormatCurrency(double.Parse(ViewData[CommonDataKey.SUB_TOTAL_ITEM].ToString())) : "0", 
                                        new { @readonly = true, @style="width:130px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Other
                                </td>
                                <td class="input">
                                    <%= Html.TextBox("OtherCost", ViewData.Model != null ? obj.OtherCost.ToString() :
                                        "0", new { @maxlength = "10", @style = "width:130px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px;">
                                    <%=Html.DropDownList("SaleTaxName", null, Constants.FIRST_ITEM_SALE_TAX, new { @style = "width:105px"})%>
                                    <span style="color: Red">*</span>
                                </td>
                                <td class="input">
                                    <%
                                        if (ViewData.Model == null)
                                        {
                                            Response.Write(Html.TextBox("SaleTaxValue", "0",
                                                new { @maxlength = "10", @style = "width:130px" }));
                                        }
                                        else
                                        {
                                            if (obj.SaleTaxName == "0")//case select no sale tax
                                            {
                                                Response.Write(Html.TextBox("SaleTaxValue", "0",
                                                    new { @disabled = "disabled", @maxlength = "10", @style = "width:130px" }));
                                            }
                                            else
                                            {                                                
                                                System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();                                                
                                                nfi.CurrencySymbol = "";
                                                string taxValue = string.Format(nfi, "{0:C}", obj.SaleTaxValue.Value);
                                                
                                                Response.Write(Html.TextBox("SaleTaxValue", obj.SaleTaxValue.HasValue ?
                                                    taxValue : "0",
                                                    new { @maxlength = "10", @style = "width:130px" }));
                                            }
                                        }%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Shipping
                                </td>
                                <td class="input">
                                    <%= Html.TextBox("Shipping", ViewData.Model != null ? obj.Shipping.HasValue ? 
                                        obj.Shipping.Value.ToString() : "0" : "0", 
                                        new { @maxlength = "10", @style = "width:130px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Discount
                                </td>
                                <td class="input">
                                    <%= Html.TextBox("Discount", ViewData.Model != null ? obj.Discount.HasValue ? 
                                        obj.Discount.Value.ToString() : "0" : "0", 
                                        new { @maxlength = "10", @style = "width:130px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Service Charge
                                </td>
                                <td class="input">
                                    <%= Html.TextBox("ServiceCharge", ViewData.Model != null ? obj.ServiceCharge.HasValue ?
                                        obj.ServiceCharge.Value.ToString() : "0" : "0", 
                                        new { @maxlength = "10", @style = "width:130px" })%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <span style="color: #ccc">-----------------------------------------------</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="label" style="width: 150px;">
                                    Total
                                </td>
                                <td class="input">
                                    <input type="text" id="Total" readonly="readonly" style="width: 130px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr style="height: 10px">
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="label required" style="width: 120px;">
                        Resolution<span>*</span>
                    </td>
                    <td class="input">
                        <%=Html.DropDownList("WFResolutionID", null, Constants.PURCHASE_REQUEST_RESOLUTION_LABEL, new { @style = "width:195px" })%>
                    </td>
                    <td class="label" style="width: 120px;">
                        Status
                    </td>
                    <td class="input">
                        <%=Html.DropDownList("WFStatusID",null, new  {@style="width:150px" })%>
                    </td>
                </tr>
                <tr id="trAssign">
                    <td class="label required">
                        Forward To<span>*</span>
                    </td>
                    <td class="input" colspan="3">
                        <%=Html.DropDownList("Assign", Constants.PURCHASE_REQUEST_APPROVAL_MAN)%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div id="list" style="width: 1050px">
    <div class="clrfix">
    </div>
    <div style="width: 1050px">
        <%if (ViewData[CommonDataKey.PR_COMMENT] != null)
          {
              List<PurchaseComment> listComment = (List<PurchaseComment>)ViewData[CommonDataKey.PR_COMMENT];
              if (listComment.Count > 0)
              { %>
        <h2 class="heading">
            Comment(s)</h2>
        <div style="height: 170px; overflow-y: scroll; overflow-x: hidden;" class="view_comment">
            <table border="0" cellpadding="0" cellspacing="0" class="tb_comment">
                <%
int i = 0;
foreach (PurchaseComment item in listComment)
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
                        <%= Html.Encode(item.Contents.Replace("\n", "<br />"))%>
                        <br />
                        <%= CommonFunc.SplitFileName(item.Files, Constants.PERFORMANCE_REVIEW_PATH, false)%>
                    </td>
                </tr>
                <%
i++;
                }
                %>
            </table>
        </div>
        <%  }
              } %>
        <h2 class="heading">
            New comment</h2>
        <table border="0" cellpadding="0" cellspacing="0" class="edit" width="1050px">
            <tr>
                <td style="width: 40%">
                    <%= Html.Hidden("RequestId", (ViewData.Model != null ? obj.ID.ToString():""))%>
                    <%= Html.TextArea("Contents", "", new { @Style = "width: 500px;height:46px",@maxlength="500" })%>
                </td>
                <td valign="top" style="width: 40%">
                    <table id="tblUpload">
                        <tr>
                            <td>
                                <input type="file" name="file" />
                                &nbsp;
                                <button type="button" id="btnAddFile" class="icon plus" title="Add New Upload">
                    </button>
                    <button type="button" id="btnRemoveFile" class="icon minus" title="Remove">
                    </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="file" name="file" />
                            </td>
                        </tr>
                    </table>
                </td>
               
            </tr>
        </table>
    </div>
</div>
<table cellspacing="0" cellpadding="0" border="0" width="1050px" class="form profile">
    <tr>
        <td align="center" colspan="6" style="padding-top: 20px">
            <input type="submit" class="save" value=""  />
            <input type="button" class="cancel" id="btnCancel" value="" />
        </td>
    </tr>
</table>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var row_ID = $("#tbl_purchase >tbody >tr").length;
    var subtotal = parseFloat($("#SubTotal").val().replace(/,/g, ""));
    var other = parseFloat($("#OtherCost").val());
    var tax = parseFloat(convertCurrencyToFloat($("#SaleTaxValue").val()))
    //var tax = parseFloat($("#SaleTaxValue").val());
    var shipping = parseFloat($("#Shipping").val());
    var discount = parseFloat($("#Discount").val());
    var serviceCharge = parseFloat($("#ServiceCharge").val());
    var tax_us = '<%= Constants.TAX_US %>';
    var tax_vn = '<%= Constants.TAX_VN %>';
    
    function AddRowComment() {
        $("#tblUpload").append($("#tblUpload tr:last").clone());
        var tr = $("#tblUpload").find("tr");
        var row = tr.length;
        if (row > 4) {
            $("#btnAddFile").removeClass();
            $("#btnAddFile").addClass("icon plus_off");            
        }
        if (row > 1) {
            $("#btnRemoveFile").removeClass();
            $("#btnRemoveFile").addClass("icon minus");
        } 
    }
    function RemoveRowComment() {
        $("#tblUpload tr:last").remove();
        var tr = $("#tblUpload").find("tr");
        var row = tr.length;
        if (row < 2) {
            $("#btnRemoveFile").removeClass();
            $("#btnRemoveFile").addClass("icon minus_off");            
        } 
        if (row <5) {
            $("#btnAddFile").removeClass();
            $("#btnAddFile").addClass("icon plus");            
        }
    }
    $('#btnAddFile').click(function () {
        var tr = $("#tblUpload").find("tr");
        var row = tr.length;
        if (row >= 5) {
            return false;
        }
        else {
            AddRowComment();
        }
    });
    $('#btnRemoveFile').click(function () {
        var tr = $("#tblUpload").find("tr");
        var row = tr.length;
        if (row < 2) {
            return false;
        }
        else {
            RemoveRowComment();
        }
    });
    function AddValidate() {
        $(".temp_description").rules("add", "required");
        $("#<%=CommonDataKey.DDL_PR_RESOLUTION%>").rules("add", { required: true });
        $("#<%=CommonDataKey.DDL_PR_ASSIGN%>").rules("add", { required: true });
        $("#<%=CommonDataKey.DDL_PR_STATUS%>").rules("add", { required: true });
        $(".temp_quantity").bind("blur", function () {
            checkQuanlity($(this));
        });
        $(".temp_price").bind("blur", function () {
            checkPrice($(this));
        });
    }

    function checkQuanlity(obj) {
        var index = $(obj).attr("id").substring(8); //sub string char "quantity"
        var temp_obj = $(obj).parent().parent().find("#temp_total" + index);
        var oldMoney = temp_obj.val();
        if ($(obj).val() != "") {
            if (isNaN($(obj).val())) {
                $(obj).val(0);
                setSubTotal(oldMoney, 0, temp_obj, false);
            }
            else if ($(obj).val() < 0) {
                $(obj).val(0);
                setSubTotal(oldMoney, 0, temp_obj, false);
            }
            else {
                $(obj).val(Math.round($(obj).val() * 100) / 100);
                var nextValue = $(obj).parent().parent().find("#price" + index).val();
                if (nextValue != "") {
                    var money = Math.round($(obj).val() * nextValue * 100) / 100;
                    setSubTotal(oldMoney, money, temp_obj, true);
                }
            }
        }
        else {
            $(obj).val(0);
            setSubTotal(oldMoney, 0, temp_obj, false);
        }
    }

    function checkPrice(obj) {
        var index = $(obj).attr("id").substring(5); //sub string char "quantity"
        var temp_obj = $(obj).parent().parent().find("#temp_total" + index);
        var oldMoney = temp_obj.val();
        if ($(obj).val() != "") {
            if (isNaN($(obj).val())) {
                $(obj).val(0);
                setSubTotal(oldMoney, 0, temp_obj, false);
            }
            else if ($(obj).val() < 0) {
                $(obj).val(0);
                setSubTotal(oldMoney, 0, temp_obj, false);
            }
            else {
                $(obj).val(Math.round($(obj).val() * 100) / 100);
                var nextValue = $(obj).parent().parent().find("#quantity" + index).val();
                if (nextValue != "") {
                    var money = Math.round($(obj).val() * nextValue * 100) / 100;
                    setSubTotal(oldMoney, money, temp_obj, true);
                }
            }
        }
        else {
            $(obj).val(0);
            setSubTotal(oldMoney, 0, temp_obj, false);
        }
    }

    function setSubTotal(oldInt, newInt, obj, empty) {
        oldInt = oldInt.replace(/,/g, "");
        if (empty == true) {
            if (oldInt != "") {
                if (oldInt != newInt) {
                    if (oldInt > newInt) {
                        subtotal = subtotal - (oldInt - newInt);
                    }
                    else {
                        subtotal = subtotal + (newInt - oldInt);
                    }
                }
            }
            else {
                subtotal += newInt;
            }
            obj.val(formatCurrency(newInt));
//            obj.val(newInt);
        }
        else {
            obj.val(0);
            if (oldInt != "") {
                subtotal -= oldInt;
            }
        }
        subtotal = Math.round(subtotal * 1000) / 1000;
        $("#SubTotal").val(formatCurrency(subtotal));
//        $("#SubTotal").val(subtotal);
        $("#SaleTaxName").change();
        SetTotal();
    }
    function AddRow() {
        var value = row_ID + 1;
        $("#tbl_purchase").append(
                    "<tr id='row" + value + "'>" +
                        "<td>" + value + "</td>" +
                        "<td ><input   id='description" + value + "' name='description" + value + "' type='text' style='width:150px;' maxlength='100'  /></td>" +
                        "<td ><input value='0' name='quantity" + value + "'  id='quantity" + value + "' type='text' maxlength='5' style='width:80px;' /></td>" +
                        "<td ><input value='0' name='price" + value + "' id='price" + value + "' type='text' maxlength='10' style='width:80px;'/></td>" +
                        "<td ><input value='0' class='temp_total' id='temp_total" + value + "' type='text' readonly='readonly' /></td>" +
                    "</tr");
        $("#quantity" + value).bind("blur", function () {
            checkQuanlity($("#quantity" + value));
        });
        $("#price" + value).bind("blur", function () {
            checkPrice($("#price" + value));
        });
        $("#description" + value).rules("add", "required");
        $("#quantity" + value).rules("add", "required");
        $("#price" + value).rules("add", "required");
        row_ID++;
        $("#hidItem").val(row_ID);
    }
    function SetPrefix() {
        $("#Type").find("input[type='radio']").each(function () {
            if ($(this).is(":checked") == true) {
                var type_Value = $(this).parent().attr("hidValue");
                prefix = type_Value;
            }
        });
    }
    function RemoveRow() {
        if (row_ID != 1) {
            var oldValue = $("#tbl_purchase tr:last").find(".temp_total").val();
            subtotal -= oldValue;
            subtotal = Math.round(subtotal * 100) / 100;
            $("#tbl_purchase tr:last").remove();
            $("#SubTotal").val(subtotal);
            SetTotal();
            row_ID--;
            $("#hidItem").val(row_ID);
        }
    }
    function SetTotal() {
        var value = Math.round(parseFloat(subtotal + other + shipping + tax + serviceCharge - discount) * 100) / 100;
        var prefix;
        $("#Type").find("input[type='radio']").each(function () {
            if ($(this).is(":checked") == true) {
                var type_Value = $(this).parent().attr("hidvalue");
                prefix = type_Value;
            }
        });
        $("#Total").val(formatCurrency(value) + " " + prefix);
//        $("#Total").val(value + " " + prefix);
        $("#hidItem").val(row_ID);
    }

    function CalculateTax() {
        var value = $("#SaleTaxName").val();
        if (value != "0")//case if select no sale tax
        {
            if (value == "1")
                tax = Math.round(parseFloat(subtotal + other) * parseFloat(tax_us)) / 100;
            else if(value == "2")
                tax = Math.round(parseFloat(subtotal + other) * parseFloat(tax_vn)) / 100;
            $('#SaleTaxValue').val(formatCurrency(tax));
        }
    }

    function formatCurrency(num) {
        num = parseFloat(num);
        var main = num.toString();
        var plus = "";
        if (main.lastIndexOf('.') != -1) {
            plus = main.substring(main.lastIndexOf('.'), main.length);
            main = main.substring(0, main.lastIndexOf('.'));
        }
        if (main.length <= 3)
            return main + plus;
        var arrPart = new Array();
        for (i = main.length; i > 0; i--) {
            if (main.length > 3) {
                arrPart.push(main.substr(main.length - 3, 3));
                main = main.substring(0, main.length - 3);
            }
            else {
                arrPart.push(main);
                main = "";
                break;
            }
        }
        return arrPart.reverse().join(',') + plus;
    }
    function ShowMessage() {
        if (!$("#purchaseForm").valid()) {
            CRM.msgBox(I0009, 500);
            return false;
        }
        else {
            $("form").submit();
            return false;
        }

    }
    function isNumberOrCurrency(value)
    {
        ischeck = true;
        num = value.replace(/,/gi,"");
        if(isNaN(num) || num < 0)
        {
            ischeck = false;
        }
        return ischeck;
    }

    function convertCurrencyToFloat(value)
    {
        return value.replace(/,/g,"");
    }
    $(document).ready(function () {
        $("#btnCancel").click(function () {

            var returnUrl = document.referrer;
            if (returnUrl != "") {
                window.location = returnUrl;
            }
            else {
                window.location = "/PurchaseRequestUS/";
            }
        });
        $("#ExpectedDate").datepicker({
            onClose: function () { $(this).valid(); }
        });
        /*$("#RequestDate").datepicker({
            onClose: function () { $(this).valid(); }
        });*/
        $("#fieldCC").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=WorkFlow&workflowID=<%= Constants.WORK_FLOW_PURCHASE_REQUEST%>', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CCList", employee: true });
        $("#OtherCost").blur(function () {
            if ($(this).val() != "") {
                if (isNaN($(this).val())) {
                    $(this).val(0);
                    other = 0;
                    CalculateTax();
                    SetTotal();
                }
                else if ($(this).val() < 0) {
                    $(this).val(0);
                    other = 0;
                    CalculateTax();
                    SetTotal();
                }
                else {
                    other = Math.round(parseFloat($(this).val() * 100)) / 100;
                    $(this).val(other);
                    CalculateTax();
                    SetTotal();
                }
            }
            else {
                $(this).val(0);
                other = 0;
                CalculateTax();
                SetTotal();
            }
        });

        $("#SaleTaxValue").blur(function () {
            if ($(this).val() != "") {

                if (isNumberOrCurrency($(this).val())==false) {
                    $(this).val(0);
                    tax = 0;
                    SetTotal();
                }
                else {
                    //tax = Math.round(parseFloat($(this).val() * 100)) / 100;
                    tax = Math.round(parseFloat(convertCurrencyToFloat($(this).val()) * 100)) / 100;
                    $(this).val(formatCurrency(tax));                    
                    SetTotal();
                }
            }
            else {
                $(this).val(0);
                tax = 0;
                SetTotal();
            }
        });

        $("#Shipping").blur(function () {
            if ($(this).val() != "") {
                if (isNaN($(this).val())) {
                    $(this).val(0);
                    shipping = 0;
                    SetTotal();
                }
                else if ($(this).val() < 0) {
                    $(this).val(0);
                    shipping = 0;
                    SetTotal();
                }
                else {
                    shipping = Math.round(parseFloat($(this).val() * 100)) / 100;
                    $(this).val(shipping);
                    SetTotal();
                }
            }
            else {
                $(this).val(0);
                shipping = 0;
                SetTotal();
            }
        });
        $("#Discount").blur(function () {
            if ($(this).val() != "") {
                if (isNaN($(this).val())) {
                    $(this).val(0);
                    discount = 0;
                    SetTotal();
                }
                else if ($(this).val() < 0) {
                    $(this).val(0);
                    discount = 0;
                    SetTotal();
                }
                else {
                    discount = Math.round(parseFloat($(this).val() * 100)) / 100;
                    $(this).val(discount);
                    SetTotal();
                }
            }
            else {
                $(this).val(0);
                discount = 0;
                SetTotal();
            }
        });
        $("#ServiceCharge").blur(function () {
            if ($(this).val() != "") {
                if (isNaN($(this).val())) {
                    $(this).val(0);
                    serviceCharge = 0;
                    SetTotal();
                }
                else if ($(this).val() < 0) {
                    $(this).val(0);
                    serviceCharge = 0;
                    SetTotal();
                }
                else {
                    serviceCharge = Math.round(parseFloat($(this).val() * 100)) / 100;
                    $(this).val(serviceCharge);
                    SetTotal();
                }
            }
            else {
                $(this).val(0);
                serviceCharge = 0;
                SetTotal();
            }
        });
        AddValidate();
        SetTotal();
        $("#Type").find("input[type='radio']").click(function () {
            SetTotal();
        });
        $("#DepartmentName").change(function () {
            $("#SubDepartment").html("");
            var department = $("#DepartmentName").val();
            $("#SubDepartment").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
            $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                $.each(item, function () {
                    if (this['ID'] != undefined) {
                        $("#SubDepartment").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                    }
                });
            });
        });

        $("input[name='chk_BillableGroup']").change(function () {
            var value = $("input[name='chk_BillableGroup']:checked").val();
            if (value != "") {
                $("#txt_BillableGroup").val(value);
                $("span[htmlfor=txt_BillableGroup]").remove();
            }
        });

        
        $("#SaleTaxName").change(function () {
            var value = $("#SaleTaxName").val();
            if (value == "0" || value == "")//case if select no sale tax
            {
                $("#SaleTaxValue").val(0);
                tax = 0;
                SetTotal();
                $("#SaleTaxValue").attr("disabled", "disabled");
            }
            else if (value != "") {
                CalculateTax();
                $("#SaleTaxValue").attr("disabled", "");
                SetTotal();
            }
        });

        $("#WFResolutionID").change(function () {
            var oldValue = $("#RequestorId").val() + "@" + $("#UserRole").val();
            var resolutionId = $("#WFResolutionID").val();
            if (resolutionId != 0) {
                $("#WFStatusID").html("");
                $("#Assign").html("");

                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Status', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#WFStatusID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }

                    });
                });
                if (resolutionId != '<%= Constants.PRUS_RESOLUTION_CANCELLED %>') {
                    $("#Assign").rules("add", "required");
                    $("#trAssign").css("display", "");
                    var sParam = "";
                    <%
                        if(ViewData.Model != null && ViewData[CommonDataKey.TO_GROUP] != null)    
                        {
                            PurchaseRequest pur = ViewData.Model as PurchaseRequest;
                    %>
                        sParam = "&toGroup=<%=(bool)ViewData[CommonDataKey.TO_GROUP]%>&roleId=<%=pur.AssignRole%>";
                    <%
                        }
                    %>
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId +
                        '&Page=PurChaseAssignUS' + sParam, function (item) {
                            $.each(item, function () {
                                if (this['ID'] != undefined) {
                                    if (this['ID'] == oldValue || this['ID'] == "<%=selectedRequestor%>") {
                                        $("#Assign").append($("<option value='" + this['ID'] + "' selected>" + this['Name'] + "</option>"));
                                    }
                                    else {
                                        $("#Assign").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                                    }
                                }
                            });
                        });

                }
                else {
                    $("#trAssign").css("display", "none");
                    $("#Assign").rules("remove");
                }
            }
            else {
                //$("#<%=CommonDataKey.DDL_PR_STATUS%>").html("<option value=''><%=Constants.PURCHASE_REQUEST_STATUS_FIRST_ITEM%></option>");
                $("#<%=CommonDataKey.DDL_PR_ASSIGN%>").html("<option value=''><%=Constants.PURCHASE_REQUEST_APPROVAL_MAN%></option>");
            }
        });                      
    });    
</script>
