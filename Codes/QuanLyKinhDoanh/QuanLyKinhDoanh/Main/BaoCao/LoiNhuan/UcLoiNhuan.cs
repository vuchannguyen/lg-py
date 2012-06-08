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

namespace QuanLyKinhDoanh
{
    public partial class UcLoiNhuan : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;
        private string sortColumnThu;
        private string sortOrderThu;
        private string sortColumnChi;
        private string sortOrderChi;

        private long totalThu;
        private long totalChi;

        public UcLoiNhuan()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_UP);

                pbTotalPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);
                pbBackPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);

                pbTotalPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_TOTALPAGE);
                pbBackPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
                pbNextPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcLoiNhuan_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnQuanLy.Top);

            tbPageThu.Location = new Point(pnPageThu.Left + 2, pnPageThu.Top - 1);
            tbPageThu.LostFocus += new EventHandler(tbPageThu_LostFocus);

            tbPageChi.Location = new Point(pnPageChi.Left + 2, pnPageChi.Top - 1);
            tbPageChi.LostFocus += new EventHandler(tbPageChi_LostFocus);

            this.BringToFront();

            Init();

            this.Visible = true;
        }

        private void Init()
        {
            totalThu = 0;
            totalChi = 0;

            sortColumnThu = string.Empty;
            sortOrderThu = Constant.SORT_ASCENDING;

            sortColumnChi = string.Empty;
            sortOrderChi = Constant.SORT_ASCENDING;

            cbFilter.SelectedIndex = 0;

            RefreshListViewThu(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderThu, 1);
            SetStatusButtonPageThu(1);

            RefreshListViewChi(string.Empty, Constant.ID_TYPE_MUA_CHI, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderChi, 1);
            SetStatusButtonPageChi(1);
        }

        private void CalculateRevenue()
        {
            float revenue = 0;

            if (totalThu > 0 && totalChi > 0)
            {
                revenue = (1f * totalThu / totalChi * 100) - 100;
            }

            if (revenue < 0)
            {
                pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_DOWN);
            }
            else
            {
                pbLoiNhuan.Image = Image.FromFile(ConstantResource.THUCHI_ICON_UP);
            }

            lbLoiNhuan.Text = revenue.ToString("##.##") + Constant.SYMBOL_DISCOUNT;
            lbLoiNhuan.Location = CommonFunc.SetWidthCenter(pnLoiNhuan.Size, lbLoiNhuan.Size, lbLoiNhuan.Top);
        }



        #region Function Thu
        private int GetTotalPageThu(int total)
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

        private void RefreshListViewThu(string text, int type, int status, int idKH, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            int total = HoaDonBus.GetCount(text, type, status, idKH, timeType, date);
            int maxPage = GetTotalPageThu(total) == 0 ? 1 : GetTotalPageThu(total);
            lbTotalPageThu.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPageThu.Text) > maxPage)
            {
                lbPageThu.Text = maxPage.ToString();

                return;
            }

            List<DTO.HoaDon> listTotal = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                string.Empty, string.Empty, 0, 0);
            totalThu = 0;

            foreach (DTO.HoaDon data in listTotal)
            {
                totalThu += data.ThanhTien;
            }

            tbTongThu.Text = totalThu.ToString(Constant.DEFAULT_FORMAT_MONEY);

            List<DTO.HoaDon> list = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTinThu);

            foreach (DTO.HoaDon data in list)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTinThu.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.MaHoaDon.ToString());
                lvi.SubItems.Add(data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                lvi.SubItems.Add(data.GhiChu);
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTinThu.Items.Add(lvi);
            }
        }

        private void SetStatusButtonPageThu(int iPage)
        {
            if (ConvertUtil.ConvertToInt(lbPageThu.Text) == 1)
            {
                pbBackPageThu.Enabled = false;
                pbBackPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_DISABLE);
            }
            else
            {
                pbBackPageThu.Enabled = true;
                pbBackPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
            }

            if (ConvertUtil.ConvertToInt(lbPageThu.Text) == ConvertUtil.ConvertToInt(lbTotalPageThu.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                pbNextPageThu.Enabled = false;
                pbNextPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_DISABLE);
            }
            else
            {
                pbNextPageThu.Enabled = true;
                pbNextPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
        }
        #endregion



        #region Function Chi
        private int GetTotalPageChi(int total)
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

        private void RefreshListViewChi(string text, int type, int status, int idKH, string timeType, DateTime date,
            string sortColumn, string sortOrder, int page)
        {
            int total = HoaDonBus.GetCount(text, type, status, idKH, timeType, date);
            int maxPage = GetTotalPageChi(total) == 0 ? 1 : GetTotalPageChi(total);
            lbTotalPageChi.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPageChi.Text) > maxPage)
            {
                lbPageChi.Text = maxPage.ToString();

                return;
            }

            List<DTO.HoaDon> listTotal = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                string.Empty, string.Empty, 0, 0);
            totalChi = 0;

            foreach (DTO.HoaDon data in listTotal)
            {
                totalChi += data.ThanhTien;
            }

            tbTongChi.Text = totalChi.ToString(Constant.DEFAULT_FORMAT_MONEY);

            List<DTO.HoaDon> list = HoaDonBus.GetList(text, type, status, idKH, timeType, date,
                sortColumn, sortOrder, row * (page - 1), row);

            CommonFunc.ClearlvItem(lvThongTinChi);

            foreach (DTO.HoaDon data in list)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTinChi.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.MaHoaDon.ToString());
                lvi.SubItems.Add(data.CreateDate.ToString(Constant.DEFAULT_DATE_TIME_FORMAT));
                lvi.SubItems.Add(data.GhiChu);
                lvi.SubItems.Add(data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY));

                lvThongTinChi.Items.Add(lvi);
            }
        }

        private void SetStatusButtonPageChi(int iPage)
        {
            if (ConvertUtil.ConvertToInt(lbPageChi.Text) == 1)
            {
                pbBackPageChi.Enabled = false;
                pbBackPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_DISABLE);
            }
            else
            {
                pbBackPageChi.Enabled = true;
                pbBackPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE);
            }

            if (ConvertUtil.ConvertToInt(lbPageChi.Text) == ConvertUtil.ConvertToInt(lbTotalPageChi.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
            {
                pbNextPageChi.Enabled = false;
                pbNextPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_DISABLE);
            }
            else
            {
                pbNextPageChi.Enabled = true;
                pbNextPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE);
            }
        }
        #endregion



        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshListViewThu(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderThu, ConvertUtil.ConvertToInt(lbPageThu.Text));
            SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));

            RefreshListViewChi(string.Empty, Constant.ID_TYPE_MUA_CHI, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderChi, ConvertUtil.ConvertToInt(lbPageChi.Text));
            SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));

            CalculateRevenue();
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshListViewThu(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderThu, ConvertUtil.ConvertToInt(lbPageThu.Text));
            SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));

            RefreshListViewChi(string.Empty, Constant.ID_TYPE_MUA_CHI, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    string.Empty, sortOrderChi, ConvertUtil.ConvertToInt(lbPageChi.Text));
            SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));

            CalculateRevenue();
        }



        #region Controls Thu
        private void lvThongTinThu_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != 0 && e.Column != 1 && e.Column != 2)
            {
                sortColumnThu = lvThongTinThu.Columns[e.Column].Text;
                sortOrderThu = sortOrderThu == Constant.SORT_ASCENDING ? Constant.SORT_DESCENDING : Constant.SORT_ASCENDING;

                RefreshListViewThu(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumnThu, sortOrderThu, ConvertUtil.ConvertToInt(lbPageThu.Text));
                SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));
            }
        }

        private void lvThongTinThu_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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

        private void lvThongTinThu_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTinThu.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTinThu.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new QuanLyKinhDoanh.Thu.UcDetail(HoaDonBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
        }

        private void lbPageThu_Click(object sender, EventArgs e)
        {
            pbBackPageThu.Enabled = false;
            pbNextPageThu.Enabled = false;

            tbPageThu.Visible = true;
            tbPageThu.Text = "";
            tbPageThu.Focus();
        }

        private void lbPageThu_TextChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(lbPageThu.Text) == 0)
            {
                lbPageThu.Text = "1";
            }
            else
            {
                RefreshListViewThu(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                   sortColumnThu, sortOrderThu, ConvertUtil.ConvertToInt(lbPageThu.Text));
                SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));
            }
        }

        private void pbBackPageThu_Click(object sender, EventArgs e)
        {
            lbPageThu.Text = (ConvertUtil.ConvertToInt(lbPageThu.Text) - 1).ToString();
        }

        private void pbBackPageThu_MouseEnter(object sender, EventArgs e)
        {
            pbBackPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_MOUSEOVER);
        }

        private void pbBackPageThu_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));
        }

        private void pbNextPageThu_Click(object sender, EventArgs e)
        {
            lbPageThu.Text = (ConvertUtil.ConvertToInt(lbPageThu.Text) + 1).ToString();
        }

        private void pbNextPageThu_MouseEnter(object sender, EventArgs e)
        {
            pbNextPageThu.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_MOUSEOVER);
        }

        private void pbNextPageThu_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPageThu(ConvertUtil.ConvertToInt(lbPageThu.Text));
        }

        private void tbPageThu_LostFocus(object sender, EventArgs e)
        {
            tbPageThu.Visible = false;
        }

        private void tbPageThu_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            if (!e.Handled && e.KeyChar == (char)Keys.Enter)
            {
                if (tbPageThu.Text.Length > 0)
                {
                    if (ConvertUtil.ConvertToInt(tbPageThu.Text) <= ConvertUtil.ConvertToInt(lbTotalPageThu.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        tbPageThu.Visible = false;
                        lbPageThu.Text = tbPageThu.Text;
                    }
                }
            }
        }
        #endregion



        #region Controls Chi
        private void lvThongTinChi_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != 0 && e.Column != 1 && e.Column != 2)
            {
                sortColumnChi = lvThongTinChi.Columns[e.Column].Text;
                sortOrderChi = sortOrderChi == Constant.SORT_ASCENDING ? Constant.SORT_DESCENDING : Constant.SORT_ASCENDING;

                RefreshListViewChi(string.Empty, Constant.ID_TYPE_BAN_THU, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                    sortColumnChi, sortOrderChi, ConvertUtil.ConvertToInt(lbPageChi.Text));
                SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));
            }
        }

        private void lvThongTinChi_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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

        private void lvThongTinChi_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (lvThongTinChi.SelectedItems.Count > 0)
                {
                    int id = ConvertUtil.ConvertToInt(lvThongTinChi.SelectedItems[0].SubItems[1].Text);

                    UserControl uc = new QuanLyKinhDoanh.Chi.UcDetail(HoaDonBus.GetById(id));
                    this.Controls.Add(uc);
                }
            }
        }

        private void lbPageChi_Click(object sender, EventArgs e)
        {
            pbBackPageChi.Enabled = false;
            pbNextPageChi.Enabled = false;

            tbPageChi.Visible = true;
            tbPageChi.Text = "";
            tbPageChi.Focus();
        }

        private void lbPageChi_TextChanged(object sender, EventArgs e)
        {
            if (ConvertUtil.ConvertToInt(lbPageChi.Text) == 0)
            {
                lbPageChi.Text = "1";
            }
            else
            {
                RefreshListViewChi(string.Empty, Constant.ID_TYPE_MUA_CHI, Constant.ID_STATUS_DONE, 0, cbFilter.Text, dtpFilter.Value,
                   sortColumnChi, sortOrderChi, ConvertUtil.ConvertToInt(lbPageChi.Text));
                SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));
            }
        }

        private void pbBackPageChi_Click(object sender, EventArgs e)
        {
            lbPageChi.Text = (ConvertUtil.ConvertToInt(lbPageChi.Text) - 1).ToString();
        }

        private void pbBackPageChi_MouseEnter(object sender, EventArgs e)
        {
            pbBackPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_BACK_PAGE_MOUSEOVER);
        }

        private void pbBackPageChi_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));
        }

        private void pbNextPageChi_Click(object sender, EventArgs e)
        {
            lbPageChi.Text = (ConvertUtil.ConvertToInt(lbPageChi.Text) + 1).ToString();
        }

        private void pbNextPageChi_MouseEnter(object sender, EventArgs e)
        {
            pbNextPageChi.Image = Image.FromFile(ConstantResource.CHUC_NANG_BUTTON_NEXT_PAGE_MOUSEOVER);
        }

        private void pbNextPageChi_MouseLeave(object sender, EventArgs e)
        {
            SetStatusButtonPageChi(ConvertUtil.ConvertToInt(lbPageChi.Text));
        }

        private void tbPageChi_LostFocus(object sender, EventArgs e)
        {
            tbPageChi.Visible = false;
        }

        private void tbPageChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            if (!e.Handled && e.KeyChar == (char)Keys.Enter)
            {
                if (tbPageChi.Text.Length > 0)
                {
                    if (ConvertUtil.ConvertToInt(tbPageChi.Text) <= ConvertUtil.ConvertToInt(lbTotalPageChi.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0]))
                    {
                        tbPageChi.Visible = false;
                        lbPageChi.Text = tbPageChi.Text;
                    }
                }
            }
        }
        #endregion
    }
}
