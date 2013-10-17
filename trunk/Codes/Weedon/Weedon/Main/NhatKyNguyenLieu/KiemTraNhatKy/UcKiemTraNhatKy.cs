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
using System.IO;
using System.Diagnostics;

namespace Weedon
{
    public partial class UcKiemTraNhatKy : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private string sortColumn;
        private string sortOrder;

        private List<DTO.NhatKyNguyenLieu> listData;
        private bool isUpdate;

        public UcKiemTraNhatKy()
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
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcKiemTraNhatKy_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            this.BringToFront();

            Init();

            this.Visible = true;
        }



        #region Function
        private void Init()
        {
            dtpNgayTruoc.MaxDate = DateTime.Now;
            dtpNgayTruoc.Value = DateTime.Now.AddDays(-1);

            dtpNgaySau.MaxDate = DateTime.Now.AddDays(1);

            cbExportType.SelectedIndex = 0;
            listData = new List<DTO.NhatKyNguyenLieu>();

            RefreshData(string.Empty, true, dtpNgayTruoc.Value, dtpNgaySau.Value);
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(string text, bool? isActive, DateTime dateBefore, DateTime dateAfter)
        {
            lvThongTin.Items.Clear();
            ListView lv = GetListView(text, isActive, dateBefore, dateAfter, true);

            if (lv.Items.Count > 0)
            {
                foreach (ListViewItem lviTemp in lv.Items)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = lviTemp.SubItems[0].Text;
                    lvi.SubItems.Add(lviTemp.SubItems[1].Text);
                    lvi.SubItems.Add(lviTemp.SubItems[2].Text);
                    lvi.SubItems.Add(lviTemp.SubItems[3].Text);
                    lvi.SubItems.Add(lviTemp.SubItems[4].Text);
                    lvi.SubItems.Add(lviTemp.SubItems[5].Text);
                    lvi.SubItems.Add(lviTemp.SubItems[6].Text);

                    if (ConvertUtil.ConvertToDouble(lvi.SubItems[6].Text.Replace(Constant.SYMBOL_PERCENT, string.Empty)) != 0)
                    {
                        lvi.UseItemStyleForSubItems = false;
                        lvi.SubItems[5].ForeColor = Color.Red;
                        lvi.SubItems[6].ForeColor = Color.Red;
                    }

                    lvThongTin.Items.Add(lvi);
                }

                lvThongTin.Columns[3].Text = lv.Columns[3].Text;
                lvThongTin.Columns[4].Text = lv.Columns[4].Text;
            }
            else
            {
                lvThongTin.Items.Clear();

                lvThongTin.Columns[3].Text = "Tồn cuối ngày trước";
                lvThongTin.Columns[4].Text = "Tồn đầu ngày sau";
            }
        }

        private ListView GetListView(string text, bool? isActive, DateTime dateBefore, DateTime dateAfter, bool isUpdateDtp = false)
        {
            ListView lv = new ListView();

            if (NhatKyNguyenLieuBus.GetCount(text, isActive, dateBefore) > 0)
            {
                isUpdate = true;

                while (dateAfter < DateTime.Now && NhatKyNguyenLieuBus.GetCount(text, isActive, dateAfter) == 0)
                {
                    dateAfter = dateAfter.AddDays(1);
                }

                isUpdate = false;

                List<DTO.NhatKyNguyenLieu> listNLNgayTruoc = NhatKyNguyenLieuBus.GetList(text, isActive, dateBefore,
                    string.Empty, string.Empty, 0, 0);
                List<DTO.NhatKyNguyenLieu> listNLNgaySau = NhatKyNguyenLieuBus.GetList(text, isActive, dateAfter,
                    string.Empty, string.Empty, 0, 0);
                List<DTO.NhatKyNguyenLieu> listTemp = listNLNgayTruoc.Count >= listNLNgaySau.Count ? listNLNgayTruoc : listNLNgaySau;

                for (int i = 0; i < lvThongTin.Columns.Count; i++)
                {
                    lv.Columns.Add((ColumnHeader)lvThongTin.Columns[i].Clone());
                }

                for (int i = 0; i < listTemp.Count; i++)
                {
                    NhatKyNguyenLieu temp = listNLNgayTruoc.Count >= listNLNgaySau.Count ?
                        listNLNgaySau.Where(p => p.IdNguyenLieu == listTemp[i].IdNguyenLieu).FirstOrDefault() :
                        listNLNgayTruoc.Where(p => p.IdNguyenLieu == listTemp[i].IdNguyenLieu).FirstOrDefault();

                    double chenhLech = (temp == null ? 0 : temp.TonDau) - listTemp[i].TonCuoi;
                    double percentChenhLech = (((temp == null ? 0 : temp.TonDau) / listTemp[i].TonCuoi) - 1) * 100;

                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = (lv.Items.Count + 1).ToString();
                    lvi.SubItems.Add(listTemp[i].NguyenLieu.Ten);
                    lvi.SubItems.Add(listTemp[i].NguyenLieu.DonViTinh);
                    lvi.SubItems.Add(listTemp[i].TonCuoi.ToString());
                    lvi.SubItems.Add(temp == null ? "0" : temp.TonDau.ToString());
                    lvi.SubItems.Add(chenhLech.ToString(Constant.DEFAULT_FORMAT_PERCENT));

                    if (double.IsNaN(percentChenhLech))
                    {
                        lvi.SubItems.Add(Constant.NaN);
                    }
                    else if (double.IsInfinity(percentChenhLech))
                    {
                        lvi.SubItems.Add(Constant.INFINITY);
                    }
                    else
                    {
                        lvi.SubItems.Add(percentChenhLech.ToString(Constant.DEFAULT_FORMAT_PERCENT) + Constant.SYMBOL_PERCENT);
                    }

                    lv.Items.Add(lvi);
                }

                lv.Columns[3].Text = "Tồn cuối " + dateBefore.ToShortDateString();
                lv.Columns[4].Text = "Tồn đầu " + dateAfter.ToShortDateString();
            }

            if (isUpdateDtp)
            {
                dtpNgaySau.Value = dateAfter;
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

        private void Export(string path, DateTime date, ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                ExportExcel.InitNewSheet("KTNK " + date.ToString(Constant.DEFAULT_DATE_FORMAT_EXPORT));
                ExportExcel.CreateSummaryKTNK(date);
                ExportExcel.CreateDetailsTableKTNK(lv);
            }
        }

        private void ExportDay(string path)
        {
            ExportExcel.InitWorkBook();

            Export(path, dtpNgayTruoc.Value, GetListView(string.Empty, true, dtpNgayTruoc.Value, dtpNgaySau.Value));

            ExportExcel.SaveExcel(path);
        }

        private void ExportWeek(string path)
        {
            ExportExcel.InitWorkBook();

            for (int day = 0; day < 7; day++)
            {
                ListView lv = GetListView(string.Empty, true, dtpNgayTruoc.Value.AddDays(day), dtpNgaySau.Value.AddDays(day));

                Export(path, dtpNgayTruoc.Value.AddDays(day), GetListView(string.Empty, true, dtpNgayTruoc.Value.AddDays(day), dtpNgaySau.Value.AddDays(day)));
            }

            ExportExcel.SaveExcel(path);
        }

        private void ExportMonth(string path)
        {
            ExportExcel.InitWorkBook();
            DateTime dateBefore = new DateTime(dtpNgayTruoc.Value.Year, dtpNgayTruoc.Value.Month, 1);
            DateTime dateAfter = new DateTime(dtpNgayTruoc.Value.Year, dtpNgayTruoc.Value.Month, 2);

            for (int day = 0; day < GetDaysInMonth(dtpNgayTruoc.Value.Year, dtpNgayTruoc.Value.Month); day++)
            {
                ListView lv = GetListView(string.Empty, true, dateBefore.AddDays(day), dateAfter.AddDays(day));

                Export(path, dateBefore.AddDays(day), GetListView(string.Empty, true, dateBefore.AddDays(day), dateAfter.AddDays(day)));
            }

            ExportExcel.SaveExcel(path);
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
            lvEx.Columns.Add("Mã NL     ", 100, HorizontalAlignment.Center); //2
            lvEx.Columns.Add("Tên                    ", 100, HorizontalAlignment.Left); //3
            lvEx.Columns.Add("ĐVT                    ", 100, HorizontalAlignment.Left); //4
            lvEx.Columns.Add("Mô tả                              ", 100, HorizontalAlignment.Left); //5

            for (int i = 1; i < lvEx.Columns.Count; i++)
            {
                lvEx.Columns[i].VisibleChanged += new EventHandler(LvEx_ColumnVisibleChanged);
            }
        }

        private void HideColumn()
        {
            // Let us hide columns initally
            lvEx.Columns[0].Visible = false;
            lvEx.Columns[5].Visible = false;

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

            if (text == Constant.SEARCH_NGUYENLIEU_TIP)
            {
                text = string.Empty;
            }

            List<DTO.NguyenLieu> list = NguyenLieuBus.GetList(string.Empty, null,
                string.Empty, string.Empty, 0, 0);

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
                        lvi.Text = list[i].MaNguyenLieu;
                    }
                    else
                    {
                        lvi.SubItems.Add(list[i].MaNguyenLieu);
                    }
                }

                colNum++; //3

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].Ten);
                }

                colNum++; //4

                if (lvEx.Columns[colNum].Visible)
                {
                    lvi.SubItems.Add(list[i].DonViTinh);
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
            //RefreshLvEx(tbSearch.Text);
        }
        #endregion



        #region Buttons
        private void pbSua_Click(object sender, EventArgs e)
        {
            
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
            string path = File_Function.SaveDialog("KTNK " + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (path != null)
            {
                switch (cbExportType.Text)
                {
                    case Constant.DEFAULT_TYPE_DAY:
                        ExportDay(path);
                        break;

                    case Constant.DEFAULT_TYPE_WEEK:
                        ExportWeek(path);
                        break;

                    case Constant.DEFAULT_TYPE_MONTH:
                        ExportMonth(path);
                        break;
                }
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
        private void dtpNgayTruoc_ValueChanged(object sender, EventArgs e)
        {
            dtpNgaySau.MinDate = dtpNgayTruoc.Value;
            dtpNgaySau.Value = dtpNgayTruoc.Value.AddDays(1);

            RefreshData(string.Empty, true, dtpNgayTruoc.Value, dtpNgaySau.Value);
        }

        private void dtpNgaySau_ValueChanged(object sender, EventArgs e)
        {
            if (!isUpdate)
            {
                RefreshData(string.Empty, true, dtpNgayTruoc.Value, dtpNgaySau.Value);
            }
        }
        #endregion
    }
}
