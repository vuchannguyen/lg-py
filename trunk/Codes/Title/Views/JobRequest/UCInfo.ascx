<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<link href="/Scripts/AutoComplete/autoSuggest.css" rel="stylesheet" type="text/css" />
<style type="text/css">
     .ac_results
    {
        width:220px !important;
    }
</style>
<script type="text/javascript">
    var i =0;
    jQuery(document).ready(function () {
        $("#fieldCC").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?Page=WorkFlow&workflowID=<%= Constants.WORK_FLOW_JOB_REQUEST%>', { max: 50, highlightItem: true, multiple: true, multipleSeparator: ";", faceBook: true, hidField: "#CCList", employee: true });
        $("#as-selections").css("width", "398px");
        $(function () {
            //$("#RequestDate").datepicker();
            $("#ExpectedStartDate").datepicker({ onClose: function () { $(this).valid(); } });
            $("#RequestDate").datepicker({
                onClose: function () { $(this).valid(); }
            });
        });
        $("#DepartmentName").change(function () {
            $("#DepartmentId").html("");
            $("#PositionFrom").html("");
            $("#PositionTo").html("");
            $("#PositionFrom").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
            $("#PositionTo").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
            var department = $("#DepartmentName").val();
            $("#DepartmentId").append($("<option value=''><%= Constants.FIRST_ITEM_SUB_DEPARTMENT%></option>"));
            if (department != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=Department', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#PositionFrom").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + department + '&Page=SubDepartment', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#DepartmentId").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
        });

        $("#PositionFrom").change(function () {
            $("#PositionTo").html("");
            var positionId = $("#PositionFrom").val();
            if (positionId != 0) {
                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + positionId + '&Page=JobRequest', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#PositionTo").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }
                    });
                });
            }
            else {
                $("#PositionTo").append($("<option value=''><%= Constants.FIRST_ITEM_JOBTITLE %></option>"));
            }
        });

        $("#WFResolutionID").change(function () {
            var firstItem = '<%= ViewData["FirstChoiceStatus"]%>';
            $("#WFStatusID").html("");
            var resolutionId = $("#WFResolutionID").val();
            if (firstItem != '' && resolutionId == 0) {
                $("#WFStatusID").append($("<option value=''>" + firstItem + "</option>"));
            }
            if (resolutionId != 0) {

                $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Status', function (item) {
                    $.each(item, function () {
                        if (this['ID'] != undefined) {
                            $("#WFStatusID").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                        }

                    });
                });
            }

            //$("#assignTr").attr("style", "display:none"); //set default for assign list just dislay when have data
            firstItem = '<%= ViewData["FirstChoiceAssign"]%>';
            $("#Assign").html("");
            if (firstItem != '' && resolutionId == 0) {
                $("#Assign").append($("<option value=''>" + firstItem + "</option>"));
                $("#assignTr").attr("style", "display:");
            }
            if (resolutionId == '<%=Constants.RESOLUTION_CANCEL_ID %>') {
                $("#assignTr").css("display", "none");
            }
            else {
                if (resolutionId != 0) {
                    $.getJSON('/Library/GenericHandle/DropdownListHandler.ashx?ID=' + resolutionId + '&Page=Assign', function (item) {
                        $("#assignTr").css("display", "");
                        $.each(item, function () {
                            if (this['ID'] != undefined) {
                                $("#Assign").append($("<option value='" + this['ID'] + "'>" + this['Name'] + "</option>"));
                            }
                        });
                    });
                }
            }
        });

        if ($("#divPositionTo").css("display") != "none") {
            $("#lblFrom").html("From <span>*</span>");
        }
        else {
            $("#lblFrom").html("Job Title <span>*</span>");
        }

        $('#rdOne').click(function () {
            $("#divPositionTo").css("display", "none");
            $("#lblFrom").html("Job Title <span>*</span>");
        });

        $('#rdMany').click(function () {
            $("#divPositionTo").css("display", "");
            $("#lblFrom").html("From <span>*</span>");
        });
        //==>Updateby Tuan.minh,nguyen For vaildate form
        $("#jobRequestForm").validate({
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
                DepartmentId: {
                    required: true
                },
                RequestDate: {
                    required: true,
                    checkDate: true
                },
                ExpectedStartDate: {
                    required: true,
                    checkDate: true,
                    compareDate: ["#jobRequestForm input[name='RequestDate']", "get", "Expected Start Date", "Request Date"]
                },
                PositionFrom: {
                    required: true
                },
                Justification: {
                    maxlength: 250
                },
                RequestTypeId: { required: true },
                Quantity: {
                    number: true,
                    min: 1
                }
            },
            submitHandler: function (form) {
                if (i == 0) {
                    form.submit();
                    i++;
                }
            }
        });
        //<==end
    });    
