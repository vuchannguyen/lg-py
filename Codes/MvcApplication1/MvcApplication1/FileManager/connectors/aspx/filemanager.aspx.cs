/* Connector for aspx
 * LDTECH ,.JSC 2010
 * http://www.ldtech.com.vn
 * Written by Toan Nguyen 
 */

using System;
//using System.Collections;
//using System.Configuration;
//using System.Data;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;

public partial class filemanager : System.Web.UI.Page
{
    #region Disable ASP.NET features

    /// <summary>
    /// Theming is disabled as it interferes in the connector response data.
    /// </summary>
    public override bool EnableTheming
    {
        get { return false; }
        set { /* Ignore it with no error */ }
    }

    /// <summary>
    /// Master Page is disabled as it interferes in the connector response data.
    /// </summary>
    public override string MasterPageFile
    {
        get { return null; }
        set { /* Ignore it with no error */ }
    }

    /// <summary>
    /// Theming is disabled as it interferes in the connector response data.
    /// </summary>
    public override string Theme
    {
        get { return ""; }
        set { /* Ignore it with no error */ }
    }

    /// <summary>
    /// Theming is disabled as it interferes in the connector response data.
    /// </summary>
    public override string StyleSheetTheme
    {
        get { return ""; }
        set { /* Ignore it with no error */ }
    }

    #endregion

    #region properties
    public string BaseUrl = "";
    public string BaseInstall = "";
    #endregion

    public bool CheckAuthentication()
    {
        //return (Session["SUCCESSFULLOGINED"] != null && Session["SUCCESSFULLOGINED"].ToString() == "Yes"); //return false;
        return true;
    }

    public void SetConfig()
    {

        //The base URL used to reach files in CKFinder through the browser.
        //if (Session["LDT_SITE_ID"] == null)
        //{
        //    Response.Write("No session value");
        //    Response.End();
        //    return;
        //}

        //string strSiteValues = Session["LDT_SITE_ID"].ToString(); // LdSpace.Security.Decrypt(Request.QueryString["site"]);        
        //string id = Request.UrlReferrer.Query;
        BaseUrl = ""; //Note: not have '/' at the end

        //The installation location that we place FileManager files
        BaseInstall = "/FileManager/";
    }

    private string GetInfo(string path, string fullPhysicalPath)
    {
        if (Request.QueryString["path"] == null) return "";

        string retVal = "";
        string fileName = "";
        string fileType = "";
        string preview = "";
        string dateCreated = "";
        string dateModified = "";
        string height = "";
        string width = "";
        string size = "";
        string error = "";
        string code = "0";

        if (File.Exists(fullPhysicalPath))
        {
            // file
            FileInfo fi = new FileInfo(fullPhysicalPath);
            fileName = fi.Name;
            fileType = fi.Extension.Replace(".", "");
            if (fileType.Length == 0) fileType = "txt";
            if (IsImageExtension(fileType))
                preview = path;
            else
            {
                preview = BaseInstall + "images/fileicons/" + fileType + ".png";
            }
            dateCreated = fi.CreationTime.ToString();
            dateModified = fi.LastWriteTime.ToString();
            size = fi.Length.ToString();

            if (Request.QueryString["getsize"] != null && Request.QueryString["getsize"] == "true")
            {
                int tempHeight = 0;
                int tempWidth = 0;

                GetImageDimensions(fullPhysicalPath, out tempHeight, out tempWidth);
                height = tempHeight.ToString();
                width = tempWidth.ToString();
            }
        }
        else if (Directory.Exists(fullPhysicalPath))
        {
            // directory

            DirectoryInfo di = new DirectoryInfo(fullPhysicalPath);
            fileName = di.Name; ;
            fileType = "dir";
            preview = BaseInstall + "images/fileicons/_Close.png";
            dateCreated = di.CreationTime.ToString();
            dateModified = di.LastWriteTime.ToString();
        }
        else
        {
            // not file or directory
            error = "no file or directory";
            code = "-1";
        }
        retVal = "{ \"Path\":" + EnquoteJSON(path) + ",\r\n" +
            " \"Filename\":" + EnquoteJSON(fileName) + ",\r\n" +
            " \"File Type\":" + EnquoteJSON(fileType) + ",\r\n" +
            " \"Preview\":" + EnquoteJSON(preview) + ",\r\n" +
            " \"Properties\":{\r\n" +
            "       \"Date Created\":" + EnquoteJSON(dateCreated) + ",\r\n" +
            "       \"Date Modified\":" + EnquoteJSON(dateModified) + "\r\n";
        if (height.Length > 0) retVal += "       ,\"Height\":" + height + "\r\n";
        if (width.Length > 0) retVal += "       ,\"Width\":" + width + "\r\n";
        if (size.Length > 0) retVal += "       ,\"Size\":" + size + "\r\n";
        retVal += "},\r\n" +
            " \"Error\":" + EnquoteJSON(error) + ",\r\n" +
            " \"Code\":" + code +
            "\r\n}";

        return retVal;
    }

