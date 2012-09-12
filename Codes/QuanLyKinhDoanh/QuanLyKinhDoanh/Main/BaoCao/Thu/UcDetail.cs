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
        private DTO.HoaDon dataHD;
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

            this.dataHD = data;
        }

        private void LoadResource()
        {
            try
            {
                pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);
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
            lbNguoiBan.Text = string.Empty;
            lbKhachHang.Text = string.Empty;
            lbNgayGio.Text = string.Empty;
            lbStatusCK.Text = string.Empty;
            lbGhiChu.Text = string.Empty;
            lbTongCK.Text = string.Empty;
            lbTongHD.Text = string.Empty;
            lbTienSuDung.Text = string.Empty;
            lbTienThanhToan.Text = string.Empty;
        }

        private void LoadData(DTO.HoaDon data)
        {
            listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(data.Id);
            dataUser = data.User;
            dataKH = data.KhachHang;

            lbMaHD.Text = data.MaHoaDon;
            lbNguoiBan.Text = dataUser == null ? string.Empty : dataUser.UserName;
            lbKhachHang.Text = dataKH == null ? string.Empty : (dataKH.MaKhachHang + Constant.SYMBOL_LINK_STRING + dataKH.Ten);
            lbNgayGio.Text = data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            lbStatusCK.Text = data.IsCKTichLuy ? Constant.DEFAULT_INDIRECT_DISCOUNT : Constant.DEFAULT_DIRECT_DISCOUNT;
            lbGhiChu.Text = data.GhiChu;

            lvThongTin.Columns[5].Text = data.IsCKTichLuy ? "Điểm CK" : "Tiền CK";

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

            lbTongCK.Text = data.IsCKTichLuy ? totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY) : data.TienChietKhau.ToString(Constant.DEFAULT_FORMAT_MONEY);
            lbTongHD.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
            lbTienSuDung.Text = data.SuDung == 0 ? string.Empty : data.SuDung.ToString(Constant.DEFAULT_FORMAT_MONEY);
            lbTienThanhToan.Text = (data.ThanhTien - data.SuDung) == 0 ? string.Empty :
                (data.ThanhTien - data.SuDung).ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private void lbNguoiBan_MouseEnter(object sender, EventArgs e)
        {
            if (dataUser != null)
            {
                ttDetail.SetToolTip(lbNguoiBan, string.Format(Constant.TOOLTIP_DETAIL_USER,
                    dataUser.Ten, dataUser.GioiTinh, dataUser.UserGroup.Ten, dataUser.UserName,
                    dataUser.CMND, dataUser.DienThoai, dataUser.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }

        private void pbExcel_Click(object sender, EventArgs e)
        {
            ExportBill();
        }

        private void pbExcel_MouseEnter(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL_MOUSEOVER);
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);
        }

        private void ExportBill()
        {
            string path = File_Function.SaveDialog("HoaDon_" + lbMaHD.Text + "_" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (path != null)
            {
                ListView lvInfoNew = new ListView();

                for (int i = 2; i < lvThongTin.Columns.Count; i++)
                {
                    lvInfoNew.Columns.Add((ColumnHeader)lvThongTin.Columns[i].Clone());
                }

                for (int i = 0; i < lvThongTin.Items.Count; i++)
                {
                    ListViewItem lviInfo = new ListViewItem();

                    lviInfo.SubItems[0].Text = lvThongTin.Items[i].SubItems[2].Text; //STT
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[3].Text); //SP
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[4].Text); //CK
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[5].Text); //tien CK
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[6].Text); //SL
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[7].Text); //DVT
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[8].Text); //don gia
                    lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[9].Text); //thanh tien

                    lvInfoNew.Items.Add(lviInfo);
                }

                ExportExcel.InitWorkBook("Hóa đơn bán hàng");
                ExportExcel.CreateSummaryHoaDon(lbMaHD.Text, FormMain.user.Ten, dataKH.Ten,
                    dataHD.CreateDate, lbTongCK.Text, lbTongHD.Text);

                ExportExcel.CreateDetailsTableHoaDon(lvInfoNew);

                ExportExcel.SaveExcel(path);
            }
        }
    }
}
