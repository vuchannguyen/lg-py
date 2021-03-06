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

namespace QuanLyKinhDoanh.KhachHang
{
    public partial class UcInfo : UserControl
    {
        private DTO.KhachHang data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.KhachHang();
            isUpdate = false;

            if (Init())
            {
                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        public UcInfo(DTO.KhachHang data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (Init())
            {
                tbTen.Text = data.Ten;
                tbDiem.Text = data.TichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);
                tbDiaChi.Text = data.DiaChi;
                tbDienThoai.Text = data.DienThoai;
                tbDTDD.Text = data.DTDD;
                tbFax.Text = data.Fax;
                tbEmail.Text = data.Email;
                tbCMND.Text = data.CMND;
                tbNoiCap.Text = data.NoiCap;
                tbGhiChu.Text = data.GhiChu;

                dtpDOB.Value = data.DOB.HasValue ? data.DOB.Value : DateTime.Now;
                dtpNgayCap.Value = data.NgayCap.HasValue ? data.NgayCap.Value : DateTime.Now;

                cbGioiTinh.Text = data.GioiTinh;
                cbGroup.Text = data.KhachHangGroup.Ten;

                CreateNewIdKH(false);
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

            dtpDOB.CustomFormat = Constant.DEFAULT_DATE_FORMAT;
            dtpNgayCap.CustomFormat = Constant.DEFAULT_DATE_FORMAT;

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            List<DTO.KhachHangGroup> listData = KhachHangGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm khách hàng"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.KhachHangGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void RefreshData()
        {
            tbTen.Text = string.Empty;
            tbDiem.Text = "0";
            tbDiaChi.Text = string.Empty;
            tbDienThoai.Text = string.Empty;
            tbFax.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbCMND.Text = string.Empty;
            tbNoiCap.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            dtpDOB.Value = Constant.DEFAULT_DATE;
            dtpNgayCap.Value = Constant.DEFAULT_DATE;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbGioiTinh.SelectedIndex = 0;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbTen.Text)
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

        private void CreateNewIdKH(bool isChangeId)
        {
            int id = 0;
            DTO.KhachHangGroup SPGroup = KhachHangGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            if (!isChangeId)
            {
                string oldIdNumber = data == null ? string.Empty : data.MaKhachHang.Substring(data.MaKhachHang.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = data == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber);
            }
            else
            {
                string idSanPham = string.Empty;
                DTO.KhachHang dataTemp = KhachHangBus.GetLastData(SPGroup.Id);

                string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaKhachHang.Substring(dataTemp.MaKhachHang.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            }

            tbMa.Text = SPGroup.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

            ValidateInput();
        }

        private void InsertData()
        {
            data.MaKhachHang = tbMa.Text;
            data.Ten = tbTen.Text;
            data.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            data.GioiTinh = cbGioiTinh.Text;
            data.DOB = dtpDOB.Value;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Fax = tbFax.Text;
            data.Email = tbEmail.Text;
            data.CMND = tbCMND.Text;
            data.NoiCap = tbNoiCap.Text;
            data.NgayCap = dtpNgayCap.Value;
            data.TichLuy = ConvertUtil.ConvertToInt(tbDiem.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (KhachHangBus.Insert(data, FormMain.user))
            {
                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR +
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.MaKhachHang = tbMa.Text;
            data.Ten = tbTen.Text;
            data.KhachHangGroup = KhachHangGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));
            data.GioiTinh = cbGioiTinh.Text;
            data.DOB = dtpDOB.Value;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Fax = tbFax.Text;
            data.Email = tbEmail.Text;
            data.CMND = tbCMND.Text;
            data.NoiCap = tbNoiCap.Text;
            data.NgayCap = dtpNgayCap.Value;
            data.TichLuy = ConvertUtil.ConvertToInt(tbDiem.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (KhachHangBus.Update(data, FormMain.user))
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
        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbDiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbDiem_TextChanged(object sender, EventArgs e)
        {
            tbDiem.Text = ConvertUtil.ConvertToInt(tbDiem.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty)).ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void tbDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbDTDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value) == data.IdGroup)
            {
                CreateNewIdKH(false);
            }
            else
            {
                CreateNewIdKH(true);
            }

            if (ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value) == Constant.ID_GROUP_KHACH_THUONG)
            {
                tbDiem.Text = "0";
            }
            else
            {
                tbDiem.Text = data.TichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);
            }
        }
        #endregion
    }
}
