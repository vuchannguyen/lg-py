<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <%
        int materialType = ConvertUtil.ConvertToInt(ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_TYPE]);    
    %>
    <div id="cactionbutton">
        <%--<button id="btnRefresh" type="button" title="Refresh" class="button refresh">Refresh</button>--%>
        <button type="button" id="btnDelete" title="Delete" class="button delete">Delete</button>
        <button type="button" id="btnAddNew" title="Add New" class="button addnew">Add New</button>
    </div>
    <div class="trainingdashboard">
        <div class="main-item">
            <div class="user-tabs" style="width: 1024px;">                
                <ul>
	                <li class="tab-head">Material by</li>
                    <li id="usertab1"><a href="SubList?type=1">Professional Courses</a></li>
                    <li id="usertab2"><a href="SubList?type=2">English Courses</a></li>
                    <li id="usertab3"><a href="SubList?type=3">Categories</a></li>
                </ul>
                <div class="clrfix"><br /></div>
            </div>            
            <div id="cfilter">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <input type="text" class="keyword" value="<%=Constants.TRAINING_CENTER_MATERIAL_TXT_KEYWORD_LABEL%>"
                                id="txtKeyword"  onfocus="ShowOnFocus(this,'<%=Constants.TRAINING_CENTER_MATERIAL_TXT_KEYWORD_LABEL%>')" 
                                onblur="ShowOnBlur(this,'<%=Constants.TRAINING_CENTER_MATERIAL_TXT_KEYWORD_LABEL%>')" style="width:200px" />
                        </td>
                        <%
                            if (ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_COURSE] != null)
                            {
                        %>
                        <td>
                            <%= Html.DropDownList(CommonDataKey.TRAINING_CENTER_MATERIAL_COURSE, null, 
                                Constants.TRAINING_CENTER_MATERIAL_LIST_COURSE_LABEL, new { @style = "width:250px" })%>
                        </td>
                        <%
                            }
                            if (ViewData[CommonDataKey.TRAINING_CENTER_MATERIAL_CATEGORY] != null)
                            {
                        %>
                        <td>
                            <%= Html.DropDownList(CommonDataKey.TRAINING_CENTER_MATERIAL_CATEGORY, null, 
                                Constants.TRAINING_CENTER_MATERIAL_LIST_CATEGORY_LABEL, new { @style = "width:150px" })%>
                        </td>
                        <%
                            }
                        %>
                        <td>
                            <button type="button" id="btnFilter" title="Filter" class="button filter">Filter</button>
                        </td>
                    </tr>
                </table>
        
            </div>
            <div class="clist">
                <table id="list" class="scroll">
                </table>
                <div id="pager" class="scroll" style="text-align: center;">
                </div>
            </div>

     </div>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
<%=TrainingCenterPageInfo.FuncMaterial + CommonPageInfo.AppSepChar + TrainingCenterPageInfo.ComName +
        CommonPageInfo.AppSepChar + CommonPageInfo.AppName%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link href="/Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
<script src="/Scripts/uploadify/swfobject.js" type="text/javascript"></script>
<style type="text/css">
    .main-item .ui-jqgrid tr.jqgrow td {
        white-space: normal !important;
    }
    .main-item .ui-jqgrid-hbox
    {
        height: 0px !important;    
    }
    div.materialContent
    {
        margin: 10px 20px 5px 20px;    
    }
    div.materialTitle
    {
        font-weight:bolder;
        font-size:15px;    
    }
    div.materialLastModify
    {
        margin: 5px 0px 5px 0px;
        font-style:italic;    
        font-size:11px;
        color:Gray;
    }
    div.materialDescription
    {
        text-align:justify;    
    }
    div.materialAction
    {
        height:30px;
        vertical-align:middle;
        float:right;
    }
    #list button {
        padding-left: 25px;
        padding-right: 5px;
    }
    #list button.download {
        background-image: url('/Content/Images/Icons/iconDownload.png');
    }
    #list button.down {
        width:70px;
        background-image: url('/Content/Images/ExtraIcons/arrow_270.png');
    }
    #list button.up 
    {
        background-image: url('/Content/Images/ExtraIcons/arrow_090.png');
        width:70px;
    }
    td.moveupbutton
    {
        width:100px;    
    }
    td.movedownbutton
    {
        width:80px;    
    }
    td.editbutton
    {
        width:70px;
    }
    td.downloadbutton
    {
        width:80px;
    }
    div.materialAction table
    {
        width:550px;
        border: none !important;    
    }
    .materialAction table td
    {
        border: none !important;    
    }
    .main-item .ui-state-default, .main-item .ui-widget-content .ui-state-default {
        border: none;
    }
    .main-item .ui-jqgrid tr.ui-row-ltr td {
        border-right: none;
    }
    .trainingdashboard .main-item .item {
        border-bottom: none;
    }
