<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Src="../EForm/InterviewForm/UCInterViewR1.ascx" TagName="UCInterViewR1"
    TagPrefix="uc1" %>
<%@ Register Src="../EForm/InterviewForm/UCInterviewR2.ascx" TagName="UCInterviewR2"
    TagPrefix="uc2" %>
<%@ Register Src="../EForm/InterviewForm/UCInterviewR3.ascx" TagName="UCInterviewR3"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form">
        <div class="profile" style="height: 30px;">
            <div class="ctbox">
                <h2>
                    Candidate Information</h2>
            </div>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" class="view" width="1024px">
            <tr>
                <td class="label" style="width: 120px">
                    Full name
                </td>
                <td class="input" style="width: 150px">
                    <span class="color_green_bold">
                        <%=Html.Label(((Candidate)ViewData.Model).FirstName + " " + ((Candidate)ViewData.Model).MiddleName + " " + ((Candidate)ViewData.Model).LastName)%></span>
                </td>
                <td class="label" style="width: 100px">
                    VN Name
                </td>
                <td class="input" style="width: 200px">
                    <span class="color_green_bold">
                        <%=Html.Label(((Candidate)ViewData.Model).VnFirstName + " " + ((Candidate)ViewData.Model).VnMiddleName + " " + ((Candidate)ViewData.Model).VnLastName)%></span>
                </td>
                <td colspan="2" class="label" style="width: 300px">
                </td>
            </tr>
            <tr>
                <td class="label">
                    Date of Birth
                </td>
                <td class="input">
                    <%=((Candidate)ViewData.Model).DOB.HasValue?((Candidate)ViewData.Model).DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):""%>
                </td>
                <td class="label">
                    Telephone
                </td>
                <td class="input" style="width: 100px">
                    <%= !string.IsNullOrEmpty(((Candidate)ViewData.Model).CellPhone) ? ((Candidate)ViewData.Model).CellPhone : Constants.NODATA%>
                </td>
                <td class="label" style="width: 90px">
                    Email
                </td>
                <td class="input" style="width: 180px">
                    <%= !string.IsNullOrEmpty(((Candidate)ViewData.Model).Email) ? ((Candidate)ViewData.Model).Email : Constants.NODATA%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    University
                </td>
                <td colspan="5" class="input">
                    <%= ((Candidate)ViewData.Model).UniversityId.HasValue ? ((Candidate)ViewData.Model).University.Name : Constants.NODATA%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Address
                </td>
                <td colspan="5" class="input">
                    <%= !string.IsNullOrEmpty(((Candidate)ViewData.Model).Address) ? ((Candidate)ViewData.Model).Address : Constants.NODATA%>
                </td>
            </tr>
            <tr class="last">
                <td class="label">
                    Remarks
                </td>
                <td colspan="5" class="input">
                    <% =!string.IsNullOrEmpty(((Candidate)ViewData.Model).Note) ? ((Candidate)ViewData.Model).Note : Constants.NODATA%>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <%using (Html.BeginForm("ResultForm", "Interview", FormMethod.Post, new { @id = "editForm", @class = "form" }))
      { %>
    <% if (ViewData["InterviewForm"].ToString() == "INT-1") %>
    <% { %>
    <div style="width: 1024px">
        <uc1:UCInterViewR1 ID="UCInterViewR11" runat="server" />
    </div>
    <% }
       else if (ViewData["InterviewForm"].ToString() == "INT-2") %>
    <%  { %>
    <div style="width: 1024px">
        <uc2:UCInterviewR2 ID="UCInterviewR21" runat="server" />
    </div>
    <% }
       else if (ViewData["InterviewForm"].ToString() == "INT-3") %>
    <% { %>
    <div style="width: 1024px">
        <uc3:UCInterviewR3 ID="UCInterviewR31" runat="server" />
    </div>
    <% } %>
    <% Response.Write(Html.Hidden("Round", ViewData["Round"]));%>
    <% Response.Write(Html.Hidden("CandidateId", ((Candidate)ViewData.Model).ID));%>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" class="edit" width="1024px">
        <tr>
            <td width="100px" class="required label">
                Conclusion <span>*</span>
            </td>
            <td class="input">
                <table id="tblConclusion" width="100%">
                    <tr>
                        <td>
                            <%= Html.RadioButton("InterviewResultId", "1", ViewData["InterviewResultId"].ToString() == "1"?true:false)%>
                            &nbsp;Pass
                        </td>
                        <td>
                            <%= Html.RadioButton("InterviewResultId", "2", ViewData["InterviewResultId"].ToString() == "2"?true:false)%>
                            &nbsp;Fail
                        </td>
                        <td>
                            <%= Html.RadioButton("InterviewResultId", "3", ViewData["InterviewResultId"].ToString() == "3"?true:false)%>
                            &nbsp;Waiting List
                        </td>
                        <td>
                            <%= Html.RadioButton("InterviewResultId", "4", ViewData["InterviewResultId"].ToString() == "4"?true:false)%>
                            &nbsp;Recruit
                        </td>
                        <td>
                            <%= Html.RadioButton("InterviewResultId", "5", ViewData["InterviewResultId"].ToString() == "5"?true:false)%>
                            &nbsp;Absent
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="label">
                Note
            </td>
            <td class="input">
                <%
                    Response.Write(Html.TextArea("Note", ViewData["Note"] != null ? ViewData["Note"].ToString() : "", 2, 1, new { @style = "width:900px" }));
                %>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <div class="form" style="text-align: center">
                    <input type="submit" title="Save" class="save" value="" />
                    <% string javascript = "javascript:CRM.back()";
                       javascript = "window.location = '/Interview'";
                    %>
                    <input type="button" title="Cancel" onclick="<%=javascript %>" id="btnCancel" class="cancel"
                        value="" />
                </div>
            </td>
        </tr>
    </table>
    <% } %>
    <script type="text/javascript">

        $(document).ready(function () {
            $("input[type='radio']").click(function () {
                if ($(this).parent().parent().find("input[type='text']").attr("id") != undefined) {
                    $(this).parent().parent().find("input[type='text']").valid();
                }
            });
            $("#tblConclusion").find("td").click(function () {
                $(this).find("input[type=radio]").attr("checked", true);
            });
            $("#tblComment").find("td").click(function () {
                $(this).find("input[type=radio]").attr("checked", true);
            });
            $("#InterviewResultId").rules("add", "required");
            $("#Note").rules("add", { maxlength: 250 });
            $("#editForm").ajaxForm({
                dataType: "json", cache: true,
                beforeSubmit: function () {
                    return $("#editForm").valid();
                },
                success: function (result) {
                    if (result.MsgType == 1) {
                        CRM.message(result.MsgText, 'block', 'msgError');
                    }
                    else {
                        if (result.Holders[1] != "") {
                            CRM.popup("/Interview/ActionSendMail/" + result.Holders[1], "Send Email,Meeting Request", 600);
                            CRM.summary(result.Holders[0], "block", "msgSuccess");
                        }
                        else {
                            window.location = "/Interview";
                        }
                    }
                }
            });
        });          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.ModInterview + CommonPageInfo.AppSepChar + HiringCenterPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%
        if (ViewData.Model == null)
            Response.Redirect("/Interview");
        Candidate canObj = (CRM.Models.Candidate)ViewData.Model;  %>
       <%=  CommonFunc.GetCurrentMenu(Request.RawUrl) + Html.ActionLink(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName, "../Interview/Detail/" + canObj.ID)
                      + " » " + HiringCenterPageInfo.FuncInterviewResult%>
       

    <%--<a href='/Interview/'>
        <%=HiringCenterPageInfo.ModInterview%></a> »
    <% if (canObj != null) Response.Write(Html.ActionLink(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName, "../Interview/Detail/" + canObj.ID)); %>
    »
    <%=HiringCenterPageInfo.FuncInterviewResult%>--%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
