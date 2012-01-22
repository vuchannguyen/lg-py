using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Function;

namespace PlayStationKD
{
    public partial class BaoGioTheoTien : UserControl
    {
        int hours, minutes, seconds;
        long iTime;
        String sMay, sGioChoi, sGioNghi;
        bool bEnter;
        public static bool bTimeCounting;



        private bool bMayBan; //May dang duoc su dung ko the chuyen may

        public bool BMayBan
        {
            get { return bMayBan; }
            set { bMayBan = value; }
        }

        private bool bChuyenMay; //Neu la chuyen tu may khac qua thi khong ghi tien vao file

        public bool BChuyenMay
        {
            get { return bChuyenMay; }
            set { bChuyenMay = value; }
        }
        
        public BaoGioTheoTien()
        {
            InitializeComponent();
        }

        public BaoGioTheoTien(String sMaMay, Point pLocation)
        {
            InitializeComponent();
            this.Location = pLocation;
            lbMay.Text = "Máy " + sMaMay;
            sMay = sMaMay;
            cbChuyenMay.SelectedIndex = 0;
        }

        private void setCountDownTime()
        {
            if (hours.ToString().Length == 1)
            {
                lbHr.Text = "0" + hours.ToString();
            }
            else
            {
                lbHr.Text = hours.ToString();
            }

            if (minutes.ToString().Length == 1)
            {
                lbMin.Text = "0" + minutes.ToString();
            }
            else
            {
                lbMin.Text = minutes.ToString();
            }

            if (seconds.ToString().Length == 1)
            {
                lbSec.Text = "0" + seconds.ToString();
            }
            else
            {
                lbSec.Text = seconds.ToString();
            }
        }

        private void timerDienTu_Tick(object sender, EventArgs e)
        {
            if ((minutes == 0) && (hours == 0) && (seconds == 0))
            {
                timerCountDown.Enabled = false;
                sGioNghi = DateTime.Now.ToShortTimeString();
                String sInfo = "Tiền: " + tbTien.Text;
                this.Controls.Add(new TimesUp(sMay, sInfo, sGioChoi, sGioNghi, true));
                pnBGTT.SendToBack();

                tbTien.Text = "";
                lbTimesIn.Text = "Giờ nghỉ: " + sGioNghi;
                tbTien.ReadOnly = false;
                btBatDau.Visible = true;
                btDung.Visible = false;

                cbChuyenMay.Visible = false;
                cbChuyenMay.Items.Clear();
                cbChuyenMay.Items.Add("Chuyển Máy");
                cbChuyenMay.SelectedIndex = 0;

                bChuyenMay = false;
                bMayBan = false;
                TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = false;

                this.Tag = "";
                lbHr.Focus();
            }
            else
            {
                if (seconds < 1)
                {
                    seconds = 59;

                    if (minutes == 0)
                    {
                        minutes = 59;
                        if (hours != 0)
                        {
                            hours--;
                        }
                    }
                    else
                    {
                        minutes--;
                    }
                }
                else
                {
                    seconds--;
                }

                setCountDownTime();
            }
        }

        private bool TestTien(String sTien)
        {
            if (sTien.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền!");
                tbTien.Focus();

                return false;
            }

            int iTien = 0;
            if (int.TryParse(sTien, out iTien))
            {
                if (iTien < 1000)
                {
                    MessageBox.Show("Số tiền phải lớn hơn 1000!");
                    tbTien.Focus();
                    tbTien.SelectAll();

                    return false;
                }
                else
                {
                    if (iTien % 500 != 0)
                    {
                        MessageBox.Show("Số tiền phải làm tròn đến 500 hoặc 1000!");
                        tbTien.Focus();
                        tbTien.SelectAll();

                        return false;
                    }

                    //iTime = iTien / 500 * 450; //450s = 7.5p -> 500d
                    //iTime = iTien / 500 * 300; //300s = 5p -> 500d 6k 1 tieng
                    iTime = iTien / 1000 * 450; //450s = 7.5p -> 1000d 8k 1 tieng
                    hours = (int)iTime / 3600;
                    if (hours > 23)
                    {
                        MessageBox.Show("Số tiền quá lớn!\nVui lòng kiểm tra lại");
                        tbTien.Focus();
                        tbTien.SelectAll();

                        return false;
                    }
                    minutes = (int)(iTime % 3600) / 60;
                    seconds = (int)(iTime % 3600) % 60;
                }
            }
            else
            {
                MessageBox.Show("Số tiền không hợp lệ!");
                tbTien.Focus();
                tbTien.SelectAll();

                return false;
            }

            return true;
        }

