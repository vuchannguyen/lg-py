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

namespace Weedon.NguyenLieu
{
    public partial class UcInfo : UserControl
    {
        private DTO.NguyenLieu data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.NguyenLieu();
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

        public UcInfo(DTO.NguyenLieu data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (Init())
            {
                tbMaNL.Text = data.MaNguyenLieu;
                tbTen.Text = data.Ten;
                cbDVTNL.Text = data.DonViTinh;

                if (data.IsActive)
                {
                    rbSuDung.Checked = true;
                }
                else
                {
                    rbTamNgung.Checked = true;
                }

                tbMoTa.Text = data.MoTa;
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
            return true;
        }

        private void RefreshData()
        {
            tbTen.Text = string.Empty;
            cbDVTNL.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            rbSuDung.Checked = true;

            CreateNewIdNL();
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbTen.Text) && !string.IsNullOrEmpty(cbDVTNL.Text))
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

        private void CreateNewIdNL()
        {
            int id = 0;

            if (isUpdate)
            {
                string oldIdNumber = data == null ? string.Empty : data.MaNguyenLieu.Substring(data.MaNguyenLieu.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = data == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber);
            }
            else
            {
                string idNguyenLieu = string.Empty;
                DTO.NguyenLieu dataTemp = NguyenLieuBus.GetLastDataByMa();

                string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaNguyenLieu.Substring(dataTemp.MaNguyenLieu.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;
            }

            tbMaNL.Text = id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

            ValidateInput();
        }

        private void ConfirmContinue()
        {
            if (!isUpdate)
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Nguyên liệu " + data.MaNguyenLieu + ": " + data.Ten) +
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE,
                    Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Dispose();
                }
                else
                {
                    RefreshData();
                    CreateNewIdNL();
                }
            }
            else
            {
                this.Dispose();
            }
        }

        private void InsertData()
        {
            data = new DTO.NguyenLieu();

            data.MaNguyenLieu = tbMaNL.Text;
            data.Ten = tbTen.Text;
            data.DonViTinh = cbDVTNL.Text;
            data.IsActive = rbSuDung.Checked;
            data.MoTa = tbMoTa.Text;

            if (NguyenLieuBus.Insert(data, FormMain.user))
            {
                ConfirmContinue();
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
            data.MaNguyenLieu = tbMaNL.Text;
            data.Ten = tbTen.Text;
            data.DonViTinh = cbDVTNL.Text;
            data.IsActive = rbSuDung.Checked;
            data.MoTa = tbMoTa.Text;

            if (NguyenLieuBus.Update(data, FormMain.user))
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
        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void cbDVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbThoiHan_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
        #endregion

        

        private void cbDVTNL_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbMoTa.Focus();
        }

        private void cbDVTNL_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
    }
}
