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
    public partial class UC_QuanLyNhanSu : UserControl
    {
        private UC_HoSoCaNhan uc_HoSoCaNhan;
        private UC_TrachVu uc_TrachVu;
        private UC_NhomTrachVu uc_NhomTrachVu;
        private UC_IDV uc_IDV;

        public UC_QuanLyNhanSu()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbHoSoCaNhan.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_hscn.png");
                pbTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_trachvu.png");
                pbNhomTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_doituong.png");
                pbIDV.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_QuanLyNhanSu_Load(object sender, EventArgs e)
        {
            LoadPic();

            this.Visible = false;
            pnSelect.Location = SubFunction.SetWidthCenter(this.Size, pnSelect.Size, pnSelect.Top);
        }

        private void NewControls(int i)
        {
            if (i == 0)
            {
                uc_HoSoCaNhan = new UC_HoSoCaNhan();
                this.Controls.Add(uc_HoSoCaNhan);
            }

            if (i == 1)
            {
                uc_TrachVu = new UC_TrachVu();
                this.Controls.Add(uc_TrachVu);
            }

            if (i == 2)
            {
                uc_NhomTrachVu = new UC_NhomTrachVu();
                this.Controls.Add(uc_NhomTrachVu);
            }

            if (i == 3)
            {
                uc_IDV = new UC_IDV();
                this.Controls.Add(uc_IDV);
            }
        }

        private void NewQuanLyNhanSu()
        {
            pnSelect.Visible = true;
            uc_HoSoCaNhan.Visible = false;
            uc_NhomTrachVu.Visible = false;
            uc_TrachVu.Visible = false;
        }



        private void pbHoSoCaNhan_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(0);

            if (!uc_HoSoCaNhan.Visible)
            {
                pnSelect.Visible = true;
            }
        }

        private void pbHoSoCaNhan_MouseEnter(object sender, EventArgs e)
        {
            pbHoSoCaNhan.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_hscn_selected.png");
            lbHoSoCaNhan.ForeColor = Color.Orange;
        }

        private void pbHoSoCaNhan_MouseLeave(object sender, EventArgs e)
        {
            pbHoSoCaNhan.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_hscn.png");
            lbHoSoCaNhan.ForeColor = Color.Gray;
        }

        private void pbTrachVu_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(1);

            if (!uc_TrachVu.Visible)
            {
                pnSelect.Visible = true;
            }
        }

        private void pbTrachVu_MouseEnter(object sender, EventArgs e)
        {
            pbTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_trachvu_selected.png");
            lbTrachVu.ForeColor = Color.Orange;
        }

        private void pbTrachVu_MouseLeave(object sender, EventArgs e)
        {
            pbTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_trachvu.png");
            lbTrachVu.ForeColor = Color.Gray;
        }

        private void pbNhomTrachVu_Click(object sender, EventArgs e)
        {
            //
            pnSelect.Visible = false;

            NewControls(2);
        }

        private void pbNhomTrachVu_MouseEnter(object sender, EventArgs e)
        {
            pbNhomTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_doituong_selected.png");
            lbNhomTrachVu.ForeColor = Color.Orange;
        }

        private void pbNhomTrachVu_MouseLeave(object sender, EventArgs e)
        {
            pbNhomTrachVu.Image = Image.FromFile(@"Resources\NhanSu\icon_qlhoso_doituong.png");
            lbNhomTrachVu.ForeColor = Color.Gray;
        }

        private void pbIDV_Click(object sender, EventArgs e)
        {
            pnSelect.Visible = false;

            NewControls(3);
        }

        private void pbIDV_MouseEnter(object sender, EventArgs e)
        {
            pbIDV.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_selected.png");
            lbIDV.ForeColor = Color.Orange;
        }

        private void pbIDV_MouseLeave(object sender, EventArgs e)
        {
            pbIDV.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma.png");
            lbIDV.ForeColor = Color.Gray;
        }
    }
}
