<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= TempData["Message"]%>
<div style="padding-left:10px;padding-top:10px">
<div id="treecontrol">
	<a title="Collapse the entire tree below" href="#">Collapse All</a> | 
	<a title="Expand the entire tree below" href="#">Expand All</a> | 
	<a title="Toggle the tree below, opening closed branches, closing open branches" href="#">Toggle All</a>
</div>
<b>Menu</b>
<input type="button" class="icon add" onclick="showPopup('/Menu/Create/','Create top menu', 550)" />
<ul id="menutree" style="margin-left: 10px;" >
<%
    Html.RenderAction("CreateMenuTree");
%>
</ul>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    <%=CommonPageInfo.AppName + CommonPageInfo.AppSepChar + MenuPageInfo.FunctionName %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/Treeview/jquery.treeview.css" />	
<script src="/Scripts/Treeview/lib/jquery.cookie.js" type="text/javascript"></script>
<script src="/Scripts/Treeview/jquery.treeview.min.js" type="text/javascript"></script>
<link href="/Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
<script src="/Scripts/uploadify/swfobject.js" type="text/javascript"></script>
<script type="text/javascript">
    function removeFile() {
        $("#ServerImageName").val("");
        showMenuImage("");
    }
    function showMenuImage(imageUrl) {
        if (imageUrl != "") {
            //            $("#ServerImageName").val(imageUrl);
            //            $("#divImageName").show();
            $("#spanImageName").html(
                        "<img style='width:16px; height:16px; margin-right: 10px;vertical-align: bottom;' src='" +
                        "<%=Constants.MENU_PAGE_ICON_FOLDER%>" + imageUrl + "'/>");
            $("#btnRemoveFile").show();
            //$("#fUploadUploader").hide();
        }
        else {
            $("#spanImageName").html("No Image");
            $("#btnRemoveFile").hide();
        }
    }
    function showPopup(url, title, w) {
        CRM.loading();
        $.ajax({
            async: false,
            cache: false,
            type: "GET",
            dataType: "html",
            timeout: 1000,
            url: url,
            success: function (msg) {
                CRM.summary('', 'none', '');
                $(CONST_POPUP_CONTENT).html(msg);
                $(CONST_POPUP_TITLE).html(title);
                $(CONST_POPUP_CLOSE).attr('href', 'javascript:void(0);').click(function () { CRM.closePopup(); });
                jQuery.blockUI({
                    message: $(CONST_POPUP),
                    css: {
                        border: 'none',
                        backgroundColor: 'transparent',
                        opacity: 1,
                        textAlign: 'left',
                        top: ($(window).height() - $(CONST_POPUP).height() - 34) / 2 + 'px',
                        left: ($(window).width() - w) / 2 + 'px',
                        width: w + 'px'
                    },
                    onBlock: function () {
                        $('#fUpload').uploadify({
                            'uploader': '<%= Url.Content("/Scripts/uploadify/uploadify.swf") %>',
                            'script': '/Library/GenericHandle/UploadFileHandler.ashx?Page=Menu',
                            'fileExt': '<%=Constants.MENU_PAGE_IMAGE_FORMAT_ALLOWED%>',
                            'cancelImg': '<%= Url.Content("/Scripts/uploadify/cancel.png") %>',
                            'fileDesc': 'Image files (*.jpg; *.png; *.gif)',
                            'sizeLimit': parseInt('<%=(int)Constants.MENU_PAGE_IMAGE_MAX_SIZE%>') * 1024 * 1024,
                            //'sizeLimit': '<%=(int)Constants.MENU_PAGE_IMAGE_MAX_SIZE * 1024 * 1024%>',
                            'width'     : 16,
                            'height'    : 16,
                            'wmode'     : 'transparent',
                            'removeCompleted': true,
                            'auto': true,
                            'buttonText': 'Choose File',
                            'hideButton': true,
                            'onComplete': function (event, ID, fileObj, response, data) {
								var jsonData = $.parseJSON(response);
								if (jsonData.isSuccess == 1) {
									
                                    CRM.summary("", "none", "");
                                    $("#ServerImageName").val(jsonData.fileName);
									
                                    $("#spanImageName").html(
                                        "<img style='width:16px; height:16px; margin-right: 10px;vertical-align: bottom;' src='" +
                                        "<%=Constants.UPLOAD_TEMP_PATH%>" + jsonData.fileName + "'/>" + fileObj.name);
                                    $("#btnRemoveFile").show();
                                    //$("#fUploadUploader").hide();
                                    //$("#divImageName").show();
									
                                }
                                else {
									
                                    CRM.summary(jsonData.message, "block", "msgError");
                                    //removeFile();
                                    $("#fUploadQueue").remove();
                                    //$('#fUpload').uploadifyCancel($('.uploadifyQueueItem').first().attr('id').replace('fUpload',''));
                                }
                            },
                            'onError'   : function (event,ID,fileObj,errorObj) {
                                if( errorObj.type == 'File Size'){
                                    CRM.summary(CRM.format(E0012, '<%=Constants.MENU_PAGE_IMAGE_MAX_SIZE%>'), "block", "msgError");
                                    removeFile();
                                    $("#fUploadQueue").remove();

                                    //$("#ServerImageName").val("");
                                    //$("#divImageName").hide();
                                }
                            }
                        });
                    }
                });
                $('.blockUI').css('cursor', 'default');
                CRM.completed();
            }
        });
    };
    $(document).ready(function () {
        $("#menutree").treeview({
            animated: "fast",
            persist: "cookie",
            control: "#treecontrol"
        });
    });
</script>
<style type="text/css">
    ul.treeview a.menu_active
    {
        text-decoration:none;
        color: Blue;
    }
    ul.treeview a.menu_inactive
    {
        text-decoration:none;
        color: Gray;
    }
    ul.treeview a:hover
    {
        text-decoration:none;
        color: Red;
        font-weight:bold;
    }
</style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
    <%=MenuPageInfo.FunctionName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
    <%=CommonFunc.GetCurrentMenu(Request.RawUrl,true)%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
