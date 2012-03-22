using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRM.Models;
using CRM.Library.Common;
using CRM.Library.Attributes;
using System.IO;
using System.Collections;
namespace CRM.Library.Common
{
    public class Export
    {
        private string _temp_excel_file_path;
        private string[] _colum_letters;
        private string _save_as_export_file_path;
        private int _sheet_number = 1;
        private int _index_row_template;
        /// <summary>
        /// Set index at tempalte row in excel template file  
        /// </summary>
        public int IndexRowTemplate
        {
            set { _index_row_template = value; }
        }
        /// <summary>
        /// Set number sheet to define sheet can get in template file, if not set then default = 1
        /// </summary>
        public int SheetNumber
        {
            set { _sheet_number = value; }
        }
        /// <summary>
        /// Set string array letter that need fill value into it.Example:{"A","B","..."} 
        /// </summary>
        public string[] ColumLetters
        {
            set { _colum_letters = value; }
        }
        /// <summary>
        /// Set path excel template file (Physical path)
        /// </summary>
        public string TemplateExcelFilePath
        {
            set { _temp_excel_file_path = value; }
        }
        /// <summary>
        /// Set path to save as to new file (Physical path)
        /// </summary>
        public string SaveAsExportFilePath
        {
            set { _save_as_export_file_path = value; }
        }
        private void SetCells(Excel.Worksheet sheet, List<ArrayList> dataList)
        {
            
            object misValue = System.Reflection.Missing.Value;
            int i = _index_row_template;
            Excel.Range rangTemplate = null;
           
            foreach (ArrayList row in dataList.ToList())
            {
                int j = -1;
                foreach (string letter in _colum_letters)
                {

                    string strCell = letter + _index_row_template;
                    rangTemplate = sheet.Cells.get_Range(strCell);

                    Excel.Range range = sheet.get_Range(letter + i);
                    range.BorderAround();
                    range.Interior.Color = rangTemplate.Interior.Color;
                    range.Font.Color = rangTemplate.Font.Color;
                    range.Font.Size = rangTemplate.Font.Size;
                    range.Font.FontStyle = rangTemplate.Font.FontStyle;
                    range.Font.Name = rangTemplate.Font.Name;
                    j++;
                    string value = row[j] == null ? "" : row[j].ToString();
                    range.set_Value(misValue, value);

                }
                i++;
            }


        }
               
        /// <summary>
        /// Export DataList To Excel File
        /// </summary>
        /// <param name="dataList">List of Data need export</param>
        /// <returns></returns>
        public bool ToExcel(List<ArrayList> dataList)
        {

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(_temp_excel_file_path);
            Excel.Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(_sheet_number);
            //SetValue and Fortmat base on template file

            SetCells(xlWorkSheet, dataList);

            //Save as new file
            xlWorkSheet.SaveAs(_save_as_export_file_path);
            //Dispose obj
            xlWorkBook.Close();
            xlApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            xlWorkSheet = null;
            xlWorkBook = null;
            xlApp = null;

            return true;


        }

    }
}