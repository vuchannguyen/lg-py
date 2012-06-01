using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DTO;
using BUS;

namespace QuanLyKinhDoanh.KhachHang
{
    public partial class UcDetail : UserControl
    {
        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.KhachHang data)
        {
            InitializeComponent();

            Init();

            LoadData(data);
        }

        private void LoadResource()
        {
            try
            {
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
                pbBirthDay.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_BITRHDAY);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcDetail_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);

            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            this.BringToFront();
        }

        private void Init()
        {
            lbMa.Text = string.Empty;
            lbGroup.Text = string.Empty;
            lbTen.Text = string.Empty;
            lbGioiTinh.Text = string.Empty;
            lbTichLuy.Text = string.Empty;
            lbDiaChi.Text = string.Empty;
            lbDienThoai.Text = string.Empty;
            lbFax.Text = string.Empty;
            lbDTDD.Text = string.Empty;
            lbEmail.Text = string.Empty;
            lbDOB.Text = string.Empty;
            lbCMND.Text = string.Empty;
            lbNoiCap.Text = string.Empty;
            lbNgayCap.Text = string.Empty;
            lbGhiChu.Text = string.Empty;
        }

        private void LoadData(DTO.KhachHang data)
        {
            lbMa.Text = data.MaKhachHang;
            lbGroup.Text = data.KhachHangGroup.Ten;
            lbTen.Text = data.Ten;
            lbGioiTinh.Text = data.GioiTinh;
            lbTichLuy.Text = data.TichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);
            lbDiaChi.Text = data.DiaChi;
            lbDienThoai.Text = data.DienThoai;
            lbFax.Text = data.Fax;
            lbDTDD.Text = data.DTDD;
            lbEmail.Text = data.Email;
            lbDOB.Text = data.DOB.Value.ToString(Constant.DEFAULT_DATE_FORMAT);

            if (CommonFunc.IsBirthDay(data.DOB.Value, 7))
            {
                lbDOB.ForeColor = Color.Red;

                pbBirthDay.Visible = true;
            }

            lbCMND.Text = data.CMND;
            lbNoiCap.Text = data.NoiCap;
            lbNgayCap.Text = data.NgayCap.Value.ToString(Constant.DEFAULT_DATE_FORMAT);
            lbGhiChu.Text = data.GhiChu;
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
