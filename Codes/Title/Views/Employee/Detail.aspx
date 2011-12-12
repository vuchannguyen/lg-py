<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <%  Employee emp = (Employee)ViewData.Model;
        string styleLast = "class=\"last last_off\"";
        string styleNext = "class=\"next next_off\"";
        string styleFirst = "class=\"first first_off\"";
        string stylePrev = "class=\"prev prev_off\"";
        int index = 0;
        List<sp_GetEmployeeResult> listEmp = (List<sp_GetEmployeeResult>)ViewData["ListEmployee"];
        int number = 0;
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
        string styleCssFisrst = "display:";
        string styleCssThird = "display:none";
           
        if (emp.EmpStatusId == Constants.RESIGNED)
        {
            styleCssFisrst = "display:none";
            styleCssThird = "display:";               
        }
        if (emp.EmpStatusId == Constants.RESIGNED && emp.DeleteFlag == true)
        {
            styleCssThird = "display:none";
        } 
    %>
    <div id="cactionbutton" style="width: 1024px">
        <button type="button" style="<%=styleCssFisrst %>" id="btnResign" title="Resign" class="button resign">Resign</button> 
            <button type="button"  id="btnHistory" title="History"
                class="button history">History</button> 
                <button type="button" style="<%=styleCssThird %>"
                    id="btnReActive" title="ReActive" class="button reactive">ReActive</button>
        <button type="button" id="btnContract" title="Contract Renewal" class="button contract">Contract
            Renewal</button>             
    </div>
    <div id="cnavigation" style="width:1024px">
                    <button type="button" id="btnLast" value="Last"  <%=styleLast %> ></button>
                    <button type="button" id="btnNext" value="Next" <%=styleNext %> ></button>
                    <span><%= number + " of " + totalEmp%></span>        
                    <button type="button" id="btnPre" value="Prev" <%=stylePrev %> ></button>
                    <button type="button" id="btnFirst" value="First" <%=styleFirst %> ></button>                
                </div>   
    <%=Html.Hidden("ID", emp.ID)%>
    <%=Html.Hidden("EmpStatusId", emp.EmpStatusId)%>
    <%=Html.Hidden("CVFile", emp.CVFile)%>
    <%=Html.Hidden("Photograph", emp.Photograph)%>
    <%=Html.Hidden("UpdateDate", emp.UpdateDate.ToString())%>
   <%Response.Write(Html.Hidden("empFullName", (emp.FirstName
                + " " + emp.MiddleName + " "
                + emp.LastName).Replace(" ", "_"))); %> 
    <div class="form">
    <table cellspacing="0" cellpadding="0" border="0" width="1024px" id="list">
        <tbody>
            <tr>
                <td style="padding:0px;">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditPersonal" title="Edit Personal Info" href="javascript:void(0);">
                                        Edit</a></span>
                                    <% } %><h2>
                                        Personal Info</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="ccbox">                                    
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                                            <tbody>
                                                <tr>
                                                    <td valign="top" width="100%" style="padding:0px;">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class"table_detail">
                                                            <tr>
                                                                <td class="label" style="width: 110px;">
                                                                    Full Name
                                                                </td>
                                                                <td class="input">
                                                                    <span class="color_green_bold">
                                                                        <%=emp.FirstName + " " + emp.MiddleName + " " +  emp.LastName%></span>
                                                                </td>
                                                                <td class="label" style="width:110px">
                                                                    VN Name
                                                                </td>
                                                                <td class="input">
                                                                    <span class="color_green_bold">
                                                                        <%=emp.VnFirstName + " " + emp.VnMiddleName + " " + emp.VnLastName%></span>
                                                                </td>
                                                                <td class="label" style="width: 110px">
                                                                    Date of Birth
                                                                </td>
                                                                <td class="input">
                                                                    <%= emp.DOB.HasValue ? emp.DOB.Value.ToString(Constants.DATETIME_FORMAT_VIEW) : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    Place of Birth 
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.POB)? emp.POB:Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                    VN Place of Birth
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.VnPOB) ? emp.VnPOB : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                   Place of Origin
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.PlaceOfOrigin) ? emp.PlaceOfOrigin : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    VN Place of Origin 
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.VnPlaceOfOrigin) ? emp.VnPlaceOfOrigin : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                    Nationality
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.Nationality) ? emp.Nationality : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                   Gender
                                                                </td>
                                                                <td class="input">
                                                                    <%=emp.Gender.HasValue ? (emp.Gender == Constants.MALE ? "Male" : "Female") : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    Degree
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.Degree)?emp.Degree:Constants.NODATA%>
                                                                </td>
                                                                 <td class="label">
                                                                    Other Degree
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.OtherDegree) ? emp.OtherDegree : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                   ID Number</td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.IDNumber) ? emp.IDNumber : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label">
                                                                    Issue Date 
                                                                </td>
                                                                <td class="input">
                                                                    <%=emp.IssueDate.HasValue ? (emp.IssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW)) : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                    Issue Location
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.IDIssueLocation) ? emp.IDIssueLocation : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                   VN Issue Location
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.VnIDIssueLocation) ? emp.VnIDIssueLocation : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                            <tr >
                                                                <td class="label">
                                                                    Race 
                                                                </td>
                                                                <td class="input">
                                                                    <%= !string.IsNullOrEmpty(emp.Race)?emp.Race:Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                    Religion
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.Religion) ? emp.Religion : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                   Current Status
                                                                </td>
                                                                <td class="input">
                                                                    <span class="color_green_bold">
                                                                        <%=emp.EmpStatusId.HasValue ? (emp.EmployeeStatus.StatusName) : Constants.NODATA%></span>
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr class="last">
                                                                <td class="label">
                                                                    Married Status 
                                                                </td>
                                                                <td class="input">
                                                                    <%=emp.MarriedStatus.HasValue ? (emp.MarriedStatus == Constants.SINGLE ? "Single" : "Married") : Constants.NODATA%>
                                                                </td>
                                                                <td class="label">
                                                                    Major
                                                                </td>
                                                                <td class="input">
                                                                    <%=!string.IsNullOrEmpty(emp.Major) ? emp.Major : Constants.NODATA%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top" width="130">
                                                                    <%
                                                                        string labelPhoto = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
                                                                        if (ViewData.Model == null)
                                                                        {
                                                                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (string.IsNullOrEmpty(((CRM.Models.Employee)ViewData.Model).Photograph))
                                                                            {
                                                                                Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                            }
                                                                            else
                                                                            {
                                                                                labelPhoto = "<a id='thumb1' href='" + Constants.IMAGE_PATH + ((CRM.Models.Employee)ViewData.Model).Photograph
                                                                                                + "' class='highslide' onclick='return hs.expand(this)'>"
                                                                                                + "<img id='imgPhoto' src='" + Constants.IMAGE_PATH + ((CRM.Models.Employee)ViewData.Model).Photograph
                                                                                                + "' alt='Highslide JS' title='Click to enlarge' width='120px' /></a>";
                                                                                Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                            }
                                                                        }
                                                                    %>
                                                                </td>
                                                                <td valign="top">
                                                                    <input type="button" class="upload_image" id="btnChangePhoto" value="" title="Change Photo" />
                                                                    <%  string stylePhoto = "display: none";
                                                                        string styleCV = "display: none";
                                                                        if (ViewData.Model != null)
                                                                        {
                                                                            if (!string.IsNullOrEmpty(((CRM.Models.Employee)ViewData.Model).Photograph) && ViewData["ReActive"] == null)
                                                                            {
                                                                                stylePhoto = "display: block";
                                                                            }
                                                                            if (!string.IsNullOrEmpty(((CRM.Models.Employee)ViewData.Model).CVFile) && ViewData["ReActive"] == null)
                                                                            {
                                                                                styleCV = "display: block";
                                                                            }
                                                                        }
                                                                    %>
                                                                    <input type="button" id="btnRemoveImage" class="remove_image" style="<%=stylePhoto %>"
                                                                        onclick="CRM.msgConfirmBox('Are you sure you want to remove Photo?', 450, 'CRM.removeImage(\'<%=Constants.EMPLOYEE_DEFAULT_VALUE %>\')','Remove Photo');"
                                                                        value="" title="Remove Photo" />
                                                                    <input type="button" class="upload_cv" value="" id="btnUpload_CV" title="Upload CV" />
                                                                    <input type="button" id="btnRemoveCV" class="remove_cv" style="<%=styleCV %>" value=""
                                                                        title="Remove CV" onclick="CRM.msgConfirmBox('Are you sure you want to remove CV?', 450, 'CRM.removeCVFile(\'<%=Constants.EMPLOYEE_DEFAULT_VALUE %>\')','Remove CV');" />
                                                                    <input type="button" id="btndownload_cv" class="download_cv" style="<%=styleCV %>"
                                                                        value="" title="Download CV" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <table cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td width="330px" valign="top">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditCompany" title="Edit Company Information" href="javascript:void(0);">
                                        Edit</a></span>
                                    <% } %>
                                    <h2>
                                        At Company</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding:0px;">                                    
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                                            <tbody>
                                                <tr>
                                                    <td class="label" style="width: 110px">
                                                       Employee ID
                                                    </td>
                                                    <td class="input">
                                                        <span class="color_green_bold">
                                                            <%=emp.ID%></span>
                                                    </td>
                                                </tr>
                                                <%if (!string.IsNullOrEmpty(emp.OldEmployeeId))
                                                  {
                                                      int oldId = 0;
                                                      string link = string.Empty;
                                                      bool isNumber = Int32.TryParse(emp.OldEmployeeId.TrimEnd(','), out oldId);
                                                      if (isNumber)
                                                      {
                                                           link = "<a href='/Employee/Detail/"+oldId+"'>"+oldId+"</a>";
                                                      }
                                                      else
                                                      {
                                                          link = "<a href='/STT/Detail/" + emp.OldEmployeeId.TrimEnd(',') + "'>" + emp.OldEmployeeId.TrimEnd(',') + "</a>";
                                                      }
                                                %>
                                                <tr>
                                                    <td class="label">
                                                        Old Employee ID
                                                    </td>
                                                    <td class="input">
                                                        <%=link %>
                                                    </td>
                                                </tr>
                                                <%  }
                                                %>
                                                <tr>
                                                    <td class="label">
                                                        <%= Constants.JOB_REQUEST_ITEM_PREFIX %>
                                                    </td>
                                                    <td class="input">
                                                        <%=!string.IsNullOrEmpty(emp.JR)?emp.JR:Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        JR Approval #
                                                    </td>
                                                    <td class="input">
                                                        <%=!string.IsNullOrEmpty(emp.JRApproval) ? emp.JRApproval : Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Start Date
                                                    </td>
                                                    <td class="input">
                                                        <%=emp.StartDate.ToString(Constants.DATETIME_FORMAT_VIEW)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Contracted Date</td>
                                                    <td class="input">
                                                        <%=emp.ContractedDate.HasValue?emp.ContractedDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Work Location
                                                    </td>
                                                    <td class="input">
                                                        <%
                                                            string seatCodeID = CommonFunc.GetLocation((ViewData.Model as Employee).LocationCode, LocationType.SeatCode);
                                                            int iSeatCodeID = 0;
                                                            if (!string.IsNullOrEmpty(seatCodeID) && CheckUtil.IsInteger(seatCodeID))
                                                                iSeatCodeID = int.Parse(seatCodeID);
                                                            Response.Write("<div id='locationLabel'>" + CommonFunc.GetWorkLocationText(iSeatCodeID) + "</div>");
                                                            Response.Write(Html.Hidden("locationCodeForLabel", emp.LocationCode));
                                                        %>                                                       
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Department
                                                    </td>
                                                    <td class="input">
                                                        <%=ViewData["NameDepartment"].ToString()%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Sub-Department
                                                    </td>
                                                    <td class="input">
                                                        <%=emp.Department.DepartmentName %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Job Title
                                                    </td>
                                                    <td class="input">
                                                        <%=emp.JobTitleLevel.DisplayName%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Project
                                                    </td>
                                                    <td class="input">
                                                        <%=!string.IsNullOrEmpty(emp.Project)?emp.Project:Constants.NODATA %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Manager
                                                    </td>
                                                    <td class="input">
                                                        <%= !string.IsNullOrEmpty(emp.ManagerId) ? CommonFunc.GetEmployeeFullName(emp.Employee1,
                                                                                                                               Constants.FullNameFormat.FirstMiddleLast) : Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Labor Union
                                                    </td>
                                                    <td class="input">
                                                        <%=emp.LaborUnion.HasValue ? emp.LaborUnion == Constants.LABOR_UNION_TRUE ? "Yes" : "No" : Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Labor Union Date
                                                    </td>
                                                    <td class="input">
                                                        <%= emp.LaborUnionDate.HasValue?emp.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Tax ID
                                                    </td>
                                                    <td class="input">
                                                        <%=!string.IsNullOrEmpty(emp.TaxID)?emp.TaxID:Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Issue Date
                                                    </td>
                                                    <td class="input">
                                                        <%= emp.TaxIssueDate.HasValue?emp.TaxIssueDate.Value.ToString(Constants.DATETIME_FORMAT_VIEW):Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Insurance Book No
                                                    </td>
                                                    <td class="input">
                                                        <%=!string.IsNullOrEmpty(emp.SocialInsuranceNo)?emp.SocialInsuranceNo:Constants.NODATA%>
                                                    </td>
                                                </tr>
                                                <tr class="last">
                                                    <td width="110px" class="label">
                                                        Insurance Hospital
                                                    </td>
                                                    <td class="input">
                                                        <%= !string.IsNullOrEmpty(emp.InsuranceHospitalID)?emp.InsuranceHospital.Name:Constants.NODATA%>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="330px" valign="top">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditContactInfo" title="Edit Contact" href="javascript:void(0);">
                                        Edit</a></span>
                                    <% } %>
                                    <h2>
                                        Contact</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding:0px;">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                                            <tbody>
                                                <tr>
                                                    <td class="row_detail_label" style="width: 110px">
                                                       Home Phone
                                                    </td>
                                                    <td class="input">
                                                        <%= (emp.HomePhone != null ? emp.HomePhone : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        Cell Phone
                                                    </td>
                                                    <td class="input">
                                                        <%= (emp.CellPhone != null ? emp.CellPhone : Constants.NODATA) %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        Ext Number
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.ExtensionNumber != null ? emp.ExtensionNumber : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        SkypeID
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.SkypeId != null ? emp.SkypeId : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        YahooID
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.YahooId != null ? emp.YahooId : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        Personal Email
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.PersonalEmail != null ? emp.PersonalEmail : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        Office Email
                                                    </td>
                                                    <td class="input">
                                                        <%=emp.OfficeEmail %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="row_detail_label_left">                                                        
                                                        <b>Emergency Contact</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                       Contact Name
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.EmergencyContactName != null ? emp.EmergencyContactName : Constants.NODATA) %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="row_detail_label">
                                                        Phone
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.EmergencyContactPhone != null ? emp.EmergencyContactPhone : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr class="last">
                                                    <td class="row_detail_label">
                                                        Relationship
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.EmergencyContactRelationship != null ? emp.EmergencyContactRelationship : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    
                </td>
                <td width="335px" valign="top">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditAddressInfo" title="Edit Address" href="javascript:void(0);">
                                        Edit</a></span>
                                    <% } %>
                                    <h2>
                                        Address</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding:0px;">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                                        <tr>
                                            <td class="label" style="text-align: left;">
                                                <strong>Permanent Address
                                            </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <%= emp.PermanentAddress + (string.IsNullOrEmpty(emp.PermanentArea) ? string.Empty : ", " + emp.PermanentArea) + (string.IsNullOrEmpty(emp.PermanentDistrict) ? string.Empty : "," + emp.PermanentDistrict) + "<br/>" + (string.IsNullOrEmpty(emp.PermanentCityProvince) ? string.Empty : emp.PermanentCityProvince + ",") + (string.IsNullOrEmpty(emp.PermanentCountry) ? string.Empty : emp.PermanentCountry)%>
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td class="label" style="text-align: left;">
                                                <strong>VN Permanent Address
                                            </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <%= emp.VnPermanentAddress + (string.IsNullOrEmpty(emp.VnPermanentArea) ? string.Empty : ", " + emp.VnPermanentArea) + (string.IsNullOrEmpty(emp.VnPermanentDistrict) ? string.Empty : "," + emp.VnPermanentDistrict) + "<br/>" + (string.IsNullOrEmpty(emp.VnPermanentCityProvince) ? string.Empty : emp.VnPermanentCityProvince + ",") + (string.IsNullOrEmpty(emp.VnPermanentCountry) ? string.Empty : emp.VnPermanentCountry)%>
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td class="label" style="text-align: left;">
                                                <strong>Temp Address
                                            </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <%=emp.TempAddress + (string.IsNullOrEmpty(emp.TempArea) ? string.Empty : ", " + emp.TempArea) + (string.IsNullOrEmpty(emp.TempDistrict) ? string.Empty : "," + emp.TempDistrict) + "<br/>" + (string.IsNullOrEmpty(emp.TempCityProvince) ? string.Empty : emp.TempCityProvince + ",") + (string.IsNullOrEmpty(emp.TempCountry) ? string.Empty : emp.TempCountry)%>
                                            </td>
                                        </tr>
                                    
                                        <tr>
                                            <td class="label" style="text-align: left;">
                                                VN Temp Address
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input last">
                                                <%=emp.VnTempAddress + (string.IsNullOrEmpty(emp.VnTempArea) ? string.Empty : ", " + emp.VnTempArea ) + (string.IsNullOrEmpty(emp.VnTempDistrict) ? string.Empty : "," + emp.VnTempDistrict) + "<br/>" + (string.IsNullOrEmpty(emp.VnTempCityProvince) ? string.Empty : emp.VnTempCityProvince + ",") + (string.IsNullOrEmpty(emp.VnTempCountry) ? string.Empty : emp.VnTempCountry)%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditBankAccountInfo" title="Edit Bank Account"
                                        href="javascript:void(0);">Edit</a></span>
                                    <% } %>
                                    <h2>
                                        Bank Account</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding:0px;">                                    
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="view">
                                            <tbody>
                                                <tr>
                                                    <td width="110px" class="label"  width="132px">
                                                       Bank Name
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.BankName != null ? emp.BankName : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                                <tr class="last">
                                                    <td class="label"  width="132px">
                                                        Bank Account
                                                    </td>
                                                    <td class="input">
                                                        <%=(emp.BankAccount != null ? emp.BankAccount : Constants.NODATA)%>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <% if (emp.EmpStatusId != Constants.RESIGNED && !emp.DeleteFlag)
                                       {%>
                                    <span class="fr action"><a id="btnEditRemark" title="Edit Remarks" href="javascript:void(0);">
                                        Edit</a></span>
                                    <% } %>
                                    <h2>
                                        Remarks</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="padding:0px;">
                                    <div class="view">
                                        <div class="input last">
                                            <%=string.IsNullOrEmpty(emp.Remarks) ? Constants.NODATA : emp.Remarks.Replace("\n", "<br/>")%>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </div>
    
