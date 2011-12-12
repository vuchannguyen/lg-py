<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%=TempData["Message"]%>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="chlft">
                <div class="trainingdashboard">
                    <div class="header">
                        <div class="title">
                            <%=TrainingCenterPageInfo.FuncTrainingDashboard%>
                        </div>
                        <div class="desc">Welcome to the LogiGear Viet Nam’s Training Record Management 
                            system.
                            <br />
                            This portal allows users to access and monitor their training metrics, in 
                            addition to retrieving materials for class or self-study.</div>
                    </div>
                    <div class="main-item">
                        <div class="item training">
                            <div class="cimg"></div>
                            <div class="ctxt">
                                <div class="headtitle">
                                    <%=TrainingCenterPageInfo.FuncTrainingRecord%>
                                </div>
                                <div class="headdesc">Please select an option below to begin</div>
                            </div>
                             <div class="csub">
                                <a href="/TrainingCenterAdmin/EnglishLevelMapping" class="table">
                                    <%=TrainingCenterPageInfo.FuncEngLishLevelMapping%>
                                </a>
                                <a href="/TrainingCenterAdmin/EnglishScoreSheet" class="sheet">
                                    <%=TrainingCenterPageInfo.FuncScoreSheet%>
                                </a>
                                <a href="/TrainingCenterAdmin/ProfessionalCourseAttendance" class="pro">
                                    <%=TrainingCenterPageInfo.FuncCourseAttendance_Pro%>
                                </a>
                                <a href="/TrainingCenterAdmin/EnglishCourseAttendance" class="eng">
                                    <%=TrainingCenterPageInfo.FuncCourseAttendance_End%>
                                </a>
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
                        <div class="item course">
                            <div class="cimg"></div>
                            <div class="ctxt">
                                <div class="headtitle"><%=TrainingCenterPageInfo.FuncCourses%></div>
                                <div class="headdesc">General information of courses and availability for the 
                                    current year </div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingCenterAdmin/Courses/<%=Constants.TRAINING_CENTER_COURSE_TYPE_PRO_SKILL%>" class="pro">
                                    <%=TrainingCenterPageInfo.FuncChildPro %>
                                </a>
                                <a href="/TrainingCenterAdmin/Courses/<%=Constants.TRAINING_CENTER_COURSE_TYPE_ENGLISH%>" class="eng">
                                    <%=TrainingCenterPageInfo.FuncChildEng %>
                                </a>
                            </div>
                            <div class="clrfix"></div>
                        </div>
                        <div class="item class">
                            <div class="cimg"></div>
                            <div class="ctxt">
                                <div class="headtitle"><%=TrainingCenterPageInfo.FuncClasses%></div>
                                <div class="headdesc">Information of classes currently being offered at LogiGear 
                                    Viet Nam</div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingCenterAdmin/ProClass" class="pro">
                                    <%=TrainingCenterPageInfo.FuncChildPro %>
                                </a>
                                <a href="/TrainingCenterAdmin/EnglishClass" class="eng">
                                    <%=TrainingCenterPageInfo.FuncChildEng %>
                                </a>
                            </div>
                            <div class="clrfix"></div>
                        </div> 
                        <div class="item material">
                            <div class="cimg"></div>
                            <div class="ctxt">
                                <div class="headtitle"><%=TrainingCenterPageInfo.FuncMaterial%></div>
                                <div class="headdesc">Available soft copies of books and resources</div>
                            </div>
                            <div class="csub">
                                <a href="/TrainingMaterial/SubList?type=<%=Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE%>" class="pro">
                                    <%=TrainingCenterPageInfo.FuncChildPro %>
                                </a>
                                <a href="/TrainingMaterial/SubList?type=<%=Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE%>" class="eng">
                                    <%=TrainingCenterPageInfo.FuncChildEng %>
                                </a>
                                <a href="/TrainingMaterial/SubList?type=<%=Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY%>" class="cat">
                                    <%=TrainingCenterPageInfo.FuncChildCat %>
                                </a>
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
<%=CommonFunc.GetCurrentMenu(Request.RawUrl, true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
