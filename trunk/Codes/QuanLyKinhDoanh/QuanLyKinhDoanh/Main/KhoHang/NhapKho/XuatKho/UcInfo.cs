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

namespace QuanLyKinhDoanh.XuatKho
{
    public partial class UcInfo : UserControl
    {
        private DTO.SanPham data;
        private DTO.XuatXu dataXuatXu;

        public UcInfo()
        {
            InitializeComponent();
        }

        public UcInfo(DTO.SanPham data)
        {
            InitializeComponent();

            Init();

            this.data = data;

            LoadData(data);
        }

        private void LoadResource()
        {
            try
            {
                pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcInfo_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);

            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            this.BringToFront();

            FormMain.isEditing = true;
        }



        #region Function
        private void Init()
        {
            lbMa.Text = string.Empty;
            lbGroup.Text = string.Empty;
            lbTen.Text = string.Empty;
            tbMoTa.Text = string.Empty;
            lbSoLuong.Text = string.Empty;
            lbDVT.Text = string.Empty;
            lbGiaBan.Text = string.Empty;
            lbChietKhau.Text = string.Empty;
            lbSize.Text = string.Empty;
            lbHieu.Text = string.Empty;
            lbXuatXu.Text = string.Empty;
            lbThoiHan.Text = string.Empty;
            lbNgayNhap.Text = string.Empty;
            lbNgayHetHan.Text = string.Empty;
        }

        private void LoadData(DTO.SanPham data)
        {
            numXuat.Maximum = data.SoLuong;

            lbMa.Text = data.MaSanPham;
            lbGroup.Text = data.SanPhamGroup.Ten;
            lbTen.Text = data.Ten;
            tbMoTa.Text = data.MoTa;
            lbSoLuong.Text = data.SoLuong.ToString();
            lbDVT.Text = data.DonViTinh;
            lbGiaBan.Text = data.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY);

            DTO.ChietKhau dataCK = ChietKhauBus.GetByIdSP(data.Id);
            if (dataCK != null)
            {
                lbChietKhau.Text = dataCK.Value == 0 ? string.Empty : (dataCK.Value.ToString() + Constant.SYMBOL_DISCOUNT);
            }

            lbSize.Text = data.Size;
            lbHieu.Text = data.Hieu;

            dataXuatXu = data.XuatXu;
            lbXuatXu.Text = dataXuatXu == null ? string.Empty : dataXuatXu.Ten;
            
            lbThoiHan.Text = data.ThoiHan == 0 ? string.Empty : (data.ThoiHan.Value.ToString() + " " + data.DonViThoiHan);

            DateTime usedDay = data.CreateDate;

            switch (data.DonViThoiHan)
            {
                case Constant.DEFAULT_TYPE_DAY:
                    usedDay = data.CreateDate.AddDays(data.ThoiHan.Value);
                    break;

                case Constant.DEFAULT_TYPE_MONTH:
                    usedDay = data.CreateDate.AddMonths(data.ThoiHan.Value);
                    break;

                case Constant.DEFAULT_TYPE_YEAR:
                    usedDay = data.CreateDate.AddYears(data.ThoiHan.Value);
                    break;

                default:
                    usedDay = data.CreateDate.AddDays(data.ThoiHan.Value);
                    break;
            }

            lbNgayNhap.Text = data.CreateDate.ToString(Constant.DEFAULT_DATE_FORMAT);
            lbNgayHetHan.Text = usedDay.ToString(Constant.DEFAULT_DATE_FORMAT);

            if (data.SoLuong != 0)
            {
                switch (CommonFunc.IsExpired(data.CreateDate, Constant.DEFAULT_WARNING_DAYS_EXPIRED,
                    data.ThoiHan.Value, data.DonViThoiHan))
                {
                    case Constant.DEFAULT_STATUS_USED_DATE_NEAR:
                        lbNgayHetHan.ForeColor = Color.Orange;
                        break;

                    case Constant.DEFAULT_STATUS_USED_DATE_END:
                        lbNgayHetHan.ForeColor = Color.Red;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(data.Avatar))
            {
                pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(data.Avatar));
            }
            else
            {
                pbAvatar.Image = Image.FromFile(ConstantResource.SANPHAM_DEFAULT_SP);
            }
        }

        private void UpdateData()
        {
            data.MoTa = tbMoTa.Text;
            data.SoLuong -= ConvertUtil.ConvertToInt(numXuat.Value);

            if (data.SoLuong == 0)
            {
                data.IsSold = true;
            }

            if (SanPhamBus.Update(data, FormMain.user))
            {
                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
        }
        #endregion



        #region Controls
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
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                UpdateData();
            }
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }

        private void pbHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL_MOUSEOVER);
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
        }

        private void numXuat_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
        #endregion
    }
}
