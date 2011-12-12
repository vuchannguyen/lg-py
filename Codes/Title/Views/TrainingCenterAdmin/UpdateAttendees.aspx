<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UpdateAttendees</title>
    <%
        string tableId = "tblAttendees";
    %>
    <script src="../../Scripts/TrainingCenter.js" type="text/javascript"></script>
    <style type="text/css">
        #tblAttendees
        {
            width:100%;
            border: none;
            border-collapse:collapse;    
        }
        #tblAttendees th
        {
            background-color: #cccccc;
        }
        #tblAttendees td, #tblAttendees th
        {
            border: 1px solid #aaaaaa;
            padding:3px;
        }
        #tblAttendees td.lastCol, #tblAttendees th.lastCol
        {
            
            border-top: 0px;
            border-right:0px;
            border-bottom:0px;
            background-color: #EEEEEE !important;
        }
        #tblAttendees input.id
        {
            width:60px;
        }
        #tblAttendees input.name
        {
            width:97%;
            background-color: #EEEEEE !important;
            border: none !important;
        }
        #tblAttendees .result
        {
            background-image:none;
        }
        #tblAttendees input.result.score
        {
            width: 75px;
        }
        #tblAttendees select.result.passfail
        {
            width: 80px;
        }
        #tblAttendees input.result.comment
        {
            width: 150px;
        }
        #tblAttendees input.remark
        {
            width: 95%;
        }
        div.ac_results
        {
            width: auto !important;    
            overflow-y: scroll !important;
        }
        div.ac_results ul
        {
            overflow: visible !important;
        }
    </style>
    <script type="text/javascript">
        var $tableObj = $('#<%=tableId%>');
        var resultType = '<%=ViewData["ResultType"]%>';
        var rowTemplate =
            "<tr class='content'>" +
                "<td style='text-align:center'>{0}</td>" +
                "<td style='text-align:left'>{1}</td>" +
                "<td style='text-align:left'>{2}</td>" +
                "<td style='text-align:left'>{3}</td>" +
                "<td>{4}</td>" +
                "<td class='lastCol'><button onclick='removeAttendee($(this).parent().parent());' type='button' class='icon minus' ></button></td>" +
            "</tr>";
        function addAttendee(empId, empName, resultType, resultValue, remark) {
            var index = 0;
            if ($tableObj.find("input[name^='txtId_']").length > 0)
                index = parseInt($tableObj.find("input[name^='txtId_']:last").attr("name").split('_')[1]) + 1;
            var txtEmpID = "<input class='id' name='txtId_" + index + "' type='text' value='" + empId + "'/>";
            var txtEmpName = "<input class='name' readonly name='txtName_" + index + "' type='text' value='" + empName + "'/>";
            var txtRemark = "<input class='remark' name='txtRemark_" + index + "' type='text' value='" + remark + "'/>";
            var objResult = "";
            switch (resultType) {
                case '<%=Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_SCORE%>':
                    objResult = "<input class='result score' name='txtScore_" + index + "' type='text' value='" + resultValue + "'/>";
                    break;
                case '<%=Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_PASS_FAIL%>':
                    objResult =
                        "<select class='result passfail' name='slScore_" + index + "' >" +
                            "<option value=''>--Select--</option>" +
                            "<option value='Pass' " + (resultValue == "Pass" ? "selected" : "") + ">Pass</option>" +
                            "<option value='Fail' " + (resultValue == "Fail" ? "selected" : "") + ">Fail</option>" +
                        "</select>";
                    break;
                case '<%=Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_COMMENT%>':
                    objResult = "<input class='result comment' name='txtScore_" + index + "' type='text' value='" + resultValue + "'/>";
                    break;
                case '<%=Constants.TRAINING_CENTER_CLASS_RESULT_TYPE_NO_RESULT%>':
                    objResult = "";
                    break;
            }
            var newRow = $.format(rowTemplate, $tableObj.find("tr.content").length + 1, txtEmpID, txtEmpName, objResult, txtRemark);
            $tableObj.append(newRow);
            var $idInput = $tableObj.find("input[name='txtId_" + index + "']");
            $idInput.rules("add", 
                {   required: true,
                    remote: {
                        url: '<%=Url.Action("CheckEmployeeExists") %>',
                        type: "post",
                        data: {
                            empId: function () {
                                return $idInput.val();
                            },
                            arrId: function(){
                                var returnArr = "";
                                $.each($tableObj.find("input[name^='txtId_']"), function(){
                                    if($(this).attr("name") != $idInput.attr("name"))
                                        returnArr += $(this).val() + ",";
                                });
                                return returnArr;
                            }
                        }
                    }
                });
            $idInput.autocomplete(
                '/Library/GenericHandle/AutoCompleteHandler.ashx/?func=Employee&type=2&suffixId=1&idFirst=1', 
                { subField: "input[name='txtName_" + index + "']", employee: true
            });
        }
        function removeAttendee(row) {
            $(row).remove();
            $.each($tableObj.find("tr.content"), function (index, value) {
                $(this).find("td:first").html(index + 1);
            });
        }
        $(document).ready(function () {
//            $.validator.addMethod('notDuplicated', function (value, element, params) {
//                var isValid = true;
//                $.each($tableObj.find("input[name^='txtId_']"), function(){
//                    if($(this).attr("name") != $(element).attr("name") && $(this).val() == value){
//                        isValid = false;
//                        return;
//                    }
//                });
//                return isValid;
//            }, $.format(E0048, "Attendee"));

            $("#frmAttendee").validate({
                debug: false,
                errorElement: "span",
                errorPlacement: function (error, element) {
                    error.tooltip({
                        bodyHandler: function () {
                            return error.html();
                        }
                    });
                    error.insertAfter(element);
                }
            });

            $("#btnAddAttendee").click(function () {
                addAttendee("", "", resultType, "", "");
            });
//            addAttendee("", "", resultType, "", "");
//            addAttendee("", "", resultType, "", "");
            <%
            var list = ViewData.Model as List<sp_TC_GetListAttendeesOfClassResult>;
            string funcTempalte = "addAttendee('{0}', '{1}', '{2}', '{3}', '{4}');";
            foreach (var attendee in list)
            {
                Response.Write(string.Format(funcTempalte, attendee.ID, attendee.DisplayName, 
                    ViewData["ResultType"], attendee.Result, attendee.Remark));
            }
            %>
        });
    </script>
