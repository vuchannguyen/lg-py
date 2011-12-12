<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
    <script type="text/javascript">
        hs.graphicsDir = '../../Scripts/highslide/graphics/';
        hs.outlineType = 'rounded-white';
    </script>
    <script type="text/javascript">
    <%= TempData["Message"]%>        
    /////////////////////////////////
    var row_id = <%=ViewData["Interview"] %>;  
        
    $(document).ready(function () {     
            /* Navigator */
            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/Interview/DetailHistory/" + $('#btnFirst').val();
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/Interview/DetailHistory/" + $('#btnPre').val();
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/Interview/DetailHistory/" + $('#btnNext').val();
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {
                    window.location = "/Interview/DetailHistory/" + $('#btnLast').val();
                }
            });
            /*---------------------*/                 
        
    });    

    </script>
    <% 
        if (ViewData.Model == null)
            Response.Redirect("/HistoryInterview");
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        int lastID = 0;
        int firstID = 0;
        int nextID = 0;
        int preID = 0;
        int number = 0;
        int total = 0;
        Candidate canObj = (CRM.Models.Candidate)ViewData.Model;
        List<int> intList = new List<int>();
        intList = (List<int>)ViewData["ListInter"];
        if (intList == null)
        {
            InterviewDao interviewDao = new InterviewDao();
            List<sp_GetInterviewHistoryListResult> InterviewList = interviewDao.GetHistoryInterviewList("", 0, 0, 0, null, null);
            var finalList = interviewDao.SortHistoryInterview(InterviewList, "ID", "desc");
            intList = finalList.Select(p => p.ID).ToList();
        }
        int index = 0;

        total = intList.Count;
        if (intList.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = intList.IndexOf(canObj.ID);
            if (index == 0)
            {
                styleFirst = "class=\"first first_off\"";
                stylePrev = "class=\"prev prev_off\"";
            }
            else if (index == intList.Count - 1)
            {
                styleLast = "class=\"last last_off\"";
                styleNext = "class=\"next next_off\"";
            }
            number = index + 1;
        }
        else if (intList.Count == 1)
        {
            number = intList.Count;
        }

        if (intList != null)
        {
            lastID = intList[intList.Count - 1];
            firstID = intList[0];

            if (index + 1 < intList.Count)
                nextID = intList[index + 1];
            else
                nextID = intList[intList.Count - 1];

            if (index - 1 == -1)
                preID = intList[0];
            else
                preID = intList[index - 1];
        }
    %>
    <div id="cnavigation" style="width: 1024px">
        <button type="button" id="btnLast" title="Last" value="<%=lastID %>" <%=styleLast %>>
        </button>
        <button type="button" id="btnNext" title="Next" value="<%=nextID %>" <%=styleNext %>>
        </button>
        <span>
            <%= number + " of " + total%></span>
        <button type="button" id="btnPre" title="Previous" value="<%=preID %>" <%=stylePrev %>>
        </button>
        <button type="button" id="btnFirst" title="First" value="<%=firstID%>" <%=styleFirst %>>
        </button>
    </div>
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
            <td class="input" style="width: 145px">
                <span class="color_green_bold">
                    <%=Html.Label(((Candidate)ViewData.Model).FirstName + " " + ((Candidate)ViewData.Model).MiddleName + " " + ((Candidate)ViewData.Model).LastName)%></span>
            </td>
            <td class="label" style="width: 95px">
                VN Name
            </td>
            <td class="input" style="width: 194px">
                <span class="color_green_bold">
                    <%=Html.Label(((Candidate)ViewData.Model).VnFirstName + " " + ((Candidate)ViewData.Model).VnMiddleName + " " + ((Candidate)ViewData.Model).VnLastName)%></span>
            </td>
            <td colspan="2" class="label" style="width: 300px">
            </td>
            <td rowspan="5" style="width: 160px; text-align: center;">
                <%
                    string labelPhoto = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
                    if (ViewData.Model == null)
                    {
                        Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                    }
                    else
                    {
                        string path = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE) + ((CRM.Models.Candidate)ViewData.Model).Photograph;

                        if (string.IsNullOrEmpty(((CRM.Models.Candidate)ViewData.Model).Photograph) || !System.IO.File.Exists(path))
                        {
                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                        }
                        else
                        {
                            labelPhoto = "<a id='thumb1' href='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + ((CRM.Models.Candidate)ViewData.Model).Photograph
                                            + "' class='highslide' onclick='return hs.expand(this)'>"
                                            + "<img id='imgPhoto' src='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + ((CRM.Models.Candidate)ViewData.Model).Photograph
                                            + "' alt='Highslide JS' title='Click to enlarge' height='120px' width='120px' /></a>";
                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                        }
                    }                        
                %>
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
            <td class="label" style="width: 70px">
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
        <tr>
            <td class="label">
                Remarks
            </td>
            <td colspan="5" class="input">
                <% =!string.IsNullOrEmpty(((Candidate)ViewData.Model).Note) ? ((Candidate)ViewData.Model).Note : Constants.NODATA%>
            </td>
        </tr>
        <tr class="last">
            <td class="label">
                Job Request
            </td>
            <td colspan="5" class="input">
                <% if (ViewData.Model != null)
                   {
                       Response.Write(Html.ActionLink(Constants.JOB_REQUEST_ITEM_PREFIX + ((Candidate)ViewData.Model).JRId, "Detail", "JobRequest", new { id = CommonFunc.GetJobRequestByJobRequestItem(((Candidate)ViewData.Model).JRId.ToString()) }, new { @style = "width:100px" }));
                   }
                %>
            </td>
        </tr>
    </table>
    <br />
    <%
        int j = 1;
        // Get list interview of candidate
        List<sp_GetInterviewCandidateResult> listInter = (List<sp_GetInterviewCandidateResult>)ViewData["InterviewCandi"];
        if (ViewData["Interview"] != null)
        {
            j = int.Parse(ViewData["Interview"].ToString());
        }
        for (int i = 1; i <= j; i++)
        {
            sp_GetInterviewCandidateResult inter = null;
            if (null != listInter && i <= listInter.Count)
                inter = listInter[i - 1]; %>
    <div id="div" class="show">
        <fieldset class="form" style="width: 1010px;">
            <legend>Round
                <%=inter.Round + " : " + inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW)%></legend>
            <table cellpadding="0" cellspacing="0" class="view" style="width: 1010px; border: 0px;">
                <tr>
                    <td class="label" style="width: 120px;">
                        Date
                    </td>
                    <td class="input" style="width: 150px;">
                        <% Response.Write(inter != null ? inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : ""); %>
                    </td>
                    <td class="label" style="width: 105px;">
                        Time
                    </td>
                    <td class="input" style="width: 95px;">
                        <% Response.Write(inter != null ? inter.InterviewDate.Value.Hour.ToString().PadLeft(3 - inter.InterviewDate.Value.Hour.ToString().Length, '0') : "00"); %>
                        <span>:</span>
                        <% Response.Write(inter != null ? inter.InterviewDate.Value.Minute.ToString().PadLeft(3 - inter.InterviewDate.Value.Minute.ToString().Length, '0') : "00"); %>
                    </td>
                    <td class="label" style="width: 120px;">
                        Venue
                    </td>
                    <td class="input" style="width: 400px;">
                        <% Response.Write(inter != null ? inter.Venue : ""); %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Interviewed by
                    </td>
                    <td class="input">
                        <% Response.Write(inter != null ? inter.Pic : ""); %>
                    </td>
                    <td class="label">
                        Result
                    </td>
                    <td class="input">
                        <span class="color_green_bold">
                            <%
