<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%using (Html.BeginForm("PrintReview", "PurchaseRequest", FormMethod.Post, new { id = "frmPrintReview", @class = "form" }))
  {
      sp_GetPurchaseRequestResult request = (sp_GetPurchaseRequestResult)ViewData.Model;
%>
<script type="text/javascript">
    function printPartOfPage(elementId) {
        var printContent = document.getElementById(elementId);
        var windowUrl = 'about:blank';
        var uniqueName = new Date();
        var windowName = 'Print' + uniqueName.getTime();
        window.location = "/PurchaseRequest/Detail/<%=request.ID %>";
        var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');

        printWindow.document.write(printContent.innerHTML);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
        //alert(1);

    }
</script>
<div style="width: 870px;">
    <div align="right" id="cactionbutton">
        <button title="Print" class="button print" onclick="printPartOfPage('MyDiv'); return false;">
            Print</button>
    </div>
    <div id="MyDiv" style="width: 870px; height: 600px; overflow-y: scroll">
        <%= Html.Hidden("ID", request.ID)%>
        <table cellspacing="1" cellpadding="1" border="0" width="870px" style="border-bottom-style: solid">
            <tr>
                <img alt="Logigear Logo" width="210px" height="90px" src="../../Content/Images/LogiGear_logo.jpg" />
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-right: 10px; min-width: 200px;" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-right: 10px; min-width: 200px;" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; padding-right: 10px; min-width: 200px;" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <%=ViewData["template"].ToString()%>
                </td>
            </tr>
        </table>
    </div>
</div>
<%} %>
