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
    public partial class UcKhoHangIndex : UserControl
    {
        private UserControl uc;

        public UcKhoHangIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbNhapKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_NHAP_INDEX);
                pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_SEARCH_INDEX);
                pbXuatKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_XUAT_KHO_INDEX);
                pbTonKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_HANG_HET_HAN_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcKhoHangIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            InitPermission();
        }

        private void InitPermission()
        {
            if (FormMain.user.IdGroup != Constant.ID_GROUP_ADMIN)
            {
                CommonFunc.NewControl(this.Controls, ref uc, new UcKhoHang());
            }
        }

        private void pbNhapKho_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNhapKho());
        }

        private void pbNhapKho_MouseEnter(object sender, EventArgs e)
        {
            pbNhapKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_NHAP_INDEX_MOUSEOVER);
            lbNhapKho.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNhapKho_MouseLeave(object sender, EventArgs e)
        {
            pbNhapKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_NHAP_INDEX);
            lbNhapKho.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbKhoHang_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcKhoHang());
        }

        private void pbKhoHang_MouseEnter(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_SEARCH_INDEX_MOUSEOVER);
            lbKhoHang.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbKhoHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_SEARCH_INDEX);
            lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbXuatKho_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcXuatKho());
        }

        private void pbXuatKho_MouseEnter(object sender, EventArgs e)
        {
            pbXuatKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_XUAT_KHO_INDEX_MOUSEOVER);
            lbXuatKho.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbXuatKho_MouseLeave(object sender, EventArgs e)
        {
            pbXuatKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_XUAT_KHO_INDEX);
            lbXuatKho.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbTonKho_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcTonKho());
        }

        private void pbTonKho_MouseEnter(object sender, EventArgs e)
        {
            pbTonKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_HANG_HET_HAN_INDEX_MOUSEOVER);
        }

        private void pbTonKho_MouseLeave(object sender, EventArgs e)
        {
            pbTonKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_HANG_HET_HAN_INDEX);
        }
    }
}