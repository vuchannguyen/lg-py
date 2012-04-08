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
    public partial class UcKhoHang : UserControl
    {
        public UcKhoHang()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND);

                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

                //pbTraCuu.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEARCH);
                //pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
                pbTotalPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);

                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcKhoHang_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            //pnQuanLy.Size = new System.Drawing.Size(710, 480);
            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnFind.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            //tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            cbTinhTrang.SelectedIndex = 0;
            cbNhom.SelectedIndex = 0;

            this.BringToFront();

            //cbFilter.SelectedIndex = 0;

            //tbSearch.Text = Constant.SEARCH_NHAPKHO_TIP;

            //RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, 1);
            //SetStatusButtonPage(1);

            this.Visible = true;
        }

        private void pbFind_Click(object sender, EventArgs e)
        {

        }

        private void pbFind_MouseEnter(object sender, EventArgs e)
        {
            pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND_MOUSEOVER);
        }

        private void pbFind_MouseLeave(object sender, EventArgs e)
        {
            pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND);
        }
    }
}
