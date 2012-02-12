<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Fck
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="../../Scripts/FckEditor/fckeditor/fckeditor.js"></script>
    <script type="text/javascript" src="../../Scripts/FckEditor/fckeditorapi.js"></script>
    
    <script type="text/javascript">
        window.onload = function () {
            var oFCKeditor = new FCKeditor('content');
            oFCKeditor.BasePath = "/fckeditor/";
            oFCKeditor.Height = 300;
            oFCKeditor.ReplaceTextarea();
        }

        function InsertContent() {
            var oEditor = FCKeditorAPI.GetInstance('content');
            var sample = document.getElementById("sample").value;
            oEditor.InsertHtml(sample);
        }

        function ShowContent() {
            var oEditor = FCKeditorAPI.GetInstance('content');
            alert(oEditor.GetHTML());
        }

        function ClearContent() {
            var oEditor = FCKeditorAPI.GetInstance('content');
            oEditor.SetHTML("");
        }
    </script>
    
    <div>
        <input id="sample" type="text" /> 
        <input id="cmdInsert" type="button" value="Insert Content" onclick="InsertContent()" />
        &nbsp;
        <input id="cmdClear" type="button" value="Clear Content" onclick="ClearContent()" />
        <br /> <br />
        <textarea id="content" cols="30" rows="10"></textarea>
        <br />
        <input id="cmdShow" type="button" value="Show Content" onclick="ShowContent()" />
    </div>
</asp:Content>
