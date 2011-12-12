<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Register Src="UCInterview.ascx" TagName="UCInfo" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar + HiringCenterPageInfo.FuncSetupInterview + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    <% Response.Write(HiringCenterPageInfo.ComName); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<form id="editForm" action="<%= Url.Action("Create", "Interview")%>"
method="post">
            <uc1:UCInfo ID="UCInfo1" runat="server" />
        </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server"> 
<script type="text/javascript">
    var id = 0;
    $(document).ready(function () {
        $("#editForm").submit(function () {
            $("#txtNumber").val(row_id);
            if (id == 0) {
                $(this).ajaxSubmit({
                    dataType: "json",
                    type: "post",
                    beforeSubmit: function () {
                        var isValid = $("#editForm").valid();
                        return isValid;
                    },
                    success: function (result) {
                        if (result.MsgType == 1) {
                            CRM.message(result.MsgText, 'block', 'msgError');
                        }
                        else {
                            if (result.Holders[1] != "") {
                                CRM.popup("/Interview/ActionSendMail/" + result.Holders[1], "Send Email, Meeting Request", 600);
                                CRM.summary("" + result.Holders[0] + "", "", "msgSuccess");
                                id++;
                            }
                            else {
                                window.location = "/Interview/Index";
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
        funcTitle = HiringCenterPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + "<a href='/Candidate/'>" + HiringCenterPageInfo.ModInterviewHistory + "</a> » " +
            "<a href='/Candidate/Detail/" + can.ID + "'>" +
            can.FirstName + " " + can.MiddleName + " " + can.LastName + "</a> » " + HiringCenterPageInfo.FuncSetupInterview;
        //funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + "<a href='/Candidate/Detail/" + can.ID + "'>" +
        //   can.FirstName + " " + can.MiddleName + " " + can.LastName + "</a> » " + HiringCenterPageInfo.FuncSetupInterview;;
    }
    else
    {
        Response.Redirect("/Candidate");
    }
     %>
    <%= funcTitle%>
</asp:Content>
