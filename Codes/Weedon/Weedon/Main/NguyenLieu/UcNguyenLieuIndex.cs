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
    public partial class UcNguyenLieuIndex : UserControl
    {
        private UserControl uc;

        public UcNguyenLieuIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_INDEX);
                pbDinhLuong.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_DINHLUONG_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcNguyenLieuIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            this.BringToFront();
        }

        private void pbNguyenLieu_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNguyenLieu());
        }

        private void pbNguyenLieu_MouseEnter(object sender, EventArgs e)
        {
            pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_INDEX_MOUSEOVER);
            lbNguyenLieu.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNguyenLieu_MouseLeave(object sender, EventArgs e)
        {
            pbNguyenLieu.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_NGUYENLIEU_INDEX);
            lbNguyenLieu.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbDinhLuong_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcDinhLuong());
        }

        private void pbDinhLuong_MouseEnter(object sender, EventArgs e)
        {
            pbDinhLuong.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_DINHLUONG_INDEX_MOUSEOVER);
            lbDinhLuong.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbDinhLuong_MouseLeave(object sender, EventArgs e)
        {
            pbDinhLuong.Image = Image.FromFile(ConstantResource.NGUYENLIEU_ICON_DINHLUONG_INDEX);
            lbDinhLuong.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
