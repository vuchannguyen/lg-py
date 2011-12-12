<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    hs.graphicsDir = '../../Scripts/highslide/graphics/';
    hs.outlineType = 'rounded-white';
</script>

<div class="form" style="width: 870px">
    <div class="profile" style="height: 30px;">
        <div class="ctbox">
            <h2>
                Candidate Information</h2>
        </div>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="view" width="1024px">
        <tr>
            <td class="label" style="width: 130px">
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
            <td rowspan="4" style="width: 160px; text-align: center;">
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
                <%= ((Candidate)ViewData.Model).DOB.HasValue?((Candidate)ViewData.Model).DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW):""%>
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
                <%= (((Candidate)ViewData.Model).UniversityId).HasValue ? ((Candidate)ViewData.Model).University.Name : Constants.NODATA%>
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
    <table width="1024px" class="view">
        <tr class="last">
            <td class="label required" style="width: 118px; text-align: right;">
                <%= Constants.JOB_REQUEST_ITEM_PREFIX %><span>*</span>
            </td>
            <td class="input" style="width: 895px;">
                <% if (ViewData.Model == null)
                   {

                       Response.Write(Html.TextBox("JR", "", new { @style = "width:100px", @maxlength = "10", @readonly = true }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("JR", ((Candidate)ViewData.Model).JRId, new { @style = "width:100px", @maxlength = "10", @readonly = true }));
                   }
                %>
                <% Response.Write(Html.Hidden("JRApproval", "")); %>
                <button type="button" class="icon select" title="Select JR" onclick="CRM.listJRInterview('Interview'); return false;">
                </button>
                <button type="button" class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR'); return false;">
                </button>
            </td>
        </tr>
    </table>
    <%
        int j = 1;
        // Get list interview of candidate
        List<sp_GetInterviewCandidateResult> listInter = (List<sp_GetInterviewCandidateResult>)ViewData["InterviewCandi"];
        if (ViewData["Interview"] != null)
        {
            j = int.Parse(ViewData["Interview"].ToString());
        }
        for (int i = 1; i <= Constants.MAX_INTERVIEW; i++)
        {
            sp_GetInterviewCandidateResult inter = null;
            if (null != listInter && i <= listInter.Count)
                inter = listInter[i - 1];
            string styleCSS = string.Empty;
            if (i <= j)
            {
                styleCSS = "show form";
            }
            else
            {
                styleCSS = "hidden form";
            }
    %>
    <div id="fieldSet<%=i %>" class="<%=styleCSS%>">
        <% if (i == 1)
           { %>
        <br />
        <table width="1024px" class="profile">
            <tr>
                <td style="width: 770px;">
                </td>
                <td style="text-align: right; width: 75px;" colspan="2">
                    <button type="button" id="btnAddInterview" title='Add a New Round' class='icon plus'>
                    </button>
                    &nbsp;
                    <button type="button" id="remove" title='Remove a Round' class='icon minus'>
                    </button>
                </td>
            </tr>
        </table>
        <% } %>
        <%= Html.Hidden("InterviewId" + i.ToString(), (inter != null? inter.Id.ToString():""))%>
        <%= Html.Hidden("ResultId" + i.ToString(), (inter != null? inter.InterviewResultId.ToString():"null"))%>
        <fieldset class="form" style="width: 1010px;">
            <legend>Round:
                <%=i %></legend>
            <table border="0" cellpadding="0" cellspacing="0" class="profile" style="width: 1010px;">
                <tr>
                    <td class="required label" style="width: 125px;">
                        Date&nbsp;<span>*</span>
                    </td>
                    <td class="input" style="width: 120px;">
                        <% if (inter != null)
                           {
                               if (inter.InterviewResultId > 0)%>
                        <% { %>
                        <% Response.Write(Html.TextBox("InterviewDate" + i.ToString(), inter != null ? inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:70px;" })); %>
                        <div class="hidden">
                            <% Response.Write(Html.TextBox("InterviewDate" + i.ToString(), inter != null ? inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:70px;" })); %>
                        </div>
                        <%  }
                               else  %>
                        <%{ %>
                        <% Response.Write(Html.TextBox("InterviewDate" + i.ToString(), inter != null ? inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:70px;" })); %>
                        <%} %>
                        <% }
                           else  %>
                        <%{ %>
                        <% Response.Write(Html.TextBox("InterviewDate" + i.ToString(), inter != null ? inter.InterviewDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:70px;" })); %>
                        <%} %>
                    </td>
                    <td class="required label" style="width: 97px;">
                        Time&nbsp;<span>*</span>
                    </td>
                    <td class="intput" style="width: 120px;">
                        <% SelectList listHour = new SelectList(Constants.HourList, "Value", "Text", inter != null ? inter.InterviewDate.Value.Hour : 0);%>
                        <% SelectList listMinute = new SelectList(Constants.MinuteList, "Value", "Text", inter != null ? inter.InterviewDate.Value.Minute : 0);%>
                        <%=Html.DropDownList("Hour" + i.ToString(), listHour, new { @style = "width:40px;"})%>
                        :
                        <%=Html.DropDownList("Minute" + i.ToString(), listMinute, new { @style = "width:40px"})%>
                    </td>
                    <td class="required label" style="width: 120px;">
                        Venue&nbsp;<span>*</span>
                    </td>
                    <td class="input" style="width: 430px;">
                        <% 
                            Response.Write(Html.TextBox("txtLocation" + i.ToString(), inter != null ? inter.Venue : "", new { @style = "width:320px" }));                       
                        %>
                    </td>
                </tr>
                <tr>
                    <td class="required label">
                        Interviewed by&nbsp;<span>*</span>
                    </td>
                    <td colspan="3" class="input">
                        <input type="text" maxlength="100" style="width: 400px" value='<%= (inter != null?inter.Pic:"") %>'
                            id="txtPic<%=i.ToString()%>" name="txtPic<%=i.ToString()%>" />
                        <input type="hidden" id="txtPicHid<%=i.ToString()%>" name="txtPicHid<%=i.ToString()%>" />
                        
                    </td>
                    <td class="required label" style="width: 120px;">
                        Interview Form&nbsp;<span>*</span>
                    </td>
                    <td class="input" style="width: 430px;">
                        <% 
List<EFormMaster> resultList = (List<EFormMaster>)ViewData["ResultTemplate"];
SelectList listTemplate = new SelectList(resultList, "Code", "name", inter != null ? inter.InterviewFormId : null);
Response.Write(Html.DropDownList("ResultTemplate" + i.ToString(), listTemplate, Constants.CANDIDATE_SOURCE, new { @style = "width:305px" }));
Candidate canObj = (Candidate)ViewData.Model;
string titleName = "Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
                        + " " + canObj.LastName + " for ";
string interviewID = string.Empty;
if (inter != null)
{
    interviewID = inter.Id.ToString();
}
if (inter != null && inter.InterviewFormId != null && inter.InterviewResultId != null)
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
            str = "<button type='button' class='icon preview' title='Interview Result' id='" + i.ToString() + "' onclick=\"CRM.popup('/EForm/DetaiInteviewR1/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
    + " " + canObj.LastName + " for " + rt.Name + "', 1024)\"></button>";
        }
        else if (rt.Code == Constants.INTERVIEW_FORM_CODE + "-2")
        {
            str = "<button type='button' class='icon preview' title='Interview Result' id='" + i.ToString() + "' onclick=\"CRM.popup('/EForm/DetaiInteviewR2/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
     + " " + canObj.LastName + " for " + rt.Name + "', 1024)\"></button>";
        }
        else if (rt.Code == Constants.INTERVIEW_FORM_CODE + "-3")
        {
            str = "<button type='button' class='icon preview' title='Interview Result' id='" + i.ToString() + "' onclick=\"CRM.popup('/EForm/DetaiInteviewR3/" + eFormIndex + "','Interview Result of " + canObj.FirstName + " " + canObj.MiddleName
    + " " + canObj.LastName + " for " + rt.Name + "', 1024)\"></button>";
        }
        else // case not yet set result for this round
        {
            str = "<button type='button' class='icon preview' title='Interview Result' id='" + i.ToString() + "' onclick=\"openTemplate('#ResultTemplate" + i.ToString() + "','" + inter.Id + "','" + titleName + "\"></button>";
        }

        Response.Write(str);
    }
}
else
{
                        %>
                        <button id="TemForm<%=i.ToString()%>" name="TemForm<%=i.ToString()%>" type="button"
                            class="icon preview" title="Interview Result" onclick="openTemplate('#ResultTemplate<%=i.ToString()%>','<%=interviewID %>','<%=titleName %>'); return false;">
                        </button>
                        <% } %>
                    </td>
                </tr>
                <%if (inter != null)
                  { %>
                <tr>
                    <td class="required label">
                        Send Meeting Request
                    </td>
                    <td id="colCandidate<%=i.ToString()%>" colspan="3" class="input">
                        <%string candidateByMail = "<a id=\"candidateAction" + i.ToString() + "\" href=\"javascript:void(0);\" onclick=\"CRM.popup('/Interview/SendInterviewMail/?ids=" + inter.Id.ToString() + "&page=/Edit/" + inter.CandidateId.ToString() + "','Send Meeting Request', 1060)\">" + (inter.IsSendMailInterviewer != true ? "No" : "Yes") + "</a>";
                          Response.Write(candidateByMail);  %>
                    </td>
                    <td class="required label">
                        Email to Candidate
                    </td>
                    <td id="colReview<%=i.ToString()%>" colspan="3" class="input">
                        <%string reviewByMail = "<a id=\"reviewAction" + i.ToString() + "\" href=\"javascript:void(0);\" onclick=\"CRM.popup('/Interview/SendCandidateMail/?ids=" + inter.Id.ToString() + "&page=/Edit/" + inter.CandidateId.ToString() + "','Email to Candidate', 1060)\">" + (inter.IsSentMailCandidate != true ? "No" : "Yes") + "</a>";
                          Response.Write(reviewByMail);  %>
                    </td>
                </tr>
                <%} %>                
                <% if (inter != null)
                   {
                       if (inter.InterviewResultId > 0)%>
                <% { %>
                <tr>
                    <td class="required label">
                        Grant Edit Permission for
                    </td>
                    <td colspan="5" class="input">
                    <div style="width:800px">
                        <%
Response.Write(Html.TextBox("fieldCCList" + i, (inter != null ? inter.CClist : ""), new { @style = "width:790px" }));
Response.Write(Html.Hidden("CCList" + i, ""));
                        %>
                    </div>
                    </td>
                    
                </tr>
                <tr>
                    <td class="label">
                        Remark
                    </td>
                    <td colspan="5" class="input">
                        <%
                            Response.Write(Html.TextArea("Comment" + i.ToString(), inter != null ? inter.Content : "", 2, 1, new { @style = "width:800px" }));
                        %>
                    </td>
                </tr>
                <tr class="last">
                    <td class="label">
                        Result
                    </td>
                    <td colspan="5" class="color_green_bold">
                        <%
InterviewResultDao dao = new InterviewResultDao();
InterviewResult result = dao.GetById((int)inter.InterviewResultId);
if (result != null)
    Response.Write(result.Name);
                        %>
                    </td>
                </tr>
                <% } %>
                <% else  %>
                <% { %>
                <tr>
                    <td class="required label">
                        Grant Edit Permission for
                    </td>
                    <td colspan="5" class="input" >
                    <div style="width:800px">
                        <%
Response.Write(Html.TextBox("fieldCC" + i, (inter != null ? inter.CClist : ""), new { @style = "width:790px" }));
Response.Write(Html.Hidden("CCList" + i, ""));
                        %>
                    </div>
                    </td>
                </tr>
                <tr class="last">
                    <td class="label">
                        Remarks
                    </td>
                    <td colspan="5" class="input">
                        <%
                            Response.Write(Html.TextArea("Comment" + i.ToString(), inter != null ? inter.Content : "", 2, 1, new { @style = "width:800px" }));
                        %>
                    </td>
                </tr>
                <% } %>
                <% }
                   else  %>
                <% { %>
                <tr>
                    <td class="required label">
                        Grant Edit Permission for
                    </td>
                    <td colspan="5" class="input">
                    <div style="width:800px">
                        <%
                            Response.Write(Html.TextBox("fieldCC" + i, (inter != null ? inter.CClist : ""), new { @style = "width:790px" }));
Response.Write(Html.Hidden("CCList" + i, ""));
                        %>
                     </div>
                    </td>
                </tr>
                <tr class="last">
                    <td class="label">
                        Remarks
                    </td>
                    <td colspan="5" class="input">
                        <%
                            Response.Write(Html.TextArea("Comment" + i.ToString(), inter != null ? inter.Content : "", 2, 1, new { @style = "width:800px" }));
                        %>
                    </td>
                </tr>
                <% } %>
            </table>
        </fieldset>
    </div>
    <% } %>
    <%= Html.Hidden("txtNumber")%>
    <%= Html.Hidden("canId", ((Candidate)ViewData.Model).ID)%>
    <table style="width: 1024px">
        <tr>
            <td>
                <div class="form" style="text-align: center">
                    <input type="submit" title="Save" class="save" value="" />
                    <% string javascript = "javascript:CRM.back()";
                       javascript = "window.location = '/Interview'";
                       if (listInter == null)
                       {
                           javascript = "window.location = '/Candidate'";
                       }
                    %>
                    <input type="button" title="Cancel" onclick="<%=javascript %>" id="btnCancel" class="cancel"
                        value="" />
                </div>
            </td>
        </tr>
    </table>
</div>
<script src="/Scripts/AutoComplete/jquery.autoSuggest.js" type="text/javascript"></script>
<script type="text/javascript">
    /////////////////////////////////
    var row_id = <%=ViewData["Interview"] %>;  
    var reply = false;

    function setDisable() {
        for (var i = 1; i < 4; i++)
        {
            var idr = $('#ResultId' + i).val();         
            if (idr != "null" && idr != "") {
                $('#Hour' + i).attr('disabled', true);
                $('#Minute' + i).attr('disabled', true);
                $('#txtLocation' + i).attr('disabled', true);           
                $('#txtPic' + i).attr('disabled', true);           
                $('#fieldCC' + i).attr('disabled', true);           
                $('#ResultTemplate' + i).attr('disabled', true);
                $('#Comment' + i).attr('disabled', true);
                $('#InterviewDate' + i).datepicker("disable");
                var valCandidate = $('#candidateAction' + i).text();
                $('#colCandidate' + i).html(valCandidate);
                 var valReview = $('#reviewAction' + i).text();
                $('#colReview' + i).html(valReview);
                $(".as-close").remove();
            }
        }    
    }

    function setReply() {        
        CRM.closePopup();
        reply = true;
        if (reply) {
             jQuery.ajax({
            url: "/Interview/Delete",
            type: "POST",
            datatype: "json",
            data: ({
                'id': $('#InterviewId' + row_id.toString()).val()
            }),
            success: function (data) {
               reply = false;

            $("span[htmlfor=InterviewDate" + row_id.toString() +"]").remove();
            $("span[htmlfor=txtLocation" + row_id.toString() +"]").remove();
            $("span[htmlfor=txtPic" + row_id.toString() +"]").remove();
            $("span[htmlfor=ResultTemplate" + row_id.toString() +"]").remove();
            $("span[htmlfor=Comment" + row_id.toString() +"]").remove();

            var s = "#fieldSet" + row_id.toString();               
            $(s).removeClass();
            $(s).addClass("hidden");
            $("#InterviewDate"+ row_id.toString()).rules("remove");     
            if (row_id <= 5) {
                $("#btnAddInterview").removeClass();
                $("#btnAddInterview").addClass("icon plus");
            }

            $("#InterviewDate"+ row_id).val("");
            $("#Hour"+ row_id).val("");
            $("#Minute"+ row_id).val("");
            $("#txtLocation"+ row_id).val("");
            $("#txtPic"+ row_id).val("");
            $("#ResultTemplate"+ row_id).val("");
            $("#Comment"+ row_id).val("");
            $("#fieldCC"+ row_id).val("");
            $("#CCList"+ row_id).val("");
            $("#InterviewDate"+ row_id).rules("remove");
            $("#txtLocation"+ row_id).rules("remove");
            $("#txtPic"+ row_id).rules("remove");
            $("#ResultTemplate"+ row_id).rules("remove");
            $("#Comment"+ row_id).rules("remove");
                         
            row_id--;
            if (row_id <= 1) {                
                $("#remove").removeClass();
                $("#remove").addClass("icon minus_off");
            }          
            }
        });

            
        }
    }

    function DeleteInterview(id) {
        jQuery.ajax({
            url: "/Interview/Delete",
            type: "POST",
            datatype: "json",
            data: ({
                'id': id
            }),
            success: function (data) {
                if (data == true) {
                    return true;   
                }                
            }
        });
    }

    $(document).ready(function () {  
    
        for(i=1;i<=5;i++)
        {
            $("#txtPic"+i).autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>',
                                    {   max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", 
                                        faceBook: true, hidField: "#txtPicHid"+i, employee: true,multiObject:true  
                                    }
                                   );                   
        
        }
//        $("#txtPic1").autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>',
//                                    {   max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", 
//                                        faceBook: true, hidField: "#txtPicHid1", employee: true,multiObject:true  
//                                    }
//                                   );         
//        $("input#txtPic2").autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>');           
//        $("#txtPic3").autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>');
//        $("#txtPic4").autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>');                
//        $("#txtPic5").autocomplete('<%= Url.Action("SearchInterviewBy", "Interview") %>');   
        
        $("#txtLocation1").autocomplete('<%= Url.Action("SearchVenue", "Interview") %>');
        $("#txtLocation2").autocomplete('<%= Url.Action("SearchVenue", "Interview") %>');                
        $("#txtLocation3").autocomplete('<%= Url.Action("SearchVenue", "Interview") %>');                
        
        $("#fieldCC1").autocomplete('/Interview/SearchInterviewBy',              
              { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CCList1", employee: true,multiObject:true  });
        $("#fieldCC2").autocomplete('/Interview/SearchInterviewBy',
             { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CCList2", employee: true,multiObject:true });
        $("#fieldCC3").autocomplete('/Interview/SearchInterviewBy',
             { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CCList3", employee: true,multiObject:true });
        $("*[id^=fieldCCList]").autocomplete('/Interview/SearchInterviewBy',              
              { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, employee: true,multiObject:true  });

        $("#InterviewDate1").datepicker();
        $("#InterviewDate2").datepicker();
        $("#InterviewDate3").datepicker();
        $("#InterviewDate4").datepicker();
        $("#InterviewDate5").datepicker();
        
        $("*[id^=fieldCCList]").attr("style","display:none");
        
        $("#JR").click(function () {                
        });
        
        $("#remove").click(function () {            
            if (row_id == 1) {
                return;
            }            
            CRM.msgConfirmBox('Are you sure you want to remove interview round: ' + row_id +'?', 350, 'javascript:setReply();');            
            
        });

        $("#btnAddInterview").click(function () {
            if (row_id == 3 ) {               
            return;
            }   
            row_id++;
            if (row_id == 3 ) {              
                $(this).removeClass();
                $(this).addClass("icon plus_off");
            }
            if (row_id > 1) {
            $("#remove").removeAttr("class");                
            $("#remove").addClass("icon minus");
            }
            var s = "#fieldSet" + row_id.toString();               
            $(s).removeClass();
            $(s).addClass("show");
            
            $("#InterviewDate"+ row_id.toString()).rules("add", "required");
            if (row_id > 1)  {
                $("#InterviewDate"+ row_id).rules("add", {compareDate2: ["#editForm input[name='InterviewDate"+(row_id-1)+"']", "get", "Interview Date Round " + row_id,
                 "Interview Date Round Round " + (row_id -1), $("#InterviewDate"+ row_id).val(), "#Hour"+(row_id)+"", "#Minute"+(row_id)+"", "#Hour"+ (row_id-1), "#Minute"+ (row_id-1)]});                
            }
            $("#InterviewDate"+ row_id.toString()).rules("add", "checkDate");
            $("#txtLocation"+ row_id.toString()).rules("add", "required");
            var input = "#txtPic" + row_id;
            $("#txtPicHid1"+ row_id.toString()).rules("add", {required:true, remote: {
                                                                url: '<%= Url.Action("CheckInterviewByExist", "Interview") %>',
                                                                type: "post",
                                                                datatype: "json",
                                                                data: {
                                                                  userName: function() {                                                                   
                                                                    return $(input).val();
                                                                  }
                                                                }
                                                              }
                                                        }
            );
            $("#ResultTemplate"+ row_id.toString()).rules("add", "required");
            $("#Comment"+ row_id.toString()).rules("add", {maxlength:250});
        });

        $("#editForm").validate({
            debug: false,
            errorElement: "span",
            errorPlacement: function (error, element) {
                error.tooltip({
                    bodyHandler: function () {
                        return error.html();
                    }
                });
                error.insertAfter(element);
            }               
        });
        setDisable();
        setValidate();        
    });  
    
    jQuery.validator.addMethod(
        "compareDate2", function (value, element, parameters) {
        var firstDateOrg = convertDate(value);
        var secondDateOrg = convertDate($(parameters[0]).val());
    
        var hour1 = $(parameters[5] + " option:selected").text();
        var minute1 = $(parameters[6] + " option:selected").text();
        var hour2 = $(parameters[7] + " option:selected").text();
        var minute2 = $(parameters[8] + " option:selected").text();
        firstDateOrg = firstDateOrg + " " + hour1 + ":" + minute1 + ":0";
        secondDateOrg = secondDateOrg + " " + hour2 + ":" + minute2 + ":0";
        var firstDate = new Date(firstDateOrg);
        var secondDate = new Date(secondDateOrg);
        var isValid = false;
        switch (parameters[1]) {
        case 'gt': isValid = firstDate > secondDate;
            jQuery.validator.messages.compareDate2 = parameters[2] + " must be greater than " + parameters[3];
            break;
        case 'lt': isValid = firstDate < secondDate;
            jQuery.validator.messages.compareDate2 = parameters[2] + " must be less than " + parameters[3];
            break;
        case 'get': isValid = firstDate >= secondDate;
            jQuery.validator.messages.compareDate2 = parameters[2] + " must be greater than or equal to " + parameters[3];
            break;
        case 'let': isValid = firstDate <= secondDate;
            jQuery.validator.messages.compareDate2 = parameters[2] + " must be less than or equal to " + parameters[3];
            break;
        case 'eq': isValid = firstDate == secondDate;
            jQuery.validator.messages.compareDate2 = parameters[2] + " must be equal to " + parameters[3];
            break;
        };

    return isValid;
    }, jQuery.validator.messages.compareDate2);

    function setValidate() {    
        $("#JR").rules("add", {required:true, remote: {
                                                        url: '<%= Url.Action("CheckJRByExist", "Interview") %>',
                                                        type: "post",
                                                        datatype: "json",
                                                        data: {
                                                            Jr: function() {                                                                   
                                                                return $("#JR").val();
                                                                }
                                                            }
                                                        }
                               }
        );
        for(var i=1; i<= row_id;i++)
        {    
            $("#InterviewDate"+ i.toString()).rules("add", {required:true, checkDate:true});
            if (i>1)  {
                $("#InterviewDate"+ i).rules("add", {compareDate2: ["#editForm input[name='InterviewDate"+(i-1)+"']", "gt", "Interview Date Round " + i,
                 "Interview Date Round " + (i -1), $("#InterviewDate"+ i).val(), "#Hour"+(i)+"", "#Minute"+(i)+"", "#Hour"+ (i-1), "#Minute"+ (i-1)]});                
            }
            var txtPic = "#txtPic" + i;
            if (i == 1) {
                $("#txtPicHid1").rules("add", {required:true});
            }
            if (i == 2) {
                $("#txtPicHid2").rules("add", {required:true});
            }
            if (i == 3) {
                $("#txtPicHid3").rules("add", {required:true});
            }
            $("#txtLocation"+ i).rules("add", "required");
            $("#ResultTemplate"+ i).rules("add", "required");
            $("#Comment"+ i).rules("add", {maxlength:250});

        }        
    }

    function ClearJR() {
        $("#JR").attr('value', '');
    }

    function openTemplate(value,interviewID,title) {   
        if ($(value).val() ==  '<%= Constants.INTERVIEW_FORM_CODE %>-1') {
            CRM.popup('/Interview/PreviewForm/?id=1&interviewID='+interviewID, ""+title+"<%= Constants.INTERVIEW_ROUND_1 %>", 1050);
        }
        else if ($(value).val() ==  '<%= Constants.INTERVIEW_FORM_CODE %>-2') {
            CRM.popup('/Interview/PreviewForm/?id=2&interviewID='+interviewID, ""+title+"<%= Constants.INTERVIEW_ROUND_2 %>", 1050);
        }
        else if ($(value).val() ==  '<%= Constants.INTERVIEW_FORM_CODE %>-3') {
            CRM.popup('/Interview/PreviewForm/?id=3&interviewID='+interviewID, ""+title+"<%= Constants.INTERVIEW_ROUND_3 %>", 1050);
        }
        else {
            CRM.msgBox('Please select interview form', 400);
        }
    }

</script>
