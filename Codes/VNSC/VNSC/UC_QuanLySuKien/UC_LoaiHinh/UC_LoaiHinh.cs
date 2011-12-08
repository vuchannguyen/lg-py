﻿using System;
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
    public partial class UC_LoaiHinh : UserControl
    {
        private List<LoaiHinh> list_dto;
        private List<string> list_NhomLoaiHinh;

        private string sMa;
        private int iRows;
        private int iTotalPage;
        private int iRowsPerPage;

        Font myPageFont;

        public UC_LoaiHinh()
        {
            InitializeComponent();
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

                pbTitle.Image = Image.FromFile(@"Resources\SuKien\icon_sukien_loaihinh_title.png");

                pbTraCuu.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
            catch
            {
                this.Dispose();
                Form_Notice frm_Notice = new Form_Notice("Kiểm tra thư mục Resource!", false);
            }
        }

        private void UC_LoaiHinh_Load(object sender, EventArgs e)
        {
            list_NhomLoaiHinh = new List<string>();

            if (!LayDSNhomLoaiHinh_ComboBox(cbNhomLoaiHinh))
            {
                this.Visible = false;
                Form_Notice frm = new Form_Notice("Chưa khởi tạo Khu vực!", false);
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

            pnInfo.Size = new System.Drawing.Size(580, 390);
            pnInfo.Location = SubFunction.SetCenterLocation(this.Size, pnInfo.Size);

            tbPage.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage.LostFocus += new EventHandler(tbPage_LostFocus);

            list_dto = new List<LoaiHinh>();
            myPageFont = lbPage.Font;
            iRows = 16;
            refreshListView();
        }

        private void NewInfo()
        {
            tbTen.Text = "";
            tbMoTa.Text = "";

            if (cbNhomLoaiHinh.Items.Count > 0)
            {
                cbNhomLoaiHinh.SelectedIndex = 0;
            }

            rbThieu.Checked = true;
            lbKhac.Enabled = false;
            tbKhac.Enabled = false;

            tbKhac.Text = "";
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
            list_dto = LoaiHinh_BUS.LayDSLoaiHinh();
            if (list_dto.Count > 0)
            {
                sMa = list_dto[list_dto.Count - 1].Ma;
            }

            setTotalPage();
            lbTotalPage.Text = iTotalPage.ToString() + " Trang";
            SubFunction.SetError(lbPage, lbPage.Top, pnPage.Size, "1");
            refreshListView(1);

            pbOk.Enabled = false;
            pbOk.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView(string sTen)
        {
            list_dto = LoaiHinh_BUS.TraCuuDSLoaiHinhTheoTen(sTen);

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
                lvi.Text = list_dto[i].Ma;
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto[i].Ten);
                lvi.SubItems.Add(NhomLoaiHinh_BUS.TraCuuNhomLoaiHinhTheoMa(list_dto[i].MaNhomLoaiHinh).Ten);
                lvi.SubItems.Add(list_dto[i].Nganh);
                lvi.SubItems.Add(list_dto[i].MoTa);

                lvThongTin.Items.Add(lvi);
            }

            pbXoa.Enabled = false;
            pbXoa.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            pbSua.Enabled = false;
            pbSua.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
        }

        private bool LayDSNhomLoaiHinh_ComboBox(ComboBox cb)
        {
            List<NhomLoaiHinh> list_Temp = NhomLoaiHinh_BUS.LayDSNhomLoaiHinh();
            if (list_Temp.Count > 0)
            {
                list_NhomLoaiHinh.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_NhomLoaiHinh.Add(list_Temp[i].Ma);
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

            lbTitle.Text = "THÊM LOẠI HÌNH";
            lbSelect.Text = "THÊM";

            NewInfo();

            list_dto = LoaiHinh_BUS.LayDSLoaiHinh();
            if (list_dto.Count > 0)
            {
                tbMa.Text = "L" + SubFunction.ThemMa3So(int.Parse(sMa.Substring(sMa.Length - 3, 3)));
            }
            else
            {
                tbMa.Text = "L001";
            }

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
                    if (!LoaiHinh_BUS.Delete(lvThongTin.SelectedItems[i].SubItems[0].Text))
                    {
                        Form_Notice frm_Notice = new Form_Notice("Không thể xóa!", "Vẫn còn Hồ sơ có Loại hình này!", false);
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
            LoaiHinh dto = LoaiHinh_BUS.TraCuuLoaiHinhTheoMa(lvThongTin.SelectedItems[0].SubItems[0].Text);
            tbMa.Text = dto.Ma;
            cbNhomLoaiHinh.Text = NhomLoaiHinh_BUS.TraCuuNhomLoaiHinhTheoMa(dto.MaNhomLoaiHinh).Ten;
            tbTen.Text = dto.Ten;

            if (dto.Nganh == "Ấu")
            {
                rbAu.Checked = true;
            }

            if (dto.Nganh == "Thiếu")
            {
                rbThieu.Checked = true;
            }

            if (dto.Nganh == "Kha")
            {
                rbKha.Checked = true;
            }

            if (dto.Nganh == "Tráng")
            {
                rbTrang.Checked = true;
            }

            if (dto.Nganh != "Ấu" && dto.Nganh != "Thiếu" && dto.Nganh != "Kha" && dto.Nganh != "Tráng")
            {
                rbKhac.Checked = true;
                lbKhac.Enabled = true;
                tbKhac.Enabled = true;
                tbKhac.Text = dto.Nganh;
            }

            tbMoTa.Text = dto.MoTa;

            pnQuanLy.Visible = false;
            pnSelect.Visible = false;
            pnInfo.Visible = true;

            lbTitle.Text = "SỬA LOẠI HÌNH";
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

                NewInfo();

                lbTitle.Text = "LOẠI HÌNH";
                lbSelect.Text = "";

                lvThongTin.SelectedItems.Clear();
            }
        }

        private void pbHoanTat_Click(object sender, EventArgs e)
        {
            if (lbSelect.Text == "THÊM")
            {
                LoaiHinh dto = new LoaiHinh();
                dto.Ma = tbMa.Text;
                dto.MaNhomLoaiHinh = list_NhomLoaiHinh[cbNhomLoaiHinh.SelectedIndex];
                dto.Ten = tbTen.Text;

                if (rbAu.Checked)
                {
                    dto.Nganh = rbAu.Text;
                }

                if (rbThieu.Checked)
                {
                    dto.Nganh = rbThieu.Text;
                }

                if (rbKha.Checked)
                {
                    dto.Nganh = rbKha.Text;
                }

                if (rbTrang.Checked)
                {
                    dto.Nganh = rbTrang.Text;
                }

                if (rbKhac.Checked)
                {
                    dto.Nganh = tbKhac.Text;
                }

                dto.MoTa = tbMoTa.Text;

                if (LoaiHinh_BUS.Insert(dto))
                {
                    pnQuanLy.Visible = true;
                    pnSelect.Visible = true;
                    pnInfo.Visible = false;

                    NewInfo();

                    lbTitle.Text = "LOẠI HÌNH";
                    lbSelect.Text = "";

                    refreshListView();

                    lvThongTin.SelectedItems.Clear();
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể tạo Loại hình!", false);
                }
            }

            if (lbSelect.Text == "SỬA")
            {
                LoaiHinh dto = LoaiHinh_BUS.TraCuuLoaiHinhTheoMa(tbMa.Text);
                dto.MaNhomLoaiHinh = list_NhomLoaiHinh[cbNhomLoaiHinh.SelectedIndex];
                dto.Ten = tbTen.Text;

                if (rbAu.Checked)
                {
                    dto.Nganh = rbAu.Text;
                }

                if (rbThieu.Checked)
                {
                    dto.Nganh = rbThieu.Text;
                }

                if (rbKha.Checked)
                {
                    dto.Nganh = rbKha.Text;
                }

                if (rbTrang.Checked)
                {
                    dto.Nganh = rbTrang.Text;
                }

                if (rbKhac.Checked)
                {
                    dto.Nganh = tbKhac.Text;
                }

                dto.MoTa = tbMoTa.Text;

                if (LoaiHinh_BUS.UpdateLoaiHinhInfo(dto))
                {
                    pnQuanLy.Visible = true;
                    pnSelect.Visible = true;
                    pnInfo.Visible = false;

                    NewInfo();

                    lbTitle.Text = "LOẠI HÌNH";
                    lbSelect.Text = "";

                    refreshListView();
                }
                else
                {
                    Form_Notice frm = new Form_Notice("Không thể cập nhật Loại hình!", false);
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
            if (tbKhac.Enabled)
            {
                if (tbTen.Text.Length > 0 && tbKhac.Text.Length > 0)
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
            else
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

        private void rbKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKhac.Checked)
            {
                lbKhac.Enabled = true;
                tbKhac.Enabled = true;

                tbKhac.Text = "";
                tbTen_TextChanged(sender, e);
            }
            else
            {
                lbKhac.Enabled = false;
                tbKhac.Enabled = false;

                tbKhac.Text = "";
                tbTen_TextChanged(sender, e);
            }
        }

        private void tbKhac_TextChanged(object sender, EventArgs e)
        {
            tbTen_TextChanged(sender, e);
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
    }
}
