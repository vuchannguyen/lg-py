using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyKinhDoanh.KhoHang;
using Library;
using DTO;
using BUS;

namespace QuanLyKinhDoanh
{
    public partial class UcKhoHang : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;

        private string sortColumn;
        private string sortOrder;
        private Image imgCheck;
        private Image imgWarning;
        private int lvWidth;
        private int centerX;
        private int idGroup;
        private string status;
        private string search;

        public UcKhoHang()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND);

                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");

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

        private void UcKhoHang_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnFind.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            this.BringToFront();

            if (!Init())
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm sản phẩm"), Constant.CAPTION_ERROR,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
            else
            {
                this.Visible = true;
            }
        }



        #region Function
        private bool Init()
        {
            if (!GetListGroupSP())
            {
                return false;
            }
            else
            {
                imgCheck = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CHECK);
                imgWarning = Image.FromFile(ConstantResource.CHUC_NANG_ICON_WARNING);

                lvWidth = 0;

                for (int i = 0; i < lvThongTin.Columns.Count; i++)
                {
                    lvWidth += lvThongTin.Columns[i].Width;
                }

                centerX = lvThongTin.Columns[lvThongTin.Columns.Count - 1].Width / 2;

                lvThongTin.OwnerDraw = true;

                sortColumn = string.Empty;
                sortOrder = Constant.SORT_ASCENDING;

                tbSearch.Text = string.Empty;
                cbStatus.SelectedIndex = 0;
                cbGroup.SelectedIndex = 0;

                RefreshListView(search, idGroup, status,
                    sortColumn, sortOrder, 1);
                SetStatusButtonPage(1);

                return true;
            }
        }
        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;

            RefreshListView(search, idGroup, status,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
            SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));

            if (tbSearch.Focused)
            {
                tbSearch.Text = string.Empty;
            }
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
            int total = SanPhamBus.GetCount(text, idGroup, false, status);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.SanPham> list = SanPhamBus.GetList(text, idGroup, false, status,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.SanPham data in list)
            {
                Color color = Color.Black;
                ListViewItem lvi = new ListViewItem();
                lvi.UseItemStyleForSubItems = false;

                if (data.SoLuong == 0)
                {
                    color = Color.Red;
                }
                else
                {
                    switch (CommonFunc.IsEndOfUseDate(data.CreateDate, Constant.DEFAULT_WARNING_DAYS_USED_DATE,
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
        }

        private bool GetListGroupSP()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm sản phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            cbGroup.Items.Add(new CommonComboBoxItems(Constant.DEFAULT_FIRST_VALUE_COMBOBOX, 0));

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
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

        private bool UpdateStatusSold(int id)
        {
            DTO.SanPham data = SanPhamBus.GetById(id);

            if (data.SoLuong == 0)
            {
                return false;
            }

            data.IsSold = !data.IsSold;

            if (SanPhamBus.Update(data, FormMain.user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion



        #region Controls
        private void pbFind_Click(object sender, EventArgs e)
        {
            pbFind.Focus();

            search = tbSearch.Text;
            status = cbStatus.Text;

            RefreshListView(search, idGroup, status,
                    sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);
        }

        private void pbFind_MouseEnter(object sender, EventArgs e)
        {
            pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND_MOUSEOVER);
        }

        private void pbFind_MouseLeave(object sender, EventArgs e)
        {
            pbFind.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_FIND);
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

            lvWidth = 0;

            for (int i = 0; i < lvThongTin.Columns.Count; i++)
            {
                lvWidth += lvThongTin.Columns[i].Width;
            }

            centerX = lvThongTin.Columns[lvThongTin.Columns.Count - 1].Width / 2;
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

                RefreshListView(search, idGroup, status,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void lvThongTin_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvThongTin_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == lvThongTin.Columns.Count - 1)
            {
                if (e.SubItem.Text == Constant.IS_SOLD)
                {
                    e.Graphics.DrawImage(imgWarning, new System.Drawing.RectangleF(lvWidth - centerX - 6, e.Item.Position.Y, imgWarning.Width * 1f, imgWarning.Height * 1f));
                }
                else
                {
                    e.Graphics.DrawImage(imgCheck, new System.Drawing.RectangleF(lvWidth - centerX - 6, e.Item.Position.Y, imgCheck.Width * 1f, imgCheck.Height * 1f));
                }
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        private void lvThongTin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > lvWidth - centerX - 12 && e.X < lvWidth - centerX + 12)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void lvThongTin_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvThongTin.GetItemAt(e.X, e.Y);

            if (e.X > lvWidth - centerX - 12 && e.X < lvWidth - centerX + 12)
            {
                if (item != null && item.SubItems[lvThongTin.Columns.Count - 1].Text == Constant.IS_SOLD)
                {
                    if (UpdateStatusSold(ConvertUtil.ConvertToInt(item.SubItems[1].Text)))
                    {
                        RefreshListView(search, idGroup, status,
                            sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                        SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
                    }
                }
            }
        }

        private void lvThongTin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTin.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new UcDetail(SanPhamBus.GetById(id));
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
                RefreshListView(search, idGroup, status,
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

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            idGroup = (CommonComboBoxItems)cbGroup.SelectedItem == null ? 0 :
                ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
        }

        private void cbGroup_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbGroup.Text))
            {
                cbGroup.SelectedIndex = 0;
            }
        }

        private void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                pbFind_Click(sender, e);

                tbSearch.Focus();
            }
        }
        #endregion
    }
}
