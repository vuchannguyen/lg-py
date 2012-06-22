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

namespace QuanLyKinhDoanh.GiaoDich
{
    public partial class UcThanhToan : UserControl
    {
        private DTO.SanPham dataSP;
        private DTO.KhachHang dataKH;
        private DTO.ChietKhau dataCK;
        private DTO.HoaDon dataHoaDon;
        private DTO.HoaDonDetail dataHoaDonDetail;
        private List<DTO.SanPham> listDataSP;

        private long totalMoney;
        private long totalDiscount;

        public UcThanhToan()
        {
            InitializeComponent();

            if (Init())
            {
                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        private void LoadResource()
        {
            try
            {
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcThanhToan_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetWidthCenter(this.Size, pnInfo.Size, pnInfo.Top);
            pnDetail.Location = CommonFunc.SetWidthCenter(this.Size, pnDetail.Size, pnDetail.Top);

            this.BringToFront();

            FormMain.isEditing = true;

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            listDataSP = SanPhamBus.GetList(string.Empty, 0, true, Constant.DEFAULT_STATUS_SP_NOT_ZERO,
                false, 0,
                string.Empty, string.Empty, 0, 0);

            GetListSP(listDataSP);
            GetListKhachHang();
            GetListHoaDonStatus();

            return true;
        }

        private void RefreshData()
        {
            totalMoney = 0;
            totalDiscount = 0;

            tbNguoiBan.Text = FormMain.user.UserName;
            tbGhiChu.Text = string.Empty;

            tbSoLuong.Text = "1";
            tbChietKhau.Text = string.Empty;

            dtpNgayGio.Value = DateTime.Now;
            dtpNgayGio.CustomFormat = Constant.DEFAULT_DATE_TIME_FORMAT;
            lbNgayGio.Text = dtpNgayGio.Value.ToString(Constant.DEFAULT_DATE_TIME_FORMAT);

            cbMaSP.SelectedIndex = cbMaSP.Items.Count > 0 ? 0 : -1;
            cbStatus.SelectedIndex = cbStatus.Items.Count > 0 ? 0 : -1;
            cbMaKH.Text = string.Empty;

            tbTenKH.Text = string.Empty;
            tbTichLuy.Text = string.Empty;
            tbSuDung.Text = string.Empty;
            tbTienHoiLai.Text = string.Empty;
            tbTienThanhToan.Text = string.Empty;
            tbTongCK.Text = string.Empty;
            tbTongHoaDon.Text = string.Empty;

            foreach (ListViewItem lvi in lvThongTin.Items)
            {
                RestoreSanPham(ConvertUtil.ConvertToInt(lvi.SubItems[1].Text));
            }

            lvThongTin.Items.Clear();

            pbXoa.Enabled = false;
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);

            CreateNewId();

            ValidateHoanTat();
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(cbMaSP.Text) &&
                !string.IsNullOrEmpty(tbSoLuong.Text) &&
                !string.IsNullOrEmpty(tbThanhTien.Text)
                )
            {
                pbAdd.Enabled = true;
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
            }
            else
            {
                pbAdd.Enabled = false;
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_DISABLE);
            }
        }

        private void ValidateHoanTat()
        {
            if (lvThongTin.Items.Count > 0)
            {
                long money = 0;

                money = ConvertUtil.ConvertToLong(tbTienThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty)) +
                        ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                if (cbStatus.SelectedIndex == 0)
                {
                    if (money >= totalMoney)
                    {
                        pbHoanTat.Enabled = true;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
                    }
                    else
                    {
                        pbHoanTat.Enabled = false;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
                    }
                }
                else
                {
                    if (money < totalMoney)
                    {
                        pbHoanTat.Enabled = true;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
                    }
                    else
                    {
                        pbHoanTat.Enabled = false;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
                    }
                }
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
            }
        }

        private void CheckListViewItemsIsChecked()
        {
            if (lvThongTin.CheckedItems.Count > 0)
            {
                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
            }
        }

