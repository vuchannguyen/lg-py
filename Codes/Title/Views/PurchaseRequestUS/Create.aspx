<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCInfo.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%using (Html.BeginForm("Create", "PurchaseRequestUS", FormMethod.Post, new { id = "purchaseForm", @class = "form", enctype = "multipart/form-data" }))
  { %>
<uc1:UCInfo ID="UCInfo1" runat="server" />
<% } %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= PurchaseRequestPageInfo.FuncAddNewPurchaseRequest + CommonPageInfo.AppSepChar + PurchaseRequestPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    var indexSubmit = 0;
    
    $(document).ready(function () {
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
            rules: {
                SubDepartment: { required: true },
                DepartmentName: { required: true },
                Justification: { required: true },
                txt_BillableGroup: { required: true },                
                PRPayMethod: { required: true },
                VendorEmail: { email: true },
                SaleTaxName: { required: true },
                OtherCost: { required: true },
                RequestDate: {
                    required: true,
                    checkDate: true
                },
                ExpectedDate: {
                    required: true,
                    checkDate: true,
                    compareDate: ["#purchaseForm input[name='RequestDate']", "get", "Expected Date", "Request Date"]
                }
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
    });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=PurchaseRequestPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
   <%=  CommonFunc.GetCurrentMenu(Request.RawUrl)  + PurchaseRequestPageInfo.FuncAddNewPurchaseRequest%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
