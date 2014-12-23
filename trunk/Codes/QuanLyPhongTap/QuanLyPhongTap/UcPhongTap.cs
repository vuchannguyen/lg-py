using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;
using Controller;
using Model;

namespace QuanLyPhongTap
{
    public partial class UcPhongTap : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;
        private string sortColumn;
        private string sortOrder;

        public UcPhongTap()
        {
            InitializeComponent();
        }

        private void UcPhongTap_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Dock = DockStyle.Fill;
            tbPage.BringToFront();
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);
            this.BringToFront();
            sortColumn = string.Empty;
            sortOrder = Constant.SORT_ASCENDING;
            tbSearch.Text = Constant.SEARCH_KHACHHANG_TIP;
            RefreshListView(tbSearch.Text,
                sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);
            this.Visible = true;
        }

        #region Function
        private void InitPermission()
        {
            if (UserImp.currentUser.IdGroup != Constant.ID_GROUP_ADMIN)
            {
                btThem.Visible = false;
                btXoa.Visible = false;
            }
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_KHACHHANG_TIP;
            RefreshListView(tbSearch.Text,
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

        private void RefreshListView(string text,
            string sortColumn, string sortOrder, int page)
        {
            try
            {
                if (text == Constant.SEARCH_KHACHHANG_TIP)
                {
                    text = string.Empty;
                }

                int total = KhachHangImp.GetCount(text);
                int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
                lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

                if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage || total == 0)
                {
                    lbPage.Text = maxPage.ToString();
                    return;
                }

                List<KhachHang> list = KhachHangImp.GetList(text,
                    sortColumn, sortOrder, row * (page - 1), row);
                CommonFunc.ClearlvItem(lvThongTin);

                if (list != null)
                {
                    foreach (KhachHang data in list)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.SubItems.Add(data.Id.ToString());
                        lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString());
                        lvi.SubItems.Add(data.Ma);
                        lvi.SubItems.Add(data.Ten);
                        lvi.SubItems.Add(data.NgayHetHan.HasValue ? data.NgayHetHan.Value.ToString(Constant.DEFAULT_DATE_FORMAT) : string.Empty);
                        lvi.SubItems.Add(data.SoXe == null ? string.Empty : data.SoXe);
                        lvi.SubItems.Add(data.DTDD == null ? string.Empty : data.DTDD);
                        lvi.SubItems.Add(data.Email == null ? string.Empty : data.Email);
                        lvi.SubItems.Add(data.GhiChu == null ? string.Empty : data.GhiChu);
                        lvThongTin.Items.Add(lvi);
                    }

                    CheckListViewItemsIsChecked();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count > 0)
            {
                if (lvThongTin.CheckedItems.Count == 1)
                {
                    btSua.Enabled = true;
                }
                else
                {
                    btSua.Enabled = false;
                }

                btXoa.Enabled = true;
            }
            else
            {
                btXoa.Enabled = false;
                btSua.Enabled = false;
            }
        }

        private void SetStatusButtonPage(int iPage)
        {
            if (ConvertUtil.ConvertToInt(lbPage.Text) == 1)
            {
                btBackPage.Enabled = false;
            }
            else
            {
                btBackPage.Enabled = true;
            }

            if (ConvertUtil.ConvertToInt(lbPage.Text) == ConvertUtil.ConvertToInt(lbTotalPage.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                btNextPage.Enabled = false;
            }
            else
            {
                btNextPage.Enabled = true;
            }
        }
        #endregion

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
                RefreshListView(tbSearch.Text,
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
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    if (lvThongTin.SelectedItems.Count > 0)
            //    {
            //        int id = ConvertUtil.ConvertToInt(lvThongTin.SelectedItems[0].SubItems[1].Text);
            //        UserControl uc = new UcDetail(KhachHangBus.GetById(id));
            //        this.Controls.Add(uc);
            //    }
            //}
        }

        private void lbPage_Click(object sender, EventArgs e)
        {
            btBackPage.Enabled = false;
            btNextPage.Enabled = false;
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
                RefreshListView(tbSearch.Text,
                    sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
            }
        }

        private void btBackPage_Click(object sender, EventArgs e)
        {
            lbPage.Text = (ConvertUtil.ConvertToInt(lbPage.Text) - 1).ToString();
        }

        private void btNextPage_Click(object sender, EventArgs e)
        {
            lbPage.Text = (ConvertUtil.ConvertToInt(lbPage.Text) + 1).ToString();
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
    }
}
