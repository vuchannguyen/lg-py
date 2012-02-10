<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FckJquery
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!--script type="text/javascript" src="/ckeditor-with-filemanager/ckeditor.js"></script>-->
    <script type="text/javascript" src="/ckeditor/ckeditor.js"></script>
    <form method="post">
		<p>
			My Editor:<br />
			<textarea id="editor1" name="editor1">&lt;p&gt;Initial value.&lt;/p&gt;</textarea>
			<script type="text/javascript">
			    CKEDITOR.replace('editor1');
			</script>
		</p>
		<p>
			<input type="submit" />
		</p>
	</form>
</asp:Content>
