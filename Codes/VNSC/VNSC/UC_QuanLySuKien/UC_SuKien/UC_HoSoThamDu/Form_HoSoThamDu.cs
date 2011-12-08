using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAO;
using BUS;
using System.IO;

namespace VNSC
{
    public partial class Form_HoSoThamDu : Form
    {
        private int iMaSuKien;

        private List<SuKien_HoSo> list_dto_HSTD;
        private List<HoSo> list_dto_HSCN;

        private int iRows_HSTD;
        private int iTotalPage_HSTD;
        private int iRowsPerPage_HSTD;

        private int iRows_HSCN;
        private int iTotalPage_HSCN;
        private int iRowsPerPage_HSCN;

        private List<string> list_FolderAvatar;
        private string sAvatarPath;

        Form_Notice frm_Notice;
        Form_Confirm frm_Confirm;

        public Form_HoSoThamDu()
        {
            InitializeComponent();

            this.ShowDialog();
        }

        public Form_HoSoThamDu(int iMa)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            this.ShowDialog();
        }

        private void LoadPic()
        {
            try
            {
                //this.BackgroundImage = Image.FromFile(@"Resources\background.jpg");
                pnTopBar.BackgroundImage = Image.FromFile(@"Resources\SuKien\topbar_green.png");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbThem_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
                pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbTraCuu_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");



                pbTraCuu_HSCN.Image = Image.FromFile(@"Resources\ChucNang\icon_searchtextbox.png");
                pbOk_HSCN.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
                pbTotalPage_HSCN.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage_HSCN.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage_HSCN.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");



                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
                pbHoSoCaNhan.Image = Image.FromFile(@"Resources\SuKien\icon_hosothamdu_hscn.png");
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void Form_HoSoThamDu_Load(object sender, EventArgs e)
        {
            LoadPic();

            lvThongTin_HSTD.LostFocus += new EventHandler(lvThongTin_HSTD_LostFocus);
            lvThongTin_HSCN.LostFocus += new EventHandler(lvThongTin_HSCN_LostFocus);

            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("DB");
            list_FolderAvatar.Add("Avatar");

            iRows_HSTD = 20;
            iRows_HSCN = 20;

            refreshListView_HSTD();
            refreshListView_HSCN();
        }

        private void lvThongTin_HSTD_LostFocus(object sender, EventArgs e)
        {
            if (!pbXoa_HSTD.Focused && !pbSua_HSTD.Focused)
            {
                lvThongTin_HSTD.SelectedItems.Clear();
            }

            if (!lvThongTin_HSCN.Focused)
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }

        private void lvThongTin_HSCN_LostFocus(object sender, EventArgs e)
        {
            lvThongTin_HSCN.SelectedItems.Clear();

            if (!lvThongTin_HSTD.Focused)
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }

        #region Refresh Listview HSTD
        private void setRowsPerPage_HSTD()
        {
            int n = list_dto_HSTD.Count - ((int.Parse(lbPage_HSTD.Text) - 1) * iRows_HSTD);
            if (n < iRows_HSTD)
            {
                iRowsPerPage_HSTD = list_dto_HSTD.Count;
            }
            else
            {
                iRowsPerPage_HSTD = int.Parse(lbPage_HSTD.Text) * iRows_HSTD;
            }
        }

        private void setTotalPage_HSTD()
        {
            if (list_dto_HSTD.Count % iRows_HSTD == 0)
            {
                iTotalPage_HSTD = list_dto_HSTD.Count / iRows_HSTD;
            }
            else
            {
                iTotalPage_HSTD = (list_dto_HSTD.Count / iRows_HSTD) + 1;
            }
        }

        private void refreshListView_HSTD()
        {
            tbSearch_HSTD.Text = "";
            list_dto_HSTD = SuKien_HoSo_BUS.TraCuuDSSuKien_HoSoTheoMaSuKien(iMaSuKien);

            setTotalPage_HSTD();
            lbTotalPage_HSTD.Text = iTotalPage_HSTD.ToString() + " Trang";
            SubFuntion.SetError(lbPage_HSTD, lbPage_HSTD.Top, pnPage.Size, "1");
            refreshListView_HSTD(1);

            pbOk_HSTD.Enabled = false;
            pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView_HSTD(string sTen)
        {
            list_dto_HSTD = SuKien_HoSo_BUS.TraCuuDSSuKien_HoSoTheoTen(iMaSuKien, sTen);

            setTotalPage_HSTD();
            lbTotalPage_HSTD.Text = iTotalPage_HSTD.ToString() + " Trang";
            SubFuntion.SetError(lbPage_HSTD, lbPage_HSTD.Top, pnPage.Size, "1");
            refreshListView_HSTD(1);
        }

        private void refreshListView_HSTD(int Page)
        {
            SubFuntion.ClearlvItem(lvThongTin_HSTD);
            setRowsPerPage_HSTD();
            for (int i = (int.Parse(lbPage_HSTD.Text) - 1) * iRows_HSTD; i < iRowsPerPage_HSTD; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto_HSTD[i].Ma;
                lvi.SubItems.Add(SubFuntion.setSTT(i + 1));
                lvi.SubItems.Add(list_dto_HSTD[i].HoTen);
                //lvi.SubItems.Add(DoiTuong_BUS.TraCuuDoiTuongTheoMa(list_dto_HSTD[i].MaDoiTuong).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto_HSTD[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto_HSTD[i].Nganh);
                lvi.SubItems.Add(list_dto_HSTD[i].DonVi);

                lvThongTin_HSTD.Items.Add(lvi);
            }

            pbXoa_HSTD.Enabled = false;
            pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            pbSua_HSTD.Enabled = false;
            pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
        }
        #endregion



        #region Refresh Listview HSCN
        private void setRowsPerPage_HSCN()
        {
            int n = list_dto_HSCN.Count - ((int.Parse(lbPage_HSCN.Text) - 1) * iRows_HSCN);
            if (n < iRows_HSCN)
            {
                iRowsPerPage_HSCN = list_dto_HSCN.Count;
            }
            else
            {
                iRowsPerPage_HSCN = int.Parse(lbPage_HSCN.Text) * iRows_HSCN;
            }
        }

        private void setTotalPage_HSCN()
        {
            if (list_dto_HSCN.Count % iRows_HSCN == 0)
            {
                iTotalPage_HSCN = list_dto_HSCN.Count / iRows_HSCN;
            }
            else
            {
                iTotalPage_HSCN = (list_dto_HSCN.Count / iRows_HSCN) + 1;
            }
        }

        private void refreshListView_HSCN()
        {
            tbSearch_HSCN.Text = "";
            list_dto_HSCN = HoSo_BUS.LayDSHoSo();

            setTotalPage_HSCN();
            lbTotalPage_HSCN.Text = iTotalPage_HSCN.ToString() + " Trang";
            SubFuntion.SetError(lbPage_HSCN, lbPage_HSCN.Top, pnPage.Size, "1");
            refreshListView_HSCN(1);

            pbOk_HSCN.Enabled = false;
            pbOk_HSCN.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView_HSCN(string sTen)
        {
            list_dto_HSCN = HoSo_BUS.TraCuuDSHoSoTheoTen(sTen);

            setTotalPage_HSCN();
            lbTotalPage_HSCN.Text = iTotalPage_HSCN.ToString() + " Trang";
            SubFuntion.SetError(lbPage_HSCN, lbPage_HSCN.Top, pnPage.Size, "1");
            refreshListView_HSCN(1);
        }

        private void refreshListView_HSCN(int Page)
        {
            SubFuntion.ClearlvItem(lvThongTin_HSCN);
            setRowsPerPage_HSCN();
            for (int i = (int.Parse(lbPage_HSCN.Text) - 1) * iRows_HSCN; i < iRowsPerPage_HSCN; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto_HSCN[i].Ma;
                lvi.SubItems.Add(SubFuntion.setSTT(i + 1));
                lvi.SubItems.Add(list_dto_HSCN[i].HoTen);
                lvi.SubItems.Add(DoiTuong_BUS.TraCuuDoiTuongTheoMa(list_dto_HSCN[i].MaDoiTuong).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto_HSCN[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto_HSCN[i].Nganh);
                lvi.SubItems.Add(list_dto_HSCN[i].DonVi);

                lvThongTin_HSCN.Items.Add(lvi);
            }
        }
        #endregion



        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pbExit_MouseEnter(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit_mouseover.png");
        }

        private void pbExit_MouseLeave(object sender, EventArgs e)
        {
            pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");
        }



        private void uc_HSTD_Disposed(object sender, EventArgs e)
        {
            refreshListView_HSTD();
        }

        private void pbThem_HSTD_Click(object sender, EventArgs e)
        {
            UC_SuKien_HoSoThamDu uc_HSTD = new UC_SuKien_HoSoThamDu(iMaSuKien);

            this.Controls.Add(uc_HSTD);

            uc_HSTD.Location = SubFuntion.SetCenterLocation(this.Size, uc_HSTD.Size);
            uc_HSTD.BringToFront();
            uc_HSTD.Disposed += new EventHandler(uc_HSTD_Disposed);
        }

        private void pbThem_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbThem_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_them_selected.png");
        }

        private void pbThem_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbThem_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_them.png");
        }



        private string setAvatarPath(string sMa, string sDate)
        {
            if (sDate.Length > 23) //22/02/2222 - 22:22:22 Chieu
            {
                if (sDate.EndsWith("Chiều"))
                {
                    sAvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "CH.jpg";
                }
                else
                {
                    sAvatarPath = sMa + "_" + sDate.Substring(0, 2) + sDate.Substring(3, 2) + sDate.Substring(6, 4) + "_" + sDate.Substring(13, 2) + sDate.Substring(16, 2) + sDate.Substring(19, 2) + "SA.jpg";
                }
            }

            return sAvatarPath;
        }

        private bool DeleteHoSo(string sMa)
        {
            List<SuKien_HoSo_HuanLuyen> list_SuKien_HoSo_HuanLuyen = SuKien_HoSo_HuanLuyen_BUS.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
            foreach (SuKien_HoSo_HuanLuyen dto_Temp in list_SuKien_HoSo_HuanLuyen)
            {
                int iTemp = dto_Temp.MaSuKien_HuanLuyen;
                if (!SuKien_HoSo_HuanLuyen_BUS.Delete(dto_Temp.MaSuKien_HoSo, dto_Temp.MaSuKien_HuanLuyen))
                {
                    return false;
                }

                if (!SuKien_HuanLuyen_BUS.Delete(iTemp))
                {
                    return false;
                }
            }

            string sPath = "";
            sPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMa, SuKien_HoSo_BUS.TraCuuSuKien_HoSoTheoMa(sMa).NgayCapNhat));

            if (File.Exists(sPath))
            {
                File.Delete(sPath);
            }

            if (!SuKien_HoSoThamDu_BUS.Delete(iMaSuKien, sMa))
            {
                return false;
            }

            if (!SuKien_HoSo_BUS.Delete(sMa))
            {
                return false;
            }

            return true;
        }

        private void pbXoa_HSTD_Click(object sender, EventArgs e)
        {
            pbXoa_HSTD.Focus();
            frm_Confirm = new Form_Confirm("Đồng ý xóa " + lvThongTin_HSTD.SelectedItems.Count + " dữ liệu?");
            if (frm_Confirm.Yes)
            {
                for (int i = 0; i < lvThongTin_HSTD.SelectedItems.Count; i++)
                {
                    if (!DeleteHoSo(lvThongTin_HSTD.SelectedItems[i].SubItems[0].Text))
                    {
                        frm_Notice = new Form_Notice("Không thể xóa Hồ Sơ " + lvThongTin_HSTD.SelectedItems[i].SubItems[0].Text + "!", false);
                        break;
                    }
                }

                refreshListView_HSTD();
            }
        }

        private void pbXoa_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_selected.png");
        }

