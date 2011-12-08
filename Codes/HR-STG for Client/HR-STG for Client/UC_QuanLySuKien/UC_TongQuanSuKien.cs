using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;
using DTO;
using BUS;

namespace HR_STG_for_Client
{
    public partial class UC_TongQuanSuKien : UserControl
    {
        private UC_HoSoThamDu uc_HoSoThamDu;

        public UC_TongQuanSuKien()
        {
            InitializeComponent();
        }

        private void LoadPic()
        {
            try
            {
                pbThongKeTongQuat.Image = Image.FromFile(@"Resources\SuKien\icon_statistic.png");
                pbEvatar.Image = Image.FromFile(@"Resources\SuKien\photodock.png");

                pbSuKien.Image = Image.FromFile(@"Resources\SuKien\Alarm-Arrow-Right-icon.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void UC_TongQuanSuKien_Load(object sender, EventArgs e)
        {
            LoadPic();

            lbTenSuKien.Text = "";
            lbDiaDiem.Text = "";
            lbDonViToChuc.Text = "";
            lbNhomLoaiHinh.Text = "";
            lbLoaiHinh_Nganh.Text = "";
            lbKhaiMac.Text = "";
            lbBeMac.Text = "";
            lbMoTa.Text = "";

            SuKien_DTO dto_SuKien = SuKien_BUS.LaySuKien();

            pbEvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(dto_SuKien.Avatar));

            lbTenSuKien.Text = dto_SuKien.Ten;
            lbDiaDiem.Text = dto_SuKien.DiaDiem;
            lbDonViToChuc.Text = dto_SuKien.DonViToChuc;
            lbNhomLoaiHinh.Text = dto_SuKien.NhomLoaiHinh;
            lbLoaiHinh_Nganh.Text = dto_SuKien.LoaiHinh + "/ " + dto_SuKien.Nganh;
            lbKhaiMac.Text = dto_SuKien.KhaiMac.ToString("dd/MM/yyyy     hh:mm tt");
            lbBeMac.Text = dto_SuKien.BeMac.ToString("dd/MM/yyyy     hh:mm tt");
            lbMoTa.Text = dto_SuKien.MoTa;
        }

        private void pbSuKien_Click(object sender, EventArgs e)
        {
            try
            {
                this.Controls.Remove(uc_HoSoThamDu);
            }
            catch
            {
                //Khong lam gi het
            }

            uc_HoSoThamDu = new UC_HoSoThamDu();
            this.Controls.Add(uc_HoSoThamDu);
            uc_HoSoThamDu.BringToFront();
        }

        private void pbSuKien_MouseEnter(object sender, EventArgs e)
        {
            pbSuKien.Image = Image.FromFile(@"Resources\SuKien\Alarm-Arrow-Right-icon-selected.png");
        }

        private void pbSuKien_MouseLeave(object sender, EventArgs e)
        {
            pbSuKien.Image = Image.FromFile(@"Resources\SuKien\Alarm-Arrow-Right-icon.png");
        }
    }
}
