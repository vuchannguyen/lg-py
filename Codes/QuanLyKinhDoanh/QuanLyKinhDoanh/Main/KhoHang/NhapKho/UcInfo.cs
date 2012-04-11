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

namespace QuanLyKinhDoanh.Mua
{
    public partial class UcInfo : UserControl
    {
        private UserControl uc;
        private DTO.HoaDon dataHoaDon;
        private DTO.HoaDonDetail dataHoaDonDetail;
        private DTO.SanPham dataSP;
        private bool isFixedMoney;

        public UcInfo()
        {
            InitializeComponent();

            dataHoaDon = new HoaDon();
            dataHoaDonDetail = new HoaDonDetail();
            dataSP = new DTO.SanPham();

            if (InitSP() && Init())
            {
                RefreshDataSP();

                RefreshData();
            }
            else
            {
                this.Visible = false;
            }
        }

        public UcInfo(DTO.SanPham data)
        {
            InitializeComponent();

            this.dataSP = data;

            if (Init())
            {
                tbMa.Text = data.IdSanPham;

                cbGroup.Text = data.SanPhamGroup.Ten;

                //cbTen.Text = data.Ten;
                tbSoLuong.Text = data.SoLuong.ToString();
                tbDonViTinh.Text = data.DonViTinh;
                tbGiaNhap.Text = data.GiaMua.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
                tbGiaBan.Text = data.GiaBan.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
                tbLaiSuat.Text = data.LaiSuat.ToString();
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
                pbHuy.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_CANCEL);
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            catch
            {
                this.Dispose();
                //Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UcInfo_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);

            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);
            this.BringToFront();

            isFixedMoney = false;

            cbChangeMoney.SelectedIndex = 0;

            CreateNewId();

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Sản Phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void RefreshData()
        {
            tbMa.Text = dataSP.IdSanPham;
            //tbSoLuong.Text = string.Empty;
            tbDonViTinh.Text = string.Empty;
            tbGiaNhap.Text = string.Empty;
            tbGiaBan.Text = string.Empty;
            tbLaiSuat.Text = string.Empty;
            tbThanhTien.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            cbGroup.SelectedIndex = 0;

            //if (cbTen.Items.Count > 0)
            //{
            //    cbTen.SelectedIndex = 0;
            //}
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMa.Text) &&
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

            string idSanPham = string.Empty;
            DTO.HoaDonDetail dataTemp = HoaDonDetailBus.GetLastData();

            id = dataTemp == null ? 1 : dataTemp.Id + 1;

            tbMa.Text = Constant.PREFIX_MUA + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void CalculateTotalMoney()
        {
            long giaNhap = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            int soLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);

            tbThanhTien.Text = giaNhap * soLuong == 0 ? string.Empty : (giaNhap * soLuong).ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
        }

        private void Insert()
        {
            if (InsertDataSP())
            {
                InsertDataHoaDon();
            }
        }

