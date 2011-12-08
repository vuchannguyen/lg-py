using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Function;
using DAO;
using BUS;

namespace VNSC
{
    public partial class Form_DieuPhoi : Form
    {
        private int iMaSuKien;

        private List<SuKien_HoSo> list_dto_HSTD;
        private List<DieuPhoi> list_dto_DP;

        private List<int> list_DonViHanhChanh;
        private List<int> list_TrachVuSuKien;

        private int iRows_HSTD;
        private int iTotalPage_HSTD;
        private int iRowsPerPage_HSTD;

        private int iRows_DP;
        private int iTotalPage_DP;
        private int iRowsPerPage_DP;

        private List<string> list_FolderAvatar;
        //private string sAvatarPath;

        Form_Notice frm_Notice;
        Form_Confirm frm_Confirm;

        public Form_DieuPhoi()
        {
            InitializeComponent();

            this.ShowDialog();
        }

        public Form_DieuPhoi(int iMa)
        {
            InitializeComponent();

            iMaSuKien = iMa;
            this.ShowDialog();
        }

        private void LoaDPic()
        {
            try
            {
                //this.BackgroundImage = Image.FromFile(@"Resources\background.jpg");
                pnTopBar.BackgroundImage = Image.FromFile(@"Resources\SuKien\topbar_green.png");
                pbExit.Image = Image.FromFile(@"Resources\ChucNang\button_exit.png");

                pbXoa_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");

                pbTotalPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");

                pbTotalPage_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_totalpagenumber.png");

                pbBackPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
                pbNextPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");



                pbHoSoThamDu.Image = Image.FromFile(@"Resources\SuKien\button_hsthamdu.png");
                pbDieuPhoi.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi.png");
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow_disable.png");
            }
            catch
            {
                MessageBox.Show("ERROR!", "Kiểm tra thư mục Resource!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void Form_DieuPhoi_Load(object sender, EventArgs e)
        {
            list_DonViHanhChanh = new List<int>();
            list_DonViHanhChanh.Add(-1);
            list_TrachVuSuKien = new List<int>();

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

            if (!LayDSTrachVuSuKien_ComboBox(cbTrachVuSuKien))
            {
                this.Visible = false;
                Form_Notice frm = new Form_Notice("Chưa khởi tạo Trách vụ sự kiện!", false);
                return;
            }
            else
            {
                this.Visible = true;
            }

            LoaDPic();

            lvThongTin_HSTD.LostFocus += new EventHandler(lvThongTin_HSTD_LostFocus);
            lvThongTin_DP.LostFocus += new EventHandler(lvThongTin_DP_LostFocus);

            list_FolderAvatar = new List<string>();
            list_FolderAvatar.Add("DB");
            list_FolderAvatar.Add("Avatar");

            tbPage_HSTD.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage_HSTD.LostFocus += new EventHandler(tbPage_HSTD_LostFocus);

            tbPage_DP.Location = new Point(pnPage.Left + 2, pnPage.Top - 1);
            tbPage_DP.LostFocus += new EventHandler(tbPage_DP_LostFocus);

            iRows_HSTD = 19;
            iRows_DP = 19;

            refreshListView_HSTD();
            //refreshListView_DP();
        }

        private void lvThongTin_HSTD_LostFocus(object sender, EventArgs e)
        {
            lvThongTin_HSTD.SelectedItems.Clear();

            if (!lvThongTin_DP.Focused)
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow_disable.png");
            }
        }

        private void lvThongTin_DP_LostFocus(object sender, EventArgs e)
        {
            if (!pbXoa_DP.Focused)
            {
                lvThongTin_DP.SelectedItems.Clear();
            }

            if (!lvThongTin_HSTD.Focused)
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow_disable.png");
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
            //tbSearch_HSTD.Text = "";
            list_dto_HSTD = SuKien_HoSo_BUS.TraCuuDSSuKien_HoSoTheoMaSuKien(iMaSuKien);

            setTotalPage_HSTD();
            lbTotalPage_HSTD.Text = iTotalPage_HSTD.ToString() + " Trang";
            SubFunction.SetError(lbPage_HSTD, lbPage_HSTD.Top, pnPage.Size, "1");
            refreshListView_HSTD(1);

            //pbOk_HSTD.Enabled = false;
            //pbOk_HSTD.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView_HSTD(string sTen)
        {
            list_dto_HSTD = SuKien_HoSo_BUS.TraCuuDSSuKien_HoSoTheoTen(iMaSuKien, sTen);

            setTotalPage_HSTD();
            lbTotalPage_HSTD.Text = iTotalPage_HSTD.ToString() + " Trang";
            SubFunction.SetError(lbPage_HSTD, lbPage_HSTD.Top, pnPage.Size, "1");
            refreshListView_HSTD(1);
        }

        private void refreshListView_HSTD(int Page)
        {
            SubFunction.ClearlvItem(lvThongTin_HSTD);
            setRowsPerPage_HSTD();
            for (int i = (int.Parse(lbPage_HSTD.Text) - 1) * iRows_HSTD; i < iRowsPerPage_HSTD; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto_HSTD[i].Ma;
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(list_dto_HSTD[i].HoTen);
                //lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(list_dto_HSTD[i].MaNhomTrachVu).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(list_dto_HSTD[i].MaTrachVu).Ten);
                lvi.SubItems.Add(list_dto_HSTD[i].Nganh);
                lvi.SubItems.Add(list_dto_HSTD[i].DonVi);

                lvThongTin_HSTD.Items.Add(lvi);
            }
        }
        #endregion



        #region Refresh Listview DP
        private void setRowsPerPage_DP()
        {
            int n = list_dto_DP.Count - ((int.Parse(lbPage_DP.Text) - 1) * iRows_DP);
            if (n < iRows_DP)
            {
                iRowsPerPage_DP = list_dto_DP.Count;
            }
            else
            {
                iRowsPerPage_DP = int.Parse(lbPage_DP.Text) * iRows_DP;
            }
        }

        private void setTotalPage_DP()
        {
            if (list_dto_DP.Count % iRows_DP == 0)
            {
                iTotalPage_DP = list_dto_DP.Count / iRows_DP;
            }
            else
            {
                iTotalPage_DP = (list_dto_DP.Count / iRows_DP) + 1;
            }
        }

        private void refreshListView_DP()
        {
            //tbSearch_DP.Text = "";
            list_dto_DP = DieuPhoi_BUS.TraCuuDSDieuPhoiTheoMaSuKien(iMaSuKien);

            setTotalPage_DP();
            lbTotalPage_DP.Text = iTotalPage_DP.ToString() + " Trang";
            SubFunction.SetError(lbPage_DP, lbPage_DP.Top, pnPage.Size, "1");
            refreshListView_DP(1);
        }

        private void refreshListView_DP(int iMaDonViHanhChanh, int iMaTrachVuSuKien)
        {
            //tbSearch_DP.Text = "";
            list_dto_DP = DieuPhoi_BUS.TraCuuDSDieuPhoiTheoMaSuKien_DonViHanhChanh_TrachVuSuKien(iMaSuKien, iMaDonViHanhChanh, iMaTrachVuSuKien);

            setTotalPage_DP();
            lbTotalPage_DP.Text = iTotalPage_DP.ToString() + " Trang";
            SubFunction.SetError(lbPage_DP, lbPage_DP.Top, pnPage.Size, "1");
            refreshListView_DP(1);

            //pbOk_DP.Enabled = false;
            //pbOk_DP.Image = Image.FromFile(@"Resources\ChucNang\button_ok_disable.png");
        }

        private void refreshListView_DP(string sTen)
        {
            list_dto_DP = DieuPhoi_BUS.TraCuuDSDieuPhoiTheoTen(sTen);

            setTotalPage_DP();
            lbTotalPage_DP.Text = iTotalPage_DP.ToString() + " Trang";
            SubFunction.SetError(lbPage_DP, lbPage_DP.Top, pnPage.Size, "1");
            refreshListView_DP(1);
        }

        private void refreshListView_DP(int Page)
        {
            SubFunction.ClearlvItem(lvThongTin_DP);
            setRowsPerPage_DP();
            for (int i = (int.Parse(lbPage_DP.Text) - 1) * iRows_DP; i < iRowsPerPage_DP; i++)
            {
                SuKien_HoSo dto_Temp = SuKien_HoSo_BUS.TraCuuSuKien_HoSoTheoMa(list_dto_DP[i].MaSuKien_HoSo);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = list_dto_DP[i].Ma.ToString();
                lvi.SubItems.Add(SubFunction.setSTT(i + 1));
                lvi.SubItems.Add(dto_Temp.HoTen);
                //lvi.SubItems.Add(NhomTrachVu_BUS.TraCuuNhomTrachVuTheoMa(dto_Temp.MaNhomTrachVu).Ten);
                lvi.SubItems.Add(TrachVu_BUS.TraCuuTrachVuTheoMa(dto_Temp.MaTrachVu).Ten);
                lvi.SubItems.Add(dto_Temp.Nganh);
                lvi.SubItems.Add(dto_Temp.DonVi);

                lvThongTin_DP.Items.Add(lvi);
            }
        }
        #endregion



        private bool LayDSDonViHanhChanh_ComboBox(ComboBox cb)
        {
            List<DonViHanhChanh> list_Temp = DonViHanhChanh_BUS.TraCuuDSDonViHanhChanhTheoMaSuKien(iMaSuKien);
            if (list_Temp.Count > 0)
            {
                list_DonViHanhChanh.Clear();
                list_DonViHanhChanh.Add(-1);
                cb.Items.Clear();
                cb.Items.Add("");

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

        private bool LayDSTrachVuSuKien_ComboBox(ComboBox cb)
        {
            List<TrachVuSuKien> list_Temp = TrachVuSuKien_BUS.LayDSTrachVuSuKien();
            if (list_Temp.Count > 0)
            {
                list_TrachVuSuKien.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_TrachVuSuKien.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LayDSTrachVuSuKienTheoMaDonViHanhChanh_ComboBox(ComboBox cb, int iMaDonViHanhChanh)
        {
            List<TrachVuSuKien> list_Temp = TrachVuSuKien_BUS.TraCuuDSTrachVuSuKienTheoMaDonViHanhChanh(iMaDonViHanhChanh);
            if (list_Temp != null)
            {
                list_TrachVuSuKien.Clear();
                cb.Items.Clear();
                for (int i = 0; i < list_Temp.Count; i++)
                {
                    list_TrachVuSuKien.Add(list_Temp[i].Ma);
                    cb.Items.Add(list_Temp[i].Ten);
                }

                return true;
            }
            else
            {
                Form_Notice frm = new Form_Notice("Kiểm tra Đơn vị hành chánh bị trùng!", false);
                return false;
            }
        }



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



        #region Tra cuu HSTD
        private void disableButtonPage_HSTD(int iPage)
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
            disableButtonPage_HSTD(int.Parse(lbPage_HSTD.Text));
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
            disableButtonPage_HSTD(int.Parse(lbPage_HSTD.Text));
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

            disableButtonPage_HSTD(int.Parse(lbPage_HSTD.Text));
        }

        private void tbPage_HSTD_LostFocus(object sender, EventArgs e)
        {
            tbPage_HSTD.Visible = false;

            pbBackPage_HSTD.Enabled = true;
            pbNextPage_HSTD.Enabled = true;
        }

        private void tbPage_HSTD_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbPage_HSTD_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbPage_HSTD.Text.Length > 0)
                {
                    if (SubFunction.TestInt(tbPage_HSTD.Text))
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



        #region Tra cuu DP
        private void disableButtonPage_DP(int iPage)
        {
            if (int.Parse(lbPage_DP.Text) == 1)
            {
                pbBackPage_DP.Enabled = false;
                pbBackPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_back_disable.png");
            }
            else
            {
                pbBackPage_DP.Enabled = true;
                pbBackPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_back.png");
            }

            if (int.Parse(lbPage_DP.Text) == iTotalPage_DP)
            {
                pbNextPage_DP.Enabled = false;
                pbNextPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_next_disable.png");
            }
            else
            {
                pbNextPage_DP.Enabled = true;
                pbNextPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_next.png");
            }
        }

        private void pbBackPage_DP_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage_DP.Text) > 1)
            {
                lbPage_DP.Text = (int.Parse(lbPage_DP.Text) - 1).ToString();
            }
        }

        private void pbBackPage_DP_MouseEnter(object sender, EventArgs e)
        {
            pbBackPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_back_selected.png");
        }

        private void pbBackPage_DP_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage_DP(int.Parse(lbPage_DP.Text));
        }



        private void pbNextPage_DP_Click(object sender, EventArgs e)
        {
            if (int.Parse(lbPage_DP.Text) < iTotalPage_DP)
            {
                lbPage_DP.Text = (int.Parse(lbPage_DP.Text) + 1).ToString();
            }
        }

        private void pbNextPage_DP_MouseEnter(object sender, EventArgs e)
        {
            pbNextPage_DP.Image = Image.FromFile(@"Resources\ChucNang\button_next_selected.png");
        }

        private void pbNextPage_DP_MouseLeave(object sender, EventArgs e)
        {
            disableButtonPage_DP(int.Parse(lbPage_DP.Text));
        }

        private void lbPage_DP_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pbBackPage_DP.Enabled = false;
            pbNextPage_DP.Enabled = false;

            tbPage_DP.Visible = true;
            tbPage_DP.Text = "";
            tbPage_DP.Focus();
        }

        private void lbPage_DP_TextChanged(object sender, EventArgs e)
        {
            refreshListView_DP(int.Parse(lbPage_DP.Text));

            disableButtonPage_DP(int.Parse(lbPage_DP.Text));
        }

        private void tbPage_DP_LostFocus(object sender, EventArgs e)
        {
            tbPage_DP.Visible = false;

            pbBackPage_DP.Enabled = true;
            pbNextPage_DP.Enabled = true;
        }

        private void tbPage_DP_KeyDown(object sender, KeyEventArgs e)
        {
            if (SubFunction.TestNoneNumberInput(e))
            {
                e.SuppressKeyPress = true;
            }

            SubFunction.MuteEnterPress(e);
        }

        private void tbPage_DP_KeyUp(object sender, KeyEventArgs e)
        {
            if (SubFunction.tbPass(e))
            {
                if (tbPage_DP.Text.Length > 0)
                {
                    if (SubFunction.TestInt(tbPage_DP.Text))
                    {
                        if (int.Parse(tbPage_DP.Text) <= iTotalPage_DP)
                        {
                            tbPage_DP.Visible = false;
                            lbPage_DP.Text = tbPage_DP.Text;
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
                    tbPage_DP.Visible = false;
                }
            }
        }
        #endregion



        private void lvThongTin_HSTD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin_HSTD.SelectedItems.Count > 0 && cbDonViHanhChanh.SelectedIndex >= 0 && cbTrachVuSuKien.SelectedIndex >= 0)
            {
                pbTransfer.Enabled = true;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow.png");
            }
            else
            {
                pbTransfer.Enabled = false;
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow_disable.png");
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



        private bool KiemTraHoThamDuBiTrungHoSoDieuPhoi(string sMa)
        {
            for (int i = 0; i < list_dto_DP.Count; i++)
            {
                if (list_dto_DP[i].MaSuKien_HoSo == sMa)
                {
                    Form_Notice frm = new Form_Notice("Kiểm tra Hồ sơ điều phối " + list_dto_DP[i].Ma + " bị trùng!", false);

                    return true;
                }
            }

            return false;
        }

        private void pbTransfer_Click(object sender, EventArgs e)
        {
            if (lvThongTin_HSTD.Focused)
            {
                for (int i = 0; i < lvThongTin_HSTD.SelectedItems.Count; i++)
                {
                    if (!KiemTraHoThamDuBiTrungHoSoDieuPhoi(lvThongTin_HSTD.SelectedItems[i].SubItems[0].Text))
                    {
                        DieuPhoi dto_DP = new DieuPhoi();
                        dto_DP.MaDonViHanhChanh = list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex];
                        dto_DP.MaTrachVuSuKien = list_TrachVuSuKien[cbTrachVuSuKien.SelectedIndex];
                        dto_DP.MaSuKien_HoSo = lvThongTin_HSTD.SelectedItems[i].SubItems[0].Text;

                        if (DieuPhoi_BUS.Insert(dto_DP))
                        {
                            SuKien_DieuPhoi dto_Temp = new SuKien_DieuPhoi();
                            dto_Temp.MaSuKien = iMaSuKien;
                            dto_Temp.MaDieuPhoi = dto_DP.Ma;

                            if (SuKien_DieuPhoi_BUS.Insert(dto_Temp))
                            {
                                cbTrachVuSuKien_SelectedIndexChanged(sender, e);
                            }
                            else
                            {
                                Form_Notice frm = new Form_Notice("Không thể tạo Hồ sơ điều phối!", false);
                            }
                        }
                        else
                        {
                            Form_Notice frm = new Form_Notice("Không thể tạo Hồ sơ điều phối!", false);
                        }
                    }
                }
            }
        }

        private void pbTransfer_MouseEnter(object sender, EventArgs e)
        {
            pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow_selected.png");
        }

        private void pbTransfer_MouseLeave(object sender, EventArgs e)
        {
            if (lvThongTin_HSTD.SelectedItems.Count > 0 || lvThongTin_DP.SelectedItems.Count > 0)
            {
                pbTransfer.Image = Image.FromFile(@"Resources\SuKien\button_dieuphoi_arrow.png");
            }
        }

        private void lvThongTin_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvThongTin_DP.SelectedItems.Count > 0)
            {
                pbXoa_DP.Enabled = true;
                pbXoa_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
            }
            else
            {
                pbXoa_DP.Enabled = false;
                pbXoa_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_disable.png");
            }
        }

