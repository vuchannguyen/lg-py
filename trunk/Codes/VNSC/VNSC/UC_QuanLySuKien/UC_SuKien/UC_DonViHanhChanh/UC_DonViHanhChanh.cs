using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;
using BUS;

namespace VNSC
{
    public partial class UC_DonViHanhChanh : UserControl
    {
        private List<DonViHanhChanh> list_dto;

        private int iRows;
        private int iTotalPage;
        private int iRowsPerPage;

        private int iMaDonViHanhChanh;
        private int iMaSuKien;

        public UC_DonViHanhChanh()
        {
            InitializeComponent();
        }

        public UC_DonViHanhChanh(int iMa, string sTen)
        {
            InitializeComponent();

            iMaSuKien = iMa;

            lbTitleSuKien.Left = lbSelectSuKien.Left;
            lbSelectSuKien.Text = "SỰ KIỆN";
            lbTitleSuKien.Text = sTen;
        }

        private void LoadPic()
        {
            try
            {
                pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");

                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_donvihanhchanh_title.png");
                pbBackChiTiet.Image = Image.FromFile(@"Resources\SuKien\button_home.png");

                pbTraCuu.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back_disable.png");
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next_disable.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_DonViHanhChanh_Load(object sender, EventArgs e)
        {
            LoadPic();

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "";

            pnQuanLy.Size = new System.Drawing.Size(670, 390);
            pnQuanLy.Location = SubFunction.SetCenterLocation(this.Size, pnQuanLy.Size);

            pnInfo.Size = new System.Drawing.Size(580, 350);
            pnInfo.Location = SubFunction.SetCenterLocation(this.Size, pnInfo.Size);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            list_dto = new List<DonViHanhChanh>();

            iRows = 16;
            refreshListView();
        }

        private void NewInfo()
        {
            tbTen.Text = "";
            tbMoTa.Text = "";

            cbPhanCap.Enabled = true;
            LayDSCapQuanTri_ComboBox(cbPhanCap);
            cbPhanCap.SelectedIndex = -1;
        }

        private void setRowsPerPage()
        {
            int n = list_dto.Count - ((int.Parse(lbPage.Text) - 1) * iRows);
            if (n < iRows)
            {
                iRowsPerPage = list_dto.Count;
            }
            else
            {
                iRowsPerPage = int.Parse(lbPage.Text) * iRows;
            }
        }

        private void setTotalPage()
        {
            if (list_dto.Count % iRows == 0)
            {
                iTotalPage = list_dto.Count / iRows;
            }
            else
            {
                iTotalPage = (list_dto.Count / iRows) + 1;
            }
        }

        private void refreshListView()
        {
            tbSearch.Text = "";
            list_dto = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);
            //if (list_dto.Count > 0)
            //{
            //    sMa = list_dto[list_dto.Count - 1].Ma;
            //}

            setTotalPage();
            lbTotalPage.Text = iTotalPage.ToString() + " Trang";
            SubFunction.SetError(lbPage, lbPage.Top, pnPage.Size, "1");
            refreshListView(1);

            pbOk.Enabled = false;
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView(string sTen)
        {
            list_dto = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoTen(sTen);

            setTotalPage();
            lbTotalPage.Text = iTotalPage.ToString() + " Trang";
            SubFunction.SetError(lbPage, lbPage.Top, pnPage.Size, "1");
            refreshListView(1);
        }

        private void refreshListView(int Page)
        {
            SubFunction.ClearlvItem(lvThongTin);
            setRowsPerPage();
            for (int i = (int.Parse(lbPage.Text) - 1) * iRows; i < iRowsPerPage; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto[i].Ma.ToString();
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto[i].Ten);
                if (list_dto[i].PhanCap == -1)
                {
                    lvi.SubItems.Add("");
                }
                else
                {
                    lvi.SubItems.Add(list_dto[i].PhanCap.ToString());
                }
                lvi.SubItems.Add(list_dto[i].CapQuanTri);
                lvi.SubItems.Add(list_dto[i].MoTa);

                lvThongTin.Items.Add(lvi);
            }

            pbXoa.Enabled = false;
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
        }

        private bool LayDSDonViHanhChanh_ComboBox(ComboBox cb)
        {
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.LayDSDonViHanhChanh();
            if (list_Temp.Count > 0)
            {
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSDonViHanhChanhTheoCapNhoHon_ComboBox(ComboBox cb, int iCap)
        {
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoCapQuanTriNhoHon(iMaSuKien, iCap);
            if (list_Temp.Count > 0)
            {
                cb.Items.Clear();
                cb.Items.Add("");
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                cb.Items.Clear();
                cb.Items.Add("");
                return false;
            }
        }

        private bool LayDSDonViHanhChanhTheoCapLonHon_ComboBox(ComboBox cb, int iCap)
        {
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoCapQuanTriLonHon(iMaSuKien, iCap);
            if (list_Temp.Count > 0)
            {
                cb.Items.Clear();
                cb.Items.Add("");
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                cb.Items.Clear();
                cb.Items.Add("");
                return false;
            }
        }

        private bool LayDSCapQuanTri_ComboBox(ComboBox cb)
        {
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);

            if (list_Temp.Count > 0)
            {
                int iTemp = 1;
                cb.Items.Clear();
                cb.Items.Add("");
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    if (list_Temp[i].PhanCap > iTemp)
                    {
                        iTemp = (int)list_Temp[i].PhanCap;
                    }
                }

                for (int i = 1; i <= iTemp; i++)
                {
                    cb.Items.Add(i);
                }
                cb.Items.Add(iTemp + 1);

                return true;
            }
            else
            {
                cb.Items.Clear();
                cb.Items.Add("");
                cb.Items.Add(1);
                return false;
            }
        }



        #region Select
        private void pbThem_Click(object sender, EventArgs e)
        {
            tbTen_TextChanged(sender, e);

            pnQuanLy.Visible = false;
            pnSelect.Visible = false;
            pnInfo.Visible = true;
            pbBackChiTiet.Visible = false;
            //pbDonViHanhChanh.Visible = false;
            //lbDonViHanhChanh.Visible = false;

            lbTitle.Text = "THÊM ĐƠN VỊ HÀNH CHÁNH";
            lbSelect.Text = "THÊM";

            NewInfo();

            tbTen.Focus();
        }

        private void pbThem_MouseEnter(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them_selected.png");
        }

        private void pbThem_MouseLeave(object sender, EventArgs e)
        {
            pbThem.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
        }



        private void pbXoa_Click(object sender, EventArgs e)
        {
            Form_Confirm frm_Confirm = new Form_Confirm("Đồng ý xóa " + lvThongTin.SelectedItems.Count + " dữ liệu?");
            if (frm_Confirm.Yes)
            {
                for (int i = 0; i < lvThongTin.SelectedItems.Count; i++)
                {
                    if (SuKien_DonViHanhChanh_BUS.Delete(iMaSuKien, int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text)))
                    {
                        if (!DonViHanhChanh_BUS.Delete(int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text)))
                        {
                            SuKien_DonViHanhChanh dto_Temp = new SuKien_DonViHanhChanh();
                            dto_Temp.MaSuKien = iMaSuKien;
                            dto_Temp.MaDonViHanhChanh = int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text);
                            SuKien_DonViHanhChanh_BUS.Insert(dto_Temp);

                            Form_Notice frm = new Form_Notice("Không thể xóa!", "Vẫn còn TVSK có ĐVHC này!", false);
                            break;
                        }
                    }
                    else
                    {
                        Form_Notice frm = new Form_Notice("Không thể xóa!", "Vui lòng thử lại!", false);
                        break;
                    }
                }

