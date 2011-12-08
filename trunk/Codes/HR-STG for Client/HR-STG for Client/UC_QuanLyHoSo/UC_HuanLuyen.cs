using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HR_STG_for_Client
{
    public partial class UC_HuanLuyen : UserControl
    {
        private int ma;

        public int Ma
        {
            get { return ma; }
            set { ma = value; }
        }

        private string nganh;

        public string Nganh
        {
            get { return nganh; }
            set { nganh = value; }
        }

        private string khoa;

        public string Khoa
        {
            get { return khoa; }
            set { khoa = value; }
        }

        private string tenKhoa;

        public string TenKhoa
        {
            get { return tenKhoa; }
            set { tenKhoa = value; }
        }

        private string khoaTruong;

        public string KhoaTruong
        {
            get { return khoaTruong; }
            set { khoaTruong = value; }
        }

        private DateTime nam;

        public DateTime Nam
        {
            get { return nam; }
            set { nam = value; }
        }

        private string mHL;

        public string MHL
        {
            get { return mHL; }
            set { mHL = value; }
        }

        private string tinhTrang;

        public string TinhTrang
        {
            get { return tinhTrang; }
            set { tinhTrang = value; }
        }



        public UC_HuanLuyen()
        {
            InitializeComponent();

            cbKhoa.SelectedIndex = 1;
        }

        public UC_HuanLuyen(int dMa, string sNganh, string sKhoa, string sTenKhoa, string sKhoaTruong, DateTime dtNam, string sMHL, string sTinhTrang, bool bEdit)
        {
            InitializeComponent();

            ma = dMa;

            if (sNganh == "Ấu")
            {
                rbAu.Checked = true;
            }

            if (sNganh == "Thiếu")
            {
                rbThieu.Checked = true;
            }

            if (sNganh == "Kha")
            {
                rbKha.Checked = true;
            }

            if (sNganh == "Tráng")
            {
                rbTrang.Checked = true;
            }

            if (sNganh == "Khác")
            {
                rbKhac.Checked = true;
            }

            cbKhoa.Text = sKhoa;
            tbTenKhoa.Text = sTenKhoa;
            tbKhoaTruong.Text = sKhoaTruong;
            dtpNam.Value = dtNam;
            tbMHL.Text = sMHL;

            if (sTinhTrang == "Tham dự")
            {
                rbThamDu.Checked = true;
            }
            else
            {
                rbTrungCach.Checked = true;
            }

            if (!bEdit)
            {
                rbAu.Enabled = false;
                rbThieu.Enabled = false;
                rbKha.Enabled = false;
                rbTrang.Enabled = false;
                rbKhac.Enabled = false;

                cbKhoa.Enabled = false;
                tbTenKhoa.ReadOnly = true;
                tbKhoaTruong.ReadOnly = true;
                dtpNam.Enabled = false;
                tbMHL.ReadOnly = true;

                rbThamDu.Enabled = false;
                rbTrungCach.Enabled = false;

                pbDelete.Visible = false;
            }

            nganh = rbThieu.Text;
            khoa = cbKhoa.Text;
            tenKhoa = tbTenKhoa.Text;
            khoaTruong = tbKhoaTruong.Text;
            nam = dtpNam.Value;
            mHL = tbMHL.Text;
            tinhTrang = rbTrungCach.Text;
        }

        private void UC_HuanLuyen_Load(object sender, EventArgs e)
        {
            try
            {
                pbDelete.Image = Image.FromFile(@"Resources\ChucNang\delete.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }

            dtpNam.MaxDate = DateTime.Now;

            nganh = rbThieu.Text;
            khoa = cbKhoa.Text;
            tenKhoa = tbTenKhoa.Text;
            khoaTruong = tbKhoaTruong.Text;
            nam = dtpNam.Value;
            mHL = tbMHL.Text;
            tinhTrang = rbTrungCach.Text;
        }

        private void pbDelete_Click(object sender, EventArgs e)
        {
            Form_Confirm frm_Confirm = new Form_Confirm("Đồng ý xóa Khóa này?");
            if (frm_Confirm.Yes)
            {
                this.Visible = false;
            }
        }

        private void rbAu_CheckedChanged(object sender, EventArgs e)
        {
            nganh = rbAu.Text;
        }

        private void rbThieu_CheckedChanged(object sender, EventArgs e)
        {
            nganh = rbThieu.Text;
        }

        private void rbKha_CheckedChanged(object sender, EventArgs e)
        {
            nganh = rbKha.Text;
        }

        private void rbTrang_CheckedChanged(object sender, EventArgs e)
        {
            nganh = rbTrang.Text;
        }

        private void rbKhac_CheckedChanged(object sender, EventArgs e)
        {
            nganh = rbKhac.Text;
        }

        private void cbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            khoa = cbKhoa.Text;
        }

        private void tbTenKhoa_TextChanged(object sender, EventArgs e)
        {
            tenKhoa = tbTenKhoa.Text;
        }

        private void tbKhoaTruong_TextChanged(object sender, EventArgs e)
        {
            khoaTruong = tbKhoaTruong.Text;
        }

        private void dtpNam_ValueChanged(object sender, EventArgs e)
        {
            nam = dtpNam.Value;
        }

        private void tbMHL_TextChanged(object sender, EventArgs e)
        {
            mHL = tbMHL.Text;
        }

        private void rbThamDu_CheckedChanged(object sender, EventArgs e)
        {
            tinhTrang = rbThamDu.Text;
        }

        private void rbTrungCach_CheckedChanged(object sender, EventArgs e)
        {
            tinhTrang = rbTrungCach.Text;
        }
    }
}
