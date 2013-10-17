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
        private List<DTO.DinhLuong> listDataDL;
        private bool isUpdate;

        private List<UcNguyenLieuDinhLuong> listUcNguyenLieu;
        private List<UcNguyenLieuDinhLuong> listUcNguyenLieuInsert;
        private List<int> listUcNguyenLieuDelete;
        private List<UcNguyenLieuDinhLuong> listUcNguyenLieuUpdate;

        private const int ucNguyenLieuWidth = 170;

        public UcInfo()
        {
            InitializeComponent();

            data = new DTO.SanPham();
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

                pbAdd.Image = Image.FromFile(@"Resources\ChucNang\add.png");
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

            listUcNguyenLieu = new List<UcNguyenLieuDinhLuong>();
            listUcNguyenLieuInsert = new List<UcNguyenLieuDinhLuong>();
            listUcNguyenLieuDelete = new List<int>();
            listUcNguyenLieuUpdate = new List<UcNguyenLieuDinhLuong>();

            GetListDinhLuong(data.Id);

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

        private void GetListDinhLuong(int id)
        {
            List<DTO.DinhLuong> listData = DinhLuongBus.GetListByIdSP(id);

            foreach (DTO.DinhLuong dataTemp in listData)
            {
                UcNguyenLieuDinhLuong ucNguyenLieu = new UcNguyenLieuDinhLuong(dataTemp, true);

                int iNewLocation = listUcNguyenLieu.Count * ucNguyenLieuWidth + pn_gbNguyenLieu.AutoScrollPosition.Y;
                ucNguyenLieu.Location = CommonFunc.SetWidthCenter(pn_gbNguyenLieu.Size, ucNguyenLieu.Size, iNewLocation);

                listUcNguyenLieu.Add(ucNguyenLieu);
                pn_gbNguyenLieu.Controls.Add(listUcNguyenLieu[listUcNguyenLieu.Count - 1]);
                listUcNguyenLieu[listUcNguyenLieu.Count - 1].VisibleChanged += new EventHandler(AfterDeleteUc);

                listUcNguyenLieuUpdate.Add(ucNguyenLieu);
            }
        }

        private void RefreshData()
        {
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

        private bool InsertDinhLuong(List<UcNguyenLieuDinhLuong> listUcNL)
        {
            for (int i = 0; i < listUcNL.Count; i++)
            {
                DTO.DinhLuong dataDL = new DTO.DinhLuong();

                dataDL.IdSanPham = data.Id;
                dataDL.IdNguyenLieu = listUcNL[i].DataNL.Id;
                dataDL.SoLuong = listUcNL[i].SoLuong;
                dataDL.GhiChu = listUcNL[i].GhiChu;

                if (!DinhLuongBus.Insert(dataDL, FormMain.user))
                {
                    if (MessageBox.Show(Constant.MESSAGE_INSERT_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Dispose();
                    }

                    return false;
                }
            }

            return true;
        }

        private void UpdateDinhLuong()
        {
            if (!InsertDinhLuong(listUcNguyenLieuInsert))
            {
                return;
            }

            for (int i = 0; i < listUcNguyenLieuDelete.Count; i++)
            {
                if (!DinhLuongBus.Delete(DinhLuongBus.GetById(listUcNguyenLieuDelete[i]), FormMain.user))
                {
                    if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Dispose();
                    }
                }
            }

            for (int i = 0; i < listUcNguyenLieuUpdate.Count; i++)
            {
                DTO.DinhLuong dataDL = DinhLuongBus.GetById(listUcNguyenLieuUpdate[i].IdDL);

                dataDL.NguyenLieu = listUcNguyenLieuUpdate[i].DataNL;
                dataDL.SoLuong = listUcNguyenLieuUpdate[i].SoLuong;
                dataDL.GhiChu = listUcNguyenLieuUpdate[i].GhiChu;

                if (DinhLuongBus.Update(dataDL, FormMain.user))
                {
                    //this.Dispose();
                }
                else
                {
                    if (MessageBox.Show(Constant.MESSAGE_ERROR + Constant.MESSAGE_NEW_LINE + Constant.MESSAGE_EXIT, Constant.CAPTION_ERROR, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.Dispose();
                    }
                }
            }

            this.Dispose();
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

            if (MessageBox.Show(Constant.MESSAGE_CONFIRM, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!isUpdate)
                {
                    //Insert();
                }
                else
                {
                    UpdateDinhLuong();
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
            
        }

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



        private void pbAdd_Click(object sender, EventArgs e)
        {
            //pnThem_HL.Top = pnThem_HL.Location.Y + 130;
            int iNewLocation = listUcNguyenLieu.Count * ucNguyenLieuWidth + pn_gbNguyenLieu.AutoScrollPosition.Y;

            UcNguyenLieuDinhLuong ucNguyenLieu = new UcNguyenLieuDinhLuong();
            ucNguyenLieu.Location = CommonFunc.SetWidthCenter(pn_gbNguyenLieu.Size, ucNguyenLieu.Size, iNewLocation);
            ucNguyenLieu.VisibleChanged += new EventHandler(AfterDeleteUc);

            listUcNguyenLieu.Add(ucNguyenLieu); //dung de hien thi
            pn_gbNguyenLieu.Controls.Add(listUcNguyenLieu[listUcNguyenLieu.Count - 1]);

            listUcNguyenLieuInsert.Add(ucNguyenLieu); //dung de insert
        }

        private void AfterDeleteUc(object sender, EventArgs e)
        {
            UcNguyenLieuDinhLuong uc_Temp = (UcNguyenLieuDinhLuong)sender;

            if (!uc_Temp.Visible)
            {
                if (lbSelect.Text == "SỬA")
                {
                    if (listUcNguyenLieuUpdate.Remove(uc_Temp))
                    {
                        listUcNguyenLieuDelete.Add(uc_Temp.IdDL);
                    }
                }

                listUcNguyenLieu.Remove(uc_Temp);
                listUcNguyenLieuInsert.Remove(uc_Temp);

                for (int i = 0; i < listUcNguyenLieu.Count; i++)
                {
                    int iNewLocation = i * ucNguyenLieuWidth + pn_gbNguyenLieu.AutoScrollPosition.Y;
                    //listUcNguyenLieu[i].Location = new Point(8, iNewLocation);
                    listUcNguyenLieu[i].Location = CommonFunc.SetWidthCenter(pn_gbNguyenLieu.Size, listUcNguyenLieu[i].Size, iNewLocation);
                }
            }
        }
    }
}
