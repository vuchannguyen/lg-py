<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    //Automatic Set End Date Depends Start Date
    function SetEndDate(selectType) {
        jQuery.ajax({
            url: "/Employee/SetEndDate",
            type: "POST",
            datatype: "json",
            data: ({
                'startDateUI': $("#StartDate").val(),
                'contractType': selectType,
                'duration': $("#content_Duration").val()
            }),
            success: function (result) {
                if (result != 0 && result != false && $("#StartDate").valid()) {
                    $("#EndDate").val(result);
                }
            }
        });
    }
    $(document).ready(function () {
        
        $("#ContractType").change(function () {
            var selectValue = $("#ContractType").val();
            if (selectValue > 0) {
                jQuery.ajax({
                    url: "/Employee/CheckDurationOfContract",
                    type: "POST",
                    datatype: "json",
                    data: ({
                        'contractTypeId': selectValue
                    }),
                    success: function (result) {
                        if (result == false) {
                            $("#row_EndDate").attr("style", "display:none");
                            $("#EndDate").val("");
                            $("#EndDate").rules("remove");
                            $("#content_Duration").val(0);
                        }
                        else {
                            $("#row_EndDate").attr("style", "display:");
                            $("#EndDate").rules("add", { checkDate: true, compareDate: ["#contractForm input[name='StartDate']", "get", "End Date", "Start Date"] });
                            $("#content_Duration").val(result);
                            if ($("#StartDate").valid()) {
                                SetEndDate(selectValue);
                            }
                        }
                    }
                })
            }
        });
    });

    $(function () {
        $("#StartDate").change(function () {
            if ($("#StartDate").val() != "") {
                if ($("#StartDate").valid() && $("#ContractType").val() > 0) {
                    SetEndDate($("#ContractType").val())
                }
            }
        });
                $("#StartDate").datepicker({
                    onClose: function () {
                        $(this).valid();
                        if ($("#ContractType").val() > 0) {
                            SetEndDate($("#ContractType").val())
                        }
                    }
                });

        $("#EndDate").datepicker({
            onClose: function () { $(this).valid(); }
        });
    });
</script>
<%
    Contract contract = (Contract)ViewData.Model;
    if (ViewData.Model == null)
   {
       Response.Write(Html.Hidden("content_Duration"));
   }
   else
   {
       Response.Write(Html.Hidden("content_Duration", contract.ContractType1.Duration != null ?
           contract.ContractType1.Duration.ToString() : "0"));
   }

    Response.Write(Html.Hidden("EmployeeId", ViewData["EmployeeId"] as string));
    Response.Write(Html.Hidden("ContractedDate", ViewData["ContractedDate"] as string));
    Response.Write(Html.Hidden("LastEndDate", ViewData["LastEndDate"] as string));
    
    
%>
<table id="tbl" width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td class="label required" style="width: 180px">
            Contract Type <span>*</span>
        </td>
        <td>
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("ContractType", ViewData["ContractType"] as SelectList,
                       Constants.FIRST_ITEM, new { @style = "width:136px" }));
               }
               else
               {
                   Response.Write(Html.DropDownList("ContractType", null, Constants.FIRST_ITEM, new { @style = "width:136px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required" style="width: 180px">
            Contract Number
        </td>
        <td>
            <% 
                Response.Write(Html.TextBox("ContractNumber", null, new { @style = "width:136px" }));
            %>
        </td>
    </tr>
    <tr>
        <td class="label required" style="width: 180px">
            Start Date <span>*</span>
        </td>
        <td>
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("StartDate", "", new { @style = "width:130px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("StartDate", contract.StartDate.ToString(Constants.DATETIME_FORMAT),
                       new { @style = "width:130px" }));
               }
            %>
        </td>
    </tr>
    <% string styleCSS = "display:;width:180px";
       if (ViewData.Model != null)
           if (contract.ContractType1.Duration == null)
           {
               styleCSS = "display:none;width:180px";
           } 
           
    %>
    <tr id="row_EndDate" style="<%=styleCSS %>">
        <td class="label required">
            End Date <span>*</span>
        </td>
        <td>
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("EndDate", "", new { @style = "width:130px" }));
               }
               else
               {
                   if (contract.ContractType1.Duration != null)
                   {
                       Response.Write(Html.TextBox("EndDate", contract.EndDate.HasValue ?
                           contract.EndDate.Value.ToString(Constants.DATETIME_FORMAT) : "",
                           new { @style = "width:130px" }));
                   }
                   else
                   {
                       Response.Write(Html.TextBox("EndDate", "", new { @style = "width:130px" }));
                   }
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label " style="width: 180px">
            Comment
        </td>
        <td>
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Comment", "", new { @style = "width: 270px; height: 80px;", @maxlength = "500" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Comment", contract.Comment,
                       new { @style = "width: 270px; height: 80px;", @maxlength = "500" }));
               }
            %>
        </td>
        <% if (ViewData.Model != null)
                   { %>
                <td  valign="bottom">
                      <% int y = 0;
                         if (!string.IsNullOrEmpty(contract.ContractFile))
                       {
                           string[] array = contract.ContractFile.TrimEnd(Constants.FILE_CHAR_PREFIX).Split(Constants.FILE_CHAR_PREFIX);
                           y = array.Count();
                       }
                       else
                       {
                           y = 1;
                       }%><button  type="button"  onclick='return AddRowUpload(<%=y %>);' title='Add New Upload' class='icon plus'> </button> </td>
            <% } %>
    </tr>
   <% if (ViewData.Model == null)
           {%>
        <tr id="row_upload">
            <td class="label">
                Attachment
            </td>
            <td id="contentFile" class="input">
                <input type="file" name="file" />
            </td>
            <td valign="top">
                <button type="button" class="icon plus" title="Add New Upload" onclick="return AddRowUpload();">
                </button>
            </td>
        </tr>
        <%}
           else
           {
               Response.Write(Html.Hidden("hidDeleteFile"));
               Response.Write(CommonFunc.SplitFileNameForView(contract.ContractFile, Constants.CONTRACT_PATH, Constants.CONTRACT, contract.ContractId.ToString(), "hidDeleteFile"));%>                 
         <%  } %>
</table>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="form">
    <tr>
        <td colspan="2" align="center">
            <input type="submit" class="save" value="" alt="" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup();" />
        </td>
    </tr>
</table>
