using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Weedon;

namespace QuanLyDoanhSo.Main.DoanhThu.BanHang
{
    public partial class BanHang : Form
    {
        private UserControl uc;

        public BanHang()
        {
            InitializeComponent();
        }

        private void BanHang_Load(object sender, EventArgs e)
        {
            CommonFunc.NewControl(this.Controls, ref uc, new UcBanHang2());
            uc.Dock = DockStyle.Fill;
        }
    }
}