<div id="shareit-box">    
    <img src='../../Content/Images/loading3.gif' alt='' />    
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<% Employee emp = (Employee)ViewData.Model;
       string funcTitle = string.Empty;
       if (emp.EmpStatusId != Constants.RESIGNED)
       {
           Response.Write(EmsPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + EmsPageInfo.ModActiveEmployees + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName);
       }
       else
       {
           Response.Write(EmsPageInfo.MenuName + CommonPageInfo.AppDetailSepChar + EmsPageInfo.ModResignedEmployees + CommonPageInfo.AppSepChar + EmsPageInfo.ComName + CommonPageInfo.AppSepChar + CommonPageInfo.AppName);
       }
       %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
    <script type="text/javascript">
        hs.graphicsDir = '../../Scripts/highslide/graphics/';
        hs.outlineType = 'rounded-white';
    </script>

    <link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
    <script src="/Scripts/Tooltip.js" type="text/javascript"></script>
    <style type="text/css">
    #shareit-box 
    {
        background: none repeat scroll 0 0 #FFFFDD;
        border: 1px solid #ccc !important;        
        padding: 5px;
        max-width:300px;
    }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btndownload_cv").click(function () {
                var file_path = '<%=Constants.CV_PATH %>' + "//" + $('#CVFile').val();
                var outputname = $('#empFullName').val();
                CRM.downLoadFile(file_path, outputname == "" ? "Employee's_CV" : outputname + "'s_CV");
                return false;
            });

            $("#btnChangePhoto").click(function () {
                var url = "/Common/UploadImage?controller=Employee&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.IMAGE_PATH %>";
                CRM.popUpWindow(url, '#Photograph', 'Upload Photo');
                return false;
            });

            $("#btnUpload_CV").click(function () {
                var url = "/Common/UploadFile?controller=Employee&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.CV_PATH %>";
                CRM.popUpWindow(url, '#CVFile', 'Upload CV');
                return false;
            });
            $('#btnEditPersonal').click(function () {
                CRM.popup('/Employee/EditPersonalInfo/' + $('#ID').val(), 'Edit Personal Info', 1010);
            });

            $('#btnEditCompany').click(function () {
                CRM.popup("/Employee/EditCompanyInfo/" + $('#ID').val(), 'Edit Company Info', 500);
            });

            $('#btnEditContactInfo').click(function () {
                CRM.summary("", 'none', '');
                CRM.popup("/Employee/EditContactInfo/" + $('#ID').val(), 'Edit Contact Info', 400);
            });

            $('#btnEditAddressInfo').click(function () {
                CRM.popup("/Employee/EditAddressInfo/" + $('#ID').val(), 'Edit Address Info', 400);
            });

            $('#btnEditBankAccountInfo').click(function () {
                CRM.popup("/Employee/EditBankAccountInfo/" + $('#ID').val(), 'Edit Bank Account Info', 400);
            });

            $('#btnEditRemark').click(function () {
                CRM.popup("/Employee/EditRemark/" + $('#ID').val(), 'Edit Remark', 400);
            });

            $('#btnReActive').click(function () {
                window.location = "/Employee/ReActive/" + $('#ID').val();
            });

            $('#btnHistory').click(function () {
                window.location = "/Employee/History/" + $('#ID').val();
            });

            $('#btnResign').click(function () {
                CRM.popup("/Employee/Resign/" + $('#ID').val(), "Resign Employee " + $('#ID').val() + "", 400);
            });

            $('#btnContract').click(function () {
                window.location = "/Employee/ContractRenewal/" + $('#ID').val();
            });

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
                + "&id=" + $('#ID').val() + "&Page=Detail";
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
                    + "&id=" + $('#ID').val() + "&Page=Detail";
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
                    + "&id=" + $('#ID').val() + "&Page=Detail";
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
                + "&id=" + $('#ID').val() + "&Page=Detail";
                }
            });
            /*---------------------*/
            $(function () {
                showTooltipOnLabel("#locationLabel", "#locationCodeForLabel");
            });
            function showTooltipOnLabel(objViewId, objCodeId) {
                locationCode = $(objCodeId).val();
                $(objViewId).unbind();
                if (locationCode != "")
                    ShowTooltip($(objViewId), $("#shareit-box"), "/Common/WorkLocationTooltip/?locationCode=" + locationCode);
            };
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%= EmsPageInfo.ComName %>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <% Employee emp = (Employee)ViewData.Model;
       string funcTitle = string.Empty;

       //duyhung.nguyen added to back to exam        
       if (ViewData["BackToExamURL"] == null)
       {
           if (emp.EmpStatusId != Constants.RESIGNED)
           {
               funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl) + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
           }
           else
           {
               funcTitle = "Management » Employee » " + "<a href='/Employee/EmployeeResignList/'>" + EmsPageInfo.ModResignedEmployees + "</a> » " + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
           }
           //funcTitle = CommonFunc.GetCurrentMenu(Request.RawUrl)  + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;
       }
       else
       {
           funcTitle = "Management » Online Test » <a href=\"/Exam\">" + LOTPageInfo.Exam + "</a> » <a href=\"" + ViewData["BackToExamURL"] + "\">" + LOTPageInfo.AssignCandidate + "</a> » " + emp.ID + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName;          
       }          
     %>
    <%= funcTitle%>
</asp:Content>
