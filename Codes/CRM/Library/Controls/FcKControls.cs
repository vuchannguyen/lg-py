using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Globalization;

namespace System.Web.Mvc.Html
{
    public static class FcKControls
    {
        public static MvcHtmlString FCKEditor(this HtmlHelper htmlHelper, string name, string value, int width, int height, string toolbarSets)
        {            
            string inclueScript = @"
<script type=""text/javascript"" src=""/Scripts/FckEditor/fckeditor/fckeditor.js""></script>
<script type=""text/javascript"" src=""/Scripts/FckEditor/jquery.FCKEditor.js""></script>";
            if (value == "")
            {
                value = Convert.ToString(htmlHelper.ViewDataContainer.ViewData[name], CultureInfo.InvariantCulture);
            }
            string textArea = String.Format(@"<textarea name=""{0}"" id=""{0}"" rows=""10"" cols=""80"" style=""width:{1}px; height: {2}"">{3}</textarea>", name,width,height, value);
            string innerScr = @"$.fck.config = { BasePath: '/Scripts/FckEditor/fckeditor/', height: " + height + ",width: " + width + ", ToolbarSet:'" + toolbarSets + "'};$('textarea#" + name + "').fck();";
            string script = BaseHelper.ScriptHelper(innerScr).ToString();

            return MvcHtmlString.Create(inclueScript + script + textArea);
        }
        public static MvcHtmlString FCKEditor(this HtmlHelper htmlHelper, string name, int width, int height, string toolbarSets)
        {
            return FCKEditor(htmlHelper, name, "", width, height, toolbarSets);
        }
    }
}