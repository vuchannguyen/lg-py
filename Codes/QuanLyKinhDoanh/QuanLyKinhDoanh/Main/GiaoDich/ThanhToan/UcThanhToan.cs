using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh.GiaoDich
{
    public partial class UcThanhToan : UserControl
    {
        public UcThanhToan()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
                //pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcThanhToan_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetWidthCenter(this.Size, pnInfo.Size, pnInfo.Top);
            pnDetail.Location = CommonFunc.SetWidthCenter(this.Size, pnDetail.Size, pnDetail.Top);

            cbTinhTrang.SelectedIndex = 0;

            this.BringToFront();
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {

        }

        private void pbAdd_MouseEnter(object sender, EventArgs e)
        {
            pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_MOUSEOVER);
        }

        private void pbAdd_MouseLeave(object sender, EventArgs e)
        {
            pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
        }
    }
}
