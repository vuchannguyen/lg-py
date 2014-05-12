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

namespace Weedon.DinhLuong
{
    public partial class UcInfo : UserControl
    {
        private DTO.SanPham data;
        private bool isUpdate;
        private string idsDinhLuongRemoved;

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
                cbGroup.Text = data.SanPhamGroup.Ten;
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
                pbAdd.Image = Image.FromFile(ConstantResource.CHUC_NANG_ADD);
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

            if (isUpdate)
            {
                LoadData(data.Id);
            }

            if (dgvThongTin.RowCount == 0)
            {
                AddNewRow();
            }

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

        private void RefreshData()
        {
            idsDinhLuongRemoved = string.Empty;
            tbMa.Text = string.Empty;
            tbTen.Text = string.Empty;
            tbMoTa.Text = string.Empty;

            cbGroup.SelectedIndex = cbGroup.Items.Count > 0 ? 0 : -1;
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

        private void AddNewRow()
        {
            dgvThongTin.Rows.Add();
            List<DTO.NguyenLieu> listData = NguyenLieuBus.GetList(string.Empty, null, string.Empty, string.Empty, 0, 0);
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colTen.Name];

            cell.DataSource = listData;
            cell.Value = listData.FirstOrDefault().Id;
            cell.ValueMember = "Id";
            cell.DisplayMember = "Ten";

            dgvThongTin.CurrentCell = cell;
            UpdateRowData();
            dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colUocLuong.Name].Value = 1;
        }

        private void UpdateRowData()
        {
            int rowIndex = dgvThongTin.CurrentCell.RowIndex;

            if (dgvThongTin.CurrentCell.ColumnIndex == dgvThongTin.Columns[colTen.Name].Index)
            {
                DTO.NguyenLieu data = NguyenLieuBus.GetById(ConvertUtil.ConvertToInt(dgvThongTin[colTen.Name, rowIndex].Value));
                dgvThongTin[colIdNL.Name, rowIndex].Value = data.Id;
                dgvThongTin[colMa.Name, rowIndex].Value = data.MaNguyenLieu;
                dgvThongTin[colDonVi.Name, rowIndex].Value = data.DonViTinh;
                dgvThongTin.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadData(int idSP)
        {
            List<DTO.DinhLuong> listData = DinhLuongBus.GetListByIdSP(idSP);
            List<DTO.NguyenLieu> listNL = NguyenLieuBus.GetList(string.Empty, null, string.Empty, string.Empty, 0, 0);

            foreach (DTO.DinhLuong data in listData)
            {
                dgvThongTin.Rows.Add();
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colTen.Name];

                cell.DataSource = listNL;
                cell.Value = data.IdNguyenLieu;
                cell.ValueMember = "Id";
                cell.DisplayMember = "Ten";

                dgvThongTin.CurrentCell = cell;
                UpdateRowData();
                dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colId.Name].Value = data.Id;
                dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colUocLuong.Name].Value = data.SoLuong;
                dgvThongTin.Rows[dgvThongTin.RowCount - 1].Cells[colGhiChu.Name].Value = data.GhiChu;
            }
        }

        private void UpdateDinhLuong()
        {
            if (!DeleteDataHoaDonDetail())
            {
                return;
            }

            for (int i = 0; i < dgvThongTin.RowCount; i++)
            {
                if (dgvThongTin[colId.Name, i].Value != null && !string.IsNullOrEmpty(dgvThongTin[colId.Name, i].Value.ToString()))
                {
                    DTO.DinhLuong dataDL = DinhLuongBus.GetById(ConvertUtil.ConvertToInt(dgvThongTin[colId.Name, i].Value));

                    dataDL.IdNguyenLieu = ConvertUtil.ConvertToInt(dgvThongTin[colIdNL.Name, i].Value);
                    dataDL.SoLuong = ConvertUtil.ConvertToDouble(dgvThongTin[colUocLuong.Name, i].Value);
                    dataDL.GhiChu = dgvThongTin[colGhiChu.Name, i].Value == null ? string.Empty : dgvThongTin[colGhiChu.Name, i].Value.ToString();

                    if (!DinhLuongBus.Update(dataDL, FormMain.user))
                    {
                        if (MessageBox.Show(Constant.MESSAGE_UPDATE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            this.Dispose();
                        }

                        return;
                    }
                }
                else
                {
                    DTO.DinhLuong dataDL = new DTO.DinhLuong();

                    dataDL.IdSanPham = data.Id;
                    dataDL.IdNguyenLieu = ConvertUtil.ConvertToInt(dgvThongTin[colIdNL.Name, i].Value);
                    dataDL.SoLuong = ConvertUtil.ConvertToDouble(dgvThongTin[colUocLuong.Name, i].Value);
                    dataDL.GhiChu = dgvThongTin[colGhiChu.Name, i].Value == null ? string.Empty : dgvThongTin[colGhiChu.Name, i].Value.ToString();

                    if (!DinhLuongBus.Insert(dataDL, FormMain.user))
                    {
                        if (MessageBox.Show(Constant.MESSAGE_UPDATE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            this.Dispose();
                        }

                        return;
                    }
                }
            }

            if (MessageBox.Show(string.Format(Constant.MESSAGE_UPDATE_SUCCESS, "Sản phẩm " + data.MaSanPham) + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private bool DeleteDataHoaDonDetail()
        {
            if (!string.IsNullOrEmpty(idsDinhLuongRemoved))
            {
                return DinhLuongBus.DeleteList(idsDinhLuongRemoved, FormMain.user);
            }

            return true;
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
            pbHoanTat.Focus();

            for (int i = 0; i < dgvThongTin.RowCount; i++)
            {
                for (int j = 0; j < dgvThongTin.RowCount; j++)
                {
                    if (i != j && Equals(dgvThongTin[colIdNL.Name, i].Value, dgvThongTin[colIdNL.Name, j].Value))
                    {
                        MessageBox.Show(Constant.MESSAGE_ERROR_DUPLICATED + Constant.MESSAGE_NEW_LINE + "Dòng " + (i + 1).ToString() + " và " + (j + 1).ToString(),
                            Constant.CAPTION_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UpdateDinhLuong();
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

        private void pbAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void dgvThongTin_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            UpdateRowData();
        }

        private void dgvThongTin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvThongTin.Columns[colRemove.Name].Index)
            {
                if (dgvThongTin[colId.Name, e.RowIndex].Value != null && !string.IsNullOrEmpty(dgvThongTin[colId.Name, e.RowIndex].Value.ToString()))
                {
                    idsDinhLuongRemoved += dgvThongTin[colId.Name, e.RowIndex].Value + Constant.SEPERATE_STRING;
                }

                dgvThongTin.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void dgvThongTin_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ConvertUtil.ConvertToDouble(dgvThongTin[colUocLuong.Name, e.RowIndex].Value) <= 0)
            {
                dgvThongTin[colUocLuong.Name, e.RowIndex].Value = 1;
            }
        }
        #endregion
    }
}
