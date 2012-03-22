using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Library.Common;

namespace CRM.Library.GenericHandle
{
    /// <summary>
    /// Summary description for UploadFileHandler
    /// </summary>
    public class UploadFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //string result = "{\"isSuccess\":\"{0}\",\"message\":\"{1}\",\"fileName\":\"{2}\"}";
            string result = "\"isSuccess\":\"{0}\",\"message\":\"{1}\",\"fileName\":\"{2}\"";
            context.Response.ContentType = "application/json";
            string fileDirectory = context.Server.MapPath(Constants.UPLOAD_TEMP_PATH);
             string filenameServer = string.Empty;
            string page = context.Request["Page"];
            
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile fileData = context.Request.Files[0];
                Message msg = null;
                switch (page)
                {
                    case "Menu":
                        msg = CheckUploadedFile(fileData, Constants.MENU_PAGE_IMAGE_FORMAT_ALLOWED, Constants.MENU_PAGE_IMAGE_MAX_SIZE);
                        filenameServer = Constants.MENU_PAGE_ICON_NAME_PREFIX + DateTime.Now.ToString("hhmmssff-") + fileData.FileName;
                        break;
                    case "Material":
                        msg = CheckUploadedFile(fileData, Constants.MATERIAL_PAGE_FILE_FORMAT_ALLOWED, Constants.MATERIAL_PAGE_FILE_MAX_SIZE);
                        filenameServer = Constants.MATERIAL_PAGE_ICON_NAME_PREFIX +  DateTime.Now.ToString("hhmmssff-") + fileData.FileName;
                        break;
                }
                if (msg == null)
                {
                   
                    fileData.SaveAs(fileDirectory + filenameServer);
                    context.Response.Write("{" + string.Format(result, 1, "success", filenameServer) + "}");
                }
                else
                {
                    context.Response.Write("{" + string.Format(result, 0, msg.MsgText, "") + "}");
                }
            }
            else
                context.Response.Write("{" + string.Format(result, 0, "No file is selected", "") + "}");
            context.Response.End();

        }

        /// <summary>
        /// Check Multi Upload
        /// </summary>
        /// <returns></returns>
        //private Message CheckMaterialFileUpload(HttpPostedFile hpf)
        //{
        //    Message msg = null;
        //    bool invalidExtension = false;
        //    bool invalidSize = false;
        //    bool invalidName = false;
        //    string errorExtension = string.Empty;
        //    string errorFileName = string.Empty;
        //    string duplicateName = string.Empty;
        //    if (hpf.ContentLength != 0)
        //    {
        //        int maxSize = 1024 * 1024 * Constants.CV_MAX_SIZE;
        //        float sizeFile = float.Parse(hpf.ContentLength.ToString("N"));
        //        string extension = System.IO.Path.GetExtension(hpf.FileName);
        //        string[] extAllowList = Constants.CONTRACT_EXT_ALLOW.Split(',');

        //        if (!extAllowList.Contains(extension.ToLower())) //check extension file is valid
        //        {
        //            invalidExtension = true;
        //            errorExtension += extension + ",";
        //        }
        //        else if (sizeFile > maxSize) //check maxlength of uploaded file
        //        {
        //            invalidSize = true;
        //        }
        //        else if (duplicateName.Contains(System.IO.Path.GetFileName(hpf.FileName)))
        //        {
        //            errorFileName = System.IO.Path.GetFileName(hpf.FileName);
        //            invalidName = true;
        //        }
        //    }
        //    if (invalidExtension == true)
        //    {
        //        msg = new Message(MessageConstants.E0013, MessageType.Error, errorExtension.TrimEnd(','), Constants.CONTRACT_EXT_ALLOW, Constants.CV_MAX_SIZE.ToString());
        //    }
        //    else if (invalidSize == true)
        //    {
        //        msg = new Message(MessageConstants.E0012, MessageType.Error, Constants.CV_MAX_SIZE.ToString());
        //    }
        //    else if (invalidName == true)
        //    {
        //        msg = new Message(MessageConstants.E0017, MessageType.Error, errorFileName);
        //    }
        //    return msg;
        //}

        private Message CheckUploadedFile(HttpPostedFile fileData, string allowedFileFormat, long maxSize)
        {
            string fileExt = fileData.FileName.Substring(fileData.FileName.LastIndexOf('.'));
            if (!allowedFileFormat.Replace("*", "").Split(';').Contains(fileExt))
                return new Message(MessageConstants.E0013, MessageType.Error, fileExt.Remove(0, 1),
                    allowedFileFormat, maxSize);
            if (fileData.ContentLength / (1024 * 1024) > maxSize)
                return new Message(MessageConstants.E0012, MessageType.Error, maxSize);
            return null;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}