using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public class ExportExcel
    {
        private static XLWorkbook workbook;
        private static IXLWorksheet ws;

        public static void SaveExcel(string path)
        {
            workbook.SaveAs(path);
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

        public static void CreateSummarySanPham(int soSP)
        {
            ws.Cell("B1").Value = "THỐNG KÊ";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Cell("A3").Value = "Số SP";

            ws.Cell("A4").Value = soSP;

            // From worksheet
            var rngTableSummary = ws.Range("A3:A4");

            var rngHeadersSummary = rngTableSummary.Range("A1:A1"); // The address is relative to rngTable (NOT the worksheet)
            rngHeadersSummary.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            rngHeadersSummary.Style.Font.FontColor = XLColor.White;

            var rngContentSummary = rngTableSummary.Range("A2:A2");
            //rngContentSummary.Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.8);
            rngContentSummary.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            rngContentSummary.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            var excelTableSummary = rngTableSummary.CreateTable();

            ws.Columns().AdjustToContents(3, 8);
        }

        public static void CreateDetailsTableSanPham(ListView lv)
        {
            ws.Cell("B6").Value = "CHI TIẾT";
            ws.Cell("B6").Style.Font.FontSize = 16;
            ws.Cell("B6").Style.Font.Bold = true;

            if (lv.Items.Count > 0)
            {
                int firstRow = 8;
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

                ws.Columns().AdjustToContents(3, 8);
            }
        }

        public static void CreateSummaryKhoHang(int soSP, int tongSoLuong, int soSPHetHan, int tongSoLuongHetHan)
        {
            ws.Cell("B1").Value = "THỐNG KÊ";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Cell("A3").Value = "Số SP";
            ws.Cell("B3").Value = "Tổng số lượng";
            ws.Cell("C3").Value = "Số SP hết hạn";
            ws.Cell("D3").Value = "Tổng số lượng hết hạn";

            ws.Cell("A4").Value = soSP;
            ws.Cell("B4").Value = tongSoLuong;
            ws.Cell("C4").Value = soSPHetHan;

            ws.Cell("C4").Style.Font.Bold = true;
            ws.Cell("C4").Style.Font.FontColor = XLColor.Red;
            ws.Cell("D4").Value = tongSoLuongHetHan;
            ws.Cell("D4").Style.Font.Bold = true;
            ws.Cell("D4").Style.Font.FontColor = XLColor.Red;

            // From worksheet
            var rngTableSummary = ws.Range("A3:D4");

            var rngHeadersSummary = rngTableSummary.Range("A1:D1"); // The address is relative to rngTable (NOT the worksheet)
            rngHeadersSummary.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            rngHeadersSummary.Style.Font.FontColor = XLColor.White;

            var rngContentSummary = rngTableSummary.Range("A2:D2");
            //rngContentSummary.Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.8);
            rngContentSummary.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            rngContentSummary.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

            var excelTableSummary = rngTableSummary.CreateTable();

            ws.Columns().AdjustToContents(3, 8);
        }

        public static void CreateDetailsTableKhoHang(ListView lv)
        {
            ws.Cell("B6").Value = "CHI TIẾT";
            ws.Cell("B6").Style.Font.FontSize = 16;
            ws.Cell("B6").Style.Font.Bold = true;

            if (lv.Items.Count > 0)
            {
                int firstRow = 8;
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

                    if (!string.IsNullOrEmpty(lv.Items[rowNum].SubItems[lastCol - 1].Text))
                    {
                        DateTime usedDay = DateTime.Parse(lv.Items[rowNum].SubItems[lastCol - 1].Text);

                        if (DateTime.Now.AddDays(Constant.DEFAULT_WARNING_DAYS_EXPIRED) >= usedDay &&
                                DateTime.Now.AddDays(-1) <= usedDay)
                        {
                            var rngExpired = ws.Range(firstRow + 1 + rowNum, firstCol, firstRow + 1 + rowNum, lastCol);
                            rngExpired.Style.Font.FontColor = XLColor.Orange;
                        }

                        if (DateTime.Now.AddDays(-1) > usedDay)
                        {
                            var rngExpired = ws.Range(firstRow + 1 + rowNum, firstCol, firstRow + 1 + rowNum, lastCol);
                            rngExpired.Style.Font.FontColor = XLColor.Red;
                        }
                    }
                }

                var rngDataDetails = ws.Range(firstRow + 1, firstCol, lastRow, lastCol);
                rngDataDetails.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngDataDetails.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                var excelTableDetails = rngTableDetails.CreateTable();

                ws.Columns().AdjustToContents(3, 8);
            }
        }

        public static void CreateSummaryHoaDon(string maHD, string tenNV, string tenKH, DateTime ngay, string tongCK, string tongHD)
        {
            ws.Cell("B1").Value = "CỬA HÀNG NGỌC ĐĂNG";
            ws.Cell("B1").Style.Font.FontSize = 16;
            ws.Cell("B1").Style.Font.Bold = true;

            ws.Cell("B2").Value = "Hóa đơn:";
            ws.Cell("B3").Value = "Nhân viên:";
            ws.Cell("B4").Value = "Khách:";

            ws.Cell("C2").Value = maHD;
            ws.Cell("C3").Value = tenNV;
            ws.Cell("C4").Value = tenKH;

            ws.Cell("B2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("B2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Cell("B3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("B3").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Cell("B4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("B4").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            ws.Cell("G2").Value = "Ngày:";
            ws.Cell("G3").Value = "Tổng CK:";
            ws.Cell("G4").Value = "Tổng HĐ:";

            ws.Cell("H2").Value = ngay.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            ws.Cell("H3").Value = tongCK;
            ws.Cell("H4").Value = tongHD;

            ws.Cell("G2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("G2").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Cell("G3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("G3").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Cell("G4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Cell("G4").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            ws.Cell("H3").Style.NumberFormat.Format = "#,###";
            ws.Cell("H4").Style.NumberFormat.Format = "#,###";

            ws.Columns().AdjustToContents(1, 6);
        }

        public static void CreateDetailsTableHoaDon(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                int firstRow = 6;
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
                rngDataDetails.Style.NumberFormat.Format = "#,###";

                var excelTableDetails = rngTableDetails.CreateTable();

                ws.Columns().AdjustToContents(1, firstRow);
            }
        }
    }
}
