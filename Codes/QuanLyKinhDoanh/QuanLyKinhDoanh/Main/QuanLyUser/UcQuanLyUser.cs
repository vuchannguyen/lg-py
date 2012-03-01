using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh.Main.QuanLyUser
{
    public partial class UcQuanLyUser : UserControl
    {
        public UcQuanLyUser()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

                pbTraCuu.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            //tbDienGiai_TextChanged(sender, e);

            pnQuanLy.Visible = false;
            pnSelect.Visible = false;

            lbTitle.Text = "THÊM USER";
            lbSelect.Text = "THÊM";

            this.Controls.Add(new UcInfo());
        }

        private void UcQuanLyUser_Load(object sender, EventArgs e)
        {
            LoadResource();

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "";

            pnQuanLy.Size = new System.Drawing.Size(670, 390);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            //tbPage.LostFocus += new EventHandler(tbPage_LostFocus);
        }
    }
}
