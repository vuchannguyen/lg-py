using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VNSC
{
    public partial class UC_HoSoThamDu : UserControl
    {
        public UC_HoSoThamDu()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbThem_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
                pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbTraCuu_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");



                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");

                pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_naphoso.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void UC_HoSoThamDu_Load(object sender, EventArgs e)
        {

        }
    }
}
