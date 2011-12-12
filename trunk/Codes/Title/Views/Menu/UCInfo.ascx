<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="form" style="width:100%">
    <div id="summary" style="display:none" class=""></div>
    <label id="lbltest"></label>
    <%
        CRM.Models.Menu menu = ViewData.Model as CRM.Models.Menu;
        Response.Write(Html.Hidden("ParentId"));
    %>
    <table class="edit" cellpadding="0" cellspacing="0" border="0"  style="width:100%" >
        <tr>
            <td class="label required" >
                Name<span>*</span>
            </td>
            <td class="input" style = "width: 200px">
                <%=Html.TextBox("Name", null, new { @style = "width:160px" })%>
            </td>
            <td class="label required">
                Display order<span>*</span>
            </td>
            <td class="input">
                <%=Html.TextBox("DisplayOrder", null, new { @style = "width:27px" })%>
            </td>
        </tr>
        <tr>
            <td class="label">
                Link
            </td>
            <td class="input">
                <%=Html.TextBox("Link", null, new { @style = "width:160px" })%>
            </td>
            <td class="label">
                Is Active
            </td>
            <td class="input">
                <%
                    if (menu == null)
                        Response.Write(Html.CheckBox("IsActive", true));
                    else
                        Response.Write(Html.CheckBox("IsActive"));
                        
                %>
            </td>
        </tr>
        <tr>
            <td class="label" style="vertical-align:top">
                Image
            </td>
            <td class="input" colspan="3">
                <div id="divImageName">
                    <table width="100%">
                        <tr>
                            <td>
                                <span id="spanImageName" style="margin:2px 0px 2px 5px;">No Image</span>        
                            </td>
                            <td style="width:20px; text-align: right">
                                <input type='button' id="btnRemoveFile" class='icon delete' onclick="removeFile();" />        
                            </td>
                        </tr>
                    </table>
                </div>
                <input id="fUpload" name="fUpload" type="file" />
                <%
                    if (menu == null)
                    {
                        Response.Write(Html.Hidden("ServerImageName"));
                    }
                    else
                    {
                        Response.Write(Html.Hidden("ServerImageName", menu.ImageUrl));
                    }
                %>
            </td>
        </tr>
        <tr>
            <td class="label" colspan="4">
                Permission &nbsp;&nbsp;&nbsp;
                <button type="button" id="btnAddRow" class="icon plus"></button>
                <button type="button" id="btnRemoveRow" class="icon minus"></button>
            </td>
        </tr>
        <tr>
            <td colspan = "4">
                <table id="tblModule" width="100%" cellpadding="0" cellspacing="0" border="0">
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align:center; vertical-align:bottom; padding-top:20px" colspan="4">
                <input class="save" type="submit" alt="" value="" />
                <input class="cancel" type="button" onclick="CRM.closePopup();" alt="" value=""/>
            </td>
        </tr>
    </table> 
</div>