    private string GetInfo()
    {
        if (Request.QueryString["path"] == null) return "";

        string path = Request.QueryString["path"];
        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        return GetInfo(path, fullPhysicalPath);

    }

    private string GetFolder()
    {
        if (Request.QueryString["path"] == null) return "";

        string retVal = "";
        string path = Request.QueryString["path"];
        if (!path.EndsWith("/")) path += "/";

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        if (Directory.Exists(fullPhysicalPath))
        {

            DirectoryInfo inputDI = new DirectoryInfo(fullPhysicalPath);

            DirectoryInfo[] subDirs = inputDI.GetDirectories();
            foreach (DirectoryInfo di in subDirs)
            {
                string subPath = path + di.Name + "/";
                string dirInfo = GetInfo(subPath, di.FullName);

                if (retVal.Length > 0) retVal += ",\r\n";
                retVal += dirInfo;
            }

            FileInfo[] fileInfos = inputDI.GetFiles();

            foreach (FileInfo fi in fileInfos)
            {
                string subPath = path + fi.Name;
                string fileInfo = GetInfo(subPath, fi.FullName);

                if (retVal.Length > 0) retVal += ",\r\n";
                retVal += fileInfo;

            }
        }

        retVal = "[\r\n" + retVal + "\r\n]";

        return retVal;
    }
    private string Rename()
    {
        if (Request.QueryString["old"] == null) return "";
        if (Request.QueryString["new"] == null) return "";


        string path = Request.QueryString["old"];

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        string retVal = "";
        string oldPath = path;
        string oldName = "";
        string newPath = "";
        string newName = Request.QueryString["new"];
        string error = "";
        string code = "0";

        if (File.Exists(fullPhysicalPath))
        {
            // file
            FileInfo fi = new FileInfo(fullPhysicalPath);

            string dir = fi.DirectoryName;
            oldName = fi.Name;
            if (path.EndsWith("/")) path = path.TrimEnd('/');
            newPath = path.Substring(0, path.Length - oldName.Length) + newName;

            if (!dir.EndsWith("\\")) dir += "\\";
            string fullPhysicalNewPath = dir + newName;

            if (File.Exists(fullPhysicalNewPath))
            {
                error = "New name exists";
                code = "-1";
            }
            else
                File.Move(fullPhysicalPath, fullPhysicalNewPath);
        }
        else if (Directory.Exists(fullPhysicalPath))
        {
            // directory
            DirectoryInfo di = new DirectoryInfo(fullPhysicalPath);

            string dir = di.Parent.FullName;
            oldName = di.Name;
            if (path.EndsWith("/")) path = path.TrimEnd('/');
            newPath = path.Substring(0, path.Length - oldName.Length) + newName;

            if (!dir.EndsWith("\\")) dir += "\\";
            string fullPhysicalNewPath = dir + newName;

            if (Directory.Exists(fullPhysicalNewPath))
            {
                error = "New name exists";
                code = "-1";
            }
            else
                Directory.Move(fullPhysicalPath, fullPhysicalNewPath);
        }
        else
        {
            // not file or directory
            error = "no file or directory";
            code = "-1";
        }

        retVal = "{ \"Error\":" + EnquoteJSON(error) + "," +
            " \"Code\":" + code + "," +
            " \"Old Path\":" + oldPath + "," +
            " \"Old Name\":" + oldName + "," +
            " \"New Path\":" + newPath + "," +
            " \"New Name\":" + newName +
            "}";

        return retVal;
    }
    private string Delete()
    {
        if (Request.QueryString["path"] == null) return "";


        string path = Request.QueryString["path"];

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        string retVal = "";
        string error = "";
        string code = "0";

        if (File.Exists(fullPhysicalPath))
        {
            // file
            File.Delete(fullPhysicalPath);
        }
        else if (Directory.Exists(fullPhysicalPath))
        {
            // directory
            Directory.Delete(fullPhysicalPath, true);
        }
        else
        {
            // not file or directory
            error = "no file or directory";
            code = "-1";
        }

        //retVal = "{ \"Error\":" + EnquoteJSON(error) + "," +
        //    " \"Code\":" + code + "," +
        //    " \"Path\":" + path +
        //    "}";

        retVal = "{ \"Error\":" + EnquoteJSON(error) + "," +
           " \"Code\":" + code + "," +
           " \"Path\":" + EnquoteJSON(path) + "}";

        return retVal;
    }
    private string Add()
    {
        string path = Request.Form["currentpath"];

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        //WriteToLog("filemanager", "path:" + path + "\r\n");
        //WriteToLog("filemanager", "fullPhysicalPath:" + fullPhysicalPath + "\r\n");

        string retVal = "";
        string name = "";
        string error = "";
        string code = "0";

        if (Directory.Exists(fullPhysicalPath))
        {
            if (Request.Files["newfile"] != null)
            {
                System.Web.HttpPostedFile inpFile = Request.Files["newfile"];
                name = GetShortFileName(inpFile.FileName);

                char[] invalidFileChars = System.IO.Path.GetInvalidFileNameChars();

                foreach (char c in invalidFileChars)
                {
                    name = name.Replace(c.ToString(), "");
                }

                if (!fullPhysicalPath.EndsWith("\\")) fullPhysicalPath += "\\";
                fullPhysicalPath += name;

                if (File.Exists(fullPhysicalPath)) File.Delete(fullPhysicalPath);

                inpFile.SaveAs(fullPhysicalPath);
            }
            else
            {
                // not file uploaded
                error = "No file uploaded";
                code = "-1";
            }
        }
        else
        {
            // not file or directory
            error = "No directory";
            code = "-1";
        }

        retVal = "<textarea>{ " +
            " \"Path\":" + EnquoteJSON(path) + "," +
            " \"Name\":" + EnquoteJSON(name) + "," +
            " \"Error\":" + EnquoteJSON(path) + "," +
            " \"Code\":" + code +
            "}</textarea>";

        return retVal;

    }
    private string AddFolder()
    {
        if (Request.QueryString["path"] == null) return "";

        string path = Request.QueryString["path"];

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        string retVal = "";
        string parent = path;
        string name = Request.QueryString["name"];
        string error = "";
        string code = "0";

        if (Directory.Exists(fullPhysicalPath))
        {
            // directory

            if (!fullPhysicalPath.EndsWith("\\")) fullPhysicalPath += "\\";
            fullPhysicalPath += name;

            if (!Directory.Exists(fullPhysicalPath))
            {
                Directory.CreateDirectory(fullPhysicalPath);
            }
            else
            {
                // not file or directory
                error = "Name already exists";
                code = "-1";
            }
        }
        else
        {
            // not file or directory
            error = "no file or directory";
            code = "-1";
        }

        retVal = "{ " +
            " \"Parent\":" + EnquoteJSON(parent) + "," +
            " \"Name\":" + EnquoteJSON(name) + "," +
            " \"Error\":" + EnquoteJSON(error) + "," +
            " \"Code\":" + code +
            "}";

        return retVal;
    }

