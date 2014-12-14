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
        private UserControl uc;
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
            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);
            this.BringToFront();
            sortColumn = string.Empty;
            sortOrder = Constant.SORT_ASCENDING;
            tbSearch.Text = Constant.SEARCH_USER_TIP;
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
            tbSearch.Text = Constant.SEARCH_USER_TIP;
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
            if (text == Constant.SEARCH_USER_TIP)
            {
                text = string.Empty;
            }

            int total = UserImp.GetCount(text);
            int maxPage = GetTotalPage(total) == 0 ? 1 : GetTotalPage(total);
            lbTotalPage.Text = maxPage.ToString() + Constant.PAGE_TEXT;

            if (ConvertUtil.ConvertToInt(lbPage.Text) > maxPage)
            {
                lbPage.Text = maxPage.ToString();

                return;
            }

            List<User> list = UserImp.GetList(text,
                sortColumn, sortOrder, row * (page - 1), row);
            CommonFunc.ClearlvItem(lvThongTin);

            foreach (User data in list)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add(data.Id.ToString());
                lvi.SubItems.Add((row * (page - 1) + lvThongTin.Items.Count + 1).ToString());
                lvi.SubItems.Add(data.UserGroup.Ten);
                lvi.SubItems.Add(data.Ten);
                lvi.SubItems.Add(data.UserName);
                lvi.SubItems.Add(data.DOB == null ? string.Empty : data.DOB.Value.ToString(Constant.DEFAULT_DATE_FORMAT));
                lvi.SubItems.Add(data.DienThoai == null ? string.Empty : data.DienThoai);
                lvi.SubItems.Add(data.DTDD == null ? string.Empty : data.DTDD);
                lvi.SubItems.Add(data.Email == null ? string.Empty : data.Email);
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
    }
}
