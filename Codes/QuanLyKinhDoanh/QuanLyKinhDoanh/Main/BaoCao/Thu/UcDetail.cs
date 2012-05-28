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

namespace QuanLyKinhDoanh.Thu
{
    public partial class UcDetail : UserControl
    {
        private List<DTO.HoaDonDetail> listHoaDonDetail;
        private DTO.User dataUser;
        private DTO.KhachHang dataKH;

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
            lbMaHĐ.Text = string.Empty;
            lbNguoiBan.Text = string.Empty;
            lbKhachHang.Text = string.Empty;
            lbNgayGio.Text = string.Empty;
            lbGhiChu.Text = string.Empty;
            lbTongCK.Text = string.Empty;
            lbTongHD.Text = string.Empty;
        }

        private void LoadData(DTO.HoaDon data)
        {
            listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(data.Id);
            dataUser = data.User;
            dataKH = data.KhachHang;

            lbMaHĐ.Text = data.MaHoaDon;
            lbNguoiBan.Text = dataUser == null ? string.Empty : dataUser.UserName;
            lbKhachHang.Text = dataKH == null ? string.Empty : (dataKH.MaKhachHang + Constant.SYMBOL_LINK_STRING + dataKH.Ten);
            lbNgayGio.Text = data.UpdateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            lbGhiChu.Text = data.GhiChu;

            long totalDiscount = 0;

            foreach (DTO.HoaDonDetail detail in listHoaDonDetail)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(detail.SanPham.Id.ToString());
                lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(detail.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten);

                if (detail.ChietKhau != 0)
                {
                    long money = (detail.ChietKhau * detail.SanPham.GiaBan / 100) * detail.SoLuong;

                    totalDiscount += money;

                    lvi.SubItems.Add(detail.ChietKhau.ToString() + Constant.SYMBOL_DISCOUNT);
                    lvi.SubItems.Add(money.ToString(Constant.DEFAULT_FORMAT_MONEY));
                }
                else
                {
                    lvi.SubItems.Add(string.Empty);
                    lvi.SubItems.Add(string.Empty);
                }

                lvi.SubItems.Add(detail.SoLuong.ToString());
                lvi.SubItems.Add(detail.SanPham.DonViTinh);
                lvi.SubItems.Add(detail.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(detail.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTin.Items.Add(lvi);
            }

            lbTongCK.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private void lbKhachHang_MouseEnter(object sender, EventArgs e)
        {
            if (dataKH != null)
            {
                ttDetail.SetToolTip(lbKhachHang, string.Format(Constant.TOOLTIP_DETAIL_KHACHHANG,
                    dataKH.Ten, dataKH.GioiTinh, dataKH.DOB.Value.ToString(Constant.DEFAULT_DATE_FORMAT),
                    dataKH.CMND, dataKH.DiaChi, dataKH.DienThoai, dataKH.DTDD, dataKH.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
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
    }
}
