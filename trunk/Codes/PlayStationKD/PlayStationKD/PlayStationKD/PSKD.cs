using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Function;
using DTO;
using BUS;

namespace PlayStationKD
{
    public partial class PlayStationKD : Form
    {
        bool bEnter = false;
        bool bChooseGame = false;
        List<RadioButton> lrbBGTT = new List<RadioButton>();
        List<RadioButton> lrbBGTTG = new List<RadioButton>();
        List<RadioButton> lrbTS = new List<RadioButton>();
        List<Panel> lpnBaoGio = new List<Panel>();
        List<BaoGioTheoTien> lBGTT = new List<BaoGioTheoTien>();
        List<BaoGioTheoThoiGian> lBGTTG = new List<BaoGioTheoThoiGian>();
        List<TinhTienTraSau> lTTTS = new List<TinhTienTraSau>();

        private ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();

        public static string sNameProgram;

        public PlayStationKD()
        {
            InitializeComponent();
            sNameProgram = this.Name;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog();
        }

        private void PlayStationKD_Load(object sender, EventArgs e)
        {
            cbMayDSG.Select();
            tabAll.Dock = DockStyle.Fill;
            tabAll_SelectedIndexChanged(sender, e);

            pnDanhSachGame.Location = SetLocation(pnDanhSachGame.Size);

            pnMay.Size = new Size(660, 560);
            pnMay.Location = SetLocation(pnMay.Size);

            pnGame.Size = new Size(660, 550);
            pnGame.Location = SetLocation(pnMay.Size);

            pnGameMay.Size = new Size(660, 550);
            pnGameMay.Location = SetLocation(pnGameMay.Size);

            pnTimKiem.Location = SetLocation(pnTimKiem.Size);

            pnTT.Location = SetLocation(pnTT.Size);

            UC_DoanhThu uc = new UC_DoanhThu();
            uc.Location = SetLocation(new Size(680, 600));
            tabDoanhThu.Controls.Add(uc);

            lbDateTT.Text = "Ngày:   " + DateTime.Now.ToString("dd/MM/yyyy");
            //LuaChonBaoGio();

            TinhTien tt = new TinhTien();
            tt.Location = new Point(0, 15);
            pnBaoGio.Controls.Add(tt);
        }

        private void PlayStationKD_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool bMayBan = false;
            for (int i = 0; i < TinhTien.listMayBan.Count; i++)
            {
                if (TinhTien.listMayBan[i])
                {
                    bMayBan = true;
                    break;
                }
            }

            if (bMayBan)
            {
                DialogResult bClose = MessageBox.Show("Mọi hoạt động tính tiền sẽ bị mất!\n                    Đồng ý?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (bClose == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }



        #region SubFuntions
        private Point SetLocation(Size size)
        {
            return (new Point((this.tabAll.Width - size.Width) / 2, (this.tabAll.Height - 30 - size.Height) / 2));
        }

        private void SortColumn(ListView lv, ColumnClickEventArgs e)
        {
            lv.ListViewItemSorter = lvwColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            lv.Sort();
        }  

        private void ClearlvItem(ListView lv)
        {
            if (lv.Items.Count > 0)
            {
                lv.Items.Clear();
            }
        }

        private void MuteEnterPress(KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bEnter = false;
            }
        }

        private void NextFocus(TextBox tb, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                bEnter = true;
                tb.Focus();
                tb.SelectAll();
            }
        }

        private void SelectDanhSachGame(ListView lv)
        {
            List<DanhSachGame_DTO> lGame = DanhSachGame_BUS.SelectDanhSachGame();
            ClearlvItem(lv);
            for (int i = 0; i < lGame.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = setSTT(i + 1);
                lvi.SubItems.Add(lGame[i].Ma);
                lvi.SubItems.Add(lGame[i].Ten);
                lvi.SubItems.Add(lGame[i].NguoiChoi.ToString());
                lvi.SubItems.Add(lGame[i].TheLoai);
                lvi.SubItems.Add(lGame[i].GhiChu);
                lv.Items.Add(lvi);
            }
        }