        private void pbXoa_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
        }



        private void pbSua_HSTD_Click(object sender, EventArgs e)
        {
            pbSua_HSTD.Focus();

            UC_SuKien_HoSoThamDu uc_HSTD = new UC_SuKien_HoSoThamDu(iMaSuKien, lvThongTin_HSTD.SelectedItems[0].SubItems[0].Text);
            uc_HSTD.Disposed += new EventHandler(uc_HSTD_Disposed);
            this.Controls.Add(uc_HSTD);

            uc_HSTD.Location = SubFuntion.SetCenterLocation(this.Size, uc_HSTD.Size);
            uc_HSTD.BringToFront();
        }

        private void pbSua_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_selected.png");
        }

        private void pbSua_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
        }



        #region Tra cuu
        private void tbSearch_HSTD_KeyDown(object sender, KeyEventArgs e)
        {
            SubFuntion.MuteEnterPress(e);
        }

        private void tbSearch_HSTD_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFuntion.tbPass(e))
            {
                pbOk_HSTD_Click(sender, e);
            }
        }

        private void tbSearch_HSTD_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch_HSTD.Text.Length > 0)
            {
                pbOk_HSTD.Enabled = true;
                pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
            }
            else
            {
                pbOk_HSTD.Enabled = false;
                pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");

                refreshListView_HSTD();
            }
        }

        private void pbOk_HSTD_Click(object sender, EventArgs e)
        {
            refreshListView_HSTD(tbSearch_HSTD.Text);
        }

        private void pbOk_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_selected.png");
        }

        private void pbOk_HSTD_MouseLeave(object sender, EventArgs e)
        {
            pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok.png");
        }



        private void disableButtonPage(int iPage)
        {
            if (int.Parse(lbPage_HSTD.Text) == 1)
            {
                pbBackPage_HSTD.Enabled = false;
                pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back_disable.png");
            }
            else
            {
                pbBackPage_HSTD.Enabled = true;
                pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
            }

            if (int.Parse(lbPage_HSTD.Text) == iTotalPage_HSTD)
            {
                pbNextPage_HSTD.Enabled = false;
                pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next_disable.png");
            }
            else
            {
                pbNextPage_HSTD.Enabled = true;
                pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
        }

        private void pbBackPage_HSTD_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage_HSTD.Text) > 1)
            {
                lbPage_HSTD.Text = (int.Parse(lbPage_HSTD.Text) - 1).ToString();
            }
        }

        private void pbBackPage_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back_selected.png");
        }

        private void pbBackPage_HSTD_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage_HSTD.Text));
        }



        private void pbNextPage_HSTD_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage_HSTD.Text) < iTotalPage_HSTD)
            {
                lbPage_HSTD.Text = (int.Parse(lbPage_HSTD.Text) + 1).ToString();
            }
        }

        private void pbNextPage_HSTD_MouseEnter(object sender, EventArgs e)
        {
            pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next_selected.png");
        }

        private void pbNextPage_HSTD_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage(int.Parse(lbPage_HSTD.Text));
        }

        private void lbPage_HSTD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pbBackPage_HSTD.Enabled = false;
            pbNextPage_HSTD.Enabled = false;

            tbPage_HSTD.Visible = true;
            tbPage_HSTD.Text = "";
            tbPage_HSTD.Focus();
        }

        private void lbPage_HSTD_TextChanged(object sender, EventArgs e)
        {
            refreshListView_HSTD(int.Parse(lbPage_HSTD.Text));

            disableButtonPage(int.Parse(lbPage_HSTD.Text));
        }

        private void tbPage_HSTD_LostFocus(object sender, EventArgs e)
        {
            tbPage_HSTD.Visible = false;

            pbBackPage_HSTD.Enabled = true;
            pbNextPage_HSTD.Enabled = true;
        }

        private void tbPage_HSTD_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFuntion.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFuntion.MuteEnterPress(e);
        }

        private void tbPage_HSTD_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFuntion.tbPass(e))
            {
                if (tbPage_HSTD.Text.Length > 0)
                {
                    if (SubFuntion.TestInt(tbPage_HSTD.Text))
                    {
                        if (int.Parse(tbPage_HSTD.Text) <= iTotalPage_HSTD)
                        {
                            tbPage_HSTD.Visible = false;
                            lbPage_HSTD.Text = tbPage_HSTD.Text;
                        }
                        else
                        {
                            frm_Notice = new Form_Notice("Không có trang này!", "Vui lòng kiểm tra lại.", false);
                        }
                    }
                    else
                    {
                        frm_Notice = new Form_Notice("Số trang không hợp lệ!", "Tắt bộ gõ dấu Tiếng Việt.", false);
                    }
                }
                else
                {
                    tbPage_HSTD.Visible = false;
                }
            }
        }
        #endregion



        private void lvThongTin_HSTD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin_HSTD.SelectedItems.Count > 0)
            {
                if (lvThongTin_HSTD.SelectedItems.Count == 1)
                {
                    pbSua_HSTD.Enabled = true;
                    pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua.png");
                }
                else
                {
                    pbSua_HSTD.Enabled = false;
                    pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");
                }

                pbXoa_HSTD.Enabled = true;
                pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");

                pbTransfer.Enabled = true;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow.png");
            }
            else
            {
                pbXoa_HSTD.Enabled = false;
                pbXoa_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
                pbSua_HSTD.Enabled = false;
                pbSua_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_sua_disable.png");

                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }

        private void lvThongTin_HSTD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Form_Main.form_Fade();
            //Form_Detail frm_Detail = new Form_Detail(lvThongTin_HSTD.SelectedItems[0].Text);
            //Form_Main.form_Normal();
        }

        private void lvThongTin_HSTD_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin_HSTD, e);
        }

 

        private void pbTransfer_Click(object sender, EventArgs e)
        {
            if (lvThongTin_HSCN.Focused)
            {
                for (int i = 0; i < lvThongTin_HSCN.SelectedItems.Count; i++)
                {
                    UC_SuKien_HoSoThamDu uc_HSTD = new UC_SuKien_HoSoThamDu(iMaSuKien, lvThongTin_HSCN.SelectedItems[i].SubItems[0].Text, true);
                    uc_HSTD.Disposed += new EventHandler(uc_HSTD_Disposed); SuKien_HoSo Temp = new SuKien_HoSo();
                    this.Controls.Add(uc_HSTD);

                    uc_HSTD.Location = SubFuntion.SetCenterLocation(this.Size, uc_HSTD.Size);
                    uc_HSTD.BringToFront();
                }
            }
        }

        private void pbTransfer_MouseEnter(object sender, EventArgs e)
        {
            pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_selected.png");
        }

        private void pbTransfer_MouseLeave(object sender, EventArgs e)
        {
            if (lvThongTin_HSTD.SelectedItems.Count > 0 || lvThongTin_HSCN.SelectedItems.Count > 0)
            {
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow.png");
            }
        }

        private void lvThongTin_HSCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin_HSCN.SelectedItems.Count > 0)
            {
                pbTransfer.Enabled = true;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow.png");
            }
            else
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu_arrow_disable.png");
            }
        }
    }
}
