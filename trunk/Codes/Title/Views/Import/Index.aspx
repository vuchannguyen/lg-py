<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= TempData["Message"]%>
    <fieldset>
        <legend>Import Employee</legend>
        <form id="employeeForm" action="<%= Url.Action("Import", "Import")%>" method="post"
        enctype="multipart/form-data">
        <div>
            <input type="file" id="file" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>
        <input type="hidden" id="inp_Employee" name="inp_Employee" value="Employee" />
        </form>
    </fieldset>
    <br />
    <fieldset>
        <legend>Import Resigned Employee</legend>
        <form id="resignedEmployeeForm" action="<%= Url.Action("Import", "Import")%>" method="post"
        enctype="multipart/form-data">
        <div>
            <input type="file" id="file3" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>
        <input type="hidden" id="Hidden1" name="inp_ResignedEmployee" value="ResignedEmployee" />
        </form>
    </fieldset>
    <br />
    <fieldset>
        <legend>Import STT</legend>
        <form id="sttForm" action="<%= Url.Action("Import", "Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file1" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>
        <input type="hidden" id="inp_STT" value="STT" name="inp_STT" />
        </form>
    </fieldset>
    <br />
    <fieldset>
        <legend>Import Candidate</legend>
        <form id="candidateForm" action="<%= Url.Action("Import", "Import")%>" method="post"
        enctype="multipart/form-data">
        <div>
            <input type="file" id="file2" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>
        <input type="hidden" id="inp_Can" value="Candidate" name="inp_Can" />
        </form>
    </fieldset>
    <br />
    <fieldset>
        <legend>Import Employee Contract</legend>
        <form id="contractForm" action="<%= Url.Action("ImportContractIntoCRM", "Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file3" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>
        <input type="hidden" id="inp_Contract" value="Contract" name="inp_Contract" />
        </form>
    </fieldset>
    
    <fieldset>
        <legend>Import Employee Contract</legend>
        <form id="Form1" action="<%= Url.Action( "EmployeeContract","Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file4" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>        
        </form>
    </fieldset>

    <fieldset>
        <legend>Import Candidate</legend>
        <form id="Form2" action="<%= Url.Action("Candidate","Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file5" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>        
        </form>
    </fieldset>

    <fieldset>
        <legend>Import Interview</legend>
        <form id="Form3" action="<%= Url.Action("Interview","Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file6" name="file" />
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>        
        </form>
    </fieldset>
    <fieldset>
        <legend>Import Labor Union</legend>
        <div>
            <a href="/Import/ImportLaborUnion">Click here to import labor union</a>
        </div>
    </fieldset>
    
    <fieldset>
        <legend>Import Seat Code</legend>
        <form id="Form4" action="<%= Url.Action("SeatCode","Import")%>" method="post" enctype="multipart/form-data">
        <div>
            <input type="file" id="file7" name="file" />            
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>        
        </form>
    </fieldset>
    <fieldset>
        <legend>Import English Result</legend>
        <form id="Form5" action="<%= Url.Action("EnglishResult","Import")%>" method="post" enctype="multipart/form-data">
        <div>
         Exam ID  <input type="text" id="examID" name="examID" />                     
         <br />
         <input type="file" id="file8" name="file" />            
        </div>
        <br />
        <div class="cbutton" style="text-align: left">
            <input type="submit" title="Save" class="save" value="Import" />
        </div>        
        </form>
    </fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ModuleName" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FunctionTitle" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="LoginRoles" runat="server">
</asp:Content>
