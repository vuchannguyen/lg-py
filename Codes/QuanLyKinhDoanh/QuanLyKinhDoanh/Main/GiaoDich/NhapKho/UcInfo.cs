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
        private DTO.SanPham dataSanPham;

        public UcInfo()
        {
            InitializeComponent();

            dataHoaDon = new HoaDon();
            dataHoaDonDetail = new HoaDonDetail();
            dataSanPham = new DTO.SanPham();

            if (Init())
            {
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

            this.dataSanPham = data;

            if (Init())
            {
                tbMa.Text = data.IdSanPham;

                cbGroup.Text = data.SanPhamGroup.Ten;

                cbTen.Text = data.Ten;
                tbSoLuong.Text = data.SoLuong.ToString();
                tbDonViTinh.Text = data.DonViTinh;
                tbGiaNhap.Text = data.GiaMua.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
                tbThanhTien.Text =
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
                pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);

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
            tbMa.Text = dataSanPham.IdSanPham;
            tbSoLuong.Text = string.Empty;
            tbDonViTinh.Text = string.Empty;
            tbGiaNhap.Text = string.Empty;
            tbGiaBan.Text = string.Empty;
            tbLaiSuat.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            cbGroup.SelectedIndex = 0;

            if (cbTen.Items.Count > 0)
            {
                cbTen.SelectedIndex = 0;
            }
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMa.Text) &&
                !string.IsNullOrEmpty(cbTen.Text) &&
                !string.IsNullOrEmpty(tbGiaNhap.Text) &&
                !string.IsNullOrEmpty(tbGiaBan.Text) &&
                !string.IsNullOrEmpty(tbSoLuong.Text) &&
                !string.IsNullOrEmpty(cbGroup.Text)
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
            InsertDataHoaDon();
            UpdateDataSanPham();
        }

        private void InsertDataHoaDon()
        {
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
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void InsertDataHoaDonDetail(int idHoaDon)
        {
            dataHoaDonDetail.IdHoaDon = idHoaDon;
            dataHoaDonDetail.IdSanPham = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value);
            dataHoaDonDetail.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataHoaDonDetail.ThanhTien = ConvertUtil.ConvertToLong(tbThanhTien.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

            if (HoaDonDetailBus.Insert(dataHoaDonDetail))
            {
                UpdateDataSanPham();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateDataSanPham()
        {
            dataSanPham.SoLuong = ConvertUtil.ConvertToInt(tbSoLuong.Text);
            dataSanPham.GiaMua = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            dataSanPham.GiaBan = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            dataSanPham.LaiSuat = ConvertUtil.ConvertToDouble(tbLaiSuat.Text);

            dataSanPham.UpdateBy = "";
            dataSanPham.UpdateDate = DateTime.Now;

            if (SanPhamBus.Update(dataSanPham))
            {
                this.Dispose();
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

        private void pbThem_Click(object sender, EventArgs e)
        {
            uc = new QuanLyKinhDoanh.SanPham.UcInfo();
            //uc.Disposed += new EventHandler(uc_Disposed);
            this.Controls.Add(uc);
        }

        private void pbThem_MouseEnter(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD_MOUSEROVER);

            ttDetail.SetToolTip(pbThem, Constant.TOOLTIP_MUA_THEM_SAN_PHAM);
        }

        private void pbThem_MouseLeave(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_ADD);
        }



        #region Controls
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, idGroup, string.Empty, string.Empty, 0, 0);

            cbTen.Items.Clear();

            foreach (DTO.SanPham data in listData)
            {
                cbTen.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            if (listData.Count > 0)
            {
                cbTen.SelectedIndex = 0;
            }
        }

        private void cbTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSanPham = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value);

            tbDonViTinh.Text = SanPhamBus.GetById(idSanPham).DonViTinh;

            ValidateInput();
        }

        private void tbGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbGiaNhap_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbGiaBan.Text))
            {
                tbGiaBan_Leave(sender, e);
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
            else
            {
                tbGiaBan.Text = string.Empty;
                tbLaiSuat.Text = string.Empty;

                tbGiaBan.Enabled = false;
                tbLaiSuat.Enabled = false;
            }

            ValidateInput();
        }

        private void tbGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbGiaBan_Leave(object sender, EventArgs e)
        {
            long moneyBuy = 0;
            long moneySell = 0;

            moneyBuy = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));
            moneySell = ConvertUtil.ConvertToLong(tbGiaBan.Text.Replace(Constant.LINK_SYMBOL_MONEY, ""));

            double laiSuat = moneySell * 1.0 / moneyBuy * 100;
            if (Math.Round(laiSuat) > Constant.DEFAULT_MAX_PERCENT_PROFIT)
            {
                laiSuat = Constant.DEFAULT_MAX_PERCENT_PROFIT;
            }

            if (Math.Round(laiSuat) == 0)
            {
                tbGiaBan.Text = "0";
            }

            tbLaiSuat.Text = Math.Round(laiSuat).ToString();
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
                long money = 0;

                money = ConvertUtil.ConvertToLong(tbGiaNhap.Text.Replace(Constant.LINK_SYMBOL_MONEY, "")) * ConvertUtil.ConvertToLong(tbLaiSuat.Text) / 100;

                tbGiaBan.Text = money == 0 ? "0" : money.ToString("#" + Constant.LINK_SYMBOL_MONEY + "###");
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
    }
}
