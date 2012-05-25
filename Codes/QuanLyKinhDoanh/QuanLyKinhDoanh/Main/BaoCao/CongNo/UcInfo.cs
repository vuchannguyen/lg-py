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

namespace QuanLyKinhDoanh.CongNo
{
    public partial class UcInfo : UserControl
    {
        private DTO.HoaDon data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.HoaDon();
            isUpdate = false;

            Init();
            RefreshData();
        }

        public UcInfo(DTO.HoaDon data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            Init();

            tbMa.Text = data.MaHoaDon;
            lbNgayGio.Text = data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            tbKhachHang.Text = data.KhachHang.MaKhachHang + Constant.SYMBOL_LINK_STRING + data.KhachHang.Ten;
            tbConLai.Text = data.ConLai.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTongHD.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbGhiChu.Text = data.GhiChu;

            LoadBill(HoaDonDetailBus.GetListByIdHoaDon(data.Id));
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

            ValidateInput();

            tbThanhToan.Focus();
        }



        #region Function
        private void Init()
        {
            //
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            lbNgayGio.Text = DateTime.Now.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
            tbKhachHang.Text = string.Empty;
            tbConLai.Text = string.Empty;
            tbThanhToan.Text = string.Empty;
            tbTongHD.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            lvThongTin.Items.Clear();
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbThanhToan.Text))
            {
                pbHoanTat.Enabled = true;
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
            }
        }

        private void LoadBill(List<HoaDonDetail> listDetail)
        {
            foreach (HoaDonDetail detail in listDetail)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(detail.Id.ToString());
                lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(detail.SanPham.Ten);
                lvi.SubItems.Add(detail.SoLuong.ToString());
                lvi.SubItems.Add(detail.SanPham.DonViTinh);
                lvi.SubItems.Add(detail.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(detail.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTin.Items.Add(lvi);
            }
        }

        private void UpdateData()
        {
            long money = ConvertUtil.ConvertToLong(tbThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            data.ConLai += money;
            data.GhiChu = tbGhiChu.Text;

            if (data.ConLai == 0)
            {
                data.IdStatus = Constant.ID_STATUS_DONE;
            }

            data.UpdateBy = "";
            data.UpdateDate = DateTime.Now;

            if (HoaDonBus.Update(data))
            {
                if (data.ConLai == 0)
                {
                    MessageBox.Show(Constant.MESSAGE_PAY_ALL_DEBT,
                        Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT,
                    Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
        }
        #endregion



        #region Ok Cancel
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
        #endregion



        #region Controls
        private void tbThanhToan_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            if (money > -data.ConLai)
            {
                money = -data.ConLai;
            }

            tbThanhToan.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbThanhToan.Select(tbThanhToan.Text.Length, 0);

            ValidateInput();
        }

        private void tbThanhToan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            ValidateInput();
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
        #endregion
    }
}
