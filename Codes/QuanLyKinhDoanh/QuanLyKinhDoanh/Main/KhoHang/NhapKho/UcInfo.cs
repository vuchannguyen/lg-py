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

namespace QuanLyKinhDoanh.NhapKho
{
    public partial class UcInfo : UserControl
    {
        private UserControl uc;
        private DTO.HoaDon dataHoaDon;
        private DTO.HoaDonDetail dataHoaDonDetail;
        private DTO.SanPham dataSP;
        private DTO.ChietKhau dataChietKhau;
        private DTO.XuatXu dataXuatXu;
        private bool isFixedMoney;

        private bool isUpdate;

        private Point point_Pic;
        private Point point_PicBound;
        private Size size_Pic;
        private Size size_PicRec;

        private int iZoom;
        private Image imgZoom;
        private Image imgAvatar;
        private bool bNewAvatar;
        private List<string> list_FolderAvatar;

        private string avatarPath;

        public UcInfo()
        {
            InitializeComponent();

            dataHoaDon = new HoaDon();
            dataHoaDonDetail = new HoaDonDetail();
            dataSP = new DTO.SanPham();
            dataChietKhau = new ChietKhau();

            isUpdate = false;
            bNewAvatar = false;

            if (InitSP() && Init())
            {
                RefreshDataSP();

                RefreshData();

                CreateNewId();
            }
            else
            {
                this.Visible = false;
            }
        }

        public UcInfo(DTO.HoaDonDetail data)
        {
            InitializeComponent();

            dataHoaDon = data.HoaDon;
            dataSP = data.SanPham;
            dataChietKhau = ChietKhauBus.GetByIdSP(data.IdSanPham);
            dataXuatXu = data.SanPham.XuatXu;

            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            tbSoLuong.ReadOnly = true;
            cbChangeMoney.SelectedIndex = 0;
            this.dataHoaDonDetail = data;

            if (InitSP() && Init())
            {
                tbMaSP.Text = data.SanPham.MaSanPham;
                cbGroup.Text = data.SanPham.SanPhamGroup.Ten;
                cbDVTSP.Text = data.SanPham.DonViTinh;
                tbTenSP.Text = data.SanPham.Ten;
                tbSize.Text = data.SanPham.Size;
                cbXuatXu.Text = data.SanPham.XuatXu == null ? string.Empty : data.SanPham.XuatXu.Ten;
                tbHieu.Text = data.SanPham.Hieu;
                tbThoiHan.Text = data.SanPham.ThoiHan == 0 ? string.Empty : data.SanPham.ThoiHan.ToString();
                cbDonViThoiHan.Text = data.SanPham.DonViThoiHan;
                tbMoTa.Text = data.SanPham.MoTa;

                tbMaNhap.Text = data.HoaDon.MaHoaDon;
                tbGiaNhap.Text = data.SanPham.GiaMua.ToString(Constant.DEFAULT_FORMAT_MONEY);
                tbSoLuong.Text = data.SoLuong.ToString();
                tbLaiSuat.Text = data.SanPham.LaiSuat.ToString();
                tbGiaBan.Text = data.SanPham.GiaBan.ToString(Constant.DEFAULT_FORMAT_MONEY);
                tbGhiChu.Text = data.HoaDon.GhiChu;

                avatarPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(data.SanPham.MaSanPham, data.SanPham.CreateDate));

                if (File.Exists(avatarPath))
                {
                    string sImage = Convert_Function.ConvertByteArrayToString(Convert_Function.ConvertImageToByteArray(Image.FromFile(avatarPath)));
                    pbAvatar.Image = Convert_Function.ConvertByteArrayToImage(Convert_Function.ConvertStringToByteArray(sImage));

                    //pbAvatar.Image = Image.FromFile(sAvatarPath);
                    bNewAvatar = true;
                }

                tbChietKhau.Text = ChietKhauBus.GetByIdSP(data.IdSanPham) == null ? string.Empty :
                    ChietKhauBus.GetByIdSP(data.IdSanPham).Value.ToString();
            }
            else
            {
                this.Visible = false;
            }
        }

