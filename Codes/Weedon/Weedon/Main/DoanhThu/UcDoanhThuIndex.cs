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
    public partial class UcDoanhThuIndex : UserControl
    {
        private UserControl uc;

        public UcDoanhThuIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_INDEX);
                pbHoaDon.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_HOADON_INDEX);
                pbGiaChinhThuc.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_PRICE_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcDoanhThuIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            this.BringToFront();
        }

        private void pbDoanhThu_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcDoanhThu());
        }

        private void pbGiaChinhThuc_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcGiaChinhThuc());
        }

        private void pbHoaDon_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcHoaDon());
        }

        private void pbDoanhThu_MouseEnter(object sender, EventArgs e)
        {
            pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_INDEX_MOUSEOVER);
            lbDoanhThu.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbDoanhThu_MouseLeave(object sender, EventArgs e)
        {
            pbDoanhThu.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_DOANHTHU_INDEX);
            lbDoanhThu.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbHoaDon_MouseEnter(object sender, EventArgs e)
        {
            pbHoaDon.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_HOADON_INDEX_MOUSEOVER);
            lbHoaDon.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbHoaDon_MouseLeave(object sender, EventArgs e)
        {
            pbHoaDon.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_HOADON_INDEX);
            lbHoaDon.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbGiaChinhThuc_MouseEnter(object sender, EventArgs e)
        {
            pbGiaChinhThuc.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_PRICE_INDEX_MOUSEOVER);
            lbGiaChinhThuc.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbGiaChinhThuc_MouseLeave(object sender, EventArgs e)
        {
            pbGiaChinhThuc.Image = Image.FromFile(ConstantResource.DOANHTHU_ICON_PRICE_INDEX);
            lbGiaChinhThuc.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
