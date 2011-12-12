<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=HiringCenterPageInfo.MenuName + CommonPageInfo.AppSepChar +
                HiringCenterPageInfo.ModCandidate + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=HiringCenterPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%
        if (ViewData.Model == null)
            Response.Redirect("/Candidate");
        Candidate canObj = (CRM.Models.Candidate)ViewData.Model; %>
    <% 
        //duyhung.nguyen added to back to exam        
        if (ViewData["BackToExamURL"] == null)
        {
            Response.Write(CommonFunc.GetCurrentMenu(Request.RawUrl));
        }
        else
        {
            if (ViewData["BackToExamURL"].ToString().IndexOf("CandidateTestList") >= 0)
            {
                Response.Write(LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar +"<a href=\"/Exam\">" + LOTPageInfo.Exam + "</a> » <a href=\"" + ViewData["BackToExamURL"] + "\">" + LOTPageInfo.CandidateTestList + "</a> » ");
            }
            else
            {
                Response.Write(LOTPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + "<a href=\"/Exam\">" + LOTPageInfo.Exam + "</a> » <a href=\"" + ViewData["BackToExamURL"] + "\">" + LOTPageInfo.AssignCandidate + "</a> » ");   
            }            
        }
      
    %>    
    <% if(canObj != null) Response.Write(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <% 
        if (ViewData.Model == null)
            Response.Redirect("/Candidate");
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        Candidate canObj = (CRM.Models.Candidate)ViewData.Model;
        List<int> canList = new List<int>();
        canList = (List<int>)ViewData["ListInter"];
        int lastID = 0;
        int firstID = 0;
        int nextID = 0;
        int preID = 0;
        int number = 0;
        int total = 0;
        if (canList == null)
        {

            //List<sp_GetCandidateResult> CanList = new CandidateDao().GetList();
            //canList = CanList.Select(p => p.ID).ToList();
        }
        
        int index = 0;
        total = canList.Count;
        if (canList.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";            
            index = canList.IndexOf(canObj.ID);
            if (index == 0)
            {
                styleFirst = "class=\"first first_off\"";
                stylePrev = "class=\"prev prev_off\"";
            }
            else if (index == canList.Count - 1)
            {
                styleLast = "class=\"last last_off\"";
                styleNext = "class=\"next next_off\"";
            }
            number = index + 1;
        }
        else if (canList.Count == 1)
        {
            number = canList.Count;
        }

        if (canList != null && canList.Count > 0)
        {
            lastID = canList[canList.Count - 1]; ;
            firstID = canList[0];
            nextID = 0;
            if (index + 1 < canList.Count)
                nextID = canList[index + 1];
            else
                nextID = canList[canList.Count - 1];

            preID = 0;
            if ((index - 1) == -1)
                preID = canList[0];
            else if(index > 0)
                preID = canList[index - 1];

        }
    %>
    <div id="cnavigation" style="width: 1024px">
        <button type="button" id="btnLast" value="<%=lastID %>" <%=styleLast %>>
        </button>
        <button type="button" id="btnNext" value="<%=nextID %>" <%=styleNext %>>
        </button>
        <span>
            <%= number + " of " + total%></span>
        <button type="button" id="btnPre" value="<%=preID %>" <%=stylePrev %>>
        </button>
        <button type="button" id="btnFirst" value="<%=firstID%>" <%=styleFirst %>>
        </button>
    </div>
    <div style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td>
                    <h2 class="heading">
                        Personal Info
                    </h2>
                </td>
                <td align="right">
                    <% if (ViewData["BackToExamURL"] == null)
                       { %>
                    <a href="javascript:void(0);" onclick="CRM.popup('/Candidate/EditPop/<%=canObj.ID.ToString() %>', 'Update Candidate Information', 940);" id="lnkEdit">Edit</a>
                        <%} %>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tbody>
                <tr>
                    <td class="label">
                        Full Name
                    </td>
                    <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                        <% 
                       
                            Response.Write(canObj.FirstName + " " + canObj.MiddleName + " " + canObj.LastName);
                            Response.Write(Html.Hidden("Fullname", canObj.FirstName.Trim() + "_" + canObj.MiddleName + "_" + canObj.LastName.Trim()));
                        %>
                    </td>
                    <td class="label">
                        VN Full Name
                    </td>
                    <td style="padding-left: 10px; height: 30px; width: 300px" class="input">
                        <% 
                            Response.Write(canObj.VnFirstName + " " + canObj.VnMiddleName + " " + canObj.VnLastName);
                                  
                        %>
                    </td>
                    <td rowspan="6" valign="top">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <%
                                        string labelPhoto = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
                                        if (ViewData.Model == null)
                                        {
                                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                        }
                                        else
                                        {
                                            string path = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE) + ((CRM.Models.Candidate)ViewData.Model).Photograph;

                                            if (string.IsNullOrEmpty(canObj.Photograph) || !System.IO.File.Exists(path))
                                            {
                                                Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                            }
                                            else
                                            {
                                                labelPhoto = "<a id='thumb1' href='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + ((CRM.Models.Candidate)ViewData.Model).Photograph
                                                                + "' class='highslide' onclick='return hs.expand(this)'>"
                                                                + "<img id='imgPhoto' src='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + ((CRM.Models.Candidate)ViewData.Model).Photograph
                                                                + "' alt='Highslide JS' title='Click here to enlarge' height='160px' width='140px' /></a>";
                                                Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                            }
                                        }
                                        if (ViewData.Model == null)
                                        {
                                            Response.Write(Html.Hidden("Photograph", ""));
                                            Response.Write(Html.Hidden("CVFile", ""));
                                            Response.Write(Html.Hidden("ID", ""));
                                        }
                                        else
                                        {

                                            Response.Write(Html.Hidden("Photograph", ((CRM.Models.Candidate)ViewData.Model).Photograph));
                                            Response.Write(Html.Hidden("CVFile", ((CRM.Models.Candidate)ViewData.Model).CVFile));
                                            Response.Write(Html.Hidden("UpdateDate", ((CRM.Models.Candidate)ViewData.Model).UpdateDate.ToString()));
                                            Response.Write(Html.Hidden("ID", ((CRM.Models.Candidate)ViewData.Model).ID));
                                        }                    
                                    %>
                                </td>
                                <td valign="top" style="width: 20px">
                                    <input type="button" class="upload_image" id="btnChangePhoto" value="" title="Change Photo" />
                                    <%  string stylePhoto = "display: none";
                                        string styleCV = "display: none";
                                        if (ViewData.Model != null)
                                        {
                                            if (!string.IsNullOrEmpty(((CRM.Models.Candidate)ViewData.Model).Photograph))
                                            {
                                                stylePhoto = "display: block";
                                            }
                                            if (!string.IsNullOrEmpty(((CRM.Models.Candidate)ViewData.Model).CVFile))
                                            {
                                                styleCV = "display: block";
                                            }
                                        }
                                    %>
                                    <input type="button" id="btnRemoveImage" class="remove_image" style="<%=stylePhoto %>"
                                        onclick="CRM.msgConfirmBox('Are you sure you want to remove Photo?', 450, 'CRM.removeImage(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')');"
                                        value="" title="Remove Photo" />
                                    <input type="button" class="upload_cv" value="" id="btnUpload_CV" title="Upload CV" />
                                    <input type="button" id="btnRemoveCV" class="remove_cv" style="<%=styleCV %>" value=""
                                        title="Remove CV" onclick="CRM.msgConfirmBox('Are you sure you want to remove CV?', 450, 'CRM.removeCVFile(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')');" />
                                    <input type="button" id="btndownload_cv" class="download_cv" style="<%=styleCV %>"
                                        value="" title="Download CV" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Date of Birth
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <%Response.Write(canObj.DOB.HasValue?canObj.DOB.Value.ToString(Constants.DATETIME_FORMAT):"");%>
                    </td>
                    <td class="label">
                        CellPhone
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% 
                            Response.Write(canObj.CellPhone);
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Email
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% 
                            Response.Write(canObj.Email);
                        %>
                    </td>
                    <td class="label">
                        Gender
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% Response.Write(canObj.Gender == Constants.MALE?"Male":"Female"); %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Searched date
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% Response.Write(canObj.SearchDate.ToString(Constants.DATETIME_FORMAT));%>
                    </td>
                    <td class="label">
                        Source
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <%Response.Write(canObj.CandidateSource.Name);%>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Position
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% 
                            Response.Write(canObj.JobTitleLevel.DisplayName);
                                  
                        %>
                    </td>
                    <td class="label">
                        Status
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                    <% 
                            Response.Write(CommonFunc.GetCandidateStatus(canObj.Status));
                                  
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        University
                    </td>
                    <td style="padding-left: 10px; height: 30px" class="input">
                        <% 
                            Response.Write(canObj.UniversityId.HasValue?canObj.University.Name:"");
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Address
                    </td>
                    <td style="padding-left: 10px; height: 30px" colspan="3" class="input">
                        <% Response.Write(canObj.Address); %>
                    </td>
                </tr>
                <tr class="last">
                    <td class="label">
                        Remarks
                    </td>
                    <td style="padding-left: 10px; height: 100px;" colspan="3" class="input">
                        <%Response.Write(canObj.Note);%>
                    </td>
                    <td valign="middle" align="center">
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <h2 class="heading">
            Exam Result(s)
        </h2>
        <table  cellspacing="0" cellpadding="0" border="0" width="100%" class="grid">
            <%
                var examList = ViewData["ExamList"] as List<LOT_Candidate_Exam>;
                if (examList == null || examList.Count == 0)
                {
                    Response.Write("<tr><td class='label'>This candidate has not yet taken an Exam</td></tr>");
                }
                else
                {
                    Response.Write("<tr><th class='exam_isfinished gray'>Finished</th><th class='exam_name gray'>Exam Name</th><th class='exam_date gray'>Exam Date</th>" +
                        "<th class='exam_mark gray'>English Skill</th><th class='exam_mark gray'>Verbal Skill</th><th class='exam_mark gray'>Critical Thinking</th>" +
                        "<th class='exam_mark gray'>Programming Skill</th></tr>");
                    foreach (var exam in examList)
                    {
                        int? englishMaxMark = exam.GetEnglishSkillMaxMark();
                        int? verbalMaxMArk = exam.GetMaxMarkOfSection(Constants.LOT_VERBAL_SKILL_ID);
                        int? criticalThinkingMaxMark = exam.GetMaxMarkOfSection(Constants.LOT_CRITICAL_THINKING_ID);
                        int? programmingMaxMark = exam.GetMaxMarkOfSection(Constants.LOT_PROGRAMMING_SKILL_ID);
                        double? fMarkEnglish = exam.GetEnglishSkillMark();
                        string markEnglish = englishMaxMark !=null && englishMaxMark.Value!=0 ? 
                            (fMarkEnglish.HasValue ? Math.Floor(fMarkEnglish.Value + 0.5) : 0) + "/" + englishMaxMark : "";
                        string markVerbal = verbalMaxMArk !=null ? exam.VerbalMark + "/" + verbalMaxMArk : "";
                        string markCriticalThinking = criticalThinkingMaxMark !=null ? 
                            exam.GetMarkOfSection(Constants.LOT_CRITICAL_THINKING_ID) + "/" + criticalThinkingMaxMark : "";
                        string markProgramming = programmingMaxMark !=null ? exam.ProgramingMark + "/" + programmingMaxMark : "";

                        Response.Write(string.Format("<tr><td style='text-align:center'>{0}</td><td style='text-align:left'>{1}</td>"+
                            "<td style='text-align:center'>{2}</td><td style='text-align:center'>{3}</td><td style='text-align:center'>{4}</td>"+
                            "<td style='text-align:center'>{5}</td><td style='text-align:center'>{6}</td></tr>", exam.IsFinish ? "Yes" : "No",
                            CommonFunc.Link("", "", "/Exam/CandidateTestDetail/?id=" + exam.ID + "&urlback=" + 
                            Request.RawUrl, exam.LOT_Exam.Title), 
                            exam.LOT_Exam.ExamDate.ToString(Constants.DATETIME_FORMAT_VIEW), 
                            markEnglish, markVerbal, markCriticalThinking, markProgramming));
                    }
                }
            %>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/highslide/highslide.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/highslide/highslide.css" />
    <style type="text/css">
        th.exam_isfinished
        {
            width:50px;    
        }
        th.exam_mark
        {
            width:120px;    
        }
        th.exam_date
        {
            width:100px;    
        }
    </style>
    <script type="text/javascript">
        function getDownload() {
            var file_path = '<%=Constants.CV_PATH_ROOT_CANDIDATE %>' + $('#CVFile').val();
            var outputname = $('#Fullname').val() + "'s_CV";

            CRM.downLoadFile(file_path, outputname == "" ? "Candidate'sCV" : outputname);

            return false;
        }
        hs.graphicsDir = '/Scripts/highslide/graphics/';
        hs.outlineType = 'rounded-white';
        jQuery(document).ready(function () {

            $("#btnChangePhoto").click(function () {
                var url = "/Common/UploadImage?controller=Candidate&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.PHOTO_PATH_ROOT_CANDIDATE %>";
                CRM.popUpWindow(url, '#Photograph', 'Upload Photo');
                return false;
            });
            $("#btnUpload_CV").click(function () {
                var url = "/Common/UploadFile?controller=Candidate&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.CV_PATH_ROOT_CANDIDATE %>";
                CRM.popUpWindow(url, '#CVFile', 'Upload CV');
                return false;
            });
            $("#btndownload_cv").click(function () {
                return getDownload();
            });
            /* Navigator */
            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/Candidate/Detail/" + $('#btnFirst').val();
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/Candidate/Detail/" + $('#btnPre').val();
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/Candidate/Detail/" + $('#btnNext').val();
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {
                    window.location = "/Candidate/Detail/" + $('#btnLast').val();
                }
            });
            /*---------------------*/
        });
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
