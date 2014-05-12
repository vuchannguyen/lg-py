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
    public partial class UcNhapHangIndex : UserControl
    {
        private UserControl uc;

        public UcNhapHangIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbNKNL.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_INDEX);
                pbNhapHang.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTCL_INDEX);
                pbKTNK.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTNK_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcNhapHangIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            this.BringToFront();
        }

        private void pbNKNL_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNhapHang());
        }

        private void pbNKNL_MouseEnter(object sender, EventArgs e)
        {
            pbNKNL.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_INDEX_MOUSEOVER);
            lbNKNL.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNKNL_MouseLeave(object sender, EventArgs e)
        {
            pbNKNL.Image = Image.FromFile(ConstantResource.NKNL_ICON_NKNL_INDEX);
            lbNKNL.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbNhapHang_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcKiemTraChenhLech());
        }

        private void pbNhapHang_MouseEnter(object sender, EventArgs e)
        {
            pbNhapHang.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTCL_INDEX_MOUSEOVER);
            lbNhapHang.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNhapHang_MouseLeave(object sender, EventArgs e)
        {
            pbNhapHang.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTCL_INDEX);
            lbNhapHang.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbKTNK_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcKiemTraNhatKy());
        }

        private void pbKTNK_MouseEnter(object sender, EventArgs e)
        {
            pbKTNK.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTNK_INDEX_MOUSEOVER);
            lbKTNK.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbKTNK_MouseLeave(object sender, EventArgs e)
        {
            pbKTNK.Image = Image.FromFile(ConstantResource.NKNL_ICON_KTNK_INDEX);
            lbKTNK.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
