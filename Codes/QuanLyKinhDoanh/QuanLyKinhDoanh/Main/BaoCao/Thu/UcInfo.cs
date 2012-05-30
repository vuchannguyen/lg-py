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
        private DTO.HoaDon data;
        private List<DTO.HoaDonDetail> listHoaDonDetail;
        private DTO.User dataUser;
        private DTO.KhachHang dataKH;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.HoaDon();
            isUpdate = false;

            InitThu();
        }

        public UcInfo(DTO.HoaDon data)
        {
            InitializeComponent();

            this.data = data;
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

            if (data.IdType == Constant.ID_TYPE_BAN)
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
        }



        #region Function
        private void InitThu()
        {
            pnInfoTraSP.Visible = false;

            if (isUpdate)
            {
                tbMa.Text = data.MaHoaDon;
                tbTien.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
                tbGhiChu.Text = data.GhiChu;
            }
            else
            {
                RefreshDataThu();
            }
        }

        private void InitTraSP()
        {
            pnInfoThu.Visible = false;

            listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(data.Id);
            dataUser = data.User;
            dataKH = data.KhachHang;

            tbMaHDTraSP.Text = data.MaHoaDon;
            tbNguoiBanTraSP.Text = data.User == null ? string.Empty : data.User.UserName;
            tbKhachHangTraSP.Text = dataKH == null ? string.Empty : dataKH.MaKhachHang + Constant.SYMBOL_LINK_STRING + dataKH.Ten;
            tbGhiChu.Text = data.GhiChu;

            lbNgayGioTraSP.Text = data.UpdateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);

            long totalDiscount = 0;

            foreach (DTO.HoaDonDetail detail in listHoaDonDetail)
            {
                ListViewItem lvi = new ListViewItem();

                if (detail.IsSendBack)
                {
                    lvi.UseItemStyleForSubItems = false;
                    lvi.ForeColor = Color.Red;
                }

                lvi.SubItems.Add(detail.Id.ToString());
                lvi.SubItems.Add((lvThongTinTraSP.Items.Count + 1).ToString());
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

                lvThongTinTraSP.Items.Add(lvi);
            }

            tbTongCKTraSP.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTongHDTraSP.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private void ValidateInputThu()
        {
            if (!string.IsNullOrEmpty(tbTien.Text) &&
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

        private bool UpdateData(DTO.HoaDonDetail data)
        {
            data.SanPham.SoLuong += data.SoLuong;
            data.IsSendBack = true;

            if (!SanPhamBus.Update(data.SanPham, FormMain.user) || !HoaDonDetailBus.Update(data))
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
                foreach (ListViewItem item in lvTraSP.Items)
                {
                    DTO.HoaDonDetail detail = HoaDonDetailBus.GetById(ConvertUtil.ConvertToInt(item.SubItems[1].Text));

                    if (!UpdateData(detail))
                    {
                        MessageBox.Show(Constant.MESSAGE_SEND_BACK_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
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
                foreach (ListViewItem item in lvTraSP.Items)
                {
                    int id = ConvertUtil.ConvertToInt(item.SubItems[1].Text);
                    DTO.HoaDonDetail data = HoaDonDetailBus.GetById(id);

                    if (UpdateData(data))
                    {
                        MessageBox.Show(Constant.MESSAGE_SEND_BACK_SUCCESS, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_DELETE_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
        #endregion

        private void lvThongTinTraSP_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (lvThongTinTraSP.Items[e.Index].ForeColor == Color.Red)
            {
                e.NewValue = e.CurrentValue;
            }
        }
    }
}
