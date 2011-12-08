using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;

namespace VNSC
{
    public partial class Form_Print : Form
    {
        public Form_Print()
        {
            InitializeComponent();

            this.ShowDialog();
        }

        public Form_Print(HoSo dto)
        {
            InitializeComponent();

            SetDefaultLabel();

            SetLabel(dto);
            lbHoTen.Location = SubFunction.SetWidthCenter(pnHoTen.Size, lbHoTen.Size, lbHoTen.Top);

            this.ShowDialog();
        }

        private void LoadPic()
        {
            try
            {
                pbPreview.BackgroundImage = Image.FromFile(@"Resources\Export\preview.png");
                pbFooter.BackgroundImage = Image.FromFile(@"Resources\Export\print_footer.png");

                pbHuy.BackgroundImage = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbPrint.BackgroundImage = Image.FromFile(@"Resources\Export\print_next.png");
                pbTiepTuc.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Forward.png");
                pbTroVe.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Back.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void Form_Print_Load(object sender, EventArgs e)
        {
            LoadPic();

            pbTroVe.Location = pbTiepTuc.Location;

            pnLyLich1.Location = new Point(35, 80);
            pnLyLich2.Location = new Point(35, 80);
        }

        private void SetDefaultLabel()
        {
            lbMa.Text = "";
            lbHoTen.Text = "";
            lbNhomTrachVu.Text = "";
            lbTrachVu.Text = "";
            lbCapNhat.Text = "";

            lbNgaySinh.Text = "";
            lbQueQuan.Text = "";
            lbTonGiao.Text = "";
            lbEmail.Text = "";
            lbDiaChi.Text = "";

            lbGioiTinh.Text = "";
            lbTrinhDoHocVan.Text = "";
            lbDienThoaiLienLac.Text = "";

            lbTuyenHua.Text = "";
            lbDonVi.Text = "";
            lbDao.Text = "";

            lbNganh.Text = "";
            lbLienDoan.Text = "";

            lbTrachVuTaiDonVi.Text = "";
            lbTrachVuNgoaiDonVi.Text = "";
            lbTenRung.Text = "";
            lbGhiChu.Text = "";

            lbNgheNghiep.Text = "";
            lbChuyenMon.Text = "";
            lbSoTruong.Text = "";
        }

        private void SetLabel(HoSo dto)
        {
            lbMa.Text = "";
            lbHoTen.Text = dto.HoTen;
            lbNhomTrachVu.Text = dto.NhomTrachVu.Ten;
            lbTrachVu.Text = dto.TrachVu.Ten;
            lbCapNhat.Text = dto.NgayCapNhat;

            lbNgaySinh.Text = dto.NgaySinh.ToString();
            lbQueQuan.Text = dto.QueQuan;
            lbTonGiao.Text = dto.TonGiao;
            lbEmail.Text = dto.Email;
            lbDiaChi.Text = dto.DiaChi;

            lbGioiTinh.Text = dto.GioiTinh;
            lbTrinhDoHocVan.Text = dto.TrinhDoHocVan;
            lbDienThoaiLienLac.Text = dto.DienThoaiLienLac;

            lbTuyenHua.Text = dto.NgayTuyenHua.ToString();
            lbDonVi.Text = dto.DonVi;
            lbDao.Text = dto.Dao;

            lbNganh.Text = dto.Nganh;
            lbLienDoan.Text = dto.LienDoan;

            lbTrachVuTaiDonVi.Text = dto.TrachVuTaiDonVi;
            lbTrachVuNgoaiDonVi.Text = dto.TrachVuNgoaiDonVi;
            lbTenRung.Text = dto.TenRung;
            lbGhiChu.Text = dto.GhiChu;

            lbNgheNghiep.Text = dto.NgheNghiep;
            //lbChuyenMon.Text = "";
            lbSoTruong.Text = dto.SoTruong;
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.BackgroundImage = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.BackgroundImage = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbPrint_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bitmap, this.ClientRectangle);

            Rectangle bounds = new Rectangle(pnLyLich1.Left, pnLyLich1.Top, 380, 540);
            Bitmap bmpCrop = bitmap.Clone(bounds, bitmap.PixelFormat);

            string sPath = File_Function.SaveDialog("JPG file", "jpg");

            if (sPath != null)
            {
                Image_Function.saveJpeg(sPath, bmpCrop, 100);
            }
        }

        private void pbPrint_MouseEnter(object sender, EventArgs e)
        {
            pbPrint.BackgroundImage = Image.FromFile(@"Resources\Export\print_next_selected.png");
        }

        private void pbPrint_MouseLeave(object sender, EventArgs e)
        {
            pbPrint.BackgroundImage = Image.FromFile(@"Resources\Export\print_next.png");
        }

        private void pbTiepTuc_Click(object sender, EventArgs e)
        {
            pnLyLich1.Visible = false;
            pnLyLich2.Visible = true;

            pbTiepTuc.Visible = false;
            pbTroVe.Visible = true;
        }

        private void pbTiepTuc_MouseEnter(object sender, EventArgs e)
        {
            pbTiepTuc.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Forward_selected.png");
        }

        private void pbTiepTuc_MouseLeave(object sender, EventArgs e)
        {
            pbTiepTuc.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Forward.png");
        }

        private void pbTroVe_Click(object sender, EventArgs e)
        {
            pnLyLich1.Visible = true;
            pnLyLich2.Visible = false;

            pbTiepTuc.Visible = true;
            pbTroVe.Visible = false;
        }

        private void pbTroVe_MouseEnter(object sender, EventArgs e)
        {
            pbTroVe.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Back_selected.png");
        }

        private void pbTroVe_MouseLeave(object sender, EventArgs e)
        {
            pbTroVe.BackgroundImage = Image.FromFile(@"Resources\ChucNang\Back.png");
        }
    }
}
