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
    public partial class UcNhatKyNguyenLieu : UserControl
    {
        private ListViewEx lvEx;

        private List<DTO.NhatKyNguyenLieu> listData;
        private bool isUpdate;

        public UcNhatKyNguyenLieu()
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

        private void UcNhatKyNguyenLieu_Load(object sender, EventArgs e)
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

            RefreshData(string.Empty, true, dtpFilter.Value);

            if (dgvThongTin.Rows.Count > 0)
            {
                btSaveSetting.Enabled = true;
            }
            else
            {
                btSaveSetting.Enabled = false;
            }
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(string text, bool? isActive, DateTime date)
        {
            dgvThongTin.Rows.Clear();

            if (NhatKyNguyenLieuBus.GetCount(text, isActive, date) > 0)
            {
                List<DTO.NhatKyNguyenLieu> list = NhatKyNguyenLieuBus.GetList(text, isActive, date,
                string.Empty, string.Empty, 0, 0);

                DTO.Setting dataSetting = SettingBus.GetById(2);

                if (dataSetting != null && dataSetting.IsActive)
                {
                    string[] arrayId = dataSetting.Value.Split(new string[] { Constant.SEPERATE_STRING }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string id in arrayId)
                    {
                        DTO.NhatKyNguyenLieu data = list.Where(p => p.IdNguyenLieu == ConvertUtil.ConvertToInt(id)).FirstOrDefault();

                        if (data != null)
                        {
                            dgvThongTin.Rows.Add(data.Id, data.IdNguyenLieu, data.NguyenLieu.Ten, data.NguyenLieu.DonViTinh,
                                data.NguyenLieu.HanMuc == 0 ? string.Empty : data.NguyenLieu.HanMuc.ToString(),
                                data.TonDau, data.Nhap, data.Huy, data.TonCuoi, data.SuDung, data.GhiChu);

                            if (data.TonCuoi < data.NguyenLieu.HanMuc)
                            {
                                dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.BackColor = Color.Red;
                                dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }
                    }

                    foreach (DTO.NhatKyNguyenLieu data in list)
                    {
                        if (arrayId.Where(p => p == data.IdNguyenLieu.ToString()).Count() == 0)
                        {
                            dgvThongTin.Rows.Add(data.Id, data.IdNguyenLieu, data.NguyenLieu.Ten, data.NguyenLieu.DonViTinh,
                                data.NguyenLieu.HanMuc == 0 ? string.Empty : data.NguyenLieu.HanMuc.ToString(),
                                data.TonDau, data.Nhap, data.Huy, data.TonCuoi, data.SuDung, data.GhiChu);

                            if (data.TonCuoi < data.NguyenLieu.HanMuc)
                            {
                                dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.BackColor = Color.Red;
                                dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }
                    }
                }
                else
                {
                    foreach (DTO.NhatKyNguyenLieu data in list)
                    {
                        dgvThongTin.Rows.Add(data.Id, data.IdNguyenLieu, data.NguyenLieu.Ten, data.NguyenLieu.DonViTinh,
                            data.NguyenLieu.HanMuc == 0 ? string.Empty : data.NguyenLieu.HanMuc.ToString(),
                            data.TonDau, data.Nhap, data.Huy, data.TonCuoi, data.SuDung, data.GhiChu);

                        if (data.TonCuoi < data.NguyenLieu.HanMuc)
                        {
                            dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.BackColor = Color.Red;
                            dgvThongTin.Rows[dgvThongTin.RowCount - 1].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }

                isUpdate = true;
            }
            else
            {
                List<DTO.NguyenLieu> list = NguyenLieuBus.GetList(text, isActive,
                string.Empty, string.Empty, 0, 0);

                DTO.Setting dataSetting = SettingBus.GetById(2);

                if (dataSetting != null && dataSetting.IsActive)
                {
                    string[] arrayId = dataSetting.Value.Split(new string[] { Constant.SEPERATE_STRING }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string id in arrayId)
                    {
                        DTO.NguyenLieu data = list.Where(p => p.Id == ConvertUtil.ConvertToInt(id)).FirstOrDefault();

                        if (data != null)
                        {
                            dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten, data.DonViTinh,
                                data.HanMuc == 0 ? string.Empty : data.HanMuc.ToString(),
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                        }
                    }

                    foreach (DTO.NguyenLieu data in list)
                    {
                        if (arrayId.Where(p => p == data.Id.ToString()).Count() == 0)
                        {
                            dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten, data.DonViTinh,
                                data.HanMuc == 0 ? string.Empty : data.HanMuc.ToString(),
                                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                        }
                    }
                }
                else
                {
                    foreach (DTO.NguyenLieu data in list)
                    {
                        dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten, data.DonViTinh, data.HanMuc == 0 ? string.Empty : data.HanMuc.ToString(),
                            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    }
                }

                isUpdate = false;
            }
        }

        private void InsertData()
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.NhatKyNguyenLieu data = new DTO.NhatKyNguyenLieu();

                data.IdNguyenLieu = ConvertUtil.ConvertToInt(row.Cells[colIdNguyenLieu.Name].Value);
                data.Date = dtpFilter.Value;
                data.TonDau = ConvertUtil.ConvertToDouble(row.Cells[colTonDau.Name].Value);
                data.Nhap = ConvertUtil.ConvertToDouble(row.Cells[colNhap.Name].Value);
                data.Huy = ConvertUtil.ConvertToDouble(row.Cells[colHuy.Name].Value);
                data.TonCuoi = ConvertUtil.ConvertToDouble(row.Cells[colTonCuoi.Name].Value);
                data.SuDung = ConvertUtil.ConvertToDouble(row.Cells[colSuDung.Name].Value);
                data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                if (NhatKyNguyenLieuBus.Insert(data, FormMain.user))
                {
                    //this.Dispose();
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        //this.Dispose();

                        return;
                    }
                }
            }

            MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Nhật ký"), Constant.CAPTION_INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData(string.Empty, true, dtpFilter.Value);
        }

        private void UpdateData()
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.NhatKyNguyenLieu data = NhatKyNguyenLieuBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value));

                data.TonDau = ConvertUtil.ConvertToDouble(row.Cells[colTonDau.Name].Value);
                data.Nhap = ConvertUtil.ConvertToDouble(row.Cells[colNhap.Name].Value);
                data.Huy = ConvertUtil.ConvertToDouble(row.Cells[colHuy.Name].Value);
                data.TonCuoi = ConvertUtil.ConvertToDouble(row.Cells[colTonCuoi.Name].Value);
                data.SuDung = ConvertUtil.ConvertToDouble(row.Cells[colSuDung.Name].Value);
                data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                if (NhatKyNguyenLieuBus.Update(data, FormMain.user))
                {
                    //this.Dispose();
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        //this.Dispose();

                        return;
                    }
                }
            }

            MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Nhật ký"), Constant.CAPTION_INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshData(string.Empty, true, dtpFilter.Value);
        }

        private void UpdateSetting()
        {
            DTO.Setting dataSetting = SettingBus.GetById(2);

            string order = string.Empty;

            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                order += row.Cells[colIdNguyenLieu.Name].Value.ToString() + Constant.SEPERATE_STRING;
            }

            if (dataSetting == null)
            {
                dataSetting = new DTO.Setting();

                dataSetting.Ten = "Thứ tự NL";
                dataSetting.Value = order;
                dataSetting.MoTa = "Sắp xếp thứ tự NL theo bảng Nhật Ký NL";
                dataSetting.IsActive = true;

                if (SettingBus.Insert(dataSetting, FormMain.user))
                {
                    MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Thứ tự NL"), Constant.CAPTION_INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (dataSetting.Value != order)
            {
                if (MessageBox.Show("Cập nhật thứ tự NL?", Constant.CAPTION_CONFIRMATION,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dataSetting.Value = order;
                    dataSetting.IsActive = true;

                    if (SettingBus.Update(dataSetting, FormMain.user))
                    {
                        MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Thứ tự NL"), Constant.CAPTION_INFORMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

            if (MessageBox.Show("Cập nhật nhật ký nguyên liệu?", Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!isUpdate)
                {
                    InsertData();
                }
                else
                {
                    UpdateData();
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
            if (dgvThongTin.Rows.Count > 0)
            {
                string path = File_Function.SaveDialog("NKNL" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

                if (path != null)
                {
                    ExportDay(path);
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
            RefreshData(string.Empty, true, dtpFilter.Value);

            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);
        }



        #region Controls
        private void dgvThongTin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvThongTin[colSuDung.Name, e.RowIndex].Value =
                ConvertUtil.ConvertToDouble(dgvThongTin[colTonDau.Name, e.RowIndex].Value) +
                ConvertUtil.ConvertToDouble(dgvThongTin[colNhap.Name, e.RowIndex].Value) -
                ConvertUtil.ConvertToDouble(dgvThongTin[colTonCuoi.Name, e.RowIndex].Value);

            if (ConvertUtil.ConvertToDouble(dgvThongTin[colSuDung.Name, e.RowIndex].Value) < 0)
            {
                MessageBox.Show("Sử dụng thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ConvertUtil.ConvertToDouble(dgvThongTin[colTonDau.Name, e.RowIndex].Value) < 0)
            {
                MessageBox.Show("Tồn đầu thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ConvertUtil.ConvertToDouble(dgvThongTin[colNhap.Name, e.RowIndex].Value) < 0)
            {
                MessageBox.Show("Nhập thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ConvertUtil.ConvertToDouble(dgvThongTin[colHuy.Name, e.RowIndex].Value) < 0)
            {
                MessageBox.Show("Hủy thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ConvertUtil.ConvertToDouble(dgvThongTin[colTonCuoi.Name, e.RowIndex].Value) < 0)
            {
                MessageBox.Show("Tồn cuối thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //for (int i = 0; i < dgvThongTin.RowCount; i++)
            //{
            //    if (string.IsNullOrEmpty(dgvThongTin[colTonDau.Name, i].Value.ToString()) ||
            //        string.IsNullOrEmpty(dgvThongTin[colTonCuoi.Name, i].Value.ToString()))
            //    {
            //        pbSua.Enabled = false;
            //        pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);

            //        return;
            //    }
            //}

            pbSua.Enabled = true;
            pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
        }
        #endregion

        private void btUp_Click(object sender, EventArgs e)
        {
            if (dgvThongTin.SelectedRows.Count == 0 && dgvThongTin.CurrentCell != null)
            {
                dgvThongTin.Rows[dgvThongTin.CurrentCell.RowIndex].Selected = true;
            }

            if (dgvThongTin.SelectedRows.Count > 0 && dgvThongTin.SelectedRows[0].Index - 1 >= 0)
            {
                SwapRow(dgvThongTin.SelectedRows[0].Index, dgvThongTin.SelectedRows[0].Index - 1);
            }
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            if (dgvThongTin.SelectedRows.Count == 0 && dgvThongTin.CurrentCell != null)
            {
                dgvThongTin.Rows[dgvThongTin.CurrentCell.RowIndex].Selected = true;
            }

            if (dgvThongTin.SelectedRows.Count > 0 && dgvThongTin.SelectedRows[0].Index + 1 < dgvThongTin.Rows.Count)
            {
                SwapRow(dgvThongTin.SelectedRows[0].Index, dgvThongTin.SelectedRows[0].Index + 1);
            }
        }

        private void SwapRow(int currentIndex, int swapIndex)
        {
            List<string> rowValues = new List<string>();

            for (int i = 0; i < dgvThongTin.Rows[currentIndex].Cells.Count; i++)
            {
                rowValues.Add(dgvThongTin.Rows[currentIndex].Cells[i].Value.ToString());
            }

            dgvThongTin.Rows[currentIndex].SetValues(
                dgvThongTin[colId.Name, swapIndex].Value,
                dgvThongTin[colIdNguyenLieu.Name, swapIndex].Value,
                dgvThongTin[colNguyenLieu.Name, swapIndex].Value,
                dgvThongTin[colDVT.Name, swapIndex].Value,
                dgvThongTin[colTonDau.Name, swapIndex].Value,
                dgvThongTin[colNhap.Name, swapIndex].Value,
                dgvThongTin[colHuy.Name, swapIndex].Value,
                dgvThongTin[colTonCuoi.Name, swapIndex].Value,
                dgvThongTin[colSuDung.Name, swapIndex].Value,
                dgvThongTin[colGhiChu.Name, swapIndex].Value);

            for (int i = 0; i < rowValues.Count; i++)
            {
                dgvThongTin.Rows[swapIndex].Cells[i].Value = rowValues[i];
            }

            dgvThongTin.Rows[swapIndex].Selected = true;
        }

        private void btSaveSetting_Click(object sender, EventArgs e)
        {
            UpdateSetting();
        }

        private void ExportDay(string path)
        {
            ExportExcel.InitWorkBook();
            Export(path, dtpFilter.Value, GetListView(string.Empty, true, dtpFilter.Value));
            ExportExcel.SaveExcel(path);
        }

        private void Export(string path, DateTime date, ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                ExportExcel.InitNewSheet("NKNL " + date.ToString(Constant.DEFAULT_DATE_FORMAT_EXPORT));
                ExportExcel.CreateSummaryNKNL(date);
                ExportExcel.CreateDetailsTableNKNL(lv);
            }
        }

        private ListView GetListView(string text, bool? isActive, DateTime date)
        {
            ListView lv = new ListView();
            lv.Columns.Add("STT");

            for (int i = 2; i < dgvThongTin.Columns.Count; i++)
            {
                lv.Columns.Add(dgvThongTin.Columns[i].Name, dgvThongTin.Columns[i].HeaderText);
            }

            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                //if (row.DefaultCellStyle.BackColor.Equals(Color.Red))
                //{
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = (lv.Items.Count + 1).ToString();

                    for (int i = 2; i < dgvThongTin.Columns.Count; i++)
                    {
                        lvi.SubItems.Add(row.Cells[i].Value.ToString());
                        lvi.SubItems[lvi.SubItems.Count - 1].Name = dgvThongTin.Columns[i].Name;
                    }

                    lv.Items.Add(lvi);
                //}
            }

            return lv;
        }
    }
}
