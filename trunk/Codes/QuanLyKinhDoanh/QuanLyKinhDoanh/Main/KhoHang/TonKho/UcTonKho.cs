using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using QuanLyKinhDoanh.XuatKho;
using DTO;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class UcTonKho : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;
        private string sortColumn;
        private string sortOrder;

        public UcTonKho()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbXuat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);

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

        private void UcTonKho_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            this.BringToFront();

            sortColumn = string.Empty;
            sortOrder = Constant.SORT_ASCENDING;

            tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;

            RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);

            this.Visible = true;
        }

        

        #region Function
        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;

            RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
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

        private void RefreshListView(string text, int idGroup, string status,
            string sortColumn, string sortOrder, int page)
        {
            if (text == Constant.SEARCH_SANPHAM_TIP)
            {
                text = string.Empty;
            }

            int total = SanPhamBus.GetCount(text, idGroup, false, status, true, Constant.DEFAULT_WARNING_DAYS_EXPIRED);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.SanPham> list = SanPhamBus.GetList(text, idGroup, false, status,
                true, Constant.DEFAULT_WARNING_DAYS_EXPIRED,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.SanPham data in list)
            {
                Color color = Color.Black;
                ListViewItem lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;

                if (data.SoLuong != 0 && data.ThoiHan.Value != 0)
                {
                    switch (CommonFunc.IsExpired(data.CreateDate, Constant.DEFAULT_WARNING_DAYS_EXPIRED,
                        data.ThoiHan.Value, data.DonViThoiHan))
                    {
                        case Constant.DEFAULT_STATUS_USED_DATE_NEAR:
                            color = Color.Orange;
                            break;

                        case Constant.DEFAULT_STATUS_USED_DATE_END:
                            color = Color.Red;
                            break;
                    }
                }

                lvi.SubItems.Add(data.Id.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.MaSanPham, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.Ten, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.MoTa, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.XuatXu == null ? string.Empty : data.XuatXu.Ten, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(CommonFunc.CalculateExpiredDate(data.CreateDate, ConvertUtil.ConvertToInt(data.ThoiHan.Value), data.DonViThoiHan).ToString(Constant.DEFAULT_DATE_FORMAT),
                    color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.SoLuong.ToString(), color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.DonViTinh, color, Color.Transparent, this.Font);
                lvi.SubItems.Add(data.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY), color, Color.Transparent, this.Font);

                if (data.IsSold)
                {
                    lvi.SubItems.Add(Constant.IS_SOLD, color, Color.Transparent, this.Font);
                }
                else
                {
                    lvi.SubItems.Add(string.Empty);
                }

                lvThongTin.Items.Add(lvi);
            }

            CheckListViewItemsIsChecked();
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count == 1)
            {
                pbXuat.Enabled = true;
                pbXuat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);

                return;
            }

            pbXuat.Enabled = false;
            pbXuat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_DISABLE);
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



        #region Xuat
        private void pbXuat_Click(object sender, EventArgs e)
        {
            //int id = ConvertUtil.ConvertToInt(lvThongTin.CheckedItems[0].SubItems[1].Text);

            //uc = new UcInfo(SanPhamBus.GetById(id));
            //uc.Disposed += new EventHandler(uc_Disposed);
            //this.Controls.Add(uc);
        }

        private void pbXuat_MouseEnter(object sender, EventArgs e)
        {
            pbXuat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK_MOUSEOVER);

            ttDetail.SetToolTip(pbXuat, Constant.TOOLTIP_XUAT_KHO);
        }

        private void pbXuat_MouseLeave(object sender, EventArgs e)
        {
            pbXuat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_SEND_BACK);
        }
        #endregion



        #region Controls
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
            if (e.Column != 0 && e.Column != 1 && e.Column != 2)
            {
                sortColumn = lvThongTin.Columns[e.Column].Text;
                sortOrder = sortOrder == Constant.SORT_ASCENDING ? Constant.SORT_DESCENDING : Constant.SORT_ASCENDING;

                RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void lvThongTin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvThongTin.CheckedItems.Count == 2)
            {
                foreach (ListViewItem item in lvThongTin.CheckedItems)
                {
                    item.Checked = false;
                }

                e.Item.Checked = true;
            }

            CheckListViewItemsIsChecked();
        }

        private void lvThongTin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTin.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new QuanLyKinhDoanh.KhoHang.UcDetail(SanPhamBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
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
                RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
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
            if (tbSearch.Text == Constant.SEARCH_SANPHAM_TIP)
            {
                tbSearch.Text = string.Empty;
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (tbSearch.Text == string.Empty)
            {
                tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;
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
            if (tbSearch.Text == Constant.SEARCH_SANPHAM_TIP)
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
            RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
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
            RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, 0, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
        }
        #endregion
    }
}
