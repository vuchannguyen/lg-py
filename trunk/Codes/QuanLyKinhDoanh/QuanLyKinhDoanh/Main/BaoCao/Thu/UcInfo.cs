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
    public partial class UcInfo : UserControl
    {
        private DTO.HoaDon dataHoaDon;
        private List<DTO.HoaDonDetail> listHoaDonDetail;
        private DTO.User dataUser;
        private DTO.KhachHang dataKH;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            dataHoaDon = new DTO.HoaDon();
            isUpdate = false;

            InitThu();
        }

        public UcInfo(DTO.HoaDon data)
        {
            InitializeComponent();

            dataHoaDon = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (data.IdType == Constant.ID_TYPE_THU)
            {
                InitThu();
            }

            if (data.IdType == Constant.ID_TYPE_BAN)
            {
                InitTraSP();
            }
        }

        private void LoadResource()
        {
            try
            {
                pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                pbHuyTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
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

            pnInfoThu.Location = CommonFunc.SetCenterLocation(this.Size, pnInfoThu.Size);
            pnInfoTraSP.Location = CommonFunc.SetCenterLocation(this.Size, pnInfoTraSP.Size);

            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            if (dataHoaDon.IdType == Constant.ID_TYPE_BAN)
            {
                tbGhiChuTraSP.Focus();
            }
            else
            {
                ValidateInputThu();

                tbTien.Focus();
            }

            this.BringToFront();

            FormMain.isEditing = true;
        }



        #region Function
        private void InitThu()
        {
            GetListKhachHang();

            pnInfoTraSP.Visible = false;

            if (isUpdate)
            {
                tbMa.Text = dataHoaDon.MaHoaDon;
                tbTien.Text = dataHoaDon.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
                cbMaKH.Text = dataHoaDon.KhachHang == null ? string.Empty : dataHoaDon.KhachHang.MaKhachHang;
                tbGhiChu.Text = dataHoaDon.GhiChu;
            }
            else
            {
                RefreshDataThu();
            }
        }

        private void InitTraSP()
        {
            pnInfoThu.Visible = false;

            listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(dataHoaDon.Id);
            dataUser = dataHoaDon.User;
            dataKH = dataHoaDon.KhachHang;

            tbMaHDTraSP.Text = dataHoaDon.MaHoaDon;
            tbNguoiBanTraSP.Text = dataUser == null ? string.Empty : dataUser.UserName;
            tbKhachHangTraSP.Text = dataKH == null ? string.Empty : dataKH.MaKhachHang + Constant.SYMBOL_LINK_STRING + dataKH.Ten;
            tbGhiChuTraSP.Text = dataHoaDon.GhiChu;

            lbNgayGioTraSP.Text = dataHoaDon.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);

            long totalDiscount = 0;

            foreach (DTO.HoaDonDetail detail in listHoaDonDetail)
            {
                Color color = Color.Black;
                ListViewItem lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;

                if (detail.IsSendBack)
                {
                    color = Color.Red;
                }

                lvi.SubItems.Add(detail.Id.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add((lvThongTinTraSP.Items.Count + 1).ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(detail.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten,
                    color, Color.Transparent, this.Font);

                if (detail.HoaDon.IsCKTongHD)
                {
                    totalDiscount = detail.HoaDon.TienChietKhau;
                    lvi.SubItems.Add(string.Empty);
                    lvi.SubItems.Add(string.Empty);
                }
                else if (detail.ChietKhau != 0)
                {
                    long money = (detail.ChietKhau * detail.SanPham.GiaBan / 100) * detail.SoLuong;

                    if (detail.HoaDon.IsCKTichLuy)
                    {
                        lvThongTinTraSP.Columns[5].Text = "Điểm CK";
                        money = money / Constant.DEFAULT_CHANGE_RATE;
                        totalDiscount += money;
                    }
                    else
                    {
                        lvThongTinTraSP.Columns[5].Text = "Tiền CK";
                        totalDiscount += money;
                    }

                    lvi.SubItems.Add(detail.ChietKhau.ToString() + Constant.SYMBOL_DISCOUNT,
                        color, Color.Transparent, this.Font);
                    lvi.SubItems.Add(money.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        color, Color.Transparent, this.Font);
                }
                else
                {
                    lvi.SubItems.Add(string.Empty);
                    lvi.SubItems.Add(string.Empty);
                }

                lvi.SubItems.Add(detail.SoLuong.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(detail.SanPham.DonViTinh, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(detail.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY),
                    color, Color.Transparent, this.Font);
                lvi.SubItems.Add(detail.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY),
                    color, Color.Transparent, this.Font);

                lvThongTinTraSP.Items.Add(lvi);

                if (!detail.IsSendBack)
                {
                    if (detail.HoaDon.IsCKTichLuy)
                    {
                        dgvTraSP.Rows.Add(lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text,
                            lvi.SubItems[4].Text, 0, 0, lvi.SubItems[7].Text,
                            lvi.SubItems[8].Text, 0);
                    }
                    else
                    {
                        dgvTraSP.Rows.Add(lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text,
                            0, 0, 0, lvi.SubItems[7].Text,
                            lvi.SubItems[8].Text, 0);

                        dgvTraSP.Columns[colCK.Name].Visible = false;
                        dgvTraSP.Columns[colDiemCK.Name].Visible = false;
                    }
                }
            }

            tbTongCKTraSP.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTongHDTraSP.Text = dataHoaDon.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void RefreshDataThu()
        {
            tbMa.Text = string.Empty;
            tbTien.Text = string.Empty;
            cbMaKH.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            CreateNewId();

            ValidateInputThu();
        }

        private void RefreshDataTraSP()
        {
            tbMaHDTraSP.Text = string.Empty;
            tbNguoiBanTraSP.Text = string.Empty;
            tbKhachHangTraSP.Text = string.Empty;
            tbTongCKTraSP.Text = string.Empty;
            tbTongHDTraSP.Text = string.Empty;
            tbGhiChuTraSP.Text = string.Empty;

            lbNgayGioTraSP.Text = string.Empty;
        }

        private void CreateNewId()
        {
            int id = 0;

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_THU);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            tbMa.Text = Constant.PREFIX_THU + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private string CreateNewIdChi()
        {
            int id = 0;

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_CHI);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            return Constant.PREFIX_CHI + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void ValidateInputThu()
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

        private bool ValidateTraSP()
        {
            foreach (DataGridViewRow row in dgvTraSP.Rows)
            {
                if (ConvertUtil.ConvertToInt(row.Cells[colSL.Name].Value.ToString()) > 0)
                {
                    pbHoanTatTraSP.Enabled = true;
                    pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                    return true;
                }
            }

            pbHoanTatTraSP.Enabled = false;
            pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);

            return false;
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

        private void InsertData()
        {
            dataHoaDon = new HoaDon();

            dataHoaDon.MaHoaDon = tbMa.Text;
            dataHoaDon.IdUser = FormMain.user.Id;
            dataHoaDon.IdKhachHang = dataKH.Id;
            dataHoaDon.IdType = Constant.ID_TYPE_THU;
            dataHoaDon.IdStatus = Constant.ID_STATUS_DONE;
            dataHoaDon.ThanhTien = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            dataHoaDon.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Insert(dataHoaDon, FormMain.user))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Hóa đơn " + dataHoaDon.MaHoaDon) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE,
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
            dataHoaDon.KhachHang = dataKH;
            dataHoaDon.ThanhTien = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            dataHoaDon.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Update(dataHoaDon, FormMain.user))
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

        private bool InsertDataHoaDonChi()
        {
            DTO.HoaDon data = new HoaDon();

            data.MaHoaDon = CreateNewIdChi();
            data.IdUser = FormMain.user.Id;
            data.IdKhachHang = dataKH.Id;
            data.IdType = Constant.ID_TYPE_CHI;
            data.IdStatus = Constant.ID_STATUS_DONE;

            long money = 0;
            string maSP = string.Empty;

            foreach (DataGridViewRow row in dgvTraSP.Rows)
            {
                money += ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                string[] sanPham = row.Cells[colSanPham.Name].Value.ToString().Split(new string[] { Constant.SYMBOL_LINK_STRING }, StringSplitOptions.RemoveEmptyEntries);
                maSP += sanPham[0] + " ";
            }

            data.ThanhTien = money;
            data.GhiChu = string.Format(Constant.MESSAGE_SEND_BACK_NOTE, maSP);

            if (!HoaDonBus.Insert(data, FormMain.user))
            {
                return true;
            }

            return true;
        }

        private bool UpdateDataTraSP(DTO.HoaDonDetail data)
        {
            if (!SanPhamBus.Update(data.SanPham, FormMain.user) ||
                !HoaDonDetailBus.Update(data) ||
                !HoaDonBus.Update(dataHoaDon, FormMain.user))
            {
                return false;
            }

            return true;
        }

        private bool UpdateDataKH()
        {
            int discount = 0;

            foreach (DataGridViewRow row in dgvTraSP.Rows)
            {
                discount += ConvertUtil.ConvertToInt(row.Cells[colDiemCK.Name].Value);
            }

            dataKH.TichLuy -= discount;

            if (dataKH.TichLuy < 0)
            {
                dataKH.TichLuy = 0;
            }

            if (!KhachHangBus.Update(dataKH, FormMain.user))
            {
                return false;
            }

            return true;
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

        private void pbHuyTraSP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void pbHuyTraSP_MouseEnter(object sender, EventArgs e)
        {
            pbHuyTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL_MOUSEOVER);
        }

        private void pbHuyTraSP_MouseLeave(object sender, EventArgs e)
        {
            pbHuyTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
        }

        private void pbHoanTatTraSP_Click(object sender, EventArgs e)
        {
            pbHoanTat.Select();

            if (!ValidateTraSP())
            {
                MessageBox.Show(Constant.MESSAGE_SEND_BACK_EMPTY, Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                pbHoanTatTraSP.Enabled = false;
                pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);

                return;
            }

            if (MessageBox.Show(Constant.MESSAGE_SEND_BACK_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool isSuccess = true;
                bool isCKTichLuy = true;

                foreach (DataGridViewRow row in dgvTraSP.Rows)
                {
                    int id = ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value);
                    DTO.HoaDonDetail data = HoaDonDetailBus.GetById(id);

                    isCKTichLuy = data.HoaDon.IsCKTichLuy;
                    data.SanPham.SoLuong += ConvertUtil.ConvertToInt(row.Cells[colSL.Name].Value);
                    data.IsSendBack = true;
                    dataHoaDon.GhiChu = tbGhiChuTraSP.Text;

                    if (!UpdateDataTraSP(data))
                    {
                        isSuccess = false;
                        MessageBox.Show(Constant.MESSAGE_SEND_BACK_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                    }
                }

                if (isSuccess)
                {
                    MessageBox.Show(Constant.MESSAGE_SEND_BACK_SUCCESS, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (isCKTichLuy)
                    {
                        if (!UpdateDataKH())
                        {
                            MessageBox.Show(Constant.MESSAGE_ERROR_TICH_LUY, Constant.CAPTION_ERROR,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (!InsertDataHoaDonChi())
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_INSERT_HD_CHI, Constant.CAPTION_ERROR,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                this.Dispose();
            }
        }

        private void pbHoanTatTraSP_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTatTraSP_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }
        #endregion



        #region Controls
        private void tbTien_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            tbTien.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTien.Select(tbTien.Text.Length, 0);

            ValidateInputThu();
        }

        private void tbTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            ValidateInputThu();
        }

        private void tbGhiChu_TextChanged(object sender, EventArgs e)
        {
            ValidateInputThu();
        }

        private void lvThongTinTraSP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0 && lvThongTinTraSP.Items.Count > 0)
            {
                bool isChecked = lvThongTinTraSP.Items[0].Checked;

                foreach (ListViewItem item in lvThongTinTraSP.Items)
                {
                    item.Checked = !isChecked;
                }
            }
        }

        private void lvThongTinTraSP_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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

        private void lvThongTinTraSP_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvThongTinTraSP.Items[e.Index].SubItems[1].ForeColor == Color.Red)
            {
                e.NewValue = e.CurrentValue;
            }
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
            ValidateInputThu();
        }
        #endregion

        private void dgvTraSP_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colSL.Index)
            {
                return;
            }

            if (ConvertUtil.ConvertToInt(dgvTraSP[colSL.Name, e.RowIndex].Value) >
                ConvertUtil.ConvertToInt(lvThongTinTraSP.Items[e.RowIndex].SubItems[6].Text))
            {
                dgvTraSP[colSL.Name, e.RowIndex].Value = lvThongTinTraSP.Items[e.RowIndex].SubItems[6].Text;
            }
            else if (ConvertUtil.ConvertToInt(dgvTraSP[colSL.Name, e.RowIndex].Value) <= 0)
            {
                dgvTraSP[colSL.Name, e.RowIndex].Value = 0;
            }

            long money = ConvertUtil.ConvertToLong(dgvTraSP[colDonGia.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            if (dgvTraSP.Columns[colDiemCK.Name].Visible)
            {
                dgvTraSP[colDiemCK.Name, e.RowIndex].Value = (ConvertUtil.ConvertToInt(dgvTraSP[colCK.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_DISCOUNT, string.Empty)) *
                    money / Constant.DEFAULT_CHANGE_RATE / Constant.DEFAULT_CHANGE_RATE) * ConvertUtil.ConvertToInt(dgvTraSP[colSL.Name, e.RowIndex].Value);
            }

            dgvTraSP[colThanhTien.Name, e.RowIndex].Value = money * ConvertUtil.ConvertToInt(dgvTraSP[colSL.Name, e.RowIndex].Value);

            pbHoanTatTraSP.Enabled = false;
            pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);

            ValidateTraSP();
        }
    }
}
