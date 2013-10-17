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
    public partial class UcNguyenLieuDinhLuong : UserControl
    {
        private int idDL;

        public int IdDL
        {
            get { return idDL; }
            set { idDL = value; }
        }

        private DTO.NguyenLieu dataNL;

        public DTO.NguyenLieu DataNL
        {
            get { return dataNL; }
            set { dataNL = value; }
        }

        private double soluong;

        public double SoLuong
        {
            get { return soluong; }
            set { soluong = value; }
        }

        private string ghichu;

        public string GhiChu
        {
            get { return ghichu; }
            set { ghichu = value; }
        }

        public UcNguyenLieuDinhLuong()
        {
            InitializeComponent();

            dataNL = new DTO.NguyenLieu();

            InitNL();
        }

        public UcNguyenLieuDinhLuong(DTO.DinhLuong data, bool isEditable)
        {
            InitializeComponent();

            InitNL();

            idDL = data.Id;
            data.NguyenLieu = data.NguyenLieu;
            soluong = data.SoLuong;
            ghichu = data.GhiChu;

            cbTen.Text = data.NguyenLieu.Ten;
            tbSoLuong.Text = data.SoLuong.ToString();
            tbDonVi.Text = data.NguyenLieu.DonViTinh;
            tbGhiChu.Text = data.GhiChu;

            if (!isEditable)
            {
                cbTen.Enabled = false;
                tbSoLuong.Enabled = false;
                tbDonVi.Enabled = false;
                tbGhiChu.Enabled = false;

                pbDelete.Visible = false;
            }
        }

        private void LoadResource()
        {
            try
            {
                pbDelete.Image = Image.FromFile(ConstantResource.CHUC_NANG_ICON_DELETE_COMPONENT);
            }
            catch
            {
                MessageBox.Show(Constant.MESSAGE_ERROR_MISSING_RESOURCE, Constant.CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Dispose();
            }
        }

        private void UcNguyenLieuDinhLuong_Load(object sender, EventArgs e)
        {
            LoadResource();
        }



        #region Function
        private bool InitNL()
        {
            GetListNL();

            return true;
        }

        private void RefreshdataNL()
        {
            tbMa.Text = string.Empty;
            cbTen.Text = string.Empty;
            tbGhiChu.Text = string.Empty;

            cbTen.SelectedIndex = -1;
        }

        private bool GetListNL()
        {
            List<DTO.NguyenLieu> listData = NguyenLieuBus.GetList(string.Empty, null, string.Empty, string.Empty, 0, 0);

            if (listData.Count == 0)
            {
                //
            }

            cbTen.Items.Clear();

            foreach (DTO.NguyenLieu data in listData)
            {
                cbTen.Items.Add(new CommonComboBoxItems(data.Ten, data.Id));
            }

            return true;
        }
        #endregion



        private void pbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Constant.MESSAGE_CONFIRM_DELETE_COMPONENT, Constant.CAPTION_CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Visible = false;
            }
        }

        private void cbTen_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataNL = NguyenLieuBus.GetById(ConvertUtil.ConvertToInt(((CommonComboBoxItems)cbTen.SelectedItem).Value));
            tbMa.Text = dataNL == null ? string.Empty : dataNL.MaNguyenLieu;
            tbDonVi.Text = dataNL.DonViTinh;
        }

        private void tbGhiChu_TextChanged(object sender, EventArgs e)
        {
            ghichu = tbGhiChu.Text;
        }

        private void tbSoLuong_Leave(object sender, EventArgs e)
        {
            soluong = ConvertUtil.ConvertToDouble(tbSoLuong.Text);
            tbSoLuong.Text = soluong.ToString();
        }
    }
}
