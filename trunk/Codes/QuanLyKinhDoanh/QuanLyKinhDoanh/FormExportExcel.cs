using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using BUS;
using System.Diagnostics;
using System.IO;

namespace QuanLyKinhDoanh
{
    public partial class FormExportExcel : Form
    {
        private string typeExport;
        private string sheetName;
        private string fileName;
        private ListViewEx lvEx;

        private int soSP;
        private int tongSoLuong;
        private int soSPHetHan;
        private int tongSoLuongHetHan;

        public FormExportExcel()
        {
            InitializeComponent();

            typeExport = string.Empty;
        }

        public FormExportExcel(string typeExport, string sheetName, string fileName,
            ListViewEx lvEx, int soSP, int tongSoLuong, int soSPHetHan, int tongSoLuongHetHan)
        {
            InitializeComponent();

            this.typeExport = typeExport;

            this.sheetName = sheetName;
            this.fileName = fileName;
            this.lvEx = lvEx;

            this.soSP = soSP;
            this.tongSoLuong = tongSoLuong;
            this.soSPHetHan = soSPHetHan;
            this.tongSoLuongHetHan = tongSoLuongHetHan;

            pnInfo.Controls.Add(this.lvEx);
        }

        public FormExportExcel(string typeExport, string sheetName, string fileName,
            ListViewEx lvEx, int soSP)
        {
            InitializeComponent();

            this.typeExport = typeExport;

            this.sheetName = sheetName;
            this.fileName = fileName;
            this.lvEx = lvEx;

            this.soSP = soSP;

            pnInfo.Controls.Add(this.lvEx);
        }

        private void LoadResource()
        {
            try
            {
                pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                pbTitle.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL_TITLE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void FormExportExcel_Load(object sender, EventArgs e)
        {
            LoadResource();

            lvEx.MouseEnter += new EventHandler(lvEx_MouseEnter);
            lvEx.MouseLeave += new EventHandler(lvEx_MouseLeave);
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL_MOUSEOVER);
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
        }

        private void ExportSanPham()
        {
            ExportExcel.InitWorkBook(sheetName);
            ExportExcel.CreateSummarySanPham(soSP);
            ExportExcel.CreateDetailsTableSanPham(lvEx);
        }

        private void ExportKhoHang()
        {
            ExportExcel.InitWorkBook(sheetName);
            ExportExcel.CreateSummaryKhoHang(soSP, tongSoLuong, soSPHetHan, tongSoLuongHetHan);
            ExportExcel.CreateDetailsTableKhoHang(lvEx);
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Constant.DEFAULT_CLOSEXML_FILE_NAME))
            {
                MessageBox.Show(Constant.DEFAULT_CLOSEXML_FILE_NAME, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!File.Exists(Constant.DEFAULT_OPENXML_FILE_NAME))
            {
                MessageBox.Show(Constant.DEFAULT_OPENXML_FILE_NAME, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string path = File_Function.SaveDialog(fileName + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (path != null)
            {
                try
                {
                    switch (typeExport)
                    {
                        case Constant.DEFAULT_TYPE_EXPORT_SANPHAM:
                            ExportSanPham();
                            break;

                        case Constant.DEFAULT_TYPE_EXPORT_KHOHANG:
                            ExportKhoHang();
                            break;
                    }

                    ExportExcel.SaveExcel(path);

                    if (MessageBox.Show(Constant.MESSAGE_SUCCESS_EXPORT_EXCEL + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_SUCCESS_EXPORT_EXCEL_OPEN,
                        Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Process.Start(path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_EXPORT_EXCEL, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            if (pbHoanTat.Enabled)
            {
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
        }

        private void lvEx_MouseEnter(object sender, EventArgs e)
        {
            lbTip.Visible = true;
        }

        private void lvEx_MouseLeave(object sender, EventArgs e)
        {
            lbTip.Visible = false;
        }
    }
}