InterviewResultDao daoR = new InterviewResultDao();
if (inter != null && inter.InterviewResultId != null)
{
    InterviewResult result = daoR.GetById((int)inter.InterviewResultId);
    if (result != null)
        Response.Write(result.Name);
}
                            %>
                        </span>
                    </td>
                    <td class="label">
                        Interview Result
                    </td>
                    <td class="input">
                        <%                     
                            if (inter != null && inter.InterviewFormId != null)
                            {
                                InterviewResultTemplateDao dao = new InterviewResultTemplateDao();
                                EFormMaster rt = dao.GetById(inter.InterviewFormId);
                                EformDao eDao = new EformDao();
                                string str = "";
                                int eFormIndex = eDao.GetIndexEform(inter.InterviewFormId, inter.CandidateId.ToString(), (int)Constants.PersonType.Candidate, (int)inter.Round);
                                if (rt != null)
                                {

                                    if (rt.Code == Constants.INTERVIEW_FORM_CODE + "-1")
                                    {
                                        str = "<a href=\"javascript:void(0);\" onclick=\"CRM.popup('/EForm/DetaiInteviewR1/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
                                            + " " + canObj.LastName + " for " + rt.Name + "', 1024)\">" + rt.Name + "</a>";
                                    }
                                    else if (rt.Code == Constants.INTERVIEW_FORM_CODE + "-2")
                                    {
                                        str = "<a href=\"javascript:void(0);\" onclick=\"CRM.popup('/EForm/DetaiInteviewR2/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
                                            + " " + canObj.LastName + " for " + rt.Name + "', 1024)\">" + rt.Name + "</a>";
                                        //Response.Write("CRM.popup('" + Html.ActionLink(rt.Name, "Round2", "EForm", new { id = eFormIndex }) + "','Result preview', 1024)");                                
                                    }
                                    else if (rt.Code == Constants.INTERVIEW_FORM_CODE + "-3")
                                    {
                                        str = "<a href=\"javascript:void(0);\" onclick=\"CRM.popup('/EForm/DetaiInteviewR3/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
                                            + " " + canObj.LastName + " for " + rt.Name + "', 1024)\">" + rt.Name + "</a>";
                                        //Response.Write("CRM.popup('" + Html.ActionLink(rt.Name, "Round3", "EForm", new { id = eFormIndex }) + "','Result preview', 1024)");                                                                
                                    }

                                    Response.Write(str);
                                }

                            }
                        %>
                    </td>
                </tr>
                <tr class="last">
                    <td class="label" style="width: 120px;">
                        Remarks
                    </td>
                    <td colspan="5" class="input" style="width: 870px;">
                        <%
                            Response.Write(Html.TextArea("Comment" + i.ToString(), inter != null ? inter.Content : "", 2, 1, new { @style = "width:820px", @readonly = "true" }));
                        %>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar +HiringCenterPageInfo.FuncInterviewHistoryDetails + CommonPageInfo.AppSepChar +  CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%
        Candidate can = (Candidate)ViewData.Model;
        string funcTitle = string.Empty;
        if (can != null)
        {
            funcTitle = "Management » Hiring Center » " + "<a href='/Interview/HistoryInterview/'>" + HiringCenterPageInfo.ModInterviewHistory + "</a> » " +
                can.FirstName + " " + can.MiddleName + " " + can.LastName;
        }
        else
        {
            Response.Redirect("/Interview");
        }
    %>
    <%= funcTitle%>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
