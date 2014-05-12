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
    public partial class UcNguonCungCapIndex : UserControl
    {
        private UserControl uc;

        public UcNguonCungCapIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbNguonCungCap.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcNguonCungCapIndex_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.DEFAULT_TOP_HEIGHT);

            FormMain.isEditing = false;

            InitPermission();

            this.BringToFront();
        }

        private void InitPermission()
        {
            if (FormMain.user.IdUserGroup != Constant.ID_GROUP_ADMIN)
            {
                CommonFunc.NewControl(this.Controls, ref uc, new UcNguonCungCap());
            }
        }

        private void pbNguonCungCap_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcNguonCungCap());
        }

        private void pbNguonCungCap_MouseEnter(object sender, EventArgs e)
        {
            pbNguonCungCap.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX_MOUSEOVER);
            lbNguonCungCap.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbNguonCungCap_MouseLeave(object sender, EventArgs e)
        {
            pbNguonCungCap.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_KHACHHANG_INDEX);
            lbNguonCungCap.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
