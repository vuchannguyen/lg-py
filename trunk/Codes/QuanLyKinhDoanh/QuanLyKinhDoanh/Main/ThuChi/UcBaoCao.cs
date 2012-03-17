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
    public partial class UcBaoCao : UserControl
    {
        public UcBaoCao()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

                pbTraCuu.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEARCH);
                pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
                pbTotalPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);

                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);

                pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX);
                pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcBaoCao_Load(object sender, EventArgs e)
        {
            LoadResource();

            this.BringToFront();
        }

        private void pbThu_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "THU";
        }

        private void pbThu_MouseEnter(object sender, EventArgs e)
        {
            pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX_MOUSEOVER);
        }

        private void pbThu_MouseLeave(object sender, EventArgs e)
        {
            pbThu.Image = Image.FromFile(ConstantResource.THUCHI_ICON_THU_INDEX);
        }

        private void pbChi_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "CHI";
        }

        private void pbChi_MouseEnter(object sender, EventArgs e)
        {
            pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX_MOUSEOVER);
        }

        private void pbChi_MouseLeave(object sender, EventArgs e)
        {
            pbChi.Image = Image.FromFile(ConstantResource.THUCHI_ICON_CHI_INDEX);
        }

        private void pbLoiNhuan_Click(object sender, EventArgs e)
        {

        }

        private void pbLoiNhuan_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pbLoiNhuan_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
