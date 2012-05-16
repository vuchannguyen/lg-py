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
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcLoiNhuan_Load(object sender, EventArgs e)
        {
            LoadResource();

            this.BringToFront();
        }
    }
}
