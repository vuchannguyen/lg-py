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

namespace Weedon.SanPham
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
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (Init())
            {
                tbMa.Text = data.MaSanPham;
                tbTen.Text = data.Ten;

                if (data.IsActive)
                {
                    rbBan.Checked = true;
                }
                else
                {
                    rbTamNgung.Checked = true;
                }

                tbMoTa.Text = data.MoTa;

                cbGroup.Text = data.SanPhamGroup.Ten;
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

            FormMain.isEditing = true;

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            if (!GetListGroupSP())
            {
                return false;
            }

            return true;
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            tbTen.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            rbBan.Checked = true;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMa.Text) &&
                !string.IsNullOrEmpty(cbGroup.Text) &&
                !string.IsNullOrEmpty(tbTen.Text)
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

        //private void CreateNewIdSP()
        //{
        //    int id = 0;
        //    DTO.SanPhamGroup SPGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));

        //    if (isUpdate)
        //    {
        //        string oldIdNumber = data == null ? string.Empty : data.MaSanPham.Substring(data.MaSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
        //        id = data == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber);
        //    }
        //    else
        //    {
        //        string idSanPham = string.Empty;
        //        DTO.SanPham dataTemp = SanPhamBus.GetLastData(SPGroup.Id);

        //        string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaSanPham.Substring(dataTemp.MaSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
        //        id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
        //    }

        //    tbMa.Text = SPGroup.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

        //    ValidateInput();
        //}

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

        private bool IsDuplicated()
        {
            List<DTO.SanPham> listData = SanPhamBus.GetList(string.Empty, 0, string.Empty, string.Empty, 0, 0);

            return listData.Exists(p => p.MaSanPham != data.MaSanPham && p.MaSanPham == tbMa.Text);
        }

        private void InsertData()
        {
            data = new DTO.SanPham();

            data.MaSanPham = tbMa.Text;
            data.Ten = tbTen.Text;
            data.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            data.MoTa = tbMoTa.Text;
            data.IsActive = rbBan.Checked;

            if (SanPhamBus.Insert(data, FormMain.user))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Sản phẩm " + data.MaSanPham) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Dispose();
                }
                else
                {
                    RefreshData();
                    //CreateNewIdSP();
                }
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
            if (IsDuplicated())
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_DUPLICATED, Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                data.MaSanPham = tbMa.Text;
                data.Ten = tbTen.Text;
                data.SanPhamGroup = SanPhamGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));
                data.MoTa = tbMoTa.Text;
                data.IsActive = rbBan.Checked;

                if (SanPhamBus.Update(data, FormMain.user))
                {
                    this.Dispose();
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
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
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        private void tbMa_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbThoiHan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
        #endregion
    }
}