</style>
<script type="text/javascript">
    function checkRadio(elm) {
        var radElement = $(elm).find("input[name='radMaterialType']");
        $(radElement).attr("checked", "checked");
        window.location = '<%=Url.Action("SubList")%>' + "?type=" + $(radElement).val();
    }
    CRM.deleteAllRows = function (listName, colName, action) {
        var gr = jQuery(listName).getGridParam('selarrrow');
        var arrID = "";
        var ids = $(listName).getDataIDs();
        $.each(gr, function (i, rowId) {
            if ($(ids).index(rowId) < 0) {
                var id = $(listName).getCell(rowId, colName);
                if (id != false) {
                    arrID += id + ",";
                }
            }
        });        
        navigateWithReferrer(action + "/" + arrID);        
    }
    //up = true -> move up
    function materialMove(materialId, isUp) {
        jQuery.ajax({
            url: '<%=Url.Action("MaterialMove")%>',
            type: "POST",
            datatype: "json",
            data: ({
                'id': materialId,
                'up': isUp
            }),
            success: function () {
                var targetUrl = getFilterParams();
                $('#list').setGridParam({ url: targetUrl }).trigger('reloadGrid');
            },
            error: function () {
                CRM.msgBox(CRM.format(E0007), '400');
            }
        });
    }
    function downloadFile(materialId, filePath, output) {
        CRM.downLoadFile(filePath, output);
        jQuery.ajax({
            url: '<%=Url.Action("UpdateDownloadTimes")%>',
            type: "POST",
            datatype: "json",
            data: ({
                'id': materialId
            }),
            success: function () {
                var targetUrl = getFilterParams();
                $('#list').setGridParam({ url: targetUrl }).trigger('reloadGrid');
            },
            error: function(){
                CRM.msgBox(CRM.format(E0007), '400');
            }
        });
    }
    function getFilterParams() {        
        var typepage = '<%=Request.QueryString["type"].ToString() %>';
        var key = "";
        
        if (typepage == '<%=Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_CATEGORY%>')
            key = $("#<%=CommonDataKey.TRAINING_CENTER_MATERIAL_CATEGORY%>").val();
        else
            key = $("#<%=CommonDataKey.TRAINING_CENTER_MATERIAL_COURSE%>").val();

        var url = '<%=Url.Action("GetSubListJQGrid")%>' +
            '?name=' + encodeURIComponent($("#txtKeyword").val()) +
            '&type=' + typepage + '&key=' + key;
        return url;
    }
    /*Create material: Upload file*/
    function removeFile() {
        $("#ServerImageName").val("");
        showFile("");
    }
    function showFile(fileName) {
        if (fileName != "") {
            $("#spanImageName").html(fileName);
            $("#hidFileDisplayName").html(fileName);
            $("#btnRemoveFile").show();
        }
        else {
            $("#spanImageName").html("No File");
            $("#hidFileDisplayName").html("");
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
                            'script': '/Library/GenericHandle/UploadFileHandler.ashx?Page=Material',
                            'fileExt': '<%=Constants.MATERIAL_PAGE_FILE_FORMAT_ALLOWED%>',
                            'cancelImg': '<%= Url.Content("/Scripts/uploadify/cancel.png") %>',
                            //'fileDesc': 'Image files (*.jpg; *.png; *.gif)',
                            'sizeLimit': parseInt('<%=(int)Constants.MATERIAL_PAGE_FILE_MAX_SIZE%>') * 1024 * 1024,
                            //'sizeLimit': '<%=(int)Constants.MENU_PAGE_IMAGE_MAX_SIZE * 1024 * 1024%>',
                            'width': 16,
                            'height': 16,
                            'wmode': 'transparent',
                            'removeCompleted': true,
                            'auto': true,
                            'buttonText': 'Choose File',
                            'hideButton': true,
                            'onComplete': function (event, ID, fileObj, response, data) {
                                var jsonData = $.parseJSON(response);
                                if (jsonData.isSuccess == 1) {
                                    CRM.summary("", "none", "");
                                    $("#ServerImageName").val(jsonData.fileName);
                                    showFile(fileObj.name);
                                    //$("#btnRemoveFile").show();
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
                            'onError': function (event, ID, fileObj, errorObj) {
                                if (errorObj.type == 'File Size') {
                                    CRM.summary(CRM.format(E0012, '<%=Constants.MATERIAL_PAGE_FILE_MAX_SIZE%>'), "block", "msgError");
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
                $(".blockOverlay").css("display", "");
                CRM.completed();
            }
        });
    };
    /*End Create material: Upload file*/
    jQuery(document).ready(function () {
        var typepage = '<%=Request.QueryString["type"].ToString() %>';
        switch (typepage) {
            case '2':
                $("#usertab2").addClass("tab-active");
                break;
            case '3':
                $("#usertab3").addClass("tab-active");
                break;
            default:
                $("#usertab1").addClass("tab-active");
                break;
        }

        CRM.onEnterKeyword();
        jQuery("#list").jqGrid({
            url: getFilterParams(),
            datatype: 'json',
            scroll: 0,
            mtype: 'GET',
            colNames: ['ID', 'Info'],
            colModel: [
                { name: 'ID', index: 'ID', hidden: true },
                { name: 'Info', index: 'Info', sortable: false}],
            pager: jQuery('#pager'),
            rowList: [5, 10, 25, 50, 100],
            //            sortname: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_COLUMN]%>',
            //            sortorder: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_SORT_ORDER]%>',
            //            rowNum: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_ROW_COUNT]%>',
            //            page: '<%= (string)ViewData[CommonDataKey.SR_CATEGORY_SEARCH_PAGE_INDEX]%>',
            sortname: 'ID',
            sortorder: 'asc',
            rowNum: '5',
            multiselect: { required: false, width: 24 },
            viewrecords: true,
            width: 1024, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });

        $('#btnAddNew').click(function () {
            var url = '<%=Url.Action("Create")%>?returnUrl=' + encodeURIComponent('<%=Request.RawUrl%>') + '&type=<%=Request["type"]%>';
            showPopup(url, "Add new Material", 700);
        });
        $("#btnDelete").click(function () {
            CRM.deleteList('#list', 'ID', '<%=Url.Action("Delete")%>');
        });
        $("#btnRefresh").click(function () {
            window.location = '<%=Url.Action("Refresh")%>';
        });
        $("#btnFilter").click(function () {
            var targetUrl = getFilterParams();
            $('#list').setGridParam({ page: 1, url: targetUrl }).trigger('reloadGrid');
        });
        $.each($("input[name='radMaterialType']"), function () {
            $(this).click(function () {
                window.location = '<%=Url.Action("SubList")%>' + "?type=" + $(this).val();
            });
        });


    });      
    
</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
<%=TrainingCenterPageInfo.ComName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
<%int nType = ConvertUtil.ConvertToInt(Request["type"]);
  string subListName = (string)ViewData[CommonDataKey.TRAINING_CENTER_SUBLIST_NAME];
%>

<%=CommonFunc.GetCurrentMenu(Url.Action("Index", "TrainingCenterAdmin"), false) + TrainingCenterPageInfo.FuncMaterial + CommonPageInfo.AppDetailSepChar +
             "<a href='/TrainingMaterial/MaterialList/"+nType+"'>" + (nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_PRO_COURSE ?
        TrainingCenterPageInfo.FuncChildPro : nType == Constants.TRAINING_CENTER_MATERIAL_SEARCH_TYPE_ENGLISH_COURSE ?
        TrainingCenterPageInfo.FuncChildEng : TrainingCenterPageInfo.FuncChildCat) + "</a>" + 
        (string.IsNullOrEmpty(subListName) ? "" : CommonPageInfo.AppDetailSepChar + HttpUtility.HtmlEncode(ViewData[CommonDataKey.TRAINING_CENTER_SUBLIST_NAME]))%>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
