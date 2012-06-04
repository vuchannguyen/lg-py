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

namespace QuanLyKinhDoanh.XuatXu
{
    public partial class UcInfo : UserControl
    {
        private DTO.XuatXu data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.XuatXu();
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

        public UcInfo(DTO.XuatXu data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            if (Init())
            {
                tbTen.Text = data.Ten;
                tbDiaChi.Text = data.DiaChi;
                tbDienThoai.Text = data.DienThoai;
                tbDTDD.Text = data.DTDD;
                tbFax.Text = data.Fax;
                tbEmail.Text = data.Email;
                tbGhiChu.Text = data.GhiChu;
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
            tbDiaChi.Text = string.Empty;
            tbDienThoai.Text = string.Empty;
            tbDTDD.Text = string.Empty;
            tbFax.Text = string.Empty;
            tbEmail.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbTen.Text)
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

        private void InsertData()
        {
            data = new DTO.XuatXu();

            data.Ten = tbTen.Text;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Fax = tbFax.Text;
            data.Email = tbEmail.Text;
            data.GhiChu = tbGhiChu.Text;

            if (XuatXuBus.Insert(data, FormMain.user))
            {
                this.Dispose();
            }
            else
            {
                if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR +
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.Ten = tbTen.Text;
            data.DiaChi = tbDiaChi.Text;
            data.DienThoai = tbDienThoai.Text;
            data.DTDD = tbDTDD.Text;
            data.Fax = tbFax.Text;
            data.Email = tbEmail.Text;
            data.GhiChu = tbGhiChu.Text;

            if (XuatXuBus.Update(data, FormMain.user))
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
            if (!isUpdate)
            {
                InsertData();
            }
            else
            {
                UpdateData();
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

        private void tbDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbDTDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbFax_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }
        #endregion
    }
}
