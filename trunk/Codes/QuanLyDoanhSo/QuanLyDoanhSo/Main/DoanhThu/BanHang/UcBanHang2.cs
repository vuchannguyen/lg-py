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
                        data.TonDau, data.ThuHoiNgayTruoc, data.Nhan, soLuong, data.ThuHoi,
                        price.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        money.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        data.TonCuoi, 0, 0,
                        data.DiemMoi, data.LuotBan);
                }

                AddLastRow(dgvThongTin);
                tbGhiChu.Text = banHang.GhiChu;
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
                        listDataNgayTruoc == null ? 0 : listDataNgayTruoc.Where(p => p.IdSanPham == data.Id).FirstOrDefault().TonCuoi,
                        listDataNgayTruoc == null ? 0 : listDataNgayTruoc.Where(p => p.IdSanPham == data.Id).FirstOrDefault().ThuHoi,
                        0, 0, 0,
                        data.Gia.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        0,
                        0, 0, 0,
                        0, 0);
                }

                AddLastRow(dgvThongTin);
            }

            GetDataLoaiTien(banHang);
            RefreshCongCu();
            CalculateAll();
            dgvThongTin.Rows[0].Selected = true;
            dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
        }

        private void GetDataLoaiTien(DTO.BanHang banHang)
        {
            if (banHang != null)
            {
                List<DTO.BanHang_LoaiTien> listData = BanHang_LoaiTienBus.GetListByIdBanHang(banHang.Id);

                if (dgvLoaiTien.Rows.Count > 0)
                {
                    for (int i = 0; i < listData.Count; i++)
                    {
                        dgvLoaiTien[colSoLuong.Name, i].Value = listData[i].SoLuong + ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, i].Value);
                        dgvLoaiTien[colThanhTienTien.Name, i].Value = listData[i].ThanhTien + ConvertUtil.ConvertToInt(dgvLoaiTien[colThanhTienTien.Name, i].Value);
                    }
                }
                else
                {
                    foreach (DTO.BanHang_LoaiTien data in listData)
                    {
                        dgvLoaiTien.Rows.Add(data.Id, data.IdLoaiTien, data.LoaiTien.Gia,
                            data.SoLuong,
                            data.ThanhTien);
                    }

                    if (dgvLoaiTien.Rows.Count > 0)
                    {
                        AddLastRow(dgvLoaiTien);
                    }
                }
            }

            if (dgvLoaiTien.Rows.Count == 0)
            {
                List<DTO.LoaiTien> listData = LoaiTienBus.GetList(string.Empty, "Giá", Constant.SORT_DESCENDING, 0, 0);

                foreach (DTO.LoaiTien data in listData)
                {
                    dgvLoaiTien.Rows.Add(string.Empty, data.Id, data.Gia,
                        0,
                        0);
                }

                AddLastRow(dgvLoaiTien);
            }

            CalculateLoaiTien();
        }

        private void GetDataBanHangToTruong(DTO.User toTruong)
        {
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
                            dgvThongTin[colTonDau.Name, i].Value = listData[i].TonDau + ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                            dgvThongTin[colThuHoiNgayTruoc.Name, i].Value = listData[i].ThuHoiNgayTruoc + ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                            dgvThongTin[colNhan.Name, i].Value = listData[i].Nhan + ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                            dgvThongTin[colBan.Name, i].Value = listData[i].Ban + ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                            dgvThongTin[colTonCuoi.Name, i].Value = listData[i].TonCuoi + ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                            dgvThongTin[colGia.Name, i].Value = listData[i].Gia;
                            dgvThongTin[colThanhTien.Name, i].Value = listData[i].ThanhTien + ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                            dgvThongTin[colThuHoi.Name, i].Value = listData[i].ThuHoi + ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                            dgvThongTin[colDiemMoi.Name, i].Value = listData[i].DiemMoi + ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                            dgvThongTin[colLuotBan.Name, i].Value = listData[i].LuotBan + ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);
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
                                data.TonDau, data.ThuHoiNgayTruoc, data.Nhan, soLuong, data.ThuHoi,
                                price.ToString(Constant.DEFAULT_FORMAT_MONEY),
                                money.ToString(Constant.DEFAULT_FORMAT_MONEY),
                                data.TonCuoi, 0, 0,
                                data.DiemMoi, data.LuotBan);
                        }

                        AddLastRow(dgvThongTin);
                    }

                    CalculateAll();
                }

                GetDataLoaiTien(banHang);
            }

            if (dgvThongTin.Rows.Count == 0)
            {
                List<DTO.SanPham> listData = SanPhamBus.GetListByGia();

                foreach (DTO.SanPham data in listData)
                {
                    dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten,
                        0, 0, 0, 0, 0,
                        data.Gia.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        0,
                        0, 0, 0,
                        0, 0);
                }

                AddLastRow(dgvThongTin);
            }

            RefreshCongCu();
            dgvThongTin.Rows[0].Selected = true;
            dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
        }

        private void GetDataBanHangAll()
        {
            tbGhiChu.Text = string.Empty;
            dgvThongTin.Rows.Clear();
            List<DTO.User> listUser = UserBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            foreach (DTO.User data1 in listUser)
            {
                DTO.BanHang banHang = null;

                if (data1.IdUserGroup == 2)
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
                            dgvThongTin[colTonDau.Name, i].Value = listData[i].TonDau + ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                            dgvThongTin[colThuHoiNgayTruoc.Name, i].Value = listData[i].ThuHoiNgayTruoc + ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                            dgvThongTin[colNhan.Name, i].Value = listData[i].Nhan + ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                            dgvThongTin[colBan.Name, i].Value = listData[i].Ban + ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                            dgvThongTin[colTonCuoi.Name, i].Value = listData[i].TonCuoi + ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                            dgvThongTin[colGia.Name, i].Value = listData[i].Gia;
                            dgvThongTin[colThanhTien.Name, i].Value = listData[i].ThanhTien + ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                            dgvThongTin[colThuHoi.Name, i].Value = listData[i].ThuHoi + ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                            dgvThongTin[colDiemMoi.Name, i].Value = listData[i].DiemMoi + ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                            dgvThongTin[colLuotBan.Name, i].Value = listData[i].LuotBan + ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);
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
                                data.TonDau, data.ThuHoiNgayTruoc, data.Nhan, soLuong, data.ThuHoi,
                                price.ToString(Constant.DEFAULT_FORMAT_MONEY),
                                money.ToString(Constant.DEFAULT_FORMAT_MONEY),
                                data.TonCuoi, 0, 0,
                                data.DiemMoi, data.LuotBan);
                        }

                        AddLastRow(dgvThongTin);
                    }

                    CalculateAll();
                }

                GetDataLoaiTien(banHang);
            }

            if (dgvThongTin.Rows.Count == 0)
            {
                List<DTO.SanPham> listData = SanPhamBus.GetListByGia();

                foreach (DTO.SanPham data in listData)
                {
                    dgvThongTin.Rows.Add(string.Empty, data.Id, data.Ten,
                        0, 0, 0, 0, 0,
                        data.Gia.ToString(Constant.DEFAULT_FORMAT_MONEY),
                        0,
                        0, 0, 0,
                        0, 0);
                }

                AddLastRow(dgvThongTin);
            }

            RefreshCongCu();
            dgvThongTin.Rows[0].Selected = true;
            dgvThongTin.FirstDisplayedScrollingRowIndex = dgvThongTin.RowCount - 1;
        }

        private void AddLastRow(DataGridView dgv)
        {
            dgv.Rows.Add();
            dgv.Rows[dgv.Rows.Count - 1].ReadOnly = true;
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
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

                List<DTO.User> listData = UserBus.GetList(string.Empty, "Tổ", string.Empty, 0, 0);
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
                tvUser.SelectedNode = root;
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
            
        }

        private void CalculateAll()
        {
            int tonDau = 0;
            int thuHoiNgayTruoc = 0;
            int nhan = 0;
            int ban = 0;
            int tonCuoi = 0;
            int tien = 0;
            int thuHoi = 0;
            double resultQuet = 0;
            double resultTLB = 0;
            int diemMoi = 0;
            int luotBan = 0;

            for (int i = 0; i < dgvThongTin.Rows.Count - 1; i++)
            //foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                tonDau += ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                thuHoiNgayTruoc += ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                nhan += ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                ban += ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                tonCuoi += ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                tien += dgvThongTin[colThanhTien.Name, i].Value == null ? 0 :
                    ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                thuHoi += ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                resultQuet += ConvertUtil.ConvertToDouble(dgvThongTin[colQuet.Name, i].Value);
                resultTLB += ConvertUtil.ConvertToDouble(dgvThongTin[colTLB.Name, i].Value);
                diemMoi += ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                luotBan += ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);
            }

            if (dgvThongTin.Rows.Count > 0)
            {
                dgvThongTin[colTonDau.Name, dgvThongTin.Rows.Count - 1].Value = tonDau;
                dgvThongTin[colThuHoiNgayTruoc.Name, dgvThongTin.Rows.Count - 1].Value = thuHoiNgayTruoc;
                dgvThongTin[colNhan.Name, dgvThongTin.Rows.Count - 1].Value = nhan;
                dgvThongTin[colBan.Name, dgvThongTin.Rows.Count - 1].Value = ban;
                dgvThongTin[colTonCuoi.Name, dgvThongTin.Rows.Count - 1].Value = tonCuoi;
                dgvThongTin[colThanhTien.Name, dgvThongTin.Rows.Count - 1].Value = tien.ToString(Constant.DEFAULT_FORMAT_MONEY);
                dgvThongTin[colThuHoi.Name, dgvThongTin.Rows.Count - 1].Value = thuHoi;
                dgvThongTin[colQuet.Name, dgvThongTin.Rows.Count - 1].Value = resultQuet;
                dgvThongTin[colTLB.Name, dgvThongTin.Rows.Count - 1].Value = resultTLB;
                dgvThongTin[colDiemMoi.Name, dgvThongTin.Rows.Count - 1].Value = diemMoi;
                dgvThongTin[colLuotBan.Name, dgvThongTin.Rows.Count - 1].Value = luotBan;
            }
        }

        private void CalculateLoaiTien()
        {
            int soLuong = 0;
            int tien = 0;

            for (int i = 0; i < dgvLoaiTien.Rows.Count - 1; i++)
            {
                soLuong += ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, i].Value);
                tien += ConvertUtil.ConvertToInt(dgvLoaiTien[colThanhTienTien.Name, i].Value);
            }

            if (dgvLoaiTien.Rows.Count > 0)
            {
                dgvLoaiTien[colSoLuong.Name, dgvLoaiTien.Rows.Count - 1].Value = soLuong;
                dgvLoaiTien[colThanhTienTien.Name, dgvLoaiTien.Rows.Count - 1].Value = tien;
            }
        }

        private void RefreshCongCu()
        {
            for (int i = 0; i < dgvThongTin.Rows.Count; i++)
            {
                int soLuong = ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                DTO.KhuyenMai khuyenMai = KhuyenMaiBus.GetByIdSanPham(ConvertUtil.ConvertToInt(dgvThongTin[colIdSanPham.Name, i].Value));
                double soLuongSanPhamKhuyenMai = 0;

                if (khuyenMai != null)
                {
                    soLuongSanPhamKhuyenMai = 1.0 * soLuong / khuyenMai.SoLuongSanPham * khuyenMai.SoLuongSanPhamKhuyenMai;
                    soLuongSanPhamKhuyenMai = Math.Round(soLuongSanPhamKhuyenMai, 1);
                    double soDu = soLuongSanPhamKhuyenMai % khuyenMai.DonViLamTron;

                    if (soDu <= khuyenMai.DonViLamTron / 2)
                    {
                        soLuongSanPhamKhuyenMai = soLuongSanPhamKhuyenMai - soDu;
                    }
                    else
                    {
                        soLuongSanPhamKhuyenMai = soLuongSanPhamKhuyenMai - soDu + khuyenMai.DonViLamTron;
                    }
                }

                if (soLuongSanPhamKhuyenMai != 0)
                {
                    if (khuyenMai.SanPham1.Ten == "Quẹt")
                    {
                        dgvThongTin[colQuet.Name, i].Value = soLuongSanPhamKhuyenMai;
                    }
                    else
                    {
                        dgvThongTin[colTLB.Name, i].Value = soLuongSanPhamKhuyenMai;
                    }
                }
            }

            CalculateAll();
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
            data.ThanhTien = ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, dgvThongTin.Rows.Count - 1].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
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
            for (int i = 0; i < dgvThongTin.Rows.Count - 1; i++)
            //foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                DTO.BanHangChiTiet data = new DTO.BanHangChiTiet();
                data.IdBanHang = dataBanHang.Id;
                data.IdSanPham = ConvertUtil.ConvertToInt(dgvThongTin[colIdSanPham.Name, i].Value);
                data.TonDau = ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                data.ThuHoiNgayTruoc = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                data.Nhan = ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                data.Ban = ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                data.TonCuoi = ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                data.Gia = ConvertUtil.ConvertToInt(dgvThongTin[colGia.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                data.ThanhTien = ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                data.ThuHoi = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                data.DiemMoi = ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                data.LuotBan = ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);

                if (!BanHangChiTietBus.Insert(data))
                {
                    return false;
                }
                else
                {
                    dgvThongTin[colId.Name, i].Value = data.Id;
                }
            }

            return true;
        }

        private bool UpdateDataBanHang(DTO.BanHang data)
        {
            //data.IdUser = FormMain.user.Id;
            data.ThanhTien = ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, dgvThongTin.Rows.Count - 1].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
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
            for (int i = 0; i < dgvThongTin.Rows.Count - 1; i++)
            //foreach (DataGridViewRow row in dgvThongTin.Rows)
            {
                if (!string.IsNullOrEmpty(dgvThongTin[colId.Name, i].Value.ToString()))
                {
                    DTO.BanHangChiTiet data = BanHangChiTietBus.GetById(ConvertUtil.ConvertToInt(dgvThongTin[colId.Name, i].Value));
                    data.TonDau = ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                    data.ThuHoiNgayTruoc = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                    data.Nhan = ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                    data.Ban = ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                    data.TonCuoi = ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                    data.ThanhTien = ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.ThuHoi = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                    data.DiemMoi = ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                    data.LuotBan = ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);

                    if (!BanHangChiTietBus.Update(data))
                    {
                        return false;
                    }
                }
                else
                {
                    DTO.BanHangChiTiet data = new DTO.BanHangChiTiet();
                    data.IdBanHang = dataBanHang.Id;
                    data.IdSanPham = ConvertUtil.ConvertToInt(dgvThongTin[colIdSanPham.Name, i].Value);
                    data.TonDau = ConvertUtil.ConvertToInt(dgvThongTin[colTonDau.Name, i].Value);
                    data.ThuHoiNgayTruoc = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, i].Value);
                    data.Nhan = ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, i].Value);
                    data.Ban = ConvertUtil.ConvertToInt(dgvThongTin[colBan.Name, i].Value);
                    data.TonCuoi = ConvertUtil.ConvertToInt(dgvThongTin[colTonCuoi.Name, i].Value);
                    data.Gia = ConvertUtil.ConvertToInt(dgvThongTin[colGia.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.ThanhTien = ConvertUtil.ConvertToInt(dgvThongTin[colThanhTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                    data.ThuHoi = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, i].Value);
                    data.DiemMoi = ConvertUtil.ConvertToInt(dgvThongTin[colDiemMoi.Name, i].Value);
                    data.LuotBan = ConvertUtil.ConvertToInt(dgvThongTin[colLuotBan.Name, i].Value);

                    if (!BanHangChiTietBus.Insert(data))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool UpdateDataBanHangLoaiTien(DTO.BanHang dataBanHang)
        {
            for (int i = 0; i < dgvLoaiTien.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(dgvLoaiTien[colIdTien.Name, i].Value.ToString()))
                {
                    DTO.BanHang_LoaiTien data = BanHang_LoaiTienBus.GetById(ConvertUtil.ConvertToInt(dgvLoaiTien[colIdTien.Name, i].Value));
                    data.SoLuong = ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, i].Value);
                    data.ThanhTien = ConvertUtil.ConvertToInt(dgvLoaiTien[colThanhTienTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                    if (!BanHang_LoaiTienBus.Update(data))
                    {
                        return false;
                    }
                }
                else
                {
                    DTO.BanHang_LoaiTien data = new DTO.BanHang_LoaiTien();

                    data.IdBanHang = dataBanHang.Id;
                    data.IdLoaiTien = ConvertUtil.ConvertToInt(dgvLoaiTien[colIdLoaiTien.Name, i].Value);
                    data.SoLuong = ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, i].Value);
                    data.ThanhTien = ConvertUtil.ConvertToInt(dgvLoaiTien[colThanhTienTien.Name, i].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                    if (!BanHang_LoaiTienBus.Insert(data))
                    {
                        return false;
                    }
                    else
                    {
                        dgvLoaiTien[colIdTien.Name, i].Value = data.Id;
                    }
                }
            }

            return true;
        }

        private void UpdateData(bool isBanHang)
        {
            DTO.BanHang banHang = BanHangBus.GetByIdUserAndDate(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag), dtpFilter.Value);

            if (banHang != null)
            {
                UpdateDataBanHang(banHang);
                UpdateDataBanHangLoaiTien(banHang);
            }
            else if (isBanHang)
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

            //if (MessageBox.Show("Cập nhật?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
                UpdateData(true);
            //}
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
            int thuHoiNgayTruoc = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoiNgayTruoc.Name, e.RowIndex].Value);
            int nhan = ConvertUtil.ConvertToInt(dgvThongTin[colNhan.Name, e.RowIndex].Value);
            int thuHoi = ConvertUtil.ConvertToInt(dgvThongTin[colThuHoi.Name, e.RowIndex].Value);
            dgvThongTin[colTonCuoi.Name, e.RowIndex].Value = tonDau + thuHoiNgayTruoc + nhan - soLuong - thuHoi;
            DTO.KhuyenMai khuyenMai = KhuyenMaiBus.GetByIdSanPham(ConvertUtil.ConvertToInt(dgvThongTin[colIdSanPham.Name, e.RowIndex].Value));
            double soLuongSanPhamKhuyenMai = 0;

            if (khuyenMai != null)
            {
                soLuongSanPhamKhuyenMai = 1.0 * soLuong / khuyenMai.SoLuongSanPham * khuyenMai.SoLuongSanPhamKhuyenMai;
                soLuongSanPhamKhuyenMai = Math.Round(soLuongSanPhamKhuyenMai, 1);
                double soDu = soLuongSanPhamKhuyenMai % khuyenMai.DonViLamTron;

                if (khuyenMai.DonViLamTron > 0)
                {
                    if (soDu <= khuyenMai.DonViLamTron / 2)
                    {
                        soLuongSanPhamKhuyenMai = soLuongSanPhamKhuyenMai - soDu;
                    }
                    else
                    {
                        soLuongSanPhamKhuyenMai = soLuongSanPhamKhuyenMai - soDu + khuyenMai.DonViLamTron;
                    }
                }
            }

            if (soLuongSanPhamKhuyenMai != 0)
            {
                if (khuyenMai.SanPham1.Ten == "Quẹt")
                {
                    dgvThongTin[colQuet.Name, e.RowIndex].Value = soLuongSanPhamKhuyenMai;
                }
                else
                {
                    dgvThongTin[colTLB.Name, e.RowIndex].Value = soLuongSanPhamKhuyenMai;
                }
            }

            CalculateAll();

            try
            {
                UpdateData(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá trình lưu trữ dữ liệu!\n" + ex.Message);
            }
        }
        #endregion

        private void dgvThongTin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tvUser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvUser.SelectedNode != null)
            {
                dgvLoaiTien.Rows.Clear();

                if (tvUser.SelectedNode.Tag.ToString() == "all")
                {
                    tbTenNV.Text = "---Tất cả---";
                    GetDataBanHangAll();
                    //dgvThongTin.Enabled = false;
                    DisableDataGridEdit();
                }
                else
                {
                    DTO.User data = UserBus.GetById(ConvertUtil.ConvertToInt(tvUser.SelectedNode.Tag));

                    if (data != null)
                    {
                        tbTenNV.Text = data.Ma + "-" + data.Ten;

                        if (data.IdUserGroup == 3)
                        {
                            GetDataBanHangToTruong(data);
                            //dgvThongTin.Enabled = false;
                            DisableDataGridEdit();
                        }
                        else
                        {
                            GetDataBanHang();
                            //dgvThongTin.Enabled = true;
                            EnableDataGridEdit();
                        }
                    }
                    else
                    {
                        dgvThongTin.Rows.Clear();
                        tbTenNV.Text = string.Empty;
                        tbGhiChu.Text = string.Empty;
                        DisableDataGridEdit();
                    }
                }

                tvUser.SelectedNode.ForeColor = Color.Red;
            }
        }

        private void EnableDataGridEdit()
        {
            dgvThongTin.Columns[colNhan.Name].ReadOnly = false;
            dgvThongTin.Columns[colBan.Name].ReadOnly = false;
            dgvThongTin.Columns[colThuHoi.Name].ReadOnly = false;
            dgvThongTin.Columns[colQuet.Name].ReadOnly = false;
            dgvThongTin.Columns[colTLB.Name].ReadOnly = false;
            dgvThongTin.Columns[colDiemMoi.Name].ReadOnly = false;
            dgvThongTin.Columns[colLuotBan.Name].ReadOnly = false;

            dgvThongTin.Columns[colNhan.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colBan.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colThuHoi.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colQuet.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colTLB.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colDiemMoi.Name].DefaultCellStyle.BackColor = Color.White;
            dgvThongTin.Columns[colLuotBan.Name].DefaultCellStyle.BackColor = Color.White;
        }

        private void DisableDataGridEdit()
        {
            dgvThongTin.Columns[colNhan.Name].ReadOnly = true;
            dgvThongTin.Columns[colBan.Name].ReadOnly = true;
            dgvThongTin.Columns[colThuHoi.Name].ReadOnly = true;
            dgvThongTin.Columns[colQuet.Name].ReadOnly = true;
            dgvThongTin.Columns[colTLB.Name].ReadOnly = true;
            dgvThongTin.Columns[colDiemMoi.Name].ReadOnly = true;
            dgvThongTin.Columns[colLuotBan.Name].ReadOnly = true;

            dgvThongTin.Columns[colNhan.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colBan.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colThuHoi.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colQuet.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colTLB.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colDiemMoi.Name].DefaultCellStyle.BackColor = Color.LightGray;
            dgvThongTin.Columns[colLuotBan.Name].DefaultCellStyle.BackColor = Color.LightGray;
        }

        private void dtpFilter_ValueChanged(object sender, EventArgs e)
        {
            GetDataBanHang();
        }

        private void dgvThongTin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateData(true);
            }

            if (ModifierKeys == Keys.Control)
            {
                e.Handled = false;
            }
        }

        private void dgvLoaiTien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvLoaiTien_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, e.RowIndex].Value) <= 0)
            {
                dgvLoaiTien[colSoLuong.Name, e.RowIndex].Value = 0;
            }

            int soLuong = ConvertUtil.ConvertToInt(dgvLoaiTien[colSoLuong.Name, e.RowIndex].Value);
            int price = ConvertUtil.ConvertToInt(dgvLoaiTien[colLoaiTien.Name, e.RowIndex].Value.ToString().Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            int money = price * soLuong;
            dgvLoaiTien[colThanhTienTien.Name, e.RowIndex].Value = price * soLuong;
            CalculateLoaiTien();

            try
            {
                UpdateData(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong quá trình lưu trữ dữ liệu!\n" + ex.Message);
            }
        }

        private void dgvLoaiTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                UpdateData(false);
            }
        }

        private void dgvThongTin_KeyDown(object sender, KeyEventArgs e)
        {
            Move(e);
        }

        private void Move(KeyEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                tvUser.SelectedNode.ForeColor = Color.Black;

                if (e.KeyCode == Keys.Right)
                {
                    if (tvUser.SelectedNode.GetNodeCount(false) == 0 && tvUser.SelectedNode.NextNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.NextNode;
                        return;
                    }

                    if (tvUser.SelectedNode.GetNodeCount(false) > 0)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.Nodes[0];
                        return;
                    }

                    if (tvUser.SelectedNode.Parent != null && tvUser.SelectedNode.Parent.NextNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.Parent.NextNode;
                    }
                }

                if (e.KeyCode == Keys.Left)
                {
                    if (tvUser.SelectedNode.Nodes.Count == 0 && tvUser.SelectedNode.PrevNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.PrevNode;
                        return;
                    }

                    if (tvUser.SelectedNode.PrevNode != null && tvUser.SelectedNode.PrevNode.GetNodeCount(false) > 0)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.PrevNode.Nodes[tvUser.SelectedNode.PrevNode.GetNodeCount(false) - 1];
                        return;
                    }

                    if (tvUser.SelectedNode.Parent != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.Parent;
                    }
                }

                if (e.KeyCode == Keys.Up)
                {
                    if (tvUser.SelectedNode.Parent != null && tvUser.SelectedNode.Parent.Text.StartsWith("Tổ ") &&
                        tvUser.SelectedNode.Parent.PrevNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.Parent.PrevNode;
                        return;
                    }

                    if (tvUser.SelectedNode.Text.StartsWith("Tổ ") &&
                        tvUser.SelectedNode.PrevNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.PrevNode;
                    }
                }

                if (e.KeyCode == Keys.Down)
                {
                    if (tvUser.SelectedNode.Parent != null && tvUser.SelectedNode.Parent.Text.StartsWith("Tổ ") &&
                        tvUser.SelectedNode.Parent.NextNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.Parent.NextNode;
                        return;
                    }

                    if (tvUser.SelectedNode.Text.StartsWith("Tổ ") &&
                        tvUser.SelectedNode.NextNode != null)
                    {
                        tvUser.SelectedNode = tvUser.SelectedNode.NextNode;
                    }
                }

                tvUser.SelectedNode.ForeColor = Color.Red;
            }

            if (ModifierKeys == Keys.Alt)
            {
                if (e.KeyCode == Keys.Right)
                {
                    dtpFilter.Value = dtpFilter.Value.AddDays(1);
                }

                if (e.KeyCode == Keys.Left)
                {
                    dtpFilter.Value = dtpFilter.Value.AddDays(-1);
                }

                if (e.KeyCode == Keys.Up)
                {
                    dtpFilter.Value = dtpFilter.Value.AddMonths(1);
                }

                if (e.KeyCode == Keys.Down)
                {
                    dtpFilter.Value = dtpFilter.Value.AddMonths(-1);
                }
            }
        }

        private void tvUser_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tvUser.SelectedNode != null)
            {
                tvUser.SelectedNode.ForeColor = Color.Black;
            }
        }

        private void tvUser_KeyDown(object sender, KeyEventArgs e)
        {
            Move(e);
        }

        private void dgvLoaiTien_KeyDown(object sender, KeyEventArgs e)
        {
            Move(e);
        }
    }
}
