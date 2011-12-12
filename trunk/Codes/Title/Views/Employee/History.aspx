<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%=EmsPageInfo.MenuName + CommonPageInfo.AppSepChar + EmsPageInfo.ModEmployeeHistory + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
   <%=EmsPageInfo.ComName%>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% Employee emp = (Employee)ViewData.Model;
       string funcTitle = string.Empty;
       if (emp.EmpStatusId != Constants.RESIGNED)
       {
           funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + " <a href='/Employee/Detail/" + emp.ID + "'>" +
             emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</a> » " + EmsPageInfo.ModEmployeeHistory;
       }
       else
       {
           funcTitle = "Management » Employee » " +  "<a href='/Employee/EmployeeResignList/'>" + EmsPageInfo.ModResignedEmployees + "</a> » <a href='/Employee/Detail/" + emp.ID + "'>" +
              emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</a> » " + EmsPageInfo.ModEmployeeHistory;
       }   
        //funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl)+" <a href='/Employee/Detail/" + emp.ID + "'>" +
        //     emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName + "</a> » " + EmsPageInfo.ModEmployeeHistory;
    %>
    <%= funcTitle%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Hidden("Id", ((Employee)ViewData.Model).ID)%>
    <% Employee emp = (Employee)ViewData.Model;
       Response.Write(Html.Hidden("EmpStatusId", ((Employee)ViewData.Model).EmpStatusId));
       string styleLast = "class=\"last last_off\"";
       string styleNext = "class=\"next next_off\"";
       string styleFirst = "class=\"first first_off\"";
       string stylePrev = "class=\"prev prev_off\"";
       int index = 0;
       int number = 0;
       List<sp_GetEmployeeResult> listEmp = (List<sp_GetEmployeeResult>)ViewData["ListEmployee"];
       int totalEmp = listEmp.Count();
       if (listEmp.Count > 1)
       {
           styleLast = "class=\"last last_on\"";
           styleNext = "class=\"next next_on\"";
           styleFirst = "class=\"first first_on\"";
           stylePrev = "class=\"prev prev_on\"";
           index = listEmp.IndexOf(listEmp.Where(p => p.ID == emp.ID).FirstOrDefault<sp_GetEmployeeResult>());
           if (index == 0)
           {
               styleFirst = "class=\"first first_off\"";
               stylePrev = "class=\"prev prev_off\"";
           }
           else if (index == listEmp.Count - 1)
           {
               styleLast = "class=\"last last_off\"";
               styleNext = "class=\"next next_off\"";
           }
           number = index + 1;
       }
       else if (listEmp.Count == 1)
       {
           number = listEmp.Count;
       }
    %>
    <div id="cactionbutton" style="width: 1024px !important">
        <button type="button" id="btnExport" class="button export">
            Export</button>
    </div>
    <div id="cnavigation" style="width: 1024px">
        <button type="button" id="btnLast" value="Last" <%=styleLast %>>
        </button>
        <button type="button" id="btnNext" value="Next" <%=styleNext %>>
        </button>
        <span>
            <%= number + " of " + totalEmp%></span>
        <button type="button" id="btnPre" value="Prev" <%=stylePrev %>>
        </button>
        <button type="button" id="btnFirst" value="First" <%=styleFirst %>>
        </button>
    </div>
    <div>
        <ul class="ctabs" style="width: 1024px;">
            <li class="active"><a href="javascript:void(0);" name="#profile">Profile History</a></li>
            <li><a href="javascript:void(0);" name="#hospitalHistory">Insurance Hospital History</a></li>
        </ul>
    </div>
    <div class="ctcontainer" style="width: 1024px;">
        <div class="ctcontent" id="profile" style="display: block;">
            <table id="list" class="scroll">
            </table>
            <div id="pager" class="scroll" style="text-align: center;">
            </div>
        </div>
    </div>
    <div class="ctcontainer" style="width: 1024px;">
        <div class="ctcontent" id="hospitalHistory" style="display: block;">
            <table id="listHospital" class="scroll">
            </table>
            <div id="pagerHospital" class="scroll" style="text-align: center;">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function LoadList(nameTab) {
            if (nameTab == '#profile') {
                jQuery("#list").jqGrid({
                    url: '/Employee/GetEmployeeHistory/' + $('#Id').val(),
                    datatype: 'json',
                    mtype: 'GET',
                    colNames: ['Date', 'Department', 'Sub Department', 'Job Title', 'Project', 'Manager', 'Id'],
                    colModel: [
                  { name: 'Date', index: 'Date', align: "center", width: 120 },
                  { name: 'Department', index: 'Department', align: "center", width: 120 },
                  { name: 'SubDepartment', index: 'SubDepartment', align: "center", width: 120 },
                  { name: 'JobTitle', index: 'JobTitle', align: "center", width: 120 },
                  { name: 'Project', index: 'Project', align: "center", width: 120 },
                  { name: 'Manager', index: 'Manager', align: "center", width: 120 },
                  { name: 'Id', index: 'Id', align: "center", width: 50, hidden: true}],
                    pager: jQuery('#pager'),
                    rowList: [20, 30, 50, 100, 200],
                    viewrecords: true,
                    width: 1000,
                    height: "auto",
                    imgpath: '/scripts/grid/themes/basic/images',
                    loadui: 'block',
                    onSelectRow: function (rowid, status) {
                        $('#' + rowid).removeClass('ui-state-highlight');
                    },
                    loadComplete: function () {
                        var numberRow = $("#list").getGridParam("records");
                        for (var i = 1; i <= numberRow; i++) {
                            var status = $("#" + i).find("td").find(".row_active").attr("id");
                            if (status != undefined) {
                                $("#" + i).find("td").find(".row_active").parent().addClass("row_active");
                            }
                        }
                    }
                });
            }
            else if (nameTab == '#hospitalHistory') {
                jQuery("#listHospital").jqGrid({
                    url: '/Employee/GetEmployeeInsuranceHospital/' + $('#Id').val(),
                    datatype: 'json',
                    mtype: 'GET',
                    colNames: ['Date', 'Hospital Name', 'Id'],
                    colModel: [
                  { name: 'Date', index: 'Date', align: "center", width: 40 },
                  { name: 'Hospital', index: 'Hospital', align: "center", width: 60 },
                  { name: 'Id', index: 'Id', align: "center", width: 60, hidden: true}],
                    pager: jQuery('#pagerHospital'),
                    rowList: [20, 30, 50, 100, 200],
                    viewrecords: true,
                    width: 1000,
                    height: "auto",
                    imgpath: '/scripts/grid/themes/basic/images',
                    loadui: 'block',
                    loadComplete: function () {
                        var numberRow = $("#list").getGridParam("records");
                        for (var i = 1; i <= numberRow; i++) {
                            var status = $("#" + i).find("td").find(".row_active").attr("id");
                            if (status != undefined) {
                                $("#" + i).find("td").find(".row_active").parent().addClass("row_active");
                            }
                        }
                    }
                });
            }
        }
        $(document).ready(function () {
            var nameTab = $("ul.ctabs li").find("a").attr("name");
            LoadList(nameTab)
            /* Navigator */

            $('#btnFirst').click(function () {
                var className = $('#btnFirst').attr('class');
                if (className != "first first_off") {
                    var status = $("#EmpStatusId").val();
                    var active = 1;
                    if (status == '<%=Constants.RESIGNED %>') {
                        active = 0;
                    }
                    window.location = "/Employee/Navigation/?Active=" + active + "&name=" + $('#btnFirst').val()
                + "&id=" + $('#Id').val() + "&Page=History";
                }
            });
            $('#btnPre').click(function () {
                var className = $('#btnPre').attr('class');
                if (className != "prev prev_off") {
                    var status = $("#EmpStatusId").val();
                    var active = 1;
                    if (status == '<%=Constants.RESIGNED %>') {
                        active = 0;
                    }
                    window.location = "/Employee/Navigation/?Active=" + active + "&name=" + $('#btnPre').val()
                + "&id=" + $('#Id').val() + "&Page=History";
                }
            });
            $('#btnNext').click(function () {
                var className = $('#btnNext').attr('class');
                if (className != "next next_off") {
                    var status = $("#EmpStatusId").val();
                    var active = 1;
                    if (status == '<%=Constants.RESIGNED %>') {
                        active = 0;
                    }
                    window.location = "/Employee/Navigation/?Active=" + active + "&name=" + $('#btnNext').val()
                    + "&id=" + $('#Id').val() + "&Page=History";
                }
            });
            $('#btnLast').click(function () {
                var className = $('#btnLast').attr('class');
                if (className != "last last_off") {
                    var status = $("#EmpStatusId").val();
                    var active = 1;
                    if (status == '<%=Constants.RESIGNED %>') {
                        active = 0;
                    }
                    window.location = "/Employee/Navigation/?Active=" + active + "&name=" + $('#btnLast').val()
                + "&id=" + $('#Id').val() + "&Page=History";
                }
            });
            /*---------------------*/

            /* Tab */
            $(".ctcontent").hide(); //Hide all content
            $("ul.ctabs li:first").addClass("active").show(); //Activate first tab
            $(".ctcontent:first").show(); //Show first tab content

            //On Click Event
            $("ul.ctabs li").click(function () {
                $("ul.ctabs li").removeClass("active"); //Remove any "active" class
                $(this).addClass("active"); //Add "active" class to selected tab
                $(".ctcontent").hide(); //Hide all tab content                            
                var activeTab = $(this).find("a").attr("name"); //Find the href attribute value to identify the active tab + content
                LoadList(activeTab)
                $(activeTab).fadeIn(); //Fade in the active ID content
                return false;
            });
            /*---------------------*/

            $("#btnExport").click(function () {
                var numberRow = $("#list").getGridParam("records");
                if (numberRow <= 0) {
                    CRM.msgBox("Have no data for Export !", "300");
                }
                else {
                    window.location = "/Employee/ExportForHistory/?id=" + $('#Id').val();
                }
            });

            $('#btnHistory').click(function () {
                window.location = "/Employee/History/" + $('#Id').val();
            });

        });
    </script>
</asp:Content>
