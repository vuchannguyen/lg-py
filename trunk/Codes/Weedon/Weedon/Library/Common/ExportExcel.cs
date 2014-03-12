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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitWorkBook()
        {
            workbook = new XLWorkbook();
        }

        public static void InitWorkBook(string sheetName)
        {
            try
            {
                workbook = new XLWorkbook();
                ws = workbook.Worksheets.Add(sheetName);
                ws.ShowGridLines = false;
                ws.Style.Font.FontName = "Arial";
                ws.Style.Alignment.WrapText = true;
                ws.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Top);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InitNewSheet(string sheetName)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void CreateSummaryNKBH(DateTime date)
        {
            //ws.Cell("A2").Value = "BARCODE";
            //ws.Cell("B2").Value = "PLU NAME";
            //ws.Cell("C2").Value = "PRICE";

            ws.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Row(2).Style.Font.FontColor = XLColor.Blue;
            ws.Columns().AdjustToContents(1, 2);
            ws.SheetView.Freeze(2, 3);
        }

        public static void CreateDetailsTableNKBH(ListView lv)
        {
            try
            {
                if (lv.Items.Count > 0)
                {
                    int firstRow = 2;
                    int firstCol = 1;
                    int lastRow = firstRow + lv.Items.Count;
                    int lastCol = lv.Columns.Count;

                    var rngDataDetails = ws.Range(1, firstCol, lastRow, lastCol);
                    rngDataDetails.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rngDataDetails.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    // From worksheet
                    //var rngTableDetails = ws.Range(firstRow, firstCol, lastRow, lastCol);
                    //var rngHeadersDetails = rngTableDetails.Range(1, firstCol, 1, lastCol); // The address is relative to rngTable (NOT the worksheet)
                    //rngHeadersDetails.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    //rngHeadersDetails.Style.Font.FontColor = XLColor.White;
                    string date = string.Empty;
                    int shiftTime = 1;

                    for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                    {
                        if (colNum < 3)
                        {
                            ws.Cell(firstRow, colNum + 1).Value = lv.Columns[colNum].Text.ToString();
                        }
                        else
                        {
                            if (date != lv.Columns[colNum].Text)
                            {
                                shiftTime = 1;
                                date = lv.Columns[colNum].Text;
                                ws.Cell(firstRow, colNum + 1).Value = "Ca " + shiftTime.ToString();
                                ws.Cell(1, colNum + 1).Value = lv.Columns[colNum].Text.ToString();
                                ws.Column(colNum + 1).Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                                ws.Column(colNum + 1).Style.Border.RightBorder = XLBorderStyleValues.Thick;
                                shiftTime++;
                            }
                            else
                            {
                                date = lv.Columns[colNum].Text;
                                ws.Cell(firstRow, colNum + 1).Value = "Ca " + shiftTime.ToString();
                                ws.Cell(1, colNum - shiftTime + 2).Value = lv.Columns[colNum].Text.ToString();
                                ws.Range(1, colNum - shiftTime + 2, 1, colNum + 1).Merge();
                                ws.Column(colNum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                                shiftTime++;
                            }

                            var rngPrice = ws.Range(firstRow + 1, firstCol + 2, lastRow, firstCol + 2);
                            var rngQuantity = ws.Range(firstRow + 1, colNum + 1, lastRow, colNum + 1);
                            ws.Cell(lastRow + 1, colNum + 1).FormulaA1 = string.Format("=SUMPRODUCT({0},{1})", rngPrice.RangeAddress.ToStringFixed(), rngQuantity.RangeAddress.ToString());
                            ws.Cell(lastRow + 1, colNum + 1).Style.NumberFormat.Format = "#,##0";
                        }
                    }

                    //col calculate total quantity
                    ws.Cell(firstRow, lastCol + 4).Value = "Thống kê ly";

                    for (int rowNum = 0; rowNum < lv.Items.Count; rowNum++)
                    {
                        switch (lv.Items[rowNum].SubItems[0].Text)
                        { 
                            case "black":
                                ws.Row(firstRow + 1 + rowNum).Style.Fill.BackgroundColor = XLColor.Black;
                                continue;
                            case "orange":
                                ws.Row(firstRow + 1 + rowNum).Style.Fill.BackgroundColor = XLColor.Orange;
                                continue;
                            case "red":
                                ws.Row(firstRow + 1 + rowNum).Style.Fill.BackgroundColor = XLColor.Red;
                                continue;
                        }

                        for (int colNum = 0; colNum < lv.Columns.Count; colNum++)
                        {
                            ws.Cell(firstRow + 1 + rowNum, colNum + 1).Value = lv.Items[rowNum].SubItems[colNum].Text;
                        }

                        //cell calculate total price
                        var rngQuantity = ws.Range(firstRow + 1 + rowNum, firstCol + 3, firstRow + 1 + rowNum, lastCol);
                        ws.Cell(firstRow + 1 + rowNum, lastCol + 4).FormulaA1 = string.Format("=SUM({0})", rngQuantity.RangeAddress.ToString());
                    }

                    //cell calculate total price of a month
                    var rngTotal = ws.Range(lastRow + 1, firstCol + 3, lastRow, lastCol);
                    ws.Cell(lastRow + 1, lastCol + 1).FormulaA1 = string.Format("=SUM({0})", rngTotal.RangeAddress.ToStringFixed());
                    ws.Cell(lastRow + 1, lastCol + 1).Style.NumberFormat.Format = "#,##0";

                    //var excelTableDetails = rngTableDetails.CreateTable();
                    ws.Columns().AdjustToContents(1, lastRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
