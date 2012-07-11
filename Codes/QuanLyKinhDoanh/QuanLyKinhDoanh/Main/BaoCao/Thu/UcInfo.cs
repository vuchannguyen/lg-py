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
                pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
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
                ValidateInputTraSP();
                ValidateHoanTatTraSP();

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
            pnInfoTraSP.Visible = false;

            if (isUpdate)
            {
                tbMa.Text = dataHoaDon.MaHoaDon;
                tbTien.Text = dataHoaDon.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

                if (detail.ChietKhau != 0)
                {
                    long money = (detail.ChietKhau * detail.SanPham.GiaBan / 100) * detail.SoLuong;

                    totalDiscount += money;

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
            }

            tbTongCKTraSP.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTongHDTraSP.Text = dataHoaDon.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void RefreshDataThu()
        {
            tbMa.Text = string.Empty;
            tbTien.Text = string.Empty;
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

            ValidateInputTraSP();
            ValidateHoanTatTraSP();
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

        private void ValidateInputTraSP()
        {
            if (lvThongTinTraSP.CheckedItems.Count > 0 ||
                lvTraSP.CheckedItems.Count > 0
                )
            {
                pbTraSP.Enabled = true;
                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
            }
            else
            {
                pbTraSP.Enabled = false;
                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
            }
        }

        private void ValidateHoanTatTraSP()
        {
            if (lvTraSP.Items.Count > 0)
            {
                pbHoanTatTraSP.Enabled = true;
                pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            else
            {
                pbHoanTatTraSP.Enabled = false;
                pbHoanTatTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
            }
        }

        private void SendBack()
        {
            foreach (ListViewItem lvi in lvThongTinTraSP.CheckedItems)
            {
                ListViewItem item = (ListViewItem)lvi.Clone();
                item.Checked = false;
                lvTraSP.Items.Add(item);
                lvThongTinTraSP.Items.Remove(lvi);
            }

            foreach (ListViewItem lvi in lvTraSP.CheckedItems)
            {
                ListViewItem item = (ListViewItem)lvi.Clone();
                item.Checked = false;
                lvThongTinTraSP.Items.Add(item);
                lvTraSP.Items.Remove(lvi);
            }

            for (int i = 0; i < lvThongTinTraSP.Items.Count; i++)
            {
                lvThongTinTraSP.Items[i].SubItems[2].Text = (i + 1).ToString();
            }

            for (int i = 0; i < lvTraSP.Items.Count; i++)
            {
                lvTraSP.Items[i].SubItems[2].Text = (i + 1).ToString();
            }

            pbTraSP.Enabled = false;
            pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
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
            data.IdType = Constant.ID_TYPE_CHI;
            data.IdStatus = Constant.ID_STATUS_DONE;

            long money = 0;
            string maSP = string.Empty;

            foreach (ListViewItem item in lvTraSP.Items)
            {
                money += ConvertUtil.ConvertToLong(item.SubItems[9].Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                string[] sanPham = item.SubItems[3].Text.Split(new string[] { Constant.SYMBOL_LINK_STRING }, StringSplitOptions.RemoveEmptyEntries);
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
            data.SanPham.SoLuong += data.SoLuong;
            data.IsSendBack = true;

            dataHoaDon.GhiChu = tbGhiChuTraSP.Text;

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
            long money = 0;

            foreach (ListViewItem item in lvTraSP.Items)
            {
                money += ConvertUtil.ConvertToLong(item.SubItems[5].Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            }

            dataKH.TichLuy -= money;

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
            if (MessageBox.Show(Constant.MESSAGE_SEND_BACK_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool isSuccess = true;

                foreach (ListViewItem item in lvTraSP.Items)
                {
                    int id = ConvertUtil.ConvertToInt(item.SubItems[1].Text);
                    DTO.HoaDonDetail data = HoaDonDetailBus.GetById(id);

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

                    if (!UpdateDataKH())
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_TICH_LUY, Constant.CAPTION_ERROR,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                e.NewWidth = 30;
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

        private void lvThongTinTraSP_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ValidateInputTraSP();
        }

        private void lvTraSP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0 && lvTraSP.Items.Count > 0)
            {
                bool isChecked = lvTraSP.Items[0].Checked;

                foreach (ListViewItem item in lvTraSP.Items)
                {
                    item.Checked = !isChecked;
                }
            }
        }

        private void lvTraSP_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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

        private void lvTraSP_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ValidateInputTraSP();
        }

        private void pbTraSP_Click(object sender, EventArgs e)
        {
            SendBack();

            ValidateHoanTatTraSP();
        }

        private void pbTraSP_MouseEnter(object sender, EventArgs e)
        {
            pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_MOUSEOVER);
        }

        private void pbTraSP_MouseLeave(object sender, EventArgs e)
        {
            if (pbTraSP.Enabled)
            {
                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
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
    }
}
