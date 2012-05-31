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

namespace QuanLyKinhDoanh.Chi
{
    public partial class UcDetail : UserControl
    {
        private List<DTO.HoaDonDetail> listHoaDonDetail;
        private DTO.User dataUser;

        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.HoaDon data)
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
            lbMaHD.Text = string.Empty;
            lbNguoiNhap.Text = string.Empty;
            lbNgayGio.Text = string.Empty;
            lbGhiChu.Text = string.Empty;
            lbTongHD.Text = string.Empty;
        }

        private void LoadData(DTO.HoaDon data)
        {
            listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(data.Id);
            dataUser = data.User;

            lbMaHD.Text = data.MaHoaDon;
            lbNguoiNhap.Text = dataUser == null ? string.Empty : dataUser.UserName;
            lbNgayGio.Text = data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            lbGhiChu.Text = data.GhiChu;

            foreach (DTO.HoaDonDetail detail in listHoaDonDetail)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(detail.SanPham.Id.ToString());
                lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(detail.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten);
                lvi.SubItems.Add(detail.SoLuong.ToString());
                lvi.SubItems.Add(detail.SanPham.DonViTinh);
                lvi.SubItems.Add(detail.SanPham.GiaMua.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(detail.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(detail.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTin.Items.Add(lvi);
            }

            lbTongHD.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private void lvThongTin_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }

            if (e.ColumnIndex == 1)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lbNguoiNhap_MouseEnter(object sender, EventArgs e)
        {
            if (dataUser != null)
            {
                ttDetail.SetToolTip(lbNguoiNhap, string.Format(Constant.TOOLTIP_DETAIL_USER,
                    dataUser.Ten, dataUser.GioiTinh, dataUser.UserGroup.Ten, dataUser.UserName,
                    dataUser.CMND, dataUser.DienThoai, dataUser.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }
    }
}
