<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCInterview.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar +HiringCenterPageInfo.FuncEditCandidate  + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%> 
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>        
        <%using (Html.BeginForm("Edit", "Interview", FormMethod.Post, new { @id = "editForm", @class = "form" }))
         { %>
            <uc1:UCInfo ID="UCInfo1" runat="server" />
        
        <%  } %> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">   
<script type="text/javascript">
    $(document).ready(function () {
        $("#editForm").submit(function () {
            var id = 0;
            $("#txtNumber").val(row_id);
            if (id == 0) {
                $(this).ajaxForm({
                    dataType: "json",
                    type: "post", cache: true,
                    beforeSubmit: function () {
                        return $("#editForm").valid();
                    },
                    success: function (result) {
                        if (result.MsgType == 1) {
                            CRM.message(result.MsgText, 'block', 'msgError');
                        }
                        else {
                            id++;
                            if (result.Holders[1] != "") {                               
                                CRM.popup("/Interview/ActionSendMail/" + result.Holders[1], "Send Email, Meeting Request", 600);
                                CRM.summary("" + result.Holders[0] + "", "block", "msgSuccess");                               
                            }
                            else {
                                window.location = "/Interview";
                            }
                        }
                    }
                });
                
            }
            return false;
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%
    Candidate can = (Candidate)ViewData.Model;
    string funcTitle = string.Empty;
    if (can != null)
    {
        //funcTitle = HiringCenterPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + "<a href='/Interview/'>" + HiringCenterPageInfo.ModInterview + "</a> » " +
        //    "<a href='/Interview/Detail/" + can.ID + "'>" +
        //    can.FirstName + " " + can.MiddleName + " " + can.LastName + "</a> » " + HiringCenterPageInfo.FuncEditInterview;
        funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + "<a href='/Interview/Detail/" + can.ID + "'>" +
            can.FirstName + " " + can.MiddleName + " " + can.LastName + "</a> » " + HiringCenterPageInfo.FuncEditInterview;
    }
    else
    {
        Response.Redirect("/Interview");
    }
     %>
    <%= funcTitle%>
</asp:Content>
