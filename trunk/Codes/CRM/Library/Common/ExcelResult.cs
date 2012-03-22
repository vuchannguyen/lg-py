using System.Web.Mvc;

namespace System.Web.Mvc
{
    /// <summary>
    /// Excel result class
    /// </summary>
    public class ExcelResult : ActionResult
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Path of excel file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Response excel file
        /// </summary>
        /// <param name="context">ControllerContext</param>
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.ContentType = "application/x-msexcel.document.12";            
            //context.HttpContext.Response.WriteFile(Path);
            context.HttpContext.Response.TransmitFile(Path);
            context.HttpContext.Response.Flush();
            context.HttpContext.Response.End();
        }
    }
}