﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyKinhDoanh.Thu;
using QuanLyKinhDoanh.Chi;
using Library;

namespace QuanLyKinhDoanh
{
    public partial class UcThuChiIndex : UserControl
    {
        public UcThuChiIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX);
                pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX);
                pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_LOINHUAN_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcThuChiIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);
        }

        private void pbThu_Click(object sender, EventArgs e)
        {
            this.Controls.Add(new UcThu());
        }

        private void pbThu_MouseEnter(object sender, EventArgs e)
        {
            pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX_MOUSEOVER);
            lbThu.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbThu_MouseLeave(object sender, EventArgs e)
        {
            pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX);
            lbThu.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbChi_Click(object sender, EventArgs e)
        {
            this.Controls.Add(new UcChi());
        }

        private void pbChi_MouseEnter(object sender, EventArgs e)
        {
            pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX_MOUSEOVER);
            lbChi.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbChi_MouseLeave(object sender, EventArgs e)
        {
            pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX);
            lbChi.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbLoiNhuan_Click(object sender, EventArgs e)
        {
            this.Controls.Add(new UcLoiNhuan());
        }

        private void pbLoiNhuan_MouseEnter(object sender, EventArgs e)
        {
            pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_LOINHUAN_INDEX_MOUSEOVER);
            lbLoiNhuan.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbLoiNhuan_MouseLeave(object sender, EventArgs e)
        {
            pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_LOINHUAN_INDEX);
            lbLoiNhuan.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}