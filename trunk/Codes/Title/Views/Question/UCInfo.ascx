<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript" src="/Scripts/jquery.jplayer.min.js"></script>
<script type="text/javascript" src="/Scripts/jquery.sound.js"></script>
<script type="text/javascript" src="/Scripts/CodeEditor/edit_area_full.js"></script>
<script type="text/javascript">    
    function addMoreAnswer(id, content, isCorrect) {
        var maxNumberAnswer = '<%=Constants.MAX_NUMBER_OF_ANSWERS%>';
        var currentNumberAnswer = $("#tblAnswer tr").length - 1;
        if (currentNumberAnswer != maxNumberAnswer) {
            $("#tblrowAnswer").find("input[name|='btnAdd']").attr("disabled", true);
            var maxNumberAnswer = '<%=Constants.MAX_NUMBER_OF_ANSWERS%>';
            var currentNumberAnswer = $("#tblAnswer tr").length;
            if (currentNumberAnswer - 1 < maxNumberAnswer) {
                var rowIndex = $("#tblAnswer tr:last").index() + 1;
                if (isCorrect)
                    setCorrectAnswerRowIndex(rowIndex);
                var isChecked = isCorrect ? " checked" : "";
                var tbl = $("#tblAnswer").append(
                "<tr>"
               + "<td align='center'></td>"
               + "<td> <input type='hidden' name='hidAnswerID' value='" + id + "'/>"
               + "<input tabIndex='" + rowIndex + "' type='text' name='txtAnswer' maxlength='" + '<%=Constants.ANSWER_CONTENT_MAX_LENGTH %>'
               + "' value='" + content + "' style='width:720px'/></td>"
               + "<td align='center' valign='middle'> <input type='radio' " + isChecked
               + " name='radAnswer' onclick='setCorrectAnswerRowIndex(" + rowIndex + ")' /></td>"
               + "<td align='right'  valign='middle'> <input type='button' name='btnRemoveRow' title='Remove' "
               + " class='icon answerremove' onclick='removeRow(" + rowIndex + ")'/>"
               + "<input type='button' name='btnMoveUp' class='icon moveup' title='Move up' onclick='moveUp("
               + rowIndex + ")' />"
               + "<input type='button' name='btnMoveDown' class='icon movedown' title='Move down' onclick='moveDown("
               + rowIndex + ")'/></td>"
               + "</tr>");
                reOrder();
            }
            $("#tblrowAnswer").find("input[name|='btnAdd']").attr("disabled", false);
        }
        else {
            CRM.message(CRM.format(E0029, "Number of answers", maxNumberAnswer), "block", "msgError");
        }
    }
    function setCorrectAnswerRowIndex(rowIndex) {
        $("#tblAnswer tr:first").find("input[name|='hidCorectAnswerRowIndex']").val(rowIndex);
    }
    function swapRowsContent(rowA_Index, rowB_Index) {
        if (isRadioButtonChecked(rowA_Index)) {
            setCorrectAnswerRowIndex(rowB_Index);
        }
        else if (isRadioButtonChecked(rowB_Index)) {
            setCorrectAnswerRowIndex(rowA_Index);
        }
        var rowA_obj = $("#tblAnswer tr:eq(" + rowA_Index + ")");
        var rowB_obj = $("#tblAnswer tr:eq(" + rowB_Index + ")");
        var txtAnswerA_Content = rowA_obj.find("input[name|='txtAnswer']");
        var radAnswerA_IsCorrect = rowA_obj.find("input[name|='radAnswer']");
        var hidAnswerA_ID = rowA_obj.find("input[name|='hidAnswerID']");
        var txtAnswerB_Content = rowB_obj.find("input[name|='txtAnswer']");
        var radAnswerB_IsCorrect = rowB_obj.find("input[name|='radAnswer']");
        var hidAnswerB_ID = rowB_obj.find("input[name|='hidAnswerID']");
        var answerA_ID = hidAnswerA_ID.val();
        var answerA_Content = txtAnswerA_Content.val();
        var answerA_isCorrect = radAnswerA_IsCorrect.attr("checked");
        var answerB_ID = hidAnswerB_ID.val();
        var answerB_Content = txtAnswerB_Content.val();
        var answerB_isCorrect = radAnswerB_IsCorrect.attr("checked");
        hidAnswerA_ID.val(answerB_ID);
        txtAnswerA_Content.val(answerB_Content);
        radAnswerA_IsCorrect.attr("checked", answerB_isCorrect);
        hidAnswerB_ID.val(answerA_ID);
        txtAnswerB_Content.val(answerA_Content);
        radAnswerB_IsCorrect.attr("checked", answerA_isCorrect);
    }
    function moveUp(rowIndex) {
        if (rowIndex != 1)
            swapRowsContent(rowIndex, rowIndex - 1);
    }
    function moveDown(rowIndex) {
        if (rowIndex != $("#tblAnswer tr").length - 1)
            swapRowsContent(rowIndex, rowIndex + 1);
    }
    function isRadioButtonChecked(rowIndex) {
        return $("#tblAnswer tr:eq(" + rowIndex + ")").find("input[name|='radAnswer']").attr("checked");
    }
    function removeRow(rowIndex) {
        var isInAnyExam = '<%=ViewData[CommonDataKey.IS_IN_ANY_EXAM] %>';
        if (isInAnyExam == "1") {
            CRM.message(CRM.format(E0006, "change number of answers", "this question"), "block", "msgError");
            return;
        }
        var currentNumberAnswer = $("#tblAnswer tr").length - 1;
        var minNumberAnswer = '<%=Constants.MIN_NUMBER_OF_ANSWERS%>';
        if (currentNumberAnswer != minNumberAnswer) {
            if (isRadioButtonChecked(rowIndex)) {
                setCorrectAnswerRowIndex(0);
            }
            var numberRow = $("#tblAnswer tr").length;
            var removeButton = $("#tblAnswer tr:eq(" + rowIndex + ")").find("input[name|='btnRemoveRow']");
            removeButton.attr("disabled", true);
            if (rowIndex < numberRow - 1) {
                for (i = rowIndex; i < numberRow - 1; i++) {
                    moveDown(i);
                }
            }
            $("#tblAnswer tr:last").remove();
            removeButton.attr("disabled", false); ;
            reOrder();
        }
        else {
            CRM.message(CRM.format(E0028, "Number of answers", minNumberAnswer), "block", "msgError");
        }
    }
    function reOrder() {
        $('#tblAnswer tr').each(function () {
            var order = $(this).index() + 64; //65: ASCII of "A"
            $(this).find("td:first").html(String.fromCharCode(order));
            switch ($(this).index()) {
                case 1:
                    $(this).find("input[name='btnMoveUp']").css("visibility", "hidden");
                    $(this).find("input[name='btnMoveDown']").css("visibility", "visible");
                    break;
                case $('#tblAnswer tr').length - 1:
                    $(this).find("input[name='btnMoveUp']").css("visibility", "visible");
                    $(this).find("input[name='btnMoveDown']").css("visibility", "hidden");
                    break;
                default:
                    $(this).find("input[name='btnMoveUp']").css("visibility", "visible");
                    $(this).find("input[name='btnMoveDown']").css("visibility", "visible");
                    break;
            }
        });
        if ($('#tblAnswer tr').length <= 2) {
            $('#tblAnswer tr:last').find("input[name='btnMoveUp']").css("visibility", "hidden");
            $('#tblAnswer tr:last').find("input[name='btnMoveDown']").css("visibility", "hidden");
        }
    }

    function getErrorMessage() {
        var sectionID = $("#SectionName").val();
        if (sectionID == '<%=Constants.LOT_LISTENING_TOPIC_ID %>'
            || sectionID == '<%=Constants.LOT_COMPREHENSION_SKILL_ID %>') {
            var minNumberOfQuestion = 0;
            if (sectionID == '<%=Constants.LOT_LISTENING_TOPIC_ID %>') {
                minNumberOfQuestion = '<%=Constants.MIN_NUMBER_OF_QUESTIONS_IN_TOPIC%>';
                //Topic name is empty
                if (jQuery.trim($("#TopicName").val()) == "")
                    return CRM.format(E0001, "Topic name");
                //No sound file is uploaded                
                if ($("#fullFileName").val() == "")
                    return CRM.format(E0011);
            }
            else {
                minNumberOfQuestion = '<%=Constants.MIN_NUMBER_OF_QUESTIONS_IN_PARAGRAPH%>';
                var text = "";
                var oEditor = FCKeditorAPI.GetInstance('ParagraphContent');
                if (oEditor) { text = oEditor.GetXHTML(true); }
                //Paragraph's content is empty
                if (jQuery.trim(strip_tags(text)) == "")
                    return CRM.format(E0001, "Paragraph's content");
            }
            //There's no question is added
            if ($("#questionlist tr").length - 1 < minNumberOfQuestion)
                return CRM.format(E0039, minNumberOfQuestion);
        }
        else {
            //Question's content is empty            
            if (sectionID == "<%=Constants.LOT_PROGRAMMING_SKILL_ID %>") {
                if (jQuery.trim(editAreaLoader.getValue("QuestionContentProgramming")) == "") {
                    return CRM.format(E0001, "Question's content");
                }
                if($('#<%=CommonDataKey.PROGRAMMING_SKILL_TYPE%>').val() == '')
                    return CRM.format(E0001, "Type");
            }
            //else if (jQuery.trim($("#QuestionContent").val()) == "")
            else {
                var text = "";
                var oEditor = FCKeditorAPI.GetInstance('QuestionContent');
                if (oEditor) { text = oEditor.GetXHTML(true); }
                if (jQuery.trim(strip_tags(text)) == "")
                    return CRM.format(E0001, "Question's content");
                else if (sectionID != "<%=Constants.LOT_WRITING_SKILL_ID %>" && sectionID != "<%=Constants.LOT_PROGRAMMING_SKILL_ID %>") {
                    //Answer's content is empty
                    if (isAnyAnswerContentBlank())
                        return CRM.format(E0001, "Answer's content");
                    //No answer is checked as correct
                    else if (!isAnAnswerChecked())
                        return CRM.format(E0038);
                }
            }
        }
        return '';
    }

    function showMessageIfError() {
        var msg = getErrorMessage();
        if (msg != "") {
            CRM.message(msg, "block", "msgError");
            return false;
        }
        return true;
    }
    function isAnyAnswerContentBlank() {
        for (i = 1; i < $("#tblAnswer tr").length; i++) {
            var currentRow = $("#tblAnswer tr:eq(" + i + ")");
            if (jQuery.trim(currentRow.find("input[name|='txtAnswer']").val()) == "")
                return true;
        }
        return false;
    }
    function isAnAnswerChecked() {
        for (i = 1; i < $("#tblAnswer tr").length; i++) {
            var currentRow = $("#tblAnswer tr:eq(" + i + ")");
            if (currentRow.find("input[name|='radAnswer']").attr("checked"))
                return true;
        }
        return false;
    }
    function hasValidAnswers() {
        var isChecked = false;
        var isAllNotNull = true;
        for (i = 1; i < $("#tblAnswer tr").length; i++) {
            var currentRow = $("#tblAnswer tr:eq(" + i + ")");
            if (isAllNotNull && jQuery.trim(currentRow.find("input[name|='txtAnswer']").val()) == "")
                isAllNotNull = false;
            if (!isChecked && currentRow.find("input[name|='radAnswer']").attr("checked"))
                isChecked = true;
        }
        return (isChecked && isAllNotNull) ? true : false;
    }
    function setDisplay(className, displayType) {
        $("#mainTable").find("tr[class|='" + className + "']").each(function () {
            $(this).css("display", displayType);
        });
    }
    function changeQuestionType() {
        var sectionID = jQuery.trim($("#SectionName option:selected").val());
        $('#questionlist').trigger('reloadGrid');
        var displayType = "table-row";
        //reset textarea
        $("#QuestionContent___Frame").css("display", "");
        //$("#QuestionContent").css("display", "");
        $("#frame_QuestionContent").remove();
        if ($.browser.msie)
            displayType = "inline-block";
        if (sectionID == '<%=Constants.LOT_WRITING_SKILL_ID%>') {
            setDisplay("info_questioncontent", displayType);
            setDisplay("programming_questioncontent", "none");
            setDisplay("info_programmingtype", "none");
            setDisplay("info_paragraphcontent", "none");
            setDisplay("info_answer", "none");
            setDisplay("info_repeattimes", "none");
            setDisplay("info_mp3file", "none");
            setDisplay("info_topicname", "none");
            setDisplay("info_questionlist", "none");
        }
        else if (sectionID == '<%=Constants.LOT_PROGRAMMING_SKILL_ID%>') {
            setDisplay("programming_questioncontent", displayType);
            setDisplay("info_programmingtype", displayType);
            editAreaLoader.init({
                id: 'QuestionContentProgramming'	// id of the textarea to transform		
                    , start_highlight: true	// if start with highlight
                    , allow_resize: "no"
                    , allow_toggle: false
                    , word_wrap: true
                    , language: "en"
                    , syntax: "csharp"
                    , min_width: 900
                    , min_height: 230
                    , toolbar: "search, |, undo, redo, |, syntax_selection"
                    , syntax_selection_allow: "css,html,js,php,python,vb,xml,c,csharp,cpp,sql,basic,pas,brainfuck"
                    , show_line_colors: true
            });
            $("#QuestionContent___Frame").css("display", "none");
            setDisplay("info_questioncontent", "none");
            setDisplay("info_paragraphcontent", "none");
            setDisplay("info_answer", "none");
            setDisplay("info_repeattimes", "none");
            setDisplay("info_mp3file", "none");
            setDisplay("info_topicname", "none");
            setDisplay("info_questionlist", "none");
        }
        else if (sectionID == '<%=Constants.LOT_LISTENING_TOPIC_ID%>') {
            setDisplay("info_questioncontent", "none");
            setDisplay("programming_questioncontent", "none");
            setDisplay("info_programmingtype", "none");
            setDisplay("info_paragraphcontent", "none");
            setDisplay("info_answer", "none");
            setDisplay("info_repeattimes", displayType);
            setDisplay("info_mp3file", displayType);
            setDisplay("info_topicname", displayType);
            setDisplay("info_questionlist", displayType);
        }
        else if (sectionID == '<%=Constants.LOT_COMPREHENSION_SKILL_ID%>') {
            setDisplay("info_questioncontent", "none");
            setDisplay("programming_questioncontent", "none");
            setDisplay("info_programmingtype", "none");
            setDisplay("info_paragraphcontent", displayType);
            setDisplay("info_answer", "none");
            setDisplay("info_repeattimes", "none");
            setDisplay("info_mp3file", "none");
            setDisplay("info_topicname", "none");
            setDisplay("info_questionlist", displayType);
        }
        else {
            setDisplay("info_questioncontent", displayType);
            setDisplay("programming_questioncontent", "none");
            setDisplay("info_programmingtype", "none");
            setDisplay("info_paragraphcontent", "none");
            setDisplay("info_answer", displayType);
            setDisplay("info_repeattimes", "none");
            setDisplay("info_mp3file", "none");
            setDisplay("info_topicname", "none");
            setDisplay("info_questionlist", "none");
        }
    }
    function removeSelectedRows(gridName) {
        var arrRowID = getJqgridSelectedRows(gridName).split(",");
        if (arrRowID != "") {
            for (i = arrRowID.length - 1; i >= 0; i--) {
                jQuery(gridName).delRowData(arrRowID[i]);
            }
        }
    }
    function getAddedQuestionIDs() {
        var addedQuestionIDs = "";
        $.each($("#questionlist tr"), function () {
            if ($(this).attr("id")) {
                var id = $(this).find("td[aria-describedby|='questionlist_QuestionID']").attr("title");
                addedQuestionIDs += id + ",";
            }
        });
        return addedQuestionIDs;
    }
    function deleteFile() {
        var fileName = $("#fullFileName").val();
        var requestParams = "fileName=" + fileName;
        CRM.ajax("/Question/DeleteSoundFile?" + requestParams);
        $("#filename").html('<%=Constants.NO_FILE_ERROR%>');
        $("#fullFileName").val("");
        $("#btnPlay").css("display", "none");
        $("#btnStop").css("display", "none");
        $("#btnRemoveFile").css("display", "none");
        $("#btnUpload").css("display", '<%=Constants.CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE%>');
        stopSound("div_jp_Media");
        CRM.closePopup();
    }
    jQuery(document).ready(function () {
        var submitflag = true;
        $("#SectionName").change(function () {
            var topicID = '<%=((SelectList)ViewData[CommonDataKey.SECTION_NAME]).SelectedValue %>';
            var isInAnyExam = '<%=ViewData[CommonDataKey.IS_IN_ANY_EXAM] %>';
            var isAssigned = '<%=ViewData[CommonDataKey.IS_ASSIGNED] %>';
            if (isInAnyExam == "1" || isAssigned == "1") {
                CRM.message(CRM.format(E0006, "change section", "this question"), "block", "msgError");
                $(this).val(topicID);
            }
            changeQuestionType();
        });
        changeQuestionType();
        jQuery("#questionlist").jqGrid({
            url: '/Question/GetListJQGrid_Edit?id=' + $("#ID").val()
                + '&sectionID=' + $("#SectionName").val(),
            datatype: 'json',
            mtype: 'GET',
            colNames: ['Question ID', 'Question Order', 'Question', 'Action'],
            colModel: [
                  { name: 'QuestionID', index: 'ID', align: "center", hidden: true },
                  { name: 'QuestionOrder', index: 'QuestionOrder', align: "center", hidden: true },
                  { name: 'Question', index: 'QuestionContent', align: "justify", width: 876, sortable: false },
                  { name: 'Action', index: 'Action', editable: false, width: 100, align: 'center', sortable: false}],
            sortname: 'QuestionID',
            sortorder: "asc",
            multiselect: { required: false, width: 24 },
            viewrecords: true,
            width: 1000, height: "auto",
            imgpath: '/scripts/grid/themes/basic/images',
            loadui: 'block'
        });
        //constructor options for jplayer
        $('#div_jp_Media').jPlayer({
            swfPath: '/Scripts',
            solution: $.browser.msie ? 'html, flash' : 'flash,html',
            supplied: '<%=Constants.SOUND_FILE_EXT_ALLOW%>',
            preload: 'metadata',
            volume: 1.0,
            muted: false,
            errorAlerts: false,
            warningAlerts: false,
            ended: function () { // The $.jPlayer.event.ended event
                $("#btnStop").css("display", "none");
                $("#btnPlay").css("display", "");
            }
        });
        $("#btnAddNew").click(function () {
            var isInAnyExam = '<%=ViewData[CommonDataKey.IS_IN_ANY_EXAM] %>';
            if (isInAnyExam == "1") {
                CRM.message(CRM.format(E0006, "change number of questions", "this topic"), "block", "msgError");
                return;
            }
            var ids = getAddedQuestionIDs();
            var sectionID = $("#SectionName").val();
            var targetUrl = "/Question/AssignQuestion?ids="
                + ids + "&sectionID=" + sectionID + "&id=" + '<%=Request["id"]%>';
            CRM.popup(targetUrl, "Assign Question", 800);
        });
        $("#btnDelete").click(function () {
            var isInAnyExam = '<%=ViewData[CommonDataKey.IS_IN_ANY_EXAM] %>';
            if (isInAnyExam == "1") {
                CRM.message(CRM.format(E0006, "change number of questions", "this topic"), "block", "msgError");
                return;
            }
            $(this).attr("disabled", true);
            removeSelectedRows("#questionlist");
            $(this).attr("disabled", false);
        });
        $("#btnUpload").click(function () {
            CRM.popup("/Question/UploadSoundFile", "Upload sound file", 500);
        });
        $("#btnRemoveFile").click(function () {
            CRM.msgConfirmBox("Are you sure you want to delete ?", 500, "deleteFile()");
        });
        $("#btnPlay").click(function () {
            try {
                playSound("div_jp_Media", $("#fullFileName").val());
                $("#btnPlay").css("display", "none");
                $("#btnStop").css("display", "");
            }
            catch (e) {
                CRM.summary('<%=Constants.SOUND_FILE_NOT_EXIST_MESSAGE%>', "block", "msgError");
            }
        });
        $("#btnStop").click(function () {
            stopSound("div_jp_Media");
            $(this).css("display", "none");
            $("#btnPlay").css("display", "");
        });
        $("#btnCancel").click(function () {
            var fileName = $("#fullFileName").val();
            var requestParams = "fileName=" + fileName + "&folderUrl=" + '<%=Constants.UPLOAD_TEMP_PATH %>';
            CRM.ajax("/Question/DeleteSoundFile?" + requestParams);
            window.location = "/Question";
        });

        $("#btnCreate").click(function () {             
            if (submitflag == true) {
                var msg = getErrorMessage();
                if (msg != "") {
                    CRM.message(msg, "block", "msgError");
                    submitflag = true;
                    return false;
                }
                else {
                    submitflag = false;
                    return true;
                }
            }
            else {
                return false;
            }
        });

        $("#btnAddMoreAnswer").click(function () {
            var isInAnyExam = '<%=ViewData[CommonDataKey.IS_IN_ANY_EXAM] %>';
            if (isInAnyExam == "1") {
                CRM.message(CRM.format(E0006, "change number of answers", "this question"), "block", "msgError");
                return;
            }
            addMoreAnswer(0, '', false);
        });
        $("#cactionbutton").removeClass("element.style");
    })

    function strip_tags(tags) {
        var stripped = tags.replace(/(<.*?>)/ig, "");
        return stripped;
    }

