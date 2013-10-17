using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace Weedon
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
                pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_GROUP_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcSanPhamIndex_Load(object sender, EventArgs e)
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
                pbNhomSanPham.Location = CommonFunc.SetWidthCenter(pnSelect.Size, pbNhomSanPham.Size, pbNhomSanPham.Top);
                lbNhomSanPham.Location = CommonFunc.SetWidthCenter(pnSelect.Size, lbNhomSanPham.Size, lbNhomSanPham.Top);
            }
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
            CommonFunc.NewControl(this.Controls, ref uc, new UcSanPhamGroup());
        }

        private void pbNhomSanPham_MouseEnter(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_GROUP_INDEX_MOUSEOVER);
            lbNhomSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNhomSanPham_MouseLeave(object sender, EventArgs e)
        {
            pbNhomSanPham.Image = Image.FromFile(ConstantResource.SANPHAM_ICON_SANPHAM_GROUP_INDEX);
            lbNhomSanPham.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