    private void Download()
    {
        if (Request.QueryString["path"] == null) return;

        string path = Request.QueryString["path"];

        string fullPhysicalPath = Server.MapPath(BaseUrl + path);

        fullPhysicalPath = fullPhysicalPath.TrimEnd(new char[] { '\\', '/' });

        if (!File.Exists(fullPhysicalPath)) return;

        string name = GetShortFileName(fullPhysicalPath);
        //name = Uri.EscapeDataString(name);

        Response.ClearContent();
        Response.ContentType = "application/x-download";
        Response.AddHeader("content-disposition", "attachment; filename=\"" + name + "\"");
        Response.AddHeader("Content-Transfer-Encoding", "Binary");

        //header('Content-Type: application/octet-stream');

        FileStream sourceFile = new FileStream(fullPhysicalPath, FileMode.Open);
        long fileSize;
        fileSize = sourceFile.Length;
        byte[] getContent = new byte[(int)fileSize];
        sourceFile.Read(getContent, 0, (int)sourceFile.Length);
        sourceFile.Close();

        Response.AddHeader("Content-length", fileSize.ToString());

        Response.BinaryWrite(getContent);

        return;
    }
    public void Page_Load()
    {
        if (!CheckAuthentication())
        {
            Response.Write(CreateError("No Permission"));
            return;
        }
        SetConfig();

        string mode = "";
        string response = "";

        Response.ContentType = "text/plain";

        if (Request.QueryString["mode"] != null)
        {
            mode = Request.QueryString["mode"];
            switch (mode)
            {
                case "getinfo":
                    response = GetInfo();
                    break;

                case "getfolder":
                    response = GetFolder();
                    break;

                case "rename":
                    response = Rename();
                    break;

                case "delete":
                    response = Delete();
                    break;

                case "addfolder":
                    response = AddFolder();
                    break;

                case "download":
                    Download();
                    break;

                default:
                    CreateError("No Mode");
                    break;
            }
        }
        else if (Request.Form["mode"] != null)
        {
            mode = Request.Form["mode"];

            switch (mode)
            {
                case "add":
                    response = Add();
                    Response.ContentType = "text/html";
                    break;

                default:
                    CreateError("No Mode");
                    break;
            }
        }
        if (response.Length > 0) Response.Write(response);
    }
    private string CreateError(string error, string code)
    {
        return "{ \"Error\":" + EnquoteJSON(error) + ", \"Code\": " + code + " }";
    }
    private string CreateError(string error)
    {
        return CreateError(error, "-1");
    }

