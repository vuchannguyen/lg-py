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

namespace QuanLyKinhDoanh.KhachHangGroup
{
    public partial class UcInfo : UserControl
    {
        private DTO.KhachHangGroup data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.KhachHangGroup();
            isUpdate = false;

            Init();
            RefreshData();
        }

        public UcInfo(DTO.KhachHangGroup data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            Init();

            tbMa.Text = data.Ma;
            tbTen.Text = data.Ten;
            tbMoTa.Text = data.MoTa;
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

            FormMain.isEditing = false;

            ValidateInput();
        }



        #region Function
        private void Init()
        {
            //
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            tbTen.Text = string.Empty;
            tbMoTa.Text = string.Empty;
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbMa.Text) &&
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
            data = new DTO.KhachHangGroup();

            data.Ma = tbMa.Text;
            data.Ten = tbTen.Text;
            data.MoTa = tbMoTa.Text;

            if (KhachHangGroupBus.Insert(data, FormMain.user))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Hóa đơn " + data.Ma) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE, Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    this.Dispose();
                }
            }
            else
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_ERROR_DUPLICATE, tbMa.Text) + 
                    Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Dispose();
                }
            }
        }

        private void UpdateData()
        {
            data.Ma = tbMa.Text;
            data.Ten = tbTen.Text;
            data.MoTa = tbMoTa.Text;

            if (KhachHangGroupBus.Update(data, FormMain.user))
            {
                List<DTO.SanPham> listSP = SanPhamBus.GetListByIdGroup(data.Id);

                foreach (DTO.SanPham dataSP in listSP)
                {
                    int id = 0;

                    string oldIdNumber = dataSP.MaSanPham.Substring(dataSP.MaSanPham.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
                    id = ConvertUtil.ConvertToInt(oldIdNumber);

                    dataSP.MaSanPham = data.Ma + id.ToString(Constant.DEFAULT_FORMAT_ID_PRODUCT);

                    SanPhamBus.Update(dataSP, FormMain.user);
                }

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
        private void tbMa_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateSpace(e);
        }

        private void tbMa_TextChanged(object sender, EventArgs e)
        {
            tbMa.Text = CommonFunc.ConvertVietNamToEnglish(tbMa.Text);

            ValidateInput();
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
        #endregion
    }
}