        private void SelectDanhSachGame_May(ListView lv, String sMaMay)
        {
            List<DanhSachGame_DTO> lGame = DanhSachGame_BUS.SelectDanhSachGame_May(sMaMay);
            ClearlvItem(lv);
            for (int i = 0; i < lGame.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = setSTT(i + 1);
                lvi.SubItems.Add(lGame[i].Ma);
                lvi.SubItems.Add(lGame[i].Ten);
                lvi.SubItems.Add(lGame[i].NguoiChoi.ToString());
                lvi.SubItems.Add(lGame[i].TheLoai);
                lvi.SubItems.Add(lGame[i].GhiChu);
                lv.Items.Add(lvi);
            }
        }

        private void SelectCbMay(ComboBox cb, bool bAll)
        {
            List<May_DTO> lMay = May_BUS.SelectMay();

            cb.Items.Clear();
            if (bAll)
            {
                cb.Items.Add("Tất cả");
            }
            for (int i = 0; i < lMay.Count; i++)
            {
                cb.Items.Add(lMay[i].Ma);
            }
            cb.SelectedIndex = 0;
        }

        private void SelectlvMay(ListView lv)
        {
            List<May_DTO> lMay = May_BUS.SelectMay();
            ClearlvItem(lv);
            for (int i = 0; i < lMay.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = setSTT(i + 1);
                lvi.SubItems.Add(lMay[i].Ma);
                lvi.SubItems.Add(lMay[i].GhiChu);
                lv.Items.Add(lvi);
            }
        }

        private void FindGame(ListView lv, String sTen)
        {
            List<DanhSachGame_DTO> lGame = DanhSachGame_BUS.FindGame(sTen);
            ClearlvItem(lv);
            for (int i = 0; i < lGame.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = setSTT(i + 1);
                lvi.SubItems.Add(lGame[i].Ma);
                lvi.SubItems.Add(lGame[i].Ten);
                lv.Items.Add(lvi);
            }
        }

        private void FindGameMay(ListView lv, String sTen, String sMaMay)
        {
            List<DanhSachGame_DTO> lGame = DanhSachGame_BUS.FindGameMay(sTen, sMaMay);
            ClearlvItem(lv);

            if (sTen.Length > 0)
            {
                for (int i = 0; i < lGame.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = setSTT(i + 1);
                    lvi.SubItems.Add(lGame[i].Ten);
                    lvi.SubItems.Add(lGame[i].NguoiChoi.ToString());
                    lvi.SubItems.Add(lGame[i].TheLoai);
                    lvi.SubItems.Add(lGame[i].MaMay);
                    lvi.SubItems.Add(lGame[i].GhiChu);
                    lv.Items.Add(lvi);
                }
            }
        }

        private void rbBGTT_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = true;
            lBGTT[i].Visible = true;
            lBGTTG[i].Enabled = false;
            lBGTTG[i].Visible = false;
            lTTTS[i].Enabled = false;
            lTTTS[i].Visible = false;
        }

