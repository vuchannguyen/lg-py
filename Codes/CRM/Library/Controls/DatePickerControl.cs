using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{

    public static class DatePickerControl
    {
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string value, bool isCheck, string yearRange, object htmlAttributes)
        {
            string innerScript = "$(\"#" + name + "\").datepicker(";
            if (isCheck || yearRange != "")
            {
                string strOnClose = "onClose: function () { $(this).valid(); }";
                string strRange = String.Format("yearRange: \"{0}\"", yearRange);
                innerScript += "{";
                if (isCheck && yearRange != "")
                {
                    innerScript += strOnClose;
                    innerScript += ",";
                    innerScript += strRange;
                }
                else
                {
                    if (isCheck)
                        innerScript += strOnClose;
                    else
                    {
                        if (yearRange != "")
                            innerScript += strRange;
                    }
                }

                innerScript += "}";
            }
            innerScript += ");";
            return BaseHelper.InputHelper(InputTypeHelper.text, name, value, innerScript, htmlAttributes);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string value, bool isCheck, string yearRange)
        {
            return DatePicker(htmlHelper, name, value, isCheck, yearRange, null);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, bool isCheck, string yearRange)
        {
            return DatePicker(htmlHelper, name, "", isCheck, yearRange, null);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, bool isCheck, string yearRange, object htmlAttributes)
        {
            return DatePicker(htmlHelper, name, "", isCheck, yearRange, htmlAttributes);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name)
        {
            return DatePicker(htmlHelper, name, "", false, "", null);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            return DatePicker(htmlHelper, name, "", false, "", htmlAttributes);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string value)
        {
            return DatePicker(htmlHelper, name, value, false, "", null);
        }
        public static MvcHtmlString DatePicker(this HtmlHelper htmlHelper, string name, string value, object htmlAttributes)
        {
            return DatePicker(htmlHelper, name, value, false, "", htmlAttributes);
        }
    }

}
