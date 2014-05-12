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
    public partial class UcKiemTraChenhLech : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;
        private List<DTO.NhatKyNguyenLieu> listData;

        public UcKiemTraChenhLech()
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

        private void UcKiemTraChenhLech_Load(object sender, EventArgs e)
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
            dtpFilter.MaxDate = DateTime.Now;
            listData = new List<DTO.NhatKyNguyenLieu>();
            cbExportType.SelectedIndex = 0;

            RefreshData(string.Empty, true, dtpFilter.Value);
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(string text, bool? isActive, DateTime date)
        {
            lvThongTin.Items.Clear();
            ListView lv = GetListView(text, isActive, date);

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

                if (ConvertUtil.ConvertToDouble(lvi.SubItems[6].Text.Replace(Constant.SYMBOL_PERCENT, string.Empty)) >
                    ConvertUtil.ConvertToDouble(SettingBus.GetById(1).Value) ||
                    ConvertUtil.ConvertToDouble(lvi.SubItems[6].Text.Replace(Constant.SYMBOL_PERCENT, string.Empty)) <
                    -ConvertUtil.ConvertToDouble(SettingBus.GetById(1).Value))
                {
                    lvi.UseItemStyleForSubItems = false;
                    lvi.SubItems[5].ForeColor = Color.Red;
                    lvi.SubItems[6].ForeColor = Color.Red;
                }

                lvThongTin.Items.Add(lvi);
            }
        }

        private ListView GetListView(string text, bool? isActive, DateTime date)
        {
            ListView lv = new ListView();

            if (NhatKyNguyenLieuBus.GetCount(text, isActive, date) > 0 && HoaDonBus.GetByDate(date) != null)
            {
                List<DTO.NhatKyNguyenLieu> listNLLyThuyet = new List<DTO.NhatKyNguyenLieu>();
                List<DTO.NhatKyNguyenLieu> listNLThucTe = NhatKyNguyenLieuBus.GetList(string.Empty, true, date,
                    string.Empty, string.Empty, 0, 0);
                List<DTO.HoaDon> listHoaDon = HoaDonBus.GetList(string.Empty, 0, 0, date, string.Empty, string.Empty, 0, 0);

                foreach (DTO.NhatKyNguyenLieu data in listNLThucTe)
                {
                    DTO.NhatKyNguyenLieu dataNKNL = new DTO.NhatKyNguyenLieu();

                    dataNKNL.IdNguyenLieu = data.IdNguyenLieu;
                    dataNKNL.SuDung = 0;

                    listNLLyThuyet.Add(dataNKNL);
                }

                foreach (DTO.HoaDon hoaDon in listHoaDon)
                {
                    List<DTO.HoaDonDetail> listHoaDonDetail = HoaDonDetailBus.GetListByIdHoaDon(hoaDon.Id);

                    foreach (DTO.HoaDonDetail data in listHoaDonDetail)
                    {
                        List<DTO.DinhLuong> listDL = DinhLuongBus.GetListByIdSP(data.IdSanPham);

                        foreach (DTO.DinhLuong dataDL in listDL)
                        {
                            listNLLyThuyet.Where(p => p.IdNguyenLieu == dataDL.IdNguyenLieu).FirstOrDefault().SuDung += dataDL.SoLuong * data.SoLuong;
                        }
                    }
                }

                for (int i = 0; i < lvThongTin.Columns.Count; i++)
                {
                    lv.Columns.Add((ColumnHeader)lvThongTin.Columns[i].Clone());
                }

                for (int i = 0; i < listNLThucTe.Count; i++)
                {
                    double lyThuyet = listNLLyThuyet[i].SuDung;
                    double thucTe = listNLThucTe[i].SuDung;
                    double chenhLech = thucTe - lyThuyet;
                    double percentChenhLech = ((thucTe / lyThuyet) - 1) * 100;

                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = (lv.Items.Count + 1).ToString();
                    lvi.SubItems.Add(listNLThucTe[i].NguyenLieu.Ten);
                    lvi.SubItems.Add(listNLThucTe[i].NguyenLieu.DonViTinh);
                    lvi.SubItems.Add(lyThuyet.ToString());
                    lvi.SubItems.Add(thucTe.ToString());
                    lvi.SubItems.Add(chenhLech.ToString());

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
                ExportExcel.InitNewSheet("KTCL " + date.ToString(Constant.DEFAULT_DATE_FORMAT_EXPORT));
                ExportExcel.CreateSummaryKTCL(date);
                ExportExcel.CreateDetailsTableKTCL(lv);
            }
        }

        private void ExportDay(string path)
        {
            ExportExcel.InitWorkBook();

            Export(path, dtpFilter.Value, GetListView(string.Empty, true, dtpFilter.Value));

            ExportExcel.SaveExcel(path);
        }

        private void ExportWeek(string path)
        {
            ExportExcel.InitWorkBook();

            for (int day = 0; day < 7; day++)
            {
                ListView lv = GetListView(string.Empty, true, dtpFilter.Value.AddDays(day));

                Export(path, dtpFilter.Value.AddDays(day), lv);
            }

            ExportExcel.SaveExcel(path);
        }

        private void ExportMonth(string path)
        {
            ExportExcel.InitWorkBook();
            DateTime date = new DateTime(dtpFilter.Value.Year, dtpFilter.Value.Month, 1);

            for (int day = 0; day < GetDaysInMonth(dtpFilter.Value.Year, dtpFilter.Value.Month); day++)
            {
                ListView lv = GetListView(string.Empty, true, date.AddDays(day));

                Export(path, date.AddDays(day), lv);
            }

            ExportExcel.SaveExcel(path);
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
            string path = File_Function.SaveDialog("KTCL" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

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

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            RefreshData(string.Empty, true, dtpFilter.Value);

            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
        }

        private void cbTimeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData(string.Empty, true, dtpFilter.Value);
        }



        #region Controls
        
        #endregion
    }
}
