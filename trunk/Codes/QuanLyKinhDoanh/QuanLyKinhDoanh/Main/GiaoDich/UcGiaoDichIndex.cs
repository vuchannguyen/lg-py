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
                pbNhapKho.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_MUA_INDEX);
                pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcGiaoDich_Load(object sender, EventArgs e)
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
            pbNhapKho.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_MUA_INDEX_MOUSEOVER);
            lbNhapKho.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbNhapKho_MouseLeave(object sender, EventArgs e)
        {
            pbNhapKho.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_MUA_INDEX);
            lbNhapKho.ForeColor = Constant.COLOR_NORMAL;
        }

        private void pbThanhToan_Click(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new QuanLyKinhDoanh.GiaoDich.UcThanhToan());
        }

        private void pbThanhToan_MouseEnter(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN_MOUSEOVER);
            lbThanhToan.ForeColor = Constant.COLOR_IN_USE;
        }

        private void pbThanhToan_MouseLeave(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            lbThanhToan.ForeColor = Constant.COLOR_NORMAL;
        }
    }
}
