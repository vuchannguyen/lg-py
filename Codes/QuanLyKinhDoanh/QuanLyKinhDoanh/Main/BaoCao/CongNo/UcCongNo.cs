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

namespace QuanLyKinhDoanh.CongNo
{
    public partial class UcCongNo : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;
        private string sortColumn;
        private string sortOrder;

        private DTO.SanPham dataSP;

        public UcCongNo()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbThanhToan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_PAY_DEBT_DISABLE);
                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);

                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

                pbTraCuu.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEARCH);
                pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
                pbTotalPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);

                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcThu_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            this.BringToFront();

            sortColumn = string.Empty;
            sortOrder = Constant.SORT_ASCENDING;

            cbFilter.SelectedIndex = 0;

            tbSearch.Text = Constant.SEARCH_CONGNO_TIP;

            RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);

            this.Visible = true;
        }



        #region Function
        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_CONGNO_TIP;

            RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));

            if (tbSearch.Focused)
            {
                tbSearch.Text = string.Empty;
            }

            FormMain.isEditing = false;
        }

        private int GetTotalPage(int total)
        {
            if (total % row == 0)
            {
                return total / row;
            }
            else
            {
                return (total / row) + 1;
            }
        }

        private void RefreshListView(string text, int type, int status, int idKH, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            if (text == Constant.SEARCH_CONGNO_TIP)
            {
                text = string.Empty;
            }

            int total = HoaDonBus.GetCount(text, type, status, idKH, timeType, date);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.HoaDon> listTotal = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                string.Empty, string.Empty, 0, 0);
            long totalMoney = 0;

            foreach (DTO.HoaDon data in listTotal)
            {
                totalMoney += data.ThanhTien;
            }

            tbTong.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);

            List<DTO.HoaDon> list = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.HoaDon data in list)
            {
                Color color = Color.Black;
                ListViewItem lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;

                if (data.CreateDate.AddMonths(1) < DateTime.Now)
                {
                    color = Color.Red;
                }

                lvi.SubItems.Add(data.Id.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.MaHoaDon.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.User == null ? string.Empty : data.User.UserName.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.KhachHang == null ? string.Empty :
                    data.KhachHang.MaKhachHang.ToString() + Constant.SYMBOL_LINK_STRING + data.KhachHang.Ten, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.GhiChu, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.ConLai.ToString(Constant.DEFAULT_FORMAT_MONEY), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY), color, Color.Transparent, this.Font);

                lvThongTin.Items.Add(lvi);
            }

            CheckListViewItemsIsChecked();
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count > 0)
            {
                if (lvThongTin.CheckedItems.Count == 1)
                {
                    pbTraSP.Enabled = true;
                    pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
                }
                else
                {
                    pbTraSP.Enabled = false;
                    pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
                }

                pbThanhToan.Enabled = true;
                pbThanhToan.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_PAY_DEBT);
            }
            else
            {
                pbThanhToan.Enabled = false;
                pbThanhToan.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_PAY_DEBT_DISABLE);
                pbTraSP.Enabled = false;
                pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
            }
        }

        private void SetStatusButtonPage(int iPage)
        {
            if (ConvertUtil.ConvertToInt(lbPage.Text) == 1)
            {
                pbBackPage.Enabled = false;
                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_DISABLE);
            }
            else
            {
                pbBackPage.Enabled = true;
                pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
            }

            if (ConvertUtil.ConvertToInt(lbPage.Text) == ConvertUtil.ConvertToInt(lbTotalPage.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                pbNextPage.Enabled = false;
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_DISABLE);
            }
            else
            {
                pbNextPage.Enabled = true;
                pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
        }

        private bool UpdateData(DTO.HoaDon data)
        {
            data.ConLai = 0;
            data.IdStatus = Constant.ID_STATUS_DONE;

            if (data.IsCKTichLuy)
            {
                long totalDiscount = 0;

                foreach (DTO.HoaDonDetail detail in HoaDonDetailBus.GetListByIdHoaDon(data.Id))
                {
                    if (detail.ChietKhau != 0)
                    {
                        totalDiscount += (detail.ChietKhau * detail.SanPham.GiaBan / 100) * detail.SoLuong;
                    }
                }

                data.KhachHang.TichLuy += totalDiscount / 100;
            }

            if (HoaDonBus.Update(data, FormMain.user))
            {
                return true;
            }

            return false;
        }
        #endregion



        #region Thêm Xóa Sửa
        private void pbThanhToan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM_PAY_DEBT, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ids = string.Empty;
                bool isSuccess = true;

                foreach (ListViewItem item in lvThongTin.CheckedItems)
                {
                    int id = ConvertUtil.ConvertToInt(item.SubItems[1].Text);
                    DTO.HoaDon data = HoaDonBus.GetById(id);

                    if (!UpdateData(data))
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        isSuccess = false;
                        break;
                    }
                }

                if (isSuccess)
                {
                    MessageBox.Show(Constant.MESSAGE_PAY_ALL_DEBT, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void pbThanhToan_MouseEnter(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_PAY_DEBT_MOUSEOVER);
        }

        private void pbThanhToan_MouseLeave(object sender, EventArgs e)
        {
            pbThanhToan.Image = Image.FromFile(ConstantResource.KHACHHANG_ICON_PAY_DEBT);
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

        private void pbTraSP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_SEND_BACK_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int id = ConvertUtil.ConvertToInt(lvThongTin.CheckedItems[0].SubItems[1].Text);
                DTO.HoaDon data = HoaDonBus.GetById(id);

                List<DTO.HoaDonDetail> listDetail = HoaDonDetailBus.GetListByIdHoaDon(data.Id);

                foreach (DTO.HoaDonDetail detail in listDetail)
                {
                    if (!UpdateData(detail))
                    {
                        MessageBox.Show(Constant.MESSAGE_SEND_BACK_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }

                if (HoaDonBus.Delete(data, FormMain.user))
                {
                    MessageBox.Show(Constant.MESSAGE_SEND_BACK_SUCCESS, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                        sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                    SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_SEND_BACK_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pbTraSP_MouseEnter(object sender, EventArgs e)
        {
            pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_MOUSEOVER);

            ttDetail.SetToolTip(pbTraSP, Constant.TOOLTIP_TRA_SP_CONGNO);
        }

        private void pbTraSP_MouseLeave(object sender, EventArgs e)
        {
            pbTraSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            int id = ConvertUtil.ConvertToInt(lvThongTin.CheckedItems[0].SubItems[1].Text);

            uc = new UcInfo(HoaDonBus.GetById(id));
            uc.Disposed += new EventHandler(uc_Disposed);
            this.Controls.Add(uc);
        }

        private void pbSua_MouseEnter(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_MOUSEOVER);
        }

        private void pbSua_MouseLeave(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
        }
        #endregion



        #region Controls
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

            if (e.Column != 0 && e.Column != 1 && e.Column != 2)
            {
                sortColumn = lvThongTin.Columns[e.Column].Text;
                sortOrder = sortOrder == Constant.SORT_ASCENDING ? Constant.SORT_DESCENDING : Constant.SORT_ASCENDING;

                RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void lvThongTin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckListViewItemsIsChecked();
        }

        private void lbPage_Click(object sender, EventArgs e)
        {
            pbBackPage.Enabled = false;
            pbNextPage.Enabled = false;

            tbPage.Visible = true;
            tbPage.Text = "";
            tbPage.Focus();
        }

        private void lvThongTin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTin.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new UcDetail(HoaDonBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
        }

        private void lbPage_TextChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(lbPage.Text) == 0)
            {
                lbPage.Text = "1";
            }
            else
            {
                RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void pbBackPage_Click(object sender, EventArgs e)
        {
            lbPage.Text = (ConvertUtil.ConvertToInt(lbPage.Text) - 1).ToString();
        }

        private void pbBackPage_MouseEnter(object sender, EventArgs e)
        {
            pbBackPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_MOUSEOVER);
        }

        private void pbBackPage_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void pbNextPage_Click(object sender, EventArgs e)
        {
            lbPage.Text = (ConvertUtil.ConvertToInt(lbPage.Text) + 1).ToString();
        }

        private void pbNextPage_MouseEnter(object sender, EventArgs e)
        {
            pbNextPage.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_MOUSEOVER);
        }

        private void pbNextPage_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void tbPage_LostFocus(object sender, EventArgs e)
        {
            tbPage.Visible = false;
        }

        private void tbPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            if (!e.Handled && e.KeyChar == (char)Keys.Enter)
            {
                if (tbPage.Text.Length > 0)
                {
                    if (ConvertUtil.ConvertToInt(tbPage.Text) <= ConvertUtil.ConvertToInt(lbTotalPage.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        tbPage.Visible = false;
                        lbPage.Text = tbPage.Text;
                    }
                }
            }
        }

        private void tbSearch_Enter(object sender, EventArgs e)
        {
            if (tbSearch.Text == Constant.SEARCH_CONGNO_TIP)
            {
                tbSearch.Text = string.Empty;
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (tbSearch.Text == string.Empty)
            {
                tbSearch.Text = Constant.SEARCH_CONGNO_TIP;
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                pbOk_Click(sender, e);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text == Constant.SEARCH_CONGNO_TIP)
            {
                pbOk.Enabled = false;
                pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE_DISABLE);
            }
            else
            {
                pbOk.Enabled = true;
                pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
            }
        }

        private void pbOk_Click(object sender, EventArgs e)
        {
            sortOrder = Constant.SORT_ASCENDING;

            RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void pbOk_MouseEnter(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE_MOUSEOVER);
        }

        private void pbOk_MouseLeave(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_OK_PAGE);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, Constant.ID_TYPE_BAN, Constant.ID_STATUS_DEBT, 0, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }
        #endregion
    }
}