    ///  FUNCTION Enquote Public Domain 2002 JSON.org 
    ///  @author JSON.org 
    ///  @version 0.1 
    ///  Ported to C# by Are Bjolseth, teleplan.no 
    public static string EnquoteJSON(string s)
    {
        if (s == null || s.Length == 0)
        {
            return "\"\"";
        }
        char c;
        int i;
        int len = s.Length;
        StringBuilder sb = new StringBuilder(len + 4);
        string t;

        sb.Append('"');
        for (i = 0; i < len; i += 1)
        {
            c = s[i];
            if ((c == '\\') || (c == '"') || (c == '>'))
            {
                sb.Append('\\');
                sb.Append(c);
            }
            else if (c == '\b')
                sb.Append("\\b");
            else if (c == '\t')
                sb.Append("\\t");
            else if (c == '\n')
                sb.Append("\\n");
            else if (c == '\f')
                sb.Append("\\f");
            else if (c == '\r')
                sb.Append("\\r");
            else
            {
                if (c < ' ')
                {
                    //t = "000" + Integer.toHexString(c); 
                    string tmp = new string(c, 1);
                    t = "000" + int.Parse(tmp, System.Globalization.NumberStyles.HexNumber);
                    sb.Append("\\u" + t.Substring(t.Length - 4));
                }
                else
                {
                    sb.Append(c);
                }
            }
        }
        sb.Append('"');
        return sb.ToString();
    }


    private static bool IsImageExtension(string extension)
    {
        switch (extension.ToLower())
        {
            case "jpg":
            case "jpeg":
            case "gif":
            case "png":
            case "bmp":
                return true;
        }
        return false;
    }

    private static bool IsImage(string filePath)
    {
        System.Drawing.Image sourceImage;

        try
        {
            sourceImage = System.Drawing.Image.FromFile(filePath);
            sourceImage.Dispose();
            return true;
        }
        catch
        {
            // This is not a valid image. Do nothing.
            return false;
        }
    }

    private static void GetImageDimensions(string filePath, out int height, out int width)
    {
        height = 0;
        width = 0;

        System.Drawing.Image sourceImage;

        try
        {
            sourceImage = System.Drawing.Image.FromFile(filePath);

            height = sourceImage.Height;
            width = sourceImage.Width;

            sourceImage.Dispose();
        }
        catch
        {
            // This is not a valid image. Do nothing.
        }
    }

    public static string GetShortFileName(string fullFileName)
    {
        int lastPlace = fullFileName.LastIndexOf('\\');
        if (lastPlace < 0) lastPlace = fullFileName.LastIndexOf('/');
        string fileName = "";
        if (lastPlace < 0)
            fileName = fullFileName;
        else if (lastPlace < fullFileName.Length - 1)
            fileName = fullFileName.Substring(lastPlace + 1);
        return fileName;
    }
    public static string GetFileExtension(string fileName)
    {
        int lastPlace = fileName.LastIndexOf('.');
        if (lastPlace < 0 || lastPlace == fileName.Length - 1) return "";

        return fileName.Substring(lastPlace + 1);
    }

    public static void WriteToLog(string sessionID, string message)
    {
        string fileName = System.Web.HttpContext.Current.Server.MapPath("/");

        if (!fileName.EndsWith("\\")) fileName += "\\";
        fileName += "temp\\" + sessionID + ".log";
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true, System.Text.Encoding.UTF8);
        sw.WriteLine(message);
        sw.Close();
        sw.Dispose();
    }
}
