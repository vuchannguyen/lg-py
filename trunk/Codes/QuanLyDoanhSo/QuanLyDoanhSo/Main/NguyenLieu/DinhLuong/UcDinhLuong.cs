using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using Weedon.DinhLuong;
using DTO;
using BUS;

namespace Weedon
{
    public partial class UcDinhLuong : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private string sortColumn;
        private string sortOrder;

        private int soSP;

        public UcDinhLuong()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
                pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);

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

        private void UcDinhLuong_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            this.BringToFront();

            Init();

            this.Visible = true;
        }



        #region Function
        private void Init()
        {
            sortColumn = string.Empty;
            sortOrder = Constant.SORT_ASCENDING;

            tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;

            RefreshListView(tbSearch.Text, 0,
                sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_SANPHAM_TIP;

            RefreshListView(tbSearch.Text, 0,
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

        private void RefreshListView(string text, int idGroup,
            string sortColumn, string sortOrder, int page)
        {
            if (text == Constant.SEARCH_SANPHAM_TIP)
            {
                text = string.Empty;
            }

            int total = SanPhamBus.GetCount(text, idGroup);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.SanPham> list = SanPhamBus.GetList(text, 0,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.SanPham data in list)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.MaSanPham);
                lvi.SubItems.Add(data.SanPhamGroup.Ten);
                lvi.SubItems.Add(data.Ten);
                lvi.SubItems.Add(DinhLuongBus.CountByIdSP(data.Id).ToString());
                lvi.SubItems.Add(data.MoTa);

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
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
                }
                else
                {
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
                }
            }
            else
            {
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

        private bool UpdateStatusSold(int id)
        {
            DTO.SanPham data = SanPhamBus.GetById(id);

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
            lvEx.Columns.Add("Đơn giá     ", 100, HorizontalAlignment.Right); //6
            //lvEx.Columns.Add("ĐVT     ", 100, HorizontalAlignment.Left); //7
            //lvEx.Columns.Add("Xuất xứ                    ", 100, HorizontalAlignment.Left); //8
            //lvEx.Columns.Add("Hiệu                    ", 100, HorizontalAlignment.Left); //9
            //lvEx.Columns.Add("Size                    ", 100, HorizontalAlignment.Left); //10
            //lvEx.Columns.Add("Thời hạn", 100, HorizontalAlignment.Center); //11

            for (int i = 1; i < lvEx.Columns.Count; i++)
            {
                lvEx.Columns[i].VisibleChanged += new EventHandler(LvEx_ColumnVisibleChanged);
            }
        }

        private void HideColumn()
        {
            // Let us hide columns initally
            lvEx.Columns[0].Visible = false;
            lvEx.Columns[6].Visible = false;
            //lvEx.Columns[7].Visible = false;
            //lvEx.Columns[8].Visible = false;
            //lvEx.Columns[9].Visible = false;
            //lvEx.Columns[10].Visible = false;
            //lvEx.Columns[11].Visible = false;

            // We will avoid removing the first column by the user.
            // Dont provide the menu to remove simple...
            lvEx.Columns[0].ColumnMenuItem.Visible = false;
            lvEx.Columns[2].ColumnMenuItem.Visible = false;
            lvEx.Columns[3].ColumnMenuItem.Visible = false;
            lvEx.Columns[4].ColumnMenuItem.Visible = false;
        }

        private void RefreshLvEx(string text)
        {
            lvEx.Items.Clear();

            if (text == Constant.SEARCH_SANPHAM_TIP)
            {
                text = string.Empty;
            }

            List<DTO.SanPham> list = SanPhamBus.GetList(text, 0,
                string.Empty, string.Empty, 0, 0);
            soSP = lvEx.Items.Count;

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

                lvEx.Items.Add(lvi);
            }
        }

        private void LvEx_ColumnVisibleChanged(object sender, EventArgs e)
        {
            RefreshLvEx(tbSearch.Text);
        }
        #endregion



        #region Buttons
        private void pbSua_Click(object sender, EventArgs e)
        {
            int id = ConvertUtil.ConvertToInt(lvThongTin.CheckedItems[0].SubItems[1].Text);

            uc = new UcInfo(SanPhamBus.GetById(id));
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

        private void pbExcel_Click(object sender, EventArgs e)
        {
            if (lvThongTin.Items.Count > 0)
            {
                NewLvEx(Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Width, Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Height);
                LoadLvExLData();
                HideColumn();

                RefreshLvEx(tbSearch.Text);

                FormExportExcel frm = new FormExportExcel(Constant.DEFAULT_TYPE_EXPORT_SANPHAM, Constant.DEFAULT_SHEET_NAME_EXPORT_KHOHANG, Constant.DEFAULT_TYPE_EXPORT_KHOHANG,
                    lvEx, soSP);
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

                RefreshListView(tbSearch.Text, 0,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void lvThongTin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckListViewItemsIsChecked();
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
                RefreshListView(tbSearch.Text, 0,
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
            RefreshListView(tbSearch.Text, 0,
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
        #endregion
    }
}
