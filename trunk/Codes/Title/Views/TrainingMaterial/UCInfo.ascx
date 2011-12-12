<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% Training_Material obj = (Training_Material)ViewData.Model;
   string trCourse = "display:none";
   string trCategorry = "display:none"; %>
<%=Html.Hidden("returnUrl", Request["returnUrl"]) %>
<div id="summary" style="display:none" class=""></div>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <tr>
        <td class="label required">
            Type Material <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write("<label for='tCourse'>" + Html.RadioButton("chk_TypeMaterial", Constants.TRAINING_MATERIAL_TYPE_COURSE, true, new { id = "tCourse" }) + " Course</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ");
                   Response.Write("<label for='tCategory'>" + Html.RadioButton("chk_TypeMaterial", Constants.TRAINING_MATERIAL_TYPE_CATEGORY, false, new { id = "tCategory" }) + " Category</label>");
                   Response.Write(Html.Hidden("TypeOfMaterial", Constants.TRAINING_MATERIAL_TYPE_COURSE));
                   trCourse = string.Empty;
               }
               else
               {
                   bool isCourse = false;
                   if (obj.TypeOfMaterial == Constants.TRAINING_MATERIAL_TYPE_COURSE)
                   {
                       isCourse = true;
                       trCourse = string.Empty;
                   }
                   else
                   {
                       trCategorry = string.Empty;
                   }
                   Response.Write("<label for=tCourse>" + Html.RadioButton("chk_TypeMaterial", Constants.TRAINING_MATERIAL_TYPE_COURSE, isCourse, new { id = "tCourse" }) + " Course</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ");
                   Response.Write("<label for=tCategory>" + Html.RadioButton("chk_TypeMaterial", Constants.TRAINING_MATERIAL_TYPE_CATEGORY, !isCourse, new { id = "tCategory" }) + " Category</label>");
                   Response.Write(Html.Hidden("TypeOfMaterial", obj.TypeOfMaterial));
                   Response.Write(Html.Hidden("ID", obj.ID));
               }
            %>
        </td>
    </tr>
    <tr id="trCourse" style=<%=trCourse %>>
        <td class="label required">
            Course <span>*</span>
        </td>
        <td class="input">
            <%
                if (ViewData.Model == null)
                {
                    Response.Write(Html.TextBox("CourseName", string.Empty, new { @readonly = "readonly" }));
                    Response.Write(Html.Hidden("CourseId", string.Empty));
                }
                else
                {
                    Training_Course objCourse = new TrainingCourseDao().GetById(ConvertUtil.ConvertToInt(obj.CourseId));
                    Response.Write(Html.TextBox("CourseName", objCourse == null ? string.Empty : objCourse.Name, new { @readonly = "readonly" }));
                    Response.Write(Html.Hidden("CourseId", obj.CourseId));
                }
            %>
            <button class="icon select" onclick="CRM.pInPopup('/Common/ListCourse?n=1&courseType=<%=Request["Type"]%>', 'Select Course Name' ,'800')"
                title="Select Course Name" type="button">
            </button>
        </td>
    </tr>
    <tr id="trCategory" style=<%=trCategorry %>>
        <td class="label required">
            Category <span>*</span>
        </td>
        <td class="input">
            <%
                if (ViewData.Model == null)
                {
                    Response.Write(Html.TextBox("Category", string.Empty, new { @style = "width:300px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("Category", obj.Category, new { @style = "width:300px" }));
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            File to Upload <span>*</span>
        </td>
        <td class="input">
            <div>
                <div id="divImageName">
                    <table width="100%">
                        <tr>
                            <td>
                                <span id="spanImageName" style="margin:2px 0px 2px 5px;">No File</span>
                                <%=Html.Hidden("hidFileDisplayName") %>
                                <%
                                    if (obj == null)
                                    {
                                        Response.Write(Html.Hidden("ServerImageName"));
                                    }
                                    else
                                    {
                                        Response.Write(Html.Hidden("ServerImageName", obj.UploadFile));
                                    }
                                %>   
                                <input id="fUpload" name="fUpload" type="file"/>
                            </td>
                            <td style="width:20px; text-align: right">
                                <input type='button' id="btnRemoveFile" class='icon delete' onclick="removeFile();" />        
                            </td>
                        </tr>
                    </table>
                </div>
                
            </div>  
            
            

            <%--<div id="divImageName" style="width: 400px">
                <table width="400px">
                    <tr>
                        <td>
                            <%
                                if (obj == null)
                                {
                                    Response.Write("<span type='text' name='spanFileName' id='spanFileName' style='margin:2px 0px 2px 5px;'>No File</span>");
                                    Response.Write(Html.Hidden("UploadFile"));
                                }
                                else
                                {
                                    Response.Write("<span name='spanFileName' id='spanFileName' style='margin:2px 0px 2px 5px;'>" + obj.UploadFile + "</span>");
                                    Response.Write(Html.Hidden("UploadFile", obj.UploadFile));
                                }
                            %>
                        </td>
                        <td style="width: 20px; text-align: left">
                            <input type='button' id="btnRemoveFile" class='icon delete' onclick="removeFile();" />
                        </td>
                    </tr>
                </table>
            </div>
            <input id="fUpload" name="fUpload" type="file" />--%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Material Title <span>*</span>
        </td>
        <td class="input">
            <%
                if (ViewData.Model == null)
                {
                    Response.Write(Html.TextBox("Title", string.Empty, new { @style = "width:300px" }));
                }
                else
                {
                    Response.Write(Html.TextBox("Title", obj.Title, new { @style = "width:300px" }));
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Permission <span>*</span>
        </td>
        <td class="input">
            <%
                if (ViewData.Model == null)
                {
                    Response.Write("<label for=Public>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_PUBLIC, true, new { id = "Public" }) + " Public</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Response.Write("<label for=AdminOnly>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_ADMIN, false, new { id = "AdminOnly" }) + " Admin Only</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Response.Write("<label id='lblInCourse' for=InCourse>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_INCOURSE, false, new { id = "InCourse" }) + " In Course</label>");
                    Response.Write(Html.Hidden("Permission", Constants.TRAINING_MATERIAL_PERMISSON_PUBLIC));
                }
                else
                {
                    bool isPublic = false;
                    bool isAdmin = false;
                    bool isCourse = false;
                    string css = "style='display:none'";
                    if (obj.Permission == Constants.TRAINING_MATERIAL_PERMISSON_PUBLIC)
                    {
                        isPublic = true;
                    }
                    else if (obj.Permission == Constants.TRAINING_MATERIAL_PERMISSON_ADMIN)
                    {
                        isAdmin = true;
                    }
                    else
                    {
                        isCourse = true;
                    }
                    Response.Write("<label for=Public>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_PUBLIC, isPublic, new { id = "Public" }) + " Public</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    Response.Write("<label for=AdminOnly>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_ADMIN, isAdmin, new { id = "AdminOnly" }) + " Admin Only</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                    if (string.IsNullOrEmpty(obj.Category))
                    {
                        css = string.Empty;
                    }
                    Response.Write("<label " + css + " id='lblInCourse' for=InCourse>" + Html.RadioButton("chk_TypePermisson", Constants.TRAINING_MATERIAL_PERMISSON_INCOURSE, isCourse, new { id = "InCourse" }) + " In Course</label>");
                    
                    Response.Write(Html.Hidden("Permission", obj.Permission));
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Active <span>*</span>
        </td>
        <td class="input">
            <%
                if (ViewData.Model == null)
                {
                    Response.Write(Html.CheckBox("IsActive", true));
                }
                else
                {
                    if (obj.IsActive.HasValue)
                    {
                        Response.Write(Html.CheckBox("IsActive", obj.IsActive.Value));
                    }
                    else
                    {
                        Response.Write(Html.CheckBox("IsActive", false));
                    }
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Description
        </td>
        <td class="input">
            <%=Html.TextArea("Description", ViewData.Model != null ? obj.Description : "", new { @style = "width:500px; height:100px", @maxlength = "4000" })%>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
<style type="text/css">
    .form .edit .headerrow
    {
        color: Blue !important;
        text-align: left !important;
        padding-left: 50px;
    }
    .form div.cancel
    {
        background: none !important;
        text-align: right;
    }
    #divImageName
    {
        border: 1px solid #aaaaaa;
        background-color: #eeeeee;
        font-weight: bold;
        margin-bottom: 2px;
        width:500px;
    }
    #fUploadUploader
    {
        background: url('/Scripts/uploadify/upload.gif') no-repeat;
    }
    #fUploadQueue
    {
        width: 300px;
    }
    #fUploadQueue div.cancel
    {
        display: none;
    }
    .uploadifyQueue div.uploadifyQueueItem
    {
        width: 300px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $("input[name=chk_TypeMaterial]").change(function () {
            var option = $(this).val();
            $("#TypeOfMaterial").val(option);
            if (option == '<%= Constants.TRAINING_MATERIAL_TYPE_CATEGORY%>') {
                $("#trCourse").css("display", "none");
                $("#trCategory").css("display", "");
                $("#lblInCourse").css("display", "none");
                $("#InCourse").attr("checked", false);
                $("#CourseName").rules("remove");
                $("#Category").rules("add", "required");
                $("#CourseName").val("");
                $("#CourseId").val("");
            }
            else {
                $("#trCourse").css("display", "");
                $("#trCategory").css("display", "none");
                $("#lblInCourse").css("display", "");
                $("#Category").rules("remove");
                $("#CourseName").rules("add", "required");
                $("#Category").val("");
            }
        });

        $("input[name=chk_TypePermisson]").change(function () {
            $("#Permission").val($(this).val());
        });

        $("#frmCourse").validate({
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
                chk_TypeMaterial: { required: true },
                CourseName: { required: true },
                Title: { required: true },
                chk_TypePermisson: { required: true },
                Description: { maxlength: 4000 },
                UploadFile: { required: true },
                ServerImageName: { required: true }
            }
        });

        $(function () {
            <%
                //var obj = ViewData.Model as Training_Material;
                if(obj != null && !string.IsNullOrEmpty(obj.UploadFile))
                {
                    Response.Write("showFile('" + obj.UploadFileDisplayName + "');");
                }
                else
                {
                    Response.Write("showFile('');");
                }
            %>
        });
    });
</script>
