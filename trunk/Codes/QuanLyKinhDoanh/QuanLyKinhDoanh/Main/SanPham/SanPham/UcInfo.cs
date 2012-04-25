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

namespace QuanLyKinhDoanh.SanPham
{
    public partial class UcInfo : UserControl
    {
        private DTO.SanPham data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.SanPham();
            isUpdate = false;

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

            this.data = data;
            isUpdate = true;

            if (Init())
            {
                tbMa.Text = data.IdSanPham;
                tbTen.Text = data.Ten;
                tbDonViTinh.Text = data.DonViTinh;
                cbXuatXu.Text = data.XuatXu == null ? string.Empty : data.XuatXu.Ten;
                tbHieu.Text = data.Hieu;
                tbThoiGianBaoHanh.Text = data.ThoiGianBaoHanh.ToString();
                tbSize.Text = data.Size;
                tbMoTa.Text = data.MoTa;

                cbGroup.Text = data.SanPhamGroup.Ten;
                cbDonViBaoHanh.Text = data.DonViBaoHanh;
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

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            if (!GetListGroupSP())
            {
                return false;
            }

            GetListXuatXu();

            return true;
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            tbTen.Text = string.Empty;
            tbDonViTinh.Text = string.Empty;
            tbHieu.Text = string.Empty;
            tbThoiGianBaoHanh.Text = string.Empty;
            tbSize.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbXuatXu.SelectedIndex = cbXuatXu.Items.Count > 0 ? 0 : -1;
            cbDonViBaoHanh.SelectedIndex = 0;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMa.Text) &&
                !string.IsNullOrEmpty(tbTen.Text) &&
                !string.IsNullOrEmpty(tbDonViTinh.Text)
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
                //MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Xuất xứ"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //return false;
            }

            cbXuatXu.Items.Clear();

            foreach (DTO.XuatXu data in listData)
            {
                cbXuatXu.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void InsertData()
        {
            data.IdSanPham = tbMa.Text;
            data.Ten = tbTen.Text;
            data.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            data.MoTa = tbMoTa.Text;
            data.DonViTinh = tbDonViTinh.Text;
            //data.XuatXu = tbXuatXu.Text;
            data.Hieu = tbHieu.Text;
            data.Size = tbSize.Text;
            data.ThoiGianBaoHanh = ConvertUtil.ConvertToByte(tbThoiGianBaoHanh.Text);
            data.DonViBaoHanh = cbDonViBaoHanh.Text;

            data.CreateBy = data.UpdateBy = "";
            data.CreateDate = data.UpdateDate = DateTime.Now;

            if (SanPhamBus.Insert(data))
            {
                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.IdSanPham = tbMa.Text;
            data.Ten = tbTen.Text;
            data.SanPhamGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            if (cbXuatXu.SelectedItem != null)
            {
                data.XuatXu = XuatXuBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbXuatXu.SelectedItem).Value));
            }
            else
            {
                data.XuatXu = null;
            }

            data.MoTa = tbMoTa.Text;
            data.DonViTinh = tbDonViTinh.Text;
            data.Hieu = tbHieu.Text;
            data.Size = tbSize.Text;
            data.ThoiGianBaoHanh = ConvertUtil.ConvertToByte(tbThoiGianBaoHanh.Text);
            data.DonViBaoHanh = cbDonViBaoHanh.Text;

            data.UpdateBy = "";
            data.UpdateDate = DateTime.Now;

            if (SanPhamBus.Update(data))
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



        #region Controls
        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            SanPhamGroup SPGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

            if (isUpdate)
            {
                string oldIdNumber = data == null ? string.Empty : data.IdSanPham.Substring(data.IdSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = data == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            }
            else
            {
                string idSanPham = string.Empty;
                DTO.SanPham dataTemp = SanPhamBus.GetLastData(SPGroup.Id);

                string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.IdSanPham.Substring(dataTemp.IdSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            }

            tbMa.Text = SPGroup.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

            ValidateInput();
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbDonViTinh_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();

            tbDonViTinh.Text = tbDonViTinh.Text.ToUpper();
            tbDonViTinh.Select(tbDonViTinh.Text.Length, 0);
        }

        private void tbThoiGianBaoHanh_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
        #endregion
    }
}
