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
    public partial class UcToolIndex : UserControl
    {
        private UserControl uc;

        public UcToolIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbDB.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_INDEX);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcToolIndex_Load(object sender, EventArgs e)
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

        private void pbDB_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcDB());
        }

        private void pbDB_MouseEnter(object sender, EventArgs e)
        {
            pbDB.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_INDEX_MOUSEOVER);
            lbSanPham.ForeColor = Constant.COLOR_MOUSEOVER;
        }

        private void pbDB_MouseLeave(object sender, EventArgs e)
        {
            pbDB.Image = Image.FromFile(ConstantResource.TOOL_ICON_DB_INDEX);
            lbSanPham.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
