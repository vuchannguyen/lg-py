<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewRnD.ascx" TagName="UC1" TagPrefix="uc1" %>    
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewManager.ascx" TagName="UC2" TagPrefix="uc2" %>
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewIT.ascx" TagName="UC3" TagPrefix="uc3" %>    
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewAdmin.ascx" TagName="UC4" TagPrefix="uc4" %> 
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewEngineerService.ascx" TagName="UC5" TagPrefix="uc5" %>    
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRView60dayReview.ascx" TagName="UC6" TagPrefix="uc6" %>    
<%@ Register Src="../EForm/PerformanceReviewForm/UCPRViewForm.ascx" TagName="UC7" TagPrefix="uc7" %>
<div id="cactionbutton">
    <button type="reset" id="btnRefresh" title="Refresh" class="button refresh">Refresh</button>
</div>
<% 
EmployeeDao empDao = new EmployeeDao();
PerformanceReview pr = (PerformanceReview)ViewData.Model; %>
<div class="form">
    <div class="profile" style="height: 30px;">
        <div class="ctbox">
            <h2>
                Performance Review Information</h2>
        </div>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="view" id="list" width="1024px">
        <tr>
            <td class="label">
                Performance Review ID
            </td>
            <td class="input">
                <%=pr.ID%>
            </td>
            <td class="label">
                Status
            </td>
            <td class="input">
                <%=pr.WFStatus.Name%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Forwarded To
            </td>
            <td class="input">
                <%=empDao.FullName(pr.AssignID, Constants.FullNameFormat.FirstMiddleLast) + " (" + pr.WFRole.Name + ")"%>
            </td>
            <td class="label">
                Resolution
            </td>
            <td class="input">
                <%=pr.WFResolution.Name%>
            </td>
        </tr>
            
        <tr>
            <td class="label" style="width: 120px">
                Employee name
            </td>
            <td class="input" style="width: 150px">
                <%=empDao.FullName(pr.EmployeeID, Constants.FullNameFormat.FirstMiddleLast)%>
            </td>
            <td class="label" style="width: 120px">
                Employee ID
            </td>
            <td class="input" style="width: 150px">
                <%=pr.EmployeeID %>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                Manager name
            </td>
            <td class="input" style="width: 150px">
                <%=empDao.FullName(pr.ManagerID, Constants.FullNameFormat.FirstMiddleLast)%>
            </td>
            <td class="label" style="width: 120px">
                Manager ID
            </td>
            <td class="input" style="width: 150px">
                <%=pr.ManagerID %>
            </td>
        </tr>
        <tr>
            <td class="label" style="width: 120px">
                PR Date
            </td>
            <td class="input" style="width: 150px">
                <%=pr.PRDate.ToString(Constants.DATETIME_FORMAT_VIEW)%>
            </td>
            <td class="label" style="width: 120px">
                Next Review Date
            </td>
            <td class="input" style="width: 150px">
                <%=pr.NextReviewDate.HasValue ? 
                    pr.NextReviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : "" %>
            </td>
        </tr>
        <tr>
            <td class="label">
                CC List
            </td>
            <td class="input" colspan="3">
                <%=string.IsNullOrEmpty(pr.CCEmail) ? "" : pr.CCEmail%>
            </td>
        </tr>
        <tr>
            <td class="label" style="vertical-align:top">
                History
            </td>
            <td colspan="3" class="input" style="border-right: 1px solid #CCCCCC;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="grid">
                    <thead>
                        <tr>
                            <th class="gray">
                                Name
                            </th>
                            <th class="gray">
                                Action
                            </th>
                            <th class="gray">
                                Date
                            </th>
                        </tr>
                    </thead>
                    <%
                        string[] arrInvoleId = pr.InvolveID.TrimEnd(',').Split(',');
                        string[] arrInvoleRole = pr.InvolveRole.TrimEnd(',').Split(',');
                        string[] arrInvoleRes = pr.InvolveResolution.TrimEnd(',').Split(',');
                        string[] arrInvoleDate = pr.InvolveDate.TrimEnd(',').Split(',');
                        RoleDao roleDao = new RoleDao ();
                        string newRow = "<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>";
                        for (int i = 0; i < arrInvoleId.Length; i++)
                        {
                            string sName = empDao.FullName(arrInvoleId[i], Constants.FullNameFormat.FirstMiddleLast) + 
                                " (" + roleDao.GetByID(int.Parse(arrInvoleRole[i])).Name + ")";
                            string sAction = arrInvoleRes[i];
                            string sDate = DateTime.Parse(arrInvoleDate[i]).ToString(Constants.DATETIME_FORMAT_JR);
                            Response.Write(string.Format(newRow, sName, sAction, sDate));
                        }
                        Response.Write(string.Format(newRow, empDao.FullName( pr.Employee.ID, 
                            Constants.FullNameFormat.FirstMiddleLast) + " (" + 
                            roleDao.GetByID(pr.AssignRole).Name + ")", "", ""));
                    %>
                    <tr>
                                        
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div class="clrfix"> 
    </div>
    <div style="width:1024px">
        <% 
            List<PRComment> commentList = ViewData[CommonDataKey.PER_REVIEW_COMMENT] as List<PRComment>;
            if (commentList.Count > 0)
            {
                
        %>
        <h2 class="heading">
            Comment(s)</h2>
        <div style="height: 170px; overflow-y: scroll; overflow-x: hidden;" class="view_comment">
            <table border="0" cellpadding="0" cellspacing="0" class="tb_comment">
                <%
int i = 0;
foreach (var item in commentList)
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
                    </td>
                </tr>
                <%
i++;
}
                %>
            </table>
        </div>
        <%  } %>
    </div>
</div>
<br />
<div style="width:1024px;" class="form">    
<% if (ViewData["eformId"].ToString() == "PR-1") %>
<% { %>
<div style="width: 1024px">
    <uc1:uc1 ID="UCR11" runat="server" />
</div>
<% }
    else if (ViewData["eformId"].ToString() == "PR-2") %>
<%  { %>
<div style="width: 1024px">
    <uc2:uc2 ID="UCR21" runat="server" />
</div>
<% }
    else if (ViewData["eformId"].ToString() == "PR-3") %>
<% { %>
<div style="width: 1024px">
    <uc3:uc3 ID="UCR31" runat="server" />
</div>
<% }
    else if (ViewData["eformId"].ToString() == "PR-4") %>
<% { %>
<div style="width: 1024px">
    <uc4:uc4 ID="UCR41" runat="server" />
</div>
<% }
    else if (ViewData["eformId"].ToString() == "PR-5") %>
<% { %>
<div style="width: 1024px">
    <uc5:uc5 ID="UCR51" runat="server" />
</div>    
<% }
    else if (ViewData["eformId"].ToString() == "PR-6") %>
<% { %>
<div style="width: 1024px">
    <uc6:uc6 ID="UCR61" runat="server" />
</div>
<% }
    else if (ViewData["eformId"].ToString() == "PR-7") %>
<% { %>
<div style="width: 1024px">
    <uc7:uc7 ID="UCR71" runat="server" />
</div>
<% } %>
<br />
</div>