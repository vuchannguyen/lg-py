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
    public partial class UcDetail : UserControl
    {
        private DTO.SanPham data;
        private string idsDinhLuongRemoved;

        public UcDetail(DTO.SanPham data)
        {
            InitializeComponent();

            this.data = data;

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
                pbHoanTat.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_OK);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcDetail_Load(object sender, EventArgs e)
        {
            LoadResource();

            pnInfo.Location = CommonFunc.SetCenterLocation(this.Size, pnInfo.Size);
            pnTitle.Location = CommonFunc.SetWidthCenter(this.Size, pnTitle.Size, pnTitle.Top);

            LoadData(data.Id);

            this.BringToFront();

            FormMain.isEditing = true;
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
            }
        }
        #endregion



        #region Ok Cancel
        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
    }
}
