using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class AutoCompleteTextBoxControl
    {
        public static MvcHtmlString AutoCompleteTextBox(this HtmlHelper htmlHelper, string name, string value,string defaultValue, string page,string addProperty,bool activeFocus_Blur, object htmlAttributes)
        {

            string innerScript = "$(\"#" + name + "\").autocomplete('/Library/GenericHandle/AutoCompleteHandler.ashx/?page=" + page + "'";
            if(!string.IsNullOrEmpty(addProperty))
            {
                innerScript += "," + addProperty;
            }
            
            innerScript+=");";
            if (activeFocus_Blur)
            {
                innerScript += "$(\"#" + name + "\").focus(function () {ShowOnFocus(this, '" + defaultValue + "');});";
                innerScript += "$(\"#" + name + "\").blur(function () {ShowOnBlur(this, '" + defaultValue + "');});";
            }
            return BaseHelper.InputHelper(InputTypeHelper.text,name, value, innerScript,htmlAttributes);
            
        }
        public static MvcHtmlString AutoCompleteTextBox(this HtmlHelper htmlHelper, string name, string value, string page, string addProperty, object htmlAttributes)
        {
            return AutoCompleteTextBox(htmlHelper, name, value, "", page, addProperty, false, htmlAttributes);
        }
        public static MvcHtmlString AutoCompleteTextBox(this HtmlHelper htmlHelper, string name, string value, string page)
        {
            return AutoCompleteTextBox(htmlHelper, name, value,"",page,"",false,null) ;
        }   
    }
}