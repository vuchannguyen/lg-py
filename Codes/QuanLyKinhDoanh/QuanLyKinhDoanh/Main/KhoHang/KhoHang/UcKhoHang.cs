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

        private ListViewEx lvEx;
        private int columnCount;

        private int soSP;
        private int tongSoLuong;
        private int soSPHetHan;
        private int tongSoLuongHetHan;

        public UcKhoHang()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);
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
            int total = SanPhamBus.GetCount(text, idGroup, false, status, false, 0);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.SanPham> list = SanPhamBus.GetList(text, idGroup, false, status,
                false, 0,
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
                else if (data.ThoiHan.Value != 0)
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



        #region Export Excel
        public void NewLvEx(int width, int height)
        {
            lvEx = new ListViewEx();

            lvEx.FullRowSelect = true;
            lvEx.GridLines = true;
            lvEx.Location = new System.Drawing.Point(3, 3);
            lvEx.MultiSelect = false;
            lvEx.Name = "lvEx";
            lvEx.Size = new System.Drawing.Size(width, height);
            lvEx.TabIndex = 0;
            lvEx.UseCompatibleStateImageBehavior = false;
            lvEx.View = System.Windows.Forms.View.Details;
        }

        private void LoadLvExLData()
        {
            lvEx.Columns.Add("Mã", 10, HorizontalAlignment.Left); //0
            lvEx.Columns.Add("STT", 50, HorizontalAlignment.Center); //1
            lvEx.Columns.Add("Mã SP     ", 100, HorizontalAlignment.Center); //2
            lvEx.Columns.Add("Nhóm                    ", 100, HorizontalAlignment.Left); //3
            lvEx.Columns.Add("Tên                    ", 100, HorizontalAlignment.Left); //4
            lvEx.Columns.Add("Mô tả                              ", 100, HorizontalAlignment.Left); //5
            lvEx.Columns.Add("SL   ", 100, HorizontalAlignment.Center); //6
            lvEx.Columns.Add("ĐVT     ", 100, HorizontalAlignment.Left); //7
            lvEx.Columns.Add("Đơn giá     ", 100, HorizontalAlignment.Right); //8
            lvEx.Columns.Add("Xuất xứ                    ", 100, HorizontalAlignment.Left); //9
            lvEx.Columns.Add("Hiệu                    ", 100, HorizontalAlignment.Left); //10
            lvEx.Columns.Add("Size                    ", 100, HorizontalAlignment.Left); //11
            lvEx.Columns.Add("Thời hạn", 100, HorizontalAlignment.Center); //12
            lvEx.Columns.Add("Ngày nhập     ", 100, HorizontalAlignment.Center); //13
            lvEx.Columns.Add("Ngày hết hạn", 100, HorizontalAlignment.Center); //14

            for (int i = 1; i < lvEx.Columns.Count; i++)
            {
                lvEx.Columns[i].VisibleChanged += new EventHandler(LvEx_ColumnVisibleChanged);
            }
        }

        private void HideColumn()
        {
            // Let us hide columns initally
            lvEx.Columns[0].Visible = false;
            lvEx.Columns[9].Visible = false;
            lvEx.Columns[10].Visible = false;
            lvEx.Columns[11].Visible = false;

            // We will avoid removing the first column by the user.
            // Dont provide the menu to remove simple...
            lvEx.Columns[0].ColumnMenuItem.Visible = false;
            lvEx.Columns[2].ColumnMenuItem.Visible = false;
            lvEx.Columns[3].ColumnMenuItem.Visible = false;
            lvEx.Columns[4].ColumnMenuItem.Visible = false;
            lvEx.Columns[6].ColumnMenuItem.Visible = false;
            lvEx.Columns[8].ColumnMenuItem.Visible = false;
            lvEx.Columns[14].ColumnMenuItem.Visible = false;
        }

        private void RefreshLvEx(string text, int idGroup, string status)
        {
            lvEx.Items.Clear();

            List<DTO.SanPham> list = SanPhamBus.GetList(text, idGroup, false, status,
                false, 0,
                string.Empty, string.Empty, 0, 0);

            soSP = list.Count;
            tongSoLuong = 0;
            soSPHetHan = 0;
            tongSoLuongHetHan = 0;

            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                int colNum = 0;

                //if (lvEx.Columns[0].Visible)
                //{
                //    lvi.Text = list_dto[i].Ma;
                //}

                colNum++; //1

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.Text = (i + 1).ToString();
                }

                colNum++; //2

                if (lvEx.Columns[colNum].Visible)
                {
                    if (!lvEx.Columns[1].Visible)
                    {
                        lvi.Text = list[i].MaSanPham;
                    }
                    else
                    {
                        lvi.SubItems.Add(list[i].MaSanPham);
                    }
                }

                colNum++; //3

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].SanPhamGroup.Ten);
                }

                colNum++; //4

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].Ten);
                }

                colNum++; //5

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].MoTa);
                }

                colNum++; //6

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].SoLuong.ToString());

                    tongSoLuong += list[i].SoLuong;
                }

                colNum++; //7

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].DonViTinh);
                }

                colNum++; //8

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY));
                }

                colNum++; //9

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].XuatXu == null ? string.Empty : list[i].XuatXu.Ten);
                }

                colNum++; //10

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].Hieu);
                }

                colNum++; //11

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].Size);
                }

                colNum++; //12

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].ThoiHan.ToString() + " " + list[i].DonViThoiHan);
                }

                colNum++; //13

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                }

                colNum++; //14

                if (lvEx.Columns[colNum].Visible)
                {
                    if (list[i].ThoiHan.Value != 0)
                    {
                        lvi.SubItems.Add(CommonFunc.CalculateExpiredDate(list[i].CreateDate, list[i].ThoiHan.Value, list[i].DonViThoiHan).ToString(Constant.DEFAULT_DATE_FORMAT));
                    }
                    else
                    {
                        lvi.SubItems.Add(string.Empty);
                    }
                }

                if (list[i].ThoiHan.Value != 0)
                {
                    switch (CommonFunc.IsExpired(list[i].CreateDate, Constant.DEFAULT_WARNING_DAYS_EXPIRED,
                        list[i].ThoiHan.Value, list[i].DonViThoiHan))
                    {
                        case Constant.DEFAULT_STATUS_USED_DATE_END:
                            soSPHetHan++;
                            tongSoLuongHetHan += list[i].SoLuong;
                            break;
                    }
                }

                lvEx.Items.Add(lvi);
            }
        }

        private void LvEx_ColumnVisibleChanged(object sender, EventArgs e)
        {
            RefreshLvEx(tbSearch.Text, idGroup, status);
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

        private void pbExcel_Click(object sender, EventArgs e)
        {
            if (lvThongTin.Items.Count > 0)
            {
                NewLvEx(Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Width, Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Height);
                LoadLvExLData();
                HideColumn();

                RefreshLvEx(tbSearch.Text, idGroup, status);

                FormExportExcel frm = new FormExportExcel(Constant.DEFAULT_TYPE_EXPORT_KHOHANG, Constant.DEFAULT_SHEET_NAME_EXPORT_KHOHANG, Constant.DEFAULT_TYPE_EXPORT_KHOHANG,
                    lvEx, soSP, tongSoLuong, soSPHetHan, tongSoLuongHetHan);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_EXPORT_EXCEL_NULL_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbExcel_MouseEnter(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL_MOUSEOVER);
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);
        }
    }
}
