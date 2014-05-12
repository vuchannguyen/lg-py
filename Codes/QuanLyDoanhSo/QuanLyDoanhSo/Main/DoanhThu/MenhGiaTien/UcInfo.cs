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

namespace Weedon.MenhGiaTien
{
    public partial class UcInfo : UserControl
    {
        private DTO.MenhGiaTien data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.MenhGiaTien();
            isUpdate = false;

            Init();
            RefreshData();
        }

        public UcInfo(DTO.MenhGiaTien data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            Init();

            tbTen.Text = data.Ten;
            tbGiaTri.Text = data.Gia.ToString();
            tbGhiChu.Text = data.GhiChu;

            if (data.IsActived)
            {
                rbSuDung.Checked = true;
            }
            else
            {
                rbTamNgung.Checked = true;
            }

            if (data.Id == 2)
            {
                tbGiaTri.Enabled = false;
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
        private void Init()
        {
            //
        }

        private void RefreshData()
        {
            tbGiaTri.Text = string.Empty;
            tbTen.Text = string.Empty;
            tbGhiChu.Text = string.Empty;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbGiaTri.Text) &&
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

        private void InsertData()
        {
            data = new DTO.MenhGiaTien();

            data.Ten = tbTen.Text;
            data.Gia = ConvertUtil.ConvertToInt(tbGiaTri.Text);
            data.GhiChu = tbGhiChu.Text;
            data.IsActived = rbSuDung.Checked;

            if (MenhGiaTienBus.Insert(data))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Cấu hình " + data.Ten) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_ERROR_DUPLICATED, tbGiaTri.Text) + 
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.Ten = tbTen.Text;
            data.Gia = ConvertUtil.ConvertToInt(tbGiaTri.Text);
            data.GhiChu = tbGhiChu.Text;
            data.IsActived = rbSuDung.Checked;

            if (MenhGiaTienBus.Update(data))
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
        private void tbMa_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbMa_TextChanged(object sender, EventArgs e)
        {
            tbGiaTri.Text = CommonFunc.ConvertVietNamToEnglish(tbGiaTri.Text);

            ValidateInput();
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
        #endregion
    }
}
