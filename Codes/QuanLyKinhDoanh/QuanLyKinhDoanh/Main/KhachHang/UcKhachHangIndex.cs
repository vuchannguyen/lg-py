using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh
{
    public partial class UcKhachHangIndex : UserControl
    {
        private UserControl uc;

        public UcKhachHangIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX);
                pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_GROUP_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcKhachHangIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            InitPermission();

            this.BringToFront();
        }

        private void InitPermission()
        {
            //
        }

        private void pbSanPham_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcKhachHang());
        }

        private void pbSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX_MOUSEOVER);
            lbSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX);
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbNhomSanPham_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcKhachHangGroup());
        }

        private void pbNhomSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_GROUP_INDEX_MOUSEOVER);
            lbNhomSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNhomSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_GROUP_INDEX);
            lbNhomSanPham.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