        private string setAvatarPath(string ma, DateTime date)
        {
            avatarPath = ma + Constant.DEFAULT_DATE_TIME_AVATAR_FORMAT;

            return avatarPath;
        }

        private void LoadResource()
        {
            try
            {
                pbThemNhomSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);

                pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcInfo_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);
            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            this.BringToFront();

            pbAvatar.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pbAvatar_MouseWheel);

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("Store");
            list_FolderAvatar.Add("SP");

            return true;
        }

        private void RefreshData()
        {
            tbSoLuong.Text = string.Empty;
            tbDonViTinh.Text = string.Empty;
            tbGiaNhap.Text = string.Empty;
            tbGiaBan.Text = string.Empty;
            tbLaiSuat.Text = string.Empty;
            tbThanhTien.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            cbChangeMoney.SelectedIndex = 0;
            cbDonViThoiHan.SelectedIndex = 1;

            isFixedMoney = false;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMaNhap.Text) &&
                //!string.IsNullOrEmpty(cbTen.Text) &&
                !string.IsNullOrEmpty(tbGiaNhap.Text) &&
                !string.IsNullOrEmpty(tbGiaBan.Text) &&
                !string.IsNullOrEmpty(tbSoLuong.Text)
                //!string.IsNullOrEmpty(cbGroup.Text)
                )
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

        private void CreateNewId()
        {
            int id = 0;

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_MUA);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            tbMaNhap.Text = Constant.PREFIX_MUA + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void CalculateTotalMoney()
        {
            long giaNhap = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            int soLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);

            tbThanhTien.Text = giaNhap * soLuong == 0 ? string.Empty : (giaNhap * soLuong).ToString(Constant.DEFAULT_FORMAT_MONEY);
        }

        private void InsertData()
        {
            if (InsertDataSP())
            {
                InsertDataHoaDon();
            }
        }

        private void InsertDataHoaDon()
        {
            dataHoaDon = new HoaDon();

            dataHoaDon.MaHoaDon = tbMaNhap.Text;
            dataHoaDon.IdUser = FormMain.user.Id;
            dataHoaDon.IdType = Constant.ID_TYPE_MUA;
            dataHoaDon.IdStatus = Constant.ID_STATUS_DONE;
            dataHoaDon.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
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

                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
                else
                {
                    CreateNewId();
                }
            }
        }

        private void InsertDataHoaDonDetail(int idHoaDon)
        {
            dataHoaDonDetail = new HoaDonDetail();

            dataHoaDonDetail.IdHoaDon = idHoaDon;
            dataHoaDonDetail.IdSanPham = dataSP.Id;
            dataHoaDonDetail.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataHoaDonDetail.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            if (HoaDonDetailBus.Insert(dataHoaDonDetail))
            {
                InsertChietKhau();
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

                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void InsertChietKhau()
        {
            dataChietKhau = new ChietKhau();

            dataChietKhau.IdSanPham = dataSP.Id;
            dataChietKhau.Value = ConvertUtil.ConvertToInt(tbChietKhau.Text);

            if (ChietKhauBus.Insert(dataChietKhau, FormMain.user))
            {
                UpdatePriceSP();
            }
            else
            {
                try
                {
                    ChietKhauBus.Delete(dataChietKhau, FormMain.user);
                }
                catch
                {
                    //
                }

                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            if (UpdateDataSP())
            {
                UpdateDataHoaDon();
            }
        }

        private bool UpdateDataSP()
        {
            dataSP = SanPhamBus.GetById(dataHoaDonDetail.IdSanPham);

            dataSP.MaSanPham = tbMaSP.Text;
            dataSP.Ten = tbTenSP.Text;
            dataSP.SanPhamGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            if (cbXuatXu.SelectedItem != null)
            {
                dataSP.XuatXu = XuatXuBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbXuatXu.SelectedItem).Value));
            }
            else
            {
                dataSP.XuatXu = null;
            }

            dataSP.MoTa = tbMoTa.Text;
            dataSP.DonViTinh = tbDonViTinh.Text;
            dataSP.Hieu = tbHieu.Text;
            dataSP.Size = tbSize.Text;
            dataSP.ThoiHan = ConvertUtil.ConvertToByte(tbThoiHan.Text);
            dataSP.DonViThoiHan = cbDonViThoiHan.Text;

            if (SanPhamBus.Update(dataSP, FormMain.user))
            {
                return true;
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.Dispose();
                }
            }

            return false;
        }

        private void UpdateDataHoaDon()
        {
            dataHoaDon.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            dataHoaDon.GhiChu = tbGhiChu.Text;

            if (HoaDonBus.Update(dataHoaDon, FormMain.user))
            {
                UpdateDataHoaDonDetail();
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

                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
                else
                {
                    CreateNewId();
                }
            }
        }

        private void UpdateDataHoaDonDetail()
        {
            dataHoaDonDetail.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataHoaDonDetail.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            if (HoaDonDetailBus.Update(dataHoaDonDetail))
            {
                UpdateChietKhau();
            }
        }

        private void UpdateChietKhau()
        {
            if (dataChietKhau == null)
            {
                InsertChietKhau();
            }
            else
            {
                dataChietKhau.Value = ConvertUtil.ConvertToInt(tbChietKhau.Text);

                if (ChietKhauBus.Update(dataChietKhau, FormMain.user))
                {
                    UpdatePriceSP();
                }
                else
                {
                    try
                    {
                        ChietKhauBus.Delete(dataChietKhau, FormMain.user);
                    }
                    catch
                    {
                        //
                    }

                    if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Dispose();
                    }
                }
            }
        }
        #endregion



        #region Ok Cancel
        private void pbHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_EXIT, Constant.CAPTION_WARNING, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Dispose(); 
            }
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL_MOUSEOVER);
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            pbHoanTat.Focus();

            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!isUpdate)
                {
                    InsertData();
                }
                else
                {
                    UpdateData();
                }
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
        #endregion



        #region Controls Nhap
        private void tbGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbGiaNhap_Leave(object sender, EventArgs e)
        {
            if (cbChangeMoney.SelectedIndex == 0)
            {
                isFixedMoney = false;
            }

            if (!string.IsNullOrEmpty(tbGiaBan.Text))
            {
                tbLaiSuat_TextChanged(sender, e);
            }
        }

        private void tbGiaNhap_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

            tbGiaNhap.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbGiaNhap.Select(tbGiaNhap.Text.Length, 0);

            if (!string.IsNullOrEmpty(tbGiaNhap.Text))
            {
                tbGiaBan.Enabled = true;
                tbLaiSuat.Enabled = true;

                CalculateTotalMoney();
            }
            else if (cbChangeMoney.SelectedIndex == 0)
            {
                tbGiaBan.Enabled = false;
                tbLaiSuat.Enabled = false;

                tbGiaBan.Text = string.Empty;
                tbLaiSuat.Text = string.Empty;
            }

            ValidateInput();
        }

        private void tbGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbGiaBan_Leave(object sender, EventArgs e)
        {
            if (cbChangeMoney.SelectedIndex == 0)
            {
                isFixedMoney = true;

                long moneyBuy = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                long moneySell = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                double laiSuat = (moneySell * 1.0 / moneyBuy * 100) - 100;

                if (Math.Round(laiSuat) > Constant.DEFAULT_MAX_PERCENT_PROFIT)
                {
                    laiSuat = Constant.DEFAULT_MAX_PERCENT_PROFIT;

                    isFixedMoney = false;
                }

                if (Math.Round(laiSuat) <= 0)
                {
                    laiSuat = 0;
                }

                tbLaiSuat.Text = Math.Round(laiSuat).ToString();
            }
            else
            {
                if (string.IsNullOrEmpty(tbLaiSuat.Text))
                {
                    tbLaiSuat.Text = "0";
                }

                tbLaiSuat_TextChanged(sender, e);
            }
        }

        private void tbGiaBan_TextChanged(object sender, EventArgs e)
        {
            if (tbGiaBan.Enabled)
            {
                long money = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                tbGiaBan.Text = money == 0 ? "0" : money.ToString(Constant.DEFAULT_FORMAT_MONEY);
                tbGiaBan.Select(tbGiaBan.Text.Length, 0);

                ValidateInput();
            }
        }

        private void tbLaiSuat_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbLaiSuat_TextChanged(object sender, EventArgs e)
        {
            if (tbLaiSuat.Enabled)
            {
                long moneySell = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
                long moneyBuy = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));

                if (cbChangeMoney.SelectedIndex == 0)
                {
                    if (!isFixedMoney)
                    {
                        moneySell = moneyBuy + (moneyBuy * ConvertUtil.ConvertToLong(tbLaiSuat.Text) / 100);

                        tbGiaBan.Text = moneySell == 0 ? "0" : moneySell.ToString(Constant.DEFAULT_FORMAT_MONEY);
                    }
                    else
                    {
                        isFixedMoney = false;
                    }
                }
                else
                {
                    moneyBuy = (long)(moneySell / (ConvertUtil.ConvertToLong(tbLaiSuat.Text) * 1.0 / 100 + 1));

                    tbGiaNhap.Text = moneyBuy == 0 ? string.Empty : moneyBuy.ToString(Constant.DEFAULT_FORMAT_MONEY);
                }
            }
        }

        private void tbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            tbSoLuong.Text = ConvertUtil.ConvertToInt(tbSoLuong.Text) == 0 ? string.Empty :
                ConvertUtil.ConvertToInt(tbSoLuong.Text).ToString();

            CalculateTotalMoney();
            ValidateInput();
        }
        #endregion



        #region Function SP
        private void uc_Disposed(object sender, EventArgs e)
        {
            if (!InitSP())
            {
                RefreshData();
                RefreshDataSP();

                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm sản phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            }
        }

        private bool InitSP()
        {
            if (!GetListGroupSP())
            {
                return false;
            }

            GetListXuatXu();

            return true;
        }

        private void RefreshDataSP()
        {
            tbMaSP.Text = string.Empty;
            tbTenSP.Text = string.Empty;
            cbDVTSP.Text = string.Empty;
            cbXuatXu.Text = string.Empty;
            tbHieu.Text = string.Empty;
            tbThoiHan.Text = string.Empty;
            tbSize.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbDonViThoiHan.SelectedIndex = 0;
            cbDVTSP.SelectedIndex = 0;
        }

        private bool GetListGroupSP()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm sản phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private bool GetListXuatXu()
        {
            List<DTO.XuatXu> listData = XuatXuBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                //
            }

            cbXuatXu.Items.Clear();

            foreach (DTO.XuatXu data in listData)
            {
                cbXuatXu.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void CreateNewIdSP()
        {
            int id = 0;
            SanPhamGroup SPGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            if (isUpdate)
            {
                string oldIdNumber = dataSP == null ? string.Empty : dataSP.MaSanPham.Substring(dataSP.MaSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = dataSP == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber);
            }
            else
            {
                string idSanPham = string.Empty;
                DTO.SanPham dataTemp = SanPhamBus.GetLastData(SPGroup.Id);

                string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaSanPham.Substring(dataTemp.MaSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            }

            tbMaSP.Text = SPGroup.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

            ValidateInputSP();
        }

        private void ValidateInputSP()
        {
            if (!string.IsNullOrEmpty(tbMaSP.Text) &&
                !string.IsNullOrEmpty(tbTenSP.Text) &&
                !string.IsNullOrEmpty(cbDVTSP.Text)
                )
            {
                gbInfo.Enabled = true;
            }
            else
            {
                gbInfo.Enabled = false;
            }
        }

        private bool InsertDataSP()
        {
            dataSP = new DTO.SanPham();

            dataSP.MaSanPham = tbMaSP.Text;
            dataSP.Ten = tbTenSP.Text;
            dataSP.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);

            if (cbXuatXu.SelectedItem != null)
            {
                dataSP.IdXuatXu = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbXuatXu.SelectedItem).Value);
            }

            dataSP.MoTa = tbMoTa.Text;
            dataSP.DonViTinh = cbDVTSP.Text;
            dataSP.Hieu = tbHieu.Text;
            dataSP.Size = tbSize.Text;
            dataSP.ThoiHan = ConvertUtil.ConvertToByte(tbThoiHan.Text);
            dataSP.DonViThoiHan = cbDonViThoiHan.Text;

            if (SanPhamBus.Insert(dataSP, FormMain.user))
            {
                return true;
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }

            return false;
        }

        private void UpdatePriceSP()
        {
            dataSP.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataSP.GiaMua = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            dataSP.GiaBan = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            dataSP.LaiSuat = ConvertUtil.ConvertToDouble(tbLaiSuat.Text);

            if (SanPhamBus.Update(dataSP, FormMain.user))
            {
                if (!isUpdate)
                {
                    if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Sản phẩm " + dataSP.MaSanPham) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE,
                        Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        this.Dispose();
                    }
                    else
                    {
                        CreateNewIdSP();
                        CreateNewId();
                    }
                }
                else
                {
                    this.Dispose();
                }
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT,
                    Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
        }
        #endregion



        #region Controls SP
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateNewIdSP();
        }

        private void tbTenSP_TextChanged(object sender, EventArgs e)
        {
            ValidateInputSP();
        }

        private void tbThoiHan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void cbChangeMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChangeMoney.SelectedIndex == 1)
            {
                tbGiaBan.Enabled = true;
                tbLaiSuat.Enabled = true;

                tbLaiSuat_TextChanged(sender, e);
            }
        }

        private void cbDVTSP_TextChanged(object sender, EventArgs e)
        {
            //cbDVTSP.Text = cbDVTSP.Text.ToUpper();
            //cbDVTSP.Select(cbDVTSP.Text.Length, 0);

            tbDonViTinh.Text = cbDVTSP.Text;

            ValidateInputSP();
        }
        #endregion



        private void pbThemNhomSP_Click(object sender, EventArgs e)
        {
            uc = new NhomSanPham.UcInfo();
            uc.Disposed += new EventHandler(uc_Disposed);
            this.Controls.Add(uc);
        }

        private void pbThemNhomSP_MouseEnter(object sender, EventArgs e)
        {
            pbThemNhomSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD_MOUSEROVER);
        }

        private void pbThemNhomSP_MouseLeave(object sender, EventArgs e)
        {
            pbThemNhomSP.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
        }

        private void cbGroup_DropDownClosed(object sender, EventArgs e)
        {
            cbDVTSP.Focus();
        }

        private void cbDVTSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbTenSP.Focus();
        }

        private void cbDonViThoiHan_DropDownClosed(object sender, EventArgs e)
        {
            tbHieu.Focus();
        }

        private void cbChangeMoney_DropDownClosed(object sender, EventArgs e)
        {
            tbGiaNhap.Focus();
        }

        private void tbChietKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbChietKhau_TextChanged(object sender, EventArgs e)
        {
            tbChietKhau.Text = ConvertUtil.ConvertToInt(tbChietKhau.Text) == 0 ? string.Empty :
                ConvertUtil.ConvertToInt(tbChietKhau.Text).ToString();
        }

        private void cbXuatXu_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataXuatXu = XuatXuBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbXuatXu.SelectedItem).Value));

            tbThoiHan.Focus();
        }

        private void cbXuatXu_MouseEnter(object sender, EventArgs e)
        {
            if (dataXuatXu != null)
            {
                ttDetail.SetToolTip(cbXuatXu, string.Format(Constant.TOOLTIP_DETAIL_XUATXU,
                    dataXuatXu.Ten, dataXuatXu.DiaChi, dataXuatXu.DienThoai, dataXuatXu.Fax, dataXuatXu.Email));
            }
            else
            {
                ttDetail.RemoveAll();
            }
        }

        private void cbXuatXu_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbXuatXu.Text))
            {
                dataXuatXu = null;
            }
        }

        private void pbBrowse_Click(object sender, EventArgs e)
        {
            string sPath = File_Function.OpenDialog("JPG file", "jpg");

            if (sPath != null)
            {
                try
                {
                    imgAvatar = Image.FromFile(sPath);
                }
                catch
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_OPEN_AVATAR, Constant.CAPTION_ERROR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                if (imgAvatar.Width > Constant.DEFAULT_AVATAR_MAX_WIDTH || imgAvatar.Height > Constant.DEFAULT_AVATAR_MAX_HEIGHT)
                {
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(Constant.DEFAULT_AVATAR_MAX_WIDTH, Constant.DEFAULT_AVATAR_MAX_HEIGHT));
                }
                else
                {
                    imgZoom = imgAvatar;
                }

                if (imgAvatar.Width >= Constant.DEFAULT_AVATAR_WIDTH && imgAvatar.Height >= Constant.DEFAULT_AVATAR_HEIGHT)
                {
                    pbAvatar.Cursor = Cursors.SizeAll;
                    pbAvatar.Enabled = true;

                    if (imgZoom.Width > imgZoom.Height)
                    {
                        iZoom = imgZoom.Width;
                    }
                    else
                    {
                        iZoom = imgZoom.Height;
                    }

                    point_Pic = new Point(imgZoom.Width / 2 - size_PicRec.Width / 2, imgZoom.Height / 2 - size_PicRec.Height / 2);
                    point_PicBound = point_Pic;
                    size_Pic.Width = imgZoom.Width;
                    size_Pic.Height = imgZoom.Height;

                    //pbAvatar.Image.Dispose();
                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    bNewAvatar = true;
                }
                else
                {
                    MessageBox.Show(Constant.MESSAGE_ERROR_TOO_SMALL_AVATAR, Constant.CAPTION_ERROR,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pbBrowse_MouseEnter(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_BROWSE_MOUSEOVER);
        }

        private void pbBrowse_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_BROWSE);
        }

        private void pbAvatar_MouseDown(object sender, MouseEventArgs e)
        {
            point_Pic.X += e.X;
            point_Pic.Y += e.Y;
        }

        private void pbAvatar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point_PicBound.X = point_Pic.X - e.X;
                point_PicBound.Y = point_Pic.Y - e.Y;

                point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
            }
        }

        private void pbAvatar_MouseUp(object sender, MouseEventArgs e)
        {
            point_Pic.X = point_PicBound.X;
            point_Pic.Y = point_PicBound.Y;
        }

        private void pbAvatar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (iZoom + 100 < 1000)
                {
                    iZoom += 100;
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                    size_Pic.Width = imgZoom.Width;
                    size_Pic.Height = imgZoom.Height;
                    point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);

                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    pbAvatar_MouseUp(sender, e);
                }
            }

            if (e.Delta < 0)
            {
                if (iZoom - 100 > 100)
                {
                    iZoom -= 100;
                    imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                    if (imgZoom.Width >= size_PicRec.Width && imgZoom.Height >= size_PicRec.Height)
                    {
                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }
                    else
                    {
                        iZoom += 100;
                        imgZoom = Image_Function.resizeImage(imgAvatar, new Size(iZoom, iZoom));

                        size_Pic.Width = imgZoom.Width;
                        size_Pic.Height = imgZoom.Height;
                        point_PicBound = Image_Function.setPicBound(point_PicBound, size_Pic, size_PicRec);
                    }

                    pbAvatar.Image = Image_Function.CropImage(imgZoom, new Rectangle(point_PicBound, size_PicRec), pbAvatar.ClientRectangle);
                    pbAvatar_MouseUp(sender, e);
                }
            }
        }

        private void pbAvatar_MouseEnter(object sender, EventArgs e)
        {
            pbAvatar.Focus();
        }

        private void pbAvatar_MouseLeave(object sender, EventArgs e)
        {
            pbBrowse.Focus();
        }
    }
}
