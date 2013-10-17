using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using DTO;
using BUS;

namespace Weedon.NguyenLieu
{
    public partial class UcDetail : UserControl
    {
        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.NguyenLieu data)
        {
            InitializeComponent();

            Init();

            LoadData(data);
        }

        private void LoadResource()
        {
            try
            {
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcDetail_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);

            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            this.BringToFront();
        }

        private void Init()
        {
            lbMa.Text = string.Empty;
            lbDonVi.Text = string.Empty;
            lbTen.Text = string.Empty;
            lbDonVi.Text = string.Empty;
            lbMoTa.Text = string.Empty;
        }

        private void LoadData(DTO.NguyenLieu data)
        {
            lbMa.Text = data.MaNguyenLieu;
            lbTen.Text = data.Ten;
            lbDonVi.Text = data.DonViTinh;
            lbMoTa.Text = data.MoTa;
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }
    }
}