        private void lvThongTin_DP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnSort.SortColumn(lvThongTin_DP, e);
        }

        private void cbDonViHanhChanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDonViHanhChanh.SelectedIndex >= 0)
            {
                cbTrachVuSuKien.Items.Clear();
                if (!LayDSTrachVuSuKienTheoMaDonViHanhChanh_ComboBox(cbTrachVuSuKien, list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex]))
                {
                    frm_Notice = new Form_Notice("Chưa có TVSK trong DVHC này!", false);
                }
                else
                {
                    cbTrachVuSuKien.Enabled = true;
                }
            }

            lvThongTin_HSTD_SelectedIndexChanged(sender, e);

            SubFunction.ClearlvItem(lvThongTin_DP);
        }

        private void cbTrachVuSuKien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTrachVuSuKien.SelectedIndex >= 0)
            {
                refreshListView_DP(list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex], list_TrachVuSuKien[cbTrachVuSuKien.SelectedIndex]);
            }
        }



        private bool DeleteHoSo(string sMa)
        {
            //List<SuKien_DieuPhoi> list_SuKien_HoSo_HuanLuyen = SuKien_HoSo_HuanLuyen_BUS.TraCuuDSSuKien_HuanLuyenTheoMaSuKien_HoSo(sMa);
            //foreach (SuKien_HoSo_HuanLuyen dto_Temp in list_SuKien_HoSo_HuanLuyen)
            //{
            //    int iTemp = dto_Temp.MaSuKien_HuanLuyen;
            //    if (!SuKien_HoSo_HuanLuyen_BUS.Delete(dto_Temp.MaSuKien_HoSo, dto_Temp.MaSuKien_HuanLuyen))
            //    {
            //        return false;
            //    }

            //    if (!SuKien_HuanLuyen_BUS.Delete(iTemp))
            //    {
            //        return false;
            //    }
            //}

            //string sPath = "";
            //sPath = Path.Combine(File_Function.getFinalFolder(list_FolderAvatar), setAvatarPath(sMa, SuKien_HoSo_BUS.TraCuuSuKien_HoSoTheoMa(sMa).NgayCapNhat));

            //if (File.Exists(sPath))
            //{
            //    File.Delete(sPath);
            //}

            //if (!SuKien_HoSoThamDu_BUS.Delete(iMaSuKien, sMa))
            //{
            //    return false;
            //}

            //if (!SuKien_HoSo_BUS.Delete(sMa))
            //{
            //    return false;
            //}

            return true;
        }

        private void pbXoa_DP_Click(object sender, EventArgs e)
        {
            pbXoa_DP.Focus();
            frm_Confirm = new Form_Confirm("Đồng ý xóa " + lvThongTin_DP.SelectedItems.Count + " dữ liệu?");
            if (frm_Confirm.Yes)
            {
                for (int i = 0; i < lvThongTin_DP.SelectedItems.Count; i++)
                {
                    if (SuKien_DieuPhoi_BUS.Delete(iMaSuKien, int.Parse(lvThongTin_DP.SelectedItems[i].SubItems[0].Text)))
                    {
                        if (!DieuPhoi_BUS.Delete(int.Parse(lvThongTin_DP.SelectedItems[i].SubItems[0].Text)))
                        {
                            SuKien_DieuPhoi dto_Temp = new SuKien_DieuPhoi();
                            dto_Temp.MaSuKien = iMaSuKien;
                            dto_Temp.MaDieuPhoi = int.Parse(lvThongTin_DP.SelectedItems[i].SubItems[0].Text);
                            SuKien_DieuPhoi_BUS.Insert(dto_Temp);

                            frm_Notice = new Form_Notice("Không thể xóa Hồ Sơ " + lvThongTin_DP.SelectedItems[i].SubItems[0].Text + "!", false);
                            break;
                        }
                    }
                    else
                    {
                        frm_Notice = new Form_Notice("Không thể xóa Hồ Sơ " + lvThongTin_DP.SelectedItems[i].SubItems[0].Text + "!", false);
                        break;
                    }
                }

                refreshListView_DP(list_DonViHanhChanh[cbDonViHanhChanh.SelectedIndex], list_TrachVuSuKien[cbTrachVuSuKien.SelectedIndex]);
            }
        }

        private void pbXoa_DP_MouseEnter(object sender, EventArgs e)
        {
            pbXoa_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa_selected.png");
        }

        private void pbXoa_DP_MouseLeave(object sender, EventArgs e)
        {
            pbXoa_DP.Image = Image.FromFile(@"Resources\ChucNang\icon_xoa.png");
        }
    }
}
