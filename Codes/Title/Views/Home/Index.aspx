<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    CRM
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Script HightChart -->
    <script type="text/javascript" src="/Scripts/hightchart/highcharts.js"></script>
    <!-- Script HightChart -->
    <style type="text/css">
        body
        {
            background: url(/Content/Images/portal.jpg) no-repeat center center;
        }
    </style>
    <!-- Notification -->
    <script src="/Scripts/Notification/ui.notificationmsg.js" type="text/javascript"></script>
    <link href="/Scripts/Notification/notificationmsg.css" rel="stylesheet" type="text/css" />
    <!-- End Notification -->
    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $(".export").click(function(){
                window.location="/Home/ExportContractList";
            });
            $('#msg1').notificationmsg({ period: 0 });
            $('#closebutton').click(function () { $('#msg1').notificationmsg('hide'); });
            showNotification();
            ShowTooltip($("a[class=showTooltip]"), $("#shareit-box"), "/Employee/EmployeeToolTip");
            ShowTooltip($("a[class=srTooltip]"), $("#shareit-box"), "/ServiceRequestAdmin/ShowTitleTooltip");
            $(function () {
                // Ẩn tất cả .accordion trừ accordion đầu tiên
                $("#accordion .pncontent:not(:first)").hide();
                $("#accordion .panel .pntitle").children().first().attr('class', 'pnup');
                // Áp dụng sự kiện click vào thẻ h3
                $("#accordion .pntitle").click(function () {
                    showhidePanel($(this));
                });

                $("#accordion .pncontent").each(function(){                    
                    if($.trim($(this).text())=='')
                    {                        
                        $(this).parent().remove();                 
                    }
                });
            });  
            
            
             $("#linkMoving,#linkPayroll,#linkAsset,#linkSkillset").click(function () {
                CRM.msgBox('<%= Resources.Message.I0003 %>', "300");
            });

            // Hight chart
            var options = {
                chart: {
                    renderTo: 'chart-container',
                    defaultSeriesType: 'line'
                },
                title: {
                    text: 'User Login Statistic'
                },
                xAxis: {
                    categories: []
                },
                yAxis: {
                    title: {
                        text: 'Number of Users'
                    }
                },
                series: []
            };

            // Split the lines
            var cats = '<%= ViewData["LoginStatistic_Title"]  %>';
            var vals = '<%= ViewData["LoginStatistic_Value"]  %>';

            var catss = cats.split(',');
            $.each(catss, function(no, cat) {
                options.xAxis.categories.push(cat);
            });
            
            var series = {
                data: []
            };
            var valss = vals.split(',');
            series.name = "Users";
            $.each(valss, function(no, val) {
                    series.data.push(parseFloat(val));
            });            
            options.series.push(series);

             // Create the chart
             var chart = new Highcharts.Chart(options);

        });


        function showhidePanel(obj)
            {
                $accordion = obj.next();
                // Kiểm tra nếu đang ẩn thì sẽ hiện và ẩn các phần tử khác
                // Nếu đang hiện thì click vào h3 sẽ ẩn
                if ($accordion.is(':hidden') === true) {
                    $("#accordion .pncontent").slideUp();                        
                    $("#accordion .pnup").attr('class', 'pndown');
                    $accordion.slideDown();
                    obj.children().first().attr('class', 'pnup');                        
                } else {
                    $accordion.slideUp();
                    obj.children().first().attr('class', 'pndown');
                }
            }
        function showNotification() {
            var animStyle = 'slide';
            <%
                String st = "0";
                if (ViewData["NotificationList"] != null 
                || ViewData["NoticeInputAnnualHolliday"] != null
                || ViewData["SrUndoneList"] != null
                || ViewData["NoticeUpdatePTOBalance"] != null
                || ViewData[CommonDataKey.PTO_NOTICE_CONFIRM] != null) {st = "1";}
             %>
            if (<%Response.Write(st);%> != '0') { 
                        $('#msg1').notificationmsg({ animation: animStyle });
                        $('#msg1').notificationmsg('show');
                    }                    
         }       
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FunctionTitle" runat="server">
    Welcome to CRM
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <%
        List<Contract> listExpired = new List<Contract>();
        List<Contract> listComming = new List<Contract>();
        
        string msgExpired = string.Empty;
        string msgComming = string.Empty;

        if (ViewData["NotificationList"] != null)
        {
            List<Contract> noticList = (List<Contract>)ViewData["NotificationList"];
            if (noticList.Count > 0)
            {
                foreach (Contract contract in noticList)
                {
                    if (contract.EndDate.Value < DateTime.Now)
                    {
                        listExpired.Add(contract);
                    }
                    else
                    {
                        listComming.Add(contract);
                    }
                }
                string msgE = "- <a href='javascript:;' onclick=\"showhidePanel($('#accordion #list-expired').prev());\">There {0} <span class=num>" + listExpired.Count + "</span> contract{1} {2} been expired</a><br />";
                string msgC = "- <a href='javascript:;' onclick=\"showhidePanel($('#accordion #list-comming').prev());\">There {0} <span class=num>" + listComming.Count + "</span> contract{1} {0} coming in next " + Constants.NOTIFICATION_DAYS.ToString() + " day(s)</a>";
                if (listExpired.Count == 1)
                {
                    msgExpired = string.Format(msgE, "is", string.Empty, "has");
                }
                else if (listExpired.Count > 1)
                {
                    msgExpired = string.Format(msgE, "are", "s", "have");
                }

                if (listComming.Count == 1)
                {
                    msgComming = string.Format(msgC, "is", string.Empty);
                }
                else if (listComming.Count > 1)
                {
                    msgComming = string.Format(msgC, "are", "s");
                }
            }
        }
        var listUndoneSr = ViewData["SrUndoneList"] as List<SR_ServiceRequest>;
        string msgUndoneSr = string.Empty;
        if (listUndoneSr != null && listUndoneSr.Count>0)
        {
            string msgS = "- <a href='javascript:;' onclick=\"showhidePanel($('#accordion #list-sr').prev());\">There {0} <span class=num>" +
                listUndoneSr.Count + "</span> service request{1} been expired</a><br />";
            if (listUndoneSr.Count > 1)
                msgUndoneSr = string.Format(msgS, "are", "s have");
            else
                msgUndoneSr = string.Format(msgS, "is", " has");
        }            
    %>
    <div id="msg1">
        <div id="modal">
            <div class="modaltop">
                <div class="modaltitle">
                    <img style="vertical-align: top; margin-right: 10px;" alt="" src="../../Content/Images/Icons/alarm_clock.png"
                        border="0" />CRM Notification</div>
                <span id="closebutton" style="cursor: pointer">
                    <img alt="Hide Popup" src="/Scripts/Notification/close_vista.gif" border="0" />
                </span>
            </div>
            <div class="modalbody">
                <% if (ViewData["NotificationList"] != null)
                   { %>
                    <p>
                        <b>Contract Notices</b></p>
                    <%=msgExpired%>
                    <%=msgComming%>
                    <p>
                    </p>
                <% } %>
                <% if (listUndoneSr != null && listUndoneSr.Count>0)
                   { %>
                    <p>
                        <b>Service Request Notices</b></p>
                    <%=msgUndoneSr%>
                    <p>
                    </p>
                <% } %>
                <%if (ViewData["NoticeInputAnnualHolliday"] != null)
                  { %>
                    <p>
                        <b>Annual Holiday Notices</b></p>
                    - <a href="/AnnualHoliday/">You have not inputted <span class="num">Annual Holiday</span>
                        for this year.</a>
                    <p>
                    </p>
                <% } %>

                <%if (ViewData["NoticeUpdatePTOBalance"] != null)
                  { %>
                    <p>
                        <b>PTO Balance Notices</b></p>
                    - PTO Balance of this month have not updated automatically, <a href="/Home/UpdateBalance"> click here</a> to update manually.
                    <p>
                    </p>
                <% } %>
                <%if (ViewData[CommonDataKey.PTO_NOTICE_CONFIRM] != null)
                  {
                      bool isPlural = false;
                      int iNumPto = (int)ViewData[CommonDataKey.PTO_NOTICE_CONFIRM];
                      if (iNumPto > 1)
                      {
                          isPlural = true;
                      }      
                %>
                    <p>
                        <b>PTO Notification</b></p>
                     - <a href="/PTOAdmin/PtoToConfirm/"><%=String.Format(Resources.Message.ResourceManager.GetString(MessageConstants.I0007),
                                (!isPlural ? "is" : "are") + " <span class='num'>" + iNumPto + "</span>").TrimEnd('.') + " before "+
                                CommonFunc.GetPtoDateTo(DateTime.Now).ToString(Constants.DATETIME_FORMAT_VIEW)
                            %>
                        </a>
                    <p>
                    </p>
                <% } %>
                
            </div>
        </div>
    </div>
    <div id="shareit-box">
        <img src='../../Content/Images/loading3.gif' alt='' />
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="chlft">
                <ul>
                    <li><a href="/Employee" class="ems"></a></li>
                    <li><a href="/JobRequest" class="jobrequest"></a></li>
                    <li><a id="A1" href="/PurchaseRequest" class="crmpurchase"></a></li>
                    <li><a href="/Candidate" class="hiring"></a></li>
                    <li><a target="_blank" href="http://lgvn11888:2222/" class="prm"></a></li>
                    <li><a id="linkLOT" href="/Exam" class="onlinetest"></a></li>
                    <li><a id="linkPTO" href="/PTOAdmin" class="pto"></a></li>
                    <%
                        var itList = new UserAdminDao().GetListITHelpDesk((int)Modules.ServiceRequestAdmin, (int)Permissions.ItHelpDesk).Select(p=>p.Name);
                        if (itList.Contains(HttpContext.Current.User.Identity.Name))
                        {
                    %>
                    <li><a id="linkDashboard" href="/ServiceRequestAdmin/DashBoard" class="dashboard"></a></li>
                    <%  } %>
                    <li><a id="A2" href="/TrainingCenterAdmin/" class="training"></a></li>
                    <li><a id="linkAsset" href="#" class="asset"></a></li>

                    <li><a id="linkMoving" href="#" class="moving"></a></li>                  
                    <li><a id="linkPayroll" href="#" class="payroll"></a></li>                    
                    <li><a id="linkSkillset" href="#" class="skillset"></a></li>
                    
                </ul>
            </td>
            <td class="chrgh">
                <div id="accordion">
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">User Login Statistic</span>
                        </div>
                        <div class="pncontent">
                            <div id="chart-container" style="width: 650px; height: 300px; margin: 0; z-index: 0;">
                            </div>
                            <table width="100%" style="margin-top: 72px">
                                <tr>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vtoday.png" />
                                        Today:
                                        <% Response.Write(ViewData["LoginStatistic_Today"]);  %>
                                    </td>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vweek.png" />
                                        This Month:
                                        <% Response.Write(ViewData["LoginStatistic_ThisMonth"]);  %>
                                    </td>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vyesterday.png" />
                                        This Year:
                                        <% Response.Write(ViewData["LoginStatistic_ThisYear"]);  %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vtoday.png" />
                                        Yesterday:
                                        <% Response.Write(ViewData["LoginStatistic_Yesterday"]);  %>
                                    </td>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vweek.png" />
                                        Last Month:
                                        <% Response.Write(ViewData["LoginStatistic_LastMonth"]);  %>
                                    </td>
                                    <td>
                                        <img src="/Content/Images/HitCounter/vyesterday.png" />
                                        Last Year:
                                        <% Response.Write(ViewData["LoginStatistic_LastYear"]);  %>
                                    </td>
                                </tr>
                                <tr valign="bottom">
                                    <td colspan="2">
                                        <img src="/Content/Images/HitCounter/stats.gif" />
                                        &nbsp; Your Web Browser:
                                        <% Response.Write(Request.Browser.Browser + " " + Request.Browser.Version);%>
                                    </td>
                                    <td align="right">
                                        <img src="/Content/Images/HitCounter/arrow_item_link.gif" /><a href="/Home/UserLoginStatistic">
                                            More detail</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">The contract list has been expired</span>
                        </div>
                        <div class="pncontent" id="list-expired">
                            <% Response.Write(CommonFunc.OutputContractListByDate(listExpired)); %>
                        </div>
                    </div>
                    <!--Service Request Notification -->
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">The service request list has been expired</span>
                        </div>
                        <div class="pncontent" id="list-sr">
                            <%if (listUndoneSr != null) { Html.RenderAction("UndoneServiceRequest"); }%>
                        </div>
                    </div>
                    <!--End Service Request Notification -->
                    <div class="panel">
                        <div class="pntitle">
                            <span class="pndown"></span><span class="pntext">The contract list is coming in next
                                <%=Constants.NOTIFICATION_DAYS.ToString()%>
                                day(s)</span>
                        </div>
                        <div class="pncontent" id="list-comming">
                            <% Response.Write(CommonFunc.OutputContractListByDate(listComming)); %>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
