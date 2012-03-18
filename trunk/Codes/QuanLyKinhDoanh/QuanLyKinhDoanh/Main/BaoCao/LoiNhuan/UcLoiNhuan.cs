using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;

namespace QuanLyKinhDoanh
{
    public partial class UcLoiNhuan : UserControl
    {
        public UcLoiNhuan()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbStatistic.Image = Image.FromFile(ConstantResource.THUCHI_ICON_UP_MOUSEOVER);
                //pbStatistic.Image = Image.FromFile(ConstantResource.THUCHI_ICON_DOWN_MOUSEOVER);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcLoiNhuan_Load(object sender, EventArgs e)
        {
            LoadResource();

            this.BringToFront();
        }
    }
}
