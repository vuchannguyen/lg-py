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

namespace QuanLyKinhDoanh.Chi
{
    public partial class UcInfo : UserControl
    {
        private DTO.HoaDon data;
        private DTO.KhachHang dataKH;
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
            tbTien.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
            cbMaKH.Text = data.KhachHang == null ? string.Empty : data.KhachHang.MaKhachHang;
            tbGhiChu.Text = data.GhiChu;

            cbMaKH.Enabled = false;
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

            ValidateInput();

            tbTien.Focus();
        }



        #region Function
        private void Init()
        {
            GetListKhachHang();
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            tbTien.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            CreateNewId();
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbTien.Text) &&
                !string.IsNullOrEmpty(tbTenKH.Text) &&
                !string.IsNullOrEmpty(tbGhiChu.Text)
                )
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

        private void CreateNewId()
        {
            int id = 0;

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_CHI);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            tbMa.Text = Constant.PREFIX_CHI + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void InsertData()
        {
            data = new HoaDon();

            data.MaHoaDon = tbMa.Text;
            data.IdUser = FormMain.user.Id;
            data.IdKhachHang = dataKH.Id;
            data.IdType = Constant.ID_TYPE_CHI;
            data.IdStatus = Constant.ID_STATUS_DONE;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Insert(data, FormMain.user))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Hóa đơn " + data.MaHoaDon) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE,
                    Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    this.Dispose();
                }
                else
                {
                    CreateNewId();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_ERROR_DUPLICATE, tbMa.Text) + 
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.KhachHang = dataKH;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Update(data, FormMain.user))
            {
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

        private void GetListKhachHang()
        {
            List<DTO.KhachHang> listData = KhachHangBus.GetList(string.Empty, false, string.Empty, string.Empty, 0, 0);

            cbMaKH.Items.Clear();

            foreach (DTO.KhachHang data in listData)
            {
                cbMaKH.Items.Add(new CommonComboBoxItems(data.MaKhachHang, data.Id));
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
            pbHoanTat.Focus();

            if (dataKH != null && MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!isUpdate)
                {
                    InsertData();
                }
                else
                {
                    UpdateData();
                }
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
        private void tbTien_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            tbTien.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTien.Select(tbTien.Text.Length, 0);

            ValidateInput();
        }

        private void tbTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            ValidateInput();
        }

        private void tbGhiChu_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataKH = KhachHangBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value));

            tbTenKH.Text = dataKH == null ? string.Empty : dataKH.Ten;
        }

        private void cbMaKH_Leave(object sender, EventArgs e)
        {
            if (cbMaKH.SelectedItem != null)
            {
                dataKH = KhachHangBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value));

                if (dataKH != null)
                {
                    tbTenKH.Text = dataKH.Ten;
                }
            }
            else
            {
                dataKH = null;
                tbTenKH.Text = string.Empty;
            }
        }

        private void cbMaKH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbMaKH.Text))
            {
                dataKH = null;
            }
        }

        private void tbTenKH_MouseEnter(object sender, EventArgs e)
        {
            if (dataKH != null)
            {
                ttDetail.SetToolTip(tbTenKH, string.Format(Constant.TOOLTIP_DETAIL_KHACHHANG,
                    dataKH.Ten, dataKH.GioiTinh, dataKH.DOB.Value.ToString(Constant.DEFAULT_DATE_FORMAT),
                    dataKH.CMND, dataKH.DiaChi, dataKH.DienThoai, dataKH.DTDD, dataKH.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }

        private void tbTenKH_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
        #endregion
    }
}
