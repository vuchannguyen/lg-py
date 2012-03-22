using System;
using System.IO;
using ComExcel = Microsoft.Office.Interop.Excel;
namespace CRM.Library.Common
{
    public class ComExportExcel
    {
        private string _fileName = null;       
        private ComExcel.Application xlApp = null;
        private ComExcel.Workbook xlWorkBook = null;
        private ComExcel.Worksheet xlWorkSheet = null;

        public ComExcel.Worksheet XlWorkSheet
        {
            get { return xlWorkSheet; }
            set { xlWorkSheet = value; }
        }

        private object misValue = System.Reflection.Missing.Value;
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                throw;
            }
        }
        public ComExportExcel(string templatePath)
        {
            xlApp = new ComExcel.Application();
            xlWorkBook = xlApp.Workbooks.Open(templatePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
            //xlWorkBook = xlApp.Workbooks.Open(templatePath, ReadOnly:false, Editable:true );
            xlWorkSheet = (ComExcel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }

        public void DisposeExcel()
        {
            xlWorkBook.Close(SaveChanges:true);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        public bool SaveFile(string fileName)
        {
            try
            {
                xlWorkBook.SaveAs(Filename:fileName);
                _fileName = fileName;
                DisposeExcel();
                return true;
            }
            catch (Exception ex)
            {
                DisposeExcel();
                throw ex;
            }
        }

        public void RemoveFile()
        {
            try
            {
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}