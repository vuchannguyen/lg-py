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
    public partial class UcBanHang2 : UserControl
    {
        private const int row = Constant.DEFAULT_ROW;

        private ListViewEx lvEx;

        private List<DTO.BanHang> listData;

        public UcBanHang2()
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

        private void UcBanHang_Load(object sender, EventArgs e)
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
            InitTree();
            //GetDataBanHang();
            //GetListUser();
        }

        private void GetDataBanHang()
        {
            tbThanhTien.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
            dgvThongTin.Rows.Clear();
            DTO.BanHang banHang = null;
            DTO.BanHang banHangNgayTruoc = null;

            if (tvUser.SelectedNode != null)
            {
                banHang = BanHangBus.GetByIdUserAndDate(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag), dtpFilter.Value);
            }

            for (int i = 0; i < 7; i++)
            {
                banHangNgayTruoc = BanHangBus.GetByIdUserAndDate(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag), dtpFilter.Value.AddDays(-i - 1));

                if (banHangNgayTruoc != null)
                {
                    break;
                }
            }

            if (banHang != null)
            {
                List<DTO.BanHangChiTiet> listData = BanHangChiTietBus.GetListByIdBanHang(banHang.Id);
                //List<DTO.BanHangChiTiet> listDataNgayTruoc = BanHangChiTietBus.GetListByIdBanHang(banHangNgayTruoc.Id);

                foreach (DTO.BanHangChiTiet data in listData)
                {
                    int soLuong = data.Ban;
                    int price = data.Gia;
                    int money = price * soLuong;
                    dgvThongTin.Rows.Add(data.Id, data.IdSanPham, data.SanPham.Ten,
                        price.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        data.TonDau,
                        data.Nhan, soLuong, data.TonCuoi, data.ThuHoi,
                        money.ToString(Constant.DEFAULT_FORMAT_MONEY));
                }

                tbGhiChu.Text = banHang.GhiChu;
                CalculateMoney();
            }
            else
            {
                List<DTO.SanPham> listData = SanPhamBus.GetListByGia();
                List<DTO.BanHangChiTiet> listDataNgayTruoc = null;

                if (banHangNgayTruoc != null)
                {
                    listDataNgayTruoc = BanHangChiTietBus.GetListByIdBanHang(banHangNgayTruoc.Id);
                }

                foreach (DTO.SanPham data in listData)
                {
                    dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten,
                        data.Gia.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        listDataNgayTruoc == null ? 0 : listDataNgayTruoc.Where(p => p.IdSanPham == data.Id).FirstOrDefault().TonCuoi,
                        0, 0, 0, 0,
                        0);
                }
            }

            dgvThongTin.Rows[0].Selected = true;
            dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
        }

        private void GetDataBanHangToTruong(DTO.User toTruong)
        {
            tbThanhTien.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
            dgvThongTin.Rows.Clear();
            List<DTO.User> listUser = UserBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            foreach (DTO.User data1 in listUser)
            {
                DTO.BanHang banHang = null;

                if (data1.IdUserGroup == 2 && data1.To == toTruong.To)
                {
                    banHang = BanHangBus.GetByIdUserAndDate(data1.Id, dtpFilter.Value);
                }

                if (banHang != null)
                {
                    List<DTO.BanHangChiTiet> listData = BanHangChiTietBus.GetListByIdBanHang(banHang.Id);

                    if (dgvThongTin.Rows.Count > 0)
                    {
                        for (int i = 0; i < listData.Count; i++)
                        {
                            dgvThongTin[colGia.Name, i].Value = listData[i].Gia;
                            dgvThongTin[colTonDau.Name, i].Value = listData[i].TonDau + ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                            dgvThongTin[colNhan.Name, i].Value = listData[i].Nhan + ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                            dgvThongTin[colBan.Name, i].Value = listData[i].Ban + ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                            dgvThongTin[colThuHoi.Name, i].Value = listData[i].ThuHoi + ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                            dgvThongTin[colTonCuoi.Name, i].Value = listData[i].TonCuoi + ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                            dgvThongTin[colThanhTien.Name, i].Value = listData[i].ThanhTien + ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                        }
                    }
                    else
                    {
                        foreach (DTO.BanHangChiTiet data in listData)
                        {
                            int soLuong = data.Ban;
                            int price = data.Gia;
                            int money = price * soLuong;
                            dgvThongTin.Rows.Add(data.Id, data.IdSanPham, data.SanPham.Ten,
                                price.ToString(Constant.DEFAULT_FORMAT_MONEY), data.TonDau, data.Nhan, soLuong, data.ThuHoi, data.TonCuoi,
                                money.ToString(Constant.DEFAULT_FORMAT_MONEY));
                        }
                    }

                    CalculateMoney();
                }
            }

            if (dgvThongTin.Rows.Count == 0)
            {
                List<DTO.SanPham> listData = SanPhamBus.GetListByGia();

                foreach (DTO.SanPham data in listData)
                {
                    dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten,
                        data.Gia.ToString(Constant.DEFAULT_FORMAT_MONEY), 0, 0, 0, 0, 0,
                        0);
                }
            }

            dgvThongTin.Rows[0].Selected = true;
            dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
        }

        private void GetListUser()
        {
            //List<DTO.User> listData = UserBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);
            //cbNhanVien.Items.Clear();

            //foreach (DTO.User data in listData)
            //{
            //    cbNhanVien.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            //}

            //if (cbNhanVien.Items.Count > 0)
            //{
            //    cbNhanVien.SelectedIndex = 0;
            //}
        }

        private void InitTree()
        {
            try
            {
                if (tvUser.Nodes != null)
                {
                    tvUser.Nodes.Clear();
                }

                TreeNode root = new TreeNode("Tất cả");
                root.Tag = "all";
                tvUser.Nodes.Add(root);

                List<DTO.User> listData = UserBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);
                int n = listData.Count;

                foreach (DTO.User data in listData)
                {
                    if (data.IdUserGroup == 3)
                    {
                        TreeNode toTruong = new TreeNode("Tổ " + data.To.ToString() + ": " + data.Ten);
                        toTruong.Tag = data.Id;
                        root.Nodes.Add(toTruong);

                        foreach (DTO.User data1 in listData)
                        {
                            if (data1.IdUserGroup == 2 && data1.To == data.To)
                            {
                                TreeNode toVien = new TreeNode(data1.Ten);
                                toVien.Tag = data1.Id;
                                toTruong.Nodes.Add(toVien);
                            }
                        }
                    }
                }

                TreeNode khac = new TreeNode("Còn lại");
                khac.Tag = "other";
                root.Nodes.Add(khac);

                foreach (DTO.User data in listData)
                {
                    if (data.IdUserGroup == 2 && data.To == null)
                    {
                        TreeNode toVien = new TreeNode(data.Ten);
                        toVien.Tag = data.Id;
                        khac.Nodes.Add(toVien);
                    }
                }

                tvUser.ExpandAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitPermission()
        {
            if (FormMain.user.IdUserGroup != Constant.ID_GROUP_ADMIN)
            {
                //dgvThongTin.ReadOnly = true;
                pnSua.Visible = false;
            }
        }

        private void uc_Disposed(object sender, EventArgs e)
        {
            FormMain.isEditing = false;
        }

        private void RefreshData(string text, int idGroup)
        {
            //dgvThongTin.Rows.Clear();
            //List<DTO.SanPham> list = SanPhamBus.GetList(text, string.Empty, string.Empty, 0, 0);

            //foreach (DTO.SanPham data in list)
            //{
            //    DTO.KhuyenMai dataKM = KhuyenMaiBus.GetByIdSanPham(data.Id);

            //    if (dataKM != null)
            //    {
            //        dgvThongTin.Rows.Add(dataKM.Id, dataKM.IdSanPham, dataKM.SanPham.Ten, dataKM.SoLuongSanPham,
            //            null, dataKM.SoLuongSanPhamKhuyenMai,
            //            dataKM.DonViLamTron,
            //            dataKM.GhiChu);
            //    }
            //    else
            //    {
            //        dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten, 0,
            //            null, 0,
            //            0,
            //            string.Empty);
            //    }

            //    List<DTO.SanPham> listDataSP = SanPhamBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);
            //    DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colSanPhamKhuyenMai.Name];
            //    cell.DataSource = listDataSP;
            //    //cell.Value = listDataSP.FirstOrDefault().Id;
            //    cell.ValueMember = "Id";
            //    cell.DisplayMember = "Ten";
            //    //dgvThongTin.CurrentCell = cell;

            //    if (dataKM != null)
            //    {
            //        cell.Value = dataKM.IdSanPhamKhuyenMai;
            //    }
            //}
        }

        private void CalculateMoney()
        {
            int money = 0;

            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                money += ConvertUtil.ConvertToInt(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            }

            tbThanhTien.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
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

        private void InsertData()
        {
            InsertDataBanHang();
        }

        private bool InsertDataBanHang()
        {
            DTO.BanHang data = new DTO.BanHang();

            data.Date = dtpFilter.Value;
            data.IdUser = ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag);
            data.ThanhTien = ConvertUtil.ConvertToInt(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (BanHangBus.Insert(data))
            {
                return InsertDataBanHangChiTiet(data);
            }
            else
            {
                return false;
            }
        }

        private bool InsertDataBanHangChiTiet(DTO.BanHang dataBanHang)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.BanHangChiTiet data = new DTO.BanHangChiTiet();
                data.IdBanHang = dataBanHang.Id;
                data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                data.Gia = ConvertUtil.ConvertToInt(row.Cells[colGia.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                data.TonDau = ConvertUtil.ConvertToInt(row.Cells[colTonDau.Name].Value);
                data.Nhan = ConvertUtil.ConvertToInt(row.Cells[colNhan.Name].Value);
                data.Ban = ConvertUtil.ConvertToInt(row.Cells[colBan.Name].Value);
                data.ThuHoi = ConvertUtil.ConvertToInt(row.Cells[colThuHoi.Name].Value);
                data.TonCuoi = ConvertUtil.ConvertToInt(row.Cells[colTonCuoi.Name].Value);
                data.ThanhTien = ConvertUtil.ConvertToInt(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                if (!BanHangChiTietBus.Insert(data))
                {
                    return false;
                }
            }

            return true;
        }

        private bool UpdateDataBanHang()
        {
            DTO.BanHang data = BanHangBus.GetByIdUserAndDate(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag), dtpFilter.Value);
            data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToInt(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            if (BanHangBus.Update(data))
            {
                return (UpdateDataBanHangChiTiet(data));
            }
            else
            {
                return false;
            }
        }

        private bool UpdateDataBanHangChiTiet(DTO.BanHang dataBanHang)
        {
            foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                if (!string.IsNullOrEmpty(row.Cells[colId.Name].Value.ToString()))
                {
                    DTO.BanHangChiTiet data = BanHangChiTietBus.GetById(ConvertUtil.ConvertToInt(row.Cells[colId.Name].Value));
                    data.Ban = ConvertUtil.ConvertToInt(row.Cells[colBan.Name].Value);
                    data.TonDau = ConvertUtil.ConvertToInt(row.Cells[colTonDau.Name].Value);
                    data.Nhan = ConvertUtil.ConvertToInt(row.Cells[colNhan.Name].Value);
                    data.Ban = ConvertUtil.ConvertToInt(row.Cells[colBan.Name].Value);
                    data.ThuHoi = ConvertUtil.ConvertToInt(row.Cells[colThuHoi.Name].Value);
                    data.TonCuoi = ConvertUtil.ConvertToInt(row.Cells[colTonCuoi.Name].Value);
                    data.ThanhTien = ConvertUtil.ConvertToInt(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                    if (!BanHangChiTietBus.Update(data))
                    {
                        return false;
                    }
                }
                else
                {
                    DTO.BanHangChiTiet data = new DTO.BanHangChiTiet();

                    data.IdBanHang = dataBanHang.Id;
                    data.IdSanPham = ConvertUtil.ConvertToInt(row.Cells[colIdSanPham.Name].Value);
                    data.Gia = ConvertUtil.ConvertToInt(row.Cells[colGia.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.TonDau = ConvertUtil.ConvertToInt(row.Cells[colTonDau.Name].Value);
                    data.Nhan = ConvertUtil.ConvertToInt(row.Cells[colNhan.Name].Value);
                    data.Ban = ConvertUtil.ConvertToInt(row.Cells[colBan.Name].Value);
                    data.ThuHoi = ConvertUtil.ConvertToInt(row.Cells[colThuHoi.Name].Value);
                    data.TonCuoi = ConvertUtil.ConvertToInt(row.Cells[colTonCuoi.Name].Value);
                    data.ThanhTien = ConvertUtil.ConvertToInt(row.Cells[colThanhTien.Name].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                    if (!BanHangChiTietBus.Insert(data))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void UpdateData()
        {
            DTO.BanHang banHang = BanHangBus.GetByIdUserAndDate(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag), dtpFilter.Value);

            if (banHang != null)
            {
                UpdateDataBanHang();
            }
            else
            {
                InsertDataBanHang();
            }

            //MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Bán hàng"), Constant.CAPTION_CONFIRMATION, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (MessageBox.Show("Cập nhật?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            //if (dgvThongTin.Rows.Count > 0)
            //{
            //    NewLvEx(Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Width, Constant.DEFAULT_SIZE_LISTVIEWEX_EXPORT.Height);
            //    LoadLvExLData();
            //    HideColumn();

            //    //RefreshLvEx(tbSearch.Text);

            //    //FormExportExcel frm = new FormExportExcel(Constant.DEFAULT_TYPE_EXPORT_NGUYENLIEU, Constant.DEFAULT_SHEET_NAME_EXPORT_NGUYENLIEU, Constant.DEFAULT_TYPE_EXPORT_NGUYENLIEU,
            //    //    lvEx, soSP);
            //    //frm.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show(Constant.MESSAGE_ERROR_EXPORT_EXCEL_NULL_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
            if (ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, e.RowIndex].Value) <= 0)
            {
                dgvThongTin[colNhan.Name, e.RowIndex].Value = 0;
            }

            if (ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, e.RowIndex].Value) <= 0)
            {
                dgvThongTin[colBan.Name, e.RowIndex].Value = 0;
            }

            if (ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, e.RowIndex].Value) <= 0)
            {
                dgvThongTin[colThuHoi.Name, e.RowIndex].Value = 0;
            }

            int soLuong = ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, e.RowIndex].Value);
            int price = ConvertUtil.ConvertToInt(dgvThongTin[colGia.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            int money = price * soLuong;

            //if (soLuong == 0)
            //{
            //    soLuong = 1;
            //    dgvThongTin[colBan.Name, e.RowIndex].Value = 1;
            //}

            dgvThongTin[colThanhTien.Name, e.RowIndex].Value = price * soLuong;

            int tonDau = ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, e.RowIndex].Value);
            int nhan = ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, e.RowIndex].Value);
            int thuHoi = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, e.RowIndex].Value);
            dgvThongTin[colTonCuoi.Name, e.RowIndex].Value = tonDau + nhan - soLuong - thuHoi;
            CalculateMoney();
        }
        #endregion

        private void dgvThongTin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == dgvThongTin.Columns[colRemove.Name].Index)
            //{
            //    if (dgvThongTin[colId.Name, e.RowIndex].Value != null && !string.IsNullOrEmpty(dgvThongTin[colId.Name, e.RowIndex].Value.ToString()))
            //    {
            //        idsBanHangChiTietRemoved += dgvThongTin[colId.Name, e.RowIndex].Value + Constant.SEPERATE_STRING;
            //    }

            //    dgvThongTin.Rows.RemoveAt(e.RowIndex);
            //    CalculateMoney();
            //}
        }

        private void tvUser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvUser.SelectedNode != null)
            {
                DTO.User data = UserBus.GetById(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag));

                if (data != null)
                {
                    tbTenNV.Text = data.Ten;

                    if (data.IdUserGroup == 3)
                    {
                        GetDataBanHangToTruong(data);
                        dgvThongTin.Enabled = false;
                    }
                    else
                    {
                        GetDataBanHang();
                        dgvThongTin.Enabled = true;
                    }
                }
                else
                {
                    dgvThongTin.Rows.Clear();
                    tbTenNV.Text = string.Empty;
                    tbThanhTien.Text = string.Empty;
                    tbGhiChu.Text = string.Empty;
                }
            }
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            GetDataBanHang();
        }
    }
}
