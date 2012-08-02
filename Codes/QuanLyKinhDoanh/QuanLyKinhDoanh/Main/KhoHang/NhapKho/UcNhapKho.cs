using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using QuanLyKinhDoanh.NhapKho;
using DTO;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class UcNhapKho : UserControl
    {
        private UserControl uc;
        //private const int row = Constant.DEFAULT_ROW;
        private const int row = 15;
        private string sortColumn;
        private string sortOrder;

        public UcNhapKho()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
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

        private void UcNhapKho_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            this.BringToFront();

            sortColumn = string.Empty;
            sortOrder = Constant.SORT_DESCENDING;

            cbFilter.SelectedIndex = 1;

            tbSearch.Text = Constant.SEARCH_NHAPKHO_TIP;

            RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);

            RefreshListViewNew(string.Empty, Constant.ID_TYPE_MUA, string.Empty, dtpFilter.Value,
                        "Ngày nhập", Constant.SORT_DESCENDING, 0);

            dtpFilter.CustomFormat = Constant.DEFAULT_DATE_FORMAT;

            this.Visible = true;
        }

        

        #region Function
        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_NHAPKHO_TIP;

            RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));

            RefreshListViewNew(string.Empty, Constant.ID_TYPE_MUA, string.Empty, dtpFilter.Value,
                "Ngày nhập", Constant.SORT_DESCENDING, 0);

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

        private void RefreshListView(string text, int type, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            if (text == Constant.SEARCH_NHAPKHO_TIP)
            {
                text = string.Empty;
            }

            int total = HoaDonDetailBus.GetCount(text, type, cbFilter.Text, dtpFilter.Value);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.HoaDonDetail> listTotal = HoaDonDetailBus.GetList(text, type, timeType, date,
                string.Empty, string.Empty, 0, 0);
            long totalMoney = 0;

            foreach (DTO.HoaDonDetail data in listTotal)
            {
                totalMoney += data.ThanhTien;
            }

            tbTong.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);

            List<DTO.HoaDonDetail> list = HoaDonDetailBus.GetList(text, type, timeType, date,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.HoaDonDetail data in list)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.HoaDon.MaHoaDon.ToString());
                lvi.SubItems.Add(data.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + data.SanPham.Ten);
                lvi.SubItems.Add(data.HoaDon.User == null ? string.Empty : data.HoaDon.User.UserName);
                lvi.SubItems.Add(data.HoaDon.UpdateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                lvi.SubItems.Add(data.SoLuong.ToString());
                lvi.SubItems.Add(data.SanPham.DonViTinh);
                lvi.SubItems.Add(data.SanPham.GiaMua.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTin.Items.Add(lvi);
            }

            CheckListViewItemsIsChecked();
        }

        private void RefreshListViewNew(string text, int type, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            List<DTO.HoaDonDetail> list = HoaDonDetailBus.GetList(text, type, timeType, date,
                sortColumn, sortOrder, 0, 3);

            CommonFunc.ClearlvItem(lvThongTinNew);

            foreach (DTO.HoaDonDetail data in list)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((lvThongTinNew.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.HoaDon.MaHoaDon.ToString());
                lvi.SubItems.Add(data.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + data.SanPham.Ten);
                lvi.SubItems.Add(data.HoaDon.User == null ? string.Empty : data.HoaDon.User.UserName);
                lvi.SubItems.Add(data.HoaDon.UpdateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                lvi.SubItems.Add(data.SoLuong.ToString());
                lvi.SubItems.Add(data.SanPham.DonViTinh);
                lvi.SubItems.Add(data.SanPham.GiaMua.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTinNew.Items.Add(lvi);
            }
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count > 0)
            {
                if (lvThongTin.CheckedItems.Count == 1)
                {
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
                }
                else
                {
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
                }

                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
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
        #endregion



        #region Thêm Xóa Sửa
        private void pbThem_Click(object sender, EventArgs e)
        {
            uc = new UcInfo();
            uc.Disposed += new EventHandler(uc_Disposed);
            this.Controls.Add(uc);
        }

        private void pbThem_MouseEnter(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD_MOUSEOVER);

            ttDetail.SetToolTip(pbThem, Constant.TOOLTIP_MUA_THEM);
        }

        private void pbThem_MouseLeave(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_DELETE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ids = string.Empty;
                string idSPs = string.Empty;
                string idCKs = string.Empty;

                foreach (ListViewItem item in lvThongTin.CheckedItems)
                {
                    HoaDonDetail temp = HoaDonDetailBus.GetById(ConvertUtil.ConvertToInt(item.SubItems[1].Text));

                    ids += (temp.IdHoaDon.ToString() + Constant.SEPERATE_STRING);
                    idSPs += (temp.IdSanPham.ToString() + Constant.SEPERATE_STRING);
                    idCKs += (ChietKhauBus.GetByIdSP(temp.IdSanPham) == null ? string.Empty : (ChietKhauBus.GetByIdSP(temp.IdSanPham).Id.ToString() + Constant.SEPERATE_STRING));
                }

                if (SanPhamBus.DeleteList(idSPs, FormMain.user) && HoaDonBus.DeleteList(ids, FormMain.user))
                {
                    ChietKhauBus.DeleteList(idCKs, FormMain.user);

                    RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                        sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                    SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));

                    RefreshListViewNew(string.Empty, Constant.ID_TYPE_MUA, string.Empty, dtpFilter.Value,
                        "Ngày nhập", Constant.SORT_DESCENDING, 0);
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_DELETE_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_MOUSEOVER);
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
        }

        private void pbSua_Click(object sender, EventArgs e)
        {
            int id = ConvertUtil.ConvertToInt(lvThongTin.CheckedItems[0].SubItems[1].Text);

            uc = new UcInfo(HoaDonDetailBus.GetById(id));
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
        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            if (e.Column != 0 && e.Column != 1 && e.Column != 2)
            {
                sortColumn = lvThongTin.Columns[e.Column].Text;
                sortOrder = sortOrder == Constant.SORT_ASCENDING ? Constant.SORT_DESCENDING : Constant.SORT_ASCENDING;

                RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void lvThongTinNew_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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

        private void lbPage_TextChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(lbPage.Text) == 0)
            {
                lbPage.Text = "1";
            }
            else
            {
                RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
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
            if (tbSearch.Text == Constant.SEARCH_NHAPKHO_TIP)
            {
                tbSearch.Text = string.Empty;
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (tbSearch.Text == string.Empty)
            {
                tbSearch.Text = Constant.SEARCH_NHAPKHO_TIP;
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
            if (tbSearch.Text == Constant.SEARCH_NHAPKHO_TIP)
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
            RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
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
            RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, Constant.ID_TYPE_MUA, cbFilter.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }
        #endregion

        private void lvThongTin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTin.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new UcDetail(HoaDonDetailBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
        }

        private void lvThongTinNew_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTinNew.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTinNew.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new UcDetail(HoaDonDetailBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
        }
    }
}
