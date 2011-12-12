<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Import Namespace="CRM.Library.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    Job Request Detail - CRM
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ModuleName" runat="server">
    Job Request 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>    
    <%  sp_GetJobRequestResult request = (sp_GetJobRequestResult)ViewData.Model; 
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        int index = 0;
        List<sp_GetJobRequestResult> listJR = (List<sp_GetJobRequestResult>)ViewData["ListJR"];
        int number = 0;
        int totalPr = listJR.Count();
        if (listJR.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = listJR.IndexOf(listJR.Where(p => p.ID == request.ID).FirstOrDefault<sp_GetJobRequestResult>());
                if (index == 0)
                {
                    styleFirst = "class=\"first first_off\"";
                    stylePrev = "class=\"prev prev_off\"";                    
                }
                else if (index == listJR.Count - 1)
                {
                    styleLast = "class=\"last last_off\"";
                    styleNext = "class=\"next next_off\"";
                }
                number = index + 1;
         }
        else if (listJR.Count == 1)
        {
            number = listJR.Count;
        }
        
    %>
    
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td align="right" style="text-align:right">
                    <%
                        string sAction = ViewData[CommonDataKey.JR_ACTIONS] as string;
                        if (!string.IsNullOrEmpty(sAction))
                        {
                            Response.Write(sAction);
                        }
                    %>
                </td>
            </tr>
        </table>
    </div>
    <% if (listJR != null && listJR.Count > 0) %>
    <% { %>
    <div id="cnavigation" style="width:1024px">
                    <button type="button" id="btnLast" value="Last"  <%=styleLast %> ></button>
                    <button type="button" id="btnNext" value="Next" <%=styleNext %> ></button>
                    <span><%= number + " of " + totalPr%></span>        
                    <button type="button" id="btnPre" value="Prev" <%=stylePrev %> ></button>
                    <button type="button" id="btnFirst" value="First" <%=styleFirst %> ></button>                
    </div>   
    <% } %>
    <%=Html.Hidden("ID", request.ID)%>
    <%  %>
    <div id="list" style="width: 1024px">
        <h2 class="heading">
            JR Information</h2>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
            <tr>
                <td class="label" style="width:18%">
                    Request
                </td>
                <td class="input"  style="width:25%"  >
                   <%=Constants.JOB_REQUEST_PREFIX %> <%=Html.Label(request.ID.ToString()) %>
                </td>
                <td class="label" style="width:20%">
                    Request Date
                </td>
                <td class="input" style="width:30%"  >
                    <%=Html.Label(request.RequestDate.ToString(Constants.DATETIME_FORMAT_VIEW)) %>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Requestor
                </td>
                <td class="input">
                    <%=Html.Encode(request.RequestorName)%>
                </td>
                <td class="label">
                    Status
                </td>
                <td class="input">
                    <span style="color: Red">
                        <%=Html.Label(request.StatusName)%></span>
                </td>
            </tr>
            <tr>
                <td class="label" >
                    Resolution
                </td>
                <td class="input" >
                    <span style="color: Red">
                        <%=Html.Label(request.ResolutionName)%></span>
                </td>
                <td class="label" >
                    Forwarded to
                </td>
                <td class="input" >
                    <span style="color: Red">
                        <%=Html.Encode(request.AssignName)%></span>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Department
                </td>
                <td class="input">
                    <%=Html.Label(request.Department)%>
                </td>
                <td class="label">
                    Sub Department
                </td>
                <td class="input">
                    <%=Html.Label(request.SubDepartment)%>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Position
                </td>
                <td class="input" >
                    <%=Html.Label(request.Position)%>
                </td>
                <td class="label">
                    Expected Start Date
                </td>
                <td class="input" >
                    <% if (request.ExpectedStartDate != null)
                       {
                           Response.Write(Html.Label(request.ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW)));
                       }
                    %>
                </td>
            </tr>
           <tr>
                <td class="label" >
                    Quantity
                </td>
                <td class="input">
                    <% if (request.Quantity != null)
                       {
                           Response.Write(Html.Label(request.Quantity.ToString()));
                       }
                       else
                       {
                           Response.Write(Html.Label(Constants.NODATA));
                       }  
                    %>
                </td>
                <td class="label" >
                    Request Type
                </td>
                <td class="input">
                    <% 
                        string requestTypeName = Constants.JR_REQUEST_TYPE.FirstOrDefault(
                            p=> p.Value == request.RequestTypeId.ToString()).Text;
                        Response.Write(requestTypeName);
                    %>
                </td>
            </tr>
            <tr>
                <td class="label" >
                    Salary Suggestion
                </td>
                <td colspan="3" class="input">
                    <% if (!string.IsNullOrEmpty(request.SalarySuggestion))
                       {
                           if ((bool)ViewData[CommonDataKey.JR_CAN_VIEW_SALARY])
                               Response.Write(EncryptUtil.Decrypt(request.SalarySuggestion));
                           else
                               Response.Write(Constants.PRIVATE_DATA);
                       }
                       else
                       {
                           Response.Write(Constants.JR_EMPTY_HOLDER);
                       }  
                    %>
                </td>
            </tr>
            <tr>
                <td class="label" >
                    Justification
                </td>
                <td colspan="3"  class="input">
                    <% if (request.Justification != null)
                       {
                           Response.Write(Html.Encode(request.Justification.ToString()));
                       }
                       else
                       {
                           Response.Write(Html.Label(Constants.NODATA));
                       }  
                    %>
                </td>
            </tr>
            <tr>
                <td class="label" >
                    CC List
                </td>
                <td colspan="3"  class="input">
                    <% if (request.CCList != null)
                       {
                           Response.Write(Html.Encode(request.CCList.ToString()));

                       }
                       else
                       {
                           Response.Write(Html.Label(Constants.NODATA));
                       }  
                    %>
                </td>
            </tr>
            <tr>
                <td class="label last">
                    History
                </td>
                <td colspan="3" class="input last" style="padding:2px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="grid">
                        <thead>
                            <tr>
                                <th style="width:30px;text-align:center" class="gray">
                                    No
                                </th>
                                <th style="width:300px;text-align:left" class="gray">
                                    Name
                                </th>
                                <th style="width:200px;text-align:left" class="gray">
                                    Action
                                </th>
                                <th style="width:200px;text-align:left" class="gray">
                                    Date
                                </th>
                            </tr>
                        </thead>
                        <% string[] array = ((string)ViewData["WorkFlow"]).Split(Constants.SEPARATE_INVOLVE_CHAR);
                           int z = 1;
                           foreach (string item in array)
                           {
                               string[] arrayItem = item.Split(';');
                               if (arrayItem.Count() > 1)
                               {%>
                        <tr>
                            <td style="width:30px;text-align:center">
                                 <%  Response.Write(z.ToString()); %>
                            </td>
                            <td style="width:300px;text-align:left">
                                <%  Response.Write(!string.IsNullOrEmpty(arrayItem[0]) ? arrayItem[0] : ""); %>
                            </td>
                            <td style="width:200px;text-align:left">
                                <%  Response.Write(!string.IsNullOrEmpty(arrayItem[1]) ? arrayItem[1] : ""); %>
                            </td>
                            <td style="width:200px;text-align:left">
                                <%  Response.Write(!string.IsNullOrEmpty(arrayItem[2]) ? arrayItem[2] : ""); %>
                            </td>
                        </tr>
                        <% 
                               }
                            z++;
                           }
                   
                        %>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <h2 class="heading">
            Successful Candidates</h2>
        <table id="tblSuccessfulCandidate" border="0" cellpadding="0" cellspacing="0" width="100%" class="grid">
                <thead>
                    <tr>
                        <th style="width:70px;text-align:center" class="gray">
                            Req#
                        </th>
                        <th style="width:300px;text-align:center" class="gray">
                            Candidate
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Emp ID
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Job Title
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Actual Start Date
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Gender
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Probation Salary
                        </th>
                        <th style="width:200px;text-align:center" class="gray">
                            Contracted Salary
                        </th>
                    </tr>
                </thead>
                <% List<sp_GetJobRequestItemListResult> list = (List<sp_GetJobRequestItemListResult>)ViewData["ItemList"];
                   if (list != null)
                   {
                       foreach (sp_GetJobRequestItemListResult item in list)
                       {
                            %>
                            <tr>
                                <td style="text-align:center">
                                     <%  Response.Write("<a id=" + item.ID + " class='showTooltip' href='#'" + item.ID +"'>" + Constants.JOB_REQUEST_ITEM_PREFIX + item.ID + "</a>"); %>
                                </td>
                                <td style="text-align:left">
                                     <%  Response.Write(item.Candidate); %>
                                </td>
                                <td style="text-align:center">
                                     <%  Response.Write(item.EmpID); %>
                                </td>
                                <td style="text-align:center">
                                     <%  Response.Write(item.DisplayName); %>
                                </td>
                                <td style="text-align:center">
                                     <%  
                                         if (item.ActualStartDate.HasValue)
                                         {
                                             Response.Write(item.ActualStartDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW));                 
                                         }
                                     %>
                                </td>
                                <td style="text-align:center">
                                     <% 
                                         if (item.Gender.HasValue)
                                         {
                                             Response.Write(item.Gender.Value ? "Male" : "Female");
                                         }    
                                    %>
                                         
                                </td>
                                <td style="text-align:center">
                                     <% Response.Write(!string.IsNullOrEmpty(item.ProbationSalary) ? 
                                            EncryptUtil.Decrypt(item.ProbationSalary) : ""); %>
                                </td>
                                <td style="text-align:center">
                                     <%  Response.Write(!string.IsNullOrEmpty(item.ContractedSalary) ?
                                             EncryptUtil.Decrypt(item.ContractedSalary) : ""); %>
                                </td>
                            </tr>
                            <%
                        }
                     }
                     %>
        </table>
        <br />
        <% if (ViewData["CommentCount"] != null)
           { %>
        <h2 class="heading">
            Comment(s)</h2>
        <div style="height: 170px; overflow-y: scroll; overflow-x: hidden;"
            class="view_comment">
            <table border="0" cellpadding="0" cellspacing="0" class="tb_comment" width="100%">
                <%
                    int i = 0;
                    foreach (JobRequestComment item in (IEnumerable)ViewData["Comment"])
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
                        <%= item.Contents.Replace("\n", "<br />")%>
                    </td>
                </tr>
                <%
i++;
                  }
                %>
            </table>
        </div>
        <%  } %>
        <h2 class="heading">
            Post new comment</h2>
        <%using (Html.BeginForm("AddComment", "JobRequest", FormMethod.Post, new { id = "addForm", @class = "form" }))
          {%>
        <table border="0" cellpadding="0" cellspacing="0" class="edit" width="100%">
            <tr>
                <td>
                    <%= Html.Hidden("RequestId", (request.ID.ToString()))%>
                    <%= Html.TextArea("Contents", "", new { @Style = "width: 890px;height:46px",@maxlength="500" })%>
                </td>
                <td>
                    <input type="submit" id="btnPost" class="btnPost" value="Post" aa />
                </td>
            </tr>
        </table>
        <% } %>
    </div>
    <div id="shareit-box">    
        <img src='../../Content/Images/loading3.gif' alt='' />    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            /* Navigator */
            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/JobRequest/Navigation/?name=" + $('#btnFirst').val()
                + "&id=" + $('#ID').val();
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/JobRequest/Navigation/?name=" + $('#btnPre').val()
                    + "&id=" + $('#ID').val();
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/JobRequest/Navigation/?name=" + $('#btnNext').val()
                    + "&id=" + $('#ID').val();
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {
                    window.location = "/JobRequest/Navigation/?name=" + $('#btnLast').val()
                + "&id=" + $('#ID').val();
                }
            });
            /*---------------------End navigation*/
            $("#tblSuccessfulCandidate").find("a[class=showTooltip]").each(function () {
                ShowTooltip($(this), $("#shareit-box"), "/JobRequest/CandidateTooltip");    
            });
            $("#Role").change(function () {
                navigateWithReferrer("/JobRequest/ChangeRole/?RoleId=" + $(this).val());
            });
            $("#btnRefesh").click(function () {
                window.location = "/JobRequest/Detail/" + $("#RequestId").val();
            });

            $("#addForm").validate({
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
                    Contents: {
                        required: true
                    }
                }
            });
        });
    </script>
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% sp_GetJobRequestResult request = (sp_GetJobRequestResult)ViewData.Model;
       string funcTitle = string.Empty;
       funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + Constants.JOB_REQUEST_PREFIX + request.ID;
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