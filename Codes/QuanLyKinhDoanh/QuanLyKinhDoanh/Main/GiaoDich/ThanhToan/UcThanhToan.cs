﻿using System;
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

namespace QuanLyKinhDoanh.GiaoDich
{
    public partial class UcThanhToan : UserControl
    {
        private DTO.SanPham dataSP;
        private DTO.KhachHang dataKhachHang;
        private int discount;
        private long totalMoney;

        public UcThanhToan()
        {
            InitializeComponent();

            if (Init())
            {
                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        private void LoadResource()
        {
            try
            {
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
                //pbThanhToan.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_THANHTOAN);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcThanhToan_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetWidthCenter(this.Size, pnInfo.Size, pnInfo.Top);
            pnDetail.Location = CommonFunc.SetWidthCenter(this.Size, pnDetail.Size, pnDetail.Top);

            this.BringToFront();

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            if (!GetListGroupSP())
            {
                return false;
            }

            GetListKhachHang();
            GetListHoaDonStatus();

            return true;
        }

        private void RefreshData()
        {
            discount = 0;
            totalMoney = 0;

            tbGhiChu.Text = string.Empty;

            tbSoLuong.Text = "1";
            tbDVT.Text = string.Empty;
            tbGiaBan.Text = string.Empty;
            tbChietKhau.Text = "0";
            tbThanhTien.Text = string.Empty;

            dtpNgayGio.Value = DateTime.Now;
            dtpNgayGio.CustomFormat = Constant.DEFAULT_DATE_TIME_FORMAT;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbTen.SelectedIndex = cbTen.Items.Count > 0 ? 0 : -1;
            //cbKhachHang.SelectedIndex = cbKhachHang.Items.Count > 0 ? 0 : -1;
            cbStatus.SelectedIndex = cbStatus.Items.Count > 0 ? 0 : -1;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(cbTen.Text) &&
                !string.IsNullOrEmpty(tbSoLuong.Text) &&
                !string.IsNullOrEmpty(tbThanhTien.Text)
                )
            {
                pbAdd.Enabled = true;
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
            }
            else
            {
                pbAdd.Enabled = true;
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_DISABLE);
            }
        }

        private void CalculateMoney()
        {
            long money = 0;

            if (dataSP != null && dataSP.GiaBan != 0)
            {
                money = (dataSP.GiaBan - (dataSP.GiaBan * ConvertUtil.ConvertToInt(tbChietKhau.Text) / 100)) * ConvertUtil.ConvertToInt(tbSoLuong.Text);
            }

            tbThanhTien.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void AddToBill()
        {
            ListViewItem lvi = new ListViewItem();

            lvi.SubItems.Add(dataSP.Id.ToString());
            lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
            lvi.SubItems.Add(dataSP.Ten);
            lvi.SubItems.Add(tbSoLuong.Text);
            lvi.SubItems.Add(tbDVT.Text);
            lvi.SubItems.Add(tbGiaBan.Text);
            lvi.SubItems.Add(tbChietKhau.Text == "0" ? string.Empty : tbChietKhau.Text + Constant.SYMBOL_DISCOUNT);
            lvi.SubItems.Add(tbThanhTien.Text);

            lvThongTin.Items.Add(lvi);

            totalMoney += ConvertUtil.ConvertToLong(dataSP.GiaBan);
            tbTotalMoney.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private bool GetListGroupSP()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Sản Phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void GetListSP()
        {
            int idGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, idGroup, true, string.Empty, string.Empty, 0, 0);

            cbTen.Items.Clear();

            foreach (DTO.SanPham data in listData)
            {
                cbTen.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            if (listData.Count > 0)
            {
                cbTen.SelectedIndex = 0;
            }
        }

        private void GetListKhachHang()
        {
            List<DTO.KhachHang> listData = KhachHangBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            cbMaKH.Items.Clear();

            foreach (DTO.KhachHang data in listData)
            {
                cbMaKH.Items.Add(new CommonComboBoxItems(data.MaKhachHang, data.Id));
            }
        }

        private void GetListHoaDonStatus()
        {
            List<DTO.HoaDonStatus> listData = HoaDonStatusBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            cbStatus.Items.Clear();

            foreach (DTO.HoaDonStatus data in listData)
            {
                cbStatus.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }
        }

        private void GetInfoSP()
        {
            try
            {
                dataSP = SanPhamBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value));

                if (dataSP.GiaBan == 0)
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_MONEY, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                tbDVT.Text = dataSP.DonViTinh;
                tbGiaBan.Text = dataSP.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_NULL_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetDiscount()
        {
            int percent = ChietKhauBus.GetByIdSP(dataSP.Id) == null ? 0 : ChietKhauBus.GetByIdSP(dataSP.Id).Value;

            return percent;
        }
        #endregion



        #region Button
        private void pbAdd_Click(object sender, EventArgs e)
        {
            AddToBill();
        }

        private void pbAdd_MouseEnter(object sender, EventArgs e)
        {
            pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_MOUSEOVER);
        }

        private void pbAdd_MouseLeave(object sender, EventArgs e)
        {
            pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            FormBill frm = new FormBill();
            frm.ShowDialog();
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_DELETE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ids = string.Empty;

                foreach (ListViewItem item in lvThongTin.CheckedItems)
                {
                    long money = ConvertUtil.ConvertToLong(item.SubItems[8].Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

                    totalMoney -= ConvertUtil.ConvertToLong(money);
                    tbTotalMoney.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);

                    lvThongTin.Items.Remove(item);
                }
            }
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_MOUSEROVER);
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
        }
        #endregion