        private void btBatDau_Click(object sender, EventArgs e)
        {
            if (!bChuyenMay)
            {
                if (TestTien(tbTien.Text))
                {
                    tbTien.ReadOnly = true;
                    btBatDau.Visible = false;
                    btDung.Visible = true;
                    sGioChoi = DateTime.Now.ToShortTimeString();
                    lbTimesIn.Text = "Giờ chơi: " + sGioChoi;
                    //this.Tag = "counting";

                    setCountDownTime();
                    timerCountDown.Enabled = true;

                    String sSave = DateTime.Now.ToShortTimeString() + "   " + tbTien.Text + "     ";
                    Save.Save2Text(DateTime.Now.ToString("ddMMyyyy") + ".txt", sSave);

                    bMayBan = true;
                    cbChuyenMay.Visible = true;
                    TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = true;
                }
            }
            else
            {
                tbTien.ReadOnly = true;
                btBatDau.Visible = false;
                btDung.Visible = true;

                setCountDownTime();
                timerCountDown.Enabled = true;

                bMayBan = true;
                cbChuyenMay.Visible = true;
                TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = true;
            }

            bEnter = true;
        }

        private void btDung_Click(object sender, EventArgs e)
        {
            timerCountDown.Enabled = false;
            tbTien.ReadOnly = false;

            cbChuyenMay.Visible = false;
            cbChuyenMay.Items.Clear();
            cbChuyenMay.Items.Add("Chuyển Máy");
            cbChuyenMay.SelectedIndex = 0;

            bChuyenMay = false;
            bMayBan = false;
            TinhTien.listMayBan[int.Parse(lbMay.Text.Substring(4)) - 1] = false;

            tbTien.Text = "";
            lbHr.Text = "00";
            lbMin.Text = "00";
            lbSec.Text = "00";
            lbTimesIn.Text = "Giờ nghỉ: " + DateTime.Now.ToShortTimeString();

            this.Tag = "";
            btBatDau.Visible = true;
            btDung.Visible = false;
        }

        private void BaoGioTheoTien_Load(object sender, EventArgs e)
        {
            btBatDau.Visible = true;
            btDung.Visible = false;
        }

        private void tbTien_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = SubFunction.TestNoneNumberInput(e);

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                bEnter = false;
            }
        }

        private void tbTien_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbTien.TextLength > 3 && e.KeyCode == System.Windows.Forms.Keys.Enter && !bEnter)
            {
                btBatDau_Click(sender, e);
            }
        }

        private void BaoGioTheoTien_EnabledChanged(object sender, EventArgs e)
        {
            if (timerCountDown.Enabled)
            {
                bTimeCounting = true;
                pnBGTT.Enabled = true;
            }
            else
            {
                //bTimeCounting = false;
                //pnBGTT.Enabled = false;
            }
        }

        private void cbChuyenMay_DropDown(object sender, EventArgs e)
        {
            cbChuyenMay.Items.Clear();
            for (int i = 0; i < TinhTien.listMayBan.Count; i++)
            {
                if (!TinhTien.listMayBan[i])
                {
                    cbChuyenMay.Items.Add("Máy " + (i + 1).ToString());
                }
            }
        }

        private void cbChuyenMay_DropDownClosed(object sender, EventArgs e)
        {
            if (cbChuyenMay.SelectedIndex == -1)
            {
                cbChuyenMay.Items.Clear();
                cbChuyenMay.Items.Add("Chuyển Máy");
                cbChuyenMay.SelectedIndex = 0;
            }
            else
            {
                TinhTien.lrbBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].Checked = true;

                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].sGioChoi = sGioChoi;

                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].tbTien.Text = tbTien.Text;
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].hours = int.Parse(lbHr.Text);
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].minutes = int.Parse(lbMin.Text);
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].seconds = int.Parse(lbSec.Text);
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbSec.Text = lbSec.Text;
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].lbTimesIn.Text = lbTimesIn.Text;
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].bChuyenMay = true;
                TinhTien.lBGTT[int.Parse(cbChuyenMay.Text.Substring(4)) - 1].btBatDau_Click(sender, e);

                string sChuyenMay = cbChuyenMay.Text;

                btDung_Click(sender, e);
                lbTimesIn.Text = "Chuyển " + sChuyenMay + ": " + DateTime.Now.ToShortTimeString();
            }
        }

        private void tbTien_TextChanged(object sender, EventArgs e)
        {
            if (tbTien.TextLength > 3)
            {
                btBatDau.Enabled = true;
            }
            else
            {
                btBatDau.Enabled = false;
            }
        }
    }
}