        private void CalculateMoney()
        {
            long money = 0;

            if (dataSP != null && dataSP.GiaBan != 0)
            {
                if (rbTichLuy.Checked)
                {
                    money = dataSP.GiaBan * ConvertUtil.ConvertToInt(tbSoLuong.Text);
                }
                else
                {
                    money = dataSP.GiaBan * ConvertUtil.ConvertToInt(tbSoLuong.Text) - ConvertUtil.ConvertToInt(tbTienCK.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                }
            }

            tbThanhTien.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void CalculateCK()
        {
            long money = 0;

            if (!string.IsNullOrEmpty(tbChietKhau.Text))
            {
                money = (ConvertUtil.ConvertToInt(tbChietKhau.Text) * dataSP.GiaBan / 100) * ConvertUtil.ConvertToInt(tbSoLuong.Text);
            }

            tbTienCK.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void CalculateTienHoiLai()
        {
            long money = 0;

            //if (!string.IsNullOrEmpty(tbTienThanhToan.Text))
            //{
                money = (ConvertUtil.ConvertToLong(tbTienThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty)) +
                    ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty)) - totalMoney);
            //}

            tbTienHoiLai.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void AddToBill()
        {
            ListViewItem lvi = new ListViewItem();

            lvi.SubItems.Add(dataSP.Id.ToString());
            lvi.SubItems.Add((lvThongTin.Items.Count + 1).ToString());
            lvi.SubItems.Add(dataSP.Ten);

            if (!string.IsNullOrEmpty(tbChietKhau.Text) && tbChietKhau.Text != "0")
            {
                totalDiscount += ConvertUtil.ConvertToLong(tbTienCK.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                lvi.SubItems.Add(tbChietKhau.Text + Constant.SYMBOL_DISCOUNT);
                lvi.SubItems.Add(tbTienCK.Text);
            }
            else
            {
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
            }

            lvi.SubItems.Add(tbSoLuong.Text);
            lvi.SubItems.Add(tbDVT.Text);
            lvi.SubItems.Add(tbGiaBan.Text);
            lvi.SubItems.Add(tbThanhTien.Text);

            lvThongTin.Items.Add(lvi);

            totalMoney += dataSP.GiaBan * ConvertUtil.ConvertToInt(tbSoLuong.Text);
            tbTongHoaDon.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);

            if (cbStatus.SelectedIndex == 0)
            {
                tbTongCK.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
            }
            else
            {
                tbTongCK.Text = string.Empty;
            }

            tbSoLuong.Text = "1";

            if (cbMaSP.Items.Count == 0)
            {
                cbMaSP.Text = string.Empty;
                tbTon.Text = string.Empty;
                tbTenSP.Text = string.Empty;
                tbChietKhau.Text = string.Empty;
                tbTienCK.Text = string.Empty;
                tbSoLuong.Text = string.Empty;
                tbDVT.Text = string.Empty;
                tbGiaBan.Text = string.Empty;
                tbThanhTien.Text = string.Empty;

                pbAdd.Enabled = false;
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_DISABLE);
            }
        }