                refreshListView();
            }
        }

        private void pbXoa_MouseEnter(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_selected.png");
        }

        private void pbXoa_MouseLeave(object sender, EventArgs e)
        {
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
        }



        private void pbSua_Click(object sender, EventArgs e)
        {
            NewInfo();

            iMaDonViHanhChanh = int.Parse(lvThongTin.SelectedItems[0].SubItems[0].Text);
            DonViHanhChanh dto = DonViHanhChanh_BUS.TraCuuDonViHanhChanhTheoMa(iMaDonViHanhChanh);

            if (dto.PhanCap > 0)
            {
                cbPhanCap.SelectedItem = dto.PhanCap;
            }

            if (dto.CapQuanTri != null)
            {
                cbCapQuanTri.Text = dto.CapQuanTri;
            }

            tbTen.Text = dto.Ten;
            tbMoTa.Text = dto.MoTa;

            pnQuanLy.Visible = false;
            pnSelect.Visible = false;
            pnInfo.Visible = true;
            pbBackChiTiet.Visible = false;
            //pbDonViHanhChanh.Visible = false;
            //lbDonViHanhChanh.Visible = false;

            cbPhanCap.Enabled = false;

            lbTitle.Text = "SỬA ĐƠN VỊ HÀNH CHÁNH";
            lbSelect.Text = "SỬA";
        }

        private void pbSua_MouseEnter(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_selected.png");
        }

        private void pbSua_MouseLeave(object sender, EventArgs e)
        {
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
        }
        #endregion



        #region Tra cuu
        private void pbOk_Click(object sender, EventArgs e)
        {
            refreshListView(tbSearch.Text);
        }

        private void pbOk_MouseEnter(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_selected.png");
        }

        private void pbOk_MouseLeave(object sender, EventArgs e)
        {
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
        }



        private void disableButtonPage(int iPage)
        {
            if (int.Parse(lbPage.Text) == 1)
            {
                pbBackPage.Enabled = false;
                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back_disable.png");
            }
            else
            {
                pbBackPage.Enabled = true;
                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
            }

            if (int.Parse(lbPage.Text) == iTotalPage)
            {
                pbNextPage.Enabled = false;
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next_disable.png");
            }
            else
            {
                pbNextPage.Enabled = true;
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
        }

        private void pbBackPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage.Text) > 1)
            {
                lbPage.Text = (int.Parse(lbPage.Text) - 1).ToString();
            }
        }

        private void pbBackPage_MouseEnter(object sender, EventArgs e)
        {
            pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back_selected.png");
        }

        private void pbBackPage_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage.Text));
        }



        private void pbNextPage_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage.Text) < iTotalPage)
            {
                lbPage.Text = (int.Parse(lbPage.Text) + 1).ToString();
            }
        }

        private void pbNextPage_MouseEnter(object sender, EventArgs e)
        {
            pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next_selected.png");
        }

        private void pbNextPage_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage.Text));
        }
        #endregion



        private void pbHuy_Click(object sender, EventArgs e)
        {
            Form_Notice frm_Notice = new Form_Notice("Dữ liệu mới sẽ không được lưu lại!", "Bạn có muốn thoát?", true);
            if (frm_Notice.Yes)
            {
                pnQuanLy.Visible = true;
                pnSelect.Visible = true;
                pnInfo.Visible = false;
                pbBackChiTiet.Visible = true;
                //pbDonViHanhChanh.Visible = true;
                //lbDonViHanhChanh.Visible = true;

                NewInfo();

                lbTitle.Text = "ĐƠN VỊ HÀNH CHÁNH";
                lbSelect.Text = "";

                refreshListView();

                lvThongTin.SelectedItems.Clear();
            }
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (lbSelect.Text == "THÊM")
            {
                DonViHanhChanh dto = new DonViHanhChanh();

                dto.Ten = tbTen.Text;
                if (cbPhanCap.SelectedIndex > 0)
                {
                    dto.PhanCap = (int)cbPhanCap.SelectedItem;
                }
                else
                {
                    dto.PhanCap = -1;
                }

                if (cbCapQuanTri.SelectedIndex > 0)
                {
                    dto.CapQuanTri = cbCapQuanTri.Text;
                }
                else
                {
                    dto.CapQuanTri = null;
                }
                dto.MoTa = tbMoTa.Text;

                if (DonViHanhChanh_BUS.Insert(dto))
                {
                    SuKien_DonViHanhChanh temp = new SuKien_DonViHanhChanh();
                    temp.MaSuKien = iMaSuKien;
                    temp.MaDonViHanhChanh = dto.Ma;

                    if (SuKien_DonViHanhChanh_BUS.Insert(temp))
                    {
                        pnQuanLy.Visible = true;
                        pnSelect.Visible = true;
                        pnInfo.Visible = false;
                        pbBackChiTiet.Visible = true;
                        //pbDonViHanhChanh.Visible = true;
                        //lbDonViHanhChanh.Visible = true;

                        NewInfo();

                        refreshListView();

                        lvThongTin.SelectedItems.Clear();
                    }
                    else
                    {
                        Form_Notice frm = new Form_Notice("Không thể tạo Đơn vị hành chánh!", false);
                    }
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Đơn vị hành chánh!", false);
                }
            }

            if (lbSelect.Text == "SỬA")
            {
                DonViHanhChanh dto = DonViHanhChanh_BUS.TraCuuDonViHanhChanhTheoMa(iMaDonViHanhChanh);

                dto.Ten = tbTen.Text;
                if (cbPhanCap.SelectedIndex > 0)
                {
                    dto.PhanCap = (int)cbPhanCap.SelectedItem;
                }
                else
                {
                    dto.PhanCap = -1;
                }

                if (cbCapQuanTri.SelectedIndex > 0)
                {
                    dto.CapQuanTri = cbCapQuanTri.Text;
                }
                else
                {
                    dto.CapQuanTri = null;
                }
                dto.MoTa = tbMoTa.Text;

                if (DonViHanhChanh_BUS.UpdateDonViHanhChanhInfo(dto))
                {
                    pnQuanLy.Visible = true;
                    pnSelect.Visible = true;
                    pnInfo.Visible = false;
                    pbBackChiTiet.Visible = true;
                    //pbDonViHanhChanh.Visible = true;
                    //lbDonViHanhChanh.Visible = true;

                    NewInfo();

                    refreshListView();
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể cập nhật Đơn vị hành chánh!", false);
                }
            }
        }

        private void pbHuy_MouseEnter(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel_selected.png");
        }

        private void pbHuy_MouseLeave(object sender, EventArgs e)
        {
            pbHuy.Image = Image.FromFile(@"Resources\ChucNang\icon_cancel.png");
        }

        private void pbHoanTat_MouseEnter(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_selected.png");
        }

        private void pbHoanTat_MouseLeave(object sender, EventArgs e)
        {
            pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
        }

        private void lvThongTin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin.SelectedItems.Count > 0)
            {
                if (lvThongTin.SelectedItems.Count == 1)
                {
                    pbSua.Enabled = true;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
                }
                else
                {
                    pbSua.Enabled = false;
                    pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
                }

                pbXoa.Enabled = true;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
            }
            else
            {
                pbXoa.Enabled = false;
                pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua.Enabled = false;
                pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
            }
        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {
            if (tbTen.Text.Length > 0)
            {
                pbHoanTat.Enabled = true;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok.png");
            }
            else
            {
                pbHoanTat.Enabled = false;
                pbHoanTat.Image = Image.FromFile(@"Resources\ChucNang\icon_ok_disable.png");
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text.Length > 0)
            {
                pbOk.Enabled = true;
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
            }
            else
            {
                pbOk.Enabled = false;
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                refreshListView();
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            SubFunction.MuteEnterPress(e);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                pbOk_Click(sender, e);
            }
        }

        private void lvThongTin_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin, e);
        }

        private void lbPage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pbBackPage.Enabled = false;
            pbNextPage.Enabled = false;

            tbPage.Visible = true;
            tbPage.Text = "";
            tbPage.Focus();
        }

        private void lbPage_TextChanged(object sender, EventArgs e)
        {
            refreshListView(int.Parse(lbPage.Text));

            disableButtonPage(int.Parse(lbPage.Text));
        }

        private void tbPage_LostFocus(object sender, EventArgs e)
        {
            tbPage.Visible = false;

            pbBackPage.Enabled = true;
            pbNextPage.Enabled = true;
        }

        private void tbPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbPage.Text.Length > 0)
                {
                    if (SubFunction.TestInt(tbPage.Text))
                    {
                        if (int.Parse(tbPage.Text) <= iTotalPage)
                        {
                            tbPage.Visible = false;
                            lbPage.Text = tbPage.Text;
                        }
                        else
                        {
                            Form_Notice frm_Notice = new Form_Notice("Không có trang này!", "Vui lòng kiểm tra lại.", false);
                        }
                    }
                    else
                    {
                        Form_Notice frm_Notice = new Form_Notice("Số trang không hợp lệ!", "Tắt bộ gõ dấu Tiếng Việt.", false);
                    }
                }
                else
                {
                    tbPage.Visible = false;
                }
            }
        }

        private void pbBackChiTiet_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbBackChiTiet_MouseEnter(object sender, EventArgs e)
        {
            pbBackChiTiet.Image = Image.FromFile(@"Resources\SuKien\button_home_selected.png");
        }

        private void pbBackChiTiet_MouseLeave(object sender, EventArgs e)
        {
            pbBackChiTiet.Image = Image.FromFile(@"Resources\SuKien\button_home.png");
        }

        private void cbPhanCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCapQuanTri.SelectedIndex = -1;

            if (cbPhanCap.SelectedIndex > 0)
            {
                LayDSDonViHanhChanhTheoCapLonHon_ComboBox(cbCapQuanTri, (int)cbPhanCap.SelectedItem);
                cbCapQuanTri.Enabled = true;
            }
            else
            {
                cbCapQuanTri.Enabled = false;
            }
        }
    }
}
