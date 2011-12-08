using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;

namespace VNSC
{
    public partial class UC_QuanLySuKien : UserControl
    {
        private UC_SuKien uc_SuKien;
        private UC_LoaiHinh uc_LoaiHinh;
        private UC_NhomLoaiHinh uc_NhomLoaiHinh;

        public UC_QuanLySuKien()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbSuKien.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien.png");
                pbLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_loaihinh.png");
                pbNhomLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_khuvuc.png");
                //pbID.Image = Image.FromFile(@"Resources\SuKien\icon_quanlyma.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_QuanLySuKien_Load(object sender, EventArgs e)
        {
            LoadPic();

            this.Visible = false;
            pnSelect.Location = SubFunction.SetWidthCenter(this.Size, pnSelect.Size, pnSelect.Top);
        }

        private void NewControls(int i)
        {
            if (i == 0)
            {
                uc_SuKien = new UC_SuKien();
                this.Controls.Add(uc_SuKien);
            }

            if (i == 1)
            {
                uc_LoaiHinh = new UC_LoaiHinh();
                this.Controls.Add(uc_LoaiHinh);
            }

            if (i == 2)
            {
                uc_NhomLoaiHinh = new UC_NhomLoaiHinh();
                this.Controls.Add(uc_NhomLoaiHinh);
            }
        }

        private void NewQuanLySuKien()
        {
            pnSelect.Visible = true;
            uc_SuKien.Visible = false;
            uc_NhomLoaiHinh.Visible = false;
            uc_LoaiHinh.Visible = false;
        }



        private void pbSuKien_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(0);

            if (!uc_SuKien.Visible)
            {
                pnSelect.Visible = true;
            }
        }

        private void pbSuKien_MouseEnter(object sender, EventArgs e)
        {
            pbSuKien.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien_selected.png");
            lbSuKien.ForeColor = Color.Orange;
        }

        private void pbSuKien_MouseLeave(object sender, EventArgs e)
        {
            pbSuKien.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_sukien.png");
            lbSuKien.ForeColor = Color.Gray;
        }

        private void pbLoaiHinh_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(1);

            if (!uc_LoaiHinh.Visible)
            {
                pnSelect.Visible = true;
            }
        }

        private void pbLoaiHinh_MouseEnter(object sender, EventArgs e)
        {
            pbLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_loaihinh_selected.png");
            lbLoaiHinh.ForeColor = Color.Orange;
        }

        private void pbLoaiHinh_MouseLeave(object sender, EventArgs e)
        {
            pbLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_loaihinh.png");
            lbLoaiHinh.ForeColor = Color.Gray;
        }

        private void pbNhomLoaiHinh_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(2);
        }

        private void pbNhomLoaiHinh_MouseEnter(object sender, EventArgs e)
        {
            pbNhomLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_khuvuc_selected.png");
            lbNhomLoaiHinh.ForeColor = Color.Orange;
        }

        private void pbNhomLoaiHinh_MouseLeave(object sender, EventArgs e)
        {
            pbNhomLoaiHinh.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_khuvuc.png");
            lbNhomLoaiHinh.ForeColor = Color.Gray;
        }
    }
}
