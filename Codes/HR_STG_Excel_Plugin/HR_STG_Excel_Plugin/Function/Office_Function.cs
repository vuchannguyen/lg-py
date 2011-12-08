using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace Function
{
    class Office_Function
    {
        private static COMExcel.Application exApp;
        private static COMExcel.Workbook exBook;

        private static void CloseExcel()
        {
            try
            {
                exBook.Close(false, false, false);
            }
            catch
            {
                //
            }

            try
            {
                exApp.Quit();
            }
            catch
            {
                //
            }

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
            }
            catch
            {
                //
            }

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
            }
            catch
            {
                //
            }
        }

        public static void ExportListViewOnly2Excel(string sPath, ListView lv)
        {
            try
            {
                //lv is the listview control name
                string[] st = new string[lv.Columns.Count];

                StreamWriter sw = new StreamWriter(sPath, false, Encoding.Unicode);
                //sw.AutoFlush = true;

                for (int col = 0; col < lv.Columns.Count; col++)
                {
                    sw.Write("\t" + lv.Columns[col].Text.ToString());
                }

                int rowIndex = 1;
                int row = 0;
                string st1 = "";

                for (row = 0; row < lv.Items.Count; row++)
                {
                    if (rowIndex <= lv.Items.Count)
                    {
                        rowIndex++;
                    }

                    st1 = "\n";

                    for (int col = 0; col < lv.Columns.Count; col++)
                    {
                        st1 = st1 + "\t" + lv.Items[row].SubItems[col].Text.ToString();
                    }

                    sw.WriteLine(st1);
                }

                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static bool ExportGridViewData2Excel07(string sPath, DataGridView dgv)
        {
            try
            {
                // Khởi động chtr Excel
                exApp = new COMExcel.Application();

                // Thêm file temp xls
                exBook = exApp.Workbooks.Add(
                          COMExcel.XlWBATemplate.xlWBATWorksheet);

                // Lấy sheet 1.
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];



                ////Open excel co san
                //string workbookPath = sPath;

                //COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                //        true, false, 0, true, false, false);
                //
                //

                exSheet.Activate();
                exSheet.Name = "Export HRSTG";


                //// Range là ô [1,1] (A1)
                //COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, 1];

                //// Ghi dữ liệu
                //r.Value2 = "Demo excel value";

                //// Giãn cột
                //r.Columns.AutoFit();

                List<int> list_iMaxLength = new List<int>(); //Gia tri max de so sanh AutoFit column

                //Dong va cot de fit
                int iRowFit = 1;
                int iColumnFit = 1;

                for (int iColumn = 0; iColumn < dgv.ColumnCount; iColumn++)
                {
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, iColumn + 1];

                    r.Value2 = dgv.Columns[iColumn].HeaderText;
                    r.BorderAround(COMExcel.XlLineStyle.xlContinuous, COMExcel.XlBorderWeight.xlThin, COMExcel.XlColorIndex.xlColorIndexAutomatic, 1);

                    list_iMaxLength.Add(dgv.Columns[iColumn].HeaderText.Length);
                }

                for (int iColumn = 0; iColumn < dgv.ColumnCount; iColumn++)
                {
                    iRowFit = 1;
                    iColumnFit = 1;

                    for (int iRow = 0; iRow < dgv.RowCount; iRow++)
                    {
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[iRow + 2, iColumn + 1];

                        r.Value2 = dgv[iColumn, iRow].Value.ToString();
                        r.BorderAround(COMExcel.XlLineStyle.xlContinuous, COMExcel.XlBorderWeight.xlThin, COMExcel.XlColorIndex.xlColorIndexAutomatic, 1);

                        //int iLength = dgv[iColumn, iRow].Value.ToString().Length;
                        if (dgv[iColumn, iRow].Value.ToString().Length > list_iMaxLength[iColumn])
                        {
                            list_iMaxLength[iColumn] = dgv[iColumn, iRow].Value.ToString().Length;

                            iRowFit = iRow + 2;
                            iColumnFit = iColumn + 1;
                        }
                    }

                    COMExcel.Range rFit = (COMExcel.Range)exSheet.Cells[iRowFit, iColumnFit];
                    rFit.Columns.AutoFit();
                }


                //// Hiển thị chương trình excel
                //exApp.Visible = true;

                //// Đóng chương trình excel
                //Console.WriteLine("Wait to excel.exe");
                //Console.ReadLine();
                //exApp.Quit();



                // Ẩn chương trình
                exApp.Visible = false;

                // Save file
                exBook.SaveAs(sPath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);



                exBook.Close(false, false, false);
                exApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);

                return true;
            }
            catch
            {
                CloseExcel();

                return false;
            }
        }

        /// <summary>
        /// Export all ListView data
        /// </summary>
        /// <param name="sSheetName"></param>
        /// <param name="sPath"></param>
        /// <param name="lv"></param>
        /// <returns></returns>
        public static bool ExportListViewData2Excel07(string sSheetName, string sPath, ListView lv)
        {
            try
            {
                // Khởi động chtr Excell
                exApp = new COMExcel.Application();

                // Thêm file temp xls
                exBook = exApp.Workbooks.Add(
                          COMExcel.XlWBATemplate.xlWBATWorksheet);

                // Lấy sheet 1.
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];



                ////Open excel co san
                //string workbookPath = sPath;

                //COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                //        true, false, 0, true, false, false);
                //
                //

                exSheet.Activate();
                exSheet.Name = sSheetName;


                //// Range là ô [1,1] (A1)
                //COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, 1];

                //// Ghi dữ liệu
                //r.Value2 = "Demo excel value";

                //// Giãn cột
                //r.Columns.AutoFit();

                List<int> list_iMaxLength = new List<int>(); //Gia tri max de so sanh AutoFit column

                //Dong va cot de fit
                int iRowFit = 1;
                int iColumnFit = 1;

                for (int iColumn = 0; iColumn < lv.Columns.Count; iColumn++)
                {
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, iColumn + 1];

                    r.Font.Bold = true;
                    r.Value2 = lv.Columns[iColumn].Text.ToString();
                    r.BorderAround(COMExcel.XlLineStyle.xlContinuous, COMExcel.XlBorderWeight.xlThin, COMExcel.XlColorIndex.xlColorIndexAutomatic, 1);

                    list_iMaxLength.Add(lv.Columns[iColumn].Text.Length);
                }

                //for (int iRow = 0; iRow < lv.Items.Count; iRow++)
                //{
                //    for (int iColumn = 1; iColumn < lv.Columns.Count; iColumn++)
                //    {
                //        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[iRow + 2, iColumn];

                //        r.Value2 = lv.Items[iRow].SubItems[iColumn].Text.ToString();
                //        //r.Columns.AutoFit();
                //    }
                //}

                for (int iColumn = 0; iColumn < lv.Columns.Count; iColumn++)
                {
                    iRowFit = 1;
                    iColumnFit = iColumn + 1;

                    for (int iRow = 0; iRow < lv.Items.Count; iRow++)
                    {
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[iRow + 2, iColumn + 1];

                        r.Value2 = lv.Items[iRow].SubItems[iColumn].Text.ToString();
                        r.BorderAround(COMExcel.XlLineStyle.xlContinuous, COMExcel.XlBorderWeight.xlThin, COMExcel.XlColorIndex.xlColorIndexAutomatic, 1);

                        //int iLength = dgv[iColumn, iRow].Value.ToString().Length;
                        if (lv.Items[iRow].SubItems[iColumn].Text.Length > list_iMaxLength[iColumn])
                        {
                            list_iMaxLength[iColumn] = lv.Items[iRow].SubItems[iColumn].Text.Length;

                            iRowFit = iRow + 2;
                            iColumnFit = iColumn + 1;
                        }
                    }

                    COMExcel.Range rFit = (COMExcel.Range)exSheet.Cells[iRowFit, iColumnFit];
                    rFit.Columns.AutoFit();
                }


                //// Hiển thị chương trình excel
                //exApp.Visible = true;

                //// Đóng chương trình excel
                //Console.WriteLine("Wait to excel.exe");
                //Console.ReadLine();
                //exApp.Quit();



                // Ẩn chương trình
                exApp.Visible = false;

                // Save file
                exBook.SaveAs(sPath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);



                exBook.Close(false, false, false);
                exApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);

                return true;
            }
            catch
            {
                CloseExcel();

                return false;
            }
        }

        /// <summary>
        /// Export selected items
        /// </summary>
        /// <param name="sSheetName"></param>
        /// <param name="sPath"></param>
        /// <param name="lv">ListView with selectes items</param>
        /// <returns></returns>
        public static bool ExportListViewItems2Excel07(string sSheetName, string sPath, ListView lv)
        {
            try
            {
                exApp = new COMExcel.Application();

                exBook = exApp.Workbooks.Add(
                          COMExcel.XlWBATemplate.xlWBATWorksheet);

                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];

                exSheet.Activate();
                exSheet.Name = sSheetName;

                for (int iColumn = 1; iColumn < lv.Columns.Count; iColumn++)
                {
                    COMExcel.Range r = (COMExcel.Range)exSheet.Cells[1, iColumn];

                    r.Value2 = lv.Columns[iColumn].Text.ToString();
                    r.Columns.AutoFit();
                }

                for (int iRow = 0; iRow < lv.SelectedItems.Count; iRow++)
                {
                    for (int iColumn = 1; iColumn < lv.Columns.Count; iColumn++)
                    {
                        COMExcel.Range r = (COMExcel.Range)exSheet.Cells[iRow + 2, iColumn];

                        r.Value2 = lv.SelectedItems[iRow].SubItems[iColumn].Text.ToString();
                    }
                }

                exApp.Visible = false;

                exBook.SaveAs(sPath, COMExcel.XlFileFormat.xlWorkbookNormal,
                                null, null, false, false,
                                COMExcel.XlSaveAsAccessMode.xlExclusive,
                                false, false, false, false, false);



                exBook.Close(false, false, false);
                exApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);

                return true;
            }
            catch
            {
                CloseExcel();

                return false;
            }
        }

        //void Export2Word()
        //{
        //    //Tạo c c đối tượng application, document, table của MS Word
        //    Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
        //    Microsoft.Office.Interop.Word.Document doc;
        //    Microsoft.Office.Interop.Word.Table table;
        //    //Hiện (mở) ứng dụng word
        //    app.Visible = true;
        //    //Tham số truyền v…o c c h…m c¢ đối l… tuỳ chọn
        //    object obj = Type.Missing;
        //    //Tạo một t…i liệu mới (để chứa dữ liệu xuất ra)
        //    doc = app.Documents.Add(ref obj, ref obj, ref obj, ref obj);
        //    Microsoft.Office.Interop.Word.Range range = doc.Range(ref obj, ref obj);
        //    //Thˆm một bảng c¢ 17 cột v… số h…ng bằng với số h…ng trong datatable.
        //    table = doc.Tables.Add(range, objDataTable.Rows.Count, 17, ref obj, ref obj);
        //    //Xuất dữ liệu từ datatable sang bảng (trong word). Ch£ ý: đối với c c đối tượng tập hợp
        //    // trong word th phần tử đầu tiˆn c¢ chỉ số l… 1 thay v 0 như trong C#
        //    for (int i = 0; i < objDataTable.Rows.Count; i++)
        //    {
        //        doc.Tables[1].Rows[i + 1].Cells[1].Range.Text = objDataTable.Rows[i]["IDlopdat"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[2].Range.Text = objDataTable.Rows[i]["tenloaidat"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[3].Range.Text = objDataTable.Rows[i]["h"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[4].Range.Text = objDataTable.Rows[i]["C"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[5].Range.Text = objDataTable.Rows[i]["γk"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[6].Range.Text = objDataTable.Rows[i]["í"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[7].Range.Text = objDataTable.Rows[i]["a"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[8].Range.Text = objDataTable.Rows[i]["Cc"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[9].Range.Text = objDataTable.Rows[i]["Cà"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[10].Range.Text = objDataTable.Rows[i]["Ch"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[11].Range.Text = objDataTable.Rows[i]["Cr"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[12].Range.Text = objDataTable.Rows[i]["e"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[13].Range.Text = objDataTable.Rows[i]["Es"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[14].Range.Text = objDataTable.Rows[i]["k"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[15].Range.Text = objDataTable.Rows[i]["ν"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[16].Range.Text = objDataTable.Rows[i]["W"].ToString();
        //        doc.Tables[1].Rows[i + 1].Cells[17].Range.Text = objDataTable.Rows[i]["Cv"].ToString();
        //    }
        //    //Thˆm đường viền cho Table nếu cần.
        //    doc.Select();
        //    Microsoft.Office.Interop.Word.WdLineStyle BorderValue = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = BorderValue;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = BorderValue;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderLeft].LineStyle = BorderValue;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderRight].LineStyle = BorderValue;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderVertical].LineStyle = BorderValue;
        //    app.Selection.Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderHorizontal].LineStyle = BorderValue;
        //}
    }
}
