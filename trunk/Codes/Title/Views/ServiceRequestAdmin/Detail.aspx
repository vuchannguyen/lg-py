<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <%  SR_ServiceRequest obj = (SR_ServiceRequest)ViewData.Model;
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        int index = 0;
        List<sp_SR_GetServiceRequest4AdminResult> listSR = (List<sp_SR_GetServiceRequest4AdminResult>)ViewData["listSR"];
        int number = 0;
        int totalPr = listSR.Count();
        if (listSR.Count > 1)
        {
            styleLast = "class=\"last last_on\"";
            styleNext = "class=\"next next_on\"";
            styleFirst = "class=\"first first_on\"";
            stylePrev = "class=\"prev prev_on\"";
            index = listSR.IndexOf(listSR.Where(p => p.ID == obj.ID).FirstOrDefault<sp_SR_GetServiceRequest4AdminResult>());
                if (index == 0)
                {
                    styleFirst = "class=\"first first_off\"";
                    stylePrev = "class=\"prev prev_off\"";                    
                }
                else if (index == listSR.Count - 1)
                {
                    styleLast = "class=\"last last_off\"";
                    styleNext = "class=\"next next_off\"";
                }
                number = index + 1;
         }
        else if (listSR.Count == 1)
        {
            number = listSR.Count;
        }
        
    %>
    <% if (listSR != null && listSR.Count > 0) 
       {%>
    <div id="cnavigation" style="width:1024px">
                    <button type="button" id="btnLast" value="Last"  <%=styleLast %> ></button>
                    <button type="button" id="btnNext" value="Next" <%=styleNext %> ></button>
                    <span><%= number + " of " + totalPr%></span>        
                    <button type="button" id="btnPre" value="Prev" <%=stylePrev %> ></button>
                    <button type="button" id="btnFirst" value="First" <%=styleFirst %> ></button>                
                </div>   
      <%}%>
    <% 
       Response.Write(Html.Hidden("ID", obj.ID)); %>
    <div id="cactionbutton">
        <table style="width: 100%">
            <tr>
                <td align="right" style="text-align: right;vertical-align:bottom; height:35px">
                    <%
                        Response.Write((string)ViewData[Constants.SR_ACTION]);
                    %>
                </td>
            </tr>
        </table>
    </div>
    <div id="list" style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
            <tr>
                <td class="ctbox">
                    <h2 class="heading">
                        SR Information</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                        <tr>
                            <td class="label">
                                Category
                            </td>
                            <td class="input" style="width: 300px">
                                <%  SR_Category objCategory = new SRCategoryDao().GetCategoryParentBySub(obj.CategoryID);
                                    Response.Write(objCategory != null ? objCategory.Name : string.Empty);%>
                            </td>
                            <td class="label">
                                Sub Category
                            </td>
                            <td class="input" style="width: 300px">
                                <%=obj.SR_Category.Name%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Title
                            </td>
                            <td colspan="3" class="input">
                                <%=   Html.Encode(obj.Title)%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Description
                            </td>
                            <td colspan="3" class="input">
                                <%=  Html.Encode(obj.Description).Replace("\r\n", "<br />")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Submiter
                            </td>
                            <td class="input">
                                <%=  obj.SubmitUser%>
                            </td>
                            <td class="label">
                                Requestor
                            </td>
                            <td class="input">
                                <%=obj.RequestUser%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Submit Date
                            </td>
                            <td class="input">
                                <%=  obj.CreateDate.ToString(Constants.DATETIME_FORMAT_SR)%>
                            </td>
                            <td class="label">
                                Due Date
                            </td>
                            <td class="input">
                                <%=  obj.DueDate.HasValue ? obj.DueDate.Value.ToString(Constants.DATETIME_FORMAT_SR) : string.Empty%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                CC List
                            </td>
                            <td class="input" colspan="3">
                                <%=obj.CCList%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Urgency
                            </td>
                            <td class="input">
                                <%=  obj.SR_Urgency.Name%>
                            </td>
                            <td class="label">
                                Related SR
                            </td>
                            <td class="input">
                                <% SR_ServiceRequest objParent = new ServiceRequestDao().GetById(obj.ParentID.HasValue ? obj.ParentID.Value : 0);
                                   if (objParent != null)
                                   {
                                       Response.Write(Html.ActionLink(Constants.SR_SERVICE_REQUEST_PREFIX + objParent.ID, "/Detail/" + obj.ParentID));
                                   }%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Status
                            </td>
                            <td class="input">
                                <%=  obj.SR_Status.Name%>
                            </td>
                            <td class="label">
                                Assigned To
                            </td>
                            <td class="input">
                                <%=obj.AssignUser%>
                            </td>
                        </tr>
                        <tr class="last">
                            <td class="label">
                                Attachment
                            </td>
                            <td class="input" colspan="3">
                                <%= CommonFunc.SplitFileName(obj.Files, Constants.SR_UPLOAD_PATH, false,',') %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
            <tr>
                <td class="ctbox">
                    <span class="fr action"><a id="btnEditSolution" title="Edit Personal Info" href="javascript:CRM.popup('/ServiceRequestAdmin/AddSolution/<%= obj.ID%>', 'Add solution for service request #<%= obj.ID%>', 400);">
                        Edit</a></span>
                    <h2 class="heading">
                        Solution</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                        <%if (string.IsNullOrEmpty(obj.SolutionLastModified) && string.IsNullOrEmpty(obj.Solution))
                          {%>
                        <tr class="last">
                            <td class="input">
                                <%="Have no data"%>
                            </td>
                        </tr>
                        <%}
                          else
                          {%>
                        <tr>
                            <td class="input">
                                <%=Html.Encode(obj.SolutionLastModified)%>
                            </td>
                        </tr>
                        <tr class="last">
                            <td class="input">
                                <%=obj.Solution != null ? Html.Encode(obj.Solution).Replace("\r\n", "<br />"): ""%>
                            </td>
                        </tr>
                        <%} %>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <% List<SR_Activity> listActivity = new ServiceRequestDao().GetListActivityBySrID(obj.ID);%>
    <br />
    <div style="width: 1024px">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
            <tr>
                <td class="ctbox">
                    <span class="fr action"><a id="btnAddNewActivity" href="javascript:void(0);">Add Activity</a>
                    </span>
                    <h2 class="heading">
                        Activity</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="grid">
                        <thead>
                            <tr>
                                <th style="width: 120px; text-align: left" class="gray">
                                    User
                                </th>
                                <th style="width: 150px; text-align: left" class="gray">
                                    Start Time
                                </th>
                                <th style="width: 150px; text-align: left" class="gray">
                                    End Time
                                </th>
                                <th style="width: 70px; text-align: left" class="gray">
                                    Total
                                </th>
                                <th style="width: 350px; text-align: left" class="gray">
                                    Description
                                </th>
                                <th style="width: 50px; text-align: left" class="gray">
                                    Action
                                </th>
                            </tr>
                        </thead>
                        <%
                            index = 1;
                            double total = 0;
                            foreach (SR_Activity activity in listActivity)
                            {
                                total += activity.Total;
                        %>
                        <tr>
                            <td style="width: 120px; text-align: left">
                                <%  Response.Write(activity.UserName); %>
                            </td>
                            <td style="width: 150px; text-align: left">
                                <%  Response.Write(activity.StartTime.ToString(Constants.DATETIME_FORMAT_FULL)); %>
                            </td>
                            <td style="width: 150px; text-align: left">
                                <%  Response.Write(activity.EndTime.ToString(Constants.DATETIME_FORMAT_FULL)); %>
                            </td>
                            <td style="width: 70px; text-align: left">
                                <%  Response.Write(CommonFunc.FormatTime(activity.Total)); %>
                            </td>
                            <td style="width: 350px; text-align: left">
                                <%  Response.Write(Html.Encode(activity.Description).Replace("\r\n", "<br />")); %>
                            </td>
                            <td style="width: 50px; text-align: center">
                                <%  //Response.Write(CommonFunc.Button("delete", "Delete",
                                        //"CRM.confirmDelete('" + Url.Action("DeleteActivity", new { @id = activity.ID }) + "');"));
                                    string funcname = string.Format("CRM.deleteItemConfirm('{0}','{1}','{2}')", Url.Action("DeleteActivity"), activity.ID, Url.Action("Detail"));
                                    Response.Write(CommonFunc.Button("delete", "Delete", funcname));
                                %>
                            </td>
                        </tr>
                        <% 
                            if (index == listActivity.Count)
                            {%>
                        <tr>
                            <td colspan="3" style="width: 200px; text-align: right;font-weight:bold">
                                Total
                            </td>
                            <td colspan="3" style="width: 200px; text-align: left;font-weight:bold">
                                <%  Response.Write(CommonFunc.FormatTime(total)); %>
                            </td>
                        </tr>
                        <% }
                            index++;
                            }%>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 600px">
        <h2 class="heading">
            History</h2>
        <table border="0" cellpadding="0" cellspacing="0" width="800" class="grid">
            <thead>
                <tr>
                    <th style="width: 150px; text-align: left" class="gray">
                        User
                    </th>
                    <th style="width: 150px; text-align: left" class="gray">
                        Action
                    </th>
                    <th style="width: 150px; text-align: left" class="gray">
                        Time
                    </th>
                </tr>
            </thead>
            <% string[] array = ((string)ViewData["WorkFlow"]).Split(Constants.SEPARATE_INVOLVE_CHAR);
               foreach (string item in array)
               {
                   string[] arrayItem = item.Split(';');
                   if (arrayItem.Count() > 1)
                   {%>
            <tr>
                <td style="width: 150px; text-align: left">
                    <%  Response.Write(!string.IsNullOrEmpty(arrayItem[0]) ? arrayItem[0] : ""); %>
                </td>
                <td style="width: 150px; text-align: left">
                    <%  Response.Write(!string.IsNullOrEmpty(arrayItem[1]) ? arrayItem[1] : ""); %>
                </td>
                <td style="width: 150px; text-align: left">
                    <%  Response.Write(!string.IsNullOrEmpty(arrayItem[2]) ? arrayItem[2] : ""); %>
                </td>
            </tr>
            <% 
}
               }
                   
            %>
        </table>
    </div>
    <br />
    <% if (ViewData["Comment"] != null)
       { %>
    <div style="width: 1024px">
        <h2 class="heading">
            Comment(s)</h2>
        <div style="height: 170px; overflow-y: scroll; overflow-x: hidden;" class="view_comment">
            <table border="0" cellpadding="0" cellspacing="0" class="tb_comment" width="100%">
                <%
                    int i = 0;
                    foreach (SR_Comment item in (IEnumerable)ViewData[CommonDataKey.PR_COMMENT])
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
                            <%= (!string.IsNullOrEmpty(item.Poster) ? "(" + item.PostTime + ")" : "")%></span>
                        <br />
                        <% if (!string.IsNullOrWhiteSpace(item.Contents))
                           { %>
                        <%= Html.Encode(item.Contents).Replace("\r\n", "<br />")%>
                        <br />
                        <% } %>
                        <%= CommonFunc.SplitFileName(item.Files, Constants.SR_UPLOAD_PATH, false)%>
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
        <%using (Html.BeginForm("AddComment", "ServiceRequestAdmin", FormMethod.Post, new { id = "addForm", @class = "form", enctype = "multipart/form-data" }))
          {%>
        <table border="0" cellpadding="0" cellspacing="0" class="edit" width="1024px">
            <tr>
                <td style="width: 600px;">
                    <%= Html.Hidden("ServiceRequestID", (obj.ID.ToString()))%>
                    <%= Html.TextArea("Contents", "", new { @Style = "width: 600px;height:80px", @maxlength = "500" })%>
                </td>
                <td style="width: 100px;">
                    <input type="submit" id="btnPost" class="btnPost" value="Post" />
                </td>
                <td valign="top">
                    <table id="tblUpload">
                        <tr>
                            <td>
                                <input type="file" name="file" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="file" name="file" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input type="file" name="file" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <% SR_ServiceRequest obj = (SR_ServiceRequest)ViewData.Model;

       Response.Write(ServiceRequestPageInfo.ComName + CommonPageInfo.AppSepChar + ServiceRequestPageInfo.FuncNameDetail + CommonPageInfo.AppSepChar + CommonPageInfo.AppName);
    %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #cactionbutton button.button
        {
            float: none;
            padding-left: 25px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            /* Navigator */
            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    window.location = "/ServiceRequestAdmin/Navigation/?name=" + $('#btnFirst').val()
                + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    window.location = "/ServiceRequestAdmin/Navigation/?name=" + $('#btnPre').val()
                    + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    window.location = "/ServiceRequestAdmin/Navigation/?name=" + $('#btnNext').val()
                    + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {

                    window.location = "/ServiceRequestAdmin/Navigation/?name=" + $('#btnLast').val()
                + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            /*---------------------End navigation*/
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

            $("#btnEditSRInformation").click(function () {

            })

            $("#btnEditSolution").click(function () {

            })

            $("#btnAddNewActivity").click(function () {
                CRM.popup('<%=Url.Action("AddActivity", new {@id=((SR_ServiceRequest)ViewData.Model).ID})%>',
                    "Add Activity for Service Request #<%=Constants.SR_SERVICE_REQUEST_PREFIX%>" +
                    '<%=((SR_ServiceRequest)ViewData.Model).ID%>', 500);
            })
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= ServiceRequestPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% SR_ServiceRequest obj = (SR_ServiceRequest)ViewData.Model;
       Response.Write(CommonFunc.GetCurrentMenu(Request.RawUrl) + Constants.SR_SERVICE_REQUEST_PREFIX + obj.ID);
    %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