        #region Controls
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetListSP();
        }

        private void cbTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInfoSP();
        }

        private void cbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void cbKhachHang_Leave(object sender, EventArgs e)
        {
            tbChietKhau.Text = discount.ToString();
        }

        private void tbChietKhau_Leave(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(tbChietKhau.Text) < discount)
            {
                tbChietKhau.Text = discount.ToString();
            }
        }

        private void tbChietKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbChietKhau_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbChietKhau.Text))
            {
                tbChietKhau.Text = "0";
            }

            CalculateMoney();
        }

        private void tbGiaBan_TextChanged(object sender, EventArgs e)
        {
            CalculateMoney();
        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSoLuong.Text))
            {
                tbSoLuong.Text = "1";
            }

            CalculateMoney();

            ValidateInput();
        }

        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lvThongTin.SelectedIndices.Count > 0)
            //{
            //    int n = ConvertUtil.ConvertToInt(lvThongTin.SelectedIndices[0]);

            //    lvThongTin.Items[n].Checked = !lvThongTin.Items[n].Checked;
            //}
        }

        private void lvThongTin_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.NewWidth = 30;
                e.Cancel = true;
            }

            if (e.ColumnIndex == 1)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lvThongTin_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0 && lvThongTin.Items.Count > 0)
            {
                bool isChecked = lvThongTin.Items[0].Checked;

                foreach (ListViewItem item in lvThongTin.Items)
                {
                    item.Checked = !isChecked;
                }
            }
        }

        private void lvThongTin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckListViewItemsIsChecked();
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count > 0)
            {
                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
            }
        }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTO.KhachHang data = KhachHangBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value));

            tbTenKH.Text = data == null ? string.Empty : data.Ten;
            tbTichLuy.Text = data.TichLuy.ToString();
            tbSuDung.Text = "0";
        }

        private void tbSoTienThanhToan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbPayMoney_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbPayMoney.Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

            tbPayMoney.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbPayMoney.Select(tbPayMoney.Text.Length, 0);
        }

        private void tbSuDung_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

            tbSuDung.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbSuDung.Select(tbSuDung.Text.Length, 0);
        }

        private void tbPayMoney_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPayMoney.Text))
            {
                tbPayMoney.Text = "0";
            }
        }

        private void tbSuDung_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSuDung.Text))
            {
                tbSuDung.Text = "0";
            }
        }
        #endregion
    }
}
