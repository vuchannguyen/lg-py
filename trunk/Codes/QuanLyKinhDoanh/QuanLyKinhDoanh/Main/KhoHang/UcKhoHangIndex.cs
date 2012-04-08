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
                pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_NHOM_SANPHAM_INDEX);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcKhoHangIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);
        }

        private void pbNhapKho_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNhapKho());
        }

        private void pbNhapKho_MouseEnter(object sender, EventArgs e)
        {
            pbNhapKho.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_NHAP_INDEX_MOUSEOVER);
            lbNhapKho.ForeColor = Constant.COLOR_IN_USE;
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
            lbKhoHang.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbKhoHang_MouseLeave(object sender, EventArgs e)
        {
            pbKhoHang.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_KHOHANG_SEARCH_INDEX);
            lbKhoHang.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbNhomSanPham_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNhomSanPham());
        }

        private void pbNhomSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_NHOM_SANPHAM_INDEX_MOUSEOVER);
            lbNhomSanPham.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbNhomSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.KHOHANG_ICON_NHOM_SANPHAM_INDEX);
            lbNhomSanPham.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
