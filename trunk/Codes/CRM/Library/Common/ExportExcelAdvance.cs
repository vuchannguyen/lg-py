using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using CRM.Models;
using CRM.Models.Entities;
namespace CRM.Library.Common
{
    /// <summary>
    /// A object contains key and value, it is similar to ListItem of WebControls
    /// </summary>
    public class CoupleString
    {
        public string key;
        public string value;

        public CoupleString(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    /// <summary>
    /// CExcell sheet class
    /// </summary>
    public class CExcelSheet
    {
        #region Variables
        private string _title;
        private string _name;
        
        private int _freezeColumn = 0;
        private int _freezeRow = 0;
        private bool _isRenderNo = false;
        private IEnumerable _list;
        private List<CoupleString> _exportBy = null;
        private string[] _header = null;
        private string[] _footer = null;
        private string[] _columnList = null;
        private bool _isGroup = false; //linh.quang.le
        private string _groupName = string.Empty; //linh.quang.le: group column name
        private string _mainColumn = string.Empty; //linh.quang.le: main column name
        #endregion

        /// <summary>
        /// Data list for sheet
        /// </summary>
        public IEnumerable List
        {
            set { _list = value; }
            get { return _list; }
        }

        /// <summary>
        /// Is have no
        /// </summary>
        public bool IsRenderNo
        {
            set { _isRenderNo = value; }
            get { return _isRenderNo; }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// Name of sheet
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// linh.quang.le: Name of freeze column
        /// </summary>
        public int FreezeColumn
        {
            set { _freezeColumn = value; }
            get { return _freezeColumn; }
        }

        /// <summary>
        /// linh.quang.le: Name of freeze row
        /// </summary>
        public int FreezeRow
        {
            set { _freezeRow = value; }
            get { return _freezeRow; }
        }

        /// <summary>
        /// Export conditions or filter conditions
        /// </summary>
        public List<CoupleString> ExportBy
        {
            set { _exportBy = value; }
            get { return _exportBy; }
        }

        /// <summary>
        /// Header sheet
        /// </summary>
        public string[] Header
        {
            set { _header = value; }
            get { return _header; }
        }

        /// <summary>
        /// Footer sheet
        /// </summary>
        public string[] Footer
        {
            set { _footer = value; }
            get { return _footer; }
        }

        /// <summary>
        /// Format column list
        /// </summary>
        public string[] ColumnList
        {
            set { _columnList = value; }
            get { return _columnList; }
        }
        public bool IsGroup
        {
            set { _isGroup = value; }
            get { return _isGroup; }
        }
        /// <summary>
        /// linh.quang.le: Name of group column
        /// </summary>
        public string GroupName
        {
            set { _groupName = value; }
            get { return _groupName; }
        }

        /// <summary>
        /// linh.quang.le: Name of main sub item's column
        /// </summary>
        public string MainColumn
        {
            set { _mainColumn = value; }
            get { return _mainColumn; }
        }
    }

    /// <summary>
    /// Export excel advance
    /// </summary>
    public class ExportExcelAdvance
    {
        private List<CExcelSheet> _sheets;
        private const int ROW_BEGIN = 2;
        private const int COL_BEGIN = 1;

        /// <summary>
        /// Sheets of excel file
        /// </summary>
        public List<CExcelSheet> Sheets
        {
            set { _sheets = value; }
            get { return _sheets; }
        }

        /// <summary>
        /// Create a worksheet
        /// </summary>
        /// <param name="worksheet">IXLWorksheet</param>
        /// <param name="sheet">CExcelSheet</param>
        private void CreateAWorkSheet(IXLWorksheet worksheet, CExcelSheet sheet)
        {

            //title
            IXLAddress firstAdd = worksheet.Cell(1, COL_BEGIN).Address;
            worksheet.Cell(1, 1).Value = sheet.Title;
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 15;
            worksheet.Cell(1, 1).Style.Font.FontColor = XLColor.White;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromArgb(0x0066cc);

            #region Export By

            int rowIdx = ROW_BEGIN;
            if (sheet.ExportBy != null)
            {
                foreach (var item in sheet.ExportBy)
                {
                    // reset column
                    int colIdx = COL_BEGIN;

                    worksheet.Cell(rowIdx, colIdx).DataType = XLCellValues.Text;
                    worksheet.Cell(rowIdx, colIdx).Value = "'" + item.key;
                    worksheet.Range(
                        worksheet.Cell(rowIdx, colIdx).Address,
                        worksheet.Cell(rowIdx, colIdx + 2).Address).Merge();
                    colIdx += 3;
                    worksheet.Cell(rowIdx, colIdx).DataType = XLCellValues.Text;
                    worksheet.Cell(rowIdx, colIdx).Value = "'" + item.value;
                    // new row
                    rowIdx++;
                }
            }

            #endregion

            // Header 
            int col = COL_BEGIN;

            #region Header
            //Add No
            if (sheet.IsRenderNo)
            {
                worksheet.Cell(rowIdx, col).Value = "No";
                worksheet.Cell(rowIdx, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Cell(rowIdx, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                col++;
            }
            foreach (string header in sheet.Header)
            {
                worksheet.Cell(rowIdx, col).Value = header;
                worksheet.Cell(rowIdx, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                col++;
            }
            //Style for header
            worksheet.Range(worksheet.Cell(rowIdx, COL_BEGIN).Address, worksheet.Cell(rowIdx, col - 1).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Range(worksheet.Cell(rowIdx, COL_BEGIN).Address, worksheet.Cell(rowIdx, col - 1).Address).Style.Font.Bold = true;
            worksheet.Range(worksheet.Cell(rowIdx, COL_BEGIN).Address, worksheet.Cell(rowIdx, col - 1).Address).Style.Font.FontSize = 12;
            worksheet.Range(worksheet.Cell(rowIdx, COL_BEGIN).Address, worksheet.Cell(rowIdx, col - 1).Address).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
            //merge title
            IXLAddress secondAdd = worksheet.Cell(1, col - 1).Address;
            worksheet.Range(firstAdd, secondAdd).Merge();
            worksheet.Range(firstAdd, secondAdd).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            // Merge export by
            for (int i = ROW_BEGIN; i < rowIdx; i++)
            {
                worksheet.Range(
                    worksheet.Cell(i, 4).Address,
                    worksheet.Cell(i, col - 1).Address).Merge();
            }
            #endregion

            // detail
            // Comment when adding export by, rowIdx is the number of export by
            //int idx_row = ROW_BEGIN + 1;
            int idx_row = rowIdx + 1;
            string preGroup = string.Empty;
            ArrayList beginSubList = new ArrayList();
            ArrayList endSubList = new ArrayList();
            //linh.quang.le: Freeze panels
            worksheet.SheetView.FreezeRows(sheet.FreezeRow);
            worksheet.SheetView.FreezeColumns(sheet.FreezeColumn);
            //linh.quang.le number
            int no = 1;

            #region Detail
            foreach (Object row in sheet.List)
            {
                int idx_col = COL_BEGIN;
                int index = 0;
                bool hasMainColumnValue = HasMainColumnValue(row, sheet);
                //linh.quang.le
                #region GroupName
                if (sheet.IsGroup)
                {
                    string groupName = string.Empty;
                    groupName = row.GetType().GetProperty(sheet.GroupName).GetValue(row, null).ToString();
                    if (!String.IsNullOrEmpty(groupName) && preGroup != groupName)
                    {
                        if (beginSubList.Count != 0)
                            endSubList.Add(idx_row - 1);

                        worksheet.Cell(idx_row, COL_BEGIN).Value = groupName;
                        worksheet.Cell(idx_row, COL_BEGIN).Style.Font.Bold = true;
                        worksheet.Cell(idx_row, COL_BEGIN).Style.Font.FontSize = 12;
                        worksheet.Cell(idx_row, COL_BEGIN).Style.Font.FontColor = XLColor.Black;
                        worksheet.Cell(idx_row, COL_BEGIN).Style.Fill.BackgroundColor = XLColor.FromArgb(0xD9D9D9);

                        IXLAddress firstGroupAddr = worksheet.Cell(idx_row, COL_BEGIN).Address;
                        IXLAddress secondGroupAddr = worksheet.Cell(idx_row, (COL_BEGIN + (sheet.IsRenderNo ? sheet.ColumnList.Length : sheet.ColumnList.Length - 1))).Address;
                        worksheet.Range(firstGroupAddr, secondGroupAddr).Merge();
                        worksheet.Range(firstGroupAddr, secondGroupAddr).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        preGroup = groupName;
                        no = 1;
                        idx_row++;
                        beginSubList.Add(idx_row);
                    }

                    if (sheet.IsRenderNo && hasMainColumnValue)
                    {
                        worksheet.Cell(idx_row, idx_col).Value = no.ToString();
                        worksheet.Cell(idx_row, idx_col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
                        no++;
                        idx_col++;
                    }
                }
                else
                {
                    if (sheet.IsRenderNo)
                    {
                        worksheet.Cell(idx_row, idx_col).Value = (idx_row - ROW_BEGIN).ToString();
                        worksheet.Cell(idx_row, idx_col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
                        idx_col++;
                    }
                }
                #endregion

                if (sheet.IsGroup)
                {
                    if (hasMainColumnValue)
                    {
                        foreach (string header in sheet.ColumnList)
                        {
                            string[] headerArr = header.Split(Convert.ToChar(":"));
                            Object obj = null;

                            if (row.GetType().GetProperty(headerArr[0].ToString()) != null)
                                obj = row.GetType().GetProperty(headerArr[0].ToString()).GetValue(row, null);
                            else
                            {
                                string[] arr = (string[])row;
                                if (arr != null)
                                    obj = arr[index++];
                            }
                            string strValue = string.Empty;

                            strValue = obj == null ? "" : obj.ToString();
                            #region  Format item
                            if (headerArr.Count() == 2)
                            {
                                switch (headerArr[1].ToString().ToLower())
                                {
                                    case "text":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                        worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                        strValue = "'" + strValue;
                                        break;
                                    case "date":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_VIEW);
                                        worksheet.Cell(idx_row, idx_col).Style.DateFormat.Format = Constants.DATETIME_FORMAT_VIEW;
                                        break;
                                    case "datetime":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_TIME);
                                        worksheet.Cell(idx_row, idx_col).Style.DateFormat.Format = Constants.DATETIME_FORMAT_TIME;
                                        worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                        break;
                                    // Convert from Int of Hour and Minute to "Hour : Minute" string
                                    // Using in Time Mangement Module
                                    // @author : tai.pham
                                    case "hour":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                        strValue = (obj == null || obj == string.Empty) ? string.Empty : "'" + ConvertUtil.ConvertToDouble(obj).ToString("0#:##");
                                        break;
                                    // Convert from location code to location string
                                    // Using in Time Mangement Module
                                    // @author : tai.pham
                                    case "location":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                        strValue = obj == null ? "" : CommonFunc.GenerateStringOfLocation((string)obj);
                                        break;
                                    case "gender":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = obj == null ? "" : (bool)obj == Constants.MALE ? "Male" : "Female";
                                        break;
                                    case "married":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = obj == null ? "" : (bool)obj == Constants.MARRIED ? "Married" : "Single";
                                        break;
                                    case "labor":
                                        strValue = obj == null ? "" : (bool)obj == Constants.LABOR_UNION_FALSE ? "No" : "Yes";
                                        break;
                                    case "hhmm": 
                                        worksheet.Cell(idx_row, idx_col).Style.NumberFormat.NumberFormatId = 20;
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = (obj == null || obj == "") ? "" : CommonFunc.FormatTime((double)obj);
                                        break;
                                    case "jr":
                                        strValue = obj == null ? "" : Constants.JOB_REQUEST_PREFIX + obj;
                                        break;
                                    case "pr":
                                        strValue = obj == null ? "" : Constants.PR_REQUEST_PREFIX + obj;
                                        break;
                                    case "sr":
                                        strValue = obj == null ? "" : Constants.SR_SERVICE_REQUEST_PREFIX + obj;
                                        break;
                                    case "candidate":
                                        strValue = obj == null ? "" : CommonFunc.GetCandidateStatus((int)obj);
                                        break;
                                    case "actionsendmail":
                                        strValue = obj == null ? "" : (bool)obj != true ? "No" : "Yes";
                                        break;
                                    case "jr_request":
                                        strValue = obj == null ? "" : (int)obj == Constants.JR_REQUEST_TYPE_NEW ? "New" : "Replace";
                                        break;
                                    case "dayofweek":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        strValue = obj == null ? "" : ((DateTime)obj).DayOfWeek.ToString();
                                        break;
                                    case "number":
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                        break;
                                    case "duration":
                                        strValue = obj == null ? "" : obj + " " + Constants.TC_DURATION_PREFIX;
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        break;
                                    default:
                                        worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                        break;
                                }

                                worksheet.Cell(idx_row, idx_col).Value = strValue;
                            }
                            #endregion
                            else
                            {
                                worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                worksheet.Cell(idx_row, idx_col).Value = "'" + strValue;
                            }

                            worksheet.Cell(idx_row, idx_col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            idx_col++;
                        }

                        worksheet.Columns(COL_BEGIN, idx_col).AdjustToContents();
                        idx_row++;
                    }
                }
                else
                {
                    foreach (string header in sheet.ColumnList)
                    {
                        string[] headerArr = header.Split(Convert.ToChar(":"));
                        Object obj = null;

                        if (row.GetType().GetProperty(headerArr[0].ToString()) != null)
                            obj = row.GetType().GetProperty(headerArr[0].ToString()).GetValue(row, null);
                        else
                        {
                            string[] arr = (string[])row;
                            if (arr != null)
                                obj = arr[index++];
                        }
                        string strValue = string.Empty;

                        strValue = obj == null ? "" : obj.ToString();
                        #region  Format item
                        if (headerArr.Count() == 2)
                        {
                            switch (headerArr[1].ToString().ToLower())
                            {
                                case "text":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                    worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                    strValue = "'" + strValue;
                                    break;
                                case "date":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_VIEW);
                                    worksheet.Cell(idx_row, idx_col).Style.DateFormat.Format = Constants.DATETIME_FORMAT_VIEW;
                                    break;
                                case "datetime":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).ToString(Constants.DATETIME_FORMAT_TIME);
                                    worksheet.Cell(idx_row, idx_col).Style.DateFormat.Format = Constants.DATETIME_FORMAT_TIME;
                                    worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                    break;
                                case "gender":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = obj == null ? "" : (bool)obj == Constants.MALE ? "Male" : "Female";
                                    break;
                                case "married":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = obj == null ? "" : (bool)obj == Constants.MARRIED ? "Married" : "Single";
                                    break;
                                case "labor":
                                    strValue = obj == null ? "" : (bool)obj == Constants.LABOR_UNION_FALSE ? "No" : "Yes";
                                    break;
                                // Convert from Int of Hour and Minute to "Hour : Minute" string
                                // Using in Time Management Module
                                // @author : tai.pham
                                case "hour":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                                    strValue = (obj == null || obj == string.Empty) ? string.Empty : "'" + ConvertUtil.ConvertToDouble(obj).ToString("0#:##");
                                    break;
                                // Convert from location code to location string
                                // Using in Time Mangement Module
                                // @author : tai.pham
                                case "location":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                    strValue = obj == null ? "" : CommonFunc.GenerateStringOfLocation((string)obj);
                                    break;
                                case "hhmm":
                                    worksheet.Cell(idx_row, idx_col).Style.NumberFormat.NumberFormatId = 20;
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = (obj == null || obj == "") ? "" : CommonFunc.FormatTime((double)obj);
                                    break;
                                case "jr":
                                    strValue = obj == null ? "" : Constants.JOB_REQUEST_PREFIX + obj;
                                    break;
                                case "pr":
                                    strValue = obj == null ? "" : Constants.PR_REQUEST_PREFIX + obj;
                                    break;
                                case "sr":
                                    strValue = obj == null ? "" : Constants.SR_SERVICE_REQUEST_PREFIX + obj;
                                    break;
                                case "candidate":
                                    strValue = obj == null ? "" : CommonFunc.GetCandidateStatus((int)obj);
                                    break;
                                case "actionsendmail":
                                    strValue = obj == null ? "" : (bool)obj != true ? "No" : "Yes";
                                    break;
                                case "jr_request":
                                    strValue = obj == null ? "" : (int)obj == Constants.JR_REQUEST_TYPE_NEW ? "New" : "Replace";
                                    break;
                                case "dayofweek":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    strValue = obj == null ? "" : ((DateTime)obj).DayOfWeek.ToString();
                                    break;
                                case "number":
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                    break;
                                case "duration":
                                    strValue = obj == null ? "" : obj + " " + Constants.TC_DURATION_PREFIX;
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                    break;
                                default:
                                    worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                    break;
                            }

                            worksheet.Cell(idx_row, idx_col).Value = strValue;
                        }
                        #endregion
                        else
                        {
                            worksheet.Cell(idx_row, idx_col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            worksheet.Cell(idx_row, idx_col).DataType = XLCellValues.Text;
                            worksheet.Cell(idx_row, idx_col).Value = "'" + strValue;
                        }

                        worksheet.Cell(idx_row, idx_col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        idx_col++;
                    }

                    worksheet.Columns(COL_BEGIN, idx_col).AdjustToContents();
                    idx_row++;
                }
            }
            if (sheet.IsGroup)
            {
                if (!String.IsNullOrEmpty(sheet.GroupName))
                {
                    for (int i = 0; i < beginSubList.Count; i++)
                    {
                        worksheet.Outline.SummaryVLocation = XLOutlineSummaryVLocation.Top;
                        if (i >= endSubList.Count)
                            worksheet.Rows((int)beginSubList[i], idx_row - 1).Group();
                        else
                            worksheet.Rows((int)beginSubList[i], (int)endSubList[i]).Group();
                    }
                }
            }

            #endregion
            #region Footer
            if (sheet.Footer != null)
            {
                col = COL_BEGIN;
                if (sheet.IsRenderNo)
                {
                    worksheet.Cell(idx_row, col).Value = "Total";
                    worksheet.Cell(idx_row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell(idx_row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    col++;
                }
                foreach (string footer in sheet.Footer)
                {
                    worksheet.Cell(idx_row, col).Value = footer;
                    worksheet.Cell(idx_row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    col++;
                }

                //Style for footer
                worksheet.Range(worksheet.Cell(idx_row, COL_BEGIN).Address, worksheet.Cell(idx_row, col - 1).Address).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Range(worksheet.Cell(idx_row, COL_BEGIN).Address, worksheet.Cell(idx_row, col - 1).Address).Style.Font.Bold = true;
                worksheet.Range(worksheet.Cell(idx_row, COL_BEGIN).Address, worksheet.Cell(idx_row, col - 1).Address).Style.Font.FontSize = 12;
                worksheet.Range(worksheet.Cell(idx_row, COL_BEGIN).Address, worksheet.Cell(idx_row, col - 1).Address).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
            }
            #endregion
        }

        /// <summary>
        /// Export excel multi sheet
        /// </summary>
        /// <param name="path">path of excel file</param>
        /// <returns>file name</returns>
        public string ExportExcelMultiSheet(string path)
        {
            FileInfo newFile = new FileInfo(path);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(path);
            }
            var workbook = new XLWorkbook();
            // add a new worksheet to the empty workbook                
            for(int i = 0 ; i < Sheets.Count(); i++)
            {
                var worksheet = workbook.Worksheets.Add(Sheets[i].Name);
                CreateAWorkSheet(worksheet, Sheets[i]);
            }
          
            // save our new workbook and we are done!
            workbook.SaveAs(path);
           
            return newFile.FullName;
       }

        private bool HasMainColumnValue(Object row, CExcelSheet sheet)
        {
            if (sheet.IsGroup == true)
            {
                if (String.IsNullOrEmpty(sheet.MainColumn) || (!String.IsNullOrEmpty(sheet.MainColumn) &&
                    row.GetType().GetProperty(sheet.MainColumn.ToString()).GetValue(row, null) != null))
                    return true;
                return false;
            }
            return true;
        }
    }
}