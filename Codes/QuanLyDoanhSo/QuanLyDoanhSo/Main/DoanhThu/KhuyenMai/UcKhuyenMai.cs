﻿using System;
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
    public partial class UcKhuyenMai : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private List<DTO.KhuyenMai> listData;

        public UcKhuyenMai()
        {
            InitializeComponent();
        }

        private void LoadResource()
        {
            try
            {
                pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
                pbExcel.Image = Image.FromFile(ConstantResource.CHUC_NANG_EXPORT_EXCEL);

                //pbTitle.Image = Image.FromFile(@"Resources\NhanSu\icon_quanlyma_title.png");
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcKhuyenMai_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            LoadResource();

            pnQuanLy.Location = CommonFunc.SetWidthCenter(this.Size, pnQuanLy.Size, pnSelect.Bottom);

            this.BringToFront();

            FormMain.isEditing = true;

            Init();

            InitPermission();

            this.Visible = true;
        }



        #region Function
        private void Init()
        {
            listData = new List<DTO.KhuyenMai>();

            RefreshData(string.Empty, 0);
        }

        private void InitPermission()
        {
            if (FormMain.user.IdUserGroup != Constant.ID_GROUP_ADMIN)
            {
                dgvThongTin.ReadOnly = true;
                pnSua.Visible = false;
            }
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(string text, int idGroup)
        {
            dgvThongTin.Rows.Clear();
            List<DTO.SanPham> list = SanPhamBus.GetListByGia();

            foreach (DTO.SanPham data in list)
            {
                DTO.KhuyenMai dataKM = KhuyenMaiBus.GetByIdSanPham(data.Id);

                if (dataKM != null)
                {
                    dgvThongTin.Rows.Add(dataKM.Id, dataKM.IdSanPham, dataKM.SanPham.Ten, dataKM.SoLuongSanPham,
                        null, dataKM.SoLuongSanPhamKhuyenMai,
                        dataKM.DonViLamTron,
                        dataKM.GhiChu);
                }
                else
                {
                    dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten, 0,
                        null, 0,
                        0,
                        string.Empty);
                }

                List<DTO.SanPham> listDataSP = SanPhamBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colSanPhamKhuyenMai.Name];
                cell.DataSource = listDataSP;
                //cell.Value = listDataSP.FirstOrDefault().Id;
                cell.ValueMember = "Id";
                cell.DisplayMember = "Ten";
                //dgvThongTin.CurrentCell = cell;

                if (dataKM != null)
                {
                    cell.Value = dataKM.IdSanPhamKhuyenMai;
                }
            }
        }

        private void AddNewRow()
        {
            //dgvThongTin.Rows.Add();
            //List<DTO.NguyenLieu> listData = NguyenLieuBus.GetList(string.Empty, null, string.Empty, string.Empty, 0, 0);
            //DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colTen.Name];

            //cell.DataSource = listData;
            //cell.Value = listData.FirstOrDefault().Id;
            //cell.ValueMember = "Id";
            //cell.DisplayMember = "Ten";

            //dgvThongTin.CurrentCell = cell;
            //UpdateRowData();
            //dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colUocLuong.Name].Value = 1;
        }

        private void UpdateRowData()
        {
            //int rowIndex = dgvThongTin.CurrentCell.RowIndex;

            //if (dgvThongTin.CurrentCell.ColumnIndex == dgvThongTin.Columns[colTen.Name].Index)
            //{
            //    DTO.NguyenLieu data = NguyenLieuBus.GetById(ConvertUtil.ConvertToInt(dgvThongTin[colTen.Name, rowIndex].Value));
            //    dgvThongTin[colIdNL.Name, rowIndex].Value = data.Id;
            //    dgvThongTin[colMa.Name, rowIndex].Value = data.MaNguyenLieu;
            //    dgvThongTin[colDonVi.Name, rowIndex].Value = data.DonViTinh;
            //    dgvThongTin.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void UpdateData()
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                if (ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPham.Name].Value) != 0 &&
                    ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPhamKhuyenMai.Name].Value) != 0)
                {
                    if (row.Cells[colId.Name].Value != null && !string.IsNullOrEmpty(row.Cells[colId.Name].Value.ToString()))
                    {
                        DTO.KhuyenMai data = KhuyenMaiBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value));
                        data.SoLuongSanPham = row.Cells[colSoLuongSanPham.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPham.Name].Value);
                        data.SanPham1 = SanPhamBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colSanPhamKhuyenMai.Name].Value));
                        data.SoLuongSanPhamKhuyenMai = row.Cells[colSoLuongSanPhamKhuyenMai.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPhamKhuyenMai.Name].Value);
                        data.DonViLamTron = row.Cells[colDonViLamTron.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToDouble(row.Cells[colDonViLamTron.Name].Value);
                        data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                        if (KhuyenMaiBus.Update(data))
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
                    else
                    {
                        DTO.KhuyenMai data = new DTO.KhuyenMai();
                        data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                        data.SoLuongSanPham = row.Cells[colSoLuongSanPham.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPham.Name].Value);
                        data.IdSanPhamKhuyenMai = ConvertUtil.ConvertToInt(row.Cells[colSanPhamKhuyenMai.Name].Value);
                        data.SoLuongSanPhamKhuyenMai = row.Cells[colSoLuongSanPhamKhuyenMai.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToInt(row.Cells[colSoLuongSanPhamKhuyenMai.Name].Value);
                        data.DonViLamTron = row.Cells[colDonViLamTron.Name].Value == null ? 0 :
                            ConvertUtil.ConvertToDouble(row.Cells[colDonViLamTron.Name].Value);
                        data.GhiChu = row.Cells[colGhiChu.Name].Value == null ? string.Empty : row.Cells[colGhiChu.Name].Value.ToString();

                        if (KhuyenMaiBus.Insert(data))
                        {
                            //this.Dispose();
                        }
                        else
                        {
                            if (MessageBox.Show(Constant.MESSAGE_UPDATE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                //this.Dispose();

                                return;
                            }
                        }
                    }
                }
            }

            //pbSua.Enabled = false;
            //pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);

            MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Khuyến mãi"), Constant.CAPTION_CONFIRMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //lvEx.Items.Clear();

            //if (text == Constant.SEARCH_NGUYENLIEU_TIP)
            //{
            //    text = string.Empty;
            //}

            //List<DTO.NguyenLieu> list = NguyenLieuBus.GetList(string.Empty, null,
            //    string.Empty, string.Empty, 0, 0);

            //for (int i = 0; i < list.Count; i++)
            //{
            //    ListViewItem lvi = new ListViewItem();
            //    int colNum = 0;

            //    //if (lvEx.Columns[0].Visible)
            //    //{
            //    //    lvi.Text = list_dto[i].Ma;
            //    //}

            //    colNum++; //1

            //    if (lvEx.Columns[colNum].Visible)
            //    {
            //        lvi.Text = (i + 1).ToString();
            //    }

            //    colNum++; //2

            //    if (lvEx.Columns[colNum].Visible)
            //    {
            //        if (!lvEx.Columns[1].Visible)
            //        {
            //            lvi.Text = list[i].MaNguyenLieu;
            //        }
            //        else
            //        {
            //            lvi.SubItems.Add(list[i].MaNguyenLieu);
            //        }
            //    }

            //    colNum++; //3

            //    if (lvEx.Columns[colNum].Visible)
            //    {
            //        lvi.SubItems.Add(list[i].Ten);
            //    }

            //    colNum++; //4

            //    if (lvEx.Columns[colNum].Visible)
            //    {
            //        lvi.SubItems.Add(list[i].DonViTinh);
            //    }

            //    colNum++; //5

            //    if (lvEx.Columns[colNum].Visible)
            //    {
            //        lvi.SubItems.Add(list[i].MoTa);
            //    }

            //    lvEx.Items.Add(lvi);
            //}
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

            if (MessageBox.Show("Cập nhật khuyến mãi?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UpdateData();
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
                NewLvEx(Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Width, Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Height);
                LoadLvExLData();
                HideColumn();

                //RefreshLvEx(tbSearch.Text);

                //FormExportExcel frm = new FormExportExcel(Constant.DEFAULT_TYPE_EXPORT_NGUYENLIEU, Constant.DEFAULT_SHEET_NAME_EXPORT_NGUYENLIEU, Constant.DEFAULT_TYPE_EXPORT_NGUYENLIEU,
                //    lvEx, soSP);
                //frm.ShowDialog();
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
        private void dgvThongTin_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (dgvThongTin[colGia.Name, e.RowIndex].Value != null)
            //{
            //    dgvThongTin[colGia.Name, e.RowIndex].Value = dgvThongTin[colGia.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty);
            //}
        }

        private void dgvThongTin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvThongTin[colGia.Name, e.RowIndex].Value != null)
            //{
            //    long money = ConvertUtil.ConvertToLong(dgvThongTin[colGia.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            //    dgvThongTin[colGia.Name, e.RowIndex].Value = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            //}

            //bool isValidated = true;

            //if (ConvertUtil.ConvertToDouble(dgvThongTin[colGia.Name, e.RowIndex].Value) < 0)
            //{
            //    MessageBox.Show("Giá thấp hơn quy định!", Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    isValidated = false;
            //}

            //if (!isValidated)
            //{
            //    pbSua.Enabled = false;
            //    pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT_DISABLE);

            //    return;
            //}

            //pbSua.Enabled = true;
            //pbSua.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_EDIT);
        }
        #endregion
    }
}
