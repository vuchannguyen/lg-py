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

namespace Weedon.NhapHang
{
    public partial class UcDetail : UserControl
    {
        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.NhapHang data)
        {
            InitializeComponent();

            RefreshData();
            AddToBill(data.Id);
            tbMa.Text = data.Id.ToString();
            tbGhiChu.Text = data.GhiChu;
            dtpFilter.Value = data.Date;
            lbNgayGio.Text = dtpFilter.Value.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
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
            pnInfo.Location = CommonFunc.SetWidthCenter(this.Size, pnInfo.Size, pnInfo.Top);
            pnDetail.Location = CommonFunc.SetWidthCenter(this.Size, pnDetail.Size, pnDetail.Top);
            this.BringToFront();
        }



        #region Function
        private void RefreshData()
        {
            tbTong.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
            dgvThongTin.Rows.Clear();
            dtpFilter.Value = DateTime.Now;
            lbNgayGio.Text = dtpFilter.Value.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
        }

        private void AddToBill(int idNhapHang)
        {
            List<DTO.NhapHangChiTiet> listDetail = NhapHangChiTietBus.GetListByIdNhapHang(idNhapHang);

            foreach (DTO.NhapHangChiTiet detail in listDetail)
            {
                int soLuong = detail.SoLuong;
                long price = detail.SanPham.Gia;
                long money = price * soLuong;

                dgvThongTin.Rows.Add(detail.Id, detail.IdSanPham, Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten,
                    price.ToString(Constant.DEFAULT_FORMAT_MONEY), soLuong,
                    money.ToString(Constant.DEFAULT_FORMAT_MONEY));
            }

            CalculateMoney();
        }

        private void CalculateMoney()
        {
            long money = 0;

            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                money += ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            }

            tbTong.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }
        #endregion



        #region Button
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
        #endregion
    }
}