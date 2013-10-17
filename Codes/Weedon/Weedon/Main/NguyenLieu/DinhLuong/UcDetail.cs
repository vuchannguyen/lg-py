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
        private List<DTO.DinhLuong> listDataDL;

        private List<UcNguyenLieuDinhLuong> listUcNguyenLieu;
        private List<UcNguyenLieuDinhLuong> listUcNguyenLieuInsert;
        private List<int> listUcNguyenLieuDelete;
        private List<UcNguyenLieuDinhLuong> listUcNguyenLieuUpdate;

        private const int ucNguyenLieuWidth = 170;

        public UcDetail()
        {
            InitializeComponent();
        }

        public UcDetail(DTO.SanPham data)
        {
            InitializeComponent();

            this.data = data;
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

            listUcNguyenLieu = new List<UcNguyenLieuDinhLuong>();
            listUcNguyenLieuInsert = new List<UcNguyenLieuDinhLuong>();
            listUcNguyenLieuDelete = new List<int>();
            listUcNguyenLieuUpdate = new List<UcNguyenLieuDinhLuong>();

            GetListDinhLuong(data.Id);

            this.BringToFront();
        }

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
                UcNguyenLieuDinhLuong ucNguyenLieu = new UcNguyenLieuDinhLuong(dataTemp, false);

                int iNewLocation = listUcNguyenLieu.Count * ucNguyenLieuWidth + pn_gbNguyenLieu.AutoScrollPosition.Y;
                ucNguyenLieu.Location = CommonFunc.SetWidthCenter(pn_gbNguyenLieu.Size, ucNguyenLieu.Size, iNewLocation);

                listUcNguyenLieu.Add(ucNguyenLieu);
                pn_gbNguyenLieu.Controls.Add(listUcNguyenLieu[listUcNguyenLieu.Count - 1]);

                listUcNguyenLieuUpdate.Add(ucNguyenLieu);
            }
        }
        
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
    }
}