        private void InsertDataHoaDon()
        {
            dataHoaDon = new HoaDon();

            dataHoaDon.IdHoaDon = tbMa.Text;
            dataHoaDon.IdType = Constant.ID_TYPE_MUA;
            dataHoaDon.Status = Constant.STATUS_DONE;
            dataHoaDon.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            dataHoaDon.GhiChu = tbGhiChu.Text;

            dataHoaDon.CreateBy = dataHoaDon.UpdateBy = "";
            dataHoaDon.CreateDate = dataHoaDon.UpdateDate = DateTime.Now;

            if (HoaDonBus.Insert(dataHoaDon))
            {
                InsertDataHoaDonDetail(dataHoaDon.Id);
            }
            else
            {
                try
                {
                    HoaDonBus.Delete(dataHoaDon);
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
            //dataHoaDonDetail.IdSanPham = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value);
            dataHoaDonDetail.IdSanPham = dataSP.Id;
            dataHoaDonDetail.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataHoaDonDetail.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

            if (HoaDonDetailBus.Insert(dataHoaDonDetail))
            {
                UpdatedataSP();
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

        private void UpdatedataSP()
        {
            //dataSP = SanPhamBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value));
            //dataSP = SanPhamBus.GetById(dataSP.Id);

            dataSP.SoLuong += ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataSP.GiaMua = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            dataSP.GiaBan = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            dataSP.LaiSuat = ConvertUtil.ConvertToDouble(tbLaiSuat.Text);

            dataSP.UpdateBy = "";
            dataSP.UpdateDate = DateTime.Now;

            if (SanPhamBus.Update(dataSP))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Sản phẩm " + dataSP.IdSanPham) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
        }
        #endregion



        #region Ok Cancel
        private void pbHuy_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            Insert();
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

        //private void pbThem_Click(object sender, EventArgs e)
        //{
        //    uc = new QuanLyKinhDoanh.SanPham.UcInfo();
        //    //uc.Disposed += new EventHandler(uc_Disposed);
        //    this.Controls.Add(uc);
        //}

        //private void pbThem_MouseEnter(object sender, EventArgs e)
        //{
        //    pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD_MOUSEROVER);

        //    ttDetail.SetToolTip(pbThem, Constant.TOOLTIP_MUA_THEM_SAN_PHAM);
        //}

        //private void pbThem_MouseLeave(object sender, EventArgs e)
        //{
        //    pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
        //}



        #region Controls
        //private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int idGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
        //    List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, idGroup, string.Empty, string.Empty, 0, 0);

        //    cbTen.Items.Clear();

        //    foreach (DTO.SanPham data in listData)
        //    {
        //        cbTen.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
        //    }

        //    if (listData.Count > 0)
        //    {
        //        cbTen.SelectedIndex = 0;
        //    }
        //}

        //private void cbTen_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int idSanPham = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value);

        //    tbDonViTinh.Text = SanPhamBus.GetById(idSanPham).DonViTinh;

        //    ValidateInput();
        //}

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
            long money = 0;

            money = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

            tbGiaNhap.Text = money.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
            tbGiaNhap.Select(tbGiaNhap.Text.Length, 0);

            if (tbGiaNhap.Text.Length > 0)
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

                long moneyBuy = 0;
                long moneySell = 0;

                moneyBuy = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
                moneySell = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

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
                long money = 0;

                money = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

                tbGiaBan.Text = money == 0 ? "0" : money.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
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
                long moneySell = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
                long moneyBuy = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

                if (cbChangeMoney.SelectedIndex == 0)
                {
                    if (!isFixedMoney)
                    {
                        moneySell = moneyBuy + (moneyBuy * ConvertUtil.ConvertToLong(tbLaiSuat.Text) / 100);

                        tbGiaBan.Text = moneySell == 0 ? "0" : moneySell.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
                    }
                    else
                    {
                        isFixedMoney = false;
                    }
                }
                else
                {
                    moneyBuy = (long)(moneySell / (ConvertUtil.ConvertToLong(tbLaiSuat.Text) * 1.0 / 100 + 1));

                    tbGiaNhap.Text = moneyBuy == 0 ? string.Empty : moneyBuy.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
                }
            }
        }

        private void tbSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbSoLuong_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalMoney();
            ValidateInput();
        }
        #endregion



        #region Function SP
        private bool InitSP()
        {
            List<DTO.SanPhamGroup> listData = SanPhamGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm Sản Phẩm"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.SanPhamGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void RefreshDataSP()
        {
            tbMaSP.Text = string.Empty;
            tbTen.Text = string.Empty;
            cbDVTSP.Text = string.Empty;
            tbXuatXu.Text = string.Empty;
            tbHieu.Text = string.Empty;
            tbThoiGianBaoHanh.Text = string.Empty;
            tbSize.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            cbGroup.SelectedIndex = 0;
            cbDonViBaoHanh.SelectedIndex = 0;
        }

        private void CreateNewIdSP()
        {
            int id = 0;
            SanPhamGroup SPGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            //if (isUpdate)
            //{
            //    id = data.Id;
            //}
            //else
            //{
            string idSanPham = string.Empty;
            DTO.SanPham dataTemp = SanPhamBus.GetLastData(SPGroup.Id);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.IdSanPham.Substring(dataTemp.IdSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            //}

            tbMaSP.Text = SPGroup.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

            ValidateInputSP();
        }

        private void ValidateInputSP()
        {
            if (!string.IsNullOrEmpty(tbMaSP.Text) &&
                !string.IsNullOrEmpty(tbTen.Text) &&
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

            dataSP.IdSanPham = tbMaSP.Text;
            dataSP.Ten = tbTen.Text;
            dataSP.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            dataSP.MoTa = tbMoTa.Text;
            dataSP.DonViTinh = cbDVTSP.Text;
            dataSP.XuatXu = tbXuatXu.Text;
            dataSP.Hieu = tbHieu.Text;
            dataSP.Size = tbSize.Text;
            dataSP.ThoiGianBaoHanh = ConvertUtil.ConvertToByte(tbThoiGianBaoHanh.Text);
            dataSP.DonViBaoHanh = cbDonViBaoHanh.Text;

            dataSP.CreateBy = dataSP.UpdateBy = "";
            dataSP.CreateDate = dataSP.UpdateDate = DateTime.Now;

            if (SanPhamBus.Insert(dataSP))
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
        #endregion



        #region Controls
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateNewIdSP();
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInputSP();
        }

        private void tbThoiGianBaoHanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
        #endregion

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
            cbDVTSP.Text = cbDVTSP.Text.ToUpper();
            cbDVTSP.Select(cbDVTSP.Text.Length, 0);

            tbDonViTinh.Text = cbDVTSP.Text;

            ValidateInputSP();
        }
    }
}
