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
using CryptoFunction;

namespace Weedon.User
{
    public partial class UcInfo : UserControl
    {
        private DTO.User data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.User();
            isUpdate = false;

            if (Init())
            {
                RefreshData();

                lbOldPassword.Visible = false;
                tbOldPassword.Visible = false;
            }
            else
            {
                this.Visible = false;
            }
        }

        public UcInfo(DTO.User data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (Init())
            {
                tbTen.Text = data.Ten;
                tbUserName.Text = data.UserName;
                //tbPassword.Text = Constant.DEFAULT_PASSWORD;
                tbDiaChi.Text = data.DiaChi;
                tbDienThoai.Text = data.DienThoai;
                tbDTDD.Text = data.DTDD;
                tbEmail.Text = data.Email;
                tbCMND.Text = data.CMND;
                tbNoiCap.Text = data.NoiCap;
                tbGhiChu.Text = data.GhiChu;

                dtpDOB.Value = data.DOB.HasValue ? data.DOB.Value : DateTime.Now;
                dtpNgayCap.Value = data.NgayCap.HasValue ? data.NgayCap.Value : DateTime.Now;

                cbGioiTinh.Text = data.GioiTinh;
                cbGroup.Text = data.UserGroup.Ten;

                cbGroup.Enabled = false;
                tbUserName.ReadOnly = true;

                if (data.Id != FormMain.user.Id)
                {
                    lbOldPassword.Visible = false;
                    tbOldPassword.Visible = false;

                    lbPassword.Visible = false;
                    tbPassword.Visible = false;

                    lbConfirmPassword.Visible = false;
                    tbConfirmPassword.Visible = false;
                }
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

            dtpDOB.CustomFormat = Constant.DEFAULT_DATE_FORMAT;
            dtpNgayCap.CustomFormat = Constant.DEFAULT_DATE_FORMAT;

            ValidateInput();
        }



        #region Function
        private bool Init()
        {
            List<DTO.UserGroup> listData = UserGroupBus.GetList(string.Empty, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                MessageBox.Show(string.Format(Constant.MESSAGE_ERROR_MISSING_DATA, "Nhóm User"), Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            cbGroup.Items.Clear();

            foreach (DTO.UserGroup data in listData)
            {
                cbGroup.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }

        private void RefreshData()
        {
            tbTen.Text = string.Empty;
            tbUserName.Text = string.Empty;
            tbPassword.Text = string.Empty;
            tbDiaChi.Text = string.Empty;
            tbDienThoai.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
            tbCMND.Text = string.Empty;
            tbNoiCap.Text = string.Empty;

            dtpDOB.Value = Constant.DEFAULT_DATE;
            dtpNgayCap.Value = Constant.DEFAULT_DATE;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
            cbGioiTinh.SelectedIndex = 0;
        }

        private void ValidateInput()
        {
            if (!isUpdate)
            {
                if (!string.IsNullOrEmpty(tbTen.Text) &&
                !string.IsNullOrEmpty(tbUserName.Text) &&
                !string.IsNullOrEmpty(tbPassword.Text) &&
                !string.IsNullOrEmpty(tbConfirmPassword.Text)
                )
                {
                    pbHoanTat.Enabled = true;
                    pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                    return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbTen.Text) &&
                !string.IsNullOrEmpty(tbUserName.Text)
                )
                {
                    if (FormMain.user.IdGroup == Constant.ID_GROUP_ADMIN && data.IdGroup != Constant.ID_GROUP_ADMIN)
                    {
                        pbHoanTat.Enabled = true;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                        return;
                    }

                    if (FormMain.user.Id == data.Id && !string.IsNullOrEmpty(tbOldPassword.Text))
                    {
                        pbHoanTat.Enabled = true;
                        pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);

                        return;
                    }
                }
            }

            pbHoanTat.Enabled = false;
            pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK_DISABLE);
        }

        private void InsertData()
        {
            data.Ten = tbTen.Text;
            data.IdGroup = ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value);
            data.UserName = tbUserName.Text;
            data.Password = Crypto.EncryptText(tbPassword.Text);
            data.GioiTinh = cbGioiTinh.Text;
            data.DOB = dtpDOB.Value;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Email = tbEmail.Text;
            data.CMND = tbCMND.Text;
            data.NoiCap = tbNoiCap.Text;
            data.NgayCap = dtpNgayCap.Value;
            data.GhiChu = tbGhiChu.Text;

            if (UserBus.Insert(data, FormMain.user))
            {
                FormMain.isEditing = false;

                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_ERROR_DUPLICATED, tbUserName.Text) +
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    FormMain.isEditing = false;

                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.Ten = tbTen.Text;
            data.UserGroup = UserGroupBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbGroup.SelectedItem).Value));
            //data.UserName = tbUserName.Text;

            if (!string.IsNullOrEmpty(tbPassword.Text))
            {
                data.Password = Crypto.EncryptText(tbPassword.Text);
            }

            data.GioiTinh = cbGioiTinh.Text;
            data.DOB = dtpDOB.Value;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Email = tbEmail.Text;
            data.CMND = tbCMND.Text;
            data.NoiCap = tbNoiCap.Text;
            data.NgayCap = dtpNgayCap.Value;
            data.GhiChu = tbGhiChu.Text;

            if (UserBus.Update(data, FormMain.user))
            {
                FormMain.isEditing = false;

                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    FormMain.isEditing = false;

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
                FormMain.isEditing = false;

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
                    if (tbPassword.Text != tbConfirmPassword.Text)
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_CONFIRM_PASS, Constant.CAPTION_ERROR,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    InsertData();
                }
                else
                {
                    if (!string.IsNullOrEmpty(tbPassword.Text) && tbPassword.Text != tbConfirmPassword.Text)
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_CONFIRM_PASS, Constant.CAPTION_ERROR,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    if (FormMain.user.IdGroup == Constant.ID_GROUP_ADMIN && data.IdGroup != Constant.ID_GROUP_ADMIN)
                    {
                        UpdateData();
                    }
                    else
                    {
                        if (Crypto.EncryptText(tbOldPassword.Text) != FormMain.user.Password)
                        {
                            MessageBox.Show(Constant.MESSAGE_ERROR_VERIFY_OLD_PASS, Constant.CAPTION_ERROR,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        UpdateData();
                    }
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

        private void tbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
            tbUserName.Text = CommonFunc.ConvertVietNamToEnglish(tbUserName.Text);
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbCMND_Enter(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        private void tbCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }
        #endregion

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void tbOldPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
    }
}
