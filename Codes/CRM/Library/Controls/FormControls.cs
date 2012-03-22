using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc.Html
{
    public static class FormControls
    {
        public static MvcHtmlString ValidateForm(this HtmlHelper htmlHelper, string formID, string rule, string submitHandler)
        {
            string scr = "$('#"+formID+"').validate({\n";
            scr += @"debug: false,
                errorElement: 'span',
                errorPlacement: function (error, element) {
                    error.tooltip({
                        bodyHandler: function () {
                            return error.html();
                        }
                    });
                    error.insertAfter(element);
                },";
            scr +=@"rules: {"+rule+"}";
            if (!string.IsNullOrEmpty(submitHandler))
            {
                scr += ",\n";
                scr += "submitHandler: function (form) {";
                scr += submitHandler;
                scr += "}\n";
            }
            scr += "});";
            return BaseHelper.ScriptHelper(scr);
        }
        public static MvcHtmlString ValidateForm(this HtmlHelper htmlHelper, string formID, string rule)
        {
            return ValidateForm(htmlHelper, formID, rule);
        }
    }
}