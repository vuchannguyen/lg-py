<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#ExamDate").datepicker({
            onClose: function () { $(this).valid(); }
        });

        $("#ExamForm").validate({
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
                Title: {
                    required: true,
                    maxlength: 200
                },
                ExamDate: {
                    required: true,
                    checkDate: true,
                    compareDate: ["#ExamForm input[name='CurrentDate']", "get", "ExamDate", "CurrentDate"]
                },
                ExamQuestionID: {
                    required: true,
                    number: true
                }
            }
        });
    });

    function onSubmit() {
        if ($("#ExamForm").valid()) {
            $("#btnSubmit").attr("disabled", "disabled");
            $("#ExamForm").submit();
        }
    }

</script>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="edit">
    <% 
        LOT_Exam exam = (LOT_Exam)ViewData.Model;
        Response.Write(Html.Hidden("CurrentDate", DateTime.Now.ToString(Constants.DATETIME_FORMAT)));
    %>
    <tr>
        <td class="label required">
            Title <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("Title", "", new { @maxlength = "200", @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("Title", exam.Title, new { @maxlength = "200", @style = "width:200px" }));
                   Response.Write(Html.Hidden("ID", exam.ID));
                   Response.Write(Html.Hidden("CreatedBy", exam.CreatedBy));
                   Response.Write(Html.Hidden("CreateDate", exam.CreateDate.ToString(Constants.DATETIME_FORMAT)));
                   Response.Write(Html.Hidden("UpdateDate", exam.UpdateDate.ToString()));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Exam Date <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.TextBox("ExamDate", "", new { @maxlength = "10", @style = "width:70px" }));
               }
               else
               {
                   Response.Write(Html.TextBox("ExamDate", exam.ExamDate.ToString(Constants.DATETIME_FORMAT), new { @maxlength = "10", @style = "width:70px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Exam Question <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList(CommonDataKey.EXAM_QUESTION_ID, null, Constants.FIRST_ITEM_EXAM_QUESTION, new { @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.DropDownList(CommonDataKey.EXAM_QUESTION_ID, null, Constants.FIRST_ITEM_EXAM_QUESTION, new { @style = "width:200px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td class="label required">
            Exam Type <span>*</span>
        </td>
        <td class="input">
            <% if (ViewData.Model == null)
               {
                   Response.Write(Html.DropDownList(CommonDataKey.EXAM_TYPE, null, new { @style = "width:200px" }));
               }
               else
               {
                   Response.Write(Html.DropDownList(CommonDataKey.EXAM_TYPE, null, new { @style = "width:200px" }));
               }
            %>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <input id="btnSubmit" type="button" class="save" value="" onclick="onSubmit()" alt="Update" />
            <input type="button" class="cancel" value="" alt="" onclick="CRM.closePopup()" />
        </td>
    </tr>
</table>
