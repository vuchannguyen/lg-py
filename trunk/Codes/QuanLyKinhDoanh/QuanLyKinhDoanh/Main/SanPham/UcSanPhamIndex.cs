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
    public partial class UcSanPhamIndex : UserControl
    {
        private UserControl uc;

        public UcSanPhamIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_INDEX);
                pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_NHOM_SANPHAM_INDEX);
                pbXuatXu.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_XUATXU_INDEX);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcSanPhamIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);
        }

        private void pbSanPham_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcSanPham());
        }

        private void pbSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_INDEX_MOUSEOVER);
            lbSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_INDEX);
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbNhomSanPham_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNhomSanPham());
        }

        private void pbNhomSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_NHOM_SANPHAM_INDEX_MOUSEOVER);
            lbNhomSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNhomSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_NHOM_SANPHAM_INDEX);
            lbNhomSanPham.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbXuatXu_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcXuatXu());
        }

        private void pbXuatXu_MouseEnter(object sender, EventArgs e)
        {
            pbXuatXu.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_XUATXU_INDEX_MOUSEOVER);
            lbXuatXu.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbXuatXu_MouseLeave(object sender, EventArgs e)
        {
            pbXuatXu.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_XUATXU_INDEX);
            lbXuatXu.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