</head>
<body>
    
        <%using (Html.BeginForm("UpdateAttendees", "TrainingCenterAdmin", FormMethod.Post, new { @id="frmAttendee", @class="form" }))
          { %>
        <%=Html.Hidden("classId", RouteData.Values["id"]) %>
        <div style="text-align:center; padding-left:5px !important;" class="edit">
            <div style="text-align:left" >
                <b>Attendees</b>
                <button style="float:right;background-image: url('/Content/Images/Icons/add.png');padding-left: 25px;
                    padding-right: 5px;" type="button" id="btnAddAttendee" title="Add Attendee" class="button addnew">Add Attendee</button>
            </div>
            <div style="height:400px;width:100%; overflow-x: hidden; overflow-y: auto; margin:20px 0px 10px 0px;">
                <table id="tblAttendees">
                    <tr>
                        <th style='text-align:center;'>No</th>
                        <th style='width:100px'>ID</th>
                        <th style='width:200px'>Name</th>
                        <th>Result</th>
                        <th>Remark</th>
                        <th class='lastCol'></th>
                    </tr>
                </table>
            </div>
            <input class="save" type="submit" value="" alt="" />
            <input class="cancel" type="button" value="" alt="" onclick="CRM.closePopup();" />
            <%=Html.Hidden("page",Request["page"]) %>
        </div>
        <%} %>
</body>
</html>
