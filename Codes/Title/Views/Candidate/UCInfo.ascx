<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="../../Scripts/highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="../../Scripts/highslide/highslide.css" />
<script type="text/javascript">
    var i = 0;
    hs.graphicsDir = '../../Scripts/highslide/graphics/';
    hs.outlineType = 'rounded-white';
    /////////////////////////////////
    jQuery(document).ready(function () {

        jQuery.validator.addMethod(
            "validatePhone", function (value) {
                return CRM.isPhone(value);
            }, 'Only input letters are : 0123456789.,()-+ ');
        $("#addFormCandidate").validate({
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
                VnFirstName: { required: true },
                VnLastName: { required: true },
                LastName: { required: true },
                FirstName: { required: true },
                TitleId: { required: true },
                DOB: { checkDate: true, checkBirthDate: true },
                Email: { required: true, email: true },
                SearchDate: { required: true, checkDate: true },
                SourceId: { required: true },
                titleId: { required: true },
                OfficeID: { required: true },
                Gender: { required: true }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    form.submit();
                    i++;
                }
            }
        });
        $("#Telephone").keypress(function (event) {
            return CRM.onKeyPress(event, this, '[0-9]', '20');
        });
        $("#Telephone").blur(function () {
            return CRM.onKeyUp(this, 'Number');
        });
        $("#Telephone").mousemove(function () {
            return CRM.onKeyUp(this, 'Number');
        });
        $("#btnChangePhoto").click(function () {
            var url = "/Common/UploadImage?controller=Candidate&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.PHOTO_PATH_ROOT_CANDIDATE %>";
            CRM.popUpWindow(url, '#Photograph', 'Upload Photo');
            return false;
        });
        $("#btnUpload_CV").click(function () {
            var url = "/Common/UploadFile?controller=Candidate&recordID=" + $('#ID').val() + "&saveTo=<%=Constants.CV_PATH_ROOT_CANDIDATE %>";
            CRM.popUpWindow(url, '#CVFile', 'Upload CV');
            return false;
        });
        $("#btndownload_cv").click(function () {
            var file_path = '<%=Constants.CV_PATH_ROOT_CANDIDATE %>' + "//" + $('#CVFile').val();
            var outputname = $('#empFullName').val();
            CRM.downLoadFile(file_path, outputname == "" ? "Candidate's_CV" : outputname + "'s_CV");
            return false;
        });
        $("#DOB").datepicker({
            yearRange: "-100:100"
        });
        $("#SearchDate").datepicker({
            onClose: function () { $(this).valid(); }
        });
        $("#btnCancel").click(function () {
            window.location.href = "/Candidate";
        });
        $("#BranchId").change(function () {
            var selectedBranch = $(this).val();
            if (selectedBranch == "") {
                $("#OfficeID").html("<option value=''>Choose branch first !</option>");
                $("#OfficeID").attr("disabled", "disabled");
                
                $("td.officelabel").html("Office").removeClass("required");
            }
            else {
                var url = "/Candidate/GetOfficeList?id=" + selectedBranch + "&selectedValue=" + $("#hidSelectedOffice").val();
                $.ajax({
                    async: false,
                    cache: false,
                    type: "GET",
                    dataType: "json",
                    timeout: 1000,
                    url: url,
                    error: function () {
                        CRM.message("error", "block", "msgError");
                    },
                    success: function (result) {
                        $("#OfficeID").html(result);
                        $("#OfficeID").removeAttr("disabled");
                        
                        $("td.officelabel").html("Office<span>*</span>").addClass("required");
                    }
                });
            }
        });
        $(function () {
            $("#BranchId").change();
        });
    });
</script>
<%
    Candidate can = (Candidate)ViewData.Model;
    int selectedOffice = 0;
    if (ViewData.Model == null)
    {
        Response.Write(Html.Hidden("empFullName", ""));
    }
    else
    {
        selectedOffice = can.OfficeID.HasValue ? can.OfficeID.Value : 0;
        Response.Write(Html.Hidden("empFullName", (can.FirstName
            + " " + can.MiddleName + " "
            + can.LastName).Replace(" ", "_")));
    }
    Response.Write(Html.Hidden("hidSelectedOffice", selectedOffice));
  %>
