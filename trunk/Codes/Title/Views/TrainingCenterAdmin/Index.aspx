<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%=TempData["Message"]%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="chlft">
                <div class="trainingdashboard">
                    <div class="header">
                        <div class="title">Training Dashboard</div>
                        <div class="desc">Text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text text </div>
                    </div>
                    <div class="main-item">
                        <div class="item course">
                            <div class="cimg"><a href="#"></a></div>
                            <div class="ctxt">
                                <div class="headtitle">Courses</div>
                                <div class="headdesc">Browse courses, descriptions Browse courses, descriptions Browse courses, descriptions</div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingCenterAdmin/Courses/<%=Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL%>" class="pro">Professional skill</a>
                                <a href="/TrainingCenterAdmin/Courses/<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH%>" class="eng">English</a>
                            </div>
                            <div class="clrfix"></div>
                        </div>
                        <div class="item class">
                            <div class="cimg"><a href="#"></a></div>
                            <div class="ctxt">
                                <div class="headtitle">Class</div>
                                <div class="headdesc">See which courses are being offered. When and where.</div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingCenterAdmin/ProClass" class="pro">Professional skill</a>
                                <a href="/TrainingCenterAdmin/EnglishClass" class="eng">English</a>
                            </div>
                            <div class="clrfix"></div>
                        </div> 
                        <div class="item material">
                            <div class="cimg"><a href="#"></a></div>
                            <div class="ctxt">
                                <div class="headtitle">Material</div>
                                <div class="headdesc">Text text text text text text text text text text text text text text text text text text text text text text text text text text text text text</div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingMaterial/MaterialList/<%= Constants.TRAINING_MATERIAL_PROF_COURSE %>" class="pro">Professional skill</a>
                                <a href="/TrainingMaterial/MaterialList/<%= Constants.TRAINING_MATERIAL_ENG %>" class="eng">English</a>
                                <a href="/TrainingMaterial/MaterialList/<%=Constants.TRAINING_MATERIAL_CATEGORY %>" class="cat">Category</a>
                            </div>
                            <div class="clrfix"></div>
                        </div>
                        <div class="item training">
                            <div class="cimg"><a href="#"></a></div>
                            <div class="ctxt">
                                <div class="headtitle">Training Record Management</div>
                                <div class="headdesc">Text text text text text text text text text text text text text text text text text text text text text text text text text text text text text</div>
                            </div>
                             <div class="csub">
                                <a href="/TrainingCenterAdmin/EnglishLevelMapping" class="table">English Mapping Table</a>
                                <a href="/TrainingCenterAdmin/EnglishScoreSheet" class="sheet">Master English Score Sheet</a>
                                <a href="/TrainingCenterAdmin/ProfessionalCourseAttendance" class="pro">Professional Courses Attendance</a>
                                <a href="/TrainingCenterAdmin/EnglishCourseAttendance" class="eng">English Courses Attendance</a>
                            </div>
                            <div class="csub">
                                View Training Record of 
                                <input type="text" maxlength="50" title="<%= Constants.TRAINING_CENTER_DASHBOARD_RECORD_LABEL%>" style="width:200px; margin-left:10px"
                                    value="<%=Constants.TRAINING_CENTER_DASHBOARD_RECORD_LABEL%>" id="txtEmployeeName" 
                                    onfocus="ShowOnFocus(this,'<%= Constants.TRAINING_CENTER_DASHBOARD_RECORD_LABEL%>')"
                                    onblur="ShowOnBlur(this,'<%= Constants.TRAINING_CENTER_DASHBOARD_RECORD_LABEL  %>')" />
                                 <button type="button" style="float:none !important;" id="btnView" title="View" class="button view">
                                    View
                                </button>
                                <%=Html.Hidden("hidEmployeeId")%>
                            </div>
                            <div class="clrfix"></div>
                        </div>                        
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%= TrainingCenterPageInfo.FuncDashBoard + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName
        + CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $("#txtEmployeeName").autocomplete("/Library/GenericHandle/AutoCompleteHandler.ashx?func=Employee&type=2&suffixId=true",
        { employee: true, hidField: "#hidEmployeeId" });
        $("#btnView").click(function () {
            var empId = $("#hidEmployeeId").val();
            if (empId != "") {
                window.location = "/TrainingCenterAdmin/TrainingRecord?userid=" + empId;
            }
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=TrainingCenterPageInfo.ComName%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%=CommonFunc.GetCurrentMenu(Request.RawUrl, false) + TrainingCenterPageInfo.FuncDashBoard%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