<script type="text/javascript">

    var moduleIdPrefix = "<%=Constants.MENU_PAGE_MODULE_ID_PREFIX%>";
    var moduleSelectName = "<%=Constants.MENU_PAGE_MODULE_NAME%>";
    var permissionIdPrefix = "<%=Constants.MENU_PAGE_PERMISSION_ID_PREFIX%>";
    var permissionSelectName = "<%=Constants.MENU_PAGE_PERMISSION_NAME%>";
    var moduleLabel = "<%=Constants.MENU_PAGE_MODULE_LABEL%>";
    var PermissionLabel = "<%=Constants.MENU_PAGE_PERMISSION_LABEL%>";
    var urlTarget = '<%=Url.Action("GetModuleList") %>';
    var rowTemplate = "<tr>" +
                            "<td class='label required' style='width:60px !important'>" +
                                "Module<span>*</span>" +
                            "</td>" +
                            "<td class='input'>" +
                                "{0}" +
                            "</td>" +
                            "<td class='label required' style='width:80px !important'>" +
                                "Permission<span>*</span>" +
                            "</td>" +
                            "<td class='input'>" +
                                "{1}" +
                            "</td>" +
                        "</tr>";
    var selectTemplate = "<select id='{0}' style='width:130px' name='{1}'>{2}</select>";
    
    function uploadLastFile()
    {
        //var queueId = $('div.uploadifyQueueItem:first').attr('id').replace('fUpload','');
        
    }
    /*Add new module row*/
    function addModuleRow() {
        var rowCount = $("#tblModule tr").length;
        $.ajax({
            async: false,
            cache: true,
            type: "GET",
            dataType: "json",
            timeout: 1000,
            url: urlTarget,
            error: function () {
                alert("error");
            },
            success: function (data) {
                var newModlueOptions = "<option value=''>" + moduleLabel + "</option>";
                var selectPermissionObj = $.format(selectTemplate, permissionIdPrefix + rowCount, permissionIdPrefix + rowCount,
                    "<option value=''>" + PermissionLabel + "</option>");
                $(data.modules).each(function (index) {
                    newModlueOptions += "<option value='" + this.Value + "'>" + this.Text + "</option>";
                });
                var selectModuleObj = $.format(selectTemplate, moduleIdPrefix + rowCount, moduleIdPrefix + rowCount, newModlueOptions);
                var newRow = $.format(rowTemplate, selectModuleObj, selectPermissionObj);
                $("#tblModule").append(newRow);
                $("#" + moduleIdPrefix + rowCount).rules("add",
                { required: true });
                $("#" + permissionIdPrefix + rowCount).rules("add",
                { required: true });
                $("#" + moduleIdPrefix + rowCount).change(function () {
                    var selectedValue = $(this).val();
                    if (selectedValue != "") {
                        $.ajax({
                            async: false,
                            cache: true,
                            type: "GET",
                            dataType: "json",
                            timeout: 1000,
                            url: '/Menu/GetPermissionList/' + selectedValue,
                            error: function () {
                                alert("error");
                            },
                            success: function (data) {
                                var newPermissionOptions = "<option value=''>" + PermissionLabel + "</option>";
                                $(data.permissions).each(function (index) {
                                    newPermissionOptions += "<option value='" + this.PermissionId + "'>" + this.Text + "</option>";
                                });
                                $("#" + permissionIdPrefix + rowCount).html(newPermissionOptions);
                            }
                        });
                    }
                    else {
                        $("#" + permissionIdPrefix + rowCount).html("<option value=''>" + PermissionLabel + "</option>");
                    }
                });

            }
        });

    }
    /*end add new module row*/
   
    $(document).ready(function () {
        $.validator.addMethod('integer', function (value, element, param) {
            if ($.trim(value) == '') {
                return true;
            }
            else {
                return (value == parseInt(value, 10));
            }
        }, E0037);
        
        $("#frmMenuForm").validate({
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
                Name: { required: true, maxlength: '<%=CommonFunc.GetLengthLimit(new CRM.Models.Menu(), "Name")%>' },
                Link: { maxlength: '<%=CommonFunc.GetLengthLimit(new CRM.Models.Menu(), "Link")%>' },
                DisplayOrder: { required: true, maxlength: 2, integer: true }
            },
            submitHandler: function (form) {
                form.submit();
            }

        });


        //addModuleRow();
        $("#btnAddRow").click(function () {
            CRM.loading();
            addModuleRow();
            CRM.completed();
        });
        $("#btnRemoveRow").click(function () {
            $("#tblModule tr:last").remove();
        });
        
        $(function () {
            <%
                CRM.Models.Menu menuObj = ViewData.Model as CRM.Models.Menu;
                if(menuObj != null && !string.IsNullOrEmpty(menuObj.ImageUrl))
                {
                    Response.Write("showMenuImage('" + menuObj.ImageUrl + "');");
                }
                else
                {
                    Response.Write("showMenuImage('');");
                }
                if(menuObj != null)
                {
                    var mpList = ViewData[CommonDataKey.MENU_PERMISSION_LIST] as List<MenuPermission>;
                    int nMp = 0;
                    foreach(var mp in mpList)   
                    {
                        Response.Write("addModuleRow();");
                        Response.Write("$('#" + Constants.MENU_PAGE_MODULE_ID_PREFIX + nMp + "').val('" + mp.ModuleId + "');");
                        Response.Write("$('#" + Constants.MENU_PAGE_MODULE_ID_PREFIX + nMp + "').change();");
                        Response.Write("$('#" + Constants.MENU_PAGE_PERMISSION_ID_PREFIX + nMp + "').val('" + mp.PermissionId + "');");
                        nMp++;
                    }
                }
            %>
        });
    });
</script>
<style type="text/css">
    .form .edit .headerrow
    {
        color:Blue !important; 
        text-align: left !important;
        padding-left: 50px;
    }
    .form div.cancel
    {
        background:none !important;
        text-align:right;
    }
    #divImageName
    {
        border:1px solid #aaaaaa;
        background-color: #eeeeee;
        font-weight: bold;
        margin-bottom: 2px;
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
        display:none;
        
    }
	.uploadifyQueue div.uploadifyQueueItem
	{
		 width: 300px;
	}
</style>