        private void RemoveFromBill()
        {
            if (MessageBox.Show(Constant.MESSAGE_DELETE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string ids = string.Empty;

                foreach (ListViewItem lvi in lvThongTin.CheckedItems)
                {
                    long money = ConvertUtil.ConvertToLong(lvi.SubItems[9].Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));
                    totalMoney -= ConvertUtil.ConvertToLong(money);
                    tbTongHoaDon.Text = totalMoney.ToString(Constant.DEFAULT_FORMAT_MONEY);

                    money = ConvertUtil.ConvertToLong(lvi.SubItems[5].Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));
                    totalDiscount -= ConvertUtil.ConvertToLong(money);

                    if (cbStatus.SelectedIndex == 0)
                    {
                        tbTongCK.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
                    }
                    else
                    {
                        tbTongCK.Text = string.Empty;
                    }

                    //RestoreSanPham(ConvertUtil.ConvertToInt(lvi.SubItems[1].Text));

                    lvThongTin.Items.Remove(lvi);

                    for (int i = 0; i < lvThongTin.Items.Count; i++)
                    {
                        lvThongTin.Items[i].SubItems[2].Text = (i + 1).ToString();
                    }

                    if (lvThongTin.Items.Count == 0)
                    {
                        pbXoa.Enabled = false;
                        pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_DISABLE);
                    }
                }
            }
        }

        private void RestoreSanPham(int id)
        {
            DTO.SanPham dataRestore = SanPhamBus.GetById(id);

            if (dataRestore != null)
            {
                cbMaSP.Items.Add(new CommonComboBoxItems(dataRestore.MaSanPham, dataRestore.Id));
            }
        }

        private void CreateNewId()
        {
            int id = 0;

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_BAN);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            tbMaHD.Text = Constant.PREFIX_BAN + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void GetListSP(List<DTO.SanPham> listData)
        {
            cbMaSP.Text = string.Empty;
            cbMaSP.Items.Clear();

            foreach (DTO.SanPham data in listData)
            {
                if (lvThongTin.Items.Count == 0)
                {
                    cbMaSP.Items.Add(new CommonComboBoxItems(data.MaSanPham, data.Id));
                }
                else
                {
                    bool isDuplicated = false;

                    foreach (ListViewItem item in lvThongTin.Items)
                    {
                        if (item.SubItems[1].Text == data.Id.ToString())
                        {
                            isDuplicated = true;

                            break;
                        }
                    }

                    if (!isDuplicated)
                    {
                        cbMaSP.Items.Add(new CommonComboBoxItems(data.MaSanPham, data.Id));
                    }
                }
            }

            if (cbMaSP.Items.Count > 0)
            {
                cbMaSP.SelectedIndex = 0;
            }
        }

        private void GetListKhachHang()
        {
            List<DTO.KhachHang> listData = KhachHangBus.GetList(string.Empty, false, string.Empty, string.Empty, 0, 0);

            cbMaKH.Items.Clear();

            foreach (DTO.KhachHang data in listData)
            {
                cbMaKH.Items.Add(new CommonComboBoxItems(data.MaKhachHang, data.Id));
            }
        }

        private void GetListHoaDonStatus()
        {
            List<DTO.HoaDonStatus> listData = HoaDonStatusBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            cbStatus.Items.Clear();

            foreach (DTO.HoaDonStatus data in listData)
            {
                cbStatus.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }
        }

        private void GetInfoSP()
        {
            try
            {
                dataSP = SanPhamBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaSP.SelectedItem).Value));
                dataCK = ChietKhauBus.GetByIdSP(dataSP.Id);

                if (dataSP.GiaBan == 0)
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_MONEY, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                tbTenSP.Text = dataSP.Ten;
                tbTon.Text = dataSP.SoLuong.ToString();
                tbChietKhau.Text = dataCK == null ? string.Empty : dataCK.Value.ToString();
                tbDVT.Text = dataSP.DonViTinh;
                tbGiaBan.Text = dataSP.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY);

                if (!string.IsNullOrEmpty(dataSP.Avatar))
                {
                    pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(dataSP.Avatar));
                }
                else
                {
                    pbAvatar.Image = Image.FromFile(ConstantResource.SANPHAM_DEFAULT_SP);
                }
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_NULL_DATA, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetDiscount()
        {
            int percent = ChietKhauBus.GetByIdSP(dataSP.Id) == null ? 0 : ChietKhauBus.GetByIdSP(dataSP.Id).Value;

            return percent;
        }

        private void ExportBill()
        {
            List<ListView> list = new List<ListView>();

            ListView lvInfoBill = new ListView();
            lvInfoBill.CheckBoxes = true;
            lvInfoBill.Columns.Add("");
            lvInfoBill.Columns.Add("Hóa đơn: " + tbMaHD.Text);

            ListViewItem lvi = new ListViewItem();

            lvi = new ListViewItem();
            lvi.SubItems.Add("Ngày: " + lbNgayGio.Text);
            lvInfoBill.Items.Add(lvi);

            lvi = new ListViewItem();
            lvi.SubItems.Add("Nhân viên: " + tbNguoiBan.Text);
            lvInfoBill.Items.Add(lvi);

            lvi = new ListViewItem();
            lvi.SubItems.Add("Khách: " + cbMaKH.Text);
            lvInfoBill.Items.Add(lvi);

            lvi = new ListViewItem();
            lvi.SubItems.Add("Tổng CK: " + tbTongCK.Text + Constant.DEFAULT_MONEY_SUBFIX);
            lvInfoBill.Items.Add(lvi);

            lvi = new ListViewItem();
            lvi.SubItems.Add("Tổng HĐ: " + tbTongHoaDon.Text + Constant.DEFAULT_MONEY_SUBFIX);
            lvInfoBill.Items.Add(lvi);

            ListView lvInfoNew = new ListView();

            for (int i = 2; i < lvThongTin.Columns.Count; i++)
            {
                lvInfoNew.Columns.Add((ColumnHeader)lvThongTin.Columns[i].Clone());
            }

            for (int i = 0; i < lvThongTin.Items.Count; i++)
            {
                ListViewItem lviInfo = new ListViewItem();

                lviInfo.SubItems[0].Text = lvThongTin.Items[i].SubItems[2].Text; //STT
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[3].Text); //SP
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[4].Text); //CK
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[5].Text); //tien CK
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[6].Text); //SL
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[7].Text); //DVT
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[8].Text + Constant.DEFAULT_MONEY_SUBFIX); //don gia
                lviInfo.SubItems.Add(lvThongTin.Items[i].SubItems[9].Text + Constant.DEFAULT_MONEY_SUBFIX); //thanh tien

                lvInfoNew.Items.Add(lviInfo);
            }

            list.Add(lvInfoBill);
            list.Add(lvInfoNew);

            string sPath = File_Function.SaveDialog("HoaDon_" + tbMaHD.Text + "_" + DateTime.Now.ToString(Constant.DEFAULT_EXPORT_EXCEL_DATE_FORMAT), Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE_NAME, Constant.DEFAULT_EXPORT_EXCEL_FILE_TYPE);

            if (sPath != null)
            {
                if (Office_Function.ExportListViews2Excel("Hóa đơn", sPath, list))
                {
                    MessageBox.Show(Constant.MESSAGE_SUCCESS_EXPORT_EXCEL, Constant.CAPTION_CONFIRM, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_EXPORT_EXCEL, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InsertData()
        {
            InsertDataHoaDon();
        }

        private void InsertDataHoaDon()
        {
            dataHoaDon = new HoaDon();

            dataHoaDon.MaHoaDon = tbMaHD.Text;
            dataHoaDon.IdUser = FormMain.user.Id;

            if (cbMaKH.SelectedItem != null)
            {
                dataHoaDon.IdKhachHang = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value);
            }

            dataHoaDon.IdType = Constant.ID_TYPE_BAN;
            dataHoaDon.IdStatus = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbStatus.SelectedItem).Value);

            if (dataHoaDon.IdStatus == Constant.ID_STATUS_DEBT)
            { 
                dataHoaDon.ConLai = ConvertUtil.ConvertToLong(tbTienHoiLai.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            }

            dataHoaDon.ThanhTien = totalMoney;
            dataHoaDon.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Insert(dataHoaDon, FormMain.user))
            {
                InsertDataHoaDonDetail(dataHoaDon.Id);
            }
            else
            {
                try
                {
                    HoaDonBus.Delete(dataHoaDon, FormMain.user);
                }
                catch
                {
                    //
                }

                MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void InsertDataHoaDonDetail(int idHoaDon)
        {
            foreach (ListViewItem lvi in lvThongTin.Items)
            {
                dataHoaDonDetail = new HoaDonDetail();

                dataHoaDonDetail.IdHoaDon = idHoaDon;
                dataHoaDonDetail.IdSanPham = ConvertUtil.ConvertToInt(lvi.SubItems[1].Text);
                dataHoaDonDetail.ChietKhau = ConvertUtil.ConvertToInt(lvi.SubItems[4].Text.Replace(Constant.SYMBOL_DISCOUNT, ""));
                dataHoaDonDetail.SoLuong = ConvertUtil.ConvertToInt(lvi.SubItems[6].Text);
                dataHoaDonDetail.DonGia = ConvertUtil.ConvertToInt(lvi.SubItems[8].Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));
                dataHoaDonDetail.ThanhTien = ConvertUtil.ConvertToLong(lvi.SubItems[9].Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

                if (HoaDonDetailBus.Insert(dataHoaDonDetail))
                {
                    UpdateDataSP(dataHoaDonDetail.SanPham, dataHoaDonDetail.SoLuong);
                }
                else
                {
                    try
                    {
                        HoaDonDetailBus.Delete(dataHoaDonDetail);
                    }
                    catch
                    {
                        //
                    }

                    MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
            }
        }

        private void UpdateDataKH()
        {
            if (dataKH != null && dataKH.IdGroup != Constant.ID_GROUP_KHACH_THUONG)
            {
                dataKH.TichLuy -= ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                dataKH.TichLuy += totalDiscount;

                if (KhachHangBus.Update(dataKH, FormMain.user))
                {
                    //this.Dispose();
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
            }
        }

        private void UpdateDataSP(DTO.SanPham dataUpdate, int soLuong)
        {
            if (dataUpdate != null)
            {
                dataUpdate.SoLuong -= soLuong;
                dataUpdate.IsSold = true;

                if (SanPhamBus.Update(dataUpdate, FormMain.user))
                {
                    //this.Dispose();
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion



        #region Button
        private void pbAdd_Click(object sender, EventArgs e)
        {
            pbAdd.Focus();

            AddToBill();

            GetListSP(listDataSP);

            ValidateHoanTat();

            CalculateTienHoiLai();
        }

        private void pbAdd_MouseEnter(object sender, EventArgs e)
        {
            pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD_MOUSEOVER);
        }

        private void pbAdd_MouseLeave(object sender, EventArgs e)
        {
            if (cbMaSP.Items.Count != 0)
            {
                pbAdd.Image = Image.FromFile(ConstantResource.GIAODICH_ICON_CART_ADD);
            }
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                InsertData();

                if (cbStatus.SelectedIndex == 0)
                {
                    UpdateDataKH();
                }

                if (MessageBox.Show(Constant.MESSAGE_CONFIRM_EXPORT, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ExportBill();
                }

                RefreshData();
            }
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_MOUSEOVER);
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
        }

        private void pbXoa_Click(object sender, EventArgs e)
        {
            RemoveFromBill();

            GetListSP(listDataSP);

            ValidateHoanTat();
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_MOUSEROVER);
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE);
        }
        #endregion



        #region Controls
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetListSP(listDataSP);
        }

        private void cbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInfoSP();
        }

        private void cbMaSP_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void cbMaKH_Leave(object sender, EventArgs e)
        {
            if (cbMaKH.SelectedItem != null)
            {
                dataKH = KhachHangBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value));

                if (dataKH != null)
                {
                    tbTienThanhToan.ReadOnly = false;
                    tbTenKH.Text = dataKH.Ten;
                    tbTichLuy.Text = dataKH.TichLuy == 0 ? "0" : dataKH.TichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);

                    return;
                }
            }

            tbTienThanhToan.ReadOnly = true;
            tbTienThanhToan.Text = string.Empty;
            tbTenKH.Text = string.Empty;
            tbTichLuy.Text = string.Empty;
        }

        private void cbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataKH = KhachHangBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbMaKH.SelectedItem).Value));

            tbTenKH.Text = dataKH == null ? string.Empty : dataKH.Ten;
            tbTichLuy.Text = dataKH.TichLuy == 0 ? "0" : dataKH.TichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);

            tbTienThanhToan.ReadOnly = false;
        }

        private void cbMaKH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbMaKH.Text))
            {
                dataKH = null;
            }
        }

        private void tbChietKhau_Leave(object sender, EventArgs e)
        {
            if (dataCK != null && ConvertUtil.ConvertToInt(tbChietKhau.Text) < dataCK.Value)
            {
                tbChietKhau.Text = dataCK.Value.ToString();
            }
        }

        private void tbChietKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbChietKhau_TextChanged(object sender, EventArgs e)
        {
            CalculateCK();

            CalculateMoney();
        }

        private void tbGiaBan_TextChanged(object sender, EventArgs e)
        {
            CalculateMoney();
        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            tbSoLuong.Text = ConvertUtil.ConvertToInt(tbSoLuong.Text) == 0 ? string.Empty :
                ConvertUtil.ConvertToInt(tbSoLuong.Text).ToString();

            if (ConvertUtil.ConvertToInt(tbSoLuong.Text) > dataSP.SoLuong)
            {
                tbSoLuong.Text = dataSP.SoLuong.ToString();
            }

            CalculateCK();

            CalculateMoney();

            ValidateInput();
        }

        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void lvThongTin_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
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
        }

        private void lvThongTin_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            CheckListViewItemsIsChecked();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.SelectedIndex == 0)
            {
                tbSuDung.Enabled = true;

                lbTienStatus.Text = Constant.DEFAULT_MONEY_STATUS_DONE;

                tbTongCK.Text = totalDiscount.ToString(Constant.DEFAULT_FORMAT_MONEY);
            }
            else
            {
                tbSuDung.Text = string.Empty;
                tbSuDung.Enabled = false;

                lbTienStatus.Text = Constant.DEFAULT_MONEY_STATUS_DEBT;

                tbTongCK.Text = string.Empty;
            }

            CalculateTienHoiLai();
            ValidateHoanTat();
        }

        private void tbTienThanhToan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbTienThanhToan_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbTienThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

            tbTienThanhToan.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbTienThanhToan.Select(tbTienThanhToan.Text.Length, 0);

            CalculateTienHoiLai();

            ValidateHoanTat();
        }

        private void tbTienThanhToan_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTienThanhToan.Text))
            {
                tbTienThanhToan.Text = "0";
            }
        }

        private void tbSuDung_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));
            long tichLuy = dataKH == null ? 0 : dataKH.TichLuy;

            tbSuDung.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbSuDung.Select(tbSuDung.Text.Length, 0);

            if (!tbSuDung.ReadOnly && ConvertUtil.ConvertToLong(tbSuDung.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty)) >= tichLuy)
            {
                tbSuDung.Text = tichLuy.ToString(Constant.DEFAULT_FORMAT_MONEY);
            }
            else
            {
                tbTienThanhToan.ReadOnly = false;
            }

            CalculateTienHoiLai();

            ValidateHoanTat();
        }

        private void tbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbSuDung_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbTichLuy_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTichLuy.Text) && tbTichLuy.Text != "0")
            {
                tbSuDung.ReadOnly = false;
            }
            else
            {
                tbSuDung.Text = string.Empty;
                tbSuDung.ReadOnly = true;
            }
        }

        private void tbTenKH_MouseEnter(object sender, EventArgs e)
        {
            if (dataKH != null)
            {
                ttDetail.SetToolTip(tbTenKH, string.Format(Constant.TOOLTIP_DETAIL_KHACHHANG,
                    dataKH.Ten, dataKH.GioiTinh, dataKH.DOB.Value.ToString(Constant.DEFAULT_DATE_FORMAT),
                    dataKH.CMND, dataKH.DiaChi, dataKH.DienThoai, dataKH.DTDD, dataKH.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }

        private void tbNguoiBan_MouseEnter(object sender, EventArgs e)
        {
            if (FormMain.user != null)
            {
                ttDetail.SetToolTip(tbNguoiBan, string.Format(Constant.TOOLTIP_DETAIL_USER,
                    FormMain.user.Ten, FormMain.user.GioiTinh, FormMain.user.UserGroup.Ten, FormMain.user.UserName,
                    FormMain.user.CMND, FormMain.user.DienThoai, FormMain.user.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }
        #endregion

        private void rbTichLuy_CheckedChanged(object sender, EventArgs e)
        {
            CalculateMoney();
        }

        private void rbTrucTiep_CheckedChanged(object sender, EventArgs e)
        {
            CalculateMoney();
        }
    }
}