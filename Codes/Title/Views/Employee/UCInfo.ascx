<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<script type="text/javascript">
    hs.graphicsDir = '../../Scripts/highslide/graphics/';
    hs.outlineType = 'rounded-white';
</script>
<link rel="stylesheet" type="text/css" href="/Content/Css/tooltip.css" />
<script src="/Scripts/Tooltip.js" type="text/javascript"></script>
<script type="text/javascript">
    box_color = "Black";
    box_sticky_color = "Black";
    tooltipoffsets = [0, 30];
    jQuery(document).ready(function () {
        $(function () {
            $("#Resigned_Date").datepicker();
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
            $("#titleLevelEffectDate").datepicker({
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
            var file_path = '<%=Constants.CV_PATH %>' + "//" + $('#CVFile').val();
            var outputname = $('#empFullName').val();
            CRM.downLoadFile(file_path, outputname == "" ? "Employee's_CV" : outputname + "'s_CV");
            return false;
        });

        $("#btnChangePhoto").click(function () {
            var url = "/Common/UploadImage?controller=Employee&recordID=" + $('#RecordID').val() + "&saveTo=<%=Constants.IMAGE_PATH %>";
            CRM.popUpWindow(url, '#Photograph', 'Upload Photo');
            return false;
        });

        $("#btnUpload_CV").click(function () {
            var url = "/Common/UploadFile?controller=Employee&recordID=" + $('#RecordID').val() + "&saveTo=<%=Constants.CV_PATH %>";
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
        $(function () {
            showWorkLocationTooltip();
        });
    });
    
</script>
<style type="text/css">
    #shareit-box
    {
        background: none repeat scroll 0 0 #FFFFDD;
        border-width: 1px;
        padding: 5px;
        max-width: 300px;
    }
</style>
<div id="shareit-box">
    <img src='../../Content/Images/loading3.gif' alt='' />
</div>
<div class="form">
    <div style="width: 1024px; text-align: right;">
        <input type="submit" title="Save" class="save" value="" />
        <% string javascript = "javascript:CRM.back()";
           if (ViewData.Model != null && ViewData["ReActive"] == null)
           {
               javascript = "window.location = '/Employee'";
           }
           else if (ViewData.Model != null && ViewData["ReActive"] != null)
           {
               javascript = "window.location = '/Employee/EmployeeResignList'";
           }
                            
        %>
        <input type="button" title="Cancel" onclick="<%=javascript %>" id="btnCancelTop"
            class="cancel" value="" />
    </div>
    <%
        Employee emp = (Employee)ViewData.Model;
        if (ViewData.Model == null)
        {
            Response.Write(Html.Hidden("Photograph", ""));
            Response.Write(Html.Hidden("CVFile", ""));
            Response.Write(Html.Hidden("empFullName", ""));
        }
        else
        {
            if (emp.EmpStatusId == Constants.RESIGNED)
            {
                Response.Write(Html.Hidden("ResignedDate", emp.ResignedDate.Value.ToString(Constants.DATETIME_FORMAT)));
            }
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
                                <td valign="top" width="100%" style="padding: 0px;">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td class="require label" style="width: 92px">
                                                <b>First Name </b><span style="color: Red">*</span>
                                            </td>
                                            <td class="input" style="width: 176px;">
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
                                                <b>Last Name </b><span style="color: Red">*</span>
                                            </td>
                                            <td style="width: 154px" class="input">
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
                                        </tr>
                                        <tr>
                                            <td class="require label">
                                                <b>VN First Name </b><span style="color: Red">*</span>
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnFirstName", "", new { @style = "width:130px", @maxlength = "30" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("VnFirstName", emp.VnFirstName, new { @style = "width:130px", @maxlength = "30" }));
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
                                        </tr>
                                        <tr>
                                            <td class="label">
                                                Date of Birth
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("DOB", "", new { @style = "width:130px" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("DOB", emp.DOB.HasValue ? emp.DOB.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:130px" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Place of Birth
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("POB", "", new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("POB", emp.POB, new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                VN Place of Birth
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnPOB", "", new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("VnPOB", emp.VnPOB, new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 92px;">
                                                Place of Origin
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PlaceOfOrigin", "", new { @style = "width:130px", @maxlength = "100" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("PlaceOfOrigin", emp.PlaceOfOrigin, new { @style = "width:130px", @maxlength = "100" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label" style="width: 102px !important">
                                                VN Place of Origin
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnPlaceOfOrigin", "", new { @style = "width:130px", @maxlength = "100" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("VnPlaceOfOrigin", emp.VnPlaceOfOrigin, new { @style = "width:130px", @maxlength = "100" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Nationality
                                            </td>
                                            <td class="input">
                                                <% Response.Write(Html.DropDownList("Nationality", null, Constants.FIRST_ITEM, new { @style = "width:136px" }));%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 92px;">
                                                Gender
                                            </td>
                                            <td class="input">
                                                <%Response.Write(Html.DropDownList("Gender", null, Constants.FIRST_ITEM, new { @style = "width:136px" })); %>
                                            </td>
                                            <td class="label">
                                                Degree
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("Degree", "", new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("Degree", emp.Degree, new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Other Degree
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("OtherDegree", "", new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("OtherDegree", emp.OtherDegree, new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 92px;">
                                                ID Number
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("IDNumber", "", new { @style = "width:130px", @maxlength = "20" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("IDNumber", emp.IDNumber, new { @style = "width:130px", @maxlength = "20" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Issue Date
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("IssueDate", "", new { @style = "width:130px" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("IssueDate", emp.IssueDate.HasValue ? emp.IssueDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:130px" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Issue Location
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("IDIssueLocation", "", new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("IDIssueLocation", emp.IDIssueLocation, new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 92px;">
                                                Race
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("Race", "", new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("Race", emp.Race, new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label">
                                                Religion
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("Religion", "", new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("Religion", emp.Religion, new { @style = "width:130px", @maxlength = "50" }));
                                                   }
                                                %>
                                            </td>
                                            <td class="label" style="width: 102px !important;">
                                                VN Issue Location
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnIDIssueLocation", "", new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("VnIDIssueLocation", emp.VnIDIssueLocation, new { @style = "width:130px", @maxlength = "200" }));
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="width: 92px;">
                                                Current Status
                                            </td>
                                            <td class="input">
                                                <% 
                                                    if (ViewData.Model != null && emp.EmpStatusId == Constants.RESIGNED)
                                                    {
                                                        Response.Write(Html.DropDownList("EmpStatusIdList", ViewData["EmpStatusId"] as SelectList,
                                                            new { @style = "width:136px", @disabled = "" }));
                                                        Response.Write(Html.Hidden("EmpStatusId", emp.EmpStatusId));
                                                    }
                                                    else
                                                    {
                                                        Response.Write(Html.DropDownList("EmpStatusId", null, Constants.FIRST_ITEM,
                                                            new { @style = "width:136px" }));
                                                    }
                                                %>
                                            </td>
                                            <td class="label">
                                                Married Status
                                            </td>
                                            <td class="input">
                                                <% Response.Write(Html.DropDownList("MarriedStatus", null, Constants.FIRST_ITEM, new { @style = "width:136px" }));%>
                                            </td>
                                            <td class="label">
                                                Major
                                            </td>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("Major", "", new { @style = "width:130px", @maxlength = "255" }));
                                                   }
                                                   else
                                                   {
                                                       Response.Write(Html.TextBox("Major", emp.Major, new { @style = "width:136px", @maxlength = "255" }));
                                                   }
                                                %>
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
                                                            labelPhoto = "<a id='thumb1' href='" + Constants.IMAGE_PATH + emp.Photograph
                                                                            + "' class='highslide' onclick='return hs.expand(this)'>"
                                                                            + "<img id='imgPhoto' src='" + Constants.IMAGE_PATH + emp.Photograph
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
                                                        if (!string.IsNullOrEmpty(emp.Photograph) && ViewData["ReActive"] == null)
                                                        {
                                                            stylePhoto = "display: block";
                                                        }
                                                        if (!string.IsNullOrEmpty(emp.CVFile) && ViewData["ReActive"] == null)
                                                        {
                                                            styleCV = "display: block";
                                                        }
                                                    }
                                                %>
                                                <input type="button" id="btnRemoveImage" class="remove_image" style="<%=stylePhoto %>"
                                                    onclick="CRM.msgConfirmBox('Are you sure you want to remove Photo?', 450,'CRM.removeImage(\'<%=Constants.EMPLOYEE_DEFAULT_VALUE %>\')','Remove Photo');"
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
    <br />
    <%
        if (emp != null && emp.EmpStatusId == Constants.RESIGNED)
        {
    %>
    <table cellspacing="0" cellpadding="0" border="0" width="100%" class="profile">
        <tbody>
            <tr>
                <td class="ctbox">
                    <h2>
                        Resigned Info</h2>
                </td>
            </tr>
            <tr>
                <td valign="top" class="ccbox">
                    <table cellspacing="0" cellpadding="0" border="0" width="1024px" class="edit">
                        <tbody>
                            <tr>
                                <td valign="top" width="100%" style="padding: 0px;">
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tr>
                                            <td class="label required last" style="height: 20px">
                                                Resigned Date<span>*</span>
                                            </td>
                                            <td class="input last" style="width: 176px;">
                                                <%
                                                    if (emp.ResignedDate.HasValue)
                                                    {
                                                        Response.Write(Html.TextBox("Resigned_Date", emp.ResignedDate.Value.ToString(Constants.DATETIME_FORMAT),
                                                            new { @maxlength = 10 }));
                                                    }
                                                    else
                                                    {
                                                        Response.Write(Html.TextBox("Resigned_Date", "", new { @maxlength = 10 }));
                                                    }
                                                %>
                                            </td>
                                            <td class="label last" style="width: 120px;">
                                                Resigned Allowance
                                            </td>
                                            <td class="input last" style="width: 156px;">
                                                <%
                                                    Response.Write(Html.TextBox("ResignedAllowance"));
                                                %>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label last" style="width: 110px; vertical-align: top">
                                                Resigned Reason
                                            </td>
                                            <td class="input last" colspan="5">
                                                <%= Html.TextArea("ResignedReason", null, new { @style="width:100%; height:50px"})%>
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
    <%
        }        
    %>
    <br />
    <table cellspacing="0" cellpadding="0" border="0" class="gbox" width="1032px" style="border: 1px">
        <tbody>
            <tr>
                <td width="360px" valign="top" style="padding: 0px;">
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
                                                <td class="require label">
                                                    <b>Employee ID </b><span style="color: Red">*</span>
                                                </td>
                                                <td class="input">
                                                    <% 
                                                        if (ViewData.Model == null)
                                                        {
                                                            Response.Write(Html.TextBox("ID", "", new { @style = "width:130px", @maxlength = "10" }));
                                                        }
                                                        else if (ViewData.Model != null && ViewData["ReActive"] != null)
                                                        {

                                                            Response.Write(Html.TextBox("NewID", "", new { @style = "width:130px", @maxlength = "10" }));
                                                            Response.Write(Html.Hidden("ID", emp.ID));
                                                        }
                                                        else
                                                        {
                                                            Response.Write(Html.TextBox("ID", emp.ID, new { @style = "width:130px", @maxlength = "10", @disabled = "disabled" }));
                                                        }
                                                        
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    <%= Constants.JOB_REQUEST_ITEM_PREFIX %>
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("JR", "", new { @style = "width:130px", @maxlength = "10", @readonly = true }));%>
                                                    <button type="button" class="icon select" title="Select JR" onclick="CRM.popup('/Common/ListJRInterview/?isOnPopup=0', 'Select Job Request', 1024); return false;">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR','#JRApproval'); return false;">
                                                    </button>
                                                    <%}
                                                       else
                                                       {
                                                           if (!string.IsNullOrEmpty(emp.JR))
                                                           {
                                                               JobRequestItem objJRItem = new JobRequestItemDao().GetByID(emp.JR);
                                                               if (objJRItem != null)
                                                               {
                                                                   if (objJRItem.StatusID == Constants.JR_ITEM_STATUS_OPEN)
                                                                   {
                                                                       Response.Write(Html.TextBox("JR", emp.JR, new { @style = "width:130px", @maxlength = "10", @readonly = true }));%>
                                                    <button type="button" class="icon select" title="Select JR" onclick="CRM.popup('/Common/ListJRInterview/?isOnPopup=0', 'Select Job Request', 1024); return false;">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR','#JRApproval'); return false;">
                                                    </button>
                                                    <%}
                                                                   else
                                                                   {
                                                                       Response.Write(Html.TextBox("JR", emp.JR, new { @style = "width:130px", @maxlength = "10", @readonly = true }));
                                                                   }
                                                               }
                                                           }
                                                           else
                                                           {
                                                               Response.Write(Html.TextBox("JR", emp.JR, new { @style = "width:130px", @maxlength = "10", @readonly = true }));%>
                                                    <button type="button" class="icon select" title="Select JR" onclick="CRM.popup('/Common/ListJRInterview/?isOnPopup=0', 'Select Job Request', 1024); return false;">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove JR" onclick="CRM.clearJR('#JR','#JRApproval'); return false;">
                                                    </button>
                                                    <%}
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    JR Approval #
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("JRApproval", "", new { @style = "width:130px", @readonly = true }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("JRApproval", emp.JRApproval, new { @style = "width:130px", @readonly = true }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="require label">
                                                    <b>Start Date </b><span style="color: Red">*</span>
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null || (ViewData.Model != null && ViewData["ReActive"] != null))
                                                       {
                                                           Response.Write(Html.TextBox("Startdate", "", new { @style = "width:130px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("Startdate", emp.StartDate.ToString(Constants.DATETIME_FORMAT), new { @style = "width:130px" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Contracted Date
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null || (ViewData.Model != null && ViewData["ReActive"] != null))
                                                       {
                                                           Response.Write(Html.TextBox("ContractedDate", "", new { @style = "width:130px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("ContractedDate", emp.ContractedDate.HasValue ? emp.ContractedDate.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @style = "width:130px" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Work Location
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("WorkLocation", "", new { @style = "width:130px", @maxlength = "100", @readonly = "readonly" }));
                                                       }
                                                       else
                                                       {
                                                           string seatCodeID = CommonFunc.GetLocation((ViewData.Model as Employee).LocationCode, LocationType.SeatCode);
                                                           int iSeatCodeID = 0;
                                                           if (!string.IsNullOrEmpty(seatCodeID) && CheckUtil.IsInteger(seatCodeID))
                                                               iSeatCodeID = int.Parse(seatCodeID);
                                                           Response.Write(Html.TextBox("WorkLocation", CommonFunc.GetWorkLocationText(iSeatCodeID),
                                                               new { @style = "width:130px", @maxlength = "100", @readonly = "readonly" }));
                                                       }
                                                       Response.Write(Html.Hidden("LocationCode"));
                                                    %>
                                                    <button type="button" class="icon select" title="Select Work Location" onclick="CRM.popup('/Common/ListSeatCode/?isOnPopup=0', 'Select Work Location', 850)">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove Work Location" onclick="$('#WorkLocation').val(''); $('#LocationCode').val(''); showWorkLocationTooltip(); ">
                                                    </button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Department
                                                </td>
                                                <td class="input">
                                                    <% Response.Write(Html.DropDownList("DepartmentName", null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:136px" })); %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="require label" style="width: 105px">
                                                    <b>Sub-Department </b><span style="color: Red">*</span>
                                                </td>
                                                <td class="input">
                                                    <% Response.Write(Html.DropDownList("DepartmentId", null, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:136px" }));%>
                                                </td>
                                            </tr>
                                            <%if (ViewData.Model != null && ViewData["ReActive"] == null)
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
                                                    <%Response.Write(Html.DropDownList("TitleId", null, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:136px" })); %>
                                                </td>
                                            </tr>
                                            <%if (ViewData.Model != null && ViewData["ReActive"] == null)
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
                                                <td class="require label">
                                                    <b>Job Title Level</b><span style="color: Red">*</span>
                                                </td>
                                                <td class="input">
                                                    <%Response.Write(Html.DropDownList("TitleLevelId", null, Constants.FIRST_ITEM_JOBTITLELEVEL, new { @style = "width:136px" })); %>
                                                </td>
                                            </tr>
                                            <%if (ViewData.Model != null && ViewData["ReActive"] == null)
                                              {%>
                                            <tr id="tr1" style="display: none">
                                                <td class="require label">
                                                    <b>Effective Date </b><span style="color: Red">*</span>
                                                </td>
                                                <td class="input">
                                                    <%= Html.TextBox("titleLevelEffectDate", "", new { @style = "width:130px", @maxlength = "10" })%>
                                                </td>
                                            </tr>
                                            <% } %>
                                            <tr>
                                                <td class="label">
                                                    Project
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("Project", "", new { @style = "width:130px", @maxlength = "100" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("Project", !string.IsNullOrEmpty(emp.Project) ? emp.Project : "", new { @style = "width:130px", @maxlength = "100" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Manager
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("Manager", "", new { @style = "width:130px", @maxlength = "100", @readonly = "readonly" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("Manager", CommonFunc.GetEmployeeFullName(emp.Employee1,
                                                               Constants.FullNameFormat.FirstMiddleLast), new { @style = "width:130px", @maxlength = "100", @readonly = "readonly" }));
                                                       }
                                                       Response.Write(Html.Hidden("ManagerId"));
                                                    %>
                                                    <button type="button" class="icon select" title="Select Manager" onclick="CRM.listManager(); return false;">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove Manager" onclick="CRM.clearManager('#Manager'); return false;">
                                                    </button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Labor Union
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.CheckBox("LaborUnion"));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.CheckBox("LaborUnion", emp.LaborUnion));
                                                       }
                                                    %>
                                                    &nbsp;
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("LaborUnionDate", "", new { @style = "width:108px", @disabled = "disabled" }));
                                                       }
                                                       else
                                                       {
                                                           if (emp.LaborUnion == true && emp.LaborUnionDate != null)
                                                           {
                                                               Response.Write(Html.TextBox("LaborUnionDate", emp.LaborUnionDate.Value.ToString(Constants.DATETIME_FORMAT), new { @style = "width:108px" }));
                                                           }
                                                           else if (emp.LaborUnion == true && emp.LaborUnionDate == null)
                                                           {
                                                               Response.Write(Html.TextBox("LaborUnionDate", "", new { @style = "width:108px" }));
                                                           }
                                                           else
                                                           {
                                                               Response.Write(Html.TextBox("LaborUnionDate", "", new { @style = "width:108px", @disabled = "disabled" }));
                                                           }
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Tax ID
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("TaxID", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("TaxID", emp.TaxID, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label">
                                                    Issue Date
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("TaxIssueDate", "", new { @style = "width:130px" }));
                                                       }
                                                       else
                                                       {
                                                           if (emp.TaxIssueDate.HasValue)
                                                           {
                                                               Response.Write(Html.TextBox("TaxIssueDate", emp.TaxIssueDate.Value.ToString(Constants.DATETIME_FORMAT), new { @style = "width:130px" }));
                                                           }
                                                           else
                                                           {
                                                               Response.Write(Html.TextBox("TaxIssueDate", "", new { @style = "width:130px" }));
                                                           }
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" style="width: 105px">
                                                    Insurance Book No
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("SocialInsuranceNo", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("SocialInsuranceNo", emp.SocialInsuranceNo, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
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
                                                <td class="label<%=cssLast %>" style="width: 105px">
                                                    Insurance Hospital
                                                </td>
                                                <td class="input<%=cssLast %>">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("InsuranceName", "", new { @style = "width:130px" }));
                                                           Response.Write(Html.Hidden("InsuranceHospitalID"));
                                                       }
                                                       else
                                                       {
                                                           string insurName = !string.IsNullOrEmpty(emp.InsuranceHospitalID) ? emp.InsuranceHospital.Name : string.Empty;

                                                           Response.Write(Html.TextBox("InsuranceName",
                                                               insurName, new { @style = "width:130px", @readonly = true, @title = insurName }));

                                                           Response.Write(Html.Hidden("InsuranceHospitalID",
                                                               !string.IsNullOrEmpty(emp.InsuranceHospitalID) ?
                                                               emp.InsuranceHospitalID : ""));
                                                       }
                                                    %>
                                                    <button type="button" class="icon select" title="Select Insurance Hospital" onclick="CRM.popup('/Employee/ListHospital','Insurance Hospital List',860); return false;">
                                                    </button>
                                                    <button type="button" class="icon remove" title="Remove Insurance Hospital" onclick="RemoveHospital(); return false;">
                                                    </button>
                                                </td>
                                            </tr>
                                            <%if (ViewData.Model != null && ViewData["ReActive"] == null)
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
                <td width="330px" valign="top" style="padding: 0px;">
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
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("HomePhone", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("HomePhone", emp.HomePhone, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    Cell Phone
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("CellPhone", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("CellPhone", emp.CellPhone, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    Ext Number
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("ExtensionNumber", "", new { @style = "width:130px", @maxlength = "10" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("ExtensionNumber", emp.ExtensionNumber, new { @style = "width:130px", @maxlength = "10" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    SkypeID
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("SkypeId", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("SkypeId", emp.SkypeId, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    YahooID
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("YahooId", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("YahooId", emp.YahooId, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    Personal Email
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("PersonalEmail", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("PersonalEmail", emp.PersonalEmail, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label" width="110px">
                                                    Office Email
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("OfficeEmail", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("OfficeEmail", emp.OfficeEmail, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" height="25px" class="label" style="text-align: left">
                                                    Emergency Contact
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    Contact Name
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactName", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactName", emp.EmergencyContactName, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label">
                                                    Phone
                                                </td>
                                                <td class="input">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactPhone", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactPhone", emp.EmergencyContactPhone, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label last">
                                                    Relationship
                                                </td>
                                                <td class="input last">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactRelationship", "", new { @style = "width:130px", @maxlength = "50" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("EmergencyContactRelationship", emp.EmergencyContactRelationship, new { @style = "width:130px", @maxlength = "50" }));
                                                       }
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
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("BankName", "", new { @style = "width:130px", @maxlength = "100" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("BankName", emp.BankName, new { @style = "width:130px", @maxlength = "100" }));
                                                       }
                                                    %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="110px" class="label last">
                                                    Bank Account
                                                </td>
                                                <td class="input last">
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextBox("BankAccount", "", new { @style = "width:130px", @maxlength = "20" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("BankAccount", emp.BankAccount, new { @style = "width:130px", @maxlength = "20" }));
                                                       }
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
                <td style="padding: 0px;">
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
                                                <strong>Permanent Address </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PermanentAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentAddress))
                                                       {
                                                           Response.Write(Html.TextBox("PermanentAddress", emp.PermanentAddress, new { @style = "width:230px", @maxlength = "255" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("PermanentAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PermanentArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentArea))
                                                       {
                                                           Response.Write(Html.TextBox("PermanentArea", emp.PermanentArea, new { @style = "width:90px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("PermanentArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PermanentDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentDistrict))
                                                       {
                                                           Response.Write(Html.TextBox("PermanentDistrict", emp.PermanentDistrict, new { @style = "width:110px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("PermanentDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PermanentCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentCityProvince))
                                                       {
                                                           Response.Write(Html.TextBox("PermanentCityProvince", emp.PermanentCityProvince, new { @style = "width:100px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("PermanentCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.DropDownList("PermanentCountry", ViewData["Nationality"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentCountry))
                                                       {
                                                           Response.Write(Html.DropDownList("PermanentCountry", null, new { @style = "width:96px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.DropDownList("PermanentCountry", null, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="text-align: left">
                                                <strong>VN Permanent Address </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnPermanentAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnPermanentAddress))
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentAddress", emp.VnPermanentAddress, new { @style = "width:230px", @maxlength = "255" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnPermanentArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnPermanentArea))
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentArea", emp.VnPermanentArea, new { @style = "width:90px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnPermanentDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnPermanentDistrict))
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentDistrict", emp.VnPermanentDistrict, new { @style = "width:110px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("PermanentCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnPermanentCityProvince))
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentCityProvince", emp.VnPermanentCityProvince, new { @style = "width:100px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnPermanentCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.DropDownList("VnPermanentCountry", ViewData["VnNationality"] as SelectList, "-Quốc gia-", new { @style = "width:96px" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentCountry))
                                                       {
                                                           Response.Write(Html.DropDownList("VnPermanentCountry", null, new { @style = "width:96px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.DropDownList("VnPermanentCountry", null, "-Quốc gia-", new { @style = "width:96px" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="text-align: left">
                                                <strong>Temp Address </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("TempAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.TempAddress))
                                                       {
                                                           Response.Write(Html.TextBox("TempAddress", emp.TempAddress, new { @style = "width:230px", @maxlength = "255" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("TempAddress", Constants.ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.ADDRESS + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("TempArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.TempArea))
                                                       {
                                                           Response.Write(Html.TextBox("TempArea", emp.TempArea, new { @style = "width:90px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("TempArea", Constants.AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.AREA + "')" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("TempDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.TempDistrict))
                                                       {
                                                           Response.Write(Html.TextBox("TempDistrict", emp.TempDistrict, new { @style = "width:110px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("TempDistrict", Constants.DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.DISTRICT + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("TempCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.TempCityProvince))
                                                       {
                                                           Response.Write(Html.TextBox("TempCityProvince", emp.TempCityProvince, new { @style = "width:100px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("TempCityProvince", Constants.CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.CITYPROVINCE + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.DropDownList("TempCountry", ViewData["Nationality"] as SelectList, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentCountry))
                                                       {
                                                           Response.Write(Html.DropDownList("TempCountry", null, new { @style = "width:96px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.DropDownList("TempCountry", null, Constants.FIRST_ITEM, new { @style = "width:96px" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="label" style="text-align: left">
                                                <strong>VN Temp Address </strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnTempAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnTempAddress))
                                                       {
                                                           Response.Write(Html.TextBox("VnTempAddress", emp.VnTempAddress, new { @style = "width:230px", @maxlength = "255" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnTempAddress", Constants.VN_ADDRESS, new { @style = "width:230px", @maxlength = "255", @onfocus = "ShowOnFocus(this,'" + Constants.VN_ADDRESS + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_ADDRESS + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnTempArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnTempArea))
                                                       {
                                                           Response.Write(Html.TextBox("VnTempArea", emp.VnTempArea, new { @style = "width:90px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnTempArea", Constants.VN_AREA, new { @style = "width:90px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_AREA + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_AREA + "')" }));
                                                       }
                                                   }
                                                %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="input">
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnTempDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnTempDistrict))
                                                       {
                                                           Response.Write(Html.TextBox("VnTempDistrict", emp.VnTempDistrict, new { @style = "width:110px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnTempDistrict", Constants.VN_DISTRICT, new { @style = "width:110px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_DISTRICT + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_DISTRICT + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.TextBox("VnTempCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.VnTempCityProvince))
                                                       {
                                                           Response.Write(Html.TextBox("VnTempCityProvince", emp.VnTempCityProvince, new { @style = "width:100px", @maxlength = "30" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextBox("VnTempCityProvince", Constants.VN_CITYPROVINCE, new { @style = "width:100px", @maxlength = "30", @onfocus = "ShowOnFocus(this,'" + Constants.VN_CITYPROVINCE + "')", @onblur = "ShowOnBlur(this,'" + Constants.VN_CITYPROVINCE + "')" }));
                                                       }
                                                   }
                                                %>
                                                <span class="fsep"></span>
                                                <% if (ViewData.Model == null)
                                                   {
                                                       Response.Write(Html.DropDownList("VnTempCountry", ViewData["VnNationality"] as SelectList, "-Quốc gia-", new { @style = "width:96px" }));
                                                   }
                                                   else
                                                   {
                                                       if (!string.IsNullOrEmpty(emp.PermanentCountry))
                                                       {
                                                           Response.Write(Html.DropDownList("VnTempCountry", null, new { @style = "width:96px" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.DropDownList("VnTempCountry", null, "-Quốc gia-", new { @style = "width:96px" }));
                                                       }
                                                   }
                                                %>
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
                                                    <% if (ViewData.Model == null)
                                                       {
                                                           Response.Write(Html.TextArea("Remarks", new { @style = "width: 370px; height: 87px;", @maxlength = "1000" }));
                                                       }
                                                       else
                                                       {
                                                           Response.Write(Html.TextArea("Remarks", emp.Remarks, new { @style = "width: 370px; height: 87px;", @maxlength = "1000" }));
                                                       }
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
                    <div class="cbutton" style="text-align: right">
                        <input type="submit" title="Save" class="save" value="" />
                        <input type="button" title="Cancel" onclick="<%=javascript %>" id="btnCancel" class="cancel"
                            value="" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