        private void rbBGTTG_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = false;
            lBGTT[i].Visible = false;
            lBGTTG[i].Enabled = true;
            lBGTTG[i].Visible = true;
            lTTTS[i].Enabled = false;
            lTTTS[i].Visible = false;
        }

        private void rbTTTS_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton temp = (RadioButton)sender;
            int i = int.Parse(temp.Name);

            if (temp.Checked)
            {
                temp.ForeColor = Color.Red;
            }
            else
            {
                temp.ForeColor = Color.Black;
            }

            lBGTT[i].Enabled = false;
            lBGTT[i].Visible = false;
            lBGTTG[i].Enabled = false;
            lBGTTG[i].Visible = false;
            lTTTS[i].Enabled = true;
            lTTTS[i].Visible = true;
        }

        private void LuaChonBaoGio()
        {
            pnBaoGio.Controls.Clear();
            lpnBaoGio.Clear();
            lBGTT.Clear();
            lBGTTG.Clear();
            List<May_DTO> lMay = May_BUS.SelectMay();
            int x = 0;
            int y = 0;

            Font newFont = new Font("Arial", 12F);

            for (int i = 0; i < lMay.Count; i++)
            {
                if (i > 0 && i % 3 == 0)
                {
                    x = 0;
                    y++;
                }

                RadioButton rbBGTT = new RadioButton();
                rbBGTT.Name = i.ToString();
                rbBGTT.Text = "Tiền";
                rbBGTT.Checked = true;
                rbBGTT.Location = new Point(2, 0);
                rbBGTT.Size = new Size(58, 24);
                rbBGTT.Font = newFont;
                rbBGTT.CheckedChanged += new EventHandler(rbBGTT_CheckedChanged);

                rbBGTT.ForeColor = Color.Red;

                RadioButton rbBGTTG = new RadioButton();
                rbBGTTG.Name = i.ToString();
                rbBGTTG.Text = "TGian";
                rbBGTTG.Location = new Point(60, 0);
                rbBGTTG.Size = new Size(68, 24);
                rbBGTTG.Font = newFont;
                rbBGTTG.CheckedChanged += new EventHandler(rbBGTTG_CheckedChanged);

                RadioButton rbTTTS = new RadioButton();
                rbTTTS.Name = i.ToString();
                rbTTTS.Text = "Trả Sau";
                rbTTTS.Location = new Point(130, 0);
                rbTTTS.Size = new Size(82, 24);
                rbBGTTG.Font = newFont;
                rbTTTS.CheckedChanged += new EventHandler(rbTTTS_CheckedChanged);

                Panel pnTemp = new Panel();
                pnTemp.Size = new Size(215, 205);
                pnTemp.Location = new Point(x * 235 + 20, y * 220 + 15);
                lpnBaoGio.Add(pnTemp);

                lBGTT.Add(new BaoGioTheoTien(lMay[i].Ma, new Point(0, 20)));
                lBGTTG.Add(new BaoGioTheoThoiGian(lMay[i].Ma, new Point(0, 20)));
                lTTTS.Add(new TinhTienTraSau(lMay[i].Ma, new Point(0, 20)));

                lpnBaoGio[i].Controls.Add(rbBGTT);
                lpnBaoGio[i].Controls.Add(rbBGTTG);
                lpnBaoGio[i].Controls.Add(rbTTTS);

                lpnBaoGio[i].Controls.Add(lBGTT[i]);
                lpnBaoGio[i].Controls.Add(lBGTTG[i]);
                lpnBaoGio[i].Controls.Add(lTTTS[i]);
                pnBaoGio.Controls.Add(lpnBaoGio[i]);
                x++;
            }
        }
        #endregion



        #region tbTest
        private bool TestNull(TextBox tb)
        {
            if (tb.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập " + tb.Tag.ToString() + "!");
                tb.Focus();
                return false;
            }

            return true;
        }

        private bool TestNguoiChoi(TextBox tb)
        {
            int iTest;
            if (!int.TryParse(tb.Text, out iTest))
            {
                MessageBox.Show("Số lượng người chơi không hợp lệ!\nVui lòng nhập lại.");
                tb.Text = "";
                tb.Focus();
                return false;
            }

            return true;
        }
        #endregion



        private void tabAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabAll.SelectedIndex == 0)
            {
                //
            }

            if (tabAll.SelectedIndex == 1)
            {
                //dateTimePickerDT.Value = DateTime.Now;
                //showDoanhThu(dateTimePickerDT.Value.ToString("ddMMyyyy") + ".txt");
            }

            if (tabAll.SelectedIndex == 2)
            {
                SelectCbMay(cbMayDSG, true);
                SelectDanhSachGame(lvGameDSG);

                lbTotalDSG.Text = "Tổng cộng: " + lvGameDSG.Items.Count.ToString();
            }

            if (tabAll.SelectedIndex == 3)
            {
                rbMay.Checked = true;
            }

            if (tabAll.SelectedIndex == 4)
            {
                SelectCbMay(cbMayTK, true);
            }
        }

        private void tabAll_MouseClick(object sender, MouseEventArgs e)
        {
            if (tabAll.SelectedIndex == 0)
            {
                cbMayDSG.Select();
            }

            if (tabAll.SelectedIndex == 1)
            {
                tbMaQLM.Focus();
            }

            if (tabAll.SelectedIndex == 2)
            {
                tbGameTK.Focus();
            }
        }



        #region DanhSachGame
        private void lvDanhSachGame_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvGameDSG, e);
        }

        private void cbMayDSG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMayDSG.SelectedIndex == 0)
            {
                SelectDanhSachGame(lvGameDSG);
            }
            else
            {
                SelectDanhSachGame_May(lvGameDSG, cbMayDSG.SelectedItem.ToString());
            }

            lbTotalDSG.Text = "Tổng cộng: " + lvGameDSG.Items.Count.ToString();
        }
        #endregion



        #region QuanLy
        #region May
        private void lvDanhSachMay_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvMayQLM, e);
        }

        private void lvDanhSachMay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvMayQLM.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvMayQLM.SelectedItems[0];
                this.tbMaQLM.Text = lvi.SubItems[1].Text;
                this.tbGhiChuQLM.Text = lvi.SubItems[2].Text;
            }
        }

        private void rbMay_CheckedChanged(object sender, EventArgs e)
        {
            pnMay.Visible = true;
            pnMay.Enabled = true;
            pnGame.Visible = false;
            pnGame.Enabled = false;
            pnGameMay.Visible = false;
            pnGameMay.Enabled = false;

            rbThemQLM.Checked = true;
            SelectlvMay(lvMayQLM);

            tbMaQLM.Text = "";
            tbGhiChuQLM.Text = "";
            tbMaQLM.Focus();
        }

        private void rbThemQLM_CheckedChanged(object sender, EventArgs e)
        {
            btThemQLM.Enabled = true;
            btXoaQLM.Enabled = false;
            btSuaQLM.Enabled = false;

            tbMaQLM.ReadOnly = false;
            tbGhiChuQLM.ReadOnly = false;

            tbMaQLM.Text = "";
            tbGhiChuQLM.Text = "";
        }

        private void rbXoaQLM_CheckedChanged(object sender, EventArgs e)
        {
            btThemQLM.Enabled = false;
            btXoaQLM.Enabled = true;
            btSuaQLM.Enabled = false;

            tbMaQLM.ReadOnly = true;
            tbGhiChuQLM.ReadOnly = true;

            tbMaQLM.Text = "";
            tbGhiChuQLM.Text = "";
        }

        private void rbSuaQLM_CheckedChanged(object sender, EventArgs e)
        {
            btThemQLM.Enabled = false;
            btXoaQLM.Enabled = false;
            btSuaQLM.Enabled = true;

            tbMaQLM.ReadOnly = true;
            tbGhiChuQLM.ReadOnly = false;

            tbMaQLM.Text = "";
            tbGhiChuQLM.Text = "";
        }

        private void lvMayQLM_KeyDown(object sender, KeyEventArgs e)
        {
            bEnter = false;
        }

        private void lvMayQLM_KeyUp(object sender, KeyEventArgs e)
        {
            if (rbXoaQLM.Checked)
            {
                if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && !bEnter)
                {
                    btXoaQLM_Click(sender, e);
                }
            }
        }

        private void btThemQLM_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLM))
            {
                if (May_BUS.AddMay(tbMaQLM.Text, tbGhiChuQLM.Text))
                {
                    SelectlvMay(lvMayQLM);
                    LuaChonBaoGio();
                    tbMaQLM.Focus();
                }
                else
                {
                    MessageBox.Show("Không thể thêm!\nĐã có máy " + tbMaQLM.Text + ".");
                }
            }

            bEnter = true;
        }

        private void btXoaQLM_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLM))
            {
                if (MessageBox.Show("Ngưng tính tiền trước khi xóa!?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (May_BUS.DeleteMay(tbMaQLM.Text))
                    {
                        tbMaQLM.Text = "";
                        tbGhiChuQLM.Text = "";
                        LuaChonBaoGio();
                        tbMaQLM.Focus();
                        SelectlvMay(lvMayQLM);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa!\nMáy " + tbMaQLM.Text + " phải được xóa hết game trước.");
                    }
                }
            }

            bEnter = true;
        }

        private void btSuaQLM_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLM))
            {
                if (May_BUS.UpdateMay(tbMaQLM.Text, tbGhiChuQLM.Text))
                {
                    tbMaQLM.Focus();
                    SelectlvMay(lvMayQLM);
                }
                else
                {
                    MessageBox.Show("Không thể sửa!\nVui lòng thử lại.");
                }
            }

            bEnter = true;
        }

        private void EnterButtonQLM(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                if (rbThemQLM.Checked == true)
                {
                    btThemQLM_Click(sender, e);
                }

                if (rbXoaQLM.Checked == true)
                {
                    btXoaQLM_Click(sender, e);
                }

                if (rbSuaQLM.Checked == true)
                {
                    btSuaQLM_Click(sender, e);
                }
            }
        }

        private void tbMaQLM_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbMaQLM_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbGhiChuQLM, e);
        }

        private void tbGhiChuQLM_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbGhiChuQLM_KeyUp(object sender, KeyEventArgs e)
        {
            EnterButtonQLM(sender, e);
        }
        #endregion



        #region Game
        private void lvGameQLG_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvGameQLG, e);
        }

        private void lvGameQLG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvGameQLG.SelectedItems.Count > 0)
            {
                ListViewItem lvi = this.lvGameQLG.SelectedItems[0];
                if (!rbThemQLG.Checked)
                {
                    this.tbMaQLG.Text = lvi.SubItems[1].Text;
                    this.tbTenQLG.Text = lvi.SubItems[2].Text;
                    this.tbNguoiChoiQLG.Text = lvi.SubItems[3].Text;
                    this.tbTheLoaiQLG.Text = lvi.SubItems[4].Text;
                    this.tbGhiChuQLG.Text = lvi.SubItems[5].Text;
                }
            }
        }

        private void ThemMaGame()
        {
            if (lvGameQLG.Items.Count < 10)
            {
                tbMaQLG.Text = "00" + (lvGameQLG.Items.Count + 1).ToString();
            }

            if (lvGameQLG.Items.Count >= 10 && lvGameQLG.Items.Count <= 99)
            {
                tbMaQLG.Text = "0" + (lvGameQLG.Items.Count + 1).ToString();
            }

            if (lvGameQLG.Items.Count > 99)
            {
                tbMaQLG.Text = (lvGameQLG.Items.Count + 1).ToString();
            }
        }

        private void rbGame_CheckedChanged(object sender, EventArgs e)
        {
            pnMay.Visible = false;
            pnMay.Enabled = false;
            pnGame.Visible = true;
            pnGame.Enabled = true;
            pnGameMay.Visible = false;
            pnGameMay.Enabled = false;

            rbThemQLG.Checked = true;
            SelectDanhSachGame(lvGameQLG);
            ThemMaGame();

            tbTenQLG.Text = "";
            tbNguoiChoiQLG.Text = "";
            tbTheLoaiQLG.Text = "";
            tbGhiChuQLG.Text = "";
            tbNguoiChoiQLG.Focus();
        }

        private void rbThemQLG_CheckedChanged(object sender, EventArgs e)
        {
            ThemMaGame();

            btThemQLG.Enabled = true;
            btXoaQLG.Enabled = false;
            btSuaQLG.Enabled = false;

            tbNguoiChoiQLG.ReadOnly = false;
            tbTheLoaiQLG.ReadOnly = false;
            tbTenQLG.ReadOnly = false;
            tbGhiChuQLG.ReadOnly = false;

            tbTenQLG.Text = "";
            tbNguoiChoiQLG.Text = "";
            tbTheLoaiQLG.Text = "";
            tbGhiChuQLG.Text = "";
        }

        private void rbXoaQLG_CheckedChanged(object sender, EventArgs e)
        {
            btThemQLG.Enabled = false;
            btXoaQLG.Enabled = true;
            btSuaQLG.Enabled = false;

            tbNguoiChoiQLG.ReadOnly = true;
            tbTheLoaiQLG.ReadOnly = true;
            tbTenQLG.ReadOnly = true;
            tbGhiChuQLG.ReadOnly = true;

            tbMaQLG.Text = "";
            tbTenQLG.Text = "";
            tbNguoiChoiQLG.Text = "";
            tbTheLoaiQLG.Text = "";
            tbGhiChuQLG.Text = "";
        }

        private void rbSuaQLG_CheckedChanged(object sender, EventArgs e)
        {
            btThemQLG.Enabled = false;
            btXoaQLG.Enabled = false;
            btSuaQLG.Enabled = true;
            
            tbNguoiChoiQLG.ReadOnly = false;
            tbTheLoaiQLG.ReadOnly = false;
            tbTenQLG.ReadOnly = false;
            tbGhiChuQLG.ReadOnly = false;

            tbMaQLG.Text = "";
            tbTenQLG.Text = "";
            tbNguoiChoiQLG.Text = "";
            tbTheLoaiQLG.Text = "";
            tbGhiChuQLG.Text = "";
        }

        private void btThemQLG_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLG))
            {
                if (DanhSachGame_BUS.AddGame(tbMaQLG.Text, tbTenQLG.Text, tbNguoiChoiQLG.Text, tbTheLoaiQLG.Text, tbGhiChuQLG.Text))
                {
                    SelectDanhSachGame(lvGameQLG);
                    ThemMaGame();
                }
                else
                {
                    MessageBox.Show("Không thể thêm!\nĐã có game " + tbMaQLG.Text + ".");
                }
            }

            bEnter = true;
        }

        private void btXoaQLG_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLG))
            {
                if (DanhSachGame_BUS.DeleteGame(tbMaQLG.Text))
                {
                    SelectDanhSachGame(lvGameQLG);

                    tbMaQLG.Text = "";
                    tbTenQLG.Text = "";
                    tbNguoiChoiQLG.Text = "";
                    tbTheLoaiQLG.Text = "";
                    tbGhiChuQLG.Text = "";
                    tbMaQLG.Focus();
                }
                else
                {
                    MessageBox.Show("Không thể xóa!\nGame " + tbMaQLG.Text + " vẫn còn trong máy.");
                }
            }

            bEnter = true;
        }

        private void btSuaQLG_Click(object sender, EventArgs e)
        {
            if (TestNull(tbMaQLG))
            {
                if (DanhSachGame_BUS.UpdateGame(tbMaQLG.Text, tbTenQLG.Text, tbNguoiChoiQLG.Text, tbTheLoaiQLG.Text, tbGhiChuQLG.Text))
                {
                    tbMaQLG.Focus();
                    SelectDanhSachGame(lvGameQLG);
                }
                else
                {
                    MessageBox.Show("Không thể sửa!\nVui lòng thử lại.");
                }
            }

            bEnter = true;
        }

        private void EnterButtonQLG(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                if (rbThemQLG.Checked == true)
                {
                    btThemQLG_Click(sender, e);
                }

                if (rbXoaQLG.Checked == true)
                {
                    btXoaQLG_Click(sender, e);
                }

                if (rbSuaQLG.Checked == true)
                {
                    btSuaQLG_Click(sender, e);
                }

                tbNguoiChoiQLG.Focus();
                tbNguoiChoiQLG.SelectAll();
            }
        }

        private void tbMaQLG_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbMaQLG_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbNguoiChoiQLG, e);
        }

        private void tbNguoiChoiQLG_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbNguoiChoiQLG_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbTheLoaiQLG, e);
        }

        private void tbTheLoaiQLG_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbTheLoaiQLG_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbTenQLG, e);
        }

        private void tbTenQLG_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbTenQLG_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbGhiChuQLG, e);
        }

        private void tbGhiChuQLG_KeyDown(object sender, KeyEventArgs e)
        {
            MuteEnterPress(e);
        }

        private void tbGhiChuQLG_KeyUp(object sender, KeyEventArgs e)
        {
            EnterButtonQLG(sender, e);
        }
        #endregion



        #region GameMay
        private void lvGameQLGM_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvGameQLGM, e);
        }

        private void lvGameMayQLMG_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvGameMayQLGM, e);
        }

        private void lvGameQLGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvGameQLGM.SelectedItems.Count > 0)
            {
                bChooseGame = true;
                ListViewItem lvi = this.lvGameQLGM.SelectedItems[0];
                this.tbMaGameQLGM.Text = lvi.SubItems[1].Text;
                this.tbTenGameQLGM.Text = lvi.SubItems[2].Text;
            }

            bEnter = false;
        }

        private void rbGame_May_CheckedChanged(object sender, EventArgs e)
        {
            pnMay.Visible = false;
            pnMay.Enabled = false;
            pnGame.Visible = false;
            pnGame.Enabled = false;
            pnGameMay.Visible = true;
            pnGameMay.Enabled = true;

            SelectCbMay(cbMayQLGM, false);
            SelectDanhSachGame(lvGameQLGM);
            SelectDanhSachGame_May(lvGameMayQLGM, cbMayQLGM.SelectedItem.ToString());

            cbMayQLGM.SelectedIndex = 0;
            tbMaGameQLGM.Text = "";
            tbTenGameQLGM.Text = "";
            tbTenGameQLGM.Focus();
        }

        private void cbMayQLGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDanhSachGame_May(lvGameMayQLGM, cbMayQLGM.SelectedItem.ToString());
        }

        private void tbTenGameQLGM_TextChanged(object sender, EventArgs e)
        {
            if (!bChooseGame)
            {
                tbMaGameQLGM.Text = "";
                FindGame(lvGameQLGM, tbTenGameQLGM.Text);
            }

            bChooseGame = false;
        }

        private void lvGameQLGM_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !bEnter)
            {
                btThemQLGM_Click(sender, e);
            }
        }

        private void lvGameMayQLGM_KeyDown(object sender, KeyEventArgs e)
        {
            bEnter = false;
        }

        private void lvGameMayQLGM_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) && !bEnter)
            {
                btXoaQLGM_Click(sender, e);
            }
        }

        private void btThemQLGM_Click(object sender, EventArgs e)
        {
            if (tbMaGameQLGM.Text.Length != 0)
            {
                if (GameMay_BUS.AddGameMay(tbMaGameQLGM.Text, cbMayQLGM.SelectedItem.ToString()))
                {
                    SelectDanhSachGame_May(lvGameMayQLGM, cbMayQLGM.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("Không thể thêm!\nĐã có game " + tbMaGameQLGM.Text + ".");
                }
            }
            else
            {
                MessageBox.Show("Không thể thêm!\nPhải chọn game trước.");
            }

            bEnter = true;
        }

        private void btXoaQLGM_Click(object sender, EventArgs e)
        {
            if (GameMay_BUS.DeleteGameMay(lvGameMayQLGM.SelectedItems[0].SubItems[1].Text, cbMayQLGM.SelectedItem.ToString()))
            {
                SelectDanhSachGame_May(lvGameMayQLGM, cbMayQLGM.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("Không thể xóa!\nVui lòng thử lại");
            }

            bEnter = true;
        }
        #endregion
        #endregion



        #region TimKiem
        private void tbTimKiem_TextChanged(object sender, EventArgs e)
        {
            FindGameMay(lvGameMayTK, tbGameTK.Text, "Tất cả");
        }

        private void lvGameMayTK_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortColumn(lvGameMayTK, e);
        }

        private void tbGameTK_TextChanged(object sender, EventArgs e)
        {
            FindGameMay(lvGameMayTK, tbGameTK.Text, cbMayTK.SelectedItem.ToString());
        }

        private void cbMayTK_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindGameMay(lvGameMayTK, tbGameTK.Text, cbMayTK.SelectedItem.ToString());
        }
        #endregion



        #region ThuChi
        private bool TestThuChi(TextBox tb)
        {
            if (tb.Text.Length != 0)
            {
                long lTemp = 0;
                if (long.TryParse(tb.Text, out lTemp))
                {
                    if (lTemp < 500)
                    {
                        tb.Focus();
                        tb.SelectAll();

                        return false;
                    }

                    return true;
                }
                else
                {
                    tb.Focus();
                    tb.SelectAll();

                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void btCapNhat_Click(object sender, EventArgs e)
        {
            if (TestThuChi(tbThu) && TestThuChi(tbChi))
            {
                if (tbThu.Text.Length != 0)
                {
                    String sSave = DateTime.Now.ToShortTimeString() + "   " + tbThu.Text + "     " + tbLyDoThu.Text + " (THU)";
                    Save.Save2Text(DateTime.Now.ToString("ddMMyyyy") + ".txt", sSave);
                    tbThu.Text = "";
                    tbLyDoThu.Text = "";

                    //tbTongTien.Text = Save.getTongTien(DateTime.Now.ToString("ddMMyyyy") + ".txt").ToString();
                }

                if (tbChi.Text.Length != 0)
                {
                    String sSave = DateTime.Now.ToShortTimeString() + "   " + tbChi.Text + "     " + tbLyDoChi.Text + " (CHI)";
                    Save.Save2Text(DateTime.Now.ToString("ddMMyyyy") + ".txt", sSave);
                    tbChi.Text = "";
                    tbLyDoChi.Text = "";

                    //tbTongTien.Text = Save.getTongTien(DateTime.Now.ToString("ddMMyyyy") + ".txt").ToString();
                }
            }
        }

        //private void btChiTiet_Click(object sender, EventArgs e)
        //{
        //    if (File.Exists(DateTime.Now.ToString("ddMMyyyy") + ".txt"))
        //    {
        //        Process.Start(DateTime.Now.ToString("ddMMyyyy") + ".txt");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Hôm nay chưa có chi tiết!");
        //    }
        //}

        private void tbThu_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = SubFunction.TestNoneNumberInput(e);

            MuteEnterPress(e);
        }

        private void tbThu_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbLyDoThu, e);
        }

        private void tbChi_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = SubFunction.TestNoneNumberInput(e);

            MuteEnterPress(e);
        }

        private void tbChi_KeyUp(object sender, KeyEventArgs e)
        {
            NextFocus(tbLyDoChi, e);
        }
        #endregion



        private String setSTT(int i)
        {
            if (i < 10)
            {
                return "00" + i.ToString();
            }
            else
            {
                if (i >= 10 && i <= 99)
                {
                    return "0" + i.ToString();
                }

                return i.ToString();
            }
        }

        private void PlayStationKD_SizeChanged(object sender, EventArgs e)
        {
            this.Size = new Size(1014, 724);

            pnDanhSachGame.Location = SetLocation(pnDanhSachGame.Size);

            pnMay.Location = SetLocation(pnMay.Size);

            pnGame.Location = SetLocation(pnMay.Size);

            pnGameMay.Location = SetLocation(pnGameMay.Size);

            pnTimKiem.Location = SetLocation(pnTimKiem.Size);

            pnTT.Location = SetLocation(pnTT.Size);
        }

        private void chuôngHếtGiờToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sound sound = new Sound();
            sound.ShowDialog();
        }
    }
}