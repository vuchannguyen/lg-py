<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="CRM.Library.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Professional Class Detail - CRM
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    Training Center
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>    
    <%  Training_Class request = (Training_Class)ViewData.Model;           
    %>    
    <%=Html.Hidden("ID", request.ID)%>
    <% 
        if (request == null)
            Response.Redirect("/EnglishClass");
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";

        List<int> classList = new List<int>();
        classList = (List<int>)ViewData[CommonDataKey.TRAINING_CENTER_CLASS_LIST];
        int lastID = 0;
        int firstID = 0;
        int nextID = 0;
        int preID = 0;
        int number = 0;
        int total = 0;

        int index = 0;
        total = classList.Count;
        if (classList.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = classList.IndexOf(request.ID);
            if (index == 0)
            {
                styleFirst = "class=\"first first_off\"";
                stylePrev = "class=\"prev prev_off\"";
            }
            else if (index == classList.Count - 1)
            {
                styleLast = "class=\"last last_off\"";
                styleNext = "class=\"next next_off\"";
            }
            number = index + 1;
        }
        else if (classList.Count == 1)
        {
            number = classList.Count;
        }

        if (classList != null && classList.Count > 0)
        {
            lastID = classList[classList.Count - 1]; ;
            firstID = classList[0];
            nextID = 0;
            if (index + 1 < classList.Count)
                nextID = classList[index + 1];
            else
                nextID = classList[classList.Count - 1];

            preID = 0;
            if ((index - 1) == -1)
                preID = classList[0];
            else if (index > 0)
                preID = classList[index - 1];

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
                        Class Information</h2>
                </td>
                <td align="right">
                   <a href="javascript:void(0)" onclick="CRM.popup('/TrainingCenterAdmin/EditClass/<%=request.ID %>?page=ProClassDetail', 'Edit Professional Class - <%=request.ClassId %>', '700')">Edit</a>
                   
                   
                </td>
            </tr>
        </table>
    </div>
    <div id="list" style="width: 1024px">
        
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label" style="width:18%">
                    Class ID
                </td>
                <td class="input"  style="padding-left: 10px; height: 30px; width:25%"  >
                   <%=request.ClassId %>
                </td>
                <td class="label" style="width:20%">
                    Course Name
                </td>
                <td class="input" style="padding-left: 10px; height: 30px; width:30%"  >
                    <%=request.Training_Course.Name %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Type
                </td>
                <td class="input" style="padding-left: 10px; height: 30px;" >
                    <%=Html.Encode(ViewData[CommonDataKey.TRAINING_CENTER_SKILL_TYPE])%>
                </td>
                <td class="label">
                    Status
                </td>
                <td class="input" style="padding-left: 10px; height: 30px;" >
                    <strong style="color:rgb(0,128,0)">
                        <%=request.Training_RegStatus.Name%></strong>
                </td>
            </tr>
            <tr>
                <td class="label" >
                    Duration(hrs)
                </td>
                <td class="input"  style="padding-left: 10px; height: 30px;" >
                    <strong style="color:rgb(0,128,0)">
                        <%=request.Training_Course.Duration.HasValue ? request.Training_Course.Duration.Value.ToString() :""%></strong>
                </td>
                <td class="label" >
                    Start Date 
                </td>
                <td class="input"  style="padding-left: 10px; height: 30px;" >
                    <strong style="color:rgb(0,128,0)">
                        <%=Html.Encode(request.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW))%></strong>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Date Time
                </td>
                <td class="input"  style="padding-left: 10px; height: 30px;" >
                    <strong style="color:rgb(0,128,0)">
                        <%=Html.Encode(request.ClassTime)%></strong>
                </td>
                <td class="label">
                    # of Attendees
                </td>
                <td class="input"  style="padding-left: 10px; height: 30px;" >
                    <% Response.Write(request.AttendeeQuantity);  %>
                </td>
            </tr>

            <tr>
                <td class="label">
                    Instructor
                </td>
                <td colspan="3" class="input"  style="padding-left: 10px; height: 30px;" >
                    <strong style="color:rgb(0,128,0)">
                        <%=String.IsNullOrEmpty(request.Instructors) ? "" : request.Instructors.TrimEnd(';').Replace(";",", ")%>
                    </strong>
                </td>                
            </tr>
            <tr>
                <td class="label">
                    Objectives
                </td>
                <td class="input" colspan=3  style="padding-left: 10px; height: 30px;" >
                    <% Response.Write(request.Training_Course.Objectives !=  null ? Server.HtmlEncode(request.Training_Course.Objectives).Replace("\r\n", "<br />"):"");%>
                </td>
                
            </tr>            
            <tr>
                <td class="label">
                    Course Overview
                </td>
                <td class="input" colspan=3  style="padding-left: 10px; height: 30px;" >
                    <% Response.Write(request.Training_Course.Overview != null ? Server.HtmlEncode(request.Training_Course.Overview).Replace("\r\n", "<br />"): "");%>
                </td>
            </tr>
           <tr  class="last">
                <td class="label" >
                    Registration Requirement
                </td>
                <td class="input" colspan=3 style="padding-left: 10px; height: 30px;" >
                    <% Response.Write(request.Training_Course.Requirements != null ? Server.HtmlEncode(request.Training_Course.Requirements).Replace("\r\n", "<br />") : "");%>
                </td>                
            </tr>                    
        </table>
        <br />
        <div style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td>
                    <h2 class="heading">
                        Attendees</h2>
                </td>
                <td align="right">
                   <a href="javascript:void(0)" onclick="CRM.popup('/TrainingCenterAdmin/UpdateAttendees/<%=request.ID %>?page=ProClassDetail', 'Update Attendees - <%=request.ClassId %>', '800')">Update Attendees</a>
                </td>
            </tr>
        </table>
    </div>        
        
        <table id="tblAttendeesList" border="0" cellpadding="0" cellspacing="0" width="100%" class="grid">
                <thead>
                    <tr>
                        <th style="width:70px;text-align:center" class="gray">
                            Emp ID
                        </th>
                        <th style="width:300px;text-align:center" class="gray">
                            Full Name
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Deparment
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Result
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Remark
                        </th>                        
                    </tr>
                 </thead>
                 <% List<sp_TC_GetListAttendeesOfClassResult> list = (List<sp_TC_GetListAttendeesOfClassResult>)ViewData[CommonDataKey.TRAINING_CENTER_ATTEND_LIST];
                    if (list != null)
                    {
                        foreach (sp_TC_GetListAttendeesOfClassResult item in list)
                        {
                            %>
                            <tr>
                                <td>
                                <%  Response.Write("<a id=" + item.ID + " target='_blank' class='showTooltip' href='/Employee/Detail/" + item.ID + "'>" + item.ID + "</a>"); %>
                                </td>
                                <td>
                                    <% Response.Write(item.DisplayName); %>
                                </td>
                                <td>
                                    <% Response.Write(item.Department); %>
                                </td>
                                <td>
                                    <% Response.Write(item.Result); %>
                                </td>
                                <td>
                                    <% Response.Write(item.Remark != null ? Server.HtmlEncode(item.Remark).Replace("\r\n", "<br />") : ""); %>
                                </td>
                            </tr>
                      <%} %>
                <% } %>
         </table>
    </div>
    <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("#tblAttendeesList").find("a[class=showTooltip]").each(function () {
                ShowTooltip($(this), $("#shareit-box"), "/TrainingCenterAdmin/EmployeeToolTip");    
            });
            $("#Role").change(function () {
                navigateWithReferrer("/TrainingCenterAdmin/ChangeRole/?RoleId=" + $(this).val());
            });
            $("#btnRefesh").click(function () {
                window.location = "/TrainingCenterAdmin/Detail/" + $("#RequestId").val();
            });
            /* Navigator */
            $('#btnFirst').click(function () {

                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/TrainingCenterAdmin/ProClassDetail/" + $('#btnFirst').val();
                }
            });
            $('#btnPre').click(function () {

                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/TrainingCenterAdmin/ProClassDetail/" + $('#btnPre').val();
                }
            });
            $('#btnNext').click(function () {

                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/TrainingCenterAdmin/ProClassDetail/" + $('#btnNext').val();
                }
            });
            $('#btnLast').click(function () {

                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {
                    window.location = "/TrainingCenterAdmin/ProClassDetail/" + $('#btnLast').val();
                }
            });
            /*---------------------*/        
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% Training_Class request = (Training_Class)ViewData.Model;
       string funcTitle = string.Empty;
       funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + TrainingCenterPageInfo.FuncClasses + CommonPageInfo.AppDetailSepChar +
           "<a href='/TrainingCenterAdmin/ProClass'>" + TrainingCenterPageInfo.FuncChildPro + "</a> » " + request.ClassId;
    %>
    <%= funcTitle%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginRoles" runat="server">
    <% if (ViewData["Role"] != null)
        {%>
    <span class="bold">Login As: </span>
    <%=Html.DropDownList("Role", null, new { @style = "width:180px" })%>
    <%} %>
</asp:Content>