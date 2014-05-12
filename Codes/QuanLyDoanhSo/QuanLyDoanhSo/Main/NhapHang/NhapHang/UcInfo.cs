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

namespace Weedon.HoaDon
{
    public partial class UcInfo : UserControl
    {
        private DTO.HoaDon dataHoaDon;
        string idsHoaDonDetailRemoved;

        public UcInfo()
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

        public UcInfo(DTO.HoaDon data)
        {
            InitializeComponent();

            dataHoaDon = data;

            if (Init())
            {
                RefreshData();
                AddToBill(data.Id);
                tbMaHD.Text = data.Id.ToString();
                tbGhiChu.Text = data.GhiChu;
                dtpFilter.Value = data.Date;
                lbNgayGio.Text = dtpFilter.Value.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);
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
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
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

            pnInfo.Location = CommonFunc.SetWidthCenter(this.Size, pnInfo.Size, pnInfo.Top);
            pnDetail.Location = CommonFunc.SetWidthCenter(this.Size, pnDetail.Size, pnDetail.Top);

            this.BringToFront();

            FormMain.isEditing = true;

            ValidateHoanTat();
        }



        #region Function
        private bool Init()
        {
            GetListSP();

            return true;
        }

        private void RefreshData()
        {
            idsHoaDonDetailRemoved = string.Empty;
            tbTongHoaDon.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
            dgvThongTin.Rows.Clear();
            dtpFilter.Value = DateTime.Now;
            lbNgayGio.Text = dtpFilter.Value.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);

            ValidateHoanTat();
        }

        private void ValidateHoanTat()
        {
            if (dgvThongTin.RowCount > 0)
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

        private void CalculateMoney()
        {
            long money = 0;

            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                money += ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            }

            tbTongHoaDon.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void AddToBill()
        {
            if (lvThongTin.SelectedItems.Count > 0)
            {
                DTO.SanPham data = SanPhamBus.GetById(ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text));
                int soLuong = 1;
                long price = GiaChinhThucBus.GetByIdSanPham(data.Id) == null ? 0 : GiaChinhThucBus.GetByIdSanPham(data.Id).Gia;
                long money = price * soLuong;

                foreach (DataGridViewRow row in dgvThongTin.Rows)
                {
                    int id = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);

                    if (data.Id == id)
                    {
                        soLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value) + 1;
                        price = ConvertUtil.ConvertToLong(row.Cells[colGia.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                        money = price * soLuong;

                        row.Cells[colSoLuong.Name].Value = soLuong;
                        row.Cells[colThanhTien.Name].Value = money.ToString(Constant.DEFAULT_FORMAT_MONEY);

                        CalculateMoney();

                        return;
                    }
                }

                dgvThongTin.Rows.Add(string.Empty, data.Id, data.MaSanPham + Constant.SYMBOL_LINK_STRING + data.Ten,
                    price.ToString(Constant.DEFAULT_FORMAT_MONEY), soLuong,
                    money.ToString(Constant.DEFAULT_FORMAT_MONEY), string.Empty);

                CalculateMoney();
                ValidateHoanTat();
                dgvThongTin.Rows[dgvThongTin.RowCount - 1].Selected = true;
                dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
            }
        }

        private void AddToBill(int idHoaDon)
        {
            List<DTO.HoaDonDetail> listDetail = HoaDonDetailBus.GetListByIdHoaDon(idHoaDon);

            foreach (DTO.HoaDonDetail detail in listDetail)
            {
                int soLuong = detail.SoLuong;
                long price = detail.DonGia;
                long money = price * soLuong;

                dgvThongTin.Rows.Add(detail.Id, detail.IdSanPham, detail.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten,
                    price.ToString(Constant.DEFAULT_FORMAT_MONEY), soLuong,
                    money.ToString(Constant.DEFAULT_FORMAT_MONEY), detail.GhiChu);
            }

            CalculateMoney();
            ValidateHoanTat();
        }

        private void GetListSP()
        {
            List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, 0,
                string.Empty, string.Empty, 0, 0);
            lvThongTin.Items.Clear();

