using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace Library
{
    public class ExportExcel
    {
        private static XLWorkbook workbook;
        private static IXLWorksheet ws;

        public static void SaveExcel(string path)
        {
            workbook.SaveAs(path);

            if (File.Exists(path))
            {
                if (MessageBox.Show(Constant.MESSAGE_SUCCESS_EXPORT_EXCEL + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_SUCCESS_EXPORT_EXCEL_OPEN,
                            Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(path);
                }
            }
        }

        public static void InitWorkBook()
        {
            workbook = new XLWorkbook();
        }

        public static void InitWorkBook(string sheetName)
        {
            workbook = new XLWorkbook();
            ws = workbook.Worksheets.Add(sheetName);
            ws.ShowGridLines = false;
            ws.Style.Font.FontName = "Arial";
            ws.Style.Alignment.WrapText = true;
            ws.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
        }

        public static void InitNewSheet(string sheetName)
        {
            if (workbook != null)
            {
                ws = workbook.Worksheets.Add(sheetName);
                ws.ShowGridLines = false;
                ws.Style.Font.FontName = "Arial";
                ws.Style.Alignment.WrapText = true;
                ws.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            }
        }

        public static void CreateSummaryKTCL(DateTime date)
        {
            ws.Cell("B1").Value = "KIỂM TRA CHÊNH LỆCH";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Cell("A1").Value = date.ToString(Constant.DEFAULT_DATE_FORMAT);

            ws.Columns().AdjustToContents(1, 2);
        }

        public static void CreateDetailsTableKTCL(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                int firstRow = 3;
                int firstCol = 1;
                int lastRow = firstRow + lv.Items.Count;
                int lastCol = lv.Columns.Count;

                // From worksheet
                var rngTableDetails = ws.Range(firstRow, firstCol, lastRow, lastCol);

                var rngHeadersDetails = rngTableDetails.Range(1, firstCol, 1, lastCol); // The address is relative to rngTable (NOT the worksheet)
                rngHeadersDetails.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                rngHeadersDetails.Style.Font.FontColor = XLColor.White;

                for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                {
                    ws.Cell(firstRow, colNum + 1).Value = lv.Columns[colNum].Text.ToString();
                }

                for (int rowNum = 0; rowNum < lv.Items.Count; rowNum++)
                {
                    for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                    {
                        ws.Cell(firstRow + 1 + rowNum, colNum + 1).Value = lv.Items[rowNum].SubItems[colNum].Text;
                    }
                }

                var rngDataDetails = ws.Range(firstRow + 1, firstCol, lastRow, lastCol);
                rngDataDetails.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngDataDetails.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var excelTableDetails = rngTableDetails.CreateTable();

                ws.Columns().AdjustToContents(1, 4);
            }
        }

        public static void CreateSummaryKTNK(DateTime date)
        {
            ws.Cell("B1").Value = "KIỂM TRA NHẬT KÝ";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Columns().AdjustToContents(1, 2);
        }

        public static void CreateDetailsTableKTNK(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                int firstRow = 3;
                int firstCol = 1;
                int lastRow = firstRow + lv.Items.Count;
                int lastCol = lv.Columns.Count;

                // From worksheet
                var rngTableDetails = ws.Range(firstRow, firstCol, lastRow, lastCol);

                var rngHeadersDetails = rngTableDetails.Range(1, firstCol, 1, lastCol); // The address is relative to rngTable (NOT the worksheet)
                rngHeadersDetails.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                rngHeadersDetails.Style.Font.FontColor = XLColor.White;

                for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                {
                    ws.Cell(firstRow, colNum + 1).Value = lv.Columns[colNum].Text.ToString();
                }

                for (int rowNum = 0; rowNum < lv.Items.Count; rowNum++)
                {
                    for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                    {
                        ws.Cell(firstRow + 1 + rowNum, colNum + 1).Value = lv.Items[rowNum].SubItems[colNum].Text;
                    }
                }

                var rngDataDetails = ws.Range(firstRow + 1, firstCol, lastRow, lastCol);
                rngDataDetails.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngDataDetails.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var excelTableDetails = rngTableDetails.CreateTable();

                ws.Columns().AdjustToContents(1, 4);
            }
        }

        public static void CreateSummaryNKNL(DateTime date)
        {
            ws.Cell("B1").Value = "NHẬT KÝ NGUYÊN LIỆU";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Columns().AdjustToContents(1, 2);
        }

        public static void CreateDetailsTableNKNL(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                int firstRow = 3;
                int firstCol = 1;
                int lastRow = firstRow + lv.Items.Count;
                int lastCol = lv.Columns.Count;

                // From worksheet
                var rngTableDetails = ws.Range(firstRow, firstCol, lastRow, lastCol);

                var rngHeadersDetails = rngTableDetails.Range(1, firstCol, 1, lastCol); // The address is relative to rngTable (NOT the worksheet)
                rngHeadersDetails.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                rngHeadersDetails.Style.Font.FontColor = XLColor.White;

                for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                {
                    ws.Cell(firstRow, colNum + 1).Value = lv.Columns[colNum].Text.ToString();
                }

                for (int rowNum = 0; rowNum < lv.Items.Count; rowNum++)
                {
                    for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                    {
                        ws.Cell(firstRow + 1 + rowNum, colNum + 1).Value = lv.Items[rowNum].SubItems[colNum].Text;
                        double tonCuoi = ConvertUtil.ConvertToDouble(lv.Items[rowNum].SubItems["colTonCuoi"].Text);
                        double hanMuc = ConvertUtil.ConvertToDouble(lv.Items[rowNum].SubItems["colHanMuc"].Text);

                        if (tonCuoi < hanMuc)
                        {
                            var rngHanMuc = ws.Range(firstRow + 1 + rowNum, firstCol, firstRow + 1 + rowNum, lv.Columns.Count);
                            rngHanMuc.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var rngDataDetails = ws.Range(firstRow + 1, firstCol, lastRow, lastCol);
                rngDataDetails.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngDataDetails.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var excelTableDetails = rngTableDetails.CreateTable();

                ws.Columns().AdjustToContents(1, 4);
            }
        }
    }
}
