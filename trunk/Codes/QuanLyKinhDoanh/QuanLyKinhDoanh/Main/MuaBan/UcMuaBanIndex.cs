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
    public partial class UcMuaBanIndex : UserControl
    {
        private UserControl uc;

        public UcMuaBanIndex()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbMua.Image = Image.FromFile(ConstantResource.MUABAN_ICON_MUA_INDEX);
                pbBan.Image = Image.FromFile(ConstantResource.MUABAN_ICON_BAN_INDEX);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcMuaBan_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnSelect.Location = CommonFunc.SetWidthCenter(this.Size, pnSelect.Size, Constant.TOP_HEIGHT_DEFAULT);
        }

        private void pbMua_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcMua());
        }

        private void pbMua_MouseEnter(object sender, EventArgs e)
        {
            pbMua.Image = Image.FromFile(ConstantResource.MUABAN_ICON_MUA_INDEX_MOUSEOVER);
            lbMua.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbMua_MouseLeave(object sender, EventArgs e)
        {
            pbMua.Image = Image.FromFile(ConstantResource.MUABAN_ICON_MUA_INDEX);
            lbMua.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbBan_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcBan());
        }

        private void pbBan_MouseEnter(object sender, EventArgs e)
        {
            pbBan.Image = Image.FromFile(ConstantResource.MUABAN_ICON_BAN_INDEX_MOUSEOVER);
            lbBan.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbBan_MouseLeave(object sender, EventArgs e)
        {
            pbBan.Image = Image.FromFile(ConstantResource.MUABAN_ICON_BAN_INDEX);
            lbBan.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
