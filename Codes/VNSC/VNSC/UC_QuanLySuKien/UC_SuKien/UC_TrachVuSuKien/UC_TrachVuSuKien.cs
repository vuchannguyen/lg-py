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
    public partial class UC_TrachVuSuKien : UserControl
    {
        private List<TrachVuSuKien> list_dto;
        private List<int> list_DonViHanhChanh;

        private int iRows;
        private int iTotalPage;
        private int iRowsPerPage;

        private int iMaTrachVuSuKien;
        private int iMaSuKien;

        public UC_TrachVuSuKien()
        {
            InitializeComponent();
        }

        public UC_TrachVuSuKien(int iMa, string sTen)
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

                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_tvsukien_title.png");
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

        private void UC_TrachVuSuKien_Load(object sender, EventArgs e)
        {
            list_DonViHanhChanh = new List<int>();

            if (!LayDSDonViHanhChanh_ComboBox(cbDonViHanhChanh))
            {
                this.Visible = false;
                Form_Notice frm = new Form_Notice("Chưa khởi tạo Đơn vị hành chánh!", false);
                return;
            }
            else
            {
                this.Visible = true;
            }

            LoadPic();

            lbTitle.Left = lbSelect.Left;
            lbSelect.Text = "";

            pnQuanLy.Size = new System.Drawing.Size(670, 390);
            pnQuanLy.Location = SubFunction.SetCenterLocation(this.Size, pnQuanLy.Size);

            pnInfo.Size = new System.Drawing.Size(580, 310);
            pnInfo.Location = SubFunction.SetCenterLocation(this.Size, pnInfo.Size);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            list_dto = new List<TrachVuSuKien>();

            iRows = 16;
            refreshListView();
        }

        private void NewInfo()
        {
            tbTen.Text = "";
            tbMoTa.Text = "";

            cbDonViHanhChanh.SelectedIndex = -1;
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
            list_dto = TrachVuSuKien_BUS.TraCuuDSTrachVuSuKienTheoMaSuKien(iMaSuKien);
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
            list_dto = TrachVuSuKien_BUS.TraCuuDSTrachVuSuKienTheoTen(sTen);

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
                lvi.SubItems.Add(list_dto[i].DonViHanhChanh.Ten);
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
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);
            if (list_Temp.Count > 0)
            {
                list_DonViHanhChanh.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_DonViHanhChanh.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
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
            //pbTrachVuSuKien.Visible = false;
            //lbTrachVuSuKien.Visible = false;

            lbTitle.Text = "THÊM TRÁCH VỤ SỰ KIỆN";
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
                    if (SuKien_TrachVuSuKien_BUS.Delete(iMaSuKien, int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text)))
                    {
                        if (!TrachVuSuKien_BUS.Delete(int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text)))
                        {
                            SuKien_TrachVuSuKien dto_Temp = new SuKien_TrachVuSuKien();
                            dto_Temp.MaSuKien = iMaSuKien;
                            dto_Temp.MaTrachVuSuKien = int.Parse(lvThongTin.SelectedItems[i].SubItems[0].Text);
                            SuKien_TrachVuSuKien_BUS.Insert(dto_Temp);

                            Form_Notice frm = new Form_Notice("Không thể xóa!", "Vẫn còn Điều phối có TVSK này!", false);
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
            iMaTrachVuSuKien = int.Parse(lvThongTin.SelectedItems[0].SubItems[0].Text);
            TrachVuSuKien dto = TrachVuSuKien_BUS.TraCuuTrachVuSuKienTheoMa(iMaTrachVuSuKien);

            tbTen.Text = dto.Ten;
            tbMoTa.Text = dto.MoTa;

            pnQuanLy.Visible = false;
            pnSelect.Visible = false;
            pnInfo.Visible = true;
            pbBackChiTiet.Visible = false;
            //pbTrachVuSuKien.Visible = false;
            //lbTrachVuSuKien.Visible = false;

            lbTitle.Text = "SỬA TRÁCH VỤ SỰ KIỆN";
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
                //pbTrachVuSuKien.Visible = true;
                //lbTrachVuSuKien.Visible = true;

                NewInfo();

                lbTitle.Text = "TRÁCH VỤ SỰ KIỆN";
                lbSelect.Text = "";

                refreshListView();

                lvThongTin.SelectedItems.Clear();
            }
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (lbSelect.Text == "THÊM")
            {
                TrachVuSuKien dto = new TrachVuSuKien();

                dto.Ten = tbTen.Text;
                dto.MaDonViHanhChanh = list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex];
                dto.MoTa = tbMoTa.Text;

                if (TrachVuSuKien_BUS.Insert(dto))
                {
                    SuKien_TrachVuSuKien temp = new SuKien_TrachVuSuKien();
                    temp.MaSuKien = iMaSuKien;
                    temp.MaTrachVuSuKien = dto.Ma;

                    if (SuKien_TrachVuSuKien_BUS.Insert(temp))
                    {
                        pnQuanLy.Visible = true;
                        pnSelect.Visible = true;
                        pnInfo.Visible = false;
                        pbBackChiTiet.Visible = true;
                        //pbTrachVuSuKien.Visible = true;
                        //lbTrachVuSuKien.Visible = true;

                        NewInfo();

                        refreshListView();

                        lvThongTin.SelectedItems.Clear();
                    }
                    else
                    {
                        Form_Notice frm = new Form_Notice("Không thể tạo Trách vụ sự kiện!", false);
                    }
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Trách vụ sự kiện!", false);
                }
            }

            if (lbSelect.Text == "SỬA")
            {
                TrachVuSuKien dto = TrachVuSuKien_BUS.TraCuuTrachVuSuKienTheoMa(iMaTrachVuSuKien);

                dto.Ten = tbTen.Text;
                dto.MaDonViHanhChanh = list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex];
                dto.MoTa = tbMoTa.Text;

                if (TrachVuSuKien_BUS.UpdateTrachVuSuKienInfo(dto))
                {
                    pnQuanLy.Visible = true;
                    pnSelect.Visible = true;
                    pnInfo.Visible = false;
                    pbBackChiTiet.Visible = true;
                    //pbTrachVuSuKien.Visible = true;
                    //lbTrachVuSuKien.Visible = true;

                    NewInfo();

                    refreshListView();
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể cập nhật Trách vụ sự kiện!", false);
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
            if (tbTen.Text.Length > 0 && cbDonViHanhChanh.SelectedIndex >= 0)
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

        private void cbDonViHanhChanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbTen.Text.Length > 0 && cbDonViHanhChanh.SelectedIndex >= 0)
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
    }
}