<table cellspacing="0" cellpadding="0" border="0" width="1024px" class="form profile">
    <tbody>
        <tr>
            <td class="ctbox">
                <h2>
                    Candidate Information</h2>
            </td>
        </tr>
        <tr>
            <td valign="top" class="ccbox">
                <table cellspacing="0" cellpadding="0" border="0" width="1024px" class="edit">
                    <tbody>
                        <tr>
                            <td class="label required" style="width: 180px">
                                First Name <span>*</span>
                            </td>
                            <td class="input" style="width: 170px">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("FirstName", "", new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("FirstName", can.FirstName, new { @maxlength = "20", @style = "width:120px" }));

                                   }
                                %>
                            </td>
                            <td class="label" style="width: 100px">
                                Middle Name
                            </td>
                            <td class="input" style="width: 170px">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("MiddleName", "", new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("MiddleName", can.MiddleName, new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label required" style="width: 100px">
                                Last Name <span>*</span>
                            </td>
                            <td class="input" style="width: 150px">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("LastName", "", new { @maxlength = "30", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("LastName", can.LastName, new { @maxlength = "30", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td rowspan="6" style="width: 80px; padding-left: 20px" valign="top" align="left">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td valign="top" width="140px">
                                            <%
                                                string labelPhoto = "<img src='/Content/Images/Common/nopic.gif' height='120px' width='120px' />";
                                                if (ViewData.Model == null)
                                                {
                                                    Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                }
                                                else
                                                {
                                                    string path = Server.MapPath(Constants.PHOTO_PATH_ROOT_CANDIDATE) + can.Photograph;

                                                    if (string.IsNullOrEmpty(can.Photograph) || !System.IO.File.Exists(path))
                                                    {
                                                        Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                    }
                                                    else
                                                    {
                                                        labelPhoto = "<a id='thumb1' href='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + can.Photograph
                                                                        + "' class='highslide' onclick='return hs.expand(this)'>"
                                                                        + "<img id='imgPhoto' src='" + Constants.PHOTO_PATH_ROOT_CANDIDATE + can.Photograph
                                                                        + "' alt='Highslide JS' title='Click to enlarge' height='120px' width='120px' /></a>";
                                                        Response.Write("<div id='spanPhoto'>" + labelPhoto + "</div>");
                                                    }
                                                }
                                                if (ViewData.Model == null)
                                                {
                                                    Response.Write(Html.Hidden("Photograph", ""));
                                                    Response.Write(Html.Hidden("CVFile", ""));
                                                    Response.Write(Html.Hidden("ID", ""));
                                                }
                                                else
                                                {

                                                    Response.Write(Html.Hidden("Photograph", can.Photograph));
                                                    Response.Write(Html.Hidden("CVFile", can.CVFile));
                                                    Response.Write(Html.Hidden("UpdateDate", can.UpdateDate.ToString()));
                                                    Response.Write(Html.Hidden("ID", can.ID));
                                                } 
                                            %>
                                        </td>
                                        <td valign="top" align="left" style="width: 20px">
                                            <input type="button" class="upload_image" id="btnChangePhoto" value="" title="Change Photo" />
                                            <%  string stylePhoto = "display: none";
                                                string styleCV = "display: none";
                                                if (ViewData.Model != null)
                                                {
                                                    if (!string.IsNullOrEmpty(can.Photograph))
                                                    {
                                                        stylePhoto = "display: block";
                                                    }
                                                    if (!string.IsNullOrEmpty(can.CVFile))
                                                    {
                                                        styleCV = "display: block";
                                                    }
                                                }
                                            %>
                                            <input type="button" id="btnRemoveImage" class="remove_image" style="<%=stylePhoto %>"
                                                onclick="CRM.msgConfirmBox('Are you sure you want to remove Photo?', 450, 'CRM.removeImage(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')');"
                                                value="" title="Remove Photo" />
                                            <input type="button" class="upload_cv" value="" id="btnUpload_CV" title="Upload CV" />
                                            <input type="button" id="btnRemoveCV" class="remove_cv" style="<%=styleCV %>" value=""
                                                title="Remove CV" onclick="CRM.msgConfirmBox('Are you sure you want to remove CV?', 450, 'CRM.removeCVFile(\'<%=Constants.CANDIDATE_DEFAULT_VALUE %>\')');" />
                                            <input type="button" id="btndownload_cv" class="download_cv" style="<%=styleCV %>"
                                                value="" title="Download CV" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="label required">
                                VN First Name <span>*</span>
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("VnFirstName", "", new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("VnFirstName", can.VnFirstName, new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label">
                                VN Middle Name
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("VnMiddleName", "", new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("VnMiddleName", can.VnMiddleName, new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label required">
                                VN Last Name <span>*</span>
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("VnLastName", "", new { @maxlength = "20", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("VnLastName", can.VnLastName, new { @maxlength = "20", @style = "width:120px" }));
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
                                       Response.Write(Html.TextBox("DOB", "", new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("DOB", can.DOB.HasValue ? can.DOB.Value.ToString(Constants.DATETIME_FORMAT) : "", new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label">
                                Telephone
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("CellPhone", "", new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("CellPhone", can.CellPhone, new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label required">
                                Email <span>*</span>
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("Email", "", new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("Email", can.Email, new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td class="require label" style="width: 92px;">
                                <b>Gender </b><span style="color: Red">*</span>
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("Gender", null, Constants.FIRST_ITEM, new { @style = "width:125px" })%>
                            </td>
                            <td class="label required">
                                Searched date <span>*</span>
                            </td>
                            <td class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("SearchDate", "", new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("SearchDate", can.SearchDate.ToString(Constants.DATETIME_FORMAT), new { @maxlength = "50", @style = "width:120px" }));
                                   }
                                %>
                            </td>
                            <td class="label required">
                                Source <span>*</span>
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("SourceId", null, Constants.FIRST_ITEM, new { @style = "width:125px" })%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                University
                            </td>
                            <td class="input" style="padding-right:0">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("UniversityDisplay", "", new { @style = "width:120px",@readonly = "readonly" }));
                                       Response.Write(Html.Hidden("UniversityId"));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("UniversityDisplay", can.UniversityId.HasValue ? can.University.Name : "", new { @style = "width:120px", @readonly = "readonly" }));
                                       Response.Write(Html.Hidden("UniversityId", can.UniversityId.HasValue?can.UniversityId.Value.ToString():""));
                                       
                                   }
                                 %>
                                <button type="button" class="icon select" title="Select University" onclick="CRM.listUniversity(); return false;">
                                </button>
                                <button type="button" class="icon remove" title="Remove University" onclick="CRM.clearHidValue('#UniversityDisplay','#UniversityId'); return false;">
                                </button>
                            </td>
                            <td class="label required">
                                Status
                            </td>
                            <td class="input">
                                <%
                                    if (ViewData.Model != null)
                                    {
                                        Candidate candidateObj = (Candidate)ViewData.Model;
                                        if ((candidateObj.Status != (int)CRM.Library.Common.CandidateStatus.Available) && (candidateObj.Status != (int)CRM.Library.Common.CandidateStatus.Unavailable))
                                        {
                                            Response.Write(((CRM.Library.Common.CandidateStatus)candidateObj.Status).ToString());
                                            Response.Write(Html.Hidden("Status", candidateObj.Status));
                                        }
                                        else
                                            Response.Write(Html.DropDownList("Status", null, new { @maxlength = "50", @style = "width:125px" }));

                                    }
                                    else
                                    {
                                        Response.Write(Html.DropDownList("Status", null, new { @maxlength = "50", @style = "width:125px" }));
                                    }
                                    
                                %>
                            </td>
                            <td class="label required">
                                Position <span>*</span>
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("TitleId", null, Constants.FIRST_ITEM, new { @style = "width:125px"})%>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Branch
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("BranchId", null, Constants.FIRST_ITEM, new { @style = "width:125px"})%>
                            </td>
                            <td class="label officelabel">
                                Office
                            </td>
                            <td class="input">
                                <%=Html.DropDownList("OfficeID", null, Constants.FIRST_ITEM, new { @style = "width:145px"})%>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="label">
                                Address
                            </td>
                            <td colspan="5" class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextBox("Address", "", new { @maxlength = "500", @class = "fn-address" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextBox("Address", can.Address, new { @maxlength = "500", @class = "fn-address" }));
                                   }
                                %>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="label">
                                Note
                            </td>
                            <td colspan="5" class="input">
                                <% if (ViewData.Model == null)
                                   {
                                       Response.Write(Html.TextArea("Note", new { @class = "fn-note" }));
                                   }
                                   else
                                   {
                                       Response.Write(Html.TextArea("Note", can.Note, new { @class = "fn-note" }));
                                   }
                                %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <div class="cbutton">
                                    <input type="submit" title="Save" class="save" value="" />
                                    <input type="button" title="Cancel" id="btnCancel" class="cancel" value="" />
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </tbody>
</table>
