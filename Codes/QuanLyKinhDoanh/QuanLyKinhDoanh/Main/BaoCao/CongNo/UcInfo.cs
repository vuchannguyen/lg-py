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

namespace QuanLyKinhDoanh.CongNo
{
    public partial class UcInfo : UserControl
    {
        private DTO.HoaDon data;
        private bool isUpdate;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.HoaDon();
            isUpdate = false;

            Init();
            RefreshData();
        }

        public UcInfo(DTO.HoaDon data)
        {
            InitializeComponent();

            this.data = data;
            isUpdate = true;
            lbSelect.Text = Constant.DEFAULT_TITLE_EDIT;

            Init();

            tbMa.Text = data.MaHoaDon;
            tbThanhToan.Text = data.ThanhTien.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbGhiChu.Text = data.GhiChu;
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

            ValidateInput();

            tbThanhToan.Focus();
        }



        #region Function
        private void Init()
        {
            //
        }

        private void RefreshData()
        {
            tbMa.Text = string.Empty;
            tbThanhToan.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            CreateNewId();
        }

        private void ValidateInput()
        {
            if (!string.IsNullOrEmpty(tbThanhToan.Text))
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

            DTO.HoaDon dataTemp = HoaDonBus.GetLastData(Constant.ID_TYPE_CHI);

            string oldIdNumber = dataTemp == null ? string.Empty : dataTemp.MaHoaDon.Substring(dataTemp.MaHoaDon.Length - Constant.DEFAULT_FORMAT_ID_PRODUCT.Length);
            id = dataTemp == null ? 1 : ConvertUtil.ConvertToInt(oldIdNumber) + 1;

            tbMa.Text = Constant.PREFIX_CHI + id.ToString(Constant.DEFAULT_FORMAT_ID_BILL);
        }

        private void InsertData()
        {
            data.MaHoaDon = tbMa.Text;
            data.IdType = Constant.ID_TYPE_CHI;
            data.IdStatus = Constant.ID_STATUS_DONE;
            data.ThanhTien = ConvertUtil.ConvertToLong(tbThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            data.CreateBy = data.UpdateBy = "";
            data.CreateDate = data.UpdateDate = DateTime.Now;

            if (HoaDonBus.Insert(data))
            {
                if (MessageBox.Show(string.Format(Constant.MESSAGE_INSERT_SUCCESS, "Hóa đơn " + data.MaHoaDon) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_CONTINUE,
                    Constant.CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
            data.ThanhTien = ConvertUtil.ConvertToLong(tbThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, string.Empty));
            data.GhiChu = tbGhiChu.Text;

            data.UpdateBy = "";
            data.UpdateDate = DateTime.Now;

            if (HoaDonBus.Update(data))
            {
                this.Dispose();
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
        private void tbTien_TextChanged(object sender, EventArgs e)
        {
            long money = ConvertUtil.ConvertToLong(tbThanhToan.Text.Replace(Constant.SYMBOL_LINK_MONEY, ""));

            tbThanhToan.Text = money.ToString(Constant.DEFAULT_FORMAT_MONEY);
            tbThanhToan.Select(tbThanhToan.Text.Length, 0);

            ValidateInput();
        }

        private void tbTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);

            ValidateInput();
        }

        private void tbGhiChu_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }
        #endregion

        private void tbConLai_KeyPress(object sender, KeyPressEventArgs e)
        {
            CommonFunc.ValidateNumeric(e);
        }
    }
}
