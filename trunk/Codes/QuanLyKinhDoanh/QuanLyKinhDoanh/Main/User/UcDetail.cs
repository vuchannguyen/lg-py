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

namespace QuanLyKinhDoanh.User
{
    public partial class UcDetail : UserControl
    {
        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.User data)
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
            lbGroup.Text = string.Empty;
            lbTen.Text = string.Empty;
            lbGioiTinh.Text = string.Empty;
            lbTenDangNhap.Text = string.Empty;
            lbCMND.Text = string.Empty;
            lbDienThoai.Text = string.Empty;
            lbDTDD.Text = string.Empty;
            lbEmail.Text = string.Empty;
            lbGhiChu.Text = string.Empty;
        }

        private void LoadData(DTO.User data)
        {
            lbGroup.Text = data.UserGroup.Ten;
            lbTen.Text = data.Ten;
            lbGioiTinh.Text = data.GioiTinh;
            lbTenDangNhap.Text = data.UserName;
            lbCMND.Text = data.CMND;
            lbDienThoai.Text = data.DienThoai;
            lbDTDD.Text = data.DTDD;
            lbEmail.Text = data.Email;
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
