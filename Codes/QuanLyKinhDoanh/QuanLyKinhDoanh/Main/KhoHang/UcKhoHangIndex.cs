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
    }
}