</script>
<style type="text/css">
    .ui-jqgrid tr.jqgrow td
    {
        white-space: normal;
    }
</style>
<div id="list" style="width: 1024px">
    <%=TempData[CommonDataKey.TEMP_SYSTEM_MESSAGE]%>
    <table id="mainTable" class="edit" width="100%">
        <tr>
            <td class="label required">
                Section
            </td>
            <td colspan="2" class="input" style="width: 914px">
                <%  
                    Type modelType = ViewData.Model == null ? typeof(Nullable) : ViewData.Model.GetType();
                    if (ViewData.Model == null)
                    {
                        Response.Write(Html.Hidden("ID", "0"));
                        Response.Write(Html.DropDownList("SectionName"));
                    }
                    else
                    {
                        Response.Write(Html.Hidden("ID"));
                        Response.Write(Html.DropDownList("SectionName"));
                        Response.Write(Html.Hidden("UpdateDate"));
                    } 
                %>
            </td>
        </tr>
        <tr class="info_programmingtype" style="display: none">
            <td class="label required">
                Type<span>*</span>
            </td>
            <td colspan="2" class="input" style="width: 914px">
                <%  
                    if (ViewData[CommonDataKey.PROGRAMMING_SKILL_TYPE]!=null)
                        Response.Write(Html.DropDownList(CommonDataKey.PROGRAMMING_SKILL_TYPE, null, Constants.LOT_PROGRAMMING_TYPE_LABEL, new { @style = "width:120px" }));
                %>
            </td>
        </tr>
        <%
            if (ViewData.Model == null || modelType.Equals(typeof(LOT_Question)))
            {   
        %>
        <tr class="info_questioncontent">
            <td class="label required" style="vertical-align: top;">
                Question<span>*</span>
            </td>
            <td colspan="2" class="input" runat="server">
                <%                                                 
                    Response.Write(Html.FCKEditor("QuestionContent", (ViewData[CommonDataKey.QUESTION_CONTENT] == null ? string.Empty : ViewData[CommonDataKey.QUESTION_CONTENT].ToString()), 900, 200, "BasicOnlineTest"));                    
                %>
            </td>
        </tr>
        <tr class="programming_questioncontent" style="display: none">
            <td class="label required" style="vertical-align: top;">
                Question<span>*</span>
            </td>
            <td colspan="2" class="input" runat="server">
                <%                                        
                    Response.Write(Html.TextArea("QuestionContentProgramming", (ViewData[CommonDataKey.QUESTION_CONTENT_PROGRAMMING] == null ? string.Empty : ViewData[CommonDataKey.QUESTION_CONTENT_PROGRAMMING].ToString()), new {@cols=175, @rows=17}));                   
                %>
            </td>
        </tr>
        <%
            }
            if (ViewData.Model == null || modelType.Equals(typeof(LOT_ComprehensionParagraph)))
            {   
        %>
        <tr class="info_paragraphcontent">
            <td class="label required" style="vertical-align: top;">
                Paragraph<span>*</span>
            </td>
            <td colspan="2" class="input">
                <%  
                    Response.Write(Html.FCKEditor("ParagraphContent", (ViewData[CommonDataKey.QUESTION_CONTENT] == null ? string.Empty : ViewData[CommonDataKey.QUESTION_CONTENT].ToString()), 900, 200, "BasicOnlineTest"));
                //Response.Write(Html.TextArea("ParagraphContent"));
                %>
            </td>
        </tr>
        <%
            }
            if (modelType.Equals(typeof(LOT_Question)) || ViewData.Model == null)
            {   
        %>
        <tr class="info_answer">
            <td valign="top" class="label required">
                Answers
            </td>
            <td align="right" colspan="2" class="input">
                <input type="button" name="btnAdd" id="btnAddMoreAnswer" class='icon add' title="Add more answer" />
            </td>
        </tr>
        <tr class="info_answer">
            <td>
            </td>
            <td colspan="2" class="input">
                <table id="tblAnswer" border="0" width="100%">
                    <tr>
                        <th style="width: 2%" align="center">
                            #
                            <input type="hidden" name="hidCorectAnswerRowIndex" value="0" />
                        </th>
                        <th>
                            Answer
                        </th>
                        <th style="width: 7%" align="center">
                            Is Correct
                        </th>
                        <th style="width: 7%" align="center">
                            Action
                        </th>
                    </tr>
                </table>
                <%  
                    Response.Write("<script>");
                    if (ViewData.Model == null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Response.Write("addMoreAnswer(0,'',false);");
                        }
                    }
                    else
                    {
                        foreach (LOT_Answer answer in (List<LOT_Answer>)ViewData[CommonDataKey.ANSWER_ARR])
                        {
                            Response.Write("addMoreAnswer(" + answer.ID + ",'"
                                + HttpUtility.HtmlEncode(answer.AnswerContent)
                                + "'," + answer.IsCorrect.ToString().ToLower() + ");");
                        }
                    }
                    Response.Write("</script>");
                %>
            </td>
        </tr>
        <%
            }
            if (modelType.Equals(typeof(LOT_ListeningTopic)) || ViewData.Model == null)
            {
        %>
        <tr class="info_topicname">
            <td class="label required">
                Topic Name<span>*</span>
            </td>
            <td colspan="2" class="input">
                <%  
                    if (ViewData.Model == null)
                    {
                        Response.Write(Html.TextBox("TopicName", "", new
                        {
                            @style = "width:350px",
                            @maxlength = Constants.TOPIC_NAME_MAX_LENGTH
                        }));
                    }
                    else
                    {
                        Response.Write(Html.TextBox("TopicName", ((LOT_ListeningTopic)ViewData.Model).TopicName,
                            new { @style = "width:350px", @maxlength = Constants.TOPIC_NAME_MAX_LENGTH }));
                    }
                %>
            </td>
        </tr>
        <tr class="info_mp3file">
            <td class="label required">
                Mp3 File<span>*</span>
            </td>
            <td colspan="2" class="input">
                <div id="div_jp_Media">
                </div>
                <%  
                    string uploadDisplay = Constants.CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE;
                    string playDisplay = "none";
                    string removeDisplay = "none";
                    string filename = Constants.NO_FILE_ERROR;
                    string fullFileName = "";

                    if (ViewData.Model != null &&
                        System.IO.File.Exists(Server.MapPath("~" + Constants.SOUND_FOLDER)
                            + ((LOT_ListeningTopic)ViewData.Model).FileName))
                    {
                        fullFileName = Constants.SOUND_FOLDER + ((LOT_ListeningTopic)ViewData.Model).FileName;
                        filename = fullFileName.Split('/').Last().Substring(Constants.UNIQUEID_STRING_FORMAT.Length + 1);
                        uploadDisplay = "none";
                        playDisplay = Constants.CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE;
                        removeDisplay = Constants.CONST_JPLAYER_BUTTON_DISPLAY_VISIBLE;
                    }
                %>
                <input type='hidden' value='<%=fullFileName%>' id='fullFileName' name='fullFileName' />
                <div id='filename' style='width: 300px; display: inline'>
                    <%=filename%></div>
                <input type='button' id='btnUpload' style='display: <%=uploadDisplay%>' class='icon upload'
                    title='Upload sound file' />
                <span id='btnPlay' class='picon play' style='display: <%=playDisplay%>' title='Play'>
                </span><span id='btnStop' class='picon stop' title='Stop' style='display: none'>
                </span>
                <input type='button' id='btnRemoveFile' style='display: <%=removeDisplay%>; position: relative;
                    top: 4px;' class='icon answerremove' title='Remove' />
            </td>
        </tr>
        <tr class="info_repeattimes">
            <td class="label required">
                Repeat Time(s)
            </td>
            <td colspan="2" class="input">
                <%  
                    if (ViewData.Model == null)
                    {
                        Response.Write(Html.DropDownList("RepeatTimes"));
                    }
                    else
                    {
                        Response.Write(Html.DropDownList("RepeatTimes"));
                    }
                %>
            </td>
        </tr>
        <%
            }
        if (ViewData.Model == null
            || modelType.Equals(typeof(LOT_ListeningTopic))
            || modelType.Equals(typeof(LOT_ComprehensionParagraph)))
        {
        %>
        <tr class="info_questionlist">
            <td class="label required">
                Question list
            </td>
            <td colspan="2" align="right" id="cactionbutton">
                <button type="button" id="btnDelete" title="Delete" class="button delete" style="margin-right: 5px;">
                    Delete</button>
                <button type="button" id="btnAddNew" title="Add New" class="button addnew">
                    Add new</button>
            </td>
        </tr>
        <tr class="info_questionlist">
            <td colspan="3">
                <div class="clist">
                    <table id="questionlist" class="scroll">
                    </table>
                </div>
            </td>
        </tr>
        <%
            } 
        %>
        <tr>
            <td align="center" colspan="3">
                <input type="submit" class="save" id="btnCreate" value="" title="Save" />
                <input type="button" class="cancel" value="" id="btnCancel" title="Cancel" />
            </td>
        </tr>
    </table>
</div>
