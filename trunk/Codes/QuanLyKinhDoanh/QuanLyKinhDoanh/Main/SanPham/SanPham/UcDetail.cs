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

namespace QuanLyKinhDoanh.SanPham
{
    public partial class UcDetail : UserControl
    {
        DTO.XuatXu dataXuatXu;

        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.SanPham data)
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
            lbGroup.Text = string.Empty;
            lbTen.Text = string.Empty;
            lbMoTa.Text = string.Empty;
            lbDVT.Text = string.Empty;
            lbSize.Text = string.Empty;
            lbHieu.Text = string.Empty;
            lbXuatXu.Text = string.Empty;
            lbThoiHan.Text = string.Empty;
        }

        private void LoadData(DTO.SanPham data)
        {
            lbMa.Text = data.MaSanPham;
            lbGroup.Text = data.SanPhamGroup.Ten;
            lbTen.Text = data.Ten;
            lbMoTa.Text = data.MoTa;
            lbDVT.Text = data.DonViTinh;
            lbSize.Text = data.Size;
            lbHieu.Text = data.Hieu;
            dataXuatXu = data.XuatXu;
            lbXuatXu.Text = dataXuatXu == null ? string.Empty : dataXuatXu.Ten;
            lbThoiHan.Text = data.ThoiHan == 0 ? string.Empty : (data.ThoiHan.Value.ToString() + " " + data.DonViThoiHan);
        }

        private void lbXuatXu_MouseEnter(object sender, EventArgs e)
        {
            if (dataXuatXu != null)
            {
                ttDetail.SetToolTip(lbXuatXu, string.Format(Constant.TOOLTIP_DETAIL_XUATXU,
                    dataXuatXu.Ten, dataXuatXu.DiaChi, dataXuatXu.DienThoai, dataXuatXu.Fax, dataXuatXu.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
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
