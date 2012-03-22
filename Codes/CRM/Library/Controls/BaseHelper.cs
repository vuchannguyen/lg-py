using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public class BaseHelper
    {
        public static MvcHtmlString InputHelper(InputTypeHelper inputType,string name, string value, string innerScript,string classAttr, object htmlAttributes)
        {

            StringBuilder strBuilder = new StringBuilder();

            TagBuilder tagScript = new TagBuilder("script");
            tagScript.InnerHtml = "$(document).ready(function () {";
            tagScript.InnerHtml += innerScript;
            tagScript.InnerHtml += "});";
            strBuilder.Append(tagScript.ToString());

            TagBuilder tagInput = new TagBuilder("input");
            if (htmlAttributes != null)
                tagInput.MergeAttributes<string, object>(((IDictionary<string, object>)new RouteValueDictionary(htmlAttributes)));
            tagInput.MergeAttribute("type",inputType.ToString());
            tagInput.MergeAttribute("name", name);
            tagInput.MergeAttribute("id", name);
            
            tagInput.MergeAttribute("class", classAttr);
            if(inputType==InputTypeHelper.button)
                tagInput.MergeAttribute("title", value);
            else
                tagInput.MergeAttribute("value", value);

            strBuilder.Append(tagInput.ToString(TagRenderMode.SelfClosing));

            return MvcHtmlString.Create(strBuilder.ToString());
        }
        public static MvcHtmlString InputHelper(InputTypeHelper inputType, string name, string value, string innerScript, object htmlAttributes)
        {
            return InputHelper(inputType, name, value, innerScript,"", htmlAttributes);
        }
        public static MvcHtmlString ScriptHelper( string innerScript)
        {
            StringBuilder strBuilder = new StringBuilder();

            TagBuilder tagScript = new TagBuilder("script");
            tagScript.InnerHtml = "$(document).ready(function () {";
            tagScript.InnerHtml += innerScript;
            tagScript.InnerHtml += "});";
            strBuilder.Append(tagScript.ToString());
            return MvcHtmlString.Create(strBuilder.ToString());
        }
        
                
    }
}