</script>
<table border="0" cellpadding="0" width="100%" cellspacing="0" class="edit">
<% if (ViewData.Model != null)
   {
%>
    <tr>
        <td class="label" style="width: 120px;">
            Req#
        </td>
        <td class="input">
            <%
                Response.Write("<span class='bold red'>" + Constants.JOB_REQUEST_PREFIX + ((JobRequest)ViewData.Model).ID + "</span>");
            %>
        </td>
    </tr>
<%
    }  
%>
    <tr>
        <td class="label" style="width:120px">
            Requestor
        </td>
        <td class="input">
            <%
                var principal = HttpContext.Current.User as AuthenticationProjectPrincipal;
                Response.Write("<span class='bold'>" + principal.UserData.UserName + "</span>");
                if (Request.UrlReferrer != null)
                {
                    Response.Write(Html.Hidden(CommonDataKey.RETURN_URL, Request.UrlReferrer.AbsolutePath));
                }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Request Type <span>*</span>
        </td>
        <td class="input">
            <% Response.Write(Html.DropDownList("RequestTypeId", null, Constants.JOB_REQUEST_REQUEST_FIRST_ITEM, new { @style = "width:170px" })); %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Deparment
        </td>
        <td class="input">
            <% Response.Write(Html.DropDownList("DepartmentName", null, Constants.FIRST_ITEM_DEPARTMENT, new { @style = "width:170px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Sub Deparment <span>*</span>
        </td>
        <td class="input">
            <% Response.Write(Html.DropDownList("DepartmentId", null, Constants.FIRST_ITEM_SUB_DEPARTMENT, new { @style = "width:170px" }));%>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Request Date <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("RequestDate", DateTime.Now.ToString(Constants.DATETIME_FORMAT), new { @maxlength = "10", @style = "width:160px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("RequestDate", ((JobRequest)ViewData.Model).RequestDate.ToString(Constants.DATETIME_FORMAT), new { @maxlength = "10", @style = "width:160px" }));
               }  
            %>
        </td>
    </tr>
    <tr>
        <td class="label required" style="width: 145px;">
            Expected Start Date <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("ExpectedStartDate", "", new { @maxlength = "10", @style = "width:160px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("ExpectedStartDate", ((JobRequest)ViewData.Model).ExpectedStartDate.HasValue ? ((JobRequest)ViewData.Model).ExpectedStartDate.Value.ToString(Constants.DATETIME_FORMAT) : string.Empty, new { @maxlength = "10", @style = "width:160px" }));
               }  
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Position <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write("<label for=rdOne>" + Html.RadioButton("position", "One", true, new { ID = "rdOne" }) + " One</label> ");
                   Response.Write("<label for=rdMany>" + Html.RadioButton("position", "Many", false, new { ID = "rdMany" }) + " Many</label>");
               }
               else
               {
                   Response.Write("<label for=rdOne>" + Html.RadioButton("position", "One", ((JobRequest)ViewData.Model).PositionTo.HasValue ? false : true, new { ID = "rdOne" }) + " One</label> ");
                   Response.Write("<label for=rdMany>" + Html.RadioButton("position", "Many", ((JobRequest)ViewData.Model).PositionTo.HasValue ? true : false, new { ID = "rdMany" }) + " Many</label>");
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
           <div id="lblFrom">Job Title <span>*</span></div>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("PositionFrom", null, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:165px" }));
               }
               else
               {
                   Response.Write(Html.DropDownList("PositionFrom", null, new { @style = "width:165px" }));
               }
            %>
        </td>
      
    </tr>
     <% if (ViewData.Model == null)
           {

               Response.Write("<tr id='divPositionTo' style='display:none'><td class='label'>To </td> <td class='input'>" + Html.DropDownList("PositionTo", null, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:165px" }) + "</td></tr>");
           }
           else
           {
               if (((JobRequest)ViewData.Model).PositionTo.HasValue)
               {
                   Response.Write("<tr id='divPositionTo' ><td class='label'>To </td> <td class='input'>" + Html.DropDownList("PositionTo", null, new { @style = "width:165px" }) + "</td></tr>");
                   
               }
               else
               {
                   Response.Write("<tr id='divPositionTo' style='display:none'><td class='label'>To </td> <td class='input'>" + Html.DropDownList("PositionTo", null, Constants.FIRST_ITEM_JOBTITLE, new { @style = "width:165px" }) + "</td></tr>");
                   
               }
           }
               
        %>
    <tr>
        <td class="label required">
            Quantity<span>*</span>
        </td>
        <td class="input">
            <%=Html.TextBox("Quantity", ViewData.Model==null ? "1" : null, new { @size=3, @maxlength=3 }) %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Salary Suggestion
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("SalarySuggestion", "", new { @maxlength = "50", @style = "width:160px" }));
               }
               else
               {
                   string salary = ((JobRequest)ViewData.Model).SalarySuggestion;
                   Response.Write(Html.TextBox("SalarySuggestion", !string.IsNullOrEmpty(salary) ? EncryptUtil.Decrypt(salary) : "" , new { @maxlength = "50" }));
                   Response.Write(Html.Hidden("AssignID", ((JobRequest)ViewData.Model).AssignID));
                   Response.Write(Html.Hidden("AssignRole", ((JobRequest)ViewData.Model).AssignRole));
                   Response.Write(Html.Hidden("InvolveID", ((JobRequest)ViewData.Model).InvolveID));
                   Response.Write(Html.Hidden("InvolveRole", ((JobRequest)ViewData.Model).InvolveRole));
                   Response.Write(Html.Hidden("InvolveResolution", ((JobRequest)ViewData.Model).InvolveResolution));
                   Response.Write(Html.Hidden("InvolveDate", ((JobRequest)ViewData.Model).InvolveDate));
                   Response.Write(Html.Hidden("RequestorId", ((JobRequest)ViewData.Model).RequestorId));
                   Response.Write(Html.Hidden("UpdateDate", ((JobRequest)ViewData.Model).UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label" style="width: 145px;">
            Justification
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextArea("Justification", new { @style = "width:400px; height:100px" }));
               }
               else
               {
                   Response.Write(Html.TextArea("Justification", ((JobRequest)ViewData.Model).Justification, new { @style = "width:400px; height:100px" }));
               }              
            %>
        </td>
    </tr>
    <tr>
        <td class="label">
            CC List
        </td>
        <td class="input">
            
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("fieldCC", ""));
                   Response.Write(Html.Hidden("CCList", ""));
               }
               else
               {
                   Response.Write(Html.TextBox("fieldCC", ((JobRequest)ViewData.Model).CCList));
                   Response.Write(Html.Hidden("CCList", ""));
               }              
            %>
            <br/><%= Constants.SAMPLE_AUTO_COMPLETE %>
        </td>
    </tr>
    <tr>
        <td class="label">
            Resolution
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("WFResolutionID"));
               }
               else
               {
                   if (ViewData["FirstChoiceResolution"] != "")
                   {
                       Response.Write(Html.DropDownList("WFResolutionID", null, ViewData["FirstChoiceResolution"] as string));
                   }
                   else
                   {
                       Response.Write(Html.DropDownList("WFResolutionID"));
                   }
               }
                                          
            %>
            &nbsp;&nbsp;&nbsp;
            Status
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList("WFStatusID", null, new { @style = "width:72px" }));
               }
               else
               {
                   if (ViewData["FirstChoiceStatus"] != "")
                   {
                       Response.Write(Html.DropDownList("WFStatusID", null, ViewData["FirstChoiceStatus"] as string, new { @style = "width:72px" }));
                   }
                   else
                   {
                       Response.Write(Html.DropDownList("WFStatusID", null, new { @style = "width:72px" }));
                   }
               }
                                          
            %>
        </td>
    </tr>
        <tr id="assignTr">
            <td class="label">
                Forward to
            </td>
            <td class="input">
                <% if (ViewData.Model == null)
                   {
                       Response.Write(Html.DropDownList("Assign"));
                   }
                   else
                   {
                       if (ViewData["FirstChoiceAssign"] != "")
                       {
                           Response.Write(Html.DropDownList("Assign", null, ViewData["FirstChoiceAssign"] as string));
                       }
                       else
                       {
                           Response.Write(Html.DropDownList("Assign"));
                       }

                   }
                                          
                %>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <input type="submit" class="save" value="" alt="" />
                <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
            </td>
        </tr>
</table>