            foreach (DTO.SanPham data in listData)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.MaSanPham + Constant.SYMBOL_LINK_STRING + data.Ten);

                long price = GiaChinhThucBus.GetByIdSanPham(data.Id) == null ? 0 : GiaChinhThucBus.GetByIdSanPham(data.Id).Gia;
                lvi.SubItems.Add(price.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTin.Items.Add(lvi);
            }
        }

        private void InsertData()
        {
            InsertDataHoaDon();
        }

        private bool InsertDataHoaDon()
        {
            DTO.HoaDon data = new DTO.HoaDon();

            data.Date = dtpFilter.Value;
            data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTongHoaDon.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Insert(data, FormMain.user))
            {
                return InsertDataHoaDonDetail(data);
            }
            else
            {
                return false;
            }
        }

        private bool InsertDataHoaDonDetail(DTO.HoaDon dataHoaDon)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.HoaDonDetail data = new DTO.HoaDonDetail();

                data.IdHoaDon = dataHoaDon.Id;
                data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                data.DonGia = ConvertUtil.ConvertToLong(row.Cells[colGia.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                data.SoLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                data.ThanhTien = ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                if (!HoaDonDetailBus.Insert(data, FormMain.user))
                {
                    return false;
                }
            }

            return true;
        }

        private bool UpdateDataHoaDon()
        {
            DTO.HoaDon data = HoaDonBus.GetById(ConvertUtil.ConvertToInt(tbMaHD.Text));

            data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTongHoaDon.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Update(data, FormMain.user))
            {
                return (DeleteDataHoaDonDetail() && UpdateDataHoaDonDetail(data));
            }
            else
            {
                return false;
            }
        }

        private bool UpdateDataHoaDonDetail(DTO.HoaDon dataHoaDon)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells[colId.Name].Value.ToString()))
                {
                    DTO.HoaDonDetail data = HoaDonDetailBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value));

                    data.SoLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                    data.ThanhTien = ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                    if (!HoaDonDetailBus.Update(data, FormMain.user))
                    {
                        return false;
                    }
                }
                else
                {
                    DTO.HoaDonDetail data = new DTO.HoaDonDetail();

                    data.IdHoaDon = dataHoaDon.Id;
                    data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                    data.DonGia = ConvertUtil.ConvertToLong(row.Cells[colGia.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.SoLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                    data.ThanhTien = ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                    if (!HoaDonDetailBus.Insert(data, FormMain.user))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool DeleteDataHoaDonDetail()
        {
            if (!string.IsNullOrEmpty(idsHoaDonDetailRemoved))
            {
                return HoaDonDetailBus.DeleteList(idsHoaDonDetailRemoved, FormMain.user);
            }

            return true;
        }
        #endregion



        #region Button
        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            pbHoanTat.Focus();

            if (MessageBox.Show("Cập nhật hóa đơn?", Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(tbMaHD.Text))
                {
                    if (InsertDataHoaDon())
                    {
                        if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Hóa đơn") + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            this.Dispose();
                        }
                        else
                        {
                            RefreshData();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            this.Dispose();
                        }
                    }
                }
                else
                {
                    if (UpdateDataHoaDon())
                    {
                        MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Hóa đơn"), Constant.CAPTION_INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            this.Dispose();
                        }
                    }
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
        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
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

        private void AddToBillWhenPressEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && lvThongTin.SelectedItems.Count > 0)
            {
                AddToBill();
            }
        }
        #endregion

        private void pbHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                FormMain.isEditing = false;

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

        private void lvThongTin_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvThongTin.SelectedItems.Count > 0)
            {
                AddToBill();
            }
        }

        private void lvThongTin_KeyPress(object sender, KeyPressEventArgs e)
        {
            AddToBillWhenPressEnter(sender, e);
        }

        private void dgvThongTin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvThongTin.Columns[colRemove.Name].Index)
            {
                if (dgvThongTin[colId.Name, e.RowIndex].Value != null && !string.IsNullOrEmpty(dgvThongTin[colId.Name, e.RowIndex].Value.ToString()))
                {
                    idsHoaDonDetailRemoved += dgvThongTin[colId.Name, e.RowIndex].Value + Constant.SEPERATE_STRING;
                }

                dgvThongTin.Rows.RemoveAt(e.RowIndex);
                CalculateMoney();
            }
        }

        private void dgvThongTin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ConvertUtil.ConvertToInt(dgvThongTin[colSoLuong.Name, e.RowIndex].Value) <= 1)
            {
                dgvThongTin[colSoLuong.Name, e.RowIndex].Value = 1;
            }

            int soLuong = ConvertUtil.ConvertToInt(dgvThongTin[colSoLuong.Name, e.RowIndex].Value);
            long price = ConvertUtil.ConvertToLong(dgvThongTin[colGia.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            long money = price * soLuong;

            if (soLuong == 0)
            {
                soLuong = 1;
                dgvThongTin[colSoLuong.Name, e.RowIndex].Value = 1;
            }

            dgvThongTin[colThanhTien.Name, e.RowIndex].Value = price * soLuong;
            ValidateHoanTat();
        }
    }
}