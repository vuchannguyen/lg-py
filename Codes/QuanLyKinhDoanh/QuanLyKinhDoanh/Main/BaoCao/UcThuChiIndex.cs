using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyKinhDoanh.Thu;
using QuanLyKinhDoanh.Chi;
using QuanLyKinhDoanh.CongNo;
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
                pbCongNo.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CONGNO_INDEX);
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

            FormMain.isEditing = false;

            InitPermission();

            this.BringToFront();
        }

        private void InitPermission()
        {
            if (FormMain.user.IdGroup != Constant.ID_GROUP_ADMIN)
            {
                pbLoiNhuan.Visible = false;
                lbLoiNhuan.Visible = false;

                pbThu.Location = CommonFunc.SetWidthCenter(pnSelect.Size, pbThu.Size, pbThu.Top);
                lbThu.Location = CommonFunc.SetWidthCenter(pnSelect.Size, lbThu.Size, lbThu.Top);

                int distance = pbLoiNhuan.Left - pbChi.Left;
                pbChi.Left += distance;
                lbChi.Left += distance;
            }
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
            this.Controls.Add(new UcThuChi());
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

        private void pbCongNo_Click(object sender, EventArgs e)
        {
            this.Controls.Add(new UcCongNo());
        }

        private void pbCongNo_MouseEnter(object sender, EventArgs e)
        {
            pbCongNo.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CONGNO_INDEX_MOUSEOVER);
        }

        private void pbCongNo_MouseLeave(object sender, EventArgs e)
        {
            pbCongNo.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CONGNO_INDEX);
        }
    }
}
