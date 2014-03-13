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

namespace Weedon
{
    public partial class UcDoanhThu : UserControl
    {
        private UserControl uc;
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private string sortColumn;
        private string sortOrder;

        private List<DTO.HoaDon> listData;
        private bool isUpdate;

        public UcDoanhThu()
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

        private void UcDoanhThu_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            //pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            this.BringToFront();

            Init();

            InitPermission();

            this.Visible = true;
        }



        #region Function
        private void Init()
        {
            dtpFilter.MaxDate = DateTime.Now;
            listData = new List<DTO.HoaDon>();

            RefreshData(dtpFilter.Value);
        }

        private void InitPermission()
        {
            if (FormMain.user.IdGroup != Constant.ID_GROUP_ADMIN)
            {
                dtpFilter.Visible = false;
            }
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(DateTime date)
        {
            tbGhiChu.Text = string.Empty;
            tbTong.Text = string.Empty;
            long total = 0;
            dgvThongTin.Rows.Clear();

            List<DTO.SanPham> listSP = SanPhamBus.GetList(string.Empty, 0, string.Empty, string.Empty, 0, 0);
            List<DTO.HoaDon> listHoaDon = HoaDonBus.GetList(string.Empty, 0, 0, date, string.Empty, string.Empty, 0, 0);

            if (listHoaDon.Count > 0)
            {
                foreach (DTO.HoaDon hoaDon in listHoaDon)
                {
                    List<DTO.HoaDonDetail> listDetail = HoaDonDetailBus.GetListByIdHoaDon(hoaDon.Id);

                    foreach (DTO.HoaDonDetail detail in listDetail)
                    {
                        bool isNew = true;
                        int soLuong = ConvertUtil.ConvertToInt(detail.SoLuong);
                        long price = ConvertUtil.ConvertToLong(detail.DonGia);
                        long money = ConvertUtil.ConvertToLong(detail.ThanhTien);

                        foreach (DataGridViewRow row in dgvThongTin.Rows)
                        {
                            if (row.Cells[colIdSanPham.Name].Value.ToString() == detail.IdSanPham.ToString())
                            {
                                soLuong += ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                                money += ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value);

                                row.Cells[colSoLuong.Name].Value = soLuong;
                                row.Cells[colThanhTien.Name].Value = money == 0 ? "0" : money.ToString(Constant.DEFAULT_FORMAT_MONEY);
                                isNew = false;
                                break;
                            }
                        }

                        if (isNew)
                        {
                            dgvThongTin.Rows.Add(detail.Id, detail.IdSanPham,
                                detail.SanPham.MaSanPham + Constant.SYMBOL_LINK_STRING + detail.SanPham.Ten,
                                price, soLuong, money);
                        }

                        total += money;
                    }

                    tbGhiChu.Text += string.IsNullOrEmpty(hoaDon.GhiChu) ? string.Empty : (hoaDon.GhiChu + Constant.SYMBOL_LINK_STRING);
                }
            }

            tbTong.Text = total.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void InsertDataHoaDon()
        {
            DTO.HoaDon data = new DTO.HoaDon();

            data.Date = dtpFilter.Value;
            data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTong.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Insert(data, FormMain.user))
            {
                InsertDataHoaDonDetail(data);
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void InsertDataHoaDonDetail(DTO.HoaDon dataHoaDon)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.HoaDonDetail data = new DTO.HoaDonDetail();

                data.IdHoaDon = dataHoaDon.Id;
                data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                data.DonGia = ConvertUtil.ConvertToLong(row.Cells[colGia.Name].Value);
                data.SoLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                data.ThanhTien = ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value);

                if (HoaDonDetailBus.Insert(data, FormMain.user))
                {
                    //
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Doanh thu"), Constant.CAPTION_CONFIRMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateDataHoaDon()
        {
            DTO.HoaDon data = HoaDonBus.GetByDate(dtpFilter.Value);

            data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbTong.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Update(data, FormMain.user))
            {
                UpdateDataHoaDonDetail(data);
            }
            else
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_ERROR, "Doanh thu") + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void UpdateDataHoaDonDetail(DTO.HoaDon dataHoaDon)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.HoaDonDetail data = HoaDonDetailBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value));

                data.SoLuong = ConvertUtil.ConvertToInt(row.Cells[colSoLuong.Name].Value);
                data.ThanhTien = ConvertUtil.ConvertToLong(row.Cells[colThanhTien.Name].Value);

                if (HoaDonDetailBus.Update(data, FormMain.user))
                {
                    //
                }
                else
                {
                    if (MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_ERROR, "Doanh thu") + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Doanh thu"), Constant.CAPTION_CONFIRMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            pbSua.Focus();

            if (MessageBox.Show("Cập nhật doanh thu?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!isUpdate)
                {
                    InsertDataHoaDon();

                    if (FormMain.user.IdGroup != Constant.ID_GROUP_ADMIN)
                    {
                        dgvThongTin.ReadOnly = true;
                        pnSua.Visible = false;

                        isUpdate = false;
                    }
                }
                else
                {
                    UpdateDataHoaDon();
                }

                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
            }
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
            if (dtpFilter.Value != null)
            {
                string path = File_Function.SaveDialog("NKBH" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

                if (path != null)
                {
                    ExportMonth(path);
                }
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

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshData(dtpFilter.Value);

            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
        }

        private ListView GetListView(DateTime date)
        {
            ListView lv = new ListView();

            try
            {
                lv.Columns.Add("barcode", "BARCODE");
                lv.Columns.Add("PLU NAME");
                lv.Columns.Add("PRICE");

                bool firstRow = true;
                int idGroup = 0;
                int days = GetDaysInMonth(date.Year, date.Month);
                List<DTO.SanPham> listSP = SanPhamBus.GetList(string.Empty, 0, string.Empty, string.Empty, 0, 0);

                if (listSP != null && listSP.Count > 0)
                {
                    foreach (DTO.SanPham data in listSP)
                    {
                        if (data.IdGroup != 7 && data.IdGroup != 9)
                        {
                            if (idGroup != data.IdGroup && !firstRow)
                            {
                                //add new blank row between 2 different group for export only
                                lv.Items.Add(new ListViewItem(new string[] { "black", string.Empty, string.Empty }));
                            }

                            firstRow = false;
                            idGroup = data.IdGroup;
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = data.MaSanPham;
                            lvi.SubItems.Add(data.Ten);
                            lvi.SubItems.Add(GiaChinhThucBus.GetByIdSanPham(data.Id) == null ? string.Empty : GiaChinhThucBus.GetByIdSanPham(data.Id).Gia.ToString());
                            lv.Items.Add(lvi);
                        }
                    }

                    lv.Items.Add(new ListViewItem(new string[] { "total", string.Empty, string.Empty }));
                    lv.Items.Add(new ListViewItem(new string[] { "orange", string.Empty, string.Empty }));

                    foreach (DTO.SanPham data in listSP)
                    {
                        if (data.IdGroup == 9)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = data.MaSanPham;
                            lvi.SubItems.Add(data.Ten);
                            lvi.SubItems.Add(GiaChinhThucBus.GetByIdSanPham(data.Id) == null ? string.Empty : GiaChinhThucBus.GetByIdSanPham(data.Id).Gia.ToString());
                            lv.Items.Add(lvi);
                        }
                    }

                    lv.Items.Add(new ListViewItem(new string[] { "red", string.Empty, string.Empty }));

                    for (int i = 1; i <= days; i++)
                    {
                        DateTime dateInMonth = new DateTime(date.Year, date.Month, i);
                        List<DTO.HoaDon> listHoaDon = HoaDonBus.GetList(string.Empty, 0, 0, dateInMonth, string.Empty, string.Empty, 0, 0);

                        if (listHoaDon.Count > 0)
                        {
                            foreach (DTO.HoaDon hoaDon in listHoaDon)
                            {
                                AddNewColumn(lv, dateInMonth.ToShortDateString());
                                int lastSubItemCount = lv.Columns.Count - 1;
                                long money = ConvertUtil.ConvertToLong(hoaDon.ThanhTien);
                                money += ConvertUtil.ConvertToLong(lv.Items[lv.Items.Count - 1].SubItems[lastSubItemCount]);
                                lv.Items[lv.Items.Count - 1].SubItems[lastSubItemCount].Text = money.ToString();
                                List<DTO.HoaDonDetail> listDetail = HoaDonDetailBus.GetListByIdHoaDon(hoaDon.Id);

                                foreach (DTO.HoaDonDetail detail in listDetail)
                                {
                                    int soLuong = ConvertUtil.ConvertToInt(detail.SoLuong);

                                    foreach (ListViewItem item in lv.Items)
                                    {
                                        if (item.Text == detail.SanPham.MaSanPham)
                                        {
                                            soLuong += ConvertUtil.ConvertToInt(item.SubItems[lastSubItemCount]);
                                            item.SubItems[lastSubItemCount].Text = soLuong.ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            AddNewColumn(lv, dateInMonth.ToShortDateString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return lv;
        }

        private void AddNewColumn(ListView lv, string name)
        {
            lv.Columns.Add(name);

            foreach (ListViewItem item in lv.Items)
            {
                item.SubItems.Add(string.Empty);
            }
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
                ExportExcel.InitNewSheet("NKBH " + date.ToString(Constant.DEFAULT_DATE_FORMAT_EXPORT));
                ExportExcel.CreateSummaryNKBH(date);
                ExportExcel.CreateDetailsTableNKBH(lv);
            }
        }

        //private void ExportDay(string path)
        //{
        //    ExportExcel.InitWorkBook();
        //    Export(path, dtpFilter.Value, GetListView(dtpFilter.Value));
        //    ExportExcel.SaveExcel(path);
        //}

        //private void ExportWeek(string path)
        //{
        //    ExportExcel.InitWorkBook();

        //    for (int day = 0; day < 7; day++)
        //    {
        //        Export(path, dtpFilter.Value.AddDays(day), GetListView(dtpFilter.Value.AddDays(day)));
        //    }

        //    ExportExcel.SaveExcel(path);
        //}

        private void ExportMonth(string path)
        {
            ExportExcel.InitWorkBook();
            Export(path, dtpFilter.Value, GetListView(dtpFilter.Value));
            ExportExcel.SaveExcel(path);
        }

        

        #region Controls
        private void dgvThongTin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ConvertUtil.ConvertToInt(dgvThongTin[colSoLuong.Name, e.RowIndex].Value) < 0)
            {
                dgvThongTin[colThanhTien.Name, e.RowIndex].Value = string.Empty;

                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);

                MessageBox.Show("Số lượng thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            dgvThongTin[colThanhTien.Name, e.RowIndex].Value =
                ConvertUtil.ConvertToDouble(dgvThongTin[colGia.Name, e.RowIndex].Value) *
                ConvertUtil.ConvertToDouble(dgvThongTin[colSoLuong.Name, e.RowIndex].Value);

            long total = 0;

            for (int i = 0; i < dgvThongTin.RowCount; i++)
            {
                total += ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value);
            }

            tbTong.Text = total.ToString(Constant.DEFAULT_FORMAT_MONEY);

            pbSua.Enabled = true;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
        }

        private void tbGhiChu_TextChanged(object sender, EventArgs e)
        {
            pbSua.Enabled = true;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
        }
        #endregion
    }
}
