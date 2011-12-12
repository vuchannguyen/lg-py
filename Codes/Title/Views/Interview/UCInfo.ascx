<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<script type="text/javascript">
    hs.graphicsDir = '../../Scripts/highslide/graphics/';
    hs.outlineType = 'rounded-white';
</script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        $(function () {
            $("#Startdate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#ContractedDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#DOB").datepicker({
                yearRange: "-100:100",
                onClose: function () { $(this).valid(); }
            });
            $("#IssueDate").datepicker({
                yearRange: "-50:50",
                onClose: function () { $(this).valid(); }
            });
            $("#departEffectDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#titleEffectDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#hospitalEffectDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            $("#LaborUnionDate").datepicker({
                yearRange: "-50:50",
                onClose: function () { $(this).valid(); }
            });
            $("#TaxIssueDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
            if ($("#LaborUnion").attr("checked") == false && $("#LaborUnionDate").val() == '') {
                $("#LaborUnionDate").datepicker("disable");
            }
        });
        
        $("#btndownload_cv").click(function () {
            var file_path = '<%=Constants.CV_PATH_ROOT_CANDIDATE %>' + "//" + $('#CVFile').val();
            var outputname = $('#empFullName').val();
            CRM.downLoadFile(file_path, outputname == "" ? "Employee's_CV" : outputname + "'s_CV");
            return false;
        });

        $("#btnChangePhoto").click(function () {
            var url = "/Common/UploadImage?controller=Candidate&recordID=" + $('#RecordID').val() + "&saveTo=<%=Constants.PHOTO_PATH_ROOT_CANDIDATE %>";
            CRM.popUpWindow(url, '#Photograph', 'Upload Photo');
            return false;
        });

        $("#btnUpload_CV").click(function () {
            var url = "/Common/UploadFile?controller=Candidate&recordID=" + $('#RecordID').val() + "&saveTo=<%=Constants.CV_PATH_ROOT_CANDIDATE %>";
            CRM.popUpWindow(url, '#CVFile', 'Upload CV');
            return false;
        });

        $("#LaborUnion").click(function () {
            if ($("#LaborUnion").attr("checked") == true) {
                $("#LaborUnionDate").attr("disabled", "");
                $("#LaborUnionDate").datepicker("enable");
                $("#LaborUnionDate").change(function () {
                    $("#LaborUnionDate").rules("add", "checkDate");
                    $("#LaborUnionDate").rules("add", { checkAge: ["#addEmployee input[name='DOB']"] });
                });
                $("#LaborUnionDate").blur(function () {
                    if ($("#LaborUnionDate").val() == "") {
                        $("span[htmlfor=LaborUnionDate]").remove();
                    }
                });
            }
            else {
                $("#LaborUnionDate").val("");
                $("#LaborUnionDate").attr("disabled", "disabled");
                $("#LaborUnionDate").rules("remove");
                $("#LaborUnionDate").datepicker("disable");
                $("span[htmlfor=LaborUnionDate]").remove();
            }
        });
        $("#ID").val("");
    });

