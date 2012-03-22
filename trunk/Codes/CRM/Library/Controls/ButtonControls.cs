using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc.Html
{
    public static class ButtonControls
    {
        public static MvcHtmlString UploadPhotoButton(this HtmlHelper htmlHelper, string name,string controller,string recordID,string saveTo, object htmlAttributes) {

            string url = String.Format("\"/Common/UploadImage?controller={0}&recordID={1}&saveTo={2}\"",controller,recordID,saveTo);
            string script = String.Format("$(\"#{0}\").click(function ()", name) + "{";
            script += String.Format("CRM.popUpWindow({0}, '#Photograph', 'Upload Photo');",url);
            script += "return false;});";
            return BaseHelper.InputHelper(InputTypeHelper.button, name, "Change photo",script, "upload_image",htmlAttributes);
        }
        public static MvcHtmlString UploadPhotoButton(this HtmlHelper htmlHelper, string name,string controller,string recordID,string saveTo)
        {

            return UploadPhotoButton(htmlHelper, name, controller, recordID, saveTo, null);
        }
    }
}