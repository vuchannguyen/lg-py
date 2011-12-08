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
    public partial class UC_ChiTietSuKien : UserControl
    {
        private int iMaSuKien;

        public UC_ChiTietSuKien()
        {
            InitializeComponent();
        }

        public UC_ChiTietSuKien(int iMa, string sTen)
        {
            InitializeComponent();

            iMaSuKien = iMa;

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "SỰ KIỆN";
            lbTitle.Text = "SỰ KIỆN " + sTen;

            //Tong quan
        }

        private void LoadPic()
        {
            try
            {
                pbHorizonline.Image = Image.FromFile(@"Resources\horizonline.png");
                pbThongKeTongQuat.Image = Image.FromFile(@"Resources\icon_statistic.png");

                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien_title.png");

                pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh.png");
                pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh.png");
                pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien.png");
                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
                pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi.png");
                pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_naphoso.png");

                pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void UC_ChiTietSuKien_Load(object sender, EventArgs e)
        {
            LoadPic();

            pnHeaderAndMainMenu.Location = SubFuntion.SetWidthCenter(this.Size, pnHeaderAndMainMenu.Size, 0);
            pnTotal.Location = new Point(pnHeaderAndMainMenu.Left, pnHeaderAndMainMenu.Bottom + 50);

            pnImageEvent.Location = new Point(pnHeaderAndMainMenu.Right - pbImageEvent.Width - 30, pnTotal.Top);
        }



        private void pbCacQuyDinh_Click(object sender, EventArgs e)
        {

        }

        private void pbCacQuyDinh_MouseEnter(object sender, EventArgs e)
        {
            pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh_selected.png");
            lbCacQuyDinh.ForeColor = Color.Orange;
        }

        private void pbCacQuyDinh_MouseLeave(object sender, EventArgs e)
        {
            pbCacQuyDinh.Image = Image.FromFile(@"Resources\SuKien\button_quydinh.png");
            lbCacQuyDinh.ForeColor = Color.Gray;
        }



        private void pbDonViHanhChanh_Click(object sender, EventArgs e)
        {

        }

        private void pbDonViHanhChanh_MouseEnter(object sender, EventArgs e)
        {
            pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh_selected.png");
            lbDonViHanhChanh.ForeColor = Color.Orange;
        }

        private void pbDonViHanhChanh_MouseLeave(object sender, EventArgs e)
        {
            pbDonViHanhChanh.Image = Image.FromFile(@"Resources\SuKien\button_donvihanhchanh.png");
            lbDonViHanhChanh.ForeColor = Color.Gray;
        }

        private void pbTrachVuSuKien_Click(object sender, EventArgs e)
        {

        }



        private void pbTrachVuSuKien_MouseEnter(object sender, EventArgs e)
        {
            pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien_selected.png");
            lbTrachVuSuKien.ForeColor = Color.Orange;
        }

        private void pbTrachVuSuKien_MouseLeave(object sender, EventArgs e)
        {
            pbTrachVuSuKien.Image = Image.FromFile(@"Resources\SuKien\button_tvsukien.png");
            lbTrachVuSuKien.ForeColor = Color.Gray;
        }



        private void pbHoSoThamDu_Click(object sender, EventArgs e)
        {

        }

        private void pbHoSoThamDu_MouseEnter(object sender, EventArgs e)
        {
            pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_selected.png");
            lbHoSoThamDu.ForeColor = Color.Orange;
        }

        private void pbHoSoThamDu_MouseLeave(object sender, EventArgs e)
        {
            pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
            lbHoSoThamDu.ForeColor = Color.Gray;
        }



        private void pbDieuPhoi_Click(object sender, EventArgs e)
        {

        }

        private void pbDieuPhoi_MouseEnter(object sender, EventArgs e)
        {
            pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_selected.png");
            lbDieuPhoi.ForeColor = Color.Orange;
        }

        private void pbDieuPhoi_MouseLeave(object sender, EventArgs e)
        {
            pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi.png");
            lbDieuPhoi.ForeColor = Color.Gray;
        }



        private void pbChucNangKhac_Click(object sender, EventArgs e)
        {

        }

        private void pbChucNangKhac_MouseEnter(object sender, EventArgs e)
        {
            pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_naphoso_selected.png");
            lbChucNangKhac.ForeColor = Color.Orange;
        }

        private void pbChucNangKhac_MouseLeave(object sender, EventArgs e)
        {
            pbChucNangKhac.Image = Image.FromFile(@"Resources\SuKien\button_naphoso.png");
            lbChucNangKhac.ForeColor = Color.Gray;
        }



        private void pbBrowse_Click(object sender, EventArgs e)
        {

        }

        private void pbBrowse_MouseEnter(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse_selected.png");
        }

        private void pbBrowse_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(@"Resources\ChucNang\icon_browse.png");
        }
    }
}
