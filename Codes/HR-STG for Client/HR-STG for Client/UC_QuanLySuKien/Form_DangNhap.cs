using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Function;
using System.Xml;
using BUS;
using DTO;

namespace HR_STG_for_Client
{
    public partial class Form_DangNhap : Form
    {
        private bool yes;

        public bool Yes
        {
            get { return yes; }
            set { yes = value; }
        }

        public Form_DangNhap()
        {
            InitializeComponent();

            this.ShowDialog();
        }

        private void LoadPic()
        {
            try
            {
                this.BackgroundImage = Image.FromFile(@"Resources\General\ct_login.jpg");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void Form_DangNhap_Load(object sender, EventArgs e)
        {
            LoadPic();

            lbError.Text = "";

            tbTen.Select();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.OpenDialog("HR-STG config", "cfg");

            if (sPath != null)
            {
                tbBrowse.Text = sPath;

                DAO.XMLConnection.sPathConfig = sPath;

                SuKien_DTO dto_SuKien = SuKien_BUS.LaySuKien();

                if (null == dto_SuKien || null == dto_SuKien.IDS)
                {
                    SubFunction.SetError(lbError, lbError.Location.Y, this.Size, "Tập tin config bị lỗi!");
                }
                else
                {
                    tbIDS.Text = dto_SuKien.IDS;
                }
            }
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            yes = false;

            this.Close();
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            SuKien_DTO dto_SuKien = SuKien_BUS.LaySuKien();
            List<IDV_DTO> list_IDV = IDV_BUS.LayDSIDV();

            if (tbIDS.Text == dto_SuKien.IDS)
            {
                foreach (IDV_DTO Temp in list_IDV)
                {
                    if (tbTen.Text == Temp.IDV && tbMatKhau.Text == Temp.MatKhau)
                    {
                        UC_HoSoThamDu.sIDV = tbTen.Text;

                        yes = true;

                        this.Close();
                    }
                    else
                    {
                        SubFunction.SetError(lbError, lbError.Location.Y, this.Size, "Sai Tên hoặc Mật khẩu!");
                    }
                }
            }
            else
            {
                SubFunction.SetError(lbError, lbError.Location.Y, this.Size, "Sai thông tin IDS sự kiện!");
            }
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }

        private void Enable_pbHoanTat()
        {
            if (tbIDS.Text.Length > 0 && tbTen.Text.Length > 0 && tbMatKhau.Text.Length > 0 && tbBrowse.Text.Length > 0)
            {
                pbHoanTat.Enabled = true;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");
            }
        }

        private void tbBrowse_TextChanged(object sender, EventArgs e)
        {
            Enable_pbHoanTat();
        }

        private void tbIDS_TextChanged(object sender, EventArgs e)
        {
            Enable_pbHoanTat();

            lbError.Text = "";
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            Enable_pbHoanTat();

            lbError.Text = "";
        }

        private void tbMatKhau_TextChanged(object sender, EventArgs e)
        {
            Enable_pbHoanTat();

            lbError.Text = "";
        }
    }
}
