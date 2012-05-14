﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class FormExportExcel : Form
    {
        private string sheetName;
        private ListViewEx lvEx;

        public FormExportExcel()
        {
            InitializeComponent();
        }

        public FormExportExcel(string sheetName, ListViewEx lvEx)
        {
            InitializeComponent();

            this.sheetName = sheetName;
            this.lvEx = lvEx;

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
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void FormExportExcel_Load(object sender, EventArgs e)
        {
            LoadResource();
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

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.SaveDialog("SanPham_" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (sPath != null)
            {
                if (Office_Function.ExportListViewData2Excel07(sheetName, sPath, lvEx))
                {
                    MessageBox.Show(Constant.MESSAGE_SUCCESS_EXPORT_EXCEL, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
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
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }
    }
}