</script>
<div class="form">
<div style="width: 1024px; text-align:right;">
            <input type="submit" title="Save" class="save" value="" />
                        <input type="button" title="Cancel" onclick="javascript:window.location = '/Interview';" id="btnCancelTop" class="cancel"
                            value="" />
    </div>   
    <%
        Candidate emp = (Candidate)ViewData.Model;
        if (ViewData.Model == null)
        {
            Response.Write(Html.Hidden("Photograph", ""));
            Response.Write(Html.Hidden("CVFile", ""));
            Response.Write(Html.Hidden("empFullName", ""));
        }
        else
        {            
            Response.Write(Html.Hidden("Photograph", emp.Photograph));
            Response.Write(Html.Hidden("CVFile", emp.CVFile));
            Response.Write(Html.Hidden("empFullName", (emp.FirstName
                + " " + emp.MiddleName + " "
                + emp.LastName).Replace(" ", "_")));
            Response.Write(Html.Hidden("UpdateDate", emp.UpdateDate.ToString()));
        } %>
    
        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                    <tbody>
                        <tr>
                            <td class="ctbox">
                                <h2>
                                    Personal Info</h2>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="ccbox">                                
                                    <table cellspacing="0" cellpadding="0" border="0" width="1024px" class="edit">
                                        <tbody>
                                            <tr>
                                                <td valign="top" width="100%" style="padding:0px;">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tr>
                                                            <td class="require label" style="width: 92px">
                                                                 <b>Last Name </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td style="width: 176px" class="input">
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("LastName", "", new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("LastName", emp.LastName, new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                %>
                                                            </td>
                                                            <td class="label" style="width: 100px">
                                                                Middle Name
                                                            </td>
                                                            <td class="input" style="width: 176px;">
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("MiddleName", "", new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("MiddleName", emp.MiddleName, new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                %>
                                                            </td>
                                                            <td class="require label" style="width: 100px">
                                                            <b>First Name </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input" style="width: 154px;">
                                                                
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("FirstName", "", new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("FirstName", emp.FirstName, new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="require label" style="width: 92px;">
                                                                <b>VN Last Name </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("VnLastName", "", new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("VnLastName", emp.VnLastName, new { @style = "width:130px", @maxlength = "30" }));
                                                                   }
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                VN Middle Name
                                                            </td>
                                                            <td class="input">
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("VnMiddleName", "", new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("VnMiddleName", emp.VnMiddleName, new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                %>
                                                            </td>
                                                            <td class="require label">
                                                            <b>VN First Name </b><span style="color: Red">*</span>
                                                                
                                                            </td>
                                                            <td class="input">
                                                                
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("VnFirstName", "", new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("VnFirstName", emp.VnFirstName, new { @style = "width:130px", @maxlength = "20" }));
                                                                   }
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="require label" style="width: 92px;">
                                                                <b>Date of Birth </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% if (ViewData.Model == null)
                                                                   {
                                                                       Response.Write(Html.TextBox("DOB", "", new { @style = "width:130px" }));
                                                                   }
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("DOB", emp.DOB.HasValue?emp.DOB.Value.ToString(Constants.DATETIME_FORMAT):"", new { @style = "width:130px" }));
                                                                   }
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                Place of Birth
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("POB", "", new { @style = "width:130px", @maxlength = "50" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                VN Place of Birth
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("VnPOB", "", new { @style = "width:130px", @maxlength = "50" }));                                                                   
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label" style="width: 92px;">
                                                                Place of Origin
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("PlaceOfOrigin", "", new { @style = "width:130px", @maxlength = "100" }));
                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                VN Place of Origin
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("VnPlaceOfOrigin", "", new { @style = "width:130px", @maxlength = "100" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="require label">
                                                                <b>Nationality </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.DropDownList("Nationality", ViewData["Nationality"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));                                                                   
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="require label" style="width: 92px;">
                                                                <b>Gender </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.DropDownList("Gender",null, Constants.FIRST_ITEM, new { @style = "width:136px" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                Degree
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                   Response.Write(Html.TextBox("Degree", "", new { @style = "width:130px", @maxlength = "200" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                Other Degree
                                                            </td>
                                                            <td class="input">
                                                                <%
                                                                       Response.Write(Html.TextBox("OtherDegree", "", new { @style = "width:130px", @maxlength = "200" }));
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="require label" style="width: 92px;">
                                                                <b>ID Number</b> <span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("IDNumber", "", new { @style = "width:130px", @maxlength = "20" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="require label">
                                                                <b>Issue Date </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("IssueDate", "", new { @style = "width:130px" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="require label">
                                                                <b>Issue Location </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.TextBox("IDIssueLocation", "", new { @style = "width:130px", @maxlength = "200" }));                                                                   
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="label" style="width: 92px;">
                                                                Race
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                   Response.Write(Html.TextBox("Race", "", new { @style = "width:130px", @maxlength = "50" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                Religion
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                   Response.Write(Html.TextBox("Religion", "", new { @style = "width:130px", @maxlength = "50" }));
                                                                   
                                                                %>
                                                            </td>
                                                            <td class="label">
                                                                VN Issue Location
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                   Response.Write(Html.TextBox("VnIDIssueLocation", "", new { @style = "width:130px", @maxlength = "200" }));
                                                                   
                                                                %>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="require label last" style="width: 92px;">
                                                                <b>Current Status</b> <span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input last">
                                                                <% 
                                                                   Response.Write(Html.DropDownList("EmpStatusId", ViewData["EmpStatusId"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));
                                                                   
                                                                %>
                                                            </td>
                                                            <td class="require label">
                                                                <b>Married Status </b><span style="color: Red">*</span>
                                                            </td>
                                                            <td class="input">
                                                                <% 
                                                                       Response.Write(Html.DropDownList("MarriedStatus", ViewData["MarriedStatus"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));                                                                   
                                                                %>
                                                            </td>
                                                            <td  class="label last">
                                                Major
                                            </td>
                                            <td class="input">
                                                <% Response.Write(Html.TextBox("Major", "", new { @style = "width:130px", @maxlength = "255" })); %>
                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td valign="top" width="130" class="clrleft clrrgh">
                                                                <%
                                                                    string labelPhoto = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
                                                                    if (ViewData.Model == null)
                                                                    {
                                                                        Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                    }
                                                                    else
                                                                    {
                                                                        if (string.IsNullOrEmpty(emp.Photograph))
                                                                        {
                                                                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                        }
                                                                        else
                                                                        {
                                                                            labelPhoto = "<a id='thumb1' href='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + emp.Photograph
                                                                                            + "' class='highslide' onclick='return hs.expand(this)'>"
                                                                                            + "<img id='imgPhoto' src='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + emp.Photograph
                                                                                            + "' alt='Highslide JS' title='Click to enlarge' height='120px' width='120px' /></a>";
                                                                            Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                                        }
                                                                    }
                                                                    if (ViewData.Model == null)
                                                                    {
                                                                        Response.Write(Html.Hidden("RecordID", ""));
                                                                    }
                                                                    else
                                                                    {
                                                                        Response.Write(Html.Hidden("RecordID", emp.ID));
                                                                    } 
                                                                %>
                                                            </td>
                                                            <td valign="top" class="clrrgh">
                                                                <input type="button" class="upload_image" id="btnChangePhoto" value="" title="Change Photo" />
                                                                <%  string stylePhoto = "display: none";
                                                                    string styleCV = "display: none";
                                                                    if (ViewData.Model != null)
                                                                    {
                                                                        if (!string.IsNullOrEmpty(emp.Photograph))
                                                                        {
                                                                            stylePhoto = "display: block";
                                                                        }
                                                                        if (!string.IsNullOrEmpty(emp.CVFile))
                                                                        {
                                                                            styleCV = "display: block";
                                                                        }
                                                                    }
                                                                %>
                                                                <input type="button" id="btnRemoveImage" class="remove_image" style="<%=stylePhoto %>"
                                                                    onclick="CRM.msgConfirmBox('Are you sure you want to remove Photo?', 450,'CRM.removeImage(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')','Remove Photo');"
                                                                    value="" title="Remove Photo" />
                                                                <input type="button" class="upload_cv" value="" id="btnUpload_CV" title="Upload CV" />
                                                                <input type="button" id="btnRemoveCV" class="remove_cv" style="<%=styleCV %>" value=""
                                                                    title="Remove CV" onclick="CRM.msgConfirmBox('Are you sure you want to remove CV?', 450, 'CRM.removeCVFile(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')','Remove CV');" />
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
<br />
    <table cellspacing="0" cellpadding="0" border="0" class="gbox" width="1032px" style="border:1px">
        <tbody>
            <tr>
                <td width="340px" valign="top" style="padding:0px;">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <h2>
                                        At Company</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="ccbox">                                    
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                            <tbody>
                                                <tr>
                                                    <td class="require label" style="width: 110px;">
                                                        <b>Employee ID </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <%                                                             
                                                                Response.Write(Html.TextBox("ID", "", new { @style = "width:130px", @maxlength = "10" })); 
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="require label">
                                                        <%= Constants.JOB_REQUEST_ITEM_PREFIX %>
                                                    </td>
                                                    <td class="input">
                                                        <% if (ViewData.Model == null)
                                                           {
                                                               Response.Write(Html.TextBox("JR", "", new { @style = "width:110px", @maxlength = "10", @readonly = true }));
                                                           }
                                                           else
                                                           {
                                                               Response.Write(Html.TextBox("JR", emp.JRId, new { @style = "width:110px", @maxlength = "10", @readonly = true }));
                                                           }
                                                        %>
                                                        <button type="button" class="icon select" title="Select JR" onclick="CRM.popup('/Common/ListJRInterview/?isOnPopup=0', 'Select Job Request', 1024); return false;">
                                                         </button>
                                                        <button class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR','#JRApproval'); return false;"></button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        JR Approval #
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("JRApproval", "", new { @style = "width:130px", @readonly = true }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="require label">
                                                        <b>Start Date </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("Startdate", "", new { @style = "width:130px" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="require label">
                                                        <b>Contracted Date </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("ContractedDate", "", new { @style = "width:130px" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Work Location
                                                    </td>
                                                    <td class="input">
                                                        <% Response.Write(Html.TextBox("WorkLocation", "", new { @style = "width:110px", @maxlength = "100", @readonly = "readonly" }));
                                                           Response.Write(Html.Hidden("LocationCode"));
                                                        %>
                                                        <button type="button" class="icon select" title="Select Work Location" onclick="CRM.popup('/Common/ListSeatCode/?isOnPopup=0', 'Select Work Location', 850)"></button>
                                                        <button type="button" class="icon remove" title="Remove Work Location" onclick="$('#WorkLocation').val(''); $('#LocationCode').val(''); showWorkLocationTooltip(); "></button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Department
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.DropDownList("DepartmentName", ViewData["Department"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="require label">
                                                        <b>Sub-Department </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.DropDownList("DepartmentId", ViewData["SubDepartment"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <%if (ViewData.Model != null)
                                                  {%>
                                                <tr id="trdepartEffectDate" style="display: none">
                                                    <td class="require label">
                                                        <b>Effective Date </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <%= Html.TextBox("departEffectDate", "", new { @style = "width:130px", @maxlength = "10" })%>
                                                    </td>
                                                </tr>
                                                <% } %>
                                                <tr>
                                                    <td class="require label">
                                                        <b>Job Title </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <% if (ViewData.Model == null)
                                                           {
                                                               Response.Write(Html.DropDownList("TitleId", ViewData["TitleId"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:136px" }));
                                                           }
                                                           else
                                                           {
                                                               Response.Write(Html.DropDownList("TitleId", null, new { @style = "width:136px" }));
                                                           }
                                                        %>
                                                    </td>
                                                </tr>
                                                <%if (ViewData.Model != null)
                                                  {%>
                                                <tr id="trTitleEffectDate" style="display: none">
                                                    <td class="require label">
                                                        <b>Effective Date </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input">
                                                        <%= Html.TextBox("titleEffectDate", "", new { @style = "width:130px", @maxlength = "10" })%>
                                                    </td>
                                                </tr>
                                                <% } %>
                                                <tr>
                                                    <td class="label">
                                                        Project
                                                    </td>
                                                    <td class="input">
                                                        <%= Html.TextBox("Project", "", new { @style = "width:110px", @maxlength = "100" })%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Manager
                                                    </td>
                                                    <td class="input">
                                                    <%= Html.TextBox("Manager", "", new { @style = "width:110px", @maxlength = "100" })%>
                                                    <%= Html.Hidden("ManagerId") %>
                                                        <button type="button" class="icon select" title="Select Manager" onclick="CRM.listManager(); return false;"></button>
                                                        <button type="button" class="icon remove" title="Remove Manager" onclick="CRM.clearManager('#Manager'); return false;"></button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Labor Union
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.CheckBox("LaborUnion"));                                                           
                                                        %>
                                                        &nbsp;
                                                        <% 
                                                               Response.Write(Html.TextBox("LaborUnionDate", "", new { @style = "width:108px", @disabled = "disabled" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Tax ID
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("TaxID", "", new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Issue Date
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("TaxIssueDate", "", new { @style = "width:130px" }));
                                                          
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="label">
                                                        Insurance Book No
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("SocialInsuranceNo", "", new { @style = "width:130px", @maxlength = "20" }));
                                                          
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%
                                                      string cssLast = " last";
                                                      if (ViewData.Model != null && ViewData["ReActive"] == null)
                                                      {
                                                          cssLast = "";
                                                      }
                                                    %>
                                                    <td class="label<%=cssLast %>">
                                                        Insurance Hospital
                                                    </td>
                                                    <td class="input<%=cssLast %>">
                                                        <% 
                                                            Response.Write(Html.TextBox("InsuranceName", "", new { @style = "width:110px" }));
                                                               Response.Write(Html.Hidden("InsuranceHospitalID"));
                                                           
                                                        %>
                                                        <button class="icon select" title="Select Insurance Hospital" onclick="CRM.popup('/Employee/ListHospital','Insurance Hospital List',860); return false;"></button>
                                                        <button class="icon remove" title="Remove Insurance Hospital" onclick="RemoveHospital(); return false;"></button>
                                                    </td>
                                                </tr>
                                                <%if (ViewData.Model != null)
                                                  {%>
                                                <tr id="trHospitalEffectDate" style="display: none">
                                                    <td class="require label last">
                                                        <b>Effective Date </b><span style="color: Red">*</span>
                                                    </td>
                                                    <td class="input last">
                                                        <%= Html.TextBox("hospitalEffectDate", "", new { @style = "width:130px", @maxlength = "10" })%>
                                                    </td>
                                                </tr>
                                                <% } %>
                                            </tbody>
                                        </table>                                    
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td width="330px" valign="top" style="padding:0px;">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <h2>
                                        Contact</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="ccbox">                                    
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                            <tbody>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Home Phone
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                           Response.Write(Html.TextBox("HomePhone", "", new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Cell Phone
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("CellPhone", emp.CellPhone, new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Floor
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("Floor", "", new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Ext Number
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("ExtensionNumber", "", new { @style = "width:130px", @maxlength = "10" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Seat Code
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("SeatCode", "", new { @style = "width:130px", @maxlength = "10" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        SkypeID
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("SkypeId", "", new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        YahooID
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("YahooId", "", new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Personal Email
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                           
                                                               Response.Write(Html.TextBox("PersonalEmail", emp.Email, new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="require label" width="110px">
                                                        <b>Office Email </b>
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("OfficeEmail", "", new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" height="25px" class="label" style="text-align:left">
                                                        <b>Emergency Contact</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Contact Name
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("EmergencyContactName", "", new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Phone
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("EmergencyContactPhone", "", new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label last">
                                                        Relationship
                                                    </td>
                                                    <td class="input last">
                                                        <% 
                                                               Response.Write(Html.TextBox("EmergencyContactRelationship", "", new { @style = "width:130px", @maxlength = "50" }));
                                                           
                                                        %>
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
                                    <h2>
                                        Bank Account</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="ccbox">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                            <tbody>
                                                <tr>
                                                    <td width="110px" class="label">
                                                        Bank Name
                                                    </td>
                                                    <td class="input">
                                                        <% 
                                                               Response.Write(Html.TextBox("BankName", "", new { @style = "width:130px", @maxlength = "100" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="110px" class="label last">
                                                        Bank Account
                                                    </td>
                                                    <td class="input last">
                                                        <% 
                                                               Response.Write(Html.TextBox("BankAccount", "", new { @style = "width:130px", @maxlength = "20" }));
                                                           
                                                        %>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td style="padding:0px;">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
                        <tbody>
                            <tr>
                                <td class="ctbox">
                                    <h2>
                                        Address</h2>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="ccbox">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                            <tr>
                                                <td class="label" style="text-align: left">
                                                    <strong>Permanent Address
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("PermanentAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("PermanentArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("PermanentDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("PermanentCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <%
                                                           Response.Write(Html.DropDownList("PermanentCountry", ViewData["Nationality"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                      
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="text-align: left">
                                                    <strong>VN Permanent Address
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("VnPermanentAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("VnPermanentArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("VnPermanentDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("PermanentCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.DropDownList("VnPermanentCountry", ViewData["VnNationality"] as SelectList, "-Quốc gia-", new { @style = "width:96px" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="text-align: left">
                                                    <strong>Temp Address
                                                    </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("TempAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("TempArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("TempDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("TempCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.DropDownList("TempCountry", ViewData["Nationality"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                      
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="text-align: left">
                                                    <strong>VN Temp Address
                                                </strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("VnTempAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("VnTempArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="input">
                                                    <% 
                                                           Response.Write(Html.TextBox("VnTempDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.TextBox("VnTempCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                       
                                                    %>
                                                    <span class="fsep"></span>
                                                    <% 
                                                           Response.Write(Html.DropDownList("VnTempCountry", ViewData["VnNationality"] as SelectList, "-Quốc gia-", new { @style = "width:96px" }));
                                                       
                                                    %>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="0" border="0" width="100%"  class="profile">
                        <tbody>
                             <tr>
                                <td class="ctbox">
                                    <h2>
                                        Remarks</h2>
                                </td>
                            </tr>

                            <tr>
                                <td class="ccbox">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" class="edit">
                                            <tbody>
                                                <tr>
                                                <td>
                                    <% 
                                           Response.Write(Html.TextArea("Remarks", new { @style = "width: 370px; height: 87px;" }));
                                       
                                    %>
                                    </td>
                                    </tr>
                                     </tbody></table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <div class="cbutton" style="text-align: right">
                        <input type="submit" title="Save" class="save" value="" />                       
                        <input type="button" title="Cancel" onclick="javascript:window.location = '/Interview';" id="btnCancel" class="cancel"
                            value="" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>