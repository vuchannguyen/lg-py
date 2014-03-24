using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Library;
using Weedon.NhatKyMuaHang;
using DTO;
using BUS;

namespace Weedon
{
    public partial class UcNhatKyMuaHang : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private string sortColumn;
        private string sortOrder;

        private int soSP;

        public UcNhatKyMuaHang()
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

        private void UcNhatKyMuaHang_Load(object sender, EventArgs e)
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
            dtpFilter.MaxDate = DateTime.Now;
            cbExportType.SelectedIndex = 0;
            sortColumn = string.Empty;
            sortOrder = Constant.SORT_DESCENDING;
            tbSearch.Text = Constant.SEARCH_NHATKYMUAHANG_TIP;
            RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
                sortColumn, sortOrder, 1);
            SetStatusButtonPage(1);
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            tbSearch.Text = Constant.SEARCH_NHATKYMUAHANG_TIP;
            RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
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

        private void RefreshListView(string text, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            if (text == Constant.SEARCH_NHATKYMUAHANG_TIP)
            {
                text = string.Empty;
            }

            int total = NhatKyMuaHangBus.GetCount(text, timeType, date);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;
            long money = 0;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<DTO.NhatKyMuaHang> list = NhatKyMuaHangBus.GetList(text, timeType, date,
                sortColumn, sortOrder, row * (page - 1), row);
            CommonFunc.ClearlvItem(lvThongTin);

            foreach (DTO.NhatKyMuaHang data in list)
            {
                money += data.ThanhTien;
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.Ten);
                lvi.SubItems.Add(data.User.UserName);
                lvi.SubItems.Add(data.Date.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.GhiChu);
                lvThongTin.Items.Add(lvi);
            }

            tbTong.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private bool UpdateStatusSold(int id)
        {
            DTO.NhatKyMuaHang data = NhatKyMuaHangBus.GetById(id);

            if (NhatKyMuaHangBus.Update(data, FormMain.user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ListView GetListView(string text, string timeType, DateTime date)
        {
            ListView lv = new ListView();

            if (text == Constant.SEARCH_NHATKYMUAHANG_TIP)
            {
                text = string.Empty;
            }

            for (int i = 2; i < lvThongTin.Columns.Count; i++)
            {
                lv.Columns.Add((ColumnHeader)lvThongTin.Columns[i].Clone());
            }

            List<DTO.NhatKyMuaHang> list = NhatKyMuaHangBus.GetList(text, timeType, date,
                string.Empty, string.Empty, 0, 0);

            foreach (DTO.NhatKyMuaHang data in list)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (lv.Items.Count + 1).ToString();
                lvi.SubItems.Add(data.Ten);
                lvi.SubItems.Add(data.User.UserName);
                lvi.SubItems.Add(data.Date.ToString());
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));
                lvi.SubItems.Add(data.GhiChu);
                lv.Items.Add(lvi);
            }

            return lv;
        }

        private int GetDaysInMonth(int year, int month)
        {
            DateTime date1 = new DateTime(year, month, 1);
            DateTime date2 = date1.AddMonths(1);
            TimeSpan ts = date2 - date1;

            return (int)ts.TotalDays;
        }

        private void Export(string path, ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                ExportExcel.InitNewSheet("Nhat ky mua hang");
                ExportExcel.CreateSummaryNKMH();
                ExportExcel.CreateDetailsTableNKMH(lv);
            }
        }

        private void ExportData(string path)
        {
            ExportExcel.InitWorkBook();
            Export(path, GetListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value));
            ExportExcel.SaveExcel(path);
        }
        #endregion



        #region Buttons
        private void pbThem_Click(object sender, EventArgs e)
        {
            uc = new UcInfo();
            uc.Disposed += new EventHandler(uc_Disposed);
            this.Controls.Add(uc);
        }

        private void pbThem_MouseEnter(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD_MOUSEOVER);
        }

        private void pbThem_MouseLeave(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_DELETE_CONFIRM, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ids = string.Empty;

                foreach (ListViewItem item in lvThongTin.CheckedItems)
                {
                    ids += (item.SubItems[1].Text + Constant.SEPERATE_STRING);
                }

                if (NhatKyMuaHangBus.DeleteList(ids, FormMain.user))
                {
                    RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
                        sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
                    SetStatusButtonPage(ConvertUtil.ConvertToInt(lbPage.Text));
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

            uc = new UcInfo(NhatKyMuaHangBus.GetById(id));
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
            string path = File_Function.SaveDialog("NKMH" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (path != null)
            {
                ExportData(path);
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
                e.NewWidth = 0;
                e.Cancel = true;
            }

            if (e.ColumnIndex == 1)
            {
                e.NewWidth = 30;
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

                RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
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

                    UserControl uc = new UcDetail(NhatKyMuaHangBus.GetById(id));
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
                RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
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
            if (tbSearch.Text == Constant.SEARCH_NHATKYMUAHANG_TIP)
            {
                tbSearch.Text = string.Empty;
            }
        }

        private void tbSearch_Leave(object sender, EventArgs e)
        {
            if (tbSearch.Text == string.Empty)
            {
                tbSearch.Text = Constant.SEARCH_NHATKYMUAHANG_TIP;
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
            if (tbSearch.Text == Constant.SEARCH_NHATKYMUAHANG_TIP)
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
            RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
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

        private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshListView(tbSearch.Text, cbExportType.Text, dtpFilter.Value,
                sortColumn, sortOrder, ConvertUtil.ConvertToInt(lbPage.Text));
        }
        #endregion
    